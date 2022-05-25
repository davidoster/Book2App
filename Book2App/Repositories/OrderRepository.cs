using Book2App.Data;
using Book2App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Book2App.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;
        public OrderRepository(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
        }
        public IEnumerable<Order> AllOrders => _context.Orders;

        public IEnumerable<Order> MostFrequentOrders => throw new NotImplementedException();

        public Order Create(Order order)
        {
            Order _order = _context.Add(order).Entity;
            _context.SaveChanges();
            return _order;
        }

        public Order Delete(string ID)
        {
            var order = _context.Orders.Find(ID);
            if (order != null)
            {
                var deletedOrder = _context.Orders.Remove(order);
                if (deletedOrder.State == Microsoft.EntityFrameworkCore.EntityState.Deleted)
                {
                    _context.SaveChanges();
                    return deletedOrder.Entity;
                }
            }
            return null;
        }

        public Order GetOrderById(string orderId)
        {
            return _context.Orders.Find(orderId);
        }

        public Order Update(Order order)
        {

            var _order = _context.Update(order).Entity;
            // backup _book to the backupDB ELSEWHERE

            //await Task.Run(() => _context.SaveChangesAsync()); // admin app
            _context.SaveChanges(); // client app
            return _order;
        }
    }
}
