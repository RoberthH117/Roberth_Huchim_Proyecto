using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using QualfixAdmin.dal.DBContext;
using QualfixAdmin.DataModel.Perfil_Model;
using QualfixAdmin.DataModel.UsuarioModel;
using QualfixAdmin.model;
using QualfixAdmin.model.EntitiSecurity;

namespace QualfixAdmin.Services.Perfil_Ser
{
    public class PerfilServicio
    {

        private readonly QualfixAdminSecurityContext _context;
        private readonly UserManager<User> _userManager;
        private readonly QualfixAdminContext _db;

        public PerfilServicio(QualfixAdminSecurityContext context, UserManager<User> userManager, QualfixAdminContext db)
        {
            _context = context;
            _userManager = userManager;
            _db = db;
        }


        public async Task<List<PerfilConAccesosDto>> GetPerfilesConAccesos(User usuario)
        {

           
     var usuarioPerfil = await _context.UsuariosPerfiles
    .Include(up => up.Perfil)
    .FirstOrDefaultAsync(up => up.UserId == usuario.Id);

            IQueryable<Perfil> query = _context.Perfiles
                .Include(p => p.UsuariosPerfiles)
                .ThenInclude(up => up.Usuario)
                .Include(p => p.PerfilAccesos)
                .ThenInclude(pa => pa.Accesos);

            if (usuario.Id_Company.HasValue)
            {
                // Obtener perfiles que tienen el mismo Id_Company y no son el perfil del usuario actual
                query = query.Where(p => p.Id_Company == usuario.Id_Company && p.PerfilId != usuarioPerfil.PerfilId);
            }
            else
            {
                // Obtener todos los perfiles excepto el del usuario actual
                query = query.Where(p => p.PerfilId != usuarioPerfil.PerfilId);
            }

            var perfilesConAccesos = await query.ToListAsync();

            var perfilesDto = perfilesConAccesos.Select(perfil =>
            {
                var accesosNombres = perfil.PerfilAccesos.Select(pa => pa.Accesos.Nombre).ToList();
                return new PerfilConAccesosDto
                {
                    Perfil = perfil.Nombre,
                    Accesos = accesosNombres,
                    PerfilId = perfil.PerfilId,
                    Descripcion = perfil.Descripcion,
                    Id_Company = perfil.Id_Company,
                };
            }).ToList();

            return perfilesDto;
        }



        public async Task<List<GetAllAccesosRequest>> FindAllAccesos(User usuario)
        {
            try
            {
                var query = _context.Accesos
      .Where(a => a.Nombre!= "Modulo Ticket Registro" && a.Nombre!= "Modulo Ticket Revision" && a.Nombre!= "Modulo Ticket Historial" && a.Nombre!= "Modulo Comprar Licencia")
      .Select(a => new GetAllAccesosRequest
      {
          AccesosId = a.AccesosId,
          Nombre = a.Nombre,
          Descripcion = a.Descripcion
      });

                if (usuario.Id_Company != null)
                {
                    // Si Id_Company no es nulo, aplicar condición adicional
                    query = query.Where(a => a.Nombre != "Modulo Clientes" && a.Nombre != "Modulo Productos" && a.Nombre!="Modulo Licencia");
                }

                var accesos = await query.ToListAsync();

                return accesos;
            }
            catch (Exception ex)
            {
                // Manejo de errores
                Exception e = ex;
                return null;
            }
        }


