using GreetingService.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreetingService.Core.Interfaces
{
    internal interface IApprovalService
    {
        public Task BeginUserApprovalAsync(User user);
    }
}
