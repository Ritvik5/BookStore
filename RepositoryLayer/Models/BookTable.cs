using System;
using System.Collections.Generic;

namespace RepoLayer.Models
{
    public partial class BookTable
    {
        public int BookId { get; set; }
        public string BookName { get; set; }
        public string AuthorName { get; set; }
        public int? DiscountPrice { get; set; }
        public int OriginalPrice { get; set; }
        public string BookDescription { get; set; }
        public string BookImage { get; set; }
        public int? BookQuantity { get; set; }
    }
}
