using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QualfixAdmin.model
{
    public class TipoDeLicencia
    {
        [Key]
        public int? TipoLicenciaId { get; set; }

        // Foreign key to Role
        public string Tipo { get; set; }
        public int Tiempo { get; set; }

        public double Costo { get; set; }

        public string? Llave { get; set; }   





    }
}
