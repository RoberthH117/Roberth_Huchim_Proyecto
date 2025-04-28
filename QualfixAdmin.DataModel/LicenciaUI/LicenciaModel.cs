using QualfixAdmin.model.EntitiSecurity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace QualfixAdmin.DataModel.LicenciaUI
{
    public  class LicenciaModel
    {
       //public DateTime Fecha_Expiracion { get;set; }

      public  int TipoId { get;set; }

      public  string Id_Usuario { get;set; }



    }



    public class LicenciaModelo
    {
        public int? LicenciaId { get; set; }
        public string Licencia { get; set; }

        public DateTime Fecha_Inicio { get; set; }

        public DateTime Fecha_Expiracion { get; set; }

        public string Creado_Por { get; set; }
    }


    public class LicenciaConTiempo
    {
        
        public int LicenciaId { get; set; }
        public string Licencia { get; set; }

        public DateTime Fecha_Inicio { get; set; }

        public DateTime Fecha_Expiracion { get; set; }

        public string Creado_Por { get; set; }



        public string UserId { get; set; }

        public string Tiempo { get; set; }


        public ICollection<LicenciaUsuario>? LicenciaUsuario { get; set; }


    }


    public class UserLicenseInfo
    {
        public string UserId { get; set; }
        public int LicenseId { get; set; }
        public string LicenseName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ExpirationDate { get; set; }
    }

    public class usuarioLicencia
    {
        public string UserId { get; set; }

        public int LicenciaId { get; set; }

    }


}
