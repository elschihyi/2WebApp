using System;

namespace CoreDataService
{
	public static class LoginService
	{

		#region External Interfaces

		// UserAuth Function
		// Authenticate a user
		// Parameters:
		//		user		user name
		//		password	the user's password
		//		IsSaved		indicated if the credential should be saved or not
		//		errmsg		error message
		// Return:
		//		true		the user is valid
		//		false		return with error

		public static Boolean UserAuth (string username, string password, Boolean IsSaved, out string errmsg)
		{
			errmsg = "";

			if (username == "" && password == "" && !IsSaved) {
				errmsg = "Username and Password are reqired";
				return false;
			}

			// check if the credential is saved
			if (username == "" && password == "" && IsSaved) {
				userinfo info = GetLocalUserInfo (out errmsg);
				if (info == null) {
					errmsg = "Missing usernmae and password";
					return false;
				}
				username = info.user;
				password = info.cred;
			} else {
				username = username.GetHashCode ().ToString ();
				password = password.GetHashCode ().ToString ();
			}

			// remote authentication
			if (!RemoteHashAuth ( username, password, out errmsg))
				return false;

			// Save it only if the authenticationi is succeed
			if (!SaveLocalUserInfo (username, password, out errmsg))
				return false;		

			Settings.local_isdatasynced = true;

			return true;
		}



		// IsLogined Function
		// return current login status
		// Parameters:
		// Return:
		//		true		the user is valid
		//		false		not login yet

		public static Boolean IsLogined ()
		{
			return Settings.local_isdatasynced;
		}



		#endregion



		#region Internal Interfaces

		private static userinfo GetLocalUserInfo (out string errmsg)
		{

			// obtain user record
			object userobj;
			string sql = "select * from userinfo";
			DBRequest req = new DBRequest ();
			req.sql = sql;
			req.tablename = "userinfo";
			if (!LocalDB.ReadData (req, out userobj, out errmsg)) {
				return null;
			}

			return (userinfo)userobj;
		}

		private static Boolean SaveLocalUserInfo (string username, string password, out string errmsg)
		{
			// insert encrypted user record
			userinfo data = new userinfo ();
			data.user = username;
			data.cred = password.GetHashCode ().ToString ();
			DBRequest req = new DBRequest ();
			req.dataset = data;
			if (!LocalDB.SaveData (req, out errmsg)) {
				return false;
			}

			return true;
		}

		private static Boolean RemoteHashAuth (string username, string password, out string errmsg)
		{

			errmsg = "";

			return true;
		}

		#endregion
	}
}

