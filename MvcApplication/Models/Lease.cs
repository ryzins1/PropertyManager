using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MvcApplication.Models
{
	public class Lease : Entity
	{
		public Lease()
		{
			PaymentDueDay = 1;
			Tenants = new Collection<Tenant>();
			Payments = new Collection<Payment>();
			Complaints = new Collection<Complaint>();
			Damages = new Collection<Damage>();
		}
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public bool IsMonthToMonth { get; set; }
		public int PaymentDueDay { get; set; }
		public Decimal MonthlyPayment { get; set; }
		public Decimal SecurityDeposit { get; set; }
		public Decimal PetDeposit { get; set; }
		public ICollection<Tenant> Tenants { get; set; }
		public ICollection<Payment> Payments { get; set; }
		public ICollection<Complaint> Complaints { get; set; }
		public ICollection<Damage> Damages { get; set; }
	}
}