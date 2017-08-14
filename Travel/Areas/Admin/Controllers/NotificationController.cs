using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.DataAccessObject;
using Model.EntityFramework;
using System.Web.Script.Serialization;
namespace Travel.Areas.Admin.Controllers
{
    public class NotificationController : BaseController
    {
        // GET: Admin/Notification
        public ActionResult Index()
        {
            GetViewBag_Session();
            var model = new SystemNotificationDAO().ListAll();
            return View(model);
        }

        [HttpPost]
        public JsonResult ViewDetail(int id)
        {
            string emailTitle = "";
            string emailContent = "";
            string pattern = "";
            string modelID ="";
            bool status = false;
            var model = new SystemNotificationDAO().ViewDetail(id);
            if (model.ID > 0)
            {
                modelID = model.ID.ToString();
                emailTitle = model.EmailTitle;
                emailContent = model.EmailContent;
                pattern = model.Pattern;
                status = true;
            }
            else { status = false; }

            return Json(new
            {
                id = modelID,
                emailTitle = emailTitle,
                emailContent = emailContent,
                pattern = pattern,
                status = status
            });
        }

        [HttpPost]
        public JsonResult Update(string entity)
        {
            GetViewBag_Session();
            var dao = new SystemNotificationDAO();
            JavaScriptSerializer js = new JavaScriptSerializer();
            SystemNotification model = js.Deserialize<SystemNotification>(entity);
            var result = new SystemNotificationDAO().Update(model);

            // Written log
            var logEntity = new Log_Admin();
            logEntity.createBy = ViewBag.Name_Session;
            logEntity.time = DateTime.Now;
            logEntity.action = "Cập nhật nội dung thông báo" + "<a href = \"/Admin/Notification\">Xem chi tiết</a>";
            logEntity.description = ViewBag.Name_Session + " cập nhật nội dung thông báo trong hệ thống";
            var wlog = new Log_AdminDAO().InsertLog(logEntity);

            return Json(new
            {
                status = result
            });
        }
    }
}