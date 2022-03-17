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

            //Kullanıcı basarılı bir şekilde Register işlemini tamamladıysa ona Mail gönderecegiz... https://localhost:44329/ Kullanıcıya link atmamız lazım.. WEBUI'a sağ click >> Web Panelinden >> Project Url kopyala..

            // bu linke destek veren bir action oluşturulmalıdır Activation(Guid id parametreli).. Url ve linkler her zaman gettir. sadece post ile değil get ile de veri gönderebiliriz.. sadece modifikasyona neden olunmuyor.. Bu method sadece bir action olacak..View'ı olmayacak..
            // https://localhost:44336/
            string gonderilecekEmail = "Tebrikler! Hesabınız olusturulmustur. Hesabınızı aktive etmek icin https://localhost:44336/Register/Activation/" + customerUser.ActivationCode + " linkine tıklayabilirsiniz.";

            MailSender.Send(customerUser.Email, body: gonderilecekEmail, subject: "Hesap aktivasyonu!");

            _crRep.Add(customerUser); //profilden önce bunu eklemelisiniz önceliginiz bunu eklemek olmalı..Cünkü AppUser'in ID'si ilk basta olusmalı...Cünkü bizim kurdugumuz birebir ilişkide AppUser zorunlu olan alan Profile ise opsiyonel alandır. Dolayısıyla ilk basta AppUser'in ID'si SaveChanges ile olusmalı ki Profile'i rahatca ekleyebilelim(eger ekleyeceksek)

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

        // Bununda View'i layoutsuz olacak..
        public ActionResult RegisterOk()
        {
            return View();
        }

    }
}