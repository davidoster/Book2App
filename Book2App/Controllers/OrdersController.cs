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
    public class OrdersController : Controller
    {
        private readonly IOrderRepository _orderRepository;

        public OrdersController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            var orders = _orderRepository.AllOrders;

            return View(await Task.Run(() => orders));
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await Task.Run(() =>
                _orderRepository.GetOrderById(id));
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }


        // GET: Orders/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderID,BookID,CustomerID")] Order order)
        {
            if (ModelState.IsValid)
            {
                await Task.Run(() => _orderRepository.Create(order));

                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await Task.Run(() => _orderRepository.GetOrderById(id));
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("OrderID,BookID,CustomerID")] Order order)
        {
            if (id != order.OrderID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _orderRepository.Update(order);
                }
                catch (DbUpdateConcurrencyException)
                {
                    var _order = await Task.Run(() => _orderRepository.GetOrderById(order.OrderID));
                    if (_order == null)
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
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

                var order = await Task.Run(() => _orderRepository.GetOrderById(id));
                if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var order = await Task.Run(() => _orderRepository.Delete(id));
            return RedirectToAction(nameof(Index));
        }

        //private bool OrderExists(string id)
        //{
        //    return _context.Orders.Any(e => e.OrderID == id);
        //}
    }
}
