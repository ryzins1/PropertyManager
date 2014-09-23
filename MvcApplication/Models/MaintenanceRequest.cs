using System;

namespace MvcApplication.Models
{
	public class MaintenanceRequest : Entity
	{
		public string Url { get; set; }
		public String LeaseId { get; set; }
		public DateTime MaintenanceDate { get; set; }
		public string Remarks { get; set; }
		public decimal Amount { get; set; }
	}
}