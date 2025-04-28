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
   
    public class ClientesConfiguration : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> entity)
        {
            entity.HasKey(e => e.ClienteId).HasName("PK_Cliente");
            entity.ToTable("Cliente");
            entity.Property(e => e.ClienteId).HasColumnName("ClienteID");
            entity.Property(e => e.CiudadId).HasColumnName("CiudadID");
            entity.Property(e => e.Colonia).HasMaxLength(250);
            entity.Property(e => e.CorreoElectronico)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Cp)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CP");
            entity.Property(e => e.Direccion).IsUnicode(false);
            entity.Property(e => e.EstadoId).HasColumnName("EstadoID");
            entity.Property(e => e.Fax)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.InformacionFiscalId).HasColumnName("InformacionFiscalID");
            entity.Property(e => e.NombreComercial)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.NumeroExterior).HasMaxLength(50);
            entity.Property(e => e.NumeroInterior).HasMaxLength(50);
            entity.Property(e => e.PaisId).HasColumnName("PaisID");
            entity.Property(e => e.Telefonos)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.HasOne(d => d.Ciudad).WithMany(p => p.Clientes)
                .HasForeignKey(d => d.CiudadId)
                .HasConstraintName("FK_Cliente_Ciudad");
            entity.HasOne(d => d.Estado).WithMany(p => p.Clientes)
                .HasForeignKey(d => d.EstadoId)
                .HasConstraintName("FK_Cliente_Estado");
            entity.HasOne(d => d.InformacionFiscal).WithMany(p => p.Clientes)
                .HasForeignKey(d => d.InformacionFiscalId)
                .HasConstraintName("FK_Cliente_InformacionFIscal");
            entity.HasOne(d => d.Pais).WithMany(p => p.Clientes)
                .HasForeignKey(d => d.PaisId)
                .HasConstraintName("FK_Cliente_Pais");

        }
    }
}
