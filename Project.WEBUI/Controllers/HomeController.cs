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


        // GET: Home
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Customer CustomerUser, StaffProfile StaffUser) // VM deki ismi ile aynı olmalı..
        {
            Customer yakalanan = _crRep.FirstOrDefault(x => x.UserName == CustomerUser.UserName); // Register da appUser'ı unique olarak oluşturuldu..
            StaffProfile yakalananstaff = _spRep.FirstOrDefault(x => x.UserName == StaffUser.UserName);
            
            if (yakalanan == null && yakalananstaff == null)
            {
                ViewBag.Kullanici = "Kullanıcı bulunamadı";
                return View();
            }

            string decrypted = "";
            if (yakalanan != null)
            {
                // kişinin şifresini kontrol edebilmek için veritabanında yakalanan verinin şifresi decrypt edilmelidir..
                decrypted = DantexCrypt.DeCrypt(yakalanan.Password);
            }

            string decryptedStaff = "";
            if (yakalananstaff != null)
            {
                // admin or staff şifresini kontrol edebilmek için veritabanında yakalanan verinin şifresi decrypt edilmelidir..
                decryptedStaff = DantexCrypt.DeCrypt(yakalananstaff.Password);
            }

            if (yakalananstaff != null)
            {
                //Eger bir RedirectToAction metodu ile yönlendirme yapmak istediginiz alan bir Area ise bunun acıkca RouteValues parametresinde anonim tip olarak belirtilmesi gerekir.
                if (StaffUser.Password == decryptedStaff && yakalananstaff.Role == ENTITIES.Enums.UserRole.Admin)
                {
                    Session["admin"] = yakalananstaff;
                    return RedirectToAction("RoomList", "Room", new { Area = "Admin" }); // Area = "Admin": RouteValue  

                    // (RedirectToAction methodun parantezleri arasına Ctrl shift+space) RouteValue, Base Url'inizde action ve controller dışındaki bilgiler
                }

                else if (StaffUser.Password == decryptedStaff && yakalananstaff.Role == ENTITIES.Enums.UserRole.Staff)
                {
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
                            return AktifKontrol(); // Ctrl r+m ile AktifKontrol() method oluşturuldu..
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
            ViewBag.AktifDegil = "Lütfen önce hesabınızı aktif hale getiriniz. Mailinizi kontrol ediniz !";
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