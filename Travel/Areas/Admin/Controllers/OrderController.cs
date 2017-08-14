using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.EntityFramework;
using Model.DataAccessObject;
namespace Travel.Areas.Admin.Controllers
{
    public class OrderController : BaseController
    {
        // GET: Admin/Order
        public ActionResult Index(string q, int page=1,int pageSize =5)
        {
            GetViewBag_Session();
            var dao = new OrderDAO();
            var model = dao.ListPaging(q, page, pageSize);
            ViewBag.QuerySearch = q;
            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            GetViewBag_Session();
            GetProduct();
            return View();
        }

        [HttpPost]
        public ActionResult Create(Order entity)
        {
            GetViewBag_Session();
            if (ModelState.IsValid)
            {
                entity.CreateDate = DateTime.Now;
                var model = new OrderDAO().Insert(entity);

                // Written log
                var logEntity = new Log_Admin();
                logEntity.createBy = ViewBag.Name_Session;
                logEntity.time = DateTime.Now;
                logEntity.action = "Thêm mới đơn hàng " + "<a href = \"/Admin/Order/Update/" + entity.ID + "\">" + entity.ID + "</a>";
                logEntity.description = ViewBag.Name_Session + " thêm mới đơn hàng vào hệ thống";
                var wlog = new Log_AdminDAO().InsertLog(logEntity);

                if (model > 0)
                {
                    return RedirectToAction("Index", "Order");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm thành công đơn hàng vào hệ thống");
                }
            }
            GetProduct();
            return View("Create");
        }

        public void GetProduct(long? selectedID = null)
        {
            var dao = new ProductDAO();
            ViewBag.ProductID = new SelectList(dao.ListAllDropDown(), "ID", "Name", selectedID);
        }
    }
}