using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QualfixAdmin.model.EntitiSecurity
{
    public class UsuarioPerfil
    {
        public string UserId { get; set; }
        public int PerfilId { get; set; }

        public User Usuario { get; set; }
        public Perfil Perfil { get; set; }
    }
}
