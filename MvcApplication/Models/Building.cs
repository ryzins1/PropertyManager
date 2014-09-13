namespace MvcApplication.Models
{
	public class Building : Entity
	{
		public string Url { get; set; }
	    public string CompanyId { get; set; }
		public string Name { get; set; }
		public string Address { get; set; }
		public string City { get; set; }
		public string State { get; set; }
		public string Zip { get; set; }
	}
}