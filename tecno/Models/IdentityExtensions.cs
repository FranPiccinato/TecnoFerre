using System.Security.Claims;
using System.Security.Principal;

namespace tecno.Models
{
    public static class IdentityExtensions
    {
        public static string GetNombre(this IIdentity identity)
        {
            ClaimsIdentity claimsIdentity = identity as ClaimsIdentity;
            Claim claim = claimsIdentity?.FindFirst(ClaimTypes.Name);

            return claim?.Value ?? string.Empty;
        }


        public static string GetRol(this IIdentity identity)
        {
            ClaimsIdentity claimsIdentity = identity as ClaimsIdentity;
            Claim claim = claimsIdentity?.FindFirst(ClaimTypes.Role);

            return claim?.Value ?? string.Empty;
        }

        public static string GetCedula(this IIdentity identity)
        {
            ClaimsIdentity claimsIdentity = identity as ClaimsIdentity;
            Claim claim = claimsIdentity?.FindFirst("Cedula");

            return claim?.Value ?? string.Empty;
        }

        public static string GetCorreo(this IIdentity identity)
        {
            ClaimsIdentity claimsIdentity = identity as ClaimsIdentity;
            Claim claim = claimsIdentity?.FindFirst("Correo");

            return claim?.Value ?? string.Empty;
        }
    }
}
