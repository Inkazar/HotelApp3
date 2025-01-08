using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelApp3.Controllers
{
    public class CustomerController
    {
        private readonly CustomerFunctions _functions;

        public CustomerController(CustomerFunctions functions)
        {
            _functions = functions;
        }

        public void AddCustomer() => _functions.AddCustomer();

        public void ViewAllCustomers() => _functions.ViewAllCustomers();

        public void DeleteCustomer() => _functions.DeleteCustomer();
    }
}
