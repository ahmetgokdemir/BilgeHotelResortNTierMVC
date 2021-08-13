using Project.BLL.DesignPatterns.RepositoryPattern.ConcRep;
using Project.COMMON.Tools;
using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.WEBUI.Controllers
{
    public class HomeController : Controller
    {
        CustomerRepository _crRep;
        StaffProfileRepository _spRep;

        public HomeController()
        {
            _crRep = new CustomerRepository();
            _spRep = new StaffProfileRepository();
        }

        public ActionResult Simulation()
        {
            return View();
        }

        // GET: Home
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Customer CustomerUser, StaffProfile StaffUser) // VM deki ismi ile aynı olmalı..
        {
            Customer yakalanan = _crRep.FirstOrDefault(x => x.UserName == CustomerUser.UserName);
            StaffProfile yakalananstaff = _spRep.FirstOrDefault(x => x.UserName == StaffUser.UserName);
            if (yakalanan == null && yakalananstaff == null)
            {
                ViewBag.Kullanici = "Kullanıcı bulunamadı";
                return View();
            }

            string decrypted = "";
            if (yakalanan != null)
            {
                decrypted = DantexCrypt.DeCrypt(yakalanan.Password);
            }

            string decryptedStaff = "";
            if (yakalananstaff != null)
            {
                decryptedStaff = DantexCrypt.DeCrypt(yakalananstaff.Password);
            }

            if (yakalananstaff != null)
            {
                if (StaffUser.Password == decryptedStaff && yakalananstaff.Role == ENTITIES.Enums.UserRole.Admin)
                {
                    //if (!yakalananstaff.Active)  
                    //{
                    //    return AktifKontrol();  // Ctrl r+m ile AktifKontrol() method oluşturuldu..
                    //}

                    Session["admin"] = yakalananstaff;
                    return RedirectToAction("RoomList", "Room", new { Area = "Admin" }); // Area = "Admin": RouteValue  

                }

                else if (StaffUser.Password == decryptedStaff && yakalananstaff.Role == ENTITIES.Enums.UserRole.Staff)
                {
                    //if (!yakalananstaff.Active)  
                    //{
                    //    return AktifKontrol();  // Ctrl r+m ile AktifKontrol() method oluşturuldu..
                    //}

                    Session["staff"] = yakalananstaff;
                    return RedirectToAction("BookingList", "BookingfromStaff", new { Area = "Staff" }); // Area = "Admin": RouteValue  

                }
            }

            if (yakalanan != null)
            {
                if (yakalanan.Role == ENTITIES.Enums.UserRole.Customer && CustomerUser.Password == decrypted)
                    {
                        if (!yakalanan.Active)
                        {
                            return AktifKontrol();
                        }

                        Session["member"] = yakalanan;
                        return RedirectToAction("BookingList", "Booking");
                    }
            }

            ViewBag.Kullanici = "Kullanici bulunamadı";
            return View();
        }

        private ActionResult AktifKontrol()
        {
            ViewBag.AktifDegil = "Lutfen hesabınızı aktif hale getiriniz..Mailinizi kontrol ediniz";
            return View("Login");
        }



        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}