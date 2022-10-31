using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MIST143_Traveler.Models;
using MIST143_Traveler.Models.miViewModel;
using MimeKit;
using MailKit.Net.Smtp;
using MIST143_Traveler.MViewModel;

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Protocols;
using System.Security.Cryptography;
using System.Text;

namespace MIST143_Traveler.Controllers
{
    public class CustomerCenterController : Controller
    {
        private readonly PlanetTravelContext _PlanetTravelContext;
        private IWebHostEnvironment _enviro;
        public CustomerCenterController(PlanetTravelContext PlanetTrave, IWebHostEnvironment p)
        {
            _PlanetTravelContext = PlanetTrave;
            _enviro = p;
        }

        public IActionResult Index()
        {
            return PartialView();
        }
        public IActionResult Logout()
        {
           HttpContext.Session.Remove(CDictionary.SK_Login);
           return RedirectToAction("Index", "Home");
        }

        public IActionResult newLoginpag()
        {
            return View();
        }
        //========================訂單明細顯示=======================
        public IActionResult OrderDE(int id)
        {
           
            OrderDetail od = _PlanetTravelContext.OrderDetails.FirstOrDefault(a => a.OrderId == id);
            COrderDetail co = new COrderDetail();
            co.訂單 = (from q in _PlanetTravelContext.Orders.Where(w=>w.OrderId==id)
                     from a in _PlanetTravelContext.OrderDetails.Where(a => a.OrderId == id)
                     from b in _PlanetTravelContext.TravelProducts.Where(s => s.TravelProductId == a.TravelProductId)
                    
                     select new 訂單管理
                     {
                         TravelProductId=b.TravelProductId,
                         訂單編號=a.OrderId,
                         商品名稱=b.TravelProductName,
                         數量=a.Quantity,
                         購買金額=(int)a.UnitPrice,
                         訂購日期=a.Order.OrderDate,
                         優惠券 =q.Coupon.CouponName,
                         付款方式=q.Payment.PaymentName
                         
                     }).ToList();
            foreach (var item in co.訂單)
            {
                item.FImagePath = _PlanetTravelContext.TravelPictures.Where(a => a.TravelProductId == item.TravelProductId).Select(s => s.TravelPicture1).ToList(); 
            }
            
            
            return View(co);
        }

        //====================================AJAX上傳圖片=========================
        public IActionResult FileUpLoad(IFormFile files)
        {
            var Name = HttpContext.Session.GetString(CDictionary.SK_Login);
            var v = JsonSerializer.Deserialize<Member>(Name);
            Member Cus = _PlanetTravelContext.Members.FirstOrDefault(a => a.MembersId == v.MembersId);
            
            if (files != null)
            {
                string pName = Guid.NewGuid().ToString() + ".jpg";
                Cus.PhotoPath = pName;
                string path = _enviro.WebRootPath + "/Images/客戶大頭貼/" + pName;
                using (var steam = new FileStream(path, FileMode.Create))
                {
                    files.CopyTo(steam);
                    _PlanetTravelContext.SaveChanges();
                    
                }
                string p = "/Images/客戶大頭貼/" + pName;
                return Json(p);
                //return new ObjectResult(new { status = "success" });
            }
            return Json(new { Res = false, Msg = "失敗" });
        }

