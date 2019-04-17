using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineBookStoreUser.Models
{
    public partial class Customers
    {
        public Customers()
        {
            Orders = new HashSet<Orders>();
            Reviews = new HashSet<Review>();
        }

        public int CustomerId { get; set; }

        [Required(ErrorMessage = "First Name is Required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name is Required")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "User Name is Required")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Email is Required")]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$",
        ErrorMessage = "Please enter valid email id.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is Required")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "Confirm Password is Required")]
        [Display(Name = "Confirm Password")]
        [Compare("OldPassword")]
        public string NewPassword { get; set; }
        [Required(ErrorMessage = "Address is Required")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Zip Code is Required")]
        public long ZipCode { get; set; }
        [Required(ErrorMessage = "Contact is Required")]
        public long Contact { get; set; }
        public bool BillingAddress { get; set; }
        public string ShippingAddress { get; set; }

        public ICollection<Orders> Orders { get; set; }
        public ICollection<Review> Reviews { get; set; }
    }
}
