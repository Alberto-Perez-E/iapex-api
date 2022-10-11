using iapex.Data;
using iapex.Models;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;

namespace iapex.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private DataContext _context = null;
        private readonly IConfiguration _config;
        public LoginController(DataContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        // POST: api/Login/Web

        [AllowAnonymous]
        [HttpPost("Web")]
        public async Task<ActionResult> PostLoginWeb(Login login)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            User Usuario = await _context.User.Where(x => x.Correo == login.Usuario).FirstOrDefaultAsync();
            if (Usuario == null)
            {
                return NotFound();
            }

            if(login.Contrasena == Usuario.Contrasena)
            {
                var secretKey = _config.GetValue<string>("SecretKey");
                var key = Encoding.ASCII.GetBytes(secretKey);

                var claims = new ClaimsIdentity();
                claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, login.Usuario));

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = claims,
                    Expires = DateTime.UtcNow.AddHours(4),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var createdToken = tokenHandler.CreateToken(tokenDescriptor);

                string bearer_token = tokenHandler.WriteToken(createdToken);

                var datos = new Dictionary<string, string>();
                datos.Add("Nombre", Usuario.Nombre.ToString());
                datos.Add("Rol", Usuario.Rol.ToString());

                Employee Empleado = await _context.Employee.Where(x => x.Id_user == Usuario.Id).FirstOrDefaultAsync();
                Hospital Hospital = await _context.Hospital.Where(x => x.Id_user == Usuario.Id).FirstOrDefaultAsync();
                if (Empleado != null)
                {
                    datos.Add("IdHospital", Empleado.Id_hospital.ToString());
                } else if (Hospital != null) {
                    datos.Add("IdHospital", Hospital.Id.ToString());
                }

                var respuesta = new Dictionary<string, object>();
                respuesta.Add("DatosUsuario", datos);
                respuesta.Add("Token", bearer_token);

                return Ok(respuesta);
            }
            else
            {
                return NotFound();
            }
        }

        // POST: api/Login/Movil

        [AllowAnonymous]
        [HttpPost("Movil")]
        public async Task<ActionResult> PostLoginMovil(Login login)
        {
            var invalidar = new Dictionary<string, string>();
            invalidar.Add("Ok", "false");

            if (!ModelState.IsValid)
            {
                return BadRequest(invalidar);
            }

            User Usuario = await _context.User.Where(x => x.Correo == login.Usuario).FirstOrDefaultAsync();
            if (Usuario == null)
            {
                return NotFound(invalidar);
            }

            if (login.Contrasena == Usuario.Contrasena)
            {
                var secretKey = _config.GetValue<string>("SecretKey");
                var key = Encoding.ASCII.GetBytes(secretKey);

                var claims = new ClaimsIdentity();
                claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, login.Usuario));

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = claims,
                    Expires = DateTime.UtcNow.AddHours(4),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var createdToken = tokenHandler.CreateToken(tokenDescriptor);

                string bearer_token = tokenHandler.WriteToken(createdToken);

                //Employee Empleado = await _context.Employee.Where(x => x.Id_user == Usuario.Id).FirstOrDefaultAsync();
                //if (Empleado == null)
                //{
                //    return NotFound(invalidar);
                //}

                //Hospital Hospital = await _context.Hospital.Where(x => x.Id == Empleado.Id_hospital).FirstOrDefaultAsync();
                //if (Hospital == null)
                //{
                //    return NotFound(invalidar);
                //}

                var datos = new Dictionary<string, string>();
                datos.Add("Nombre", Usuario.Nombre.ToString());
                datos.Add("ApellidoPaterno", Usuario.Apellido_paterno.ToString());
                datos.Add("ApellidoMaterno", Usuario.Apellido_materno.ToString());
                //datos.Add("NombreHospital", Hospital.Nombre_hospital.ToString());

                var validar = new Dictionary<string, object>();
                validar.Add("DatosUsuario", datos);
                validar.Add("Token", bearer_token);
                validar.Add("Ok", "true");

                return Ok(validar);
            }
            else
            {
                return NotFound(invalidar);
            }
        }
    }
}
