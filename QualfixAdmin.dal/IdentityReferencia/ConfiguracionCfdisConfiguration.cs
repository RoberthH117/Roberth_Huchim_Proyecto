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

    public class ConfiguracionCfdisConfiguration : IEntityTypeConfiguration<ConfiguracionCfdi>
    {
        public void Configure(EntityTypeBuilder<ConfiguracionCfdi> entity)
        {
            entity.HasKey(e => e.Id).HasName("PK_ConfiguracionId");
            entity.ToTable("ConfiguracionCFDI");
            entity.Property(e => e.Cuenta).HasColumnType("text");
            entity.Property(e => e.FileCer)
                .HasColumnType("text")
                .HasColumnName("fileCer");
            entity.Property(e => e.FileKey)
                .HasColumnType("text")
                .HasColumnName("fileKey");
            entity.Property(e => e.FilePasswordKey)
                .HasMaxLength(255)
                .HasColumnName("filePasswordKey");
            entity.Property(e => e.NameFileCer)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("nameFileCer");
            entity.Property(e => e.NameFileKey)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("nameFileKey");
            entity.Property(e => e.PasswordUsuario).HasColumnType("text");
            entity.Property(e => e.Usuario).HasColumnType("text");

        }
    }
}
