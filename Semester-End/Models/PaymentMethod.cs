using System;
using System.Collections.Generic;

#nullable disable

namespace Semester_End.Models
{
    public partial class PaymentMethod
    {
        public PaymentMethod()
        {
            Shipments = new HashSet<Shipment>();
        }

        public int Id { get; set; }
        public string PaymentMethod1 { get; set; }
        public decimal? CashOnDelivery { get; set; }

        public virtual ICollection<Shipment> Shipments { get; set; }
    }
}