        [HttpPost]
        public IActionResult SaveCusInfo(CMemberView inCus)
        {
            Member Cus = _PlanetTravelContext.Members.FirstOrDefault(a => a.MembersId == inCus.MembersId);


            if (Cus != null)
            {
                if (inCus.photo != null)
                {
                    string pName = Guid.NewGuid().ToString() + ".jpg";//創造一個唯一路徑
                    Cus.PhotoPath = pName;
                    string path = _enviro.WebRootPath + "/Images/客戶大頭貼/" + pName;//此為網頁路徑  
                    inCus.photo.CopyTo(new FileStream(path, FileMode.Create));//FileStream這串流需要兩個參數1.網路路徑OR專案路徑2.FILEMODE
                }
                Cus.Address = inCus.Address;
                Cus.MemberName = inCus.MemberName;
                Cus.Phone = inCus.Phone;
                Cus.CityId = _PlanetTravelContext.Cities.FirstOrDefault(a => a.CityName == inCus.Cityname).CityId;
                _PlanetTravelContext.SaveChanges();
                return Json(new { Res = true, Msg = "成功" });
            }
            return Json(new { Res = false, Msg = "失敗" });
        }
        //=============================變更密碼頁============================
        public IActionResult PasswordChange(CPasswordChange inCP)
        {
            Member Cus = _PlanetTravelContext.Members.FirstOrDefault(a => a.MembersId == inCP.MembersId);
            if (Cus != null)
            {
                if (inCP.舊密碼 == Cus.Password && inCP.新密碼 == inCP.確認新密碼)
                {
                    Cus.Password = inCP.確認新密碼;
                    _PlanetTravelContext.SaveChanges();
                    return Json(new { Res = true, Msg = "成功" });
                }
                return Json(new { Res = false, Msg = "失敗" });
            }
            return ViewComponent("PasswordChange");
        }
        //=============================變更密碼頁============================


        public IActionResult List(int MembersId)
        {
            CMemberView cm = new CMemberView();
            if (MembersId != 0)
            {
                cm = _PlanetTravelContext.Members.Where(a => a.MembersId == MembersId).Select(a => new CMemberView
                {
                    member = a,
                    Cityname = a.City.CityName,

                }).FirstOrDefault();

                if (cm != null)
                {
                    return View(cm);
                }
            }

            return View("test");
        }
        public IActionResult TextM(int? MembersId)
        {
            CMemberView Cus = _PlanetTravelContext.Members.Where(a => a.MembersId == MembersId).Select(inCus => new CMemberView
            {
                Address = inCus.Address,
                //Password = inCus.Password,
                MemberName = inCus.MemberName,
                Phone = inCus.Phone,
                FImagePath = inCus.PhotoPath,

            }).FirstOrDefault();


            return View();

        }


        //左邊功能開始
        [HttpPost]
        public IActionResult newLoginpag(CLogin vModel)
        {
            Member cust = _PlanetTravelContext.Members.FirstOrDefault
              (c => c.Email.Equals(vModel.Email));
            if (cust != null)
            {

                if (cust.Password.Equals(vModel.Password))
                {



                    string jsonUser = JsonSerializer.Serialize(cust);
                    HttpContext.Session.SetString(
                        CDictionary.SK_Login, jsonUser);

                    return Json(new { Res = true, Msg = "成功" });
                }
                else
                    return Json(new { Res = false, Msg = "失敗" });

            }
            return View();
        }



        public IActionResult ProductManage()
        {
            return View();
        }
        public IActionResult newProductManage()
        {
            return View();
        }

        public IActionResult Myfavorites()
        {
            return PartialView();
        }
        public IActionResult Review()
        {
            return PartialView();
        }
        //============================檢視優惠券頁==============================
        public IActionResult VIEWCoupon(int MembersId)
        {
            會員中心檢視優惠券 Coup = new 會員中心檢視優惠券();
            if (MembersId != 0)
            {
                
                Coup.優惠券列表= (from Cuu in _PlanetTravelContext.CouponLists.Where(a => a.MembersId == MembersId)
                                  from Cup in _PlanetTravelContext.Coupons.Where(s=>s.CouponId==Cuu.CouponId)
                              select new 我的優惠券
                              {
                                 MembersId = MembersId,
                                 CouponId = Cup.CouponId,
                                 CouponName= Cup.CouponName,
                                 Condition= Cup.Condition,
                                 Discount= Cup.Discount,
                                 ExDate= Cup.ExDate,
                              }
                            ).ToList();
            }
          
                return ViewComponent("Coupon", Coup);
        }

