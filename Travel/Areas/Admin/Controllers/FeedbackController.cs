using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.DataAccessObject;
namespace Travel.Areas.Admin.Controllers
{
    public class FeedbackController : BaseController
    {
        // GET: Admin/Feedback
        public ActionResult Index(string q, int page = 1, int pageSize = 5)
        {
            GetViewBag_Session();
            var dao = new FeedBackDAO();
            var model = dao.ListAll(q, page, pageSize);
            ViewBag.QueryString = q;
            return View(model);
        }

        [HttpPost]
        public JsonResult ViewDetail(int id)
        {
            bool status = false;
            var model = new FeedBackDAO().ViewDetail(id);
            if(model.ID > 0)
            {
                status = true;
            }
            return Json(new
            {
                data = model,
                status = status
            });
        }

        [HttpPost]
        public JsonResult ChangeRead(int id)
        {
            var res = new FeedBackDAO().ChangeRead(id);
            return Json(new
            {
                status = res
            });
        }
        [HttpPost]
        public JsonResult ChangeShow(int id)
        {
            var res = new FeedBackDAO().ChangeShow(id);
            return Json(new
            {
                status = res
            });
        }
        [HttpPost]
        public JsonResult Delete(int id)
        {
            var res = new FeedBackDAO().Delete(id);
            return Json(new
            {
                status = res
            });
        }
    }
}