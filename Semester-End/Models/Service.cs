using System;
using System.Collections.Generic;

#nullable disable

namespace Semester_End.Models
{
    public partial class Service
    {
        public Service()
        {
            Shipments = new HashSet<Shipment>();
        }

        public int Id { get; set; }
        public string ServiceName { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Shipment> Shipments { get; set; }
    }
}
