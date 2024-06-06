using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webIDZ.Models.Entities;
using webIDZ.Models.ViewModels;
using System.Web.Security;
using System.Security.Principal;
using System.Security.Cryptography;
using System.Text;

namespace webIDZ.Controllers
{
    public class mainController : Controller
    {
        // GET: main
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult CatchesList()
        {
            List<Catch> catches = new List<Catch>();

            using (var db = new MiceCatchEntities())
            {
                catches = db.Catch.OrderByDescending(x => x.DateCatch).ToList();
            }
            return View(catches);
        }

        [Authorize]
        [HttpGet]
        public ActionResult CatchDetails(int catchID)
        {

            Catch model = new Catch();
            using (var db = new MiceCatchEntities())
            {
                model = db.Catch.Find(catchID);
                model.Stations = db.Stations.Find(model.Station_ID);
                model.Zones = db.Zones.Find(model.Zone_ID);
            }
            return View(model);
        }





        List<Tuple<int, string>> GetStationsList()
        {
            List<Tuple<int, string>> stations = new List<Tuple<int, string>>
            {
                new Tuple<int,string>(1,"Закрытая луго-полевая"),
                new Tuple<int,string>(2,"Лесо-кустарниковая"),
                new Tuple<int,string>(3,"Населенные пункты"),
                new Tuple<int,string>(4,"Околоводная"),
                new Tuple<int,string>(5,"Открытая луго-полевая"),
                new Tuple<int,string>(6,"Постройки"),
                new Tuple<int,string>(7,"Другое"),

            };

            return stations;
        }

        List<Tuple<int, string>> GetZonesList()
        {
            List<Tuple<int, string>> zones = new List<Tuple<int, string>>
            {
                new Tuple<int,string>(1,"Лесостепь"),
                new Tuple<int,string>(2,"Тайга(мелколиственные леса)"),
                new Tuple<int,string>(3,"Тайга(средняя Тайга)"),
                new Tuple<int,string>(4,"Тайга(южная Тайга)"),
                new Tuple<int,string>(5,"Другая"),


            };

            return zones;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]

