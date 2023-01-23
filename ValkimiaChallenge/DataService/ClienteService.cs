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

        public void SetCliente(ClienteDTO clienteDTO)
        {
            try
            {

                Cliente newCliente = new Cliente();
                newCliente.Nombre = clienteDTO.Nombre;
                newCliente.Apellido = clienteDTO.Apellido;
                newCliente.Domicilio = clienteDTO.Domicilio;
                newCliente.Email = clienteDTO.Email;
                newCliente.Password = clienteDTO.Password;
                newCliente.CuidadId = clienteDTO.CuidadId;
                _context.Add(newCliente);
                _context.SaveChanges();

            }
            catch (Exception ex)
            {

                throw new ApplicationException("Error cargando Cliente");
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


        //public Cliente GetClienteModel(ClienteDTO clienteDTO)
        //{
        //    Cliente newCliente = new Cliente();
        //    newCliente.Nombre = clienteDTO.Nombre;
        //    newCliente.Apellido = clienteDTO.Apellido;
        //    newCliente.Domicilio = clienteDTO.Domicilio;
        //    newCliente.Email = clienteDTO.Email;
        //    newCliente.Password = clienteDTO.Password;
        //    newCliente.CuidadId = clienteDTO.CuidadId;
        //    return newCliente;
        //}


    }
}
