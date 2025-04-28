using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QualfixAdmin.model.EntitiSecurity
{
    public class Accesos
    {

        [Key]
        public int AccesosId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Nombre { get; set; }

        [MaxLength(255)]
        public string Descripcion { get; set; }


        public ICollection<PerfilAccesos> PerfilAccesos { get; set; }

    }
}
