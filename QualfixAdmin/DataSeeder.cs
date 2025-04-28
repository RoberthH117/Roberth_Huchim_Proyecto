using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using QualfixAdmin.dal.DBContext;
using QualfixAdmin.DataModel.Perfil_Model;
using QualfixAdmin.model;
using QualfixAdmin.model.EntitiSecurity;

public class DataSeeder
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<Rol> _roleManager;
    private readonly QualfixAdminSecurityContext _context;

    public DataSeeder(
        UserManager<User> userManager,
        RoleManager<Rol> roleManager,
        QualfixAdminSecurityContext context)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _context = context;

      
    }

  

    public async Task InitializeDataAsync()
    {
        // Lógica para inicializar datos (verificar si el usuario existe, crearlo, asignar roles, etc.)
        await CrearRolesYUsuariosInicialesAsync();
        // Puedes agregar más lógica de inicialización si es necesario
    }



    private async Task CrearRolesYUsuariosInicialesAsync()
    {
        // Lógica para crear roles y usuarios iniciales
        string[] roleNames = { "RolSuperAdministrador", "RolAdministrador","RolSoporte","RolAdministradorEmpresa" };

        foreach (var roleName in roleNames)
        {
            if (!await _roleManager.RoleExistsAsync(roleName))
            {
                await _roleManager.CreateAsync(new Rol(roleName,"Este rol tendra acceso a todos los permisos y es para un usuario Qualfix "+ roleName, DateTime.Now,"Qualfix",roleName));
            }
        }


        


        //Logica para crear Perfil

        string[] perfilNames = { "PerfilSuperAdministrador", "PerfilAdministrador","PerfilSoporte","PerfilAdministradorEmpresa" };

        foreach (var perfilName in perfilNames)

        {

            var per = await _context.Perfiles.FirstOrDefaultAsync(p => p.Nombre == perfilName);

            if (per == null)
            {

                var modeloperfil = new Perfil
                {
                    Nombre = perfilName,
                    Creado_Por = "Qualfix",
                    Descripcion="Perfil que tiene los accesos a todos los modulos disponibles destinado para usuario Qualfix"+perfilName,
                    Fecha_Creacion=DateTime.Now
                    
                    
                };

                await _context.Perfiles.AddAsync(modeloperfil);
                await _context.SaveChangesAsync();
            }
        }

   

        

        //var perfiAdmin = await _context.Perfiles.FirstOrDefaultAsync(p => p.Nombre == "Administrador");

        // Lógica para crear Permisos 
        string[] permisosNames = { "Editar","Eliminar","Crear" };

        foreach (var permisoName in permisosNames)
        {
            if (await _context.Permisos.FirstOrDefaultAsync(p => p.Name == permisoName) == null)
            {
                var modelopermiso = new Permisos
                {
                    Name = permisoName,
                    Description = "Con este permiso el usuario podra " + permisoName
                };

                await _context.Permisos.AddAsync(modelopermiso);
                await _context.SaveChangesAsync();

              

            }



            }


        //Para agregar permisos a los roles

       foreach(var permisoName in permisosNames)
        {

            var permiso = await _context.Permisos.FirstOrDefaultAsync(p => p.Name == permisoName);


            foreach (var rolename in roleNames)
            {
                var rol = await _roleManager.FindByNameAsync(rolename);

                if (await _context.RolePermissions.FirstOrDefaultAsync(p => p.RoleID == rol.Id && p.PermissionID==permiso.PermissionID) == null)
                {

                    if (rol.Name != "RolSoporte")
                    {
                        var modeloAsignar = new RolePermissions
                        {
                            PermissionID = permiso.PermissionID,
                            RoleID = rol.Id


                        };

                        await _context.RolePermissions.AddAsync(modeloAsignar);
                        await _context.SaveChangesAsync();
                    }
                }

            }
        }










            // Lógica para crear Accesos
            string[] accesosNames = { "Modulo Rol","Modulo Perfil Usuario", "Modulo Usuario", "Modulo Tipo Licencia", "Modulo Perfil","Modulo Licencia","Modulo Historial Licencia","Modulo Config Cfdi","Modulo Clientes","Modulo Productos","Modulo Usuarios", "Modulo Ticket Registro", "Modulo Ticket Revision", "Modulo Ticket Historial", "Modulo Comprar Licencia" };

        foreach (var accesoName in accesosNames)
        {
            if (await _context.Accesos.FirstOrDefaultAsync(p => p.Nombre == accesoName) == null)
            {
                var modeloacceso = new Accesos
                {
                    Nombre = accesoName,
                    Descripcion = "El usuario podra acceder a " + accesoName,
                    
                    
                };

                await _context.Accesos.AddAsync(modeloacceso);
                await _context.SaveChangesAsync();

              
            }
        }



        foreach(var accesoName in accesosNames) {


            foreach (var perfilName in perfilNames)
            {
                var perfi = await _context.Perfiles.FirstOrDefaultAsync(p => p.Nombre == perfilName);


                var acceso = await _context.Accesos.FirstOrDefaultAsync(p => p.Nombre == accesoName);

                if (await _context.PerfilAccesos.FirstOrDefaultAsync(p => p.PerfilId == perfi.PerfilId && p.AccesosId == acceso.AccesosId) == null)
                {
                    if (perfilName == "PerfilSoporte" && accesoName != "Modulo Historial Licencia" && accesoName!="Modulo Clientes" && accesoName!="Modulo Productos" && accesoName!= "Modulo Perfil Usuario" && accesoName != "Modulo Ticket Registro"  && accesoName != "Modulo Ticket Historial" && accesoName != "Modulo Comprar Licencia" || perfilName == "PerfilSuperAdministrador" && accesoName != "Modulo Historial Licencia" && accesoName != "Modulo Ticket Registro" && accesoName != "Modulo Ticket Revision" && accesoName != "Modulo Ticket Historial" && accesoName != "Modulo Comprar Licencia" || perfilName=="PerfilAdministrador" && accesoName!="Modulo Historial Licencia" && accesoName!="Modulo Clientes" && accesoName!="Modulo Productos" && accesoName != "Modulo Perfil Usuario" && accesoName != "Modulo Ticket Registro" && accesoName != "Modulo Ticket Revision" && accesoName != "Modulo Ticket Historial" && accesoName != "Modulo Comprar Licencia" || perfilName== "PerfilAdministradorEmpresa" && accesoName!="Modulo Licencia" && accesoName!="Modulo Clientes" && accesoName!= "Modulo Productos" && accesoName != "Modulo Ticket Revision") {
                        var modeloPerfilAcceso = new PerfilAccesos
                        {
                            AccesosId = acceso.AccesosId,
                            PerfilId = perfi.PerfilId
                        };

                        await _context.PerfilAccesos.AddAsync(modeloPerfilAcceso);
                        await _context.SaveChangesAsync();
                    }

                }
            }
        }



        // Verificar si el usuario ya existe
        var usuarioExistente = await _userManager.FindByEmailAsync("SuperAdministrador@gmail.com");

        if (usuarioExistente == null)
        {

            var perfi = await _context.Perfiles.FirstOrDefaultAsync(p => p.Nombre == "PerfilSuperAdministrador");
            // Crear el usuario
            var nuevoUsuario = new User
            {
                UserName = "SuperAdministrador",
                Email = "SuperAdministrador@gmail.com",
                EmailConfirmed = true,
                Activo = true,
                FirstName = "SuperAdministrador",
                LastName="SuperAdministrador",
                Status = 1
                // Puedes agregar más propiedades según tu modelo de usuario
            };

            var resultado = await _userManager.CreateAsync(nuevoUsuario, "Hunterghoul#1");

            var user = await _userManager.FindByEmailAsync("SuperAdministrador@gmail.com");

            if (resultado.Succeeded)
            {
                // Asignar roles al usuario
                await _userManager.AddToRoleAsync(nuevoUsuario, "RolSuperAdministrador");

                var modeloUsuarioPerfil = new UsuarioPerfil
                {
                    PerfilId = perfi.PerfilId,
                    UserId = user.Id
                };

                await _context.UsuariosPerfiles.AddAsync(modeloUsuarioPerfil);
                await _context.SaveChangesAsync();
                // Puedes agregar más roles según sea necesario
            }
        }





        // Verificar si el usuario ya existe
        var usuarioExistenteAdministrador = await _userManager.FindByEmailAsync("Administrador@gmail.com");

        if (usuarioExistenteAdministrador == null)
        {

            var perfi = await _context.Perfiles.FirstOrDefaultAsync(p => p.Nombre == "PerfilAdministrador");
            // Crear el usuario
            var nuevoUsuario = new User
            {
                UserName = "Administrador",
                Email = "Administrador@gmail.com",
                EmailConfirmed = true,
                Activo = true,
                FirstName = "Administrador",
                LastName = "Administrador",
                Status = 1
                // Puedes agregar más propiedades según tu modelo de usuario
            };

            var resultado = await _userManager.CreateAsync(nuevoUsuario, "Hunterghoul#1");

            var user = await _userManager.FindByEmailAsync("Administrador@gmail.com");

            if (resultado.Succeeded)
            {
                // Asignar roles al usuario
                await _userManager.AddToRoleAsync(nuevoUsuario, "RolAdministrador");

                var modeloUsuarioPerfil = new UsuarioPerfil
                {
                    PerfilId = perfi.PerfilId,
                    UserId = user.Id
                };

                await _context.UsuariosPerfiles.AddAsync(modeloUsuarioPerfil);
                await _context.SaveChangesAsync();
                // Puedes agregar más roles según sea necesario
            }
        }




        var usuarioSoporte = await _userManager.FindByEmailAsync("Soporte@gmail.com");

        if (usuarioSoporte == null)
        {

            var perfi = await _context.Perfiles.FirstOrDefaultAsync(p => p.Nombre == "PerfilSoporte");
            // Crear el usuario
            var nuevoUsuario = new User
            {
                UserName = "Soporte",
                Email = "Soporte@gmail.com",
                EmailConfirmed = true,
                Activo = true,
                FirstName = "Soporte",
                LastName = "Soporte",
                Status = 1
                // Puedes agregar más propiedades según tu modelo de usuario
            };

            var resultado = await _userManager.CreateAsync(nuevoUsuario, "Hunterghoul#1");

            var user = await _userManager.FindByEmailAsync("Soporte@gmail.com");

            if (resultado.Succeeded)
            {
                // Asignar roles al usuario
                await _userManager.AddToRoleAsync(nuevoUsuario, "RolSoporte");

                var modeloUsuarioPerfil = new UsuarioPerfil
                {
                    PerfilId = perfi.PerfilId,
                    UserId = user.Id
                };

                await _context.UsuariosPerfiles.AddAsync(modeloUsuarioPerfil);
                await _context.SaveChangesAsync();
                // Puedes agregar más roles según sea necesario
            }
        }






















    }
}