using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using QualfixAdmin.dal.DBContext;
using QualfixAdmin.DataModel.CRUDtipolicencia;
using QualfixAdmin.DataModel.UsuarioModel;
using QualfixAdmin.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QualfixAdmin.Services.CreacionTipoLicencia
{
    public class TipoLicencia

    {
        private readonly QualfixAdminContext _Admin;

        public TipoLicencia(QualfixAdminContext Admin)
        {

            _Admin = Admin;


        }




        public async Task<List<TipoLicenciaModelo>> GetAllTipoLicencia ()
        {
            try
            {
                var licencia = await _Admin.TipoDeLicencia
                    .Select(r => new TipoLicenciaModelo
                    {
                        TipoLicenciaId = r.TipoLicenciaId,
                        Tipo = r.Tipo,
                        Tiempo = r.Tiempo,
                        Costo=r.Costo
                    })
                    .ToListAsync();

                return licencia;
            }
            catch (Exception ex)
            {
                // Manejo de errores
                Exception e = ex;
                return null;
            }
        }

        public async Task<TipoLicenciaModelo> GetIdTipoLicencia(EliminarTipoLicencia Eliminar)
        {
            try
            {

                var find = await _Admin.TipoDeLicencia.FindAsync(Eliminar.TipoLicenciaId);

                var modelo = new TipoLicenciaModelo
                {
                    TipoLicenciaId = find.TipoLicenciaId,
                    Tipo = find.Tipo,
                    Tiempo = find.Tiempo,
                    Costo=find.Costo
                };
                

                return modelo;
            }
            catch (Exception ex)
            {
                // Manejo de errores
                Exception e = ex;
                return null;
            }
        }


        public string GenerateRandomString()
        {
            // Conjunto de caracteres permitidos
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

            // Longitud de la cadena deseada
            int length = 5;

            // Objeto Random para generar caracteres aleatorios
            Random random = new Random();

            // Genera la cadena aleatoria
            string randomString = new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());

            // Retorna la cadena generada como respuesta
            return randomString;
        }


        public async Task<TipoLicenciaModelo> InsertTipoLicencia(InsertTipoLicenciaModelo Ilicencia)
        {
            try
            {
                var LLave = GenerateRandomString();


                var InsertarLicencia = new TipoDeLicencia
                {
                    TipoLicenciaId = null,
                    Tipo = Ilicencia.Tipo,
                    Tiempo = Ilicencia.Tiempo,
                    Costo=Ilicencia.Costo,
                    Llave=LLave

                };

                var Agregar = await _Admin.TipoDeLicencia.AddAsync(InsertarLicencia);
                await _Admin.SaveChangesAsync(); // Guarda los cambios para obtener el ID asignado

                var tipoLicenciaId = Agregar.Entity.TipoLicenciaId; // Obtiene el ID del objeto agregado

                // Realiza una conversión explícita o mapea los datos al modelo correspondiente
                var tipoLicenciaModelo = new TipoLicenciaModelo
                {
                    TipoLicenciaId = tipoLicenciaId,
                    Tipo = Ilicencia.Tipo,
                    Tiempo = Ilicencia.Tiempo,
                    Costo = Ilicencia.Costo,

                };

                return tipoLicenciaModelo;
            }
            catch (Exception ex)
            {
                // Manejo de errores
                Exception e = ex;
                return null;
            }
        }



        public async Task<TipoLicenciaModelo> UpdateTipoLicencia(EditarTipoLicencia ELicencia)
        {


            try
            {





                // 1. Busca el registro que deseas actualizar
                var tipoLicencia = await _Admin.TipoDeLicencia.FindAsync(ELicencia.TipoLicenciaId);

                if (tipoLicencia != null)
                {
                    // 2. Actualiza los campos con los nuevos valores
                    tipoLicencia.Tipo = ELicencia.Tipo;
                    tipoLicencia.Tiempo = ELicencia.Tiempo;
                    tipoLicencia.Costo = ELicencia.Costo;

                    // 3. Guarda los cambios en la base de datos
                    var f =  _Admin.TipoDeLicencia.Update(tipoLicencia);
                   var resu= await _Admin.SaveChangesAsync();

                    // Devuelve el modelo actualizado (opcional)

                }

                var find = await _Admin.TipoDeLicencia.FindAsync(ELicencia.TipoLicenciaId);


                var modelo = new TipoLicenciaModelo
                {
                    TipoLicenciaId = find.TipoLicenciaId,
                    Tipo = find.Tipo,
                    Tiempo = find.Tiempo,
                    Costo = find.Costo
                };


                return modelo;

            }

            catch (Exception ex)
            {
                // Manejo de errores
                Exception e = ex;
                return null;
            }




        }

        public async Task<bool> DeleteTipoLicencia(EliminarTipoLicencia tipoLicenciaId)
        {
            try
            {
                // 1. Busca el registro que deseas eliminar
                var tipoLicencia = await _Admin.TipoDeLicencia.FindAsync(tipoLicenciaId.TipoLicenciaId);

                if (tipoLicencia != null)
                {
                    // 2. Marca el registro como eliminado
                    _Admin.TipoDeLicencia.Remove(tipoLicencia);

                    // 3. Guarda los cambios en la base de datos
                    await _Admin.SaveChangesAsync();

                    return true; // Eliminación exitosa
                }

                // Si no se encuentra el registro, puedes manejar el caso de error apropiadamente
                return false; // No se encontró el registro a eliminar
            }
            catch (Exception ex)
            {
                // Manejo de errores
                Exception e = ex;
                return false; // Error al eliminar
            }
        }




    }
}
