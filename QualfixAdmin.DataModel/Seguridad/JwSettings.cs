using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace QualfixAdmin.DataModel.Seguridad
{
    public class JwtSettings
    {
        public string ValidIssuer { get; set; }
        public string ValidAudience { get; set; }
        public string SecretKey { get; set; }
        public string Key { get; set; }

        public class TokenValidationResult
        {
            public bool Success { get; set; }
            public string Message { get; set; }
            public string Result { get; set; }
        }


        public static TokenValidationResult ValidarToken(ClaimsIdentity identity)
        {
            try
            {
                if (identity == null || identity.Claims == null || !identity.Claims.Any())
                {
                    return new TokenValidationResult
                    {
                        Success = false,
                        Message = "Claims vacíos",
                        Result = ""
                    };
                }

                var correoClaim = identity.FindFirst("Name");
                if (correoClaim == null || string.IsNullOrEmpty(correoClaim.Value))
                {
                    return new TokenValidationResult
                    {
                        Success = false,
                        Message = "El claim 'Name' está vacío",
                        Result = ""
                    };
                }

                return new TokenValidationResult
                {
                    Success = true,
                    Message = "Éxito",
                    Result = "Usuario"
                };
            }
            catch (Exception ex)
            {
                return new TokenValidationResult
                {
                    Success = false,
                    Message = "Error",
                    Result = ""
                };
            }
        }

    }
}
