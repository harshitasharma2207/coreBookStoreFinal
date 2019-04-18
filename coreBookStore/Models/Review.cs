using System;
using System.Collections.Generic;

namespace coreBookStore.Models
{
    public partial class Review
    {
        public int ReviewId { get; set; }
        public string ReviewSubject { get; set; }
        public string ReviewMessage { get; set; }
        public int CustomerId { get; set; }
        public int BookId { get; set; }

        public Book Book { get; set; }
        public List<Customer> Customer { get; set; }
    }
}
