using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Business.DTOs;

public class LoginResult
{
    public bool IsAuthenticated { get; set; }
    public bool IsAdmin { get; set; }
}