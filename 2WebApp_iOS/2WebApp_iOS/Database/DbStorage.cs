using System;
using SQLite;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp_iOS
{

	public class DbStorage
	{
		static SQLiteConnection db;
		string dbName = "WebDesign.db";
		private static DbStorage dbManager;
		string dbPath;

		public DbStorage ()
		{
			var doc = Environment.GetFolderPath (Environment.SpecialFolder.Personal);
			dbPath = Path.Combine (doc, dbName);
			db = new SQLiteConnection (dbPath);
		}

		public static DbStorage Manager ()
		{
			if (dbManager == null) {

				dbManager = new DbStorage ();
			}

			return dbManager;
		}


		//Create all the tables needed.
		public void createTables ()
		{
			db.CreateTable<DbProject> ();
			db.CreateTable<DbOrganization> (); 
			db.CreateTable<DbAuthenticated> ();  
		}

		public List<DbOrganization> getOrganizations(){
			var list = new List<DbOrganization> (); 
			var a = from x in db.Table<DbOrganization> ()
				select x; 
			foreach (var item in a) {
				list.Add(item);
			}
			return list; 
		}

		public void addOrganization(int orgId, String orgName, String orgStatus, String rowAddDate, int rowAddedBy, String rowUpdateDate, int rowUpdatedBy){
			db.Insert (new DbOrganization () {
				organizationId = orgId,
				organizationName = orgName,
				organizationStatus = orgStatus,
				rowAddDate = rowAddDate,
				rowAddedBy = rowAddedBy,
				rowUpdateDate = rowUpdateDate,
				rowUpdatedBy = rowUpdatedBy
			}); 
		}

		public void deleteOrganizations(){
			var a = from x in db.Table<DbOrganization> ()
				select x; 
			foreach (var item in a) {
				db.Delete<DbOrganization> (item.id);
			}
		}

		public List<DbProject> getProjects(){
			var list = new List<DbProject> (); 
			var a = from x in db.Table<DbProject> ()
				select x; 
			foreach (var item in a) {
				list.Add(item);
			}
			return list;
		}

		public void addProject(int prjId, String prjName, int orgId, int clientAcnt, 	int prjType, int prjStatus, int prjPhase, int staffId, String rowAddDate, String rowAddedBy, String rowUpdateDate, String rowUpdatedBy, String prjTypeName, String prjStatusName, String prjPhaseName, String stFirstName, String stLastName)
		{
			db.Insert(new DbProject()
				{
					projectId = prjId,
					projectName = prjName,
					organizationId = orgId,
					clientAccountId = clientAcnt,
					projectTypeId = prjType,
					projectStatusId = prjStatus,
					projectPhaseId = prjPhase,
					staffId = staffId,
					rowAddDate = rowAddDate,
					rowAddedBy = rowAddedBy,
					rowUpdateDate = rowUpdateDate,
					rowUpdatedBy = rowUpdatedBy,
					projectTypeName = prjTypeName,
					projectStatusName = prjStatusName,
					projectPhaseName = prjPhaseName,
					staffFirstName = stFirstName,
					staffLastName = stLastName
				});
		}

		public void deleteProjects(){
			var a = from x in db.Table<DbProject> ()
				select x; 
			foreach (var item in a) {
				db.Delete<DbProject> (item.id);
			}
		}

		public void placeAuthenticationToken (string usrName)
		{
			//check if user has authenticated before
			var search = from x in db.Table<DbAuthenticated> ()
			             select x;

			if (search.Count () == 0) {
				//User has not authenticated before
				db.Insert (new DbAuthenticated (){ userName = usrName }); 
			} else {
				//Another User is already authenticated, insert new token
				var a = from x in db.Table<DbAuthenticated> ()
				        select x; 
				foreach (var item in a) {
					db.Delete<DbAuthenticated> (item.id);
				}
				db.Insert (new DbAuthenticated (){ userName = usrName });
			}
		}

		public Boolean checkAuthenticationToken (){
			//check if user has authenticated before
			var search = from x in db.Table<DbAuthenticated> ()
				select x;

			if (search.Count () == 0) {
				//user has not authenticated before
				return false;
			} else {
				return true; 
			}
		}

		public void AuthenticationLogout(){
			//Logout current user
			var a = from x in db.Table<DbAuthenticated> ()
				select x; 
			foreach (var item in a) {
				db.Delete<DbAuthenticated> (item.id);
			}

		}

		//for rest check out f@h db code



	}
}
