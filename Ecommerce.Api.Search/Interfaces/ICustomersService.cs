using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ecommerce.Api.Search.Models;

namespace Ecommerce.Api.Search.Interfaces
{
    public interface ICustomersService
    {
        Task<(bool isSuccess, dynamic Customer, string ErrorMessage)> GetCustomersByIdAsync(int id);
    }
}
