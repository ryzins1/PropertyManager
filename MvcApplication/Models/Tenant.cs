using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MvcApplication.Models
{
	public class Tenant : Entity
	{
	    public Tenant()
	    {
	        LeaseIds = new Collection<string>();
	    }
	    public string Url { get; set; }
	    public string CompanyId { get; set; }
	    public string BuildingId { get; set; }
	    public string UnitId { get; set; }
        public ICollection<string> LeaseIds { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string HomePhone { get; set; }
		public string WorkPhone { get; set; }
		public string CellPhone { get; set; }
		public string Email { get; set; }
	}
}