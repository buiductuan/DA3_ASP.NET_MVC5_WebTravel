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
    public class SettingController : BaseController
    {
        // GET: Admin/Setting
        public ActionResult Index()
        {
            var model = new SystemConfigDAO().GetDetail(1);
            GetViewBag_Session();
            return View(model);
        }
        [HttpPost,ValidateInput(false)] 
        public ActionResult Update(SystemConfig entity)
        {
            if (ModelState.IsValid)
            {
                var res = new SystemConfigDAO().Update(entity);
                if (res)
                {
                    return RedirectToAction("Index","Setting");
                }
                else
                {
                    ModelState.AddModelError("","Have a error !!! Please check again");
                }
            }
            return RedirectToAction("Index", "Setting");
        }
        //[HttpPost]
        //public JsonResult Update(string strSys)
        //{
        //    var dao = new SystemConfigDAO();
        //    JavaScriptSerializer js = new JavaScriptSerializer();
        //    SystemConfig sys = js.Deserialize<SystemConfig>(strSys);
        //    var result = new SystemConfigDAO().Update(sys);

        //    return Json(new
        //    {
        //        status = result
        //    });
        //}
    }
}