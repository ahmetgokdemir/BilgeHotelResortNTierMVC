using Project.BLL.DesignPatterns.RepositoryPattern.BaseRep;
using Project.BLL.DesignPatterns.SingletonPattern;
using Project.DAL.Context;
using Project.DTO.Models;
using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.BLL.DesignPatterns.RepositoryPattern.ConcRep
{
    public class BookingDetailRepository : BaseRepository<BookingDetail>
    {
        MyContext _db;
        public BookingDetailRepository()
        {
            _db = DBTool.DBInstance;
        }


        // Aktif Rezervasyonların boşalma günü geldiyse odanın Situation'ını true yapma RoomAvailable sayısını artırma..
        public void GetActiveRezervationtoPassive()
        {
            List<BookingDetail> bds = _db.Set<BookingDetail>().Where(x => x.ReservationActive == true).ToList();

            foreach (BookingDetail bd in bds)
            {
                if (bd.CheckOut <= DateTime.Now)
                {
                    bd.ReservationActive = false;

                    RoomDetail rd = _db.Set<RoomDetail>().Where(x => x.RoomNo == bd.RoomNo).FirstOrDefault(); // BookingDetail'e RoomNo attr. vermenin faydası => x.RoomNo == bd.RoomNo kullanılabilir..
                    rd.Situation = true;

                    Room r = _db.Set<Room>().Where(x => x.ID == bd.RoomID).FirstOrDefault();
                    r.RoomAvailable++;

                    _db.SaveChanges();
                }

            }

        }

        // Aktif Rezervasyonlar..
        public List<BookingDetail> GetActiveRezervationfromStaff()
        {
            return _db.Set<BookingDetail>().Where(x => x.ReservationActive == true)
                //.Join(_db.Set<Booking>(),
                //(bd => bd.BookingID),
                //(bk => bk.ID),
                //(bd, bk) => new ActiveRezervationDTO
                //{
                //    RoomNo = bd.RoomNo,
                //    CheckIn = bd.CheckIn,
                //    CheckOut = bd.CheckOut,
                //    BookingType = bd.BookingType.ToString(),
                //    CustomerID = bk.CustomerID
                //}).Join(_db.Set<CustomerProfile>(),
                //(ar => ar.CustomerID),
                //(csp => csp.ID),
                //(ar, csp) => new ActiveRezervationSecondDTO
                //{
                //    RoomNo = ar.RoomNo,
                //    CheckIn = ar.CheckIn,
                //    CheckOut = ar.CheckOut,
                //    BookingType = ar.BookingType,
                //    CustomerID = ar.CustomerID,
                //    FirstName = csp.FirstName,
                //    LastName = csp.LastName
                //})
                .ToList();


        }


        // Günlük boşalan odalar..
        public List<BookingDetail> GetEmptiedRoomsfromStaff()
        {
            return _db.Set<BookingDetail>().Where(x => x.CheckOut == DateTime.Today).ToList();

        }

    }
}
