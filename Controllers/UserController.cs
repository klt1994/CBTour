using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using Ninject;
using AutoMapper;
using CBTour.Models;
using System.Text.RegularExpressions;
using System.IO;

namespace CBTour.Controllers
{
    public class UserController : Controller
    {
        private ISessionAccessor SessAcc;
        private IUserLogic UserLogic;
        private static Hash PassHash = new Hash();

        public UserController(IUserLogic logic, UserLogic userLogic, SessionAccessor Session)
        {
            UserLogic = logic;
            UserLogic = userLogic;
            SessAcc = Session;
        }
        // GET: User
        public ActionResult UserIndex()
        {
            return View();
        }
        public ActionResult ModIndex(string email)
        {
            return View(Mapper.Map<UserVM>(UserLogic.GetUserDetail(email)));
        }
        public ActionResult AdminIndex()
        {
            return View();
        }

        // GET: User/Details/5
        public ActionResult ViewUserDetails(string email)
        {
            UserVM model = Mapper.Map<UserVM>(UserLogic.GetUserDetail(email));
            return View(model);
        }

        public ActionResult ViewAllUsers()
        {
            List<UserVM> Model = Mapper.Map<List<UserVM>>(UserLogic.GetUserList());
            return View(Model);
        }

        // GET: User/Create
        public ActionResult CreateUser()
        {
            return View();
        }

        // POST: User/Create
        [HttpPost]
        public ActionResult CreateUser(UserVM model)
        {
            try
            {

                // TODO: Add insert logic here
                UserLogic.RegisterUser(model.Email, PassHash.GetHash(model.Password), model.Role);
                if ((string)Session["Role"] == "Admin")
                {
                    return RedirectToAction("ViewAllUsers");
                }
                else
                {
                    Login(model);
                    return RedirectToAction("Index", "Home");
                }

            }
            catch
            {
                return View();
            }
        }

        // GET: User/Edit/5
        public ActionResult EditUserInfo(string email)
        {
            return View(Mapper.Map<UserVM>(UserLogic.GetUserDetail(email)));
        }

