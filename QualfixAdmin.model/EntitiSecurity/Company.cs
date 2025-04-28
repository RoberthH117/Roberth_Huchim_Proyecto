
//using QualfixAdmin.model;
//using QualfixAdmin.model.EntitiSecurity;
//using System.ComponentModel.DataAnnotations;

//namespace QualfixAdmin.Model.EntitiSecurity
//{
//    public class Company
//    {
//        [Key]
//        public int Id_Company { get; set; }

//        [Required]
//        [StringLength(255)] // Cambia la longitud máxima según tus requerimientos
//        public string Nombre { get; set; }

//        [Required]
//        [StringLength(50)] // Cambia la longitud máxima según tus requerimientos
//        public string Licencia { get; set; }

//        // Agregar una colección de usuarios
//        public virtual ICollection<User> Users { get; set; }

//        public virtual ICollection<Perfil> Perfiles { get; set; }
//        public virtual ICollection<Rol> Roles { get; set; }


//    }
//}