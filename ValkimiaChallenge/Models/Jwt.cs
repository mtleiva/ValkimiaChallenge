using System.Security.Claims;

namespace ValkimiaChallenge.Models
{
    public class Jwt
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Auudience { get; set; }
        public string Subject { get; set; }


        public static dynamic Validartoken(ClaimsIdentity identity)
        {

            try
            {
                if (identity.Claims.Count() == 0)
                {
                    return new
                    {
                        success = false,
                        message = "Verificar si esta especificando el token de forma correcta",
                        result = ""
                    };
                }

                var id = identity.Claims.FirstOrDefault(x => x.Type == "id").Value;
                //BBDD
                // Cliente cliente = context.Cliente.Where(client=> client.id = id).FirstOrDefault();

                Cliente cliente = new Cliente()
                {
                    Id = Int32.Parse(id)

                };
                return new
                {
                    success = true,
                    message = "Exito",
                    result = cliente
                };
            }

            catch (Exception ex)
            {
                return new
                {
                    success = false,
                    message = "Catch: " + ex.Message,
                    result = ""
                };

            }

        }

    }
}

