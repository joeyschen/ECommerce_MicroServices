using System;
using System.Threading.Tasks;
using Ecommerce.Api.Orders.Interfaces;
using Ecommerce.Api.Orders.Providers;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Orders.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersProvider ordersProvider;

        public OrdersController(IOrdersProvider ordersProvider)
        {
            this.ordersProvider = ordersProvider;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrdersAsync()
        {
            var result = await ordersProvider.GetAllOrdersAsync();

            if(!result.isSuccessful)
            {
                return NotFound();
            }

            return Ok(result.orders);
        }

        [HttpGet("{customerId}")]
        public async Task<IActionResult> GetOrderByIdAsync(int customerId)
        {
            var result = await ordersProvider.GetOrdersByIdAsync(customerId);

            if (!result.isSuccessful)
            {
                return NotFound();
            }

            return Ok(result.order);
        }

        //[HttpGet]
        //[Route("orderItems")]
        //public async Task<IActionResult> GetOrderItemsAsync()
        //{
        //    var result = await ordersProvider.GetAllOrderItemsAsync();

        //    if (!result.isSuccessful)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(result);
        //}

        //[HttpGet]
        //[Route("orderItems/{id}")]
        //public async Task<IActionResult> GetOrderItemByIdAsync(int id)
        //{
        //    var result = await ordersProvider.GetOrderItemByIdAsync(id);

        //    if (!result.isSuccessful)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(result);
        //}

    }
}
