using ValkimiaChallenge.Models;

namespace ValkimiaChallenge.DataService
{
    public class ClienteService
    {
        private FacturasContext _context { get; set; }


        public ClienteService(FacturasContext context) { 
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

        public Cliente? GetCliente(int id) {
            try
            {
                var result = _context.Clientes.Where(
                    cliente => (cliente.Id == id)).FirstOrDefault();
                return result;
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public List<Cliente> GetClientes()
        {
            try
            {
                var result = _context.Clientes.ToList();
                return result;
            }
            catch (Exception ex)
            {

                return null;
            }
        }



        public bool DeleteCliente(int id)
        {
            try
            {
                Cliente cliente = _context.Clientes.Where(
                    cliente => (cliente.Id == id)).FirstOrDefault();

                _context.Remove(cliente);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }


    }
}
