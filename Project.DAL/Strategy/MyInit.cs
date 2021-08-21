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

            RoomDetail rd5 = new RoomDetail();
            rd5.RoomNo = "105";
            rd5.Situation = true;
            rd5.isMaintenance = false;
            rd5.RoomID = r1.ID;
            context.RoomDetails.Add(rd5);
            context.SaveChanges();

            ///////////////////////////////////////////////////////////
            Room r2 = new Room();
            r2.RoomType = "Üç Kişilik";
            r2.RoomDescription = "Tek kişilik üç yataklı";
            r2.RoomCount = 10;
            r2.RoomAvailable = 10;
            r2.ImagePath = "/Images/üç.jpg";
            r2.availableBalcony = false;
            r2.availableMinibar = true;
            r2.Price = 200;
            r2.HotelID = htl.ID;

            context.Rooms.Add(r2);
            context.SaveChanges();

            RoomDetail rd6 = new RoomDetail();
            rd6.RoomNo = "106";
            rd6.Situation = true;
            rd6.isMaintenance = false;
            rd6.RoomID = r2.ID;
            context.RoomDetails.Add(rd6);
            context.SaveChanges();

            RoomDetail rd7 = new RoomDetail();
            rd7.RoomNo = "107";
            rd7.Situation = true;
            rd7.isMaintenance = false;
            rd7.RoomID = r2.ID;
            context.RoomDetails.Add(rd7);
            context.SaveChanges();

            RoomDetail rd8 = new RoomDetail();
            rd8.RoomNo = "108";
            rd8.Situation = true;
            rd8.isMaintenance = false;
            rd8.RoomID = r2.ID;
            context.RoomDetails.Add(rd8);
            context.SaveChanges();

            ///////////////////////////////////////////////////////////
            Room r3 = new Room();
            r3.RoomType = "İki Kişilik";
            r3.RoomDescription = "Tek kişilik iki yataklı";
            r3.RoomCount = 10;
            r3.RoomAvailable = 10;
            r3.ImagePath = "/Images/iki.jpg";
            r3.availableBalcony = false;
            r3.availableMinibar = true;
            r3.Price = 150;
            r3.HotelID = htl.ID;

            context.Rooms.Add(r3);
            context.SaveChanges();

            RoomDetail rd9 = new RoomDetail();
            rd9.RoomNo = "201";
            rd9.Situation = true;
            rd9.isMaintenance = false;
            rd9.RoomID = r3.ID;
            context.RoomDetails.Add(rd9);
            context.SaveChanges();

            RoomDetail rd10 = new RoomDetail();
            rd10.RoomNo = "202";
            rd10.Situation = true;
            rd10.isMaintenance = false;
            rd10.RoomID = r3.ID;
            context.RoomDetails.Add(rd10);
            context.SaveChanges();

            RoomDetail rd11 = new RoomDetail();
            rd11.RoomNo = "203";
            rd11.Situation = true;
            rd11.isMaintenance = false;
            rd11.RoomID = r3.ID;
            context.RoomDetails.Add(rd11);
            context.SaveChanges();
            ///////////////////////////////////////////////////////////
            Room r4 = new Room();
            r4.RoomType = "İki Kişilik (d)";
            r4.RoomDescription = "Duble";
            r4.RoomCount = 20;
            r4.RoomAvailable = 20;
            r4.ImagePath = "/Images/tek_2.jpg";
            r4.availableBalcony = true;
            r4.availableMinibar = true;
            r4.Price = 250;
            r4.HotelID = htl.ID;

            context.Rooms.Add(r4);
            context.SaveChanges();

            RoomDetail rd12 = new RoomDetail();
            rd12.RoomNo = "301";
            rd12.Situation = true;
            rd12.isMaintenance = false;
            rd12.RoomID = r4.ID;
            context.RoomDetails.Add(rd12);
            context.SaveChanges();

            RoomDetail rd13 = new RoomDetail();
            rd13.RoomNo = "302";
            rd13.Situation = true;
            rd13.isMaintenance = false;
            rd13.RoomID = r4.ID;
            context.RoomDetails.Add(rd13);
            context.SaveChanges();

            RoomDetail rd14 = new RoomDetail();
            rd14.RoomNo = "303";
            rd14.Situation = true;
            rd14.isMaintenance = false;
            rd14.RoomID = r4.ID;
            context.RoomDetails.Add(rd14);
            context.SaveChanges();
            ///////////////////////////////////////////////////////////
            Room r5 = new Room();
            r5.RoomType = "Üç Kişilik";
            r5.RoomDescription = "Bir tek kişilik bir duble yatak";
            r5.RoomCount = 10;
            r5.RoomAvailable = 10;
            r5.ImagePath = "/Images/iki_2.jpg";
            r5.availableBalcony = true;
            r5.availableMinibar = true;
            r5.Price = 300;
            r5.HotelID = htl.ID;

            context.Rooms.Add(r5);
            context.SaveChanges();

            RoomDetail rd15 = new RoomDetail();
            rd15.RoomNo = "304";
            rd15.Situation = true;
            rd15.isMaintenance = false;
            rd15.RoomID = r5.ID;
            context.RoomDetails.Add(rd15);
            context.SaveChanges();

            RoomDetail rd16 = new RoomDetail();
            rd16.RoomNo = "305";
            rd16.Situation = true;
            rd16.isMaintenance = false;
            rd16.RoomID = r5.ID;
            context.RoomDetails.Add(rd16);
            context.SaveChanges();

            RoomDetail rd17 = new RoomDetail();
            rd17.RoomNo = "306";
            rd17.Situation = true;
            rd17.isMaintenance = false;
            rd17.RoomID = r5.ID;
            context.RoomDetails.Add(rd17);
            context.SaveChanges();
            ///////////////////////////////////////////////////////////
            Room r6 = new Room();
            r6.RoomType = "Dört Kişilik";
            r6.RoomDescription = "Bir duble iki tek kişilik yatak";
            r6.RoomCount = 6;
            r6.RoomAvailable = 6;
            r6.ImagePath = "/Images/iki_3.jpg";
            r6.availableBalcony = true;
            r6.availableMinibar = true;
            r6.Price = 400;
            r6.HotelID = htl.ID;

            context.Rooms.Add(r6);
            context.SaveChanges();

            RoomDetail rd18 = new RoomDetail();
            rd18.RoomNo = "401";
            rd18.Situation = true;
            rd18.isMaintenance = false;
            rd18.RoomID = r6.ID;
            context.RoomDetails.Add(rd18);
            context.SaveChanges();

            RoomDetail rd19 = new RoomDetail();
            rd19.RoomNo = "402";
            rd19.Situation = true;
            rd19.isMaintenance = false;
            rd19.RoomID = r6.ID;
            context.RoomDetails.Add(rd19);
            context.SaveChanges();

            RoomDetail rd20 = new RoomDetail();
            rd20.RoomNo = "403";
            rd20.Situation = true;
            rd20.isMaintenance = false;
            rd20.RoomID = r6.ID;
            context.RoomDetails.Add(rd20);
            context.SaveChanges();

            ///////////////////////////////////////////////////////////
            Room r7 = new Room();
            r7.RoomType = "Kral Dairesi";            
            r7.RoomCount = 1;
            r7.RoomAvailable = 1;
            r7.ImagePath = "/Images/kral.jpg";
            r7.availableBalcony = true;
            r7.availableMinibar = true;
            r7.Price = 700;
            r7.HotelID = htl.ID;

            context.Rooms.Add(r7);
            context.SaveChanges();

            RoomDetail rd21 = new RoomDetail();
            rd21.RoomNo = "501";
            rd21.Situation = true;
            rd21.isMaintenance = false;
            rd21.RoomID = r7.ID;
            context.RoomDetails.Add(rd21);
            context.SaveChanges();

            #endregion
        }
    }
}
