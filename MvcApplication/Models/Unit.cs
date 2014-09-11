using System.Collections.Generic;

namespace MvcApplication.Models
{
	public class Unit : Entity
	{
		public string Url { get; set; }
		public string BuildingId { get; set; }
		public string Number { get; set; }
		public string Description { get; set; }
		public ICollection<Lease> Leases { get; set; }
	}
}