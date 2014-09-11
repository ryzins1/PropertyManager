using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MvcApplication.Models
{
	public class Unit : Entity
	{
		public Unit()
		{
			Leases = new Collection<Lease>();
		}

		public string BuildingId { get; set; }
		public string Number { get; set; }
		public string Description { get; set; }
		public ICollection<Lease> Leases { get; set; }
	}
}