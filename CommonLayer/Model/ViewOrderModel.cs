using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Model
{
    public class ViewOrderModel
    {
        public int OrderId { get; set; }
        public int OrderPrice { get; set; }
        public int OrderQuantity { get; set; }
        public int UserId { get; set; }
        public int AddressId { get; set; }
    }
}
