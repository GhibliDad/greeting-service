using GreetingService.Core.Exceptions;
using GreetingService.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreetingService.Core.Entities
{
    public class User
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
        private string _email;
        public string Email 
        {
            get
            {
                return _email;
            }
            set
            {
                if (!InputValidationHelper.IsValidEmail(value))
                    throw new InvalidEmailException($"{value} is not a valid email.");

                _email = value;
            }
        }
        public string Password { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime Modified { get; set; } = DateTime.Now;
    }
}
