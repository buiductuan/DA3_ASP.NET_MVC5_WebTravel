using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.EntityFramework;
using Model.DataAccessObject;

namespace Travel.Areas.Admin.Controllers
{
    public class DomainController : BaseController
    {
        // GET: Admin/Domain
        public ActionResult Index()
        {
            GetViewBag_Session();
            return View();
        }

        [HttpGet]
        public JsonResult LoadData()
        {
            List<Domain> lst = new List<Domain>();
            lst = new DomainDAO().ListAll();
            return Json(new
            {
                data = lst,
                status = true
            },JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult Delete(int id)
        {
            var result = new DomainDAO().Delete(id);
            return Json(new
            {
                status = result
            });
        }
    }
}