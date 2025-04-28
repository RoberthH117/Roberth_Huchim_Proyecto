using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using QualfixAdmin.dal.DBContext;
//using QualfixAdmin.DataModel.Crear_Rol;
using QualfixAdmin.DataModel.UsuarioModel;
using QualfixAdmin.model;
using QualfixAdmin.model.EntitiSecurity;
using QualfixAdmin.model.QualfixAdminData;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using System.Threading.Tasks;

namespace QualfixAdmin.Services.CreacionRol
{
    public class RolService
    {




        private readonly QualfixAdminSecurityContext _context;
        private readonly QualfixAdminContext _bd;
        private UserManager<User> _userManager;
        private readonly RoleManager<Rol> _roleManager;

        //La ultima vez use RoleManager<Rol>

        public RolService(QualfixAdminSecurityContext context, UserManager<User> userManager, RoleManager<Rol> roleManager, QualfixAdminContext bd)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _bd = bd;
        }

        public async Task<object> CrearRol(CrearRolRequest rol, User usuario)
        {
            try
            {
                // Comprueba si un rol con el mismo nombre ya existe y fue creado por el mismo usuario
                var existingRol = await _roleManager.Roles.FirstOrDefaultAsync(r => r.NombreRol == rol.RolName && r.CompanyId == usuario.Id_Company);


                var company = _bd.Clientes.Where(r => r.ClienteId == usuario.Id_Company).Select(r => r.NombreComercial).ToList();

                if (existingRol != null)
                {
                    // Un rol con el mismo nombre ya existe y fue creado por el mismo usuario
                    return null; // O puedes devolver un mensaje de error apropiado
                }

           

                // Crea el rol
                Rol nuevoRol;

                if (usuario.Id_Company.HasValue)
                {
                    

                    nuevoRol = new Rol(
                        name: rol.RolName + " " + company[0],
                        descripcion: rol.Descripcion,
                        fecha_creacion: DateTime.Now,
                        creado_por: usuario.Email,
                        nombreRol: rol.RolName,
                        companyId: usuario.Id_Company
                    );

                    nuevoRol.NormalizedName = rol.RolName.ToUpper(); // Asigna el valor que desees
                }
                else
                {
                    // Usuario no tiene un Id_Company válido, crea el rol sin el nombre compuesto
                    nuevoRol = new Rol(
                        name: rol.RolName,
                        descripcion: rol.Descripcion,
                        fecha_creacion: DateTime.Now,
                        creado_por: usuario.Email,
                        nombreRol: rol.RolName
                    );

                    nuevoRol.NormalizedName = rol.RolName.ToUpper(); // Asigna el valor que desees
                }
                var resultadoCreacionRol = await _roleManager.CreateAsync(nuevoRol);

                if (resultadoCreacionRol.Succeeded)
                {
                    // El rol se creó con éxito, ahora asigna los permisos si se proporcionan
                    if (!string.IsNullOrEmpty(rol.Permisos))
                    {
                       var asignacionPermisos = new AsignarPermisosARolRequest();

                        if (usuario.Id_Company.HasValue)
                        {

                           asignacionPermisos = new AsignarPermisosARolRequest
                            {
                                RolName = rol.RolName + " " + company[0],
                                Permisos = rol.Permisos
                            };

                        }

                        else
                        {
                            asignacionPermisos = new AsignarPermisosARolRequest
                            {
                                RolName = rol.RolName,
                                Permisos = rol.Permisos
                            };
                        }
                        // Llama al método AsignarPermisosARol
                        var resultadoAsignacionPermisos = await AsignarPermisosARol(asignacionPermisos);

                        if (resultadoAsignacionPermisos != "Permisos asignados al rol con éxito")
                        {
                            // Ocurrió un error al asignar permisos, maneja el error según sea necesario
                            // Puedes decidir si debes eliminar el rol creado anteriormente
                            return null;
                        }
                    }

                    // Devuelve el rol creado
                    var rolCreado = await _roleManager.FindByIdAsync(nuevoRol.Id);

                    var rolCreadoConPermisos = await GetRoleIdWithPermissionsAsync(new DeleteRolRequest { Id = rolCreado.Id });
                    return rolCreadoConPermisos;
                }
                else
                {
                    // Ocurrió un error al crear el rol, maneja el error según sea necesario
                    return null;
                }
            }
            catch (Exception ex)
            {
                Exception e = ex;
                return null;
            }
        }



