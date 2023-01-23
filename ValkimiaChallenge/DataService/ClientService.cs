using ValkimiaChallenge.Models;

namespace ValkimiaChallenge.DataService
{
    public class ClientService
    {
        private FacturasContext _context { get; set; }


        public ClientService(FacturasContext context) { 
            this._context = context;
        }
        

        public Cliente? Login (string emailCliente, string password){
            try
            {
                var result= _context.Clientes.Where(
                    cliente => (cliente.Email == emailCliente) && 
                                (cliente.Password == password)).FirstOrDefault();
                return result;
            }
            catch(Exception ex)
            {
                
                return null;
            }
        }
    }
}
