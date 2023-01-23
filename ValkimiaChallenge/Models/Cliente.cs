﻿namespace ValkimiaChallenge.Models
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Domicilio { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int CuidadId { get; set; }
        public Ciudad Cuidad { get; set; }
        public ICollection<Factura> Facturas { get; set; }
    }
}
