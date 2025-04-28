using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QualfixAdmin.model.QualfixAdminData;

namespace QualfixAdmin.dal.IdentityReferencia
{
   
    public class productosConfigurationReference : IEntityTypeConfiguration<Productos>
    {
        public void Configure(EntityTypeBuilder<Productos> entity)
        {

            entity.HasKey(e => e.Id).HasName("PK_ProductoId");
            entity.ToTable("Productos");
            entity.Property(e => e.Id).HasColumnName("Id");
            entity.Property(e => e.Nombre).IsUnicode(false).HasColumnName("Nombre");
            entity.Property(e => e.Descripcion).IsUnicode(false).HasColumnName("Descripcion");
          

        }
    }
}
