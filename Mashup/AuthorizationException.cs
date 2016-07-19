using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mashup
{
    public class AuthorizationException : Exception
    {
        public AuthorizationException(string message, Exception innerException) : base(message, innerException) { }
    }
}
