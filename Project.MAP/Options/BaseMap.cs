using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.MAP.Options
{
    // EntityTypeConfiguration*** 
    public abstract class BaseMap<T> : EntityTypeConfiguration<T> where T : BaseEntity
    {
        public BaseMap()
        {
            Property(x => x.CreatedDate).HasColumnName("Veri Olusturma Tarihi").HasColumnType("datetime2");
            Property(x => x.ModifiedDate).HasColumnName("Veri Guncelleme Tarihi").HasColumnType("datetime2");
            Property(x => x.DeletedDate).HasColumnName("Veri Silme Tarihi").HasColumnType("datetime2");
            Property(x => x.Status).HasColumnName("Veri Durumu").IsOptional();
        }
    }
}
