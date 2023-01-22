using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ValkimiaChallenge.Models;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace ValkimiaChallenge.Controllers
{
    [ApiController]
    [Route("cliente")]
    public class ClienteController : ControllerBase
    {

        public IConfiguration _configuration { get; set; }

        public ClienteController(IConfiguration configuration) { 
            _configuration = configuration;
        }



        [HttpPost]
        [Route("login")]
        public dynamic login([FromBody] Object optData){

            var data = JsonConvert.DeserializeObject<dynamic>(optData.ToString());
            string user = data.Email.ToString();
            string password = data.Password.ToString();
            Cliente cliente = new Cliente() { 
                Id=1, 
                Nombre= "tomas",
                Apellido = "asdasd",
                Domicilio = "asdasd",
                Email = "asdasd",
                Password = "asdasd",
            };

            if (user == null) {
                //BBDD
                return new
                {
                    success = false,
                    message = "Credenciales incorrectas",
                    result= ""
                };
            }
            var jwt = _configuration.GetSection("Jwt").Get<Jwt>();
            var claims = new[] { 
                new Claim(JwtRegisteredClaimNames.Sub, jwt.Subject),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim("id", cliente.Id.ToString()),
                new Claim("nombre", cliente.Nombre)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(jwt.Issuer, jwt.Auudience, claims, signingCredentials: signIn);
            return new { 
                    success = true,
                    message = "OK",
                    result = new JwtSecurityTokenHandler().WriteToken(token)
                };
        }



        [HttpGet]
        [Route("cliente")]
        public dynamic GetCliente(string id)
        {
            return new Cliente() { Nombre="tomas"};

        }
        [HttpGet]
        [Route("clientes")]
        public dynamic GetClientes()
        {
            List<Cliente> clientes = new List<Cliente>();
            clientes.Add(new Cliente() { Nombre = "tomas" });
            return clientes;

        }

        [HttpPost]
        [Route("guardar")]
        public dynamic GuardarCliente(Cliente cliente)
        {

            cliente.Id = 1;
            return "guardado";

        }
        [HttpPost]
        [Route("eliminar")]
        public dynamic EliminarCliente(string id)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var rToken = Jwt.Validartoken(identity);
            if (!rToken.success) return rToken;


            string token = Request.Headers.Where(x => x.Key == "Authorization").FirstOrDefault().Value;
            if (token == "root") {
                return "Deleted";
            
            }

            return "Error";

        }
    }
}
