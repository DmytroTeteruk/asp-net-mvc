using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace kurs_5.Models
{
    public partial class Users
    {
        public Users()
        {
            Order = new HashSet<Order>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public long? RoleId { get; set; }

        public virtual Role Role { get; set; }
        public virtual ICollection<Order> Order { get; set; }
    }
}
