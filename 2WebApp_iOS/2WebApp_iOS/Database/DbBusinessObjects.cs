using System;
using SQLite;
using System.Collections.Generic;

namespace WebApp_iOS
{
	

	public class DbAuthenticated {
		[PrimaryKey, AutoIncrement]
		public int id { get; set; }

		public string userName { get; set; }
	}

	public class DbProject{
		[PrimaryKey, AutoIncrement]
		public int id { get; set; }

		public int projectId{get; set;}
		public String projectName{get; set;}
		public int organizationId{get; set;}
		public int clientAccountId{get; set;}
		public int projectTypeId{get; set;}
		public int projectStatusId{get; set;}
		public int projectPhaseId{get; set;}
		public int staffId{get; set;}
		public String rowAddDate{get; set;}
		public String rowAddedBy{get; set;}
		public String rowUpdateDate{get; set;}
		public String rowUpdatedBy{get; set;}
		public String projectStatusName{get; set;}
		public String projectTypeName { get; set; }
		public String projectPhaseName { get; set; }
		public String staffFirstName { get; set; }
		public String staffLastName { get; set; }
	}

	public class DbProjectDetail{
		[PrimaryKey, AutoIncrement]
		public int id { get; set; }

//		Progress = 0.5f,
//		TeamLead = "Alicia",
//		PrimaryContact = "Khaled Haggag",
//		Stage = "Prelaunch",
//		LastPost = new DateTime (2015, 04, 27),
//		NumberOfUpdates = 1,
//		Company = "Sarcan",
//		Type = "Redesign",
//		TwoWebContact = "Jillian Hare",
//		SupportType = "360",
//		SupportHours = "4/12",
//		SupportLatestBackUp = "June 1, 2015",
//		SupportLastRestored = "February 3, 2013",
//		SupportStatus = "Up-to-date",
	}

	public class DbOrganization{
		[PrimaryKey, AutoIncrement]
		public int id {get; set;}

		public int organizationId{get; set;}
		public String organizationName{get; set;}
		public String organizationStatus{get; set;}
		public String rowAddDate{get; set;}
		public int rowAddedBy{get; set;}
		public String rowUpdateDate{get; set;}
		public int rowUpdatedBy{get; set;}
	}
}

