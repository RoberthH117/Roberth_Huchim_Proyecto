using QualfixAdmin.model.QualfixAdminData;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QualfixAdmin.model.EntitiSecurity
{
    public class Perfil
    {
        [Key]
        public int PerfilId { get; set; }

        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public DateTime Fecha_Creacion { get; set; }
        public DateTime? Fecha_Modificacion { get; set; }
        public string Creado_Por { get; set; }


        // Agregar una propiedad para la clave foránea

     
        public int? Id_Company { get; set; }

        

        public ICollection<UsuarioPerfil> UsuariosPerfiles { get; set; }
        public ICollection<PerfilAccesos> PerfilAccesos { get; set; }



    }
}
