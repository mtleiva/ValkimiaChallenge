using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ValkimiaChallenge.DataService;
using ValkimiaChallenge.Models;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace ValkimiaChallenge.Controllers
{
    [ApiController]
    [Route("cliente")]
    public class ClienteController : ControllerBase
    {

        public IConfiguration _configuration { get; set; }
        private FacturasContext _context { get; set; }

        public ClienteController(IConfiguration configuration, FacturasContext facturasContext) { 
            _configuration = configuration;
            _context = facturasContext;
        }



        [HttpPost]
        [Route("login")]
        public dynamic login([FromBody] Object optData){

            var data = JsonConvert.DeserializeObject<dynamic>(optData.ToString());
            string emailCliente = data.Email.ToString();
            string password = data.Password.ToString();
            var cliente = new ClienteService(_context).Login(emailCliente, password);
            if (cliente == null) {
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


            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                //var rToken = Jwt.Validartoken(identity, _context);
                //if (!rToken.success) return rToken;
                //string token = Request.Headers.Where(x => x.Key == "Authorization").FirstOrDefault().Value;
                //if (token != null) {
                var cs = new ClienteService(_context);
                int idCliente = Int32.Parse(GetNumbers(id));
                var result = cs.GetCliente(idCliente);
                if (result!= null)
                {
                    return result;
                }
                //}
                return "Error";
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }

        }
        [HttpGet]
        [Route("clientes")]
        public dynamic GetClientes()
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                //var rToken = Jwt.Validartoken(identity, _context);
                //if (!rToken.success) return rToken;
                //string token = Request.Headers.Where(x => x.Key == "Authorization").FirstOrDefault().Value;
                //if (token != null) {
                var cs = new ClienteService(_context);
                var result = cs.GetClientes();
                if (result != null)
                {
                    return result;
                }
                //}
                return "Error";
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }

        }

        [HttpPost]
        [Route("guardar")]
        public dynamic GuardarCliente(ClienteDTO cliente)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                var cs = new ClienteService(_context);
                cs.SetCliente(cliente);
                return "guardado";
            }
            catch(Exception ex)
            {
                return "Error";
            }

        }
        [HttpPost]
        [Route("eliminar")]
        public dynamic EliminarCliente(string id)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                //var rToken = Jwt.Validartoken(identity, _context);
                //if (!rToken.success) return rToken;
                //string token = Request.Headers.Where(x => x.Key == "Authorization").FirstOrDefault().Value;
                //if (token != null) {
                var cs = new ClienteService(_context);
                int idCliente = Int32.Parse(GetNumbers(id));
                bool result = cs.DeleteCliente(idCliente);
                if (result)
                {
                    return "Deleted";
                }
                //}
                return "Error";
            }
            catch(Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }
        private static string GetNumbers(string input)
        {
            return new string(input.Where(c => char.IsDigit(c)).ToArray());
        }
    }
}
