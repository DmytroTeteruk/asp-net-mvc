using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace kurs_5.Models
{
    public partial class Product
    {
        public Product()
        {
            Order = new HashSet<Order>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string Name { get; set; }
        public long? Price { get; set; }
        public string Description { get; set; }
        public long? Amount { get; set; }

        public virtual ICollection<Order> Order { get; set; }
    }
}
