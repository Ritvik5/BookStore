using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Model
{
    public class BookModel
    {
        public string BookName { get; set; }
        public string AuthorName { get; set; }
        public int DiscountPrice { get; set; }
        public int OriginalPrice { get; set; }
        public string BookDescription { get; set; }
        public string BookImage { get; set; }
        public int BookQuantity { get; set; }
    }
}
