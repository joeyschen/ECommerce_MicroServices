using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ecommerce.Api.Orders.Db;

namespace Ecommerce.Api.Orders.Interfaces
{
    public interface IOrdersProvider
    {
        Task<(bool isSuccessful, IEnumerable<Models.Order> orders, string errorMessage)> GetAllOrdersAsync();
        Task<(bool isSuccessful, IEnumerable<Models.Order> order, string errorMessage)> GetOrdersByIdAsync(int customerId);
        //Task<(bool isSuccessful, IEnumerable<Models.OrderItem> orderItems, string errorMessage)> GetAllOrderItemsAsync();
        //Task<(bool isSuccessful, Models.OrderItem Orders, string errorMessage)> GetOrderItemByIdAsync(int id);

    }
}
