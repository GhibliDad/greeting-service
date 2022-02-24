using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreetingService.Core
{
    public interface IUserService
    {
        public Task<bool> IsValidUserAsync(string username, string password);
        public bool IsValidUser(string username, string password);
    }
}
