using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.MAP.Options
{
    public abstract class SpecMap<T> : BaseMap<T> where T : UserSpec
    {
        public SpecMap()
        {
            Property(x => x.FirstName).HasColumnName("Adi").IsRequired();
            Property(x => x.LastName).HasColumnName("Soyadi").IsRequired();
            Property(x => x.BirthDate).HasColumnName("Dogum Tarihi").HasColumnType("datetime2"); //**
            Property(x => x.Address).HasColumnName("Adres");
            Property(x => x.PhoneNo).HasColumnName("Telefon Numarasi").IsRequired();

        }
    }
}
