using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Business.Utilities.Exceptions
{
    public class InsufficientQuantityException : Exception
    {
        public InsufficientQuantityException(string message) : base (message) { }
    }
}
