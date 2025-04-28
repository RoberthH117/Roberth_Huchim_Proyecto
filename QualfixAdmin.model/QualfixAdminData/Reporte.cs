using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace QualfixAdmin.model.QualfixAdminData
{
    public class Reporte
    {
        [Key]

        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string Estado { get; set; }  
        public string Correo { get; set; }

        public string Numero_Celular { get; set; }

        public string Nombre { get; set; }
        public DateTime Fecha_Solicitud { get; set; }

        public DateTime? Fecha_Recibido { get; set; }
        public List<Imagen>? Imagenes { get; set; }
    }




}