        public async Task<Rol> UpdateRol(UpdateRolRequest rol)
        {

      


            try
            {
                var role = await _roleManager.FindByIdAsync(rol.Id);

            
                    // Modificar las propiedades del rol
                    role.Name = rol.RolName; // Cambia el nombre según tus necesidades

                    role.Descripcion = rol.Descripcion;
                
                role.Fecha_Modificacion = DateTime.Now;
                
                // Modifica la descripción si es necesario


                //var res = await _roleManager.CreateAsync(usuarios);
                var result = await _roleManager.UpdateAsync(role);



                var rolCreado = await _roleManager.FindByNameAsync(role.Name);



                return rolCreado;
            }
            catch (Exception ex)
            {
                Exception e = ex;
                return null;
            }

           


        }


        public async Task<object> UpdateRolPermisos(UpdateRolRequest rol)
        {
            var role = await _roleManager.FindByIdAsync(rol.Id);

            var id_c= await _bd.Clientes.FindAsync(role.CompanyId);

            if (role == null)
            {
                return null; // Manejo de error si el rol no se encuentra
            }


            if (id_c != null)
            {
                // Modificar las propiedades del rol
                role.Name = rol.RolName + " " + id_c.NombreComercial;
                role.NombreRol = rol.RolName;
                role.Descripcion = rol.Descripcion;
                role.Fecha_Modificacion = DateTime.Now;
                role.NormalizedName = role.Name.ToUpper();

            }

            else
            {
                role.Name = rol.RolName;
                role.NombreRol = rol.RolName;
                role.Descripcion = rol.Descripcion;
                role.Fecha_Modificacion = DateTime.Now;
                role.NormalizedName = role.Name.ToUpper();
            }

            // Recuperar todas las relaciones existentes en RolePermissions para este rol
            var existingRolePermissions = _context.RolePermissions
                .Where(rp => rp.RoleID == role.Id)
                .ToList();

            // Obtener los nombres de permisos que se desean asignar
            var nuevosPermisos = rol.Permisos.Split(",").Select(p => p.Trim()).ToList();

            // Eliminar las relaciones existentes que no están en los nuevos permisos
            foreach (var existingRolePermission in existingRolePermissions)
            {
                var permission = await _context.Permisos.FindAsync(existingRolePermission.PermissionID);
                if (permission != null && !nuevosPermisos.Contains(permission.Name))
                {
                    _context.RolePermissions.Remove(existingRolePermission);
                }
            }

            // Agregar nuevas relaciones
            foreach (var permiso in nuevosPermisos)
            {
                var permission = await _context.Permisos.FirstOrDefaultAsync(p => p.Name == permiso);
                if (permission != null && !existingRolePermissions.Any(rp => rp.PermissionID == permission.PermissionID))
                {
                    var nuevoRolePermission = new RolePermissions
                    {
                        RoleID = role.Id,
                        PermissionID = permission.PermissionID
                    };
                    _context.RolePermissions.Add(nuevoRolePermission);
                }
            }

            await _context.SaveChangesAsync();

            // Recuperar el rol actualizado
            var rolCreadoConPermisos = await GetRoleIdWithPermissionsAsync(new DeleteRolRequest { Id = role.Id });


            return rolCreadoConPermisos;
        }
        public async Task<Rol> FindRolId(DeleteRolRequest rol)
        {




            try
            {
                var role = await _roleManager.FindByIdAsync(rol.Id);


              



                return role;
            }
            catch (Exception ex)
            {
                Exception e = ex;
                return null;
            }




        }



public async Task<PermisosRolResponse> GetPermisosDeRolAsync(DeleteRolRequest rol)
    {
        var response = new PermisosRolResponse();

        // Obtener el rol por ID
        var role = await _roleManager.FindByIdAsync(rol.Id);

        if (role == null)
        {
            return null; // O manejar el caso en el que no se encuentre el rol
        }

        // Obtener todos los permisos disponibles
        var permisosDisponibles = await _context.Permisos.ToListAsync();

        // Obtener los permisos asignados al rol
        var permisosAsignados = await _context.RolePermissions
            .Where(rp => rp.RoleID == rol.Id)
            .Select(rp => rp.Permission)
            .ToListAsync();

        response.PermisosAsignados = permisosAsignados;
        response.PermisosDisponibles = permisosDisponibles.Except(permisosAsignados).ToList();
        return response;
    }




