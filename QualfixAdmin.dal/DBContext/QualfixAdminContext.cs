

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using QualfixAdmin.model.QualfixAdminData;
using System.Reflection;
using QualfixAdmin.model;

namespace QualfixAdmin.dal.DBContext
{
    public partial class QualfixAdminContext : DbContext
    {
        public QualfixAdminContext() { }

        public QualfixAdminContext(DbContextOptions<QualfixAdminContext> options)
            : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public virtual DbSet<Ciudad> Ciudads { get; set; }
        public virtual DbSet<Cliente> Clientes { get; set; }

 
        public virtual DbSet<TipoDeLicencia> TipoDeLicencia { get; set; }
        public virtual DbSet<Productos> Productos { get; set; } = null!;
        public virtual DbSet<ConfiguracionCfdi> ConfiguracionCfdis { get; set; }

        public virtual DbSet<Reporte> Reporte { get; set; }

        public virtual DbSet<Imagen> Imagen { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=ep-weathered-recipe-a4w1p2qs-pooler.us-east-1.aws.neon.tech;Port=5432;Database=Qualfix;Username=neondb_owner;Password=npg_zbNni4ceBQv7;Ssl Mode=Require;Trust Server Certificate=true;");

        public virtual DbSet<Estado> Estados { get; set; }

        public virtual DbSet<InformacionFiscals> InformacionFiscal { get; set; }

        public virtual DbSet<Pai> Pais { get; set; }

      

        public virtual DbSet<RegimenFiscal> RegimenFiscals { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<Imagen>()
             .HasOne(i => i.Reporte)
             .WithMany(r => r.Imagenes)
             .HasForeignKey(i => i.ReporteId);



            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            //builder.Entity<UsuarioRegistro>(entity =>
            // {
            //     //ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            //     builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            // });





        }
    }

}