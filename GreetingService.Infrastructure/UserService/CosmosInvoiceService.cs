using GreetingService.Core.Entities;
using GreetingService.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreetingService.Infrastructure.UserService
{
    public class CosmosInvoiceService : IInvoiceService
    {
        public async Task CreateOrUpdateInvoiceAsync(Invoice invoice)
        {
            throw new NotImplementedException();
        }

        public async Task<Invoice> GetInvoiceAsync(int year, int month, string email)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Invoice>> GetInvoicesAsync(int year, int month)
        {
            throw new NotImplementedException();
        }
    }
}
