using Microsoft.AspNetCore.Identity;
using QualfixAdmin.model.QualfixAdminData;

using System.ComponentModel.DataAnnotations.Schema;

public class Rol : IdentityRole
{
    public Rol(string name, string descripcion, DateTime fecha_creacion, string creado_por, string nombreRol, int? companyId) : base(name)
    {
        Descripcion = descripcion;
        Fecha_Creacion = fecha_creacion;
        Creado_Por = creado_por;
        NombreRol= nombreRol;
        CompanyId= companyId;
     
    }

    public Rol(string name, string descripcion, DateTime fecha_creacion, string creado_por, string nombreRol) : base(name)
    {
        Descripcion = descripcion;
        Fecha_Creacion = fecha_creacion;
        Creado_Por = creado_por;
        NombreRol = nombreRol;
    }


        public Rol()
    {
        // Constructor sin parámetros
    }

    public string Descripcion { get; set; }
    public DateTime Fecha_Creacion { get; set; }
    public DateTime? Fecha_Modificacion { get; set; }
    public string Creado_Por { get; set; }

    public string? NombreRol { get;set; }

    // Agregar una propiedad para la clave foránea
    public int? CompanyId { get; set; }

    // Agregar una referencia a la empresa
    
}