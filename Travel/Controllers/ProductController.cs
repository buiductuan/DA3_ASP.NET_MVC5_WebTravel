using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.EntityFramework;
using Model.DataAccessObject;
using Travel.Common;
using System.Xml.Linq;
namespace Travel.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index(int page = 1, int pageSize = 10)
        {
            //Set viewBag get product 
            ViewBag.Theme = new ThemeDAO().ViewDetail(1);
            ViewBag.MenuProduct = new MenuDAO().GetDetail(26);
            var model = new ProductDAO().GetListProductPaging(page, pageSize);
            return View(model);
        }

        //[OutputCache(CacheProfile = "Cache1DayForProduct")]
        public ActionResult Detail(long id)
        {
            ViewBag.SystemConfig = new SystemConfigDAO().GetDetail(1);
            ViewBag.ListTags = new ProductDAO().GetListTags(id);
            ViewBag.Menu = new MenuDAO().GetDetail(26);
            var menu = new MenuDAO().GetDetail(26);
            var dao = new ProductDAO();
            dao.UpdateViewCount(id);
            var model = dao.GetDetail(id);
            if (Session[CommonConstants.TOUR_REVIEWED_SESSION] == null)
            {
                //add tour_reviewed_session
                List<TourReviewed> list = new List<TourReviewed>();
                var tour = new TourReviewed();
                tour.ID = model.ID;
                tour.Name = model.Name;
                tour.MetaTile = model.MetaTitle;
                tour.menu = menu.Link;
                list.Add(tour);
                Session.Add(CommonConstants.TOUR_REVIEWED_SESSION, list);
            }
            else
            {
                var list = (List<TourReviewed>)Session[CommonConstants.TOUR_REVIEWED_SESSION];
                if (Check_Tour_Reviewed(model.ID) == false)
                {
                    var tour = new TourReviewed();
                    tour.ID = model.ID;
                    tour.Name = model.Name;
                    tour.MetaTile = model.MetaTitle;
                    tour.menu = menu.Link;
                    list.Add(tour);
                }
            }
            return View(model);
        }
        public bool Check_Tour_Reviewed(long id)
        {
            var list = (List<TourReviewed>)Session[CommonConstants.TOUR_REVIEWED_SESSION];
            foreach (var item in list)
            {
                if (item.ID == id) return true;
            }
            return false;
        }

        #region Ajax
        public JsonResult GetImagesBindToSlideShow(long id)
        {
            var model = new ProductDAO().GetDetail(id);
            var images = model.MoreImages;
            XElement xElement = XElement.Parse(images);
            List<string> listImages = new List<string>();

            foreach (var item in xElement.Elements())
            {
                listImages.Add(item.Value);
            }

            return Json(new
            {
                data = listImages
            }, JsonRequestBehavior.AllowGet);

        }
        #endregion
    }
}