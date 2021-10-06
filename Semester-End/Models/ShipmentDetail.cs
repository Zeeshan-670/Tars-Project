using System;
using System.Collections.Generic;

#nullable disable

namespace Semester_End.Models
{
    public partial class ShipmentDetail
    {
        public int Id { get; set; }
        public int? TrackingId { get; set; }
        public int BranchId { get; set; }
        public int? DeliveryAddress { get; set; }
        public int? Charges { get; set; }

        public virtual Branch Branch { get; set; }
        public virtual Shipment ChargesNavigation { get; set; }
        public virtual Shipment DeliveryAddressNavigation { get; set; }
        public virtual Shipment Tracking { get; set; }
    }
}
