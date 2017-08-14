using Model.DataAccessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Travel.Areas.Admin.Models;
using Travel.Common;

namespace Travel.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Admin/Login
        public ActionResult Index()
        {
            return View();
        }

        
        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDAO();
                int result = dao.CheckLogin(model.UserName, Encrypt.EncryptText(model.Password, true));
                if (result == -1)
                {
                    ModelState.AddModelError("", "Tài khoản không tồn tại ");
                }
                else
                {
                    if (result == 0)
                    {
                        ModelState.AddModelError("", "Tài khoản của bạn đã bị khóa");
                    }
                    else
                    {
                        if (result == -2)
                        {
                            ModelState.AddModelError("", "Mật khẩu không đúng");
                        }
                        else
                        {
                            if (result == 1)
                            {
                                var user = dao.GetById(model.UserName);
                                var userSession = new UserLogin();
                                userSession.UserName = user.UserName;
                                userSession.Name = user.Name;
                                userSession.Image = user.Image;
                                userSession.UserID = user.ID;
                                userSession.UserGroup = user.GroupID;
                                var listCredential = new UserDAO().GetListCredential(user.UserName);

                                //Add session
                                Session.Add(CommonConstants.CREDENTIALS_SESSION, listCredential);
                                Session.Add(CommonConstants.USER_SESSION, userSession);

                                return RedirectToAction("Index", "Home");
                            }
                        }
                    }
                }
            }
            return View("Index");
        }

    }
}