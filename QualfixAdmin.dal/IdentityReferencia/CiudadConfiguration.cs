using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using QualfixAdmin.model.QualfixAdminData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QualfixAdmin.dal.IdentityReferencia
{
   
    public class CiudadConfiguration : IEntityTypeConfiguration<Ciudad>
    {
        public void Configure(EntityTypeBuilder<Ciudad> entity)
        {
            entity.HasKey(e => e.CiudadId).HasName("PK_Ciudad");
            entity.ToTable("Ciudad");
            entity.Property(e => e.CiudadId).HasColumnName("CiudadID");
            entity.Property(e => e.EstadoId).HasColumnName("EstadoID");
            entity.Property(e => e.Nombre)
                .HasMaxLength(90)
                .IsUnicode(false);

        }
    }
}
