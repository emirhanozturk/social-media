using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions
{
    public class AuthenticationException : Exception
    {
        public AuthenticationException(): base("Kimlik doğrulama hatası!")
        {
        }

        public AuthenticationException(string message) : base(message) 
        { }

        public AuthenticationException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
