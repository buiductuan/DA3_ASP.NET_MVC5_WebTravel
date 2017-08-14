using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.DataAccessObject;
using Model.EntityFramework;
using System.Web.Script.Serialization;
namespace Travel.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        [OutputCache(CacheProfile = "Cache1Day")]
        public ActionResult Index()
        {
            ViewBag.SystemConfig = new SystemConfigDAO().GetDetail(1);
            return View();
        }

        [HttpPost]
        public JsonResult SendFeedAjax(string feed)
        {
            bool status = false;
            JavaScriptSerializer js = new JavaScriptSerializer();
            FeedBack fe = js.Deserialize<FeedBack>(feed);
            var dao = new FeedBackDAO().Insert(fe);
            if(fe.ID > 0)
            {
                status = true;
            }
            return Json(new
            {
                status = status
            });
        }
    }
}