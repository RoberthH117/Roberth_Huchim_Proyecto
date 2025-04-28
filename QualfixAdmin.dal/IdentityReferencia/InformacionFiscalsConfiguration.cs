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

    public class InformacionFiscalsConfiguration : IEntityTypeConfiguration<InformacionFiscals>
    {
        public void Configure(EntityTypeBuilder<InformacionFiscals> entity)
        {
            entity.HasKey(e => e.InformacionFiscalId).HasName("PK_RegimenFIscal");
            entity.ToTable("InformacionFIscal");
            entity.Property(e => e.InformacionFiscalId).HasColumnName("InformacionFiscalID");
            entity.Property(e => e.CiudadId).HasColumnName("CiudadID");
            entity.Property(e => e.Colonia).HasMaxLength(80);
            entity.Property(e => e.Cp)
                .HasMaxLength(50)
                .HasColumnName("CP");
            entity.Property(e => e.Direccion).HasMaxLength(200);
            entity.Property(e => e.EstadoId).HasColumnName("EstadoID");
            entity.Property(e => e.Localidad).HasMaxLength(80);
            entity.Property(e => e.Municipio).HasMaxLength(50);
            entity.Property(e => e.NumeroExterior).HasMaxLength(15);
            entity.Property(e => e.NumeroInterior).HasMaxLength(15);
            entity.Property(e => e.PaisId).HasColumnName("PaisID");
            entity.Property(e => e.RazonSocial).HasMaxLength(150);
            entity.Property(e => e.RegFiscalId).HasColumnName("RegFiscalID");
            entity.Property(e => e.Rfc)
                .HasMaxLength(90)
                .HasColumnName("RFC");
            entity.HasOne(d => d.Ciudad).WithMany(p => p.InformacionFiscals)
                .HasForeignKey(d => d.CiudadId)
                .HasConstraintName("FK_InformacionFIscal_Ciudad");
            entity.HasOne(d => d.Estado).WithMany(p => p.InformacionFiscals)
                .HasForeignKey(d => d.EstadoId)
                .HasConstraintName("FK_InformacionFIscal_Estado");
            entity.HasOne(d => d.Pais).WithMany(p => p.InformacionFiscals)
                .HasForeignKey(d => d.PaisId)
                .HasConstraintName("FK_InformacionFIscal_Pais");
            entity.HasOne(d => d.RegFiscal).WithMany(p => p.InformacionFiscals)
                .HasForeignKey(d => d.RegFiscalId)
                .HasConstraintName("FK_InformacionFIscal_RegimenFiscal");

        }
    }
}
