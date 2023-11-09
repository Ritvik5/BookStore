using System;
using System.Collections.Generic;

namespace RepoLayer.Models
{
    public partial class OrderTable
    {
        public int OrderId { get; set; }
        public int OrderPrice { get; set; }
        public int OrderQuantity { get; set; }
        public int? UserId { get; set; }
        public int? AddressId { get; set; }

        public virtual AddressTable Address { get; set; }
        public virtual UserTable User { get; set; }
    }
}
