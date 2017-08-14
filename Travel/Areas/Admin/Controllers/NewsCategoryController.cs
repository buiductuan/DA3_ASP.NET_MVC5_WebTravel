using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.DataAccessObject;
using Model.EntityFramework;
using System.IO;
using Travel.Common;
using System.Configuration;
namespace Travel.Areas.Admin.Controllers
{
    public class NewsCategoryController : BaseController
    {
        // GET: Admin/NewsCategory
        public ActionResult Index(string q, int page = 1, int pageSize = 5)
        {
            GetViewBag_Session();
            var dao = new NewsCategoryDAO();
            var model = dao.ListAll(q, page, pageSize);
            ViewBag.QuerySearch = q;
            return View(model);
        }
        [HttpGet]
        public ActionResult Create()
        {
            GetViewBag_Session();
            return View();
        }
        public ActionResult Update(long id)
        {
            GetViewBag_Session();
            var model = new NewsCategoryDAO().ViewDetail(id);
            return View(model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult Create(NewsCategory newsCategory, HttpPostedFileBase file)
        {
            GetViewBag_Session();
            if (ModelState.IsValid)
            {
                var dao = new NewsCategoryDAO();
                try
                {
                    if (file.FileName != null && file.ContentLength > 0)
                    {
                        if (file.ContentType == "image/jpeg" || file.ContentType == "image/png")
                        {
                            if (file.ContentLength < 2000000)
                            {
                                string path = Path.Combine(Server.MapPath("~/images/images/NewsCategory/"), Path.GetFileName(file.FileName));
                                file.SaveAs(path);
                                newsCategory.Image = file.FileName;
                            }
                            else
                            {
                                ModelState.AddModelError("", "Ảnh đại diện phải nhỏ hơn 2MB");
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("", "Ảnh đại diện phải có định dạng *png hoặc *jpg");
                        }
                    }
                }
                catch { }

                var metaTitle = Tool.ChangeText(newsCategory.MetaTitle);
                newsCategory.MetaTitle = metaTitle;
                newsCategory.CreatedBy = ViewBag.Name_Session;
                newsCategory.CreatedDate = DateTime.Now;
                long id = dao.Insert(newsCategory);
                if (id > 0)
                {
                    return RedirectToAction("Index", "NewsCategory");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm danh mục tin tức thành công");
                }
            }
            return View("Create");
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Update(NewsCategory entity, HttpPostedFileBase file)
        {
            GetViewBag_Session();
            if (ModelState.IsValid)
            {
                var dao = new NewsCategoryDAO();
                try
                {
                    if (dao.Check_Image_NewsCategory(file.FileName) == false)
                    {
                        if (file.FileName != null && file.ContentLength > 0)
                        {
                            if (file.ContentType == "image/jpeg" || file.ContentType == "image/png")
                            {
                                if (file.ContentLength < 2000000)
                                {
                                    string path = Path.Combine(Server.MapPath("~/images/images/NewsCategory/"), Path.GetFileName(file.FileName));
                                    file.SaveAs(path);
                                    entity.Image = file.FileName;
                                }
                                else
                                {
                                    ModelState.AddModelError("", "Ảnh đại diện phải nhỏ hơn 2MB");
                                }
                            }
                            else
                            {
                                ModelState.AddModelError("", "Ảnh đại diện phải có dạng *png hoặc *jpg");
                            }
                        }
                    }
                }
                catch { }
                var metaTitle = Tool.ChangeText(entity.MetaTitle);
                entity.MetaTitle = metaTitle;
                entity.ModifiedBy = ViewBag.Name_Session;
                entity.ModifiedDate = DateTime.Now;
                var result = dao.Update(entity);
                if (result)
                {
                    return RedirectToAction("Index", "NewsCategory");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhập thành công danh mục tin tức");
                }
            }
            return View("Update");
        }
        [HttpDelete]
        public ActionResult Delete(long id)
        {
            new NewsCategoryDAO().Delete(id);
            return RedirectToAction("Index", "NewsCategory");
        }

        [HttpPost]
        public JsonResult ChangeStatus(long id)
        {
            var result = new NewsCategoryDAO().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }
    }
}