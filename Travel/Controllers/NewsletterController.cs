using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.DataAccessObject;
using Model.EntityFramework;
namespace Travel.Controllers
{
    public class NewsletterController : Controller
    {
        // GET: Newsletter
        public ActionResult Index(int page =1 , int pageSize=10)
        {

            var model = new NewsletterDAO().GetListInPage(page, pageSize);
            ViewBag.Theme = new ThemeDAO().ViewDetail(1);
            ViewBag.System = new SystemConfigDAO().GetDetail(1);
            return View(model);
        }

       // [OutputCache(CacheProfile = "Cache1DayForNewsletter")]
        public ActionResult Detail(long id)
        {
            var dao = new NewsletterDAO();
            //Get News
            ViewBag.GetNews = dao.GetNews().Take(5);
            ViewBag.GetHotNews = dao.GetHotNews().Take(5);

            //Get systemConfig
            ViewBag.SystemConfig = new SystemConfigDAO().GetDetail(1);

            //Get tags 
            ViewBag.ListTags = new NewsletterDAO().GetListTagInNewsletter(id);

            var model = dao.GetDetail(id);
            dao.ViewUp(id);
            return View(model);
        }

        #region Ajax emoij
        [HttpPost]
        public JsonResult Up_Emoij(int id , string text_emoij)
        {
            var res = new NewsletterDAO().Update_emoij(id, text_emoij);

            return Json(new
            {
                status = res
            });
        }
        #endregion
    }
}