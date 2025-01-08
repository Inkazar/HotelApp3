using System;
using HotelApp3.Utilities.Functions;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelApp3.Controllers
{
    public class CustomerController
    {
        private readonly CustomerFunction _functions;

        public CustomerController(CustomerFunction functions)
        {
            _functions = functions;
        }

        public void AddCustomer() => _functions.AddCustomer();

        public void ViewAllCustomers() => _functions.ViewAllCustomers();

        public void UpdateCustomer() => _functions.UpdateCustomer();

        public void DeleteCustomer() => _functions.DeleteCustomer();

    }
}
