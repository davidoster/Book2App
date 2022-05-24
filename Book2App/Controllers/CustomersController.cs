using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Book2App.Data;
using Book2App.Models;
using Book2App.Repositories;

namespace Book2App.Controllers
{
    public class CustomersController : Controller
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomersController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        // GET: Customers
        public async Task<IActionResult> Index()
        {
            var customers = _customerRepository.AllCustomers;

            return View(await Task.Run(() => customers));
        }

        // GET: Customers/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await Task.Run(() =>
                _customerRepository.GetCustomerById(id));
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }


        // GET: Customers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CustomerID,FullName")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                await Task.Run(() => _customerRepository.Create(customer));
               
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        // GET: Customers/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await Task.Run(() => _customerRepository.GetCustomerById(id)); 
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("CustomerID,FullName")] Customer customer)
        {
            if (id != customer.CustomerID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _customerRepository.Update(customer);
                }
                catch (DbUpdateConcurrencyException) 
                { 
                 var _customer = await Task.Run(() => _customerRepository.GetCustomerById(customer.CustomerID));
                if (_customer==null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        //throw; // admin app
                        RedirectToAction(nameof(Index)); // client app
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await Task.Run(() => _customerRepository.GetCustomerById(id));
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var customer = await Task.Run(() => _customerRepository.Delete(id));
            return RedirectToAction(nameof(Index));
        }

        //private bool CustomerExists(int id)
        //{
        //    return _context.Customers.Any(e => e.CustomerID == id);
        //}
    }
}
