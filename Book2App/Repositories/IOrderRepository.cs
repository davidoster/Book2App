using Book2App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Book2App.Repositories
{
    public interface IOrderRepository
    {
        Order GetOrderById(string orderId);
        Order Create(Order order);
        Order Update(Order order);
        Order Delete(string ID);
        IEnumerable<Order> AllOrders { get; }
        IEnumerable<Order> MostFrequentOrders { get; }
    }
}
