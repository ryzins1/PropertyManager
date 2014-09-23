using System;

namespace MvcApplication.Models
{
	public class Damage : Entity
	{
	    public string Url { get; set; }
	    public string LeaseId { get; set; }
		public DateTime DamageDate { get; set; }
		public string Remarks { get; set; }
		public decimal Amount { get; set; }
	}
}