using GreetingService.Core.Entities;
using GreetingService.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
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

        public async Task CreateOrUpdateInvoiceAsync(Invoice invoice)
        {
            var existingInvoice = await _greetingDbContext.Invoices.FirstOrDefaultAsync(x => x.Year == invoice.Year && x.Month == invoice.Month && x.Sender.Email.Equals(invoice.Sender.Email));
            if (existingInvoice == null)        //invoice does not exist for user and month, insert a new row
            {
                await _greetingDbContext.AddAsync(invoice);
                await _greetingDbContext.SaveChangesAsync();
            }
            else                                //invoice for user and month already exists, update greetings list and total cost
            {
                existingInvoice.Greetings = invoice.Greetings;
                existingInvoice.TotalCost = invoice.TotalCost;
                await _greetingDbContext.SaveChangesAsync();
            }
        }
        public async Task<Invoice> GetInvoiceAsync(int year, int month, string email)
        {
            //Only one invoice per user and month should exist
            //Relations are not included by default, here we use .Include() to explicitly state that the query result should include the relations for Greetings and Sender
            var invoice = await _greetingDbContext.Invoices.Include(x => x.Greetings)
                                                           .Include(x => x.Sender)
                                                           .FirstOrDefaultAsync(x => x.Year == year && x.Month == month && x.Sender.Email.Equals(email));
            return invoice;
        }

        public async Task<IEnumerable<Invoice>> GetInvoicesAsync(int year, int month)
        {
                //Relations are not included by default, here we use .Include() to explicitly state that the query result should include the relations for Greetings and Sender
                var invoices = await _greetingDbContext.Invoices.Include(x => x.Greetings)
                                                                .Include(x => x.Sender)
                                                                .Where(x => x.Year == year && x.Month == month).ToListAsync();
                return invoices;
        }
    }
}
