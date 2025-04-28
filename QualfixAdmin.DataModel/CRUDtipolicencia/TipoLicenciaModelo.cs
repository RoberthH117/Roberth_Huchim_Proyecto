using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QualfixAdmin.DataModel.CRUDtipolicencia
{
    public class TipoLicenciaModelo
    {

        public int? TipoLicenciaId { get; set; } 

        public string Tipo { get; set; }

        public int Tiempo { get; set; }

        public double Costo { get; set; }




    }



    public class InsertTipoLicenciaModelo
    {
        public string Tipo { get; set; }
        public int Tiempo { get;set; }
        public double Costo { get; set; }

        //public string? Llave { get;set; }

    }

    public class EditarTipoLicencia
    {
        public int TipoLicenciaId { get; set;}

        public string Tipo { get; set; }

        public int Tiempo { get; set; }
        public double Costo { get; set; }


    }


    public class EliminarTipoLicencia
    {
        public int TipoLicenciaId { get;set; }
    }


}
