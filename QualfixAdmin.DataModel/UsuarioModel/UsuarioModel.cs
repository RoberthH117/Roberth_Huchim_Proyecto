using QualfixAdmin.model;
using QualfixAdmin.model.EntitiSecurity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace QualfixAdmin.DataModel.UsuarioModel
{
    public class UsuarioModel
    {
    }


    public class UsuarioGetResponse
    {

        public List<DataUsuario> DataResponse { get; set; }


    }

    public class UsuarioRequests
    {

        public string Email { get; set; }
        //public string NombreUsuario { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PasswordHash { get; set; }
        public string RolName { get; set; }



    }

    public class PermisosUsu
    {
        public bool Editar { get; set; }
        public bool Eliminar { get; set; }

        public bool Crear { get;set; }
        public bool ModuloRol { get; set; }
        public bool ModuloUsuario { get; set; }

        public bool ModuloPerfil { get; set; }
        public bool ModuloTipoLicencia { get; set; }
        public bool ModuloLicencia { get; set; }

        public bool ModuloHistorialLicencia { get; set; }

        public bool ModuloConfigCfdi { get; set; }

        public bool ModuloClientes { get; set; }

        public bool ModuloProductos { get; set; }

        public bool ModuloUsuarios { get; set; }

        public bool ModuloPerfilUsuario { get; set; }

        public bool ModuloTicketRegistro { get; set; }

        public bool ModuloRevisionTicket { get; set; }

        public bool ModuloTicketHistorial { get; set; }

        public bool ModuloComprarLicencia { get; set; }





    }

    public class BodyRequest
    {
        public User usuario { get; set; }
        public string? nombrePerfil { get; set; }
        public string? nombreRol { get; set; }

        public string? nombreCompany { get; set; }

        
    }

    public class AsignarRol
    {
        public string Id { get; set; }  
        public string RolName { get;set; }
    }

    public class CrearRolRequest
    {
        public string RolName { get; set; }
        public string Descripcion { get; set; }

        public string Permisos { get; set; }
      
       
    }


    public class UpdateRolRequest
    {
        public string Id { get;set; }
        public string RolName { get; set; }
        public string Descripcion { get; set; }

        public string Permisos { get; set; }

        
    }

    public class GetAllRolRequest
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Descripcion { get; set; }


    }

    public class PermisosRolResponse
    {
        public List<Permisos> PermisosAsignados { get; set; }
        public List<Permisos> PermisosDisponibles { get; set; }
    }


    public class GetAllPermissionsRequest
    {
        public int PermissionID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }


    }


  
    public class DeleteRolRequest
    {
        public string Id { get; set; }

    }

    public class DataUsuario
    {
        public string Id { get; set; }  

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public bool Activo { get; set; }

        public string UserName { get; set; } = null!;

        public int? Status { get; set; }

        public string Email { get; set; } = null!;

        public bool EmailConfirmed { get; set; }

  

        

        public string PasswordHash { get; set; } = null!;

    }

    public class Usuari
    {


        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;



        public string UserName { get; set; } = null!;



        public string Email { get; set; } = null!;

 




        public string PasswordHash { get; set; } = null!;

    }

}




