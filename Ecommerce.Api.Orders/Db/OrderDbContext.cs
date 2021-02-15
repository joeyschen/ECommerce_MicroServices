using System;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Api.Orders.Db
{
    public class OrderDbContext : DbContext
    {
        public DbSet<Order> orders { get; set; }
        public DbSet<OrderItem> orderItems { get; set; }

        public OrderDbContext(DbContextOptions options) : base(options) 
        {
        }
    }
}
