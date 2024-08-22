using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using servicio.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ServicioApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutenticacionController : Controller
    {

        private readonly string secretKey;
        private readonly AppDbContext _context;

        public AutenticacionController(IConfiguration config, AppDbContext context)
        {
            secretKey = config.GetValue<string>("settings:secretkey");
            _context = context;
        }

        [HttpPost]
        [Route("Validar")]
        public async Task<IActionResult> Validar([FromBody] UsuarioRequest request)
        {
            if (_context == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error de configuración del servidor.");
            }
            var usuario = await _context.Usuarios
            .FirstOrDefaultAsync(u => u.correo == request.correo);

            if (usuario == null || usuario.contrasena != request.contrasena)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, new { token = "" });
            }

            var keyBytes = Encoding.UTF8.GetBytes(secretKey);
            var claims = new ClaimsIdentity();
            claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, request.correo));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);

            string tokencreado = tokenHandler.WriteToken(tokenConfig);

            return StatusCode(StatusCodes.Status200OK, new { token = tokencreado });


        }



    }
}
