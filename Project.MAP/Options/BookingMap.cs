using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.MAP.Options
{
    public class BookingMap : BaseMap<Booking>
    {
        public BookingMap()
        {
            ToTable("Rezervasyon");

            Property(x => x.UserName).HasColumnName("Kullanici Adi").IsRequired();  
            Property(x => x.Email).HasColumnName("E-mail");  
            Property(x => x.TotalPrice).HasColumnName("Toplam Tutar").HasColumnType("money"); //**
        }
    }
}
