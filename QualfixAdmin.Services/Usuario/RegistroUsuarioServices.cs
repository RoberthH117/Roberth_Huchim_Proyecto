using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QualfixAdmin.model;
using QualfixAdmin.dal.DBContext;
using System.Net.Mail;
using System.Net;
using Microsoft.EntityFrameworkCore;
using QualfixAdmin.DataModel.UsuarioModel;
using QualfixAdmin.model.EntitiSecurity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Xml.Serialization;
using System.Data;

namespace QualfixAdmin.Services.Usuario
{

    public class RegistroUsuarioServices
    {
        private readonly QualfixAdminSecurityContext _dbContext;
        private readonly UserManager<User> _manager;
        private readonly RoleManager<Rol> _rol;
       

        public RegistroUsuarioServices(QualfixAdminSecurityContext qualfixAdminContext, UserManager<User> manager, RoleManager<Rol> rol )
        {
            _dbContext = qualfixAdminContext;
            _manager=manager;
            _rol = rol;
          
        }

        public async Task<List<User>> ObtenerTodosUsuarios(User user)
        {
            try
            {
                if (user.Id_Company == null)
                {
                    // Si Id_Company es nulo, devolver la lista completa sin filtrar
                    return _dbContext.Users.ToList();
                }
                else
                {
                    // Si Id_Company tiene un valor, filtrar por ese valor
                    return  _dbContext.Users
                        .Where(u => u.Id_Company == user.Id_Company)
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores
                Exception e = ex;
                return null;
            }
        }


        public async Task<List<object>> GetUsuariosConPerfiles(User user)
        {
            try
            {
                IQueryable<dynamic> baseQuery;

                if (user.Id_Company == null)
                {
                    baseQuery = _dbContext.UsuariosPerfiles
                        .GroupBy(up => new
                        {
                            UserId = up.Usuario.Id,
                            UserName = up.Usuario.UserName,
                            Id = up.Usuario.Id,
                            FirstName = up.Usuario.FirstName,
                            LastName = up.Usuario.LastName,
                            Email = up.Usuario.Email,
                            PasswordHash = up.Usuario.PasswordHash,
                            User = up.Usuario.UserName,
                            Activo = up.Usuario.Activo,
                            ProfileId = up.Perfil.PerfilId,
                            ProfileName = up.Perfil.Nombre,
                        })
                        .Select(group => new
                        {
                            UserId = group.Key.UserId,
                            UserName = group.Key.UserName,
                            ProfileId = group.Key.ProfileId,
                            ProfileName = group.Key.ProfileName,
                            id = group.Key.Id,
                            activo = group.Key.Activo,
                            email = group.Key.Email,
                            firstName = group.Key.FirstName,
                            lastName = group.Key.LastName
                        });
                }
                else
                {
                    baseQuery = _dbContext.UsuariosPerfiles
                        .Where(r => r.Usuario.Id_Company == user.Id_Company)
                        .GroupBy(up => new
                        {
                            UserId = up.Usuario.Id,
                            UserName = up.Usuario.UserName,
                            Id = up.Usuario.Id,
                            FirstName = up.Usuario.FirstName,
                            LastName = up.Usuario.LastName,
                            Email = up.Usuario.Email,
                            PasswordHash = up.Usuario.PasswordHash,
                            User = up.Usuario.UserName,
                            Activo = up.Usuario.Activo,
                            ProfileId = up.Perfil.PerfilId,
                            ProfileName = up.Perfil.Nombre,
                        })
                        .Select(group => new
                        {
                            UserId = group.Key.UserId,
                            UserName = group.Key.UserName,
                            ProfileId = group.Key.ProfileId,
                            ProfileName = group.Key.ProfileName,
                            id = group.Key.Id,
                            activo = group.Key.Activo,
                            email = group.Key.Email,
                            firstName = group.Key.FirstName,
                            lastName = group.Key.LastName
                        });
                }

                var usuarios = await baseQuery.ToListAsync(); // 🔥 Aquí cierro la consulta a BD primero

                var resultado = new List<object>();

                foreach (var usuario in usuarios)
                {
                    var userIdentity = await _manager.FindByIdAsync(usuario.id);
                    var roles = await _manager.GetRolesAsync(userIdentity);

                    resultado.Add(new
                    {
                        usuario.UserId,
                        usuario.UserName,
                        usuario.ProfileId,
                        usuario.ProfileName,
                        usuario.id,
                        usuario.activo,
                        usuario.email,
                        usuario.firstName,
                        usuario.lastName,
                        rolName = roles // aquí roles es un List<string>
                    });
                }

                return resultado;
            }
            catch (Exception ex)
            {
                // Manejar la excepción de alguna manera
                return null;
            }
        }

        public async Task<User> ObtenerUsuarioPorId(string id)
        {

            var consulta = await _manager.Users.Where(r => r.Id == id).ToListAsync();


            var modelo = new User() {
                Id = consulta[0].Id,
                FirstName = consulta[0].FirstName,
                LastName= consulta[0].LastName,
                Email = consulta[0].Email,
                Activo= consulta[0].Activo

            };




            return modelo;
            

        }

        public void AgregarUsuario(User usuario)
        {
            _dbContext.Users.Add(usuario);
            _dbContext.SaveChanges();
        }

        public async Task ActualizarUsuario(User usuario)
        {
            // Recupera el usuario existente de la base de datos
            var usuarioExistente = await _manager.FindByIdAsync(usuario.Id);

            if (usuarioExistente != null)
            {
               
                usuarioExistente.Email = usuario.Email;
                usuarioExistente.FirstName = usuario.FirstName;
                usuarioExistente.LastName = usuario.LastName;
                // Otras propiedades...

                // Si el PasswordHash no es nulo, actualiza la contraseña
                if (usuario.PasswordHash != null)
                {
                    var resetToken = await _manager.GeneratePasswordResetTokenAsync(usuarioExistente);
                    var resulta = await _manager.ResetPasswordAsync(usuarioExistente, resetToken, usuario.PasswordHash);
                }

                // Guarda los cambios en la base de datos
                var result = await _manager.UpdateAsync(usuarioExistente);

                if (result.Succeeded)
                {
                    // La actualización fue exitosa
                }
                else
                {
                    // Manejar errores de actualización
                    // Puedes acceder a los errores con result.Errors
                }
            }
            else
            {
                // Manejar el caso cuando el usuario no existe
            }
        }

        public async Task EliminarUsuario(string id)
        {
            using (var newContext = new QualfixAdminSecurityContext())
            {
                var usuario = _dbContext.Users.FirstOrDefault(u => u.Id == id);

                if (usuario != null)
                {
                

                   await _manager.DeleteAsync(usuario);
                    
                }
            }
        }

        public void EnviarCorreoValidacion(string nombreUsuario)
        {
            try
            {
                string EmailOrigen = "Euan211@gmail.com";
                string Contraseña = "tcyagpevfnwpdohd";
                string EmailDestino = "Euan21132@gmail.com";
                string Asunto = "Validación de Registro";
                string rutaPlantilla = "C:\\Users\\jess\\source\\repos\\Jorge15PP\\QualfixAdmin\\QualfixAdmin.Services\\plantilla_correo_validacion\\index.html";
                string plantilla = File.ReadAllText(rutaPlantilla);
                plantilla = plantilla.Replace("[Nombre de Usuario]", nombreUsuario);

                MailMessage oMailMessage = new MailMessage(EmailOrigen, EmailDestino, Asunto, plantilla);
                oMailMessage.IsBodyHtml = true;

                SmtpClient oStmpClient = new SmtpClient("smtp.gmail.com");
                oStmpClient.EnableSsl = true;
                oStmpClient.UseDefaultCredentials = false;
                oStmpClient.Port = 587;
                oStmpClient.Credentials = new NetworkCredential(EmailOrigen, Contraseña);

                oStmpClient.Send(oMailMessage);

                oStmpClient.Dispose();
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción que pueda ocurrir al enviar el correo electrónico
                Console.WriteLine("Error al enviar el correo electrónico: " + ex.Message);
            }
        }

    }
}