using Project.BLL.DesignPatterns.RepositoryPattern.ConcRep;
using Project.COMMON.Tools;
using Project.ENTITIES.Models;
using Project.WEBUI.AuthenticationClasses;
using Project.WEBUI.VMClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.WEBUI.Areas.Admin.Controllers
{
    [AdminAuthentication]
    public class RoomController : Controller
    {
        RoomRepository _rRep;
        RoomDetailRepository _rdRep;
        BookingDetailRepository _bdRep;

        public RoomController()
        {
            _rRep = new RoomRepository();
            _rdRep = new RoomDetailRepository();
            _bdRep = new BookingDetailRepository();
        }

        public ActionResult RoomList()
        {
            _bdRep.GetActiveRezervationtoPassive();

            RoomVM rvm = new RoomVM
            {
                Rooms = _rRep.GetAll()
            };
            return View(rvm);
        }

        public ActionResult OperateRooms()
        {
            RoomVM rvm = new RoomVM
            {
                Rooms = _rRep.GetAll()
            };
            return View(rvm);
        }

        public PartialViewResult MaintenanceRoomAjax(int id)
        {
            RoomVM rvm = new RoomVM
            {
                RoomDetails = _rdRep.GetRoomsForMaintenance(id)
            };

            return PartialView("_MaintenanceRoomPartial", rvm);
        }

        [HttpPost]
        public ActionResult MaintenanceRoom(RoomDetail RoomDetail)
        {
            // RoomDetail RoomDetail'ın diğer propertyleri empty gelmekte o yüzden rd üzerinden çalışıldı..
            RoomDetail rd = _rdRep.Find(RoomDetail.ID);

            rd.isMaintenance = true;
            _rdRep.Update(rd);

            Room r = _rRep.Find(rd.RoomID);
            r.RoomAvailable--;
            _rRep.Update(r);

            return RedirectToAction("OperateRooms");
        }

        // UnMaintenanceRoomAjax
        public PartialViewResult UnMaintenanceRoomAjax(int id)
        {
            RoomVM rvm = new RoomVM
            {
                RoomDetails = _rdRep.GetRoomsForUnMaintenance(id)
            };

            return PartialView("_UnMaintenanceRoomPartial", rvm);
        }

        [HttpPost]
        public ActionResult UnMaintenanceRoom(RoomDetail RoomDetail)
        {

            RoomDetail rd = _rdRep.Find(RoomDetail.ID);

            rd.isMaintenance = false;
            _rdRep.Update(rd);

            Room r = _rRep.Find(rd.RoomID);
            r.RoomAvailable++;
            _rRep.Update(r);

            return RedirectToAction("OperateRooms");
        }

        public ActionResult UpdateRoom(int id)
        {
            RoomVM rvm = new RoomVM
            {
                Room = _rRep.Find(id)
            };
            return View(rvm);
        }

        [HttpPost]
        public ActionResult UpdateRoom(Room Room, HttpPostedFileBase resim, bool? Remember)
        {
            if (Remember == null)
            {
                Remember = false;
            }

            if (Remember.Value) // checkbox ticklendi ise Room.ImagePath'e eski değeri set et..
            {
                Room oldOne = _rRep.Find(Room.ID);
                Room.ImagePath = oldOne.ImagePath;
            }
            else // // checkbox ticklenmedi ise
            {
                if (resim == null) // Resim seçilmediyse..
                {
                    Room.ImagePath = "/Images/default.jpg";
                }
                else               // Resim seçildiyse..
                {
                    Room.ImagePath = ImageUploader.UploadImage("~/Images/", resim);
                }

            } 
 
                // Room Room'ın diğer propertyleri empty gelmekte o yüzden rnew üzerinden çalışıldı..
                Room rnew = _rRep.Find(Room.ID);
                rnew.ImagePath = Room.ImagePath;               
                rnew.Price = Room.Price; 


                _rRep.Update(rnew);
                return RedirectToAction("OperateRooms");
 

        }

        public ActionResult LogOut()
        {
            
            if (Session["admin"] != null)
            {
                Session.Remove("admin"); 
            }

            TempData["out"] = "Çıkıs yaptınız";
            return RedirectToAction("Login", "Home", new { Area = "" });
        }
    }
}