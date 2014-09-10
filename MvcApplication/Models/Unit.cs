using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MvcApplication.Models
{
	public class Unit
	{
		public Unit()
		{
			Leases = new Collection<Lease>();
		}
		public Guid Id { get; set; }
		public string Number { get; set; }
		public ICollection<Lease> Leases { get; set; }
	}
}