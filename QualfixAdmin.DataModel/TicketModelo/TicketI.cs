using Microsoft.AspNetCore.Http;
using QualfixAdmin.model.QualfixAdminData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QualfixAdmin.DataModel.TicketModelo
{
    public class TicketI
    {

   
     




    }

    public class IdRequest
    {
        public string Id { get; set; }
    }


    public class ReporteModelo
    {
        
       public int? Id { get; set; }
        public string? Titulo { get; set; }
        public string? Descripcion { get; set; }
        public string? Estado { get; set; }
        public DateTime? Fecha_Solicitud { get; set; }
      
      
      
        public List<ImagenModelo>? Imagenes { get; set; }
    }



    public class TicketDto
    {
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public List<IFormFile> Imagenes { get; set; }
    }

 


    public class ImagenModelo
    {
        public string Ruta { get; set; }
        public string Nombre { get; set; }
        public long? Tamaño { get; set; }
        public int? ReporteId { get; set; }
        public Reporte? Reporte { get; set; }


    }













}
