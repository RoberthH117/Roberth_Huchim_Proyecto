using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using QualfixAdmin.dal.DBContext;
using QualfixAdmin.DataModel.LicenciaUI;
using QualfixAdmin.model;
using QualfixAdmin.model.EntitiSecurity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace QualfixAdmin.Services.Security
{
    public class Encripts
    {

        private readonly QualfixAdminSecurityContext _context;
        private readonly QualfixAdminContext _db;
        private UserManager<User> _userManager;
        private RoleManager<Rol> _roleManager;

        //La ultima vez use RoleManager<Rol>

        public Encripts(QualfixAdminSecurityContext context, UserManager<User> userManager, RoleManager<Rol> roleManager, QualfixAdminContext db)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _db = db;
        }



        public async Task<IEnumerable<UserLicenseInfo>> GetAllUserLicenses()
        {
            var users = await _userManager.Users
                .Include(u => u.LicenciaUsuario)
                .ThenInclude(lu => lu.Licencia)
                .ToListAsync();

            var allUserLicenses = new List<UserLicenseInfo>();

            foreach (var user in users)
            {
                var userLicenses = user.LicenciaUsuario.Select(lu => new UserLicenseInfo
                {
                    UserId = user.Id,
                    LicenseId = lu.Licencia.LicenciaId,
                    LicenseName = lu.Licencia.Licencia,
                    StartDate = lu.Licencia.Fecha_Inicio,
                    ExpirationDate = lu.Licencia.Fecha_Expiracion
                }).ToList();

                allUserLicenses.AddRange(userLicenses);
            }
            return allUserLicenses;
        }




        public async Task<IEnumerable<LicenciaConTiempo>> ObtenerLicenciasPorUsuarioId(string userId)
        {
            var licencias = await _context.Licencias
                .Where(l => l.UserId == userId)
                .ToListAsync();

            var tipoLicencias = await _db.TipoDeLicencia
                .ToListAsync();

            var licenciasConTiempo = licencias
     .Join(
         tipoLicencias,
         licencia => licencia.Licencia.Substring(24),  // Cambiado a 24 para obtener las últimas 5 letras
         tipoLicencia => tipoLicencia.Llave.ToString(),
         (licencia, tipoLicencia) => new LicenciaConTiempo
         {
             LicenciaId = licencia.LicenciaId,
             Licencia = licencia.Licencia,
             Fecha_Inicio = licencia.Fecha_Inicio,
             Fecha_Expiracion = licencia.Fecha_Expiracion,
             Creado_Por = licencia.Creado_Por,
             UserId = licencia.UserId,
             LicenciaUsuario = licencia.LicenciaUsuario,
             Tiempo = tipoLicencia.Tipo
         })
     .ToList();

            return licenciasConTiempo;
        }

        public async Task<IEnumerable<User>> GetUsuariosRolAdministradorEmpresa()
        {
            var roleExists = await _roleManager.RoleExistsAsync("RolAdministradorEmpresa");

            if (!roleExists)
            {
                // Manejar el caso en el que el rol no existe
                return Enumerable.Empty<User>();
            }

            var usersInRole = await _userManager.GetUsersInRoleAsync("RolAdministradorEmpresa");

            return usersInRole;
        }


        public string EncryptString(string inputString, string key)
        {
            // Encriptar la clave usando SHA-256
            string hashedKey = ComputeSha256Hash(key);

            // Combinar la clave encriptada con el string
            string combinedString = hashedKey + inputString;

            // Dividir la cadena cada 5 caracteres por un "-"
            string formattedString = FormatString(combinedString, 5, '-');

            // Retorna la cadena resultante
            return formattedString;
        }

        // Método para encriptar una cadena usando SHA-256
        private string ComputeSha256Hash(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = sha256.ComputeHash(bytes);

                StringBuilder builder = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    builder.Append(b.ToString("x2"));
                }

                return builder.ToString().Substring(0, 20); // Tomar los primeros 20 caracteres
            }
        }

        // Método para formatear una cadena dividiéndola cada ciertos caracteres
        private string FormatString(string input, int chunkSize, char separator)
        {
            StringBuilder formattedString = new StringBuilder();
            for (int i = 0; i < input.Length; i += chunkSize)
            {
                int endIndex = Math.Min(i + chunkSize, input.Length);
                formattedString.Append(input.Substring(i, endIndex - i));

                if (endIndex < input.Length)
                {
                    formattedString.Append(separator);
                }
            }

            return formattedString.ToString();
        }



        public DateTime AddDays(int daysToAdd, DateTime baseDate)
        {
            // Calcula la nueva fecha sumando los días proporcionados
            DateTime resultDate = baseDate.AddDays(daysToAdd);

            // Retorna la nueva fecha
            return resultDate;
        }




        public string GenerateRandomString()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random random = new Random();

            // Generar una cadena de 5 caracteres seleccionando aleatoriamente de 'chars'
            string randomString = new string(Enumerable.Repeat(chars, 5)
                .Select(s => s[random.Next(s.Length)]).ToArray());

            return randomString;
        }

        public async Task<usuarioLicencia> usuarioLicencia(string id)
        {

            var consulta=  _context.LicenciaUsuario.FirstOrDefault(r=>r.UserId== id);


            var nuevo = new usuarioLicencia
            {
                UserId = consulta.UserId,
                LicenciaId=consulta.LicenciaId
            };

            return nuevo;

           


        }


        public async Task<int> EstadoLicencia(String id)
        {

            var consulta = _context.LicenciaUsuario.FirstOrDefault(r => r.UserId==id);

            if (consulta != null)
            {
                var consultaLicencia = _context.Licencias.FirstOrDefault(r => r.LicenciaId == consulta.LicenciaId);


                // var restante = consultaLicencia.Fecha_Inicio - consultaLicencia.Fecha_Expiracion;

                TimeSpan restante = consultaLicencia.Fecha_Expiracion.Subtract(consultaLicencia.Fecha_Inicio);
                int diasRestantes = restante.Days;




                return diasRestantes;
            }

            return 0;


        }

        public async Task<bool> EstadoLicenciaTrue(User usuario)
        {

            var consulta = _context.LicenciaUsuario.FirstOrDefault(r => r.UserId == usuario.Id);

            if (consulta != null)
            {
                var consultaLicencia = _context.Licencias.FirstOrDefault(r => r.LicenciaId == consulta.LicenciaId);


                // var restante = consultaLicencia.Fecha_Inicio - consultaLicencia.Fecha_Expiracion;

                TimeSpan restante = consultaLicencia.Fecha_Expiracion.Subtract(consultaLicencia.Fecha_Inicio);
                int diasRestantes = restante.Days;

                bool estado;


                if (diasRestantes > 0)
                {
                    estado = true;


                }

                else
                {
                    estado = false;
                }





                return estado;

            }

            return false;

           


        }




        public async Task<object> GetUserLicenseInfo(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return "No se encontró el usuario con el ID {userId}";
            }

            // Obtén la LicenciaUsuario del primer contexto
            var userLicense = _context.LicenciaUsuario
                .Include(lu => lu.Licencia)
                .FirstOrDefault(lu => lu.UserId == userId);

            if (userLicense == null)
            {
                return "No se encontró una licencia para el usuario con el ID {userId}";
            }

            // Ahora, realiza una consulta en el segundo contexto para obtener TipoDeLicencia
            var tipoLicencia = _db.TipoDeLicencia
               .FirstOrDefault(tl => tl.Llave == userLicense.Licencia.Licencia.Substring(24));

            if (tipoLicencia == null)
            {
                return "No se encontró información de TipoLicencia para la licencia {userLicense.Licencia.Licencia}";
            }

            // Puedes devolver estos datos en el formato que necesites
            return (new
            {
                UserId = user.Id,
                UserName = user.UserName,
                LicenseId = userLicense.Licencia.LicenciaId,
                LicenseName = userLicense.Licencia.Licencia,
                Last5Characters = userLicense.Licencia.Licencia.Substring(24),
                MonthOfLicense = tipoLicencia.Tipo
            });
        }




    }



}



