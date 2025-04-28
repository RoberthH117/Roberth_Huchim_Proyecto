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

    public class RegimenFiscalsConfiguration : IEntityTypeConfiguration<RegimenFiscal>
    {
        public void Configure(EntityTypeBuilder<RegimenFiscal> entity)
        {
            entity.HasKey(e => e.Id).HasName("PK_RegimenFiscal_1");
            entity.ToTable("RegimenFiscal");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(250)
                .IsUnicode(false);

        }
    }
}
