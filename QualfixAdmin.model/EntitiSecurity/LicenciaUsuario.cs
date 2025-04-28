using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QualfixAdmin.model.EntitiSecurity
{
    public class LicenciaUsuario
    {
        [ForeignKey("UserId")]
        public string UserId { get; set; }
        public Licencias Licencia { get; set; }



        [ForeignKey("LicenciaId")]
        public int LicenciaId { get; set; }

        public User Usuario { get; set; }
       
    }
}