        public async Task<List<GetAllRolRequest>> FindAllRolId()
    {
        try
        {
                var roles = await _roleManager.Roles
                    .Select(r => new GetAllRolRequest
                    {
                        Id = r.Id,
                        Name = r.Name,
                        Descripcion = r.Descripcion
                    })
                    .ToListAsync();

                return roles;
            }
            catch (Exception ex)
            {
                // Manejo de errores
                Exception e = ex;
                return null;
            }
        }



    


        public async Task<List<GetAllPermissionsRequest>> FindAllPermissions()
        {
            try
            {
                var permisos = await _context.Permisos
                    .Select(r => new GetAllPermissionsRequest
                    {
                        PermissionID = r.PermissionID,
                        Name = r.Name,
                        Description = r.Description
                    })
                    .ToListAsync();

                return permisos;
            }
            catch (Exception ex)
            {
                // Manejo de errores
                Exception e = ex;
                return null;
            }
        }


        public async Task<List<object>> GetRolesWithPermissionsAsync(User usuario, Rol roles)
        {
            IQueryable<object> query;

            if (usuario.Id_Company != null)
            {
                query = _context.RolePermissions
                    .Where(rp => rp.Role.CompanyId == usuario.Id_Company && rp.Role.NombreRol != roles.NombreRol)
                    .GroupBy(rp => new
                    {
                        RolName = rp.Role.NombreRol,
                        Descripcion = rp.Role.Descripcion,
                        RolId = rp.Role.Id
                    })
                    .Select(group => new
                    {
                        Rol = group.Key.RolName,
                        Descripcion = group.Key.Descripcion,
                        RolId = group.Key.RolId,
                        Permisos = string.Join(", ", group.Select(rp => rp.Permission.Name))
                    });
            }
            else
            {
                query = _context.RolePermissions
                     .Where(rp =>  rp.Role.NombreRol != roles.NombreRol)
                    .GroupBy(rp => new
                    {
                        RolName = rp.Role.NombreRol,
                        Descripcion = rp.Role.Descripcion,
                        RolId = rp.Role.Id
                    })
                    .Select(group => new
                    {
                        Rol = group.Key.RolName,
                        Descripcion = group.Key.Descripcion,
                        RolId = group.Key.RolId,
                        Permisos = string.Join(", ", group.Select(rp => rp.Permission.Name))
                    });
            }
            
            var rolesWithPermissions = query.ToList();

            return rolesWithPermissions;
        }


        public async Task<object> GetRoleIdWithPermissionsAsync(DeleteRolRequest rol)
        {
            var roleWithPermissions = await _context.RolePermissions
                .Where(rp => rp.RoleID == rol.Id)
                .GroupBy(rp => new
                {
                    RolName = rp.Role.NombreRol,
                    Descripcion = rp.Role.Descripcion, // Agrega la descripción del rol
                    RolId = rp.Role.Id
                })
                .Select(group => new
                {
                    Rol = group.Key.RolName,
                    Descripcion = group.Key.Descripcion, // Incluye la descripción
                    RolId = group.Key.RolId,
                    Permisos = string.Join(", ", group.Select(rp => rp.Permission.Name))
                })
                .FirstOrDefaultAsync(); // Usamos FirstOrDefault para obtener un solo resultado

            if (roleWithPermissions != null)
            {
                return (object)roleWithPermissions;
            }
            else
            {
                return null; // O puedes lanzar una excepción si el rol no se encuentra
            }
        }






