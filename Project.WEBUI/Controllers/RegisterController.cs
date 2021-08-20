using Project.BLL.DesignPatterns.RepositoryPattern.ConcRep;
using Project.COMMON.Tools;
using Project.ENTITIES.Models;
using Project.WEBUI.VMClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.WEBUI.Controllers
{
    public class RegisterController : Controller
    {
        CustomerRepository _crRep;
        CustomerProfileRepository _crpRep;
        public RegisterController()
        {
            _crRep = new CustomerRepository();
            _crpRep = new CustomerProfileRepository();
        }

        // GET: Register
        public ActionResult RegisterNow()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RegisterNow(AppUserVM apvm)
        {
            Customer customerUser = apvm.CustomerUser;
            CustomerProfile profile = apvm.Profile;

            customerUser.Password = DantexCrypt.Crypt(customerUser.Password); //sifreyi kriptoladık
            customerUser.RePassword = DantexCrypt.Crypt(customerUser.RePassword); //sifreyi kriptoladık

            if (_crRep.Any(x => x.UserName == customerUser.UserName))
            {
                ViewBag.ZatenVar = "Kullanıcı ismi daha önce alınmış";
                return View();
            }
            else if (_crRep.Any(x => x.Email == customerUser.Email))
            {
                ViewBag.ZatenVar = "Email zaten kayıtlı";
                return View();
            }

            // https://localhost:44336/
            string gonderilecekEmail = "Tebrikler! Hesabınız olusturulmustur. Hesabınızı aktive etmek icin https://localhost:44336/Register/Activation/" + customerUser.ActivationCode + " linkine tıklayabilirsiniz.";

            MailSender.Send(customerUser.Email, body: gonderilecekEmail, subject: "Hesap aktivasyonu!");

            _crRep.Add(customerUser);

            if (!string.IsNullOrEmpty(profile.FirstName.Trim()) && !string.IsNullOrEmpty(profile.LastName.Trim()) && !string.IsNullOrEmpty(profile.PhoneNo.Trim()))
            {
                profile.ID = customerUser.ID;
                _crpRep.Add(profile);
            }

            return View("RegisterOk");

        }

        public ActionResult Activation(Guid id)
        {
            Customer aktifEdilecek = _crRep.FirstOrDefault(x => x.ActivationCode == id);
            if (aktifEdilecek != null)
            {
                aktifEdilecek.Active = true;
                _crRep.Update(aktifEdilecek);
                TempData["HesapAktifMi"] = "Hesabınız aktif hale getirildi";

            }
            else
                TempData["HesapAktifMi"] = "Hesabınız bulunamadı";
            return RedirectToAction("Login", "Home");
        }

        public ActionResult RegisterOk()
        {
            return View();
        }

    }
}