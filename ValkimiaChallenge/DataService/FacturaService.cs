using ValkimiaChallenge.Models;
using ValkimiaChallenge.Models.DTO;

namespace ValkimiaChallenge.DataService
{
    public class FacturaService
    {
        private FacturasContext _context { get; set; }


        public FacturaService(FacturasContext context) { 
            this._context = context;
        }
        

        public Factura? GetFactura(int id) {
            try
            {
                var result = _context.Facturas.Where(
                    factura => (factura.Id == id)).FirstOrDefault();
                return result;
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public List<Factura> GetFacturas()
        {
            try
            {
                var result = _context.Facturas.ToList();
                return result;
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public List<Factura> GetFacturas(int clienteId)
        {
            try
            {
                var result = _context.Facturas.Where(factura => factura.ClienteId == clienteId).ToList();
                return result;
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public void SetFactura(FacturaDTO facturaDTO)
        {
            try
            {

                Factura newFactura = new Factura();
                newFactura.Fecha = DateTime.Now ;
                newFactura.Detalle = facturaDTO.Detalle ;
                newFactura.Importe = facturaDTO.Importe ;
                newFactura.ClienteId = facturaDTO.ClienteId ;
                _context.Add(newFactura);
                _context.SaveChanges();

            }
            catch (Exception ex)
            {

                throw new ApplicationException("Error cargando Factura");
            }

        }

        public bool DeleteFactura(int id)
        {
            try
            {
                Factura factura = _context.Facturas.Where(
                    factura => (factura.Id == id)).FirstOrDefault();

                _context.Remove(factura);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }


        //public Factura GetFacturaModel(FacturaDTO facturaDTO)
        //{
        //    Factura newFactura = new Factura();
        //    newFactura.Nombre = facturaDTO.Nombre;
        //    newFactura.Apellido = facturaDTO.Apellido;
        //    newFactura.Domicilio = facturaDTO.Domicilio;
        //    newFactura.Email = facturaDTO.Email;
        //    newFactura.Password = facturaDTO.Password;
        //    newFactura.CuidadId = facturaDTO.CuidadId;
        //    return newFactura;
        //}


    }
}
