using System;

namespace MvcApplication.Models
{
	public class Damage
	{
		public Guid Id { get; set; }
		public DateTime DamageDate { get; set; }
		public string DamageRemarks { get; set; }
		public decimal Amount { get; set; }
	}
}