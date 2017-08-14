using Model.DataAccessObject;
using Model.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Travel.Areas.Admin.Controllers
{
    public class MenuController : BaseController
    {
        // GET: Admin/Menu
        public ActionResult Index(int page=1,int pageSize=5)
        {
            GetViewBag_Session();
            var dao = new MenuDAO();
            var model = dao.ListAll(page, pageSize);
            return View(model);
        }
        [HttpGet]
        public ActionResult CreateMenu()
        {
            GetViewBag_Session();
            ListMenuType();
            return View();
        }
        public ActionResult UpdateMenu(int id)
        {
            GetViewBag_Session();
            var dao = new MenuDAO().GetDetail(id);
            ListMenuType(dao.ID);
            return View(dao);
        }
        [HttpPost]
        public ActionResult CreateMenu(Menu mn)
        {
            GetViewBag_Session();
            if (ModelState.IsValid)
            {
                var dao = new MenuDAO();
                int id = dao.Insert(mn);
                // Written log
                var logEntity = new Log_Admin();
                logEntity.createBy = ViewBag.Name_Session;
                logEntity.time = DateTime.Now;
                logEntity.action = "Thêm mới menu " + "<a href = \"/Admin/Menu/Update/" + mn.ID + "\">" + mn.ID + "</a>";
                logEntity.description = ViewBag.Name_Session + " thêm mới menu vào hệ thống";
                var wlog = new Log_AdminDAO().InsertLog(logEntity);

                if (id > 0)
                {
                    return RedirectToAction("Index", "Menu");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm menu thành công");
                }
            }
            ListMenuType();
            return View("CreateMenu");
        }
        [HttpPost]
        public ActionResult UpdateMenu(Menu mn)
        {
            GetViewBag_Session();
            if (ModelState.IsValid)
            {
                var dao = new MenuDAO();
                var id = dao.Update(mn);

                // Written log
                var logEntity = new Log_Admin();
                logEntity.createBy = ViewBag.Name_Session;
                logEntity.time = DateTime.Now;
                logEntity.action = "Cập nhật menu " + "<a href = \"/Admin/Menu/Update/" + mn.ID + "\">" + mn.ID + "</a>";
                logEntity.description = ViewBag.Name_Session + " cập nhật menu trong hệ thống";
                var wlog = new Log_AdminDAO().InsertLog(logEntity);

                if (id)
                {
                    return RedirectToAction("Index", "Menu");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm menu thành công");
                }
            }
            ListMenuType(mn.TypeID);
            return View("UpdateMenu");
        }

        [HttpPost]
        public JsonResult CreateMenuType(string strMenuType)
        {
            var dao = new MenuTypeDAO();
            JavaScriptSerializer js = new JavaScriptSerializer();
            MenuType menu = js.Deserialize<MenuType>(strMenuType);
            bool status = false;
            string message = string.Empty;
            if (dao.Check_Name(menu.Name) == false)
            {
                dao.Insert(menu);
                message = "Thêm loại menu " + menu.Name + " thành công";
                status = true;
            }
            else
            {
                status = false;
                message = "Tên loại menu "+menu.Name+" đã tồn tại";
            }
            return Json(new
            {
                status = status,
                message=message
            });
        }

        public void ListMenuType(long? selectedID = null)
        {
            var dao = new MenuTypeDAO();
            ViewBag.TypeID = new SelectList(dao.BindDropDown(), "ID", "Name", selectedID);
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new MenuDAO().Delete(id);
            return RedirectToAction("Index", "Menu");
        }
        
        [HttpPost]
        public JsonResult ChangeStatus(int id)
        {
            var result = new MenuDAO().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }
        [HttpPost]
        public JsonResult DeleteAjax(int id)
        {
            var result = new MenuDAO().Delete(id);
            return Json(new
            {
                status=result
            });
        }
    }
}