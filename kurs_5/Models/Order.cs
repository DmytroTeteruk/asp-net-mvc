using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace kurs_5.Models
{
    public partial class Order
    {
        public long UserId { get; set; }
        public long ProductId { get; set; }
        public long? StatusId { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public virtual Product Product { get; set; }
        public virtual Status Status { get; set; }
        public virtual Users User { get; set; }
    }
}
