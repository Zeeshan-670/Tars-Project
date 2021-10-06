using System;
using System.Collections.Generic;

#nullable disable

namespace Semester_End.Models
{
    public partial class Branch
    {
        public Branch()
        {
            ShipmentDetails = new HashSet<ShipmentDetail>();
            Shipments = new HashSet<Shipment>();
        }

        public int Id { get; set; }
        public string Branch1 { get; set; }
        public int City { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }

        public virtual City CityNavigation { get; set; }
        public virtual ICollection<ShipmentDetail> ShipmentDetails { get; set; }
        public virtual ICollection<Shipment> Shipments { get; set; }
    }
}
