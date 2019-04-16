using System;
using System.Collections.Generic;

namespace OnlineBookStoreUser.Models
{
    public partial class Review
    {
        public int ReviewId { get; set; }
        public string ReviewSubject { get; set; }
        public string ReviewMessage { get; set; }
        public int CustomerId { get; set; }
        public int BookId { get; set; }

        public Books Book { get; set; }
        public Customers Customer { get; set; }
    }
}
