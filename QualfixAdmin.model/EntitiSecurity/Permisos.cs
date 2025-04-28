using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QualfixAdmin.model.EntitiSecurity
{
    public class Permisos
    {
        [Key]
        public int PermissionID { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(255)]
        public string Description { get; set; }
    }
}