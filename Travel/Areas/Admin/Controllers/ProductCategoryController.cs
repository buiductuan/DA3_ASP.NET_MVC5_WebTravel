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
    public class ProductCategoryController : BaseController
    {
        // GET: Admin/ProductCategory
        public ActionResult Index(string q,int page= 1, int pageSize = 5)
        {
            GetViewBag_Session();
            var dao = new ProductCategoryDAO();
            var model = dao.ListAll(q,page, pageSize);
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
            var model = new ProductCategoryDAO().ViewDetail(id);
            return View(model);
        }
        [HttpPost,ValidateInput(false)]
        public ActionResult Create(ProductCategory prCategory,HttpPostedFileBase file)
        {
            GetViewBag_Session();
            if (ModelState.IsValid)
            {
                var dao = new ProductCategoryDAO();
                if (file != null && file.ContentLength > 0)
                {
                    if(file.ContentLength < 2000000)
                    {
                        string path = Path.Combine(Server.MapPath("~/images/images/ProductCategory/"), Path.GetFileName(file.FileName));
                        file.SaveAs(path);
                        prCategory.Image = file.FileName;
                    }
                    else
                    {
                        ModelState.AddModelError("", "Ảnh đại diện kích thước nhỏ hơn 2MB");
                    }
                }
                // change alias metatitle
                var metaTitle = Tool.ChangeText(prCategory.MetaTitle);
                prCategory.MetaTitle = metaTitle;
                prCategory.CreatedBy = ViewBag.Name_Session;
                prCategory.CreatedDate = DateTime.Now;
                long id = dao.InsertProductCategory(prCategory);
                if(id > 0)
                {
                    return RedirectToAction("Index", "ProductCategory");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm danh mục thành công");
                }
            }
            return View("Create");
        }
        [HttpPost,ValidateInput(false)]
        public ActionResult Update(ProductCategory prCategory,HttpPostedFileBase file)
        {
            GetViewBag_Session();
            if (ModelState.IsValid)
            {
                var dao = new ProductCategoryDAO();
                try
                {
                    if (file.FileName != "")
                    {
                        if (file.ContentType == "image/jpeg" || file.ContentType == "image/png")
                        {
                            if (file.ContentLength < 2000000)
                            {
                                string path = Path.Combine(Server.MapPath("~/images/images/ProductCategory/"), Path.GetFileName(file.FileName));
                                file.SaveAs(path);
                                prCategory.Image = file.FileName;
                            }
                            else
                            {
                                ModelState.AddModelError("", "Ảnh đại diện chỉ dưới 2MB");
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("", "Hãy sử dụng ảnh là *png hoặc *jpg");
                        }
                    }
                }
                catch { }
                prCategory.ModifiedBy = ViewBag.Name_Session;
                prCategory.ModifiedDate = DateTime.Now;
                var result = dao.UpdateProductCategory(prCategory);
                if (result)
                {
                    return RedirectToAction("Index", "ProductCategory");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm danh mục thành công");
                }
            }
            return View("Update");
        }
        [HttpDelete]
        public ActionResult Delete(long id)
        {
            new ProductCategoryDAO().DeleteProductCategory(id);
            return RedirectToAction("Index", "ProductCategory");
        }
        [HttpPost]
        public JsonResult ChangeStatus(long id)
        {
           var result =  new ProductCategoryDAO().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }
    }
}