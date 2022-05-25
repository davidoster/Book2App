using Book2App.Data;
using Book2App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Book2App.Repositories
{
    public class CustomerRepository : ICustomerRepository

    {
        private readonly ApplicationDbContext _context;
        public CustomerRepository(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
        }
        public IEnumerable<Customer> AllCustomers => _context.Customers;

        public IEnumerable<Customer> MostFrequentCustomers => throw new NotImplementedException();

        public Customer Create(Customer customer)
        {
            Customer _customer = _context.Add(customer).Entity;
            _context.SaveChanges();
            return _customer;
        }

        public Customer Delete(string ID)
        {
            var customer = _context.Customers.Find(ID);
            if (customer != null)
            {
                var deletedCustomer = _context.Customers.Remove(customer);
                if (deletedCustomer.State == Microsoft.EntityFrameworkCore.EntityState.Deleted)
                {
                    _context.SaveChanges();
                    return deletedCustomer.Entity;
                }
            }
            return null;
        }

        public Customer GetCustomerById(string customerId)
        {
            return _context.Customers.Find(customerId);
        }

        public Customer Update(Customer customer)
        {

            var _customer = _context.Update(customer).Entity;
            // backup _book to the backupDB ELSEWHERE

            //await Task.Run(() => _context.SaveChangesAsync()); // admin app
            _context.SaveChanges(); // client app
            return _customer;
        }
    }
}
