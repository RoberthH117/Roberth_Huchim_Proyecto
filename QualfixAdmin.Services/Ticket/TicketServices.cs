using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using QualfixAdmin.dal.DBContext;

using QualfixAdmin.DataModel.TicketModelo;
using QualfixAdmin.model;
using QualfixAdmin.model.QualfixAdminData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QualfixAdmin.Services.Ticket
{
    public class TicketServices
    {

        private readonly QualfixAdminContext _context;
     
        public TicketServices(QualfixAdminContext context) { 
        
            _context = context;
            

        }



        public async Task CrearReporte(ReporteModelo reporte)
        {
            var insertar = new model.QualfixAdminData.Reporte()
            {
                Descripcion = reporte.Descripcion,
                Estado = reporte.Estado,
                Fecha_Solicitud = (DateTime)reporte.Fecha_Solicitud,
                Titulo = reporte.Titulo
            };

            _context.Reporte.Add(insertar);
            _context.SaveChanges();

            foreach (var item in reporte.Imagenes)
            {
                var image = new model.QualfixAdminData.Imagen()
                {
                    Nombre = item.Nombre,
                    Ruta = item.Ruta,
                    Tamaño = item.Tamaño,
                    Reporte = insertar
                };

                _context.Imagen.Add(image);
            }

            _context.SaveChangesAsync();
        }




        public async Task<object> ProcesarTicket(TicketDto ticketDto, User usuario)
        {
            // Crear una instancia del servicio que contiene el método ProcesarImagenes
            // Asegúrate de usar el nombre correcto de tu servicio

            var ticket = new model.QualfixAdminData.Reporte
            {
                Titulo = ticketDto.Titulo,
                Descripcion = ticketDto.Descripcion,
                Estado = "Pendiente",
                Fecha_Recibido = DateTime.Now,
                Nombre=usuario.FirstName,
                Correo=usuario.Email,
                Numero_Celular=usuario.PhoneNumber
                //Imagenes = rutasImagenes
            };

            var resultado = _context.Reporte.Add(ticket);
           var guardar= await _context.SaveChangesAsync();


            // Validar y procesar las imágenes (guardar localmente y obtener rutas)
            var rutasImagenes = await ProcesarImagenes(ticketDto.Imagenes, ticket.Id);

            ticket.Imagenes = rutasImagenes;


            var actualizar = _context.Reporte.Update(ticket);
            // Crear objeto Ticket y guardarlo en la base de datos
           

          

            

            return ticket;
        }




        public async Task<object> UpdateTicket(ReporteModelo ticketDto)
        {
            // Crear una instancia del servicio que contiene el método ProcesarImagenes
            // Asegúrate de usar el nombre correcto de tu servicio


            var modelo = _context.Reporte.FirstOrDefault(r => r.Id == ticketDto.Id);

          


            if(ticketDto.Estado =="Completado" || ticketDto.Estado=="completado") {

                modelo.Fecha_Recibido = DateTime.Now;
                modelo.Estado = "Completado";
            }

            


           

            var actualizar = _context.Reporte.Update(modelo);
            var guardar = _context.SaveChanges();
            // Crear objeto Ticket y guardarlo en la base de datos






            return modelo;
        }





        public async Task<List<Imagen>> ProcesarImagenes(List<IFormFile> imagenes, int id)
        {
            var rutasImagenes = new List<Imagen>();
            var directorioImagenes = @"C:\Users\9241422247\Documents\Evidencias_Pagina";

            if (!Directory.Exists(directorioImagenes))
            {
                Directory.CreateDirectory(directorioImagenes);
            }

            foreach (var imagen in imagenes)
            {
                var nombreUnico = Guid.NewGuid().ToString() + "_" + imagen.FileName; // Genera un nombre único para la imagen
                var rutaImagen = Path.Combine(directorioImagenes, nombreUnico);

                using (var stream = new FileStream(rutaImagen, FileMode.Create))
                {
                    await imagen.CopyToAsync(stream);
                }


                var base64String = Convert.ToBase64String(File.ReadAllBytes(rutaImagen));




                var imagenInfo = new Imagen
                {
                    Ruta = base64String,
                    Nombre = nombreUnico,
                    Tamaño = imagen.Length,
                    ReporteId=id
                    
                    
                };

                rutasImagenes.Add(imagenInfo);

              var resultado=  _context.Imagen.Add(imagenInfo);
                _context.SaveChanges();


            }

            return rutasImagenes;
        }



        public async Task<List<ReporteModelo>> GetReporte()
        {
            var reportes = from r in _context.Reporte
                           where r.Estado == "Pendiente"
                           select new ReporteModelo
                           {
                               Id=r.Id,
                               Titulo = r.Titulo,
                               Descripcion = r.Descripcion,
                               Estado = r.Estado,
                               Fecha_Solicitud = r.Fecha_Solicitud,
                               Imagenes = (from i in r.Imagenes
                                           select new ImagenModelo
                                           {
                                               Nombre = i.Nombre,
                                               Ruta = i.Ruta,
                                               Tamaño = i.Tamaño
                                           }).ToList()
                           };
            return reportes.ToList();
        }

     

        public async Task<List<ReporteModelo>> FindTicket(User usuario)
        {
            var reportes = from r in _context.Reporte
                           where  r.Correo==usuario.Email
                           select new ReporteModelo
                           {
                               Id = r.Id,
                               Titulo = r.Titulo,
                               Descripcion = r.Descripcion,
                               Estado = r.Estado,
                               Fecha_Solicitud = r.Fecha_Solicitud,
                               Imagenes = (from i in r.Imagenes
                                           select new ImagenModelo
                                           {
                                               Nombre = i.Nombre,
                                               Ruta = i.Ruta,
                                               Tamaño = i.Tamaño
                                           }).ToList()
                           };
            return reportes.ToList();
        }



        public async Task<List<ReporteModelo>> GetIdTicket(int id)
        {
            var reportes = from r in _context.Reporte
                           where r.Estado == "Pendiente" && r.Id==id
                           select new ReporteModelo
                           {
                               Id = r.Id,
                               Titulo = r.Titulo,
                               Descripcion = r.Descripcion,
                               Estado = r.Estado,
                               Fecha_Solicitud = r.Fecha_Solicitud,
                               Imagenes = (from i in r.Imagenes
                                           select new ImagenModelo
                                           {
                                               Nombre = i.Nombre,
                                               Ruta = i.Ruta,
                                               Tamaño = i.Tamaño
                                           }).ToList()
                           };
            return reportes.ToList();
        }



    }
}
