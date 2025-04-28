using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using QualfixAdmin.dal.DBContext;
using QualfixAdmin.DataModel.ChangePassword;
using QualfixAdmin.DataModel.ForgetUsuario;
using QualfixAdmin.DataModel.UsuarioModel;

using QualfixAdmin.model;
using QualfixAdmin.Services.CreacionRol;
using QualfixAdmin.Services.Perfil_Ser;
using System.Net.Mail;
using System.Text;

namespace QualfixAdmin.Services.Login
{
    public class Login
    {

        private readonly QualfixAdminSecurityContext _context;
        private readonly UserManager<User> _userManager;
        private readonly PerfilServicio _perfilServicio;
        private readonly RolService _rolService;

        //La ultima vez use RoleManager<Rol>

        public Login(QualfixAdminSecurityContext context, UserManager<User> userManager, PerfilServicio perfilServicio, RolService rolService)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _perfilServicio = perfilServicio ?? throw new ArgumentNullException(nameof(perfilServicio));
            _rolService = rolService ?? throw new ArgumentNullException(nameof(rolService));



        }


       public async Task<UsuarioGetResponse> GetUsuario()
        {
            UsuarioGetResponse resp = new UsuarioGetResponse();
            var Data = new List<DataUsuario>();

       








            try
            {


          

                var entity = await this._context.Users.Select(x => new {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName=x.LastName,
                    Activo=x.Activo,
                    UserName=x.UserName,

                    Email=x.Email,
                    EmailConfirmed=x.EmailConfirmed,
                    PasswordHash=x.PasswordHash,
                   

                }).OrderBy(x => x.FirstName).ToListAsync();

              

                foreach (var item in entity)
                {
                    Data.Add(new DataUsuario
                    {
                      Id=item.Id,
                        FirstName = item.FirstName,
                        LastName=item.LastName,
                        Activo = item.Activo,
                        UserName = item.UserName,

                        Email = item.Email,
                        EmailConfirmed = item.EmailConfirmed,
                        PasswordHash = item.PasswordHash
                    });
                }
                resp.DataResponse = Data;



            }catch(Exception ex)
            {
                Exception e = ex;
            }

            return resp;



        }



        public async Task<UsuarioGetResponse> AgregarUsuario(BodyRequest Body)
        {
            UsuarioGetResponse resp = new UsuarioGetResponse();
            DataUsuario nuv = new DataUsuario();


            User user = Body.usuario;


            try
            {
                IdentityResult result = null;

                if (user.Id_Company != null)
                {

                    var usuario = new User
                    {
                        UserName = user.Email.Trim(),
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        PasswordHash = user.PasswordHash.Trim(),
                        Email = user.Email.Trim(),
                        Activo = user.Activo,
                        EmailConfirmed = true,
                        Id_Company = user.Id_Company
                    };

                    result = await _userManager.CreateAsync(usuario, user.PasswordHash.Trim());


                }
                else
                {
                    var usuario = new User
                    {
                        UserName = user.Email.Trim(),
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        PasswordHash = user.PasswordHash.Trim(),
                        Email = user.Email.Trim(),
                        Activo = user.Activo,
                        EmailConfirmed = true,
                       
                    };

                    result = await _userManager.CreateAsync(usuario, user.PasswordHash.Trim());
                }


                    if (result.Succeeded)
                    {


                        var dato = _context.User.FirstOrDefault(r => r.NormalizedEmail == user.Email.Trim().ToUpper());

                        var modelo = new AsignarRol()
                        {
                            Id = dato.Id,
                            RolName = Body.nombrePerfil
                        };



                  
                        var resultadoCreacion = _perfilServicio.AgregarPerfil(modelo);




                        var modeloRol = new AsignarRol()
                        {
                            Id = dato.Id,
                            RolName = Body.nombreRol
                        };

                        var resultadoCreacionRol = await _rolService.AgregarRol(modeloRol);



                    }
                




                



            }
            catch (Exception ex)
            {
                // Loguear o manejar la excepción de alguna manera
            }

            return resp;
        }





        public async Task<UsuarioGetResponse> ObtenerUsuarioPorId(string id)
        {
              UsuarioGetResponse resp = new UsuarioGetResponse();
            var Data = new List<DataUsuario>();

            

            try
            {

                var entity = await this._context.Users.FindAsync(id);


                if (entity != null)
                {
                    Data.Add(new DataUsuario
                    {
                        Id = entity.Id,
                        FirstName = entity.FirstName,
                        LastName = entity.LastName,
                        Activo = entity.Activo,
                        UserName = entity.UserName,

                        Email = entity.Email,
                        EmailConfirmed = entity.EmailConfirmed,
                        PasswordHash = entity.PasswordHash
                    });

                }

                resp.DataResponse = Data;
            }
            catch (Exception ex)
            {
                Exception e = ex;
            }

            return resp;

        }



