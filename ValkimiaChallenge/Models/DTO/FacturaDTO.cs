namespace ValkimiaChallenge.Models.DTO
{
    public class FacturaDTO
    {
        public int? Id { get; set; }
        public DateTime? Fecha { get; set; }
        public string Detalle { get; set; }
        public decimal Importe { get; set; }
        public int ClienteId { get; set; }
    }
}
