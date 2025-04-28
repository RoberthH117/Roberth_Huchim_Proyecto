
using Microsoft.AspNetCore.Identity;
using QualfixAdmin.model.EntitiSecurity;
using QualfixAdmin.model.QualfixAdminData;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QualfixAdmin.model
{
    public class User : IdentityUser
    {
         
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? Status { get; set; }
        public bool Activo { get; set; }

        // Agregar una propiedad para la clave foránea
        
        public int? Id_Company { get; set; }

        // Agregar una referencia a la empresa

        

        public ICollection<UsuarioPerfil>? UsuarioPerfiles { get; set; }
        public ICollection<LicenciaUsuario>? LicenciaUsuario { get; set; }

        
    }
}