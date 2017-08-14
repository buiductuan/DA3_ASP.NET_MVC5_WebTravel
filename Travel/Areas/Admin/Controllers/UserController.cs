using Model.DataAccessObject;
using Model.EntityFramework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Travel.Common;
namespace Travel.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        // GET: Admin/User
        [HasCredential(RoleID = "MANAGE_SETTING")]
        public ActionResult Index(int page=1,int pageSize=5)
        {
            GetViewBag_Session();
            var dao = new UserDAO();
            var model = dao.ListAll(page, pageSize);
            return View(model);
        }
        [HasCredential(RoleID = "MANAGE_SETTING")]
        [HttpGet]
        public ActionResult Create()
        {
            GetViewBag_Session();
            return View();
        }
        public  ActionResult ProfileUser(int id)
        {
            GetViewBag_Session();
            var model = new UserDAO().ViewDetail(id);
            //Count product and news createby
            ViewBag.CountProductAdd = new ProductDAO().CountProductByCreateby(model.Name);
            ViewBag.CountNewsAdd = new NewsletterDAO().CountNewsletterByCreateby(model.Name);
            //Get list product and news createby
            ViewBag.ListProductCreateby = new ProductDAO().GetListProductByCreateby(model.Name);
            ViewBag.ListNewsCreateby = new NewsletterDAO().GetListNewsByCreateby(model.Name);
            return View(model);
        }
        [HasCredential(RoleID = "MANAGE_SETTING")]
        public ActionResult Update(int id)
        {
            GetViewBag_Session();
            var user = new UserDAO().ViewDetail(id);
            return View(user);
        }
        [HttpPost]
        [HasCredential(RoleID = "MANAGE_SETTING")]
        public ActionResult Create(User user)
        {
            GetViewBag_Session();
            if (ModelState.IsValid)
            {
                var dao = new UserDAO();
                if (!dao.Check_UserName(user.UserName))
                {
                    if (string.IsNullOrEmpty(user.Image))
                    {
                      user.Image = Tool.Radom_Avatar_Customer();
                    }
                    var encryptPassword = Encrypt.EncryptText(user.Password, true);
                    user.Password = encryptPassword;
                    long id = dao.InsertUser(user);
                    if (id > 0)
                    {
                        return RedirectToAction("Index", "User");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Thêm tài khoản thành công");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Tên đăng nhập đã tồn tại");
                }
            }
            return View("Create");
        }
        [HttpPost,ValidateInput(false)]
        [HasCredential(RoleID = "MANAGE_SETTING")]
        public ActionResult Update(User user)
        {
            GetViewBag_Session();
            if (ModelState.IsValid)
            {
                var dao = new UserDAO();
                if (!string.IsNullOrEmpty(user.Password))
                {
                    var encryptPassword = Encrypt.EncryptText(user.Password, true);
                    user.Password = encryptPassword;
                }
                user.ModifiedBy = ViewBag.Name_Session;
                var result = dao.Update(user);
                if (result)
                {
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật khoản thành công");
                }
            }
            return View("Index");
        }
        [HttpPost]
        public ActionResult ProfileUser(User entity, HttpPostedFileBase file)
        {
            GetViewBag_Session();
            if (ModelState.IsValid)
            {
                try
                {
                    if(file.FileName != null && file.ContentLength > 0)
                    {
                        if(file.ContentType =="image/png" || file.ContentType == "image/jpeg")
                        {
                            if(file.ContentLength < 2000000)
                            {
                                string path = Path.Combine(Server.MapPath("~/images/images/User/"), Path.GetFileName(file.FileName));
                                file.SaveAs(path);
                                entity.Image = file.FileName;
                            }
                            else
                            {
                                ModelState.AddModelError("", "Ảnh đại diện nhỏ hơn 2MB");
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("", "File không hợp lệ. Vui lòng chọn file ảnh .png hoặc .jpg");
                        }
                    }
                }
                catch (Exception) { }
                entity.ModifiedBy = ViewBag.Name_Session;
                entity.ModifiedDate = DateTime.Now;
                var model = new UserDAO().Update(entity);
            }
            return RedirectToAction("ProfileUser","User");
        }
        [HttpDelete]
        [HasCredential(RoleID = "MANAGE_SETTING")]
        public ActionResult Delete(int id)
        {
            new UserDAO().Delete(id);
            return RedirectToAction("Index", "User");
        }
    }
}