using System.Security.Claims;

namespace ValkimiaChallenge.Models
{
    public class Jwt
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Auudience { get; set; }
        public string Subject { get; set; }
    }

    //public static dynamic validartoken(ClaimsIdentity identity) {

    //    try { 
    //        if (identity.Claims.Count() == 0)
    //        {
    //            return new
    //            {
    //                success = false,
    //                message = "Verificar si esta especificando el token de forma correcta",
    //                result = ""
    //            };
    //        }

    //        var id = identity.Claims.FirstOrDefault(x => x.Type == "id").Value;
    //        Cliente cliente = Cliente.DB().FirstOrDefault(x => x.Id);
    //        return new
    //        {
    //            success = true,
    //            message = "Exito",
    //            result = cliente
    //        };
    //    } 
        
    //    catch (Exception ex) { 
        
        
    //    }
    
    //}


}
