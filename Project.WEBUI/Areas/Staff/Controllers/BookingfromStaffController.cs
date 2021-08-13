﻿using Project.BLL.DesignPatterns.RepositoryPattern.ConcRep;
using Project.ENTITIES.Models;
using Project.WEBUI.Models.BookingTools;
using Project.WEBUI.VMClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static Project.ENTITIES.Models.BookingDetail;

namespace Project.WEBUI.Areas.Staff.Controllers
{
    public class BookingfromStaffController : Controller
    {
        BookingRepository _bRep;
        RoomRepository _rRep;
        RoomDetailRepository _rdRep;
        BookingDetailRepository _bdRep;
        CustomerProfileRepository _cpRep;
        CustomerRepository _cRep;

        public BookingfromStaffController()
        {
            _bRep = new BookingRepository();
            _rRep = new RoomRepository();
            _rdRep = new RoomDetailRepository();
            _bdRep = new BookingDetailRepository();
            _cpRep = new CustomerProfileRepository();
            _cRep = new CustomerRepository();
        }

        public ActionResult BookingList()
        {
            _bdRep.GetActiveRezervation();


            RoomVM rvm = new RoomVM
            {
                Rooms = _rRep.GetAvailableRooms()
                // Rooms = _rRep.GetAll()
            };

            return View(rvm);
        }

        public ActionResult RezervationRooms()
        {
            BookingDetailVM bdvm = new BookingDetailVM
            {
                ActiveRezervationDTOs = _bdRep.GetActiveRezervationfromStaff()
            };

            return View(bdvm);
        }

        public ActionResult EmptiedandMaintenanceRooms()
        {
            RoomVM rvm = new RoomVM
            {
                RoomDetails = _rdRep.GetMaintenanceRoomsfromStaff()
            };

            return View(rvm);
        }

        public ActionResult AddToCart(int id)
        {
            Cart c = Session["bcart"] == null ? new Cart() : Session["bcart"] as Cart;
            Room eklenecekOda = _rRep.Find(id);

            CartItem ci = new CartItem
            {
                ID = eklenecekOda.ID,
                RoomTypeName = eklenecekOda.RoomType,
                Price = eklenecekOda.Price,
                ImagePath = eklenecekOda.ImagePath
            };
            c.SepeteEkle(ci);
            Session["bcart"] = c;

            eklenecekOda.RoomAvailable--;
            _rRep.Save();

            return RedirectToAction("BookingList");
        }

        public ActionResult CartPage()
        {
            if (Session["bcart"] != null)
            {
                CartPageVM cpvm = new CartPageVM();
                Cart c = Session["bcart"] as Cart;
                cpvm.Cart = c;
                return View(cpvm);
            }
            TempData["sepetBos"] = "Sepetinizde ürün bulunmamaktadır";
            return RedirectToAction("BookingList");
        }

        public ActionResult DeleteFromCart(int id)
        {
            if (Session["bcart"] != null)
            {
                Cart c = Session["bcart"] as Cart;
                c.SepettenSil(id);

                Room eklenecekOda = _rRep.Find(id);
                eklenecekOda.RoomAvailable++;
                _rRep.Save();

                if (c.Sepetim.Count == 0)
                {
                    Session.Remove("bcart");
                    TempData["sepetBos"] = "Sepetinizde tamamen bosalmıstır";
                    return RedirectToAction("BookingList");
                }
                return RedirectToAction("CartPage");
            }

            return RedirectToAction("BookingList");
        }

        public ActionResult RoomDetails(int id)
        {
            RoomVM rvm = new RoomVM
            {
                Room = _rRep.Find(id)
            };

            return View(rvm);

        }

        public ActionResult RezervasyonOnayla(CartPageVM cpvm)
        {
            if (cpvm.BookingDetail.CheckIn >= cpvm.BookingDetail.CheckOut)
            {
                TempData["tarihhatali"] = "Tarih seçimini doğru yapmadınız";
                return RedirectToAction("CartPage");
            }

            if (cpvm.BookingDetail.CheckIn < DateTime.Today)
            {
                TempData["tarihhatali"] = "Tarih seçimini doğru yapmadınız";
                return RedirectToAction("CartPage");
            }

            TempData["tarihgiris"] = cpvm.BookingDetail.CheckIn;
            TempData["tarihcikis"] = cpvm.BookingDetail.CheckOut;
            TempData["rezervasyontur"] = cpvm.BookingDetail.BookingType;

            TempData["firstName"] = cpvm.CustomerProfile.FirstName;
            TempData["lastName"] = cpvm.CustomerProfile.LastName;

            return RedirectToAction("RezervasyonBankaOnay");

        }

