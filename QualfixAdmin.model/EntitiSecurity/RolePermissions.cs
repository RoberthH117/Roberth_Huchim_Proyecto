
using QualfixAdmin.model.EntitiSecurity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security;

public class RolePermissions
{
    [Key]
    public int RolePermissionID { get; set; }

    // Foreign key to Role
    public string RoleID { get; set; }
    public Rol Role { get; set; }

    // Foreign key to Permission
    public int PermissionID { get; set; }
    public Permisos Permission { get; set; }
}