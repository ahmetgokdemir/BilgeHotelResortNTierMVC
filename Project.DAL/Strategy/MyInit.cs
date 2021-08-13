using Project.COMMON.Tools;
using Project.DAL.Context;
using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.DAL.Strategy
{
    public class MyInit : CreateDatabaseIfNotExists<MyContext>
    {
        protected override void Seed(MyContext context)
        {
            #region Hotel

            Hotel htl = new Hotel();
            htl.Name = "Bilge Hotel Resort";
            htl.Address = "Caferağa, Mühürdar Cd. No:76, 34710 Kadıköy/İstanbul";
            htl.PhoneNo = "0216 347 21 21";

            context.Hotels.Add(htl);
            context.SaveChanges();
            #endregion

            #region Admin

            StaffProfile sp = new StaffProfile();

            sp.FirstName = "Çağrı";
            sp.LastName = "Yolyapar";
            sp.BirthDate = new DateTime(1988, 12, 31);
            sp.PhoneNo = "0555 555 55 55";
            sp.UserName = "cgr";
            sp.Password = DantexCrypt.Crypt("123");
            sp.JobTitle = "Manager";
            sp.Salary = 10000;
            sp.HireDate = new DateTime(2010, 12, 31);
            sp.Role = ENTITIES.Enums.UserRole.Admin;
            sp.HotelID = htl.ID;

            context.StaffProfiles.Add(sp);
            context.SaveChanges();

            #endregion

            #region Rooms

            Room r1 = new Room();
            r1.RoomType = "Tek kişilik";
            r1.RoomCount = 20;
            r1.RoomAvailable = 20;
            r1.ImagePath = "/Images/tek.jpg";
            r1.availableBalcony = false;
            r1.availableMinibar = false;
            r1.Price = 100;
            r1.HotelID = htl.ID;

            context.Rooms.Add(r1);
            context.SaveChanges();

            RoomDetail rd1 = new RoomDetail();
            rd1.RoomNo = "101";
            rd1.Situation = true;
            rd1.isMaintenance = false;
            rd1.RoomID = r1.ID;
            context.RoomDetails.Add(rd1);
            context.SaveChanges();

            RoomDetail rd2 = new RoomDetail();
            rd2.RoomNo = "102";
            rd2.Situation = true;
            rd2.isMaintenance = false;
            rd2.RoomID = r1.ID;
            context.RoomDetails.Add(rd2);
            context.SaveChanges();

            RoomDetail rd3 = new RoomDetail();
            rd3.RoomNo = "103";
            rd3.Situation = true;
            rd3.isMaintenance = false;
            rd3.RoomID = r1.ID;
            context.RoomDetails.Add(rd3);
            context.SaveChanges();

            RoomDetail rd4 = new RoomDetail();
            rd4.RoomNo = "104";
            rd4.Situation = true;
            rd4.isMaintenance = false;
            rd4.RoomID = r1.ID;
            context.RoomDetails.Add(rd4);
            context.SaveChanges();
            ///////////////////////////////////////////////////////////
            Room r2 = new Room();
            r2.RoomType = "Üç Kişilik";
            r2.RoomDescription = "Tek kişilik üç yataklı";
            r2.RoomCount = 10;
            r2.RoomAvailable = 10;
            r2.ImagePath = "/Images/tek.jpg";
            r2.availableBalcony = false;
            r2.availableMinibar = true;
            r2.Price = 200;
            r2.HotelID = htl.ID;

            context.Rooms.Add(r2);
            context.SaveChanges();
            ///////////////////////////////////////////////////////////
            Room r3 = new Room();
            r3.RoomType = "İki Kişilik";
            r3.RoomDescription = "Tek kişilik iki yataklı";
            r3.RoomCount = 10;
            r3.RoomAvailable = 10;
            r3.ImagePath = "/Images/tek.jpg";
            r3.availableBalcony = false;
            r3.availableMinibar = true;
            r3.Price = 150;
            r3.HotelID = htl.ID;

            context.Rooms.Add(r3);
            context.SaveChanges();
            ///////////////////////////////////////////////////////////
            Room r4 = new Room();
            r4.RoomType = "İki Kişilik (d)";
            r4.RoomDescription = "Duble";
            r4.RoomCount = 20;
            r4.RoomAvailable = 20;
            r4.ImagePath = "/Images/tek.jpg";
            r4.availableBalcony = true;
            r4.availableMinibar = true;
            r4.Price = 250;
            r4.HotelID = htl.ID;

            context.Rooms.Add(r4);
            context.SaveChanges();
            ///////////////////////////////////////////////////////////
            Room r5 = new Room();
            r5.RoomType = "Üç Kişilik";
            r5.RoomDescription = "Bir tek kişilik bir duble yatak";
            r5.RoomCount = 10;
            r5.RoomAvailable = 10;
            r5.ImagePath = "/Images/tek.jpg";
            r5.availableBalcony = true;
            r5.availableMinibar = true;
            r5.Price = 300;
            r5.HotelID = htl.ID;

            context.Rooms.Add(r5);
            context.SaveChanges();
            ///////////////////////////////////////////////////////////
            Room r6 = new Room();
            r6.RoomType = "Dört Kişilik";
            r6.RoomDescription = "Bir duble iki tek kişilik yatak";
            r6.RoomCount = 6;
            r6.RoomAvailable = 6;
            r6.ImagePath = "/Images/tek.jpg";
            r6.availableBalcony = true;
            r6.availableMinibar = true;
            r6.Price = 400;
            r6.HotelID = htl.ID;

            context.Rooms.Add(r6);
            context.SaveChanges();
            ///////////////////////////////////////////////////////////
            Room r7 = new Room();
            r7.RoomType = "Kral Dairesi";            
            r7.RoomCount = 1;
            r7.RoomAvailable = 1;
            r7.ImagePath = "/Images/tek.jpg";
            r7.availableBalcony = true;
            r7.availableMinibar = true;
            r7.Price = 700;
            r7.HotelID = htl.ID;

            context.Rooms.Add(r7);
            context.SaveChanges();

            RoomDetail rd5 = new RoomDetail();
            rd5.RoomNo = "500";
            rd5.Situation = true;
            rd5.isMaintenance = false;
            rd5.RoomID = r7.ID;
            context.RoomDetails.Add(rd5);
            context.SaveChanges();

            #endregion
        }
    }
}
