using System;
using System.IO;
using RestSharp;
using System.Collections.Generic;
using System.Text;
using System.Net;
using SystemConfiguration;
using CoreFoundation;

namespace CoreDataService
{
	public class Connection
	{

		// check if connection is available and accessible
		protected Boolean CheckConnection(out string errmsg) {

			errmsg = "";
			NetworkStatus internetStatus = Reachability.InternetConnectionStatus();
			if ( internetStatus == NetworkStatus.NotReachable ) {
				errmsg = "Internet connection is not available";
				return false;
			} else if ( !Reachability.IsHostReachable(Settings.ws_address) ) {
				errmsg = "The web service can not be reached";
				return false;
			}
			
			return true;
		}



		// build and return request block
		protected Dictionary<string, string> BuildRequest ( string svcstring )
		{
			Dictionary<string, string> request = new Dictionary<string, string> ();
			request.Add ("ADDRESS", Settings.ws_address);
			request.Add ("PATH", Settings.ws_basepath + svcstring);
			request.Add ("TYPE", "table");
			request.Add ("FORMAT", "json");
			return request;
		}


		
		// MakeRequest Function
		// Obtain data from remote web service
		// Parameters
		//		request:
		//				address		host address
		//				path		svc path on the host
		//				type		table | ...
		//				format		json
		//		data (out)			data obtained or error msg
		// Return
		//		true	sucessful
		//		false	returned with error in errmsg
		protected Boolean MakeRequest (Dictionary<string, string> request, out string data)
		{
			string format = request["FORMAT"].ToUpper ();
			data = null;

			try
			{
				var client = new RestClient(request["ADDRESS"]);

				// build request
				RestRequest req = null;
				if (format == "JSON") {
					req = new RestRequest (request["PATH"], Method.GET);
					req.AddHeader("Accept", "application/json");
					req.Timeout = Settings.ws_timeout;
				}
				else {
					// handle other types of the formats
				}

				// send the request and receive its result
				if ( Settings.ws_SyncRequest ) {
					// sync transfer
					var response = client.Execute(req);
					if (response.ErrorException != null)
					{
						throw new Exception(response.ErrorMessage);
					}

					data = response.Content;

				} else {
					// async transfer
					client.ExecuteAsync(req, resp => {
						if (resp.ResponseStatus == ResponseStatus.Completed)
						{
							//File.WriteAllBytes (request["LOCALPATH"], client.DownloadData (req));

						} else if (resp.ErrorException != null)
						{
							throw new Exception(resp.ErrorMessage);
						}
					});
				}

			}
			catch (Exception e)
			{
				data = e.Source + " -> " + e.Message;
				return false;
			}

			return true;
		}

	}
	
	
	
	
	
	public enum NetworkStatus {
		NotReachable,
		ReachableViaCarrierDataNetwork,
		ReachableViaWiFiNetwork
	}

	public static class Reachability
	{
		public static string HostName = Settings.ws_address;

		public static bool IsReachableWithoutRequiringConnection (NetworkReachabilityFlags flags)
		{
			// Is it reachable with the current network configuration?
			bool isReachable = (flags & NetworkReachabilityFlags.Reachable) != 0;

			// Do we need a connection to reach it?
			bool noConnectionRequired = (flags & NetworkReachabilityFlags.ConnectionRequired) == 0
				|| (flags & NetworkReachabilityFlags.IsWWAN) != 0;

			return isReachable && noConnectionRequired;
		}

		// Is the host reachable with the current network configuration
		public static bool IsHostReachable(string host)
		{
			if (string.IsNullOrEmpty(host))
				return false;

			using (var r = new NetworkReachability(host)) {
				NetworkReachabilityFlags flags;

				if (r.TryGetFlags(out flags))
					return IsReachableWithoutRequiringConnection(flags);
			}
			return false;
		}

		//
		// Raised every time there is an interesting reachable event,
		// we do not even pass the info as to what changed, and
		// we lump all three status we probe into one
		//
		public static event EventHandler ReachabilityChanged;

		static void OnChange(NetworkReachabilityFlags flags)
		{
			var h = ReachabilityChanged;
			if (h != null)
				h(null, EventArgs.Empty);
		}

		//
		// Returns true if it is possible to reach the AdHoc WiFi network
		// and optionally provides extra network reachability flags as the
		// out parameter
		//
		static NetworkReachability adHocWiFiNetworkReachability;

		public static bool IsAdHocWiFiNetworkAvailable (out NetworkReachabilityFlags flags)
		{
			if (adHocWiFiNetworkReachability == null) {
				adHocWiFiNetworkReachability = new NetworkReachability(new IPAddress(new byte [] { 169, 254, 0, 0 }));
				adHocWiFiNetworkReachability.SetNotification(OnChange);
				adHocWiFiNetworkReachability.Schedule(CFRunLoop.Current, CFRunLoop.ModeDefault);
			}

			return adHocWiFiNetworkReachability.TryGetFlags(out flags) && IsReachableWithoutRequiringConnection(flags);
		}

		static NetworkReachability defaultRouteReachability;

		static bool IsNetworkAvailable(out NetworkReachabilityFlags flags)
		{
			if (defaultRouteReachability == null) {
				defaultRouteReachability = new NetworkReachability(new IPAddress(0));
				defaultRouteReachability.SetNotification(OnChange);
				defaultRouteReachability.Schedule(CFRunLoop.Current, CFRunLoop.ModeDefault);
			}
			return defaultRouteReachability.TryGetFlags(out flags) && IsReachableWithoutRequiringConnection(flags);
		}

		static NetworkReachability remoteHostReachability;

		public static NetworkStatus RemoteHostStatus()
		{
			NetworkReachabilityFlags flags;
			bool reachable;

			if (remoteHostReachability == null) {
				remoteHostReachability = new NetworkReachability (HostName);

				// Need to probe before we queue, or we wont get any meaningful values
				// this only happens when you create NetworkReachability from a hostname
				reachable = remoteHostReachability.TryGetFlags (out flags);

				remoteHostReachability.SetNotification (OnChange);
				remoteHostReachability.Schedule (CFRunLoop.Current, CFRunLoop.ModeDefault);
			} else {
				reachable = remoteHostReachability.TryGetFlags (out flags);
			}

			if (!reachable)
				return NetworkStatus.NotReachable;

			if (!IsReachableWithoutRequiringConnection(flags))
				return NetworkStatus.NotReachable;

			return (flags & NetworkReachabilityFlags.IsWWAN) != 0 ?
				NetworkStatus.ReachableViaCarrierDataNetwork : NetworkStatus.ReachableViaWiFiNetwork;
		}

		public static NetworkStatus InternetConnectionStatus ()
		{
			NetworkReachabilityFlags flags;
			bool defaultNetworkAvailable = IsNetworkAvailable(out flags);
			if (defaultNetworkAvailable && ((flags & NetworkReachabilityFlags.IsDirect) != 0))
				return NetworkStatus.NotReachable;
			else if ((flags & NetworkReachabilityFlags.IsWWAN) != 0)
				return NetworkStatus.ReachableViaCarrierDataNetwork;
			else if (flags == 0)
				return NetworkStatus.NotReachable;
			return NetworkStatus.ReachableViaWiFiNetwork;
		}

		public static NetworkStatus LocalWifiConnectionStatus()
		{
			NetworkReachabilityFlags flags;
			if (IsAdHocWiFiNetworkAvailable(out flags))
			if ((flags & NetworkReachabilityFlags.IsDirect) != 0)
				return NetworkStatus.ReachableViaWiFiNetwork;

			return NetworkStatus.NotReachable;
		}
	}
}
