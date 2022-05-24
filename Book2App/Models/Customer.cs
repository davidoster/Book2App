using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Book2App.Models
{
    public class Customer
    {
        public string CustomerID { get; set; }
        public string FullName { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