        public IActionResult Giftkey(會員中心檢視優惠券 Cou)
        {

             var q = _PlanetTravelContext.Coupons.FirstOrDefault(a => a.GiftKey == Cou.GiftKey);
                if (q != null)
                {
                    var use = _PlanetTravelContext.CouponLists.FirstOrDefault(s =>s.MembersId==Cou.MembersId&&s.CouponId==q.CouponId);
               
                if (use == null) { 
                    CouponList li = new CouponList
                    {
                        CouponId = q.CouponId,
                        CouponStatus=true,
                        MembersId=Cou.MembersId,
                    };
                    會員中心檢視優惠券 動態 = new 會員中心檢視優惠券
                    {
                        CouponId=q.CouponId,
                        Condition=q.Condition,
                        ExDate=q.ExDate,
                        Useful=q.Useful,
                        Discount=q.Discount,
                        CouponName=q.CouponName
                    };
                    _PlanetTravelContext.CouponLists.Add(li);
                    _PlanetTravelContext.SaveChanges();
                return Json(new { Res = true, Msg = "成功",data= 動態 });
                }
                return Json(new { Res = false, Msg = "已經使用過" });
            }
            return Json(new { Res = false, Msg = "無效的優惠" });
        }
        //============================檢視優惠券頁==============================
        public IActionResult Coupon()
        {
            return PartialView();
        }
        public IActionResult Star()
        {
            return PartialView();
        }

        public IActionResult CustomerInfo(int MembersId)
        {

            CMemberView cm = new CMemberView();
            if (MembersId != 0)
            {
                cm = _PlanetTravelContext.Members.Where(a => a.MembersId == MembersId).Select(a => new CMemberView
                {
                    member = a,
                    Cityname = a.City.CityName,

                }).FirstOrDefault();

                if (cm != null)
                {
                    return View(cm);
                }
            }

            return View("test");
        }
        [HttpPost]
        public IActionResult CustomerInfo(CMemberView inCus)
        {

            Member Cus = _PlanetTravelContext.Members.FirstOrDefault(a => a.MembersId == inCus.MembersId);

            if (Cus != null)
            {
                Cus.Address = inCus.Address;
                Cus.Password = inCus.Password;
                Cus.MemberName = inCus.MemberName;
                Cus.Phone = inCus.Phone;
                Cus.CityId = _PlanetTravelContext.Cities.FirstOrDefault(a => a.CityName == inCus.Cityname).CityId;
                _PlanetTravelContext.SaveChanges();
                return Json(new { Res = true, Msg = "成功" });
            }
            return Json(new { Res = false, Msg = "失敗" });
        }
        //左邊功能結束
        public IActionResult test()
        {

            return View();
        }
      
        //============================找出客戶訂單=============================
        public IActionResult Order(int MembersId)
        {
            if (MembersId != 0)
            {
                COrderDetail Cord = new COrderDetail();
                Cord.訂單 = (from od in _PlanetTravelContext.Orders.Where(a => a.MembersId == MembersId)
                           from p in od.OrderDetails.Where(a => a.OrderId == od.OrderId)
                           select new 訂單管理
                           {
                               orderId = od.OrderId,
                               訂單編號 = od.OrderId,
                               商品名稱 = p.TravelProduct.TravelProductName,
                               訂單狀態 = od.OrderStatus.OrderStatusName,
                               訂購日期 = DateTime.Parse(od.OrderDate).ToString("yyyy-MM-dd"),
                               購買金額 = p.UnitPrice,
                               數量=p.Quantity,
                           }).ToList();
                if (Cord.訂單.Count > 0)
                {
                    return ViewComponent("CustomerOrder", Cord);
                }


            }
            return ViewComponent("ProductManage");
            //}
            //===================================客戶取消訂單光箱================================
        }
        public IActionResult OrderCancel(int id)
        {
            OrderCancel idd = new OrderCancel();
            idd.OrderId = id;
            return View(idd);
        }

