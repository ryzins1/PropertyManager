using System;

namespace MvcApplication.Models
{
	public class Complaint : Entity
	{
	    public string Url { get; set; }
	    public string LeaseId { get; set; }
		public DateTime ComplaintDate { get; set; }
		public string ReportedBy { get; set; }
		public string Remarks { get; set; }
	}
}