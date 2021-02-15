using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using AutoMapper;
using Ecommerce.Api.Orders.Db;
using Ecommerce.Api.Orders.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Ecommerce.Api.Orders.Providers
{
    public class OrdersProvider : IOrdersProvider
    {
        private readonly OrderDbContext orderDbContext;
        private readonly ILogger<OrdersProvider> logger;
        private readonly IMapper mapper;

        public OrdersProvider(OrderDbContext orderDbContext, ILogger<OrdersProvider> logger, IMapper mapper)
        {
            this.orderDbContext = orderDbContext;
            this.logger = logger;
            this.mapper = mapper;

            SeedData();
        }

        private void SeedData()
        {
            

            if (!orderDbContext.orders.Any())
            {
                orderDbContext.orders.Add(new Order()
                {
                    Id = 1,
                    CustomerId = 1,
                    OrderDate = DateTime.Now,
                    Items = new List<OrderItem>()
                    {
                        new OrderItem() { OrderId = 1, ProductId = 1, Quantity = 10, UnitPrice = 10 },
                        new OrderItem() { OrderId = 1, ProductId = 2, Quantity = 10, UnitPrice = 10 },
                        new OrderItem() { OrderId = 1, ProductId = 3, Quantity = 10, UnitPrice = 10 },
                        new OrderItem() { OrderId = 2, ProductId = 2, Quantity = 10, UnitPrice = 10 },
                        new OrderItem() { OrderId = 3, ProductId = 3, Quantity = 1, UnitPrice = 100 }
                    },
                    Total = 100
                });
                orderDbContext.orders.Add(new Order()
                {
                    Id = 2,
                    CustomerId = 1,
                    OrderDate = DateTime.Now.AddDays(-1),
                    Items = new List<OrderItem>()
                    {
                        new OrderItem() { OrderId = 1, ProductId = 1, Quantity = 10, UnitPrice = 10 },
                        new OrderItem() { OrderId = 1, ProductId = 2, Quantity = 10, UnitPrice = 10 },
                        new OrderItem() { OrderId = 1, ProductId = 3, Quantity = 10, UnitPrice = 10 },
                        new OrderItem() { OrderId = 2, ProductId = 2, Quantity = 10, UnitPrice = 10 },
                        new OrderItem() { OrderId = 3, ProductId = 3, Quantity = 1, UnitPrice = 100 }
                    },
                    Total = 100
                });
                orderDbContext.orders.Add(new Order()
                {
                    Id = 3,
                    CustomerId = 2,
                    OrderDate = DateTime.Now,
                    Items = new List<OrderItem>()
                    {
                        new OrderItem() { OrderId = 1, ProductId = 1, Quantity = 10, UnitPrice = 10 },
                        new OrderItem() { OrderId = 2, ProductId = 2, Quantity = 10, UnitPrice = 10 },
                        new OrderItem() { OrderId = 3, ProductId = 3, Quantity = 1, UnitPrice = 100 }
                    },
                    Total = 100
                });

                orderDbContext.SaveChanges();
            }

            
        }

        //public async Task<(bool isSuccessful, IEnumerable<Models.OrderItem> orderItems, string errorMessage)> GetAllOrderItemsAsync()
        //{
        //    try
        //    {
        //        var orderItems = await orderDbContext.orderItems.ToListAsync();

        //        if (orderItems != null)
        //        {
        //            var result = mapper.Map<IEnumerable<Db.OrderItem>, IEnumerable<Models.OrderItem>>(orderItems);
        //            return (true, result, null);
        //        }

        //        return (false, null, "Not Found");
        //    }
        //    catch(Exception ex)
        //    {
        //        logger?.LogError(ex.ToString());
        //        return (false, null, ex.Message);
        //    }
            
        //}

        public async Task<(bool isSuccessful, IEnumerable<Models.Order> orders, string errorMessage)> GetAllOrdersAsync()
        {
            try
            {
                var orders = await orderDbContext.orders.ToListAsync();

                if (orders != null && orders.Any())
                {
                    var result = mapper.Map<IEnumerable<Db.Order>, IEnumerable<Models.Order>>(orders);
                    return (true, result, null);
                }

                return (false, null, "Not Found");
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }

        //public async Task<(bool isSuccessful, Models.OrderItem Orders, string errorMessage)> GetOrderItemByIdAsync(int id)
        //{
        //    try
        //    {
        //        var orderItem = await orderDbContext.orderItems.FirstOrDefaultAsync(x => x.Id == id);

        //        if (orderItem != null)
        //        {
        //            var result = mapper.Map<Db.OrderItem, Models.OrderItem>(orderItem);
        //            return (true, result, null);
        //        }

        //        return (false, null, "Not Found");
        //    }
        //    catch (Exception ex)
        //    {
        //        logger?.LogError(ex.ToString());
        //        return (false, null, ex.Message);
        //    }
        //}

        public async Task<(bool isSuccessful, IEnumerable<Models.Order> order, string errorMessage)> GetOrdersByIdAsync(int id)
        {
            try
            {
                var order = await orderDbContext.orders
                    .Where(x => x.CustomerId == id)
                    .Include(x => x.Items)
                    .ToListAsync();

                if (order != null && order.Any())
                {
                    var result = mapper.Map<IEnumerable<Db.Order>, IEnumerable<Models.Order>>(order);
                    return (true, result, null);
                }

                return (false, null, "Not Found");
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }
    }
}