        // POST: User/Edit/5
        [HttpPost]
        public ActionResult EditUserInfo(int id, string email, string password, string role, string oldPass, string confirmPass)
        {
            try
            {
                // TODO: Add update logic here
                if ((string)Session["Role"] != "Admin")
                {
                    oldPass = PassHash.GetHash(oldPass);

                    if (UserLogic.GetUserDetail(email).Password == oldPass)
                    {
                        UserLogic.UpdateUserInfo(id, email, PassHash.GetHash(password), role);
                    }
                }
                else if ((string)Session["Role"] == "Admin")
                {
                    UserLogic.UpdateUserInfo(id, email, PassHash.GetHash(password), role);
                }
                else
                {
                    ModelState.AddModelError("", "The Old Password doesn't match...");
                }

                if ((string)Session["Role"] == "Mod")
                {
                    return RedirectToAction("ViewUserDetails", new { Email = (string)Session["Email"] });
                }
                else if ((string)Session["Role"] == "Admin")
                {
                    return RedirectToAction("ViewAllUsers");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            catch
            {
                return View();
            }
        }

        public ActionResult EditUserEmail(string email)
        {
            return View(Mapper.Map<UserVM>(UserLogic.GetUserDetail(email)));
        }

        [HttpPost]
        public ActionResult EditUserEmail(UserVM model)
        {
            try
            {
                // TODO: Add update logic here
                UserVM user = Mapper.Map<UserVM>(UserLogic.VerifyRole(model.Email, model.Password));
                if ((string)Session["Role"] != "Admin")
                {
                    UserLogic.UpdateUserInfo(model.ID, model.Email, PassHash.GetHash(model.Password), model.Role);
                }
                else if ((string)Session["Role"] == "Admin")
                {
                    UserLogic.UpdateUserInfo(model.ID, model.Email, PassHash.GetHash(model.Password), model.Role);
                }
                else
                {
                    ModelState.AddModelError("", "The Old Password doesn't match...");
                }

                if ((string)Session["Role"] == "Mod")
                {
                    return RedirectToAction("ViewUserDetails", new { Email = (string)Session["Email"] });
                }
                else if ((string)Session["Role"] == "Admin")
                {
                    return RedirectToAction("ViewAllUsers");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            catch
            {
                return View();
            }
        }

        public ActionResult EditUserPassword(string email)
        {
            UserVM model = Mapper.Map<UserVM>(UserLogic.GetUserDetail(email));
            model.Password = "";
            return View(model);
        }

        [HttpPost]
        public ActionResult EditUserPassword(UserVM model)
        {
            try
            {
                // TODO: Add update logic here
                UserVM user = Mapper.Map<UserVM>(UserLogic.VerifyRole(model.Email, model.Password));
                if ((string)Session["Role"] != "Admin")
                {
                    if (UserLogic.GetUserDetail(model.Email).Password == PassHash.GetHash(model.OldPass))
                    {
                        UserLogic.UpdateUserInfo(model.ID, model.Email, PassHash.GetHash(model.Password), model.Role);
                    }
                }
                else if ((string)Session["Role"] == "Admin")
                {
                    UserLogic.UpdateUserInfo(model.ID, model.Email, PassHash.GetHash(model.Password), model.Role);
                }
                else
                {
                    ModelState.AddModelError("", "The Old Password doesn't match...");
                }

                if ((string)Session["Role"] == "Mod")
                {
                    return RedirectToAction("ViewUserDetails", new { Email = (string)Session["Email"] });
                }
                else if ((string)Session["Role"] == "Admin")
                {
                    return RedirectToAction("ViewAllUsers");
                }
                else
                {
                    return RedirectToAction("UserIndex");
                }
            }
            catch
            {
                return View();
            }
        }

        public ActionResult EditUserRole(string email)
        {
            return View(Mapper.Map<UserVM>(UserLogic.GetUserDetail(email)));
        }

        [HttpPost]
        public ActionResult EditUserRole(UserVM model)
        {
            try
            {
                // TODO: Add update logic here
                UserLogic.UpdateUserInfo(model.ID, model.Email, PassHash.GetHash(model.Password), model.Role);
                UserVM user = Mapper.Map<UserVM>(UserLogic.VerifyRole(model.Email, model.Password));
                if ((string)Session["Role"] == "Admin")
                {
                    return RedirectToAction("ViewAllUsers");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            catch
            {
                return View();
            }
        }

        //// GET: User/Delete/5
        public ActionResult DeleteUser(string email)
        {
            UserVM model = Mapper.Map<UserVM>(UserLogic.GetUserDetail(email));
            return View(model);
        }

        // POST: User/Delete/5
        [HttpPost]
        public ActionResult DeleteUser(UserVM model)
        {
            try
            {
                // TODO: Add delete logic here
                UserLogic.DeleteUser(model.Email);
                return RedirectToAction("ViewAllUsers");
            }
            catch
            {
                return View();
            }
        }
        // GET: User/Login
        public ActionResult Login()
        {
            return View();
        }

        // POST: User/Login
        [HttpPost]
        public ActionResult Login(UserVM user)
        {

            user = Mapper.Map<UserVM>(UserLogic.VerifyRole(user.Email, PassHash.GetHash(user.Password)));
            SessAcc.SetSessionAccessor(user);
            if (user.Role != null)
            {
                if (user.Role == "User")
                {
                    return RedirectToAction("UserIndex");
                }
                else if (user.Role == "Mod")
                {
                    return RedirectToAction("ModIndex", new { email = user.Email });
                }
                else if (user.Role == "Admin")
                {
                    return RedirectToAction("AdminIndex");
                }
                else
                {
                    ModelState.AddModelError("", "Login data is incorrect!");
                }
            }
            return View();

        }

        [HttpPost]
        public ActionResult SetImage(UserVM model, HttpPostedFileBase file)
        {
            string image = Path.GetFileName(file.FileName);
            string path = Path.Combine(HttpContext.Server.MapPath("~/Images/"
                + image));
            file.SaveAs(path);
            model.ImagePath = image;

            if ((string)Session["Role"] == "Mod")
            {
                return RedirectToAction("ViewUserDetails", new { Email = (string)Session["Email"] });
            }
            else if ((string)Session["Role"] == "Admin")
            {
                return RedirectToAction("ViewAllUsers");
            }
            else
            {
                return RedirectToAction("UserIndex");
            }
        }

        public ActionResult Logout()
        {
            SessAcc.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}