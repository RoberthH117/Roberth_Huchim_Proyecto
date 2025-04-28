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

    public class EstadoConfiguration : IEntityTypeConfiguration<Estado>
    {
        public void Configure(EntityTypeBuilder<Estado> entity)
        {
            entity.HasKey(e => e.EstadoId).HasName("PK_EstadoId");
            entity.ToTable("Estado");
            entity.Property(e => e.EstadoId).HasColumnName("EstadoID");
            entity.Property(e => e.Nombre)
                .HasMaxLength(90)
                .IsUnicode(false);
            entity.Property(e => e.PaisId).HasColumnName("PaisID");

        }
    }
}
