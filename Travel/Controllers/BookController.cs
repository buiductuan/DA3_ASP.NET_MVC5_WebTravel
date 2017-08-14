using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.DataAccessObject;
using System.IO;
using Model.EntityFramework;
using System.Web.Script.Serialization;

namespace Travel.Controllers
{
    public class BookController : Controller
    {
        // GET: Book
        public ActionResult Index(long id)
        {
            ViewBag.System = new SystemConfigDAO().GetDetail(1);
            ViewBag.Product = new ProductDAO().GetDetail(id);
            return View();
        }

        [HttpPost]
        public ActionResult BookingPDF()
        {
            string path = Path.Combine(@"~/Assets/Client/pdf/", "quytrinhbooktour");
            return File(path, "application/pdf");
        }


        #region Ajax
        [HttpPost]
        public JsonResult CreateOrder(string data)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            Order od = js.Deserialize<Order>(data);


            var res = new OrderDAO().Insert(od);
            if (res > 0)
            {
                return Json(new
                {
                    status = true
                });
            }
            else
            {

                return Json(new
                {
                    status = false
                });


            }

        }
        #endregion
    }
}