using Project.BLL.DesignPatterns.RepositoryPattern.ConcRep;
using Project.COMMON.Tools;
using Project.ENTITIES.Models;
using Project.WEBUI.Models.BookingTools;
using Project.WEBUI.VMClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using static Project.ENTITIES.Models.BookingDetail;

namespace Project.WEBUI.Controllers
{
    // [MemberAuthentication]
    public class BookingController : Controller
    {
        BookingRepository _bRep;
        RoomRepository _rRep;
        RoomDetailRepository _rdRep; 
        BookingDetailRepository _bdRep;

        public BookingController()
        {
            _bRep = new BookingRepository();
            _rRep = new RoomRepository();
            _rdRep = new RoomDetailRepository();
            _bdRep = new BookingDetailRepository();
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

            if (cpvm.BookingDetail.BookingType == 0)
            {
                TempData["rezervasyonturuhatali"] = "Rezervasyon türü seçmelisiniz";
                return RedirectToAction("CartPage");
            }

            TempData["tarihgiris"] = cpvm.BookingDetail.CheckIn;
            TempData["tarihcikis"] = cpvm.BookingDetail.CheckOut;
            TempData["rezervasyontur"] = cpvm.BookingDetail.BookingType;

            Customer mevcutKullanici;
            if (Session["member"] != null)
            {
                mevcutKullanici = Session["member"] as Customer;
            }
            else TempData["anonim"] = "Kullanıcı üye degil";
            return View();
        }



        //https://localhost:44337/api/Payment/ReceivePayment https://localhost:44337/

        [HttpPost]
        public ActionResult RezervasyonBankaOnay(BookingVM bvm)
        {
            bool result;
            Cart sepet = Session["bcart"] as Cart;

            DateTime dg = Convert.ToDateTime(TempData["tarihgiris"]);
            DateTime dc = Convert.ToDateTime(TempData["tarihcikis"]);

            TimeSpan kalan = dc.Subtract(dg);
            decimal tutar = sepet.TotalPrice * Convert.ToInt32(kalan.Days);

            if (TempData["rezervasyontur"].ToString() == "TamPansiyon")
            {
                tutar = tutar * 2;
            }

            else if (TempData["rezervasyontur"].ToString() == "HerseyDahil")
            {
                tutar = tutar * 3;
            }


            TimeSpan indirim = dg.Subtract(DateTime.Today);
            int discount;

            if (Convert.ToInt32(indirim.Days) >= 90)
            {
                discount = Convert.ToInt32(tutar) / 100;
                discount *= 23;
                tutar = tutar - discount;
            }
            else if (Convert.ToInt32(indirim.Days) >= 30 && TempData["rezervasyontur"].ToString() == "HerseyDahil")
            {
                discount = Convert.ToInt32(tutar) / 100;
                discount *= 18;
                tutar = tutar - discount;
            }
            else if (Convert.ToInt32(indirim.Days) >= 30 && TempData["rezervasyontur"].ToString() == "TamPansiyon")
            {
                discount = Convert.ToInt32(tutar) / 100;
                discount *= 16;
                tutar = tutar - discount;
            }


            bvm.Booking.TotalPrice = bvm.PaymentDTO.BookingPrice = tutar;

            #region APISection


            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44337/api/");
                Task<HttpResponseMessage> postTask = client.PostAsJsonAsync("Payment/ReceivePayment", bvm.PaymentDTO);
                HttpResponseMessage sonuc;

                try
                {
                    sonuc = postTask.Result;
                }
                catch (Exception ex)
                {
                    TempData["baglantiRed"] = "Banka baglantıyı red etti";
                    return RedirectToAction("BookingList");
                }

                if (sonuc.IsSuccessStatusCode) result = true;
                else result = false;

                if (result)
                {
                    if (Session["member"] != null)
                    {
                        Customer kullanici = Session["member"] as Customer;

                      
                        bvm.Booking.CustomerID = kullanici.ID;
                        bvm.Booking.UserName = kullanici.UserName;
                    }
                    else
                    {
                        // bvm.Booking.CustomerID = null;
                        bvm.Booking.UserName = TempData["anonim"].ToString();
                    }

                    _bRep.Add(bvm.Booking);  

                    foreach (CartItem item in sepet.Sepetim)
                    {
                        for (int i = 1; i <= item.Amount; i++)
                        {

                            BookingDetail bd = new BookingDetail();
                            bd.BookingID = bvm.Booking.ID; // _oRep.Add(ovm.Order); sayesinde OrderID'si oluşmuştu yoksa null olur..
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
                                    if(rd.Situation == true  && rd.isMaintenance == false)
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

                    TempData["odeme"] = "Rezervazyonunuz bize ulasmıstır..Tesekkür ederiz";
                    MailSender.Send(bvm.Booking.Email, body: $"Rezervasyonunuz basarıyla alındı..{bvm.Booking.TotalPrice}", subject: "Rezervasyon!!");
                    Session.Remove("bcart");
                    return RedirectToAction("BookingList");


                }

                else
                {
                    TempData["sorun"] = "Odeme ile ilgili bir sorun olustu..Lutfen bankanız ile iletişime geciniz"; // bu kısımda yazılımcının bir sorumluuluğu yoktur.. Bank'a taraflı bir sorun var..
                    return RedirectToAction("BookingList");
                }

            }

            #endregion
        }

        // GET: Booking
        public ActionResult Index()
        {
            return View();
        }
    }
}