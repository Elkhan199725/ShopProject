using System.ComponentModel;

namespace Shop.Business.Utilities.Exceptions;

public class NotFoundException:Exception
{
    public NotFoundException(string message) : base(message) { }
}
