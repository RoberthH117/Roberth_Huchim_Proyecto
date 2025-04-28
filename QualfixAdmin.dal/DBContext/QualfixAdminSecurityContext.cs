using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

//using QualfixAdmin.DataModel.Crear_Rol;
using QualfixAdmin.model;
using QualfixAdmin.model.EntitiSecurity;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QualfixAdmin.dal.DBContext
{
    //internal class QualfixAdminSecurityContext
    //{
    //}



    public partial class QualfixAdminSecurityContext : IdentityDbContext<User, Rol, string>
    {


        public QualfixAdminSecurityContext()
        {

        }



        public QualfixAdminSecurityContext(DbContextOptions<QualfixAdminSecurityContext> options)
            : base(options) { }

        public virtual DbSet<Rol> Rol { get; set; }
        public virtual DbSet<User> User { get; set; }
        public DbSet<Permisos> Permisos { get; set; }

        public DbSet<RolePermissions> RolePermissions { get; set; }

        public DbSet<Perfil> Perfiles { get; set; }
        public DbSet<UsuarioPerfil> UsuariosPerfiles { get; set; }
        public DbSet<Accesos> Accesos { get; set; }
        public DbSet<PerfilAccesos> PerfilAccesos { get; set; }

        public DbSet<Licencias> Licencias { get; set; }

        public DbSet<LicenciaUsuario> LicenciaUsuario { get; set; }
        //public DbSet<Company> Company { get; set; }

        //public DbSet<CrearRol> CrearRol { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Server=ep-weathered-recipe-a4w1p2qs-pooler.us-east-1.aws.neon.tech;Database=Qualfix;Username=neondb_owner;Password=npg_zbNni4ceBQv7;Ssl Mode=Require;Trust Server Certificate=true;");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


           


          






            modelBuilder.Entity<UsuarioPerfil>()
          .HasKey(up => new { up.UserId, up.PerfilId });

            modelBuilder.Entity<UsuarioPerfil>()
                .HasOne(up => up.Usuario)
                .WithMany(u => u.UsuarioPerfiles)
                .HasForeignKey(up => up.UserId);

            modelBuilder.Entity<UsuarioPerfil>()
                .HasOne(up => up.Perfil)
                .WithMany(p => p.UsuariosPerfiles)
                .HasForeignKey(up => up.PerfilId);





            modelBuilder.Entity<LicenciaUsuario>()
         .HasKey(up => new { up.UserId, up.LicenciaId });

            modelBuilder.Entity<LicenciaUsuario>()
                .HasOne(up => up.Usuario)
                .WithMany(u => u.LicenciaUsuario)
                .HasForeignKey(up => up.UserId);

            modelBuilder.Entity<LicenciaUsuario>()
                .HasOne(up => up.Licencia)
                .WithMany(p => p.LicenciaUsuario)
                .HasForeignKey(up => up.LicenciaId);







            modelBuilder.Entity<PerfilAccesos>()
       .Property(pa => pa.PerfilId)
       .ValueGeneratedOnAdd();


            modelBuilder.Entity<PerfilAccesos>()
      .HasKey(pa => new { pa.PerfilId, pa.AccesosId });

            modelBuilder.Entity<PerfilAccesos>()
                .HasOne(pa => pa.Perfil)
                .WithMany(p => p.PerfilAccesos)
                .HasForeignKey(pa => pa.PerfilId);

            modelBuilder.Entity<PerfilAccesos>()
                .HasOne(pa => pa.Accesos)
                .WithMany(a => a.PerfilAccesos)
                .HasForeignKey(pa => pa.AccesosId);

        





        }



    }



}
