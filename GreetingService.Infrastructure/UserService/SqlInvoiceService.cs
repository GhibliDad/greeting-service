using GreetingService.Core.Entities;
using GreetingService.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreetingService.Infrastructure.UserService
{
    public class SqlInvoiceService : IInvoiceService
    {
        private readonly GreetingDbContext _greetingDbContext;

        public SqlInvoiceService(GreetingDbContext greetingDbContext)
        {
            _greetingDbContext = greetingDbContext;
        }

        public Task CreateOrUpdateInvoice(Invoice invoice)
        {
            throw new NotImplementedException();
        }

        public Task<Invoice> GetInvoice(int year, int month, string email)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Invoice>> GetInvoices(int year, int month)
        {
            throw new NotImplementedException();
        }
    }
}
