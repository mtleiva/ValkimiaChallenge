using Microsoft.AspNetCore.Mvc;
using ValkimiaChallenge.Models;

namespace ValkimiaChallenge.Controllers
{
    [ApiController]
    [Route("cliente")]
    public class ClienteController: ControllerBase
    {
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
            string token = Request.Headers.Where(x => x.Key == "Authorization").FirstOrDefault().Value;
            if (token == "root") {
                return "Deleted";
            
            }

            return "Error";

        }
    }
}
