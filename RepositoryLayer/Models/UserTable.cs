using System;
using System.Collections.Generic;

namespace RepoLayer.Models
{
    public partial class UserTable
    {
        public UserTable()
        {
            AddressTable = new HashSet<AddressTable>();
            OrderTable = new HashSet<OrderTable>();
        }

        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserEmailId { get; set; }
        public string UserPassword { get; set; }
        public string UserPhoneNumber { get; set; }
        public string UserRole { get; set; }

        public virtual ICollection<AddressTable> AddressTable { get; set; }
        public virtual ICollection<OrderTable> OrderTable { get; set; }
    }
}
