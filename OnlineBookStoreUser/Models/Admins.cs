using System;
using System.Collections.Generic;

namespace OnlineBookStoreUser.Models
{
    public partial class Admins
    {
        public Admins()
        {
            Publications = new HashSet<Publications>();
        }

        public int AdminId { get; set; }
        public string AdminUserName { get; set; }
        public string AdminPassword { get; set; }

        public ICollection<Publications> Publications { get; set; }
    }
}
