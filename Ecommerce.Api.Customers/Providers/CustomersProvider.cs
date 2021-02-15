using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Ecommerce.Api.Customers.Db;
using Ecommerce.Api.Customers.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Ecommerce.Api.Customers.Providers
{
    public class CustomersProvider : ICustomersProvider
    {
        private readonly CustomersDbContext customersDbContext;
        private readonly ILogger<CustomersProvider> logger;
        private readonly IMapper mapper;

        public CustomersProvider(CustomersDbContext customersDbContext, ILogger<CustomersProvider> logger, IMapper mapper)
        {
            this.customersDbContext = customersDbContext;
            this.logger = logger;
            this.mapper = mapper;

            SeedData();
        }

        private void SeedData()
        {
            if (!customersDbContext.customers.Any())
            {
                customersDbContext.customers.Add(new Customer { Id = 1, Address = "1234 Main St", Name = "John" });
                customersDbContext.customers.Add(new Customer { Id = 2, Address = "1235 Main St", Name = "James" });
                customersDbContext.customers.Add(new Customer { Id = 3, Address = "1236 Main St", Name = "Joey" });
                customersDbContext.SaveChanges();
            }
            
        }

        public async Task<(bool isSuccessful, IEnumerable<Models.Customer> customers, string errorMessage)> GetCustomersAsync()
        {
            try
            {
                var customers = await customersDbContext.customers.ToListAsync();

                if (customers != null && customers.Any())
                {
                    var results = mapper.Map<IEnumerable<Db.Customer>, IEnumerable<Models.Customer>>(customers);
                    return (true, results, null);
                }

                return (false, null, "Customers not found");
            }
            catch(Exception ex)
            {
                logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
            

        }
        public async Task<(bool isSuccessful, Models.Customer customer, string errorMessage)> GetCustomerByIdAsync(int id)
        {
            try
            {
                var customer = await customersDbContext.customers.FirstOrDefaultAsync(x => x.Id == id);

                if (customer != null)
                {
                    var results = mapper.Map<Db.Customer, Models.Customer>(customer);
                    return (true, results, null);
                }

                return (false, null, "Customers not found");
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }


        }
    }
}