        public async Task<AccesosPorIdResponse> GetAccesosPorId(PerfilIdModel perf)
        {
            // Obtener los accesos asignados al perfil por ID
            var accesosAsignados = await _context.Perfiles
                .Where(p => p.PerfilId == perf.PerfilId)
                .SelectMany(p => p.PerfilAccesos.Select(pa => new GetAllAccesosRequest
                {
                    AccesosId = pa.Accesos.AccesosId,
                    Nombre = pa.Accesos.Nombre,
                    Descripcion = pa.Accesos.Descripcion
                }))
                .ToListAsync();

            // Obtener todos los accesos disponibles
            var todosLosAccesos = await _context.Accesos
                .Select(a => new GetAllAccesosRequest
                {
                    AccesosId = a.AccesosId,
                    Nombre = a.Nombre,
                    Descripcion = a.Descripcion
                })
                .ToListAsync();

            // Filtrar los accesos disponibles que no están asignados
            var accesosDisponibles = todosLosAccesos
                .Where(a => accesosAsignados.All(aa => aa.AccesosId != a.AccesosId))
                .ToList();

            var response = new AccesosPorIdResponse
            {
                AccesosDisponibles = accesosDisponibles,
                AccesosAsignados = accesosAsignados
            };

            return response;
        }

        public async Task<PerfilConAccesosDto> GetIdPerfilAcces(PerfilIdModel Perfil)
        {
            var perfilConAccesos = await _context.Perfiles
        .Where(p => p.PerfilId == Perfil.PerfilId)
        .Include(p => p.UsuariosPerfiles)
        .ThenInclude(up => up.Usuario)
        .Include(p => p.PerfilAccesos)
        .ThenInclude(pa => pa.Accesos)
        .FirstOrDefaultAsync();

            if (perfilConAccesos != null)
            {
                var accesosNombres = perfilConAccesos.PerfilAccesos.Select(pa => pa.Accesos.Nombre).ToList();
                var perfilDto = new PerfilConAccesosDto
                {
                    PerfilId=perfilConAccesos.PerfilId,
                    Perfil = perfilConAccesos.Nombre,
                    Accesos = accesosNombres,
                    Descripcion=perfilConAccesos.Descripcion,
                    Id_Company=perfilConAccesos.PerfilId
                };
                return perfilDto;
            }

            return null; // Si no se encuentra el perfil, puedes devolver null o algún otro valor indicando que no se encontró.


        }



        public async Task<string> CreatePerfil(PerfilConAccesosDto perfilDto, User usuario)
        {
            try
            {
                // Crear un nuevo perfil y asignar los datos proporcionados


                var company = _db.Clientes.Where(r => r.ClienteId == usuario.Id_Company).Select(r => r.NombreComercial).ToList();

                var nuevoPerfil = new Perfil
                {
                    Nombre = perfilDto.Perfil+' ' + company[0],
                    Id_Company = usuario.Id_Company, // Se registra si no es nulo
                    Fecha_Creacion = DateTime.Now,
                    Creado_Por=usuario.Email,
                    Descripcion=perfilDto.Descripcion
                };

                if (usuario.Id_Company == null)
                {
                    nuevoPerfil.Id_Company = null; // Anula el valor si es nulo
                    nuevoPerfil.Nombre = perfilDto.Perfil;
                }

                _context.Perfiles.Add(nuevoPerfil);
                await _context.SaveChangesAsync();
                
                if(usuario.Id_Company == null)
                {

                }
                else
                {
                    perfilDto.Accesos.Add("Modulo Perfil Usuario");
                }


                // Obtener los accesos por nombre y asociarlos al perfil
                foreach (var accesoNombre in perfilDto.Accesos)
                {
                    var acceso = await _context.Accesos.FirstOrDefaultAsync(a => a.Nombre == accesoNombre);

                    if (acceso != null)
                    {
                        var perfilAcceso = new PerfilAccesos
                        {
                            PerfilId = nuevoPerfil.PerfilId,
                            AccesosId = acceso.AccesosId,
                            
                            
                        };

                        await _context.PerfilAccesos.AddAsync(perfilAcceso);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        // Manejar el caso en el que el acceso no se encontró
                    }
                }

              

                return "OK"; // La inserción se realizó con éxito
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción que pueda ocurrir durante la inserción
                return"Error: " + ex.Message;
            }
        }



