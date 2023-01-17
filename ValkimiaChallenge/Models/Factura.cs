namespace ValkimiaChallenge.Models
{
    public class Factura
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string Detalle { get; set; }
        public decimal Importe { get; set; }

    }
}
