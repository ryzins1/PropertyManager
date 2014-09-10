using System;

namespace MvcApplication.Models
{
	public class Payment
	{
		public DateTime PaymentDate { get; set; }
		public string CheckNumber { get; set; }
		public decimal Amount { get; set; }
		public bool IsLate { get; set; }
	}
}