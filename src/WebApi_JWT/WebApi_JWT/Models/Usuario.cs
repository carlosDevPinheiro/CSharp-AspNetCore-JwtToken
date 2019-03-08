using SecurityToken.ProviderJWT;

namespace WebApi_JWT.Models
{
    public class Usuario
    {
        public string name { get; set; }

        public string password { get; set; }

        public ETypeUser tipo { get; set; }

        
    }
}
