using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QualfixAdmin.model.EntitiSecurity
{
    public class PerfilAccesos
    {



        [ForeignKey("PerfilId")]
        public int PerfilId { get; set; }
        public Perfil Perfil { get; set; }

        // Foreign key to Permission
        [ForeignKey("AccesosId")]
        public int AccesosId { get; set; }
        public Accesos Accesos { get; set; }
    }
}
