using Project.BLL.DesignPatterns.RepositoryPattern.ConcRep;
using Project.COMMON.Tools;
using Project.ENTITIES.Models;
using Project.WEBUI.VMClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.WEBUI.Areas.Admin.Controllers
{
    //[AdminAuthentication]
    public class StaffController : Controller
    {
        StaffProfileRepository _spRep;

        public StaffController()
        {
            _spRep = new StaffProfileRepository();
        }


        public ActionResult StaffList()
        {
            AppUserVM auvm = new AppUserVM
            {
                StaffUsers = _spRep.GetActives()
            };
            return View(auvm);
        }

        public PartialViewResult AddStaffAjax()
        {           
            return PartialView("_AddStaffPartial");
        }

        [HttpPost]
        public ActionResult AddStaff(StaffProfile StaffUser)
        {
            if (_spRep.Any(x => x.UserName == StaffUser.UserName))
            {
                TempData["ZatenVar"] = "Kullanıcı ismi daha önce alınmış";
                return RedirectToAction("StaffList");
            }

            StaffUser.Password = DantexCrypt.Crypt(StaffUser.Password);
            StaffUser.Role = ENTITIES.Enums.UserRole.Staff;
            StaffUser.HotelID = 1;

            _spRep.Add(StaffUser);

            return RedirectToAction("StaffList");
        }

        public PartialViewResult UpdateStaffAjax(int id)
        {
            AppUserVM auvm = new AppUserVM
            {
                StaffUser = _spRep.Find(id)

            };
             
            return PartialView("_UpdatetaffPartial", auvm);
        }

        [HttpPost]
        public ActionResult UpdateStaff(StaffProfile StaffUser)
        {
            if (_spRep.Any(x => x.UserName == StaffUser.UserName))
            {
                TempData["ZatenVar"] = "Kullanıcı ismi daha önce alınmış";
                return RedirectToAction("StaffList");
            }

            StaffProfile sp = _spRep.Find(StaffUser.ID);
                        
            sp.Password = DantexCrypt.Crypt(StaffUser.Password);
            sp.Salary = StaffUser.Salary;
            sp.PhoneNo = StaffUser.PhoneNo;

            _spRep.Update(sp);

            return RedirectToAction("StaffList");
        }

        public ActionResult DeleteStaff(int id)
        {

            StaffProfile silinecek = _spRep.Find(id);
            _spRep.Delete(silinecek);
           

            TempData["mesaj"] = "Veri pasife cekildi";

            return RedirectToAction("StaffList");
        }


        // GET: Admin/Staff
        public ActionResult Index()
        {
            return View();
        }
    }
}