        public async Task<bool> EliminarPerfilYAccesos(PerfilIdModel Per)
        {
            var perfil = await _context.Perfiles.Include(p => p.PerfilAccesos).FirstOrDefaultAsync(p => p.PerfilId == Per.PerfilId);

            if (perfil != null)
            {
                // Elimina los accesos asociados al perfil.
                var accesosParaEliminar = perfil.PerfilAccesos.ToList();
                foreach (var accesoEliminar in accesosParaEliminar)
                {
                    _context.PerfilAccesos.Remove(accesoEliminar);
                }

                // Luego, elimina el perfil.
                _context.Perfiles.Remove(perfil);

                await _context.SaveChangesAsync();
                return true;
            }

            return false; // Devuelve false si el perfil no se encontró y no se eliminó.
        }


        public async Task<bool> ActualizarPerfilYAccesos(PerfilConAccesosDtoUpdate perfilActualizado)
        {
            // Obtén el perfil de la base de datos
            var perfil = await _context.Perfiles
                .Include(p => p.PerfilAccesos)
                .FirstOrDefaultAsync(p => p.PerfilId == perfilActualizado.PerfilId);

            if (perfil != null)
            {
                perfil.Nombre = perfilActualizado.Perfil;
                perfil.Descripcion = perfilActualizado.Descripcion;

                // Obtén los accesos que ya están asociados al perfil
                var accesosActuales = perfil.PerfilAccesos.Select(pa => pa.AccesosId).ToList();

                // Obtén los accesos que se desean asignar
                var accesosNuevos = perfilActualizado.Accesos.Select(accesoNombre =>
                {
                    var acceso = _context.Accesos.FirstOrDefault(a => a.Nombre == accesoNombre);
                    return acceso?.AccesosId;
                }).ToList();

                // Elimina las relaciones existentes que no están en los nuevos accesos
                var accesosParaEliminar = perfil.PerfilAccesos
                    .Where(pa => !accesosNuevos.Contains(pa.AccesosId))
                    .ToList();

                foreach (var accesoEliminar in accesosParaEliminar)
                {
                    _context.PerfilAccesos.Remove(accesoEliminar);
                }

                // Agrega nuevas relaciones
                foreach (var accesoId in accesosNuevos)
                {
                    if (!accesosActuales.Contains((int)accesoId))
                    {
                        var nuevoPerfilAcceso = new PerfilAccesos
                        {
                            PerfilId = perfil.PerfilId,
                            AccesosId = (int)accesoId
                        };
                        _context.PerfilAccesos.Add(nuevoPerfilAcceso);
                    }
                }

                await _context.SaveChangesAsync();
                return true;
            }

            return false; // Devuelve false si el perfil no se encontró y no se actualizó.
        }


        public async Task<List<string>> GetNombresPerfiles(User user)
        {


            try
            {
                if (user.Id_Company == null)
                {
                    var nombresPerfiles = await _context.Perfiles
                        .Select(p => p.Nombre)
                        .ToListAsync();

                    return nombresPerfiles;
                }

                else
                {
                    var nombresPerfiles = await _context.Perfiles.Where(r=>r.Id_Company==user.Id_Company)
                     .Select(p => p.Nombre)
                     .ToListAsync();

                    return nombresPerfiles;
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores
                Exception e = ex;
                return null;
            }
        }



        public  UsuarioGetResponse AgregarPerfil(AsignarRol user)
        {

            UsuarioGetResponse resp = new UsuarioGetResponse();


            try
            {



                var use = _context.User.FirstOrDefault(r => r.Id == user.Id);

                var nd = _context.Perfiles.FirstOrDefault(r => r.Nombre == user.RolName);

                var de = new UsuarioPerfil()
                {
                    PerfilId = nd.PerfilId,
                    UserId = use.Id
                };

                var result = _context.UsuariosPerfiles.Add(de);
                _context.SaveChanges();















            }
            catch (Exception ex)
            {
                Exception e = ex;
            }

            return resp;



        }

    }
}
