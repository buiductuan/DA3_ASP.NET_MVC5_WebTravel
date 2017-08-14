using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.DataAccessObject;
using Model.EntityFramework;
using System.IO;
using System.Configuration;

namespace Travel.Areas.Admin.Controllers
{
    public class NewsletterController : BaseController
    {
        // GET: Admin/Newsletter
        public ActionResult Index(string q, int page = 1, int pageSize = 5)
        {
            GetViewBag_Session();
            var dao = new NewsletterDAO();
            var model = dao.ListAll(q, page, pageSize);
            ViewBag.QuerySearch = q;
            return View(model);
        }
        [HttpGet]
        public ActionResult Create()
        {
            GetViewBag_Session();
            GetNewsCategory();
            return View();
        }
        public ActionResult Update(long id)
        {
            GetViewBag_Session();
            var dao = new NewsletterDAO();
            var model = dao.GetDetail(id);
            GetNewsCategory(model.CategoryID);
            return View(model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult Create(Newsletter news,HttpPostedFileBase file)
        {
            GetViewBag_Session();
            if (ModelState.IsValid)
            {
                var dao = new NewsletterDAO();
                try
                {
                    if (file.FileName != null && file.ContentLength > 0)
                    {
                        if (file.ContentType == "image/jpeg" || file.ContentType == "image/png")
                        {
                            if (file.ContentLength < 2000000)
                            {
                                string path = Path.Combine(Server.MapPath("~/images/images/Newsletter/"), Path.GetFileName(file.FileName));
                                file.SaveAs(path);
                                news.Image = file.FileName;
                            }
                            else
                            {
                                ModelState.AddModelError("", "Ảnh đại diện phải nhỏ hơn 2MB");
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("", "Ảnh phải có định dạng *png hoặc *jpg");
                        }
                    }
                }
                catch { }

                if (string.IsNullOrEmpty(news.CreatedBy))
                {
                    news.CreatedBy = ViewBag.Name_Session;
                }
                news.CreatedDate = DateTime.Now;
                var metaTitle = Common.Tool.ChangeText(news.MetaTitle);
                news.MetaTitle = metaTitle;
                long id = dao.InsertNewsletter(news);

                // Written log
                var logEntity = new Log_Admin();
                logEntity.createBy = ViewBag.Name_Session;
                logEntity.time = DateTime.Now;
                logEntity.action = "Thêm mới tin tức " + "<a href = \"/Admin/Newsletter/Update/" + news.ID + "\">" + news.ID + "</a>";
                logEntity.description = ViewBag.Name_Session + " thêm mới tin vào hệ thống";
                var wlog = new Log_AdminDAO().InsertLog(logEntity);

                if (id > 0)
                {
                    return RedirectToAction("Index", "Newsletter");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm tin tức thành công");
                }
            }
            GetNewsCategory();
            return View("Create");
        }

        [HttpPost,ValidateInput(false)]
        public ActionResult Update(Newsletter news, HttpPostedFileBase file)
        {
            GetViewBag_Session();
            if (ModelState.IsValid)
            {
                var dao = new NewsletterDAO();
                try
                {
                    if (file.FileName != null && file.ContentLength > 0)
                    {
                        if (file.ContentType == "image/jpeg" || file.ContentType == "image/png")
                        {
                            if (file.ContentLength < 2000000)
                            {
                                string path = Path.Combine(Server.MapPath("~/images/images/Newsletter/"), Path.GetFileName(file.FileName));
                                file.SaveAs(path);
                                news.Image = file.FileName;
                            }
                            else
                            {
                                ModelState.AddModelError("", "Ảnh đại diện phải nhỏ hơn 2MB");
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("", "Ảnh phải có định dạng *png hoặc *jpg");
                        }
                    }
                }
                catch { }
            
                news.ModifiedBy = ViewBag.Name_Session;
                news.ModifiedDate = DateTime.Now;
                var metaTitle = Common.Tool.ChangeText(news.MetaTitle);
                news.MetaTitle = metaTitle;
                var result = dao.Update(news);

                // Written log
                var logEntity = new Log_Admin();
                logEntity.createBy = ViewBag.Name_Session;
                logEntity.time = DateTime.Now;
                logEntity.action = "Cập nhật tin tức " + "<a href = \"/Admin/Newsletter/Update/" + news.ID + "\">" + news.ID + "</a>";
                logEntity.description = ViewBag.Name_Session + " cập nhật tin tức hệ thống";
                var wlog = new Log_AdminDAO().InsertLog(logEntity);

                if (result)
                {
                    return RedirectToAction("Index", "Newsletter");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật tin tức thành công");
                }
            }
            GetNewsCategory(news.CategoryID);
            return View("Update");
        }
        [HttpDelete]
        public ActionResult Delete(long id)
        {
            new NewsletterDAO().Delete(id);
            return RedirectToAction("Index", "Newsletter");
        }
        public void GetNewsCategory(long? selectedID =null)
        {
            var dao = new NewsCategoryDAO();
            ViewBag.CategoryID = new SelectList(dao.ListAllToDropDown(), "ID", "Name", selectedID);
        }
        [HttpPost]
        public JsonResult ChangeStatus(long id)
        {
           var result =  new NewsletterDAO().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }
        [HttpGet]
        public JsonResult GetDataBindToAutoComplete()
        {
            List<string> arr_name = new List<string>();
            var result = new NewsletterDAO().GetAllNews();
            foreach (var item in result)
            {
                arr_name.Add(item.Name);
            }
            return Json(new
            {
                data = arr_name
            }, JsonRequestBehavior.AllowGet);
        }
    }
}