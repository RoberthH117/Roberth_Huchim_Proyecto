using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QualfixAdmin.model.QualfixAdminData
{
    public class Imagen
    {
        [Key]
        public int Id { get; set; }
        public string Ruta { get; set; }
        public string Nombre { get; set; }
        public long? Tamaño { get; set; }
        public int? ReporteId { get; set; }
        public Reporte? Reporte { get; set; }


    }
}
