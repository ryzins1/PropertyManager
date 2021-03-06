﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MvcApplication.Models
{
	public class Lease : Entity
	{
		public Lease()
		{
            TenantIds = new Collection<string>();
			PaymentDueDay = 1;
			Payments = new Collection<Payment>();
			Complaints = new Collection<Complaint>();
			Damages = new Collection<Damage>();
		}
	    public string Url { get; set; }
	    public string CompanyId { get; set; }
	    public string BuildingId { get; set; }
	    public string UnitId { get; set; }
	    public ICollection<string> TenantIds { get; set; }
	    public string BuildingName { get; set; }
	    public string UnitNumber { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public bool IsMonthToMonth { get; set; }
		public int PaymentDueDay { get; set; }
		public Decimal MonthlyPayment { get; set; }
		public Decimal SecurityDeposit { get; set; }
		public Decimal PetDeposit { get; set; }
	    public decimal TotalDamages { get; set; }
        public Decimal DepositLessDamages { get; set; }
		public ICollection<Payment> Payments { get; set; }
		public ICollection<Complaint> Complaints { get; set; }
		public ICollection<Damage> Damages { get; set; }
	}
}