        public async Task<UsuarioGetResponse> UpdateUser(ChangePassword request, string id)
        {

            UsuarioGetResponse resp = new UsuarioGetResponse();
            var Data = new List<DataUsuario>();

            var Datas = new DataUsuario();

            try
            {
                var entity = await this._context.Users.FindAsync(id);

              


                if (entity != null)
                {
                    Data.Add(new DataUsuario
                    {

                        Id = id,
                        FirstName = entity.FirstName,
                        LastName = entity.LastName,
                        Activo = entity.Activo,
                        UserName = entity.UserName,

                        Email = entity.Email,
                        EmailConfirmed = entity.EmailConfirmed,
                        PasswordHash = entity.PasswordHash

                    });


                    Datas = new DataUsuario()
                    {

                        Id = id,
                        FirstName = entity.FirstName,
                        LastName = entity.LastName,
                        Activo = entity.Activo,
                        UserName = entity.UserName,

                        Email = entity.Email,
                        EmailConfirmed = entity.EmailConfirmed,
                        PasswordHash = entity.PasswordHash,
                        Status = 1,


                    };




                    entity.PasswordHash = request.PasswordHash;

                    Data[0].PasswordHash = request.PasswordHash;



                }

               
                

                resp.DataResponse = Data;

                _context.Users.Update(entity);
                await _context.SaveChangesAsync();
              
            }
            catch (Exception ex)
            {
                Exception e = ex;
            }



            return resp;
        }

        public static string GetRandomPassword(int length)
        {
            const string alphanumericChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string digits = "0123456789";
            const string nonAlphanumericChars = "!@#$%^&*()_-+=<>?";

            StringBuilder sb = new StringBuilder();
            Random rnd = new Random();

            // Agregar al menos un dígito
            int digitIndex = rnd.Next(digits.Length);
            sb.Append(digits[digitIndex]);

            for (int i = 1; i < length; i++)
            {
                // Elige si se usará un carácter alfanumérico o no alfanumérico.
                if (i % 2 == 0)
                {
                    int index = rnd.Next(alphanumericChars.Length);
                    sb.Append(alphanumericChars[index]);
                }
                else
                {
                    int index = rnd.Next(nonAlphanumericChars.Length);
                    sb.Append(nonAlphanumericChars[index]);
                }
            }

            // Mezcla aleatoriamente los caracteres en la cadena de contraseñas.
            for (int i = sb.Length - 1; i > 0; i--)
            {
                int swapIndex = rnd.Next(i + 1);
                char temp = sb[i];
                sb[i] = sb[swapIndex];
                sb[swapIndex] = temp;
            }

            return sb.ToString();
        }

        public async Task<UsuarioGetResponse> ForgetUser(ForgetUsuario request)
        {

            UsuarioGetResponse resp = new UsuarioGetResponse();
            var Data = new List<DataUsuario>();


            try
            {

                

                var entity = await this._context.Users.FindAsync(request.id);



                if (entity != null)
                {
                    Data.Add(new DataUsuario
                    {

                        Id = request.id,
                        FirstName = entity.FirstName,
                        LastName = entity.LastName,
                        Activo = entity.Activo,
                        UserName = entity.UserName,

                        Email = entity.Email,
                        EmailConfirmed = entity.EmailConfirmed,
                        PasswordHash = entity.PasswordHash

                    });





                    string password = GetRandomPassword(10).Trim();

                    var resetToken = await _userManager.GeneratePasswordResetTokenAsync(entity);
                    var result = await _userManager.ResetPasswordAsync(entity, resetToken, password);

                    entity.PasswordHash = password;

                    Data[0].PasswordHash = password;
                    


                }



                IdentityUser user = new()
                {
                    Email = request.correo,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = "roberth@gmail.com"

                };







                string EmailOrigen = "roberthhuchim4@gmail.com";
                string EmailDestino = entity.Email;
                string Contraseña = "wikmopjtvemayskz";

                MailMessage oMailMessage = new MailMessage(EmailOrigen, EmailDestino,"Contraseña QFX", Data[0].PasswordHash);

                oMailMessage.IsBodyHtml = true;

                SmtpClient oStmpClient = new SmtpClient("smtp.gmail.com");
                oStmpClient.EnableSsl = true;
                oStmpClient.UseDefaultCredentials = false;
                oStmpClient.Port = 587;
                oStmpClient.Credentials = new System.Net.NetworkCredential(EmailOrigen, Contraseña);
                oStmpClient.Send(oMailMessage);

                oStmpClient.Dispose();








                resp.DataResponse = Data;




            }
            catch (Exception ex)
            {
                Exception e = ex;
            }



            return resp;
        }




  

        public async Task<UsuarioGetResponse> ForgetUser()
        {
            UsuarioGetResponse resp = new UsuarioGetResponse();
            var Data = new List<DataUsuario>();


            return resp;


        }

    }
}
