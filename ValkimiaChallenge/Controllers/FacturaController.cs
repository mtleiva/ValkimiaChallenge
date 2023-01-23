using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ValkimiaChallenge.DataService;
using ValkimiaChallenge.Models;
using ValkimiaChallenge.Models.DTO;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace ValkimiaChallenge.Controllers
{
    [ApiController]
    [Route("factura")]
    public class FacturaController : ControllerBase
    {

        public IConfiguration _configuration { get; set; }
        private FacturasContext _context { get; set; }

        public FacturaController(IConfiguration configuration, FacturasContext facturasContext)
        {
            _configuration = configuration;
            _context = facturasContext;
        }




        [HttpGet]
        [Route("factura")]
        public dynamic GetFactura(string id)
        {


            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                //var rToken = Jwt.Validartoken(identity, _context);
                //if (!rToken.success) return rToken;
                //string token = Request.Headers.Where(x => x.Key == "Authorization").FirstOrDefault().Value;
                //if (token != null) {
                var cs = new FacturaService(_context);
                int idFactura = Int32.Parse(GetNumbers(id));
                var result = cs.GetFactura(idFactura);
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
        [HttpGet]
        [Route("facturas")]
        public dynamic GetFacturas()
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                //var rToken = Jwt.Validartoken(identity, _context);
                //if (!rToken.success) return rToken;
                //string token = Request.Headers.Where(x => x.Key == "Authorization").FirstOrDefault().Value;
                //if (token != null) {
                var cs = new FacturaService(_context);
                var result = cs.GetFacturas();
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
        public dynamic GuardarFactura(FacturaDTO factura)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                var cs = new FacturaService(_context);
                cs.SetFactura(factura);
                return "guardado";
            }
            catch (Exception ex)
            {
                return "Error";
            }

        }
        [HttpPost]
        [Route("eliminar")]
        public dynamic EliminarFactura(string id)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                //var rToken = Jwt.Validartoken(identity, _context);
                //if (!rToken.success) return rToken;
                //string token = Request.Headers.Where(x => x.Key == "Authorization").FirstOrDefault().Value;
                //if (token != null) {
                var cs = new FacturaService(_context);
                int idFactura = Int32.Parse(GetNumbers(id));
                bool result = cs.DeleteFactura(idFactura);
                if (result)
                {
                    return "Deleted";
                }
                //}
                return "Error";
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }
        private string GetNumbers(string input)
        {
            return new string(input.Where(c => char.IsDigit(c)).ToArray());
        }

        [HttpGet]
        [Route("facturasById")]
        public dynamic GetFacturasById(string usuarioId)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                //var rToken = Jwt.Validartoken(identity, _context);
                //if (!rToken.success) return rToken;
                //string token = Request.Headers.Where(x => x.Key == "Authorization").FirstOrDefault().Value;
                //if (token != null) {
                var cs = new FacturaService(_context);
                int id = Int32.Parse(GetNumbers(usuarioId));
                var result = cs.GetFacturas(id);
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
    }
}