        public ActionResult CreateCatch()
        {
            ViewBag.Stations = new SelectList(GetStationsList(), "Item1", "Item2");
            ViewBag.Zones = new SelectList(GetZonesList(), "Item1", "Item2");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult CreateCatch(CatchVM newCatch)
        {
            if (ModelState.IsValid)
            {
                using (var db = new MiceCatchEntities())
                {
                    Catch catchy = new Catch()
                    {
                        ID_Catch = db.Catch.OrderByDescending(x => x.ID_Catch).FirstOrDefault().ID_Catch + 1,
                        DateCatch = newCatch.DateCatch,
                        Zone_ID = newCatch.Zone_ID,
                        Station_ID = newCatch.Station_ID,
                        Biotope = newCatch.Biotope,
                        Distrtict = newCatch.Distrtict,
                        Place = newCatch.Place,
                        Coords_X = newCatch.Coords_X,
                        Coords_Y = newCatch.Coords_Y,
                        Traps_Amount = newCatch.Traps_Amount,
                        Catched_Amount = newCatch.Catched_Amount,
                        Comments = newCatch.Comments
                    };



                    db.Catch.Add(catchy);
                    db.SaveChanges();
                }
                return RedirectToAction("CatchesList");
            }
            ViewBag.Stations = new SelectList(GetStationsList(), "Item1", "Item2");
            ViewBag.Zones = new SelectList(GetZonesList(), "Item1", "Item2");
            return View(newCatch);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult EditCatch(int catchID)
        {
            CatchVM model;
            using (var db = new MiceCatchEntities())
            {
                Catch catchy = db.Catch.Find(catchID);
                model = new CatchVM
                {
                    ID_Catch = catchy.ID_Catch,
                    DateCatch = catchy.DateCatch,
                    Zone_ID = catchy.Zone_ID,
                    Station_ID = catchy.Station_ID,
                    Biotope = catchy.Biotope,
                    Distrtict = catchy.Distrtict,
                    Place = catchy.Place,
                    Coords_X = catchy.Coords_X,
                    Coords_Y = catchy.Coords_Y,
                    Traps_Amount = catchy.Traps_Amount,
                    Catched_Amount = catchy.Catched_Amount,
                    Comments = catchy.Comments
                };
            }
            ViewBag.Stations = new SelectList(GetStationsList(), "Item1", "Item2");
            ViewBag.Zones = new SelectList(GetZonesList(), "Item1", "Item2");
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        [Authorize(Roles = "Admin")]
        public ActionResult EditCatch(CatchVM model)
        {
            if (ModelState.IsValid)
            {
                using (var db = new MiceCatchEntities())
                {
                    Catch editedCatch = new Catch
                    {
                        ID_Catch = model.ID_Catch,
                        DateCatch = model.DateCatch,
                        Zone_ID = model.Zone_ID,
                        Station_ID = model.Station_ID,
                        Biotope = model.Biotope,
                        Distrtict = model.Distrtict,
                        Place = model.Place,
                        Coords_X = model.Coords_X,
                        Coords_Y = model.Coords_Y,
                        Traps_Amount = model.Traps_Amount,
                        Catched_Amount = model.Catched_Amount,
                        Comments = model.Comments
                    };
                    db.Catch.Attach(editedCatch);
                    db.Entry(editedCatch).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
                return RedirectToAction("CatchesList");
            }
            ViewBag.Stations = new SelectList(GetStationsList(), "Item1", "Item2");
            ViewBag.Zones = new SelectList(GetZonesList(), "Item1", "Item2");
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteCatch(int catchID)
        {
            Catch catchToDelete;
            using (var db = new MiceCatchEntities())
            {
                catchToDelete = db.Catch.Find(catchID);
                catchToDelete.Stations = db.Stations.Find(catchToDelete.Station_ID);
                catchToDelete.Zones = db.Zones.Find(catchToDelete.Zone_ID);
            }
            return View(catchToDelete);
        }

        [HttpPost, ActionName("DeleteCatch")]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteCatchPost(int catchID)
        {
            using (var db = new MiceCatchEntities())
            {
                Catch catchToDelete = db.Catch.Find(catchID);
                db.Catch.Remove(catchToDelete);
                db.SaveChanges();
            }
            return RedirectToAction("CatchesList");
        }

        [AllowAnonymous]
        [HttpGet]

        public ActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(UserVM webUser)
        {
            if (ModelState.IsValid)
            {
                using (MiceCatchEntities context = new MiceCatchEntities())
                {
                    User user = null;
                    user = context.User.Where(u => u.Login == webUser.Login).FirstOrDefault();
                    if (user != null)
                    {
                        string passwordHash = ReturnHashCode(webUser.Password + user.Salt.ToString().ToUpper());

                        if (passwordHash == user.PasswordHash)
                        {
                            string userRole = "";


                            switch (user.UserRole)
                            {
                                case 1:
                                    userRole = "User";
                                    break;
                                case 2:
                                    userRole = "Admin";
                                    break;
                            }

                            FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(1, user.Login, DateTime.Now, DateTime.Now.AddDays(1), false, userRole);

                            string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                            HttpContext.Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket));
                            return RedirectToAction("CatchesList");
                        }
                    }
                }
            }
            ViewBag.Error = "Неверный логин/пароль или пользователь не существует";
            return View(webUser);
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("CatchesList");
        }

        private string ReturnHashCode(string loginAndSalt)
        {
            string hash = "";
            using (SHA1 sha1hash = SHA1.Create())
            {
                byte[] data = sha1hash.ComputeHash(Encoding.UTF8.GetBytes(loginAndSalt));
                StringBuilder sBuilder = new StringBuilder();
                for(int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }
                hash = sBuilder.ToString().ToUpper();
            }
            return hash;
        }

        public string findPlace(decimal x, decimal y)
        {
            string url = "https://geotree.ru/coordinates?lat=" + y.ToString() + "&lon=" + x.ToString() + "&z=13&mlat=" + y.ToString() + "&mlon=" + x.ToString();
            return url;
        }

        [HttpGet]
        public ActionResult CreateMouse()
        {
            return View();
        }


        public ActionResult MiceList(int catchID)
        {
            List<Mouse> mice = new List<Mouse>();
            using (var db = new MiceCatchEntities())
            {
                mice = db.Mouse.OrderByDescending(x => x.Catch.DateCatch).Where(x => x.Catch_ID == catchID).ToList();
                foreach (Mouse m in mice)
                {
                    m.Catch = db.Catch.Find(m.Catch_ID);
                    m.Types = db.Types.Find(m.Type_ID);
                    m.Pregnancy = db.Pregnancy.Find(m.Pregnancy_ID);
                }
            }
            return View(mice);
        }

        [HttpGet]

        public ActionResult MouseDetails(Guid mouseID)
        {
            Mouse mouse = new Mouse();
            using (var db = new MiceCatchEntities())
            {
                mouse = db.Mouse.Find(mouseID);
                mouse.Catch = db.Catch.Find(mouse.Catch_ID);
                mouse.Pregnancy = db.Pregnancy.Find(mouse.Pregnancy_ID);
                mouse.Types = db.Types.Find(mouse.Type_ID);
            }
            return View(mouse);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult CreateMouse(MouseVM newMouse, int cid)
        {
            if (ModelState.IsValid)
            {
                using (var db = new MiceCatchEntities())
                {
                    Mouse m = new Mouse()
                    {
                        ID_Mouse = Guid.NewGuid(),
                        Catch_ID = cid,
                        Type_ID = newMouse.Type_ID,
                        Pregnancy_ID = newMouse.Pregnancy_ID,
                        Gender = newMouse.Gender,
                        Age = newMouse.Age,
                        Embryos_Amount = newMouse.Embryos_Amount

                    };



                    db.Mouse.Add(m);
                    db.SaveChanges();
                }
                return RedirectToAction("CatchesList");
            }
            return View(newMouse);
        }
    }
}