using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.EntityFramework;
using Model.DataAccessObject;
using System.IO;

namespace Travel.Areas.Admin.Controllers
{
    public class ThemeController : BaseController
    {
        // GET: Admin/Theme
        public ActionResult Index()
        {
            GetViewBag_Session();
            return View();
        }
        [HttpGet]
        public ActionResult Editor()
        {
            GetViewBag_Session();
            var model = new ThemeDAO().ViewDetail(1);
            return View(model);
        }
        [HttpPost]
        public ActionResult Editor(Theme entity ,HttpPostedFileBase file)
        {
            GetViewBag_Session();
            try
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        if (file.ContentLength > 0 || file.FileName != null)
                        {
                            if (file.ContentLength < 3000000)
                            {
                                if (file.ContentType == "image/png" || file.ContentType == "image/jpeg")
                                {
                                    string path = Path.Combine(Server.MapPath("~/images/images/Slides/"), Path.GetFileName(file.FileName));
                                    file.SaveAs(path);
                                    entity.Header_image = file.FileName;
                                }
                                else
                                {
                                    ModelState.AddModelError("", "Please choose file .png or .jpg");
                                }
                            }
                            else
                            {
                                ModelState.AddModelError("", "Image less than 3MB");
                            }
                        }
                    }
                    catch (Exception)
                    {

                    }
                    new ThemeDAO().Update(entity);

                    // Written log
                    var logEntity = new Log_Admin();
                    logEntity.createBy = ViewBag.Name_Session;
                    logEntity.time = DateTime.Now;
                    logEntity.action = "Thay đổi giao diện " + "<a href = \"/Admin/Theme\">Xem chi tiết</a>";
                    logEntity.description = ViewBag.Name_Session + " thay đổi giao diện trong hệ thống";
                    var wlog = new Log_AdminDAO().InsertLog(logEntity);
                }
                return View("Index");
            }
            catch (Exception)
            {
                return View("Index");
            }
        }
    }

}