        [HttpPost]
        public IActionResult OrderCancel(OrderCancel inD)
        {
            OrderCancel co = null;
            if (inD != null)
            {

                co = _PlanetTravelContext.Orders.Where(a => a.OrderId == inD.OrderId).Select(s => new OrderCancel
                {
                    OrderId = inD.OrderId,
                    Titel = inD.Titel,
                    CancaelContent = inD.CancaelContent,

                }).FirstOrDefault();


                _PlanetTravelContext.OrderCancels.Add(co);
                _PlanetTravelContext.SaveChanges();
                return Json(new { Res = true, Msg = "成功" });
            };
            return Json(new { Res = false, Msg = "失敗" });
        }


        //===================================註冊頁面=======================================

        public IActionResult Createmember()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Createmember(CMemberView CCC)//註冊
        {
            if (ModelState.IsValid)
            {
                if (CCC != null)
                {

                    var e = _PlanetTravelContext.Members.Where(a => a.Email == CCC.Email).ToList();

                    if (e.Count > 0)
                        return Json(new { Res = false, Msg = "EMAIL帳號重複" });
                    if (e.Count == 0 && CCC.Password == CCC.Password2)
                    {
                        var q = _PlanetTravelContext.Cities.FirstOrDefault(a => a.CityName == CCC.Cityname);

                        CCC.CityId = q.CityId;
                        Member aa = CCC.member;

                        _PlanetTravelContext.Add(aa);
                        _PlanetTravelContext.SaveChanges();

                        return Json(new { Res = true, Msg = "成功" });
                    }
                    return Json(new { Res = false, Msg = "失敗" });



                }
            }
            return RedirectToAction("Index", "Home");
        }
        //===================================註冊頁面=======================================
        public IActionResult LoginModal()
        {
            return View();
        }

        public IActionResult CheckPas(string Email)
        {
            var e = _PlanetTravelContext.Members.Where(a => a.Email == Email).ToList();
            if (e.Count > 0)
                return Content("此帳號已存在", "text/plain");

            else
                return Content("", "text/plain");
        }
        //=========================忘記密碼============================
        public IActionResult Forgetpas()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Forgetpas(CLogin Mem)
        {
            
            
            Member Cust = _PlanetTravelContext.Members.FirstOrDefault(a => a.Email == Mem.Email);
            if (Cust==null)
            {
                return Json(new { Res = false});
            }
            // 取出會員信箱
            string UserEmail = Cust.Email;


            //取得系統自定密鑰，在 Web.config 設定
            //string SecretKey = ConfigurationManager.AppSettings["SecretKey"];

            //產生帳號 + 時間驗證碼
            string sVerify = Cust.Email + "|" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");

            // 將驗證碼使用 3DES 加密
            //TripleDESCryptoServiceProvider DES = new TripleDESCryptoServiceProvider();
            //MD5 md5 = new MD5CryptoServiceProvider();
            ////byte[] buf = Encoding.UTF8.GetBytes(SecretKey);
            //byte[] result = md5.ComputeHash(buf);
            //string md5Key = BitConverter.ToString(result).Replace("-", "").ToLower().Substring(0, 24);
            //DES.Key = UTF8Encoding.UTF8.GetBytes(md5Key);
            //DES.Mode = CipherMode.ECB;
            //ICryptoTransform DESEncrypt = DES.CreateEncryptor();
            //byte[] Buffer = UTF8Encoding.UTF8.GetBytes(sVerify);
            //sVerify = Convert.ToBase64String(DESEncrypt.TransformFinalBlock(Buffer, 0, Buffer.Length)); // 3DES 加密後驗證碼

            MimeMessage message = new MimeMessage();
            BodyBuilder builder = new BodyBuilder();

            Random ran = new Random();
            int rannum = ran.Next(9999) + 1966728;
            HttpContext.Session.SetInt32(CDictionary.SK_忘記密碼驗證碼, rannum);
            builder.HtmlBody = $"<p>你好，你的驗證碼為{rannum}</p>" +
                              $"<div style='border: 2px solid black;text-align: center;'>      </div>" +
                              $"<p>當前時間:{DateTime.Now:yyyy-MM-dd HH:mm:ss}</p>";

            message.From.Add(new MailboxAddress("PlanetTraveler星球旅遊", "planettravelermsit143@outlook.com"));
            message.To.Add(new MailboxAddress("親愛的顧客", Mem.Email));
            message.Subject = "PlanetTraveler星球旅遊:忘記密碼";
            message.Body = builder.ToMessageBody();

            using (SmtpClient client = new SmtpClient())
            {
                client.Connect("smtp.outlook.com", 25, MailKit.Security.SecureSocketOptions.StartTls);
                client.Authenticate("planettravelermsit143@outlook.com", "gogo1116");
                client.Send(message);
                client.Disconnect(true);
            }
            return Json(new {Res=true});
        }

