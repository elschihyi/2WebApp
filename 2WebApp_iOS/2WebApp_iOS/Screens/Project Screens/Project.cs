using System;

namespace WebApp_iOS
{
	public class Project
	{
		public int Id { get; set;}
		public int OrganizationId { get; set; }
		public string Title { get; set; } 
		public string Company { get; set; } 
		public string Type { get; set; }
		public string StaffContact { get; set; } 
		public string Status {get; set;} 
		public string SupportType { get; set; } 
		public string SupportHours { get; set; } 
		public string SupportLatestBackUp { get; set; }
		public string SupportLastRestored { get; set; } 
		public string SupportStatus {get; set;} 
		public string PrimaryContact { get; set; } 
		public DateTime LastPost { get; set; } 
		public int NumberOfUpdates { get; set; } 
	}
}

