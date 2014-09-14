namespace MvcApplication.Models
{
	public class Tenant : Entity
	{
	    public string Url { get; set; }
	    public string CompanyId { get; set; }
	    public string BuildingId { get; set; }
	    public string UnitId { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Phone { get; set; }
	}
}