        public ActionResult RezervasyonBankaOnay()
        {
            Cart sepet = Session["bcart"] as Cart;

            DateTime dg = Convert.ToDateTime(TempData["tarihgiris"]);
            DateTime dc = Convert.ToDateTime(TempData["tarihcikis"]);

            TimeSpan kalan = dc.Subtract(dg);
            decimal tutar = sepet.TotalPrice * Convert.ToInt32(kalan.Days);

            if (TempData["rezervasyontur"].ToString() == "TamPansiyon")
            {
                tutar = tutar * 2;
            }

            if (TempData["rezervasyontur"].ToString() == "HerseyDahil")
            {
                tutar = tutar * 3;
            }


            Customer c = new Customer();
            c.UserName = "asd";
            c.Password = "123";
            c.RePassword = "123";
            c.Active = true;
            c.Email = "asd";
            _cRep.Add(c);

            CustomerProfile cp = new CustomerProfile();
            cp.FirstName = TempData["firstName"].ToString();
            cp.LastName = TempData["lastName"].ToString();
            cp.Address = "asd";
            cp.PhoneNo = "asda";
            cp.BirthDate = DateTime.Now;
            cp.ID = c.ID;
            _cpRep.Add(cp);

            Booking book = new Booking();
            book.TotalPrice = tutar;
            book.UserName = "asd";
            book.Email = "asd";
             
            book.CustomerID = cp.ID;   // Yukarıda cp.ID Add ile oluşturuldu..
            _bRep.Add(book);
            //book.UserName = kullanici.UserName;



            foreach (CartItem item in sepet.Sepetim)
            {
                for (int i = 1; i <= item.Amount; i++)
                {

                    BookingDetail bd = new BookingDetail();
                    bd.BookingID = book.ID; // _oRep.Add(ovm.Order); sayesinde OrderID'si oluşmuştu yoksa null olur..
                    bd.RoomID = item.ID;
                    bd.SubPrice = item.Price;

                    // bd.Quantity = item.Amount;

                    bd.CheckIn = dg;
                    bd.CheckOut = dc;



                    if (TempData["rezervasyontur"].ToString() == "TamPansiyon")
                    {
                        bd.BookingType = Package.TamPansiyon;
                    }
                    if (TempData["rezervasyontur"].ToString() == "HerseyDahil")
                    {
                        bd.BookingType = Package.HerseyDahil;
                    }
                    bd.ReservationActive = true;


                    List<RoomDetail> rds = _rdRep.GetRoomById(item.ID);
                    // _db.Set<RoomDetail>().Where(x => x.ID == item.ID).List();

                    foreach (RoomDetail rd in rds)
                    {
                        if (rd.Situation == true && rd.isMaintenance == false)
                        {
                            bd.RoomNo = rd.RoomNo;


                            rd.Situation = false;
                            _rdRep.Update(rd);   //  RoomDetailRepository _rdRep; // _rdRep = new RoomDetailRepository();

                            break; // continue
                        }
                    }





                    _bdRep.Add(bd);

                    //Stoktan düsmesini istiyorsanız
                    //Product stokDus = _pRep.Find(item.ID);
                    //stokDus.UnitsInStock -= item.Amount;
                    //_pRep.Update(stokDus);

                }
            }

            TempData["odeme"] = "Rezervasyon işlemi gerçekleştirilmiştir ";
            //MailSender.Send(bvm.Booking.Email, body: $"Rezervasyonunuz basarıyla alındı..{bvm.Booking.TotalPrice}", subject: "Rezervasyon!!");
            Session.Remove("bcart");
            return RedirectToAction("BookingList");









        }



        // GET: Staff/BookingfromStaff
        public ActionResult Index()
        {
            return View();
        }
    }
}