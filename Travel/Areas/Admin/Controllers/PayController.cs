using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.EntityFramework;
using Model.DataAccessObject;
using System.Web.Script.Serialization;
namespace Travel.Areas.Admin.Controllers
{
    public class PayController : BaseController
    {
        // GET: Admin/Pay
        public ActionResult Index()
        {
            GetViewBag_Session();
            ViewBag.System = new SystemConfigDAO().GetDetail(1);
            var model = new PayDAO().ViewDetail();
            return View(model);
        }

       [HttpPost]
       public ActionResult Update(Pay entity)
        {
            if (ModelState.IsValid)
            {
                var res = new PayDAO().Update(entity);
                if (res)
                {
                    return RedirectToAction("Index", "Pay");
                }
                else
                {
                    ModelState.AddModelError("", "Không thể cập nhật cấu hình thanh toán");
                }
            }
            return RedirectToAction("Index", "Pay");
        }

        #region Ajax
        //load data 
        [HttpGet]
        public JsonResult GetPay(int id)
        {
            bool status = false;
            var model = new PayDAO().GetDetail(id);

            if (model != null)
            {
                status = true;
            }

            return Json(new
            {
                status = status,
                data = model
            },JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult SaveData(string data)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            Pay model = js.Deserialize<Pay>(data);
            var res = new PayDAO().Update(model);

            return Json(new
            {
                status = res
            });
        }
        #endregion
    }
}