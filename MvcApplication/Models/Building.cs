using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MvcApplication.Models
{
	public class Building : Entity
	{
		public Building()
		{
			Units = new Collection<Unit>();
		}
		public string Name { get; set; }
		public string Address { get; set; }
		public string City { get; set; }
		public string State { get; set; }
		public string Zip { get; set; }
		public ICollection<Unit> Units { get; set; }
	}
}