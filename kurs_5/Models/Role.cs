﻿using System;
using System.Collections.Generic;

namespace kurs_5.Models
{
    public partial class Role
    {
        public Role()
        {
            Users = new HashSet<Users>();
        }

        public long Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Users> Users { get; set; }
    }
}