        public IActionResult Resetpas(string email)
        {


            return View(email);
        }

        //==========================================在網頁加到我的最愛==================================
        public IActionResult toMyFavorites(CtoMyFavorites PMID)
        {
            if (PMID != null)
            { 
            var Cus = _PlanetTravelContext.Members.FirstOrDefault(a => a.MembersId == PMID.MembersId);
                if (Cus != null)
                {

                    var cc = _PlanetTravelContext.Myfavorites.Where(s => s.MembersId == PMID.MembersId&&s.TravelProductId == PMID.TravelProductId).ToList();
                    
                    if (cc.Count > 0)
                    {
                       return Json(new { Res = false});
                    }
                    Myfavorite fa = PMID.myfavorite;
                    _PlanetTravelContext.Add(fa);
                    _PlanetTravelContext.SaveChanges();
                    return Json(new { Res = true });
                }
                return Json(new { Res = false });
            }
            return RedirectToAction("Index", "Home");
        }
        //==============================檢視會員中心的我最愛==============================
        public IActionResult LMyFavorites(int MembersId,int id)
        {
            if (MembersId != 0||id!=0)
            {
                會員中心檢視最愛 CP = new 會員中心檢視最愛();
                CP.商品列表 = (from fa in _PlanetTravelContext.Myfavorites.Where(a => a.MembersId == MembersId)
                               from pid in _PlanetTravelContext.TravelProducts.Where(c=>c.TravelProductId==fa.TravelProductId)
                           //from image in _PlanetTravelContext.TravelPictures.Where(m => m.TravelPictureId == pid.TravelProductId)
                           select new 最愛商品
                           {
                               TravelProductId=pid.TravelProductId,
                               TravelProductName=pid.TravelProductName,
                               MyfavoritesID = fa.MyfavoritesId,
                               Price =pid.Price,
                               TravelPicturePath = pid.TravelPictures.FirstOrDefault().TravelPicture1,
                               MembersId=MembersId,
                           }
                         ).ToList();

                if (id != 0)
                {
                    var c = _PlanetTravelContext.Myfavorites.FirstOrDefault(a => a.MyfavoritesId == id);
                    _PlanetTravelContext.Remove(c);
                    _PlanetTravelContext.SaveChanges();

                }


                if (CP.商品列表.Count > 0)
                {
                    return ViewComponent("MyFavorites", CP);
                }
            }

            return PartialView("Myfavorites");
        }

        //============================================================================

        public IActionResult City(int id)
        {
            /* var qq = _PlanetTravelContext.Members.FirstOrDefault(p=>p.MembersId==id).CityId;*///找到這個MEMBER的城市ID
            var city = _PlanetTravelContext.Cities.Where(a => a.CityId != id && a.CountryId == 1).Select(a => a.CityName);
            return Json(city);
        }


        //客戶評論檢視頁================================================

