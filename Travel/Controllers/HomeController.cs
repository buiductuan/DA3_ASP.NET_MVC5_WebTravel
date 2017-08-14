using Model.DataAccessObject;
using Model.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Travel.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            //Get product in home pages
            var themes = new ThemeDAO().ViewDetail(1);
            ViewBag.ProductHome = new ProductDAO().GetListProductAtHome().Take(themes.Home_Number_P);
            ViewBag.NewsHome = new NewsletterDAO().GetNewsShowOnHome().Take(themes.Home_Number_Blog);
            ViewBag.MenuProduct = new MenuDAO().GetDetail(26);
            ViewBag.MenuNews = new MenuDAO().GetDetail(30);
            ViewBag.ListTag = new ProductDAO().GetTagInProduct();
            var visit = new VisitorDAO().ListAll();
            if (visit.Count <= 0)
            {
                var entity = new Visitor();
                entity.Year = DateTime.Now.Year;
                entity.NumberOfVisit = 0;
                var dao = new VisitorDAO().Insert(entity);
            }
            else
            {
                var upVisit = new VisitorDAO();
                upVisit.UpdateVisitor(DateTime.Now.Year);
            }
            ViewBag.Theme = new ThemeDAO().ViewDetail(1);
            return View();
        }
        public ActionResult FeedBack()
        {
            ViewBag.FeedBack = new FeedBackDAO().GetList().Take(3);
            ViewBag.Theme = new ThemeDAO().ViewDetail(1);
            return View();
        }
        public ActionResult Header()
        {
            ViewBag.Theme = new ThemeDAO().ViewDetail(1);
            ViewBag.place = new SelectList(new ProductDAO().GetPlaceOfDeparture());
            ViewBag.pid = new SelectList(new ProductCategoryDAO().GetNameProductCategory());
            return View();
        }
        [ChildActionOnly]
        public ActionResult MainMenu()
        {
            //Get property config
            ViewBag.SystemConfig = new SystemConfigDAO().GetDetail(1);
            var model = new MenuDAO().ListMenu(1);
            return PartialView(model);
        }
        [ChildActionOnly]
        public ActionResult FooterMenu()
        {
            //Get property config
            ViewBag.SystemConfig = new SystemConfigDAO().GetDetail(1);
            ViewBag.Theme = new ThemeDAO().ViewDetail(1);
            var model = new MenuDAO().ListMenu(1);
            return PartialView(model);
        }

        //Hash tags
        public ActionResult HashTagNews(string tagID)
        {
            var model = new NewsletterDAO().GetListNewByTags(tagID);

            ViewBag.Menu = new MenuDAO().GetDetail(30);
            ViewBag.tagID = new NewsletterDAO().GetNameTagByID(tagID);
            return View(model);
        }
        public ActionResult HashTagProduct(string tagID)
        {
            var model = new ProductDAO().GetListProductByTags(tagID);

            ViewBag.Menu = new MenuDAO().GetDetail(26);
            ViewBag.tagID = new NewsletterDAO().GetNameTagByID(tagID);
            return View(model);
        }

        //Search 
        public ActionResult Search(string place, string pid, DateTime date_start)
        {
            ViewBag.Menu = new MenuDAO().GetDetail(26);
            var model = new ProductDAO().SearchProduct(place, pid, date_start);
            return View(model);
        }
    }
}