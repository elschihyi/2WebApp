using System;
using System.IO;
using RestSharp;
using System.Collections.Generic;
using System.Text;
using System.Net;
using Newtonsoft.Json;


namespace CoreDataService
{
	public class Connection
	{

		// check if connection is available and accessible
		protected Boolean CheckConnection(out string errmsg) {
			
			errmsg = "";
			string magicToken = Path.GetRandomFileName();
			
			Dictionary<string, string> request = new Dictionary<string, string> ();
			request.Add ("ADDRESS", Settings.ws_address);
			request.Add ("PATH", Settings.ws_basepath + "Ping");
			request.Add ("FORMAT", "json");
			request.Add ("BODY",magicToken);
			
			string data;
			if ( !MakeRequest( request, out data) || data == null || data != magicToken ) {
				
				errmsg = ErrorMessage.Connection;
				return false;
			}
			
			return true;
		}



		// build and return request block
		// Parameters
		//		info		body message's object
		// Return
		//		non-null	sucessful
		//		null		returned with error in errmsg
		protected Dictionary<string, string> BuildRequest ( object info, ActionType action )
		{
			
			Dictionary<string, string> request = new Dictionary<string, string> ();
			request.Add ("ADDRESS", Settings.ws_address);
			request.Add ("FORMAT", "json");

			// add action type into the request json
			string json = JsonConvert.SerializeObject (info);
			json = json.Replace ("}", ",\"option\":\""+action+"\"}");
			request.Add ("BODY", json);

			switch (action) {
			case ActionType.LOGIN:
			case ActionType.SYNC:
				request.Add ("PATH", Settings.ws_basepath + Settings.ws_svcname);
				break;

			case ActionType.CREATEACCOUNT:
			case ActionType.UPDATEACCOUNT:
			case ActionType.UPDATESETTINGS:
				request.Add ("PATH", Settings.ws_basepath + Settings.ws_reqname);
				break;
			}
			return request;
		}


		
		// MakeRequest Function
		// Obtain data from remote web service
		// Parameters
		//		request:
		//				address		host address
		//				path		svc path on the host
		//				format		json
		//				body		request body in json format
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
					if ( Settings.ws_reqtype == RequestType.GET )
						req = new RestRequest (request["PATH"], Method.GET);
					else if (Settings.ws_reqtype == RequestType.POST ) {
						req = new RestRequest (request["PATH"], Method.POST);
						req.AddParameter("application/json; charset=utf-8", request["BODY"], ParameterType.RequestBody);
					}
					else {

						if ( Settings.runmode == RunMode.Normal ) {

							data = ErrorMessage.Connection;

						} else if ( Settings.runmode == RunMode.Debug ) {

							data = ErrorMessage.DataAccess_RequestType;
						}

						return false;
					}

					req.AddHeader("Accept-Encoding","gzip,deflate");
					req.AddHeader("Accept", "application/json");
					req.Timeout = Settings.ws_timeout;
				}
				else {
					// handle other types of the formats
				}

				// send the request and receive its result by a synchronous call
				var response = client.Execute(req);
				if (response.ErrorException != null)
				{
					throw new Exception(response.ErrorMessage);
				}

				data = response.Content;

			}
			catch (Exception e)
			{
				if ( Settings.runmode == RunMode.Normal ) {
					
					data = ErrorMessage.Connection;

				} else if ( Settings.runmode == RunMode.Debug ) {
					
					data = e.Source + " -> " + e.Message;
				}

				return false;
			}

			return true;
		}

	}

}
