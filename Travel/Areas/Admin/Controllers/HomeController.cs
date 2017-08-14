using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Travel.Common;
using Model.DataAccessObject;

namespace Travel.Areas.Admin.Controllers
{
    public class HomeController : BaseController
    {
        // GET: Admin/Home
        public ActionResult Index(int page = 1, int pageSize = 5)
        {
            GetViewBag_Session();
            new UserDAO().TimeActiveUser(Convert.ToInt64(ViewBag.ID_Session));
            var model = new Log_AdminDAO().ListLog(page, pageSize);
            ViewBag.CountProduct = new ProductDAO().CountProduct();
            ViewBag.CountBlog = new NewsletterDAO().CountBlog();
            ViewBag.CountOrder = new OrderDAO().Count_Order();
            ViewBag.CountCustomer = new CustomerDAO().CountCustomer();
            return View(model);
        }
        [HttpGet]
        public ActionResult SignOut()
        {
            Session.Remove(CommonConstants.USER_SESSION);
            Session.Remove(CommonConstants.CREDENTIALS_SESSION);
           return RedirectToAction("Index","Login");
        }
        public ActionResult GetDataMorris()
        {
            var listData = new List<Entry>();
            var dao = new VisitorDAO().ListAll().Take(5);
            foreach(var item in dao)
            {
                listData.Add(new Entry(item.Year.ToString(), item.NumberOfVisit));
            }
            return Json(listData, JsonRequestBehavior.AllowGet);
        }
        public class Entry
        {
            public string year { get; set; }
            public long value { get; set; }
            public Entry(string year, long value)
            {
                this.year = year;
                this.value = value;
            }
        }

        public ActionResult _SideBarLeft()
        {
            GetViewBag_Session();
           ViewBag.newsFeedback = new FeedBackDAO().CountNewsFeedback();
            return View();
        }

        public ActionResult Activity(int page = 1, int pageSize = 15)
        {
            GetViewBag_Session();
            var model = new Log_AdminDAO().ListLog(page, pageSize);
            return View(model);
        }
    }
}