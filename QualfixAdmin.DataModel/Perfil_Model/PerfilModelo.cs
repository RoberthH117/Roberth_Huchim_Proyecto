
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Metadata;

namespace QualfixAdmin.DataModel.Perfil_Model
{
    public class PerfilModelo
    {

        public int PerfilId { get; set; }

        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public DateTime Fecha_Creacion { get; set; }
        public DateTime? Fecha_Modificacion { get; set; }
        public string Creado_Por { get; set; }


        public int? Id_Company { get; set; }

    


    }

    public class PerfilConAccesosDto
    {
        public int? PerfilId { get; set; }
        public string Perfil { get; set; }
        public int? Id_Company { get; set; }
        public string Descripcion { get; set; }
        public List<string> Accesos { get; set; }
    }

    public class PerfilConAccesosDtoUpdate
    {
        public int PerfilId { get; set; }
        public string Perfil { get; set; }
        public int? Id_Company { get; set; }
        public string Descripcion { get; set; }
        public List<string> Accesos { get; set; }
    }






    public class PerfilIdModel
    {
        public int PerfilId { get; set; }
    }

    public class PerfilAsignar
    {
        public int PerfilId { get; set; }
        public string UserId { get; set;}
    }

    public class GetAllAccesosRequest
    {
        public int AccesosId { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }


    }

    public class AccesosPorIdResponse
    {
        public List<GetAllAccesosRequest> AccesosDisponibles { get; set; }
        public List<GetAllAccesosRequest> AccesosAsignados { get; set; }
    }


}
