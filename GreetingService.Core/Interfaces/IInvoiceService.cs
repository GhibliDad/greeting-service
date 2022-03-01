using GreetingService.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreetingService.Core.Interfaces
{
    internal interface IInvoiceService
    {
        public Task<IEnumerable<Invoice>> GetInvoices(int year, int month);
        public Task<Invoice> GetInvoice(int year, int month, string email);
        public Task CreateOrUpdateInvoice(Invoice invoice);
    }
}
