using System;
using System.Collections.Generic;

namespace coreBookStore.Models
{
    public partial class Payment
    {
        public int PaymentId { get; set; }
        public string StripePaymentId { get; set; }
        public float PaymentAmount { get; set; }
        public DateTime DateOfPayment { get; set; }
        public string PaymentDescription { get; set; }
        public long CardLastDigit { get; set; }
        public int OrderId { get; set; }
        public int? CustomerId { get; set; }

        public Customer Customer { get; set; }
        public Order Order { get; set; }
    }
}
