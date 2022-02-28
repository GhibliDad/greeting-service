using GreetingService.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GreetingService.Core.Helpers
{
    public class InputValidationHelper
    {
        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
                return false;

            try
            {
                return Regex.IsMatch(email,
                   @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                   RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch 
            {
                throw new Exception();
            }
        }
    }
}
