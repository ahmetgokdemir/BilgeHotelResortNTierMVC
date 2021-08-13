using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.MAP.Options
{
    public class BookingDetailMap : BaseMap<BookingDetail>
    {
        public BookingDetailMap()
        {
            ToTable("Rezervasyon Detayi");

            Ignore(x => x.ID);
            HasKey(x => new { x.BookingID, x.RoomID, x.RoomNo });

            Property(x => x.CheckIn).HasColumnName("Giriş Tarihi").HasColumnType("datetime2").IsRequired(); //**
            Property(x => x.CheckOut).HasColumnName("Cikis Tarihi").HasColumnType("datetime2").IsRequired(); //**
            Property(x => x.SubPrice).HasColumnName("Ara Fiyat").HasColumnType("money"); //**
            Property(x => x.ReservationActive).HasColumnName("Rezervasyon Durumu").IsRequired();
            Property(x => x.BookingType).HasColumnName("Paket Turu").IsRequired();
            Property(x => x.RoomNo).HasColumnName("Oda No").IsRequired();

        }
    }
}
