using System;
using System.Collections.Generic;

namespace RepoLayer.Models
{
    public partial class AddressTable
    {
        public AddressTable()
        {
            OrderTable = new HashSet<OrderTable>();
        }

        public int AddressId { get; set; }
        public string UserAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int Pincode { get; set; }
        public int? UserId { get; set; }

        public virtual UserTable User { get; set; }
        public virtual ICollection<OrderTable> OrderTable { get; set; }
    }
}
