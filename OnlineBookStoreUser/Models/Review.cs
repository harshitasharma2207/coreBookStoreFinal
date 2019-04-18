using System;
using System.Collections.Generic;

namespace OnlineBookStoreUser.Models
{
    public partial class Review
    {
        public Review()
        {
            Customers = new HashSet<Customers>();
        }

        public int ReviewId { get; set; }
        public string ReviewSubject { get; set; }
        public string ReviewMessage { get; set; }
        public int CustomerId { get; set; }
        public int BookId { get; set; }

        public Books Book { get; set; }
        public ICollection<Customers> Customers { get; set; }
    }
}
