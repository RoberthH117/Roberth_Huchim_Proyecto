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

    public class PaisConfiguration : IEntityTypeConfiguration<Pai>
    {
        public void Configure(EntityTypeBuilder<Pai> entity)
        {

            entity.HasKey(e => e.PaisId).HasName("PK_Pais");
            entity.ToTable("Pais");
            entity.Property(e => e.PaisId).HasColumnName("PaisId");
            entity.Property(e => e.Nombre).IsUnicode(false).HasColumnName("Nombre");
            entity.Property(e => e.TipoMoneda).IsUnicode(false).HasColumnName("TipoMoneda");
            entity.Property(e => e.SimboloMoneda).IsUnicode(false).HasColumnName("SimboloMoneda");

        }
    }
}