        public async Task<PermisosUsu> GetPermisos(Rol rol, User user, Perfil perf)
        {
            try
            {
                // Obtén los permisos asociados a este rol
                var rolePermissions = _context.RolePermissions
                    .Where(rp => rp.RoleID == rol.Id)
                    .Select(rp => rp.Permission.Name)
                    .ToList();

                var perfilAccesos = _context.PerfilAccesos
                    .Where(rp => rp.PerfilId == perf.PerfilId)
                    .Select(rp => rp.Accesos.Nombre)
                    .ToList();

                // Define variables booleanas para cada permiso
                bool Editar = rolePermissions.Contains("Editar");
                bool Eliminar = rolePermissions.Contains("Eliminar");
                bool Crear = rolePermissions.Contains("Crear");

                bool ModuloRol = perfilAccesos.Contains("Modulo Rol");
                bool ModuloUsuario = perfilAccesos.Contains("Modulo Usuario");
                bool ModuloTipoLicencia = perfilAccesos.Contains("Modulo Tipo Licencia");
                bool ModuloPerfil = perfilAccesos.Contains("Modulo Perfil");
                bool ModuloLicencia = perfilAccesos.Contains("Modulo Licencia");
                bool ModuloHistorialLicenia = perfilAccesos.Contains("Modulo Historial Licencia");
                bool ModuloConfigCfdi = perfilAccesos.Contains("Modulo Config Cfdi");
                bool ModuloClientes = perfilAccesos.Contains("Modulo Clientes");
                bool ModuloProductos = perfilAccesos.Contains("Modulo Productos");
                bool ModuloUsuarios = perfilAccesos.Contains("Modulo Usuarios");
                bool ModuloPerfilUsuario = perfilAccesos.Contains("Modulo Perfil Usuario");
                bool ModuloTicketRegistro = perfilAccesos.Contains("Modulo Ticket Registro");
                bool ModuloRevisionTicket = perfilAccesos.Contains("Modulo Ticket Revision");
                bool ModuloTicketHistorial = perfilAccesos.Contains("Modulo Ticket Historial");
                bool ModuloComprarLicencia = perfilAccesos.Contains("Modulo Comprar Licencia");



                // Agrega más variables booleanas para otros permisos si es necesario

                // Puedes devolver un objeto con los valores booleanos
                var permisos = new PermisosUsu
                {
                    Crear = Crear,
                    Editar=Editar,
                    Eliminar=Eliminar,
                    ModuloRol=ModuloRol,
                    ModuloPerfil=ModuloPerfil,
                    ModuloTipoLicencia=ModuloTipoLicencia,
                    ModuloUsuario=ModuloUsuario,
                    ModuloLicencia=ModuloLicencia,
                    ModuloHistorialLicencia=ModuloHistorialLicenia,
                    ModuloConfigCfdi=ModuloConfigCfdi,
                    ModuloClientes=ModuloClientes,
                    ModuloProductos=ModuloProductos,
                    ModuloUsuarios=ModuloUsuarios,
                    ModuloPerfilUsuario=ModuloPerfilUsuario,
                    ModuloTicketRegistro=ModuloTicketRegistro,
                    ModuloRevisionTicket=ModuloRevisionTicket,
                    ModuloTicketHistorial=ModuloTicketHistorial,
                    ModuloComprarLicencia=ModuloComprarLicencia
                    

                    // Agrega más permisos aquí
                };

                return permisos;
            }
            catch (Exception ex)
            {
                Exception e = ex;
                return null;
            }
        }
        public async Task<String> DeleteRol(DeleteRolRequest rol)
        {




            try
            {
                var role = await _roleManager.FindByIdAsync(rol.Id);
                var del = await _roleManager.DeleteAsync(role);





                return "Rol eliminado exitosamente.";
            }
            catch (Exception ex)
            {
                Exception e = ex;
                return "Error al eliminar rol";
            }




        }



