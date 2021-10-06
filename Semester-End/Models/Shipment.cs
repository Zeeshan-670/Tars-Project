using System;
using System.Collections.Generic;

#nullable disable

namespace Semester_End.Models
{
    public partial class Shipment
    {
        public Shipment()
        {
            ShipmentDetailChargesNavigations = new HashSet<ShipmentDetail>();
            ShipmentDetailDeliveryAddressNavigations = new HashSet<ShipmentDetail>();
            ShipmentDetailTrackings = new HashSet<ShipmentDetail>();
        }

        public int Id { get; set; }
        public string TrackingId { get; set; }
        public string User { get; set; }
        public DateTime TimeShipped { get; set; }
        public DateTime DateShipped { get; set; }
        public string SenderAddress { get; set; }
        public string DeliveryAddress { get; set; }
        public int BranchId { get; set; }
        public string OrderStatus { get; set; }
        public string DeliveryStatus { get; set; }
        public int ServicesType { get; set; }
        public string Weight { get; set; }
        public decimal Charges { get; set; }
        public int Payment { get; set; }
        public bool? SignatureRequired { get; set; }

        public virtual Branch Branch { get; set; }
        public virtual PaymentMethod PaymentNavigation { get; set; }
        public virtual Service ServicesTypeNavigation { get; set; }
        public virtual ICollection<ShipmentDetail> ShipmentDetailChargesNavigations { get; set; }
        public virtual ICollection<ShipmentDetail> ShipmentDetailDeliveryAddressNavigations { get; set; }
        public virtual ICollection<ShipmentDetail> ShipmentDetailTrackings { get; set; }
    }
}
