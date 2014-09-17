using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApplication.Models
{
	public class MaintenanceRequest : Entity
	{
		public string Url { get; set; }
		public string CompanyId { get; set; }
		public string BuildingId { get; set; }
		public string UnitId { get; set; }
		public string TenantId { get; set; }
		public string BuildingName { get; set; }
		public string UnitNumber { get; set; }
		public String Name { get; set; }
		public DateTime MaintenanceDate { get; set; }
		public string Remarks { get; set; }
		public decimal Amount { get; set; }
	}
}