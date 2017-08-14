using Model.DataAccessObject;
using Model.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Travel.Common;
using Travel.Models;
using Facebook;
using System.Configuration;
namespace Travel.Controllers
{
    public class RegistrationController : Controller
    {
        private Uri RedirectUri
        {
            get
            {
                var uriBuilder = new UriBuilder(Request.Url);
                uriBuilder.Query = null;
                uriBuilder.Fragment = null;
                uriBuilder.Path = Url.Action("FacebookCallBack");
                return uriBuilder.Uri;
            }
        }
        // GET: Registration
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult SignUp()
        {
            return View();
        }
        [HttpGet]
        public ActionResult SignIn()
        {
            return View();
        }
        public ActionResult LoginFacebook()
        {
            var fb = new FacebookClient();
            var login = fb.GetLoginUrl(new
            {
                client_id = ConfigurationManager.AppSettings["FbAppID"].ToString(),
                client_secret = ConfigurationManager.AppSettings["FbAppSecret"].ToString(),
                redirect_uri = RedirectUri.AbsoluteUri,
                response_type = "code",
                scope = "email"
            });
            return Redirect(login.AbsoluteUri);

        }
        public ActionResult FacebookCallBack(string code)
        {
            var fb = new FacebookClient();
            dynamic result = fb.Post("oauth/access_token", new
            {
                client_id = ConfigurationManager.AppSettings["FbAppID"].ToString(),
                client_secret = ConfigurationManager.AppSettings["FbAppSecret"].ToString(),
                redirect_uri = RedirectUri.AbsoluteUri,
                code = code
            });
            var accessToken = result.access_token;

            if (!string.IsNullOrEmpty(accessToken))
            {
                fb.AccessToken = accessToken;
                dynamic me = fb.Get("me?fields=first_name,middle_name,last_name,id,email");
                string profile = string.Format("https://graph.facebook.com/{0}/picture", me.id);
                string email = me.email;
                string username = me.email;
                string firstname = me.first_name;
                string midname = me.middle_name;
                string lastname = me.last_name;
                var customer = new Customer();
                customer.Email = email;
                customer.UserName = username;
                customer.Name = firstname + " " + midname + " " + lastname;
                customer.Image = profile;
                var resInsert = new CustomerDAO().InsertForFacebook(customer);
                if(resInsert > 0)
                {
                    var customerLogin = new CustomerLogin();
                    customerLogin.CustomerID = customer.ID;
                    customerLogin.Name = customer.Name;
                    customerLogin.Image = customer.Image;
                    Session.Add(CommonConstants.CUSTOMER_SESSION, customerLogin);
                }
                else
                {
                    var customerLogin = new CustomerLogin();
                    var customerGet = new CustomerDAO().GetCustomerByUserName(customer.UserName);
                    customerLogin.CustomerID = customerGet.ID;
                    customerLogin.Name = customerGet.Name;
                    customerLogin.Image = customerGet.Image;
                    Session.Add(CommonConstants.CUSTOMER_SESSION, customerLogin);
                }
            }
            return Redirect("/");
        }
        [HttpPost]
        public ActionResult SignIn(SignInModel entity)
        {
            if (ModelState.IsValid)
            {
                var model = new CustomerDAO().Check_SignIn(entity.UserName, Encrypt.EncryptText(entity.Password, true));
                if (model == -1)
                {
                    ModelState.AddModelError("", "Tài khoản không tồn tại");
                }
                else if (model == 0)
                {
                    ModelState.AddModelError("", "Mật khẩu đăng nhập sai");
                }
                else if (model == 1)
                {
                    var customerLogin = new CustomerLogin();
                    var customer = new CustomerDAO().GetCustomerByUserName(entity.UserName);
                    customerLogin.CustomerID = customer.ID;
                    customerLogin.Name = customer.Name;
                    customerLogin.Image = customer.Image;

                    //add session
                    Session.Add(CommonConstants.CUSTOMER_SESSION, customerLogin);
                    return RedirectToAction("Index", "Home");
                }
            }
            return View("SignIn");
        }
        [HttpPost]
        public JsonResult SignUp(string data)
        {
            bool status = false;
            JavaScriptSerializer js = new JavaScriptSerializer();
            Customer entity = js.Deserialize<Customer>(data);

            //Encypt to MD5
            entity.Password = Travel.Common.Encrypt.EncryptText(entity.Password, true);
            var model = new CustomerDAO().Insert(entity);

            if (model > 0)
            {
                status = true;
            }

            return Json(new
            {
                status = status
            });
        }
        [HttpPost]
        public JsonResult CheckUserName(string username)
        {
            var res = new CustomerDAO().Check_UserName(username);

            return Json(new
            {
                status = res
            });
        }
        [HttpPost]
        public JsonResult CheckEmail(string email)
        {
            var res = new CustomerDAO().Check_Email(email);

            return Json(new
            {
                status = res
            });
        }

        public ActionResult SignOut()
        {
            Session.Remove(CommonConstants.CUSTOMER_SESSION);
            return RedirectToAction("Index", "Home");
        }
    }
}