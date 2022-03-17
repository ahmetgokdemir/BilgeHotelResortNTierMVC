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

            // Ignore, HasKey: Çoka çok ilişkilerde kullanılır...
            Ignore(x => x.ID);
            HasKey(x => new { x.BookingID, x.RoomID, x.RoomNo }); // x.RoomID: 1-7; x.RoomNo: 101-501
            /* 
             * x.RoomNo (RoomDetail'den geliyor..) verilmesinin sebebi ilk iki haskey (x.BookingID, x.RoomID) verinin unique olmasını sağlamıyor.. duplicate key yani benzer primary key hatası vermektedir.. Unique olmasını sağlamak için bir de RoomNo verildi.. x.ID her veri için 0 değeri alıyor o yüzden x.ID unique'liyi sağlamaz.. 
             SqlException: Violation of PRIMARY KEY constraint 'PK_dbo.Rezervasyon Detayi'. Cannot insert duplicate key in object 'dbo.Rezervasyon Detayi'. The duplicate
             key value is (1, 1).
             The statement has been terminated.
             */

            Property(x => x.CheckIn).HasColumnName("Giriş Tarihi").HasColumnType("datetime2").IsRequired(); //**
            Property(x => x.CheckOut).HasColumnName("Cikis Tarihi").HasColumnType("datetime2").IsRequired(); //**
            Property(x => x.SubPrice).HasColumnName("Ara Fiyat").HasColumnType("money"); //**
            Property(x => x.ReservationActive).HasColumnName("Rezervasyon Durumu").IsRequired();
            Property(x => x.BookingType).HasColumnName("Paket Turu").IsRequired();
            Property(x => x.RoomNo).HasColumnName("Oda No").IsRequired();

        }
    }
}
