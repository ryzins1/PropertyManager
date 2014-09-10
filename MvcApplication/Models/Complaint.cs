using System;

namespace MvcApplication.Models
{
	public class Complaint
	{
		public Guid Id { get; set; }
		public DateTime ComplaintDate { get; set; }
		public string ReportedBy { get; set; }
		public string ComplaintRemarks { get; set; }
	}
}