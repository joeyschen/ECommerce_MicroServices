using System;
using System.Threading.Tasks;
using Ecommerce.Api.Search.Models;

namespace Ecommerce.Api.Search.Interfaces
{
    public interface ISearchService
    {
        Task<(bool isSuccessful, dynamic SearchResults)> SearchAsync(int customerId);
    }
}