        public IActionResult CommentList(int MembersId)
        {
            if (MembersId != 0)
            {

                CCommentList Comli = new CCommentList();
                Comli.評論 = (from li in _PlanetTravelContext.Comments.Where(a => a.MembersId == MembersId)
                            select new 會員評論
                            {
                                CommentID = li.CommentId,
                                產品名稱 = li.TravelProduct.TravelProductName,
                                內容 = li.CommentText,
                                分數 = li.Star,
                                日期 = li.CommentDate,
                                //DateTime.Parse(od.OrderDate).ToString("yyyy-MM-dd"),
                            }).ToList();
                if (Comli.評論.Count > 0)
                {
                    return ViewComponent("CommentList", Comli);
                }

            }
            return PartialView("Review");
        }
        //新增評論=============================
       
        public IActionResult CommentCreate(int id)
        {
            C新增評論 cc = new C新增評論();
            cc = (from a in _PlanetTravelContext.Orders.Where(s => s.OrderId == id)
                  from b in _PlanetTravelContext.Members.Where(d => d.MembersId == a.MembersId)
                  from c in _PlanetTravelContext.OrderDetails.Where(e => e.OrderId == a.OrderId)
                  select new C新增評論
                  {
                    CMembersId=b.MembersId,
                    CTravelProductID=c.TravelProductId,
                  }).FirstOrDefault();
            
            return View(cc);
        }
        [HttpPost]
        public IActionResult CommentCreate(Ccomment comm) 
        {
            

            var q = _PlanetTravelContext.Members.Where(a => a.MembersId == comm.MembersId);
            comm.CommentDate = DateTime.Now.ToString();
            comm.CommentStatus = true;
            Comment ss = comm.comment;
                
                
             
                _PlanetTravelContext.Comments.Add(ss);
                _PlanetTravelContext.SaveChanges();
                
           
            return Json(new { Res=true});
        }
        //評論修改檢視頁
        public IActionResult CommentEdit(int? CommentID)
        {
            if (CommentID != 0)
            {
                CCommentList Comli = new CCommentList();
                Comli.評論 = (from li in _PlanetTravelContext.Comments.Where(a => a.CommentId == CommentID)
                            select new 會員評論
                            {
                                CommentID = li.CommentId,
                                產品名稱 = li.TravelProduct.TravelProductName,
                                內容 = li.CommentText,
                                分數 = li.Star,

                            }).ToList();
                return View(Comli);
            }
            return RedirectToAction("CommentList");
        }
        //評論修改
        [HttpPost]
        public IActionResult CommentEdit(CCommentList inComli)
        {
            var data = _PlanetTravelContext.Comments.FirstOrDefault(a => a.CommentId == inComli.CommentID);
            if (data != null)
            {
                data.CommentText = inComli.內容;
                data.Star = inComli.分數;
                _PlanetTravelContext.SaveChanges();
                return Json(new { Res = true, Msg = "成功" });
            };
            return Json(new { Res = false, Msg = "失敗" });
        }
        public IActionResult CommentDelete(int MembersId, int CommentID)
        {
            if (MembersId != 0)
            {
                if (CommentID != 0)
                {
                    var od = _PlanetTravelContext.Comments.Where(x => x.CommentId == CommentID).FirstOrDefault();
                    _PlanetTravelContext.Comments.Remove(od);
                    _PlanetTravelContext.SaveChanges();
                    CCommentList Comli = new CCommentList();
                    Comli.評論 = (from li in _PlanetTravelContext.Comments.Where(a => a.MembersId == MembersId)
                                select new 會員評論
                                {
                                    CommentID = li.CommentId,
                                    產品名稱 = li.TravelProduct.TravelProductName,
                                    內容 = li.CommentText,
                                    分數 = li.Star,
                                    日期 = li.CommentDate
                                }).ToList();
                    if (Comli.評論.Count > 0)
                    {
                        return ViewComponent("CommentList", Comli);
                    }
                }

            }
            return PartialView("Review");

            //客戶評論 END================================================
        }
    }

}

