using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.MAP.Options
{
    public class HotelMap : BaseMap<Hotel>
    {
        public HotelMap()
        {
            ToTable("Otel");

            Property(x => x.Name).HasColumnName("Otel Adi").IsRequired();
            Property(x => x.Address).HasColumnName("Adres").IsRequired();
            Property(x => x.PhoneNo).HasColumnName("Telefon Numarası").IsRequired();
        }
    }
}
