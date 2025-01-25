using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthServiceBulgakov.Domain.Exceptions
{
    public class ValidationApplicationException : Exception
    {
        public ValidationApplicationException(string message) : base(message) { }
    }
}