        public async Task<string> DeleteRolWithPermissions(DeleteRolRequest rol)
        {
            try
            {
                var role = await _roleManager.FindByIdAsync(rol.Id);

                if (role != null)
                {
                    // Eliminar las relaciones en la tabla RolePermissions para este rol
                    var rolePermissions = _context.RolePermissions.Where(rp => rp.RoleID == role.Id).ToList();
                    _context.RolePermissions.RemoveRange(rolePermissions);

                    // Eliminar el rol
                    var result = await _roleManager.DeleteAsync(role);

                    if (result.Succeeded)
                    {
                        return "Rol eliminado exitosamente.";
                    }
                    else
                    {
                        // Aquí puedes manejar los errores específicos del resultado
                        return "Error al eliminar rol.";
                    }
                }
                else
                {
                    return "Rol no encontrado.";
                }
            }
            catch (Exception ex)
            {
                // Manejar las excepciones generales
                Exception e = ex;
                return "Error al eliminar rol.";
            }
        }

        public async Task<String> AsignarPermisosARol(AsignarPermisosARolRequest asignacion)
        {
            try
            {
                var rol = await _context.Roles.FirstOrDefaultAsync(r => r.Name == asignacion.RolName);

                if (rol == null)
                {
                    return "Rol no encontrado";
                }

                var permisos = asignacion.Permisos.Split(',').Select(p => p.Trim());

                foreach (var permiso in permisos)
                {
                    var permisoEnBaseDeDatos = await _context.Permisos.FirstOrDefaultAsync(p => p.Name == permiso);

                    if (permisoEnBaseDeDatos != null)
                    {
                        var rolePermission = new RolePermissions
                        {
                            RoleID = rol.Id,
                            PermissionID = permisoEnBaseDeDatos.PermissionID
                        };

                        _context.RolePermissions.Add(rolePermission);
                    }
                    else
                    {
                        return "Alguno de los permisos no existe en la base de datos";
                    }
                }

                await _context.SaveChangesAsync();

                return "Permisos asignados al rol con éxito";
            }
            catch (Exception ex)
            {
                // Maneja errores y excepciones según sea necesario
                return  ex.Message;
            }
        }



        public async Task<List<string>> GetNombresRoles(User user)
        {
            try
            {
                if (user.Id_Company == null)
                {
                    var nombresRoles = await _context.Roles
                        .Select(r => r.Name)
                        .ToListAsync();

                    return nombresRoles;
                }

                else
                {
                    var nombresRoles = await _context.Roles.Where(r=>r.CompanyId==user.Id_Company)
                        .Select(r => r.Name)
                        .ToListAsync();

                    return nombresRoles;
                }



            }
            catch (Exception ex)
            {
                // Manejo de errores
                Exception e = ex;
                return null;
            }
        }

        public  async Task<UsuarioGetResponse> AgregarRol(AsignarRol user)
        {
            UsuarioGetResponse resp = new UsuarioGetResponse();

            try
            {
                // Validar el usuario
                var appUser =  _context.User.FirstOrDefault(r=>r.Id == user.Id);

                if (appUser != null)
                {
                    // Agregar el usuario al rol
                    var result = await  _userManager.AddToRoleAsync(appUser, user.RolName);


                }
            }
            catch (Exception ex)
            {
                // Loguear o manejar la excepción de alguna manera
            }

            return resp;
        }





        public async Task<List<string>> ObtenerNombresComerciales(User user)
        {
            // Utilizar LINQ para realizar la consulta y obtener la lista de NombreComercial

            try
            {

                if (user.Id_Company == null)
                {

                    var nombresComerciales = await _bd.Clientes
                        .Where(cliente => cliente.NombreComercial != null)
                        .Select(cliente => cliente.NombreComercial)
                        .ToListAsync();
                    nombresComerciales.Add("Qualfix");
                    return nombresComerciales;
                }

                else
                {

                    var nombresComerciales = await _bd.Clientes
                        .Where(cliente => cliente.ClienteId==user.Id_Company)
                        .Select(cliente => cliente.NombreComercial)
                        .ToListAsync();

                 
                    return nombresComerciales;
                }
            }
            catch (Exception e)
            {

                return null;
            }

            }




    }

}
