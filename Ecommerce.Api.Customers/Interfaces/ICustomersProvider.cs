using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ecommerce.Api.Customers.Interfaces
{
    public interface ICustomersProvider
    {
        Task<(bool isSuccessful, Models.Customer customer, string errorMessage)> GetCustomerByIdAsync(int id);
        Task<(bool isSuccessful, IEnumerable<Models.Customer> customers, string errorMessage)> GetCustomersAsync();

    }
}
