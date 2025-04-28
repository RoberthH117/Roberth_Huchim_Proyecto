using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QualfixAdmin.model.EntitiSecurity
{
    public class Licencias
    {
        [Key]
        public int LicenciaId { get; set; }
        public string Licencia { get; set; }   

        public DateTime Fecha_Inicio { get; set; }

        public DateTime Fecha_Expiracion { get; set; }

        public string Creado_Por { get; set; }


      
        public string UserId { get; set; }

    
        public ICollection<LicenciaUsuario>? LicenciaUsuario { get; set; }


    }
}
