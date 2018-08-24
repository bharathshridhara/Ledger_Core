using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LedgerCore.Authentication
{
    interface IAuthProvider
    {
        ClaimsPrincipal Authenticate(string identifier);
    }
}
