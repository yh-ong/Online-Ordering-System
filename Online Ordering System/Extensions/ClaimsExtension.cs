using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace Online_Ordering_System.Extensions
{
    public static class ClaimsExtension
    {
        public static string GetClaimValue(this IPrincipal currentPrincipal, string key)
        {
            var identity = currentPrincipal.Identity as ClaimsIdentity;
            if (identity == null)
                return null;

            var claims = identity.Claims.FirstOrDefault(c => c.Type == key);
                return claims.Value;
        }
    }
}
