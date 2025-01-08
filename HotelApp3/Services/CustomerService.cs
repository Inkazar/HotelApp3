using HotelApp3.Models;
using HotelApp3.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelApp3.Services
{
    public class CustomerService
    {
        private readonly CustomerRepository _repository;

        public CustomerService(CustomerRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            return _repository.GetAllCustomers().ToList();
        }

        public Customer GetCustomerById(int id)
        {
            return _repository.GetCustomerById(id);
        }

        public void AddCustomer(Customer customer)
        {
            _repository.AddCustomer(customer);
        }

        public void UpdateCustomer(Customer customer)
        {
            _repository.UpdateCustomer(customer);
        }

        public void DeleteCustomer(int id)
        {
            _repository.DeleteCustomer(id);
        }
    }
}
