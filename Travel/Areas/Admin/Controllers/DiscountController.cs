using Model.DataAccessObject;
using Model.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Travel.Areas.Admin.Controllers
{
    public class DiscountController : BaseController
    {
        // GET: Admin/Discount
        public ActionResult Index(string q, int page=1,int pageSize=5)
        {
            GetViewBag_Session();
            var dao = new DiscountDAO();
            var model = dao.ListAll(q, page, pageSize);
            ViewBag.QuerySearch = q;
            return View(model);
        }
        [HttpGet]
        public ActionResult Create()
        {
            GetViewBag_Session();
            GetProductCategory();
            ViewBag.System = new SystemConfigDAO().GetDetail(1);
            return View();
        }
        [HttpPost]
        public ActionResult Create(Discount entity)
        {
            GetViewBag_Session();
            if(entity.Dispercent > 0 && entity.Dispercent <= 100)
            {
                entity.Disprice = 0;
            }
            else
            {
                if(entity.Disprice > 0)
                {
                    entity.Dispercent = 0;
                }
            }
            if (ModelState.IsValid)
            {
                var res = new DiscountDAO().Insert(entity);
                if (res)
                {
                    return RedirectToAction("Index", "Discount");
                }
                else
                {
                    ModelState.AddModelError("", "Có lỗi xảy ra vui lòng kiểm tra lại");
                }
            }
            GetProductCategory();
            return View("Create");
        }
        [HttpDelete]
        public ActionResult Delete(string id)
        {
            new DiscountDAO().Delete(id);
            return RedirectToAction("Index", "Discount");
        }

        public void GetProductCategory(long? selectedID = null)
        {
            var dao = new ProductCategoryDAO();
            ViewBag.ProductCategoryID = new SelectList(dao.ListAllToDropDown(), "ID", "Name", selectedID);
        }

        [HttpPost]
        public JsonResult ChangeStatus(string id)
        {
            var model = new DiscountDAO().ChangeStatus(id);

            return Json(new
            {
                status = model
            });
        }
    }
}