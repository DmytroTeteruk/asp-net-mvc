using System;
using System.Collections.Generic;

namespace kurs_5.Models
{
    public partial class Status
    {
        public Status()
        {
            Order = new HashSet<Order>();
        }

        public long Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Order> Order { get; set; }
    }
}
