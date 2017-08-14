using ClosedXML.Excel;
using Model.DataAccessObject;
using Model.EntityFramework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Xml.Linq;

namespace Travel.Areas.Admin.Controllers
{
    public class ProductController : BaseController
    {
        // GET: Admin/Product
        public ActionResult Index(string q, int page = 1, int pageSize = 5)
        {
            GetViewBag_Session();
            var dao = new ProductDAO();
            var model = dao.ListAll(q, page, pageSize);
            ViewBag.QuerySearch = q;
            return View(model);
        }
        [HttpGet]
        public ActionResult Create()
        {
            GetViewBag_Session();
            GetProductCategory();
            return View();
        }
        public ActionResult Update(long id)
        {
            GetViewBag_Session();
            var model = new ProductDAO().GetDetail(id);
            GetProductCategory(model.CategoryID);
            return View(model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult Create(Product pr, HttpPostedFileBase file)
        {
            GetViewBag_Session();
            if (ModelState.IsValid)
            {
                var dao = new ProductDAO();
                if (dao.Check_Name_Product(pr.Name))
                {
                    ModelState.AddModelError("", "Tên sản phẩm đã tồn tại ! Vui lòng nhập tên khác");
                }
                else
                {
                    #region Add Image
                    try
                    {
                        if (file.FileName != null && file.ContentLength > 0)
                        {
                            if (file.ContentType == "image/png" || file.ContentType == "image/jpeg")
                            {
                                if (file.ContentLength < 2000000)
                                {
                                    string path = Path.Combine(Server.MapPath("~/images/images/Products/"), Path.GetFileName(file.FileName));
                                    file.SaveAs(path);
                                    pr.Image = file.FileName;
                                }
                                else
                                {
                                    ModelState.AddModelError("", "Ảnh đại diện phải có kích thước nhỏ hơn 2MB");
                                }
                            }
                            else
                            {
                                ModelState.AddModelError("", "Ảnh đại diện phải có định dạng *png hoặc *jpg");
                            }
                        }
                    }
                    catch { }
                    #endregion
                    pr.CreatedBy = ViewBag.Name_Session;
                    pr.CreatedDate = DateTime.Now;
                    var metaTitle = Common.Tool.ChangeText(pr.MetaTitle);
                    pr.MetaTitle = metaTitle;
                    long id = dao.Insert(pr);
                    // Written log
                    var logEntity = new Log_Admin();
                    logEntity.createBy = ViewBag.Name_Session;
                    logEntity.time = DateTime.Now;
                    logEntity.action = "Thêm mới sản phẩm " + "<a href = \"/Admin/Product/Update/" + pr.ID + "\">" + pr.ID + "</a>";
                    logEntity.description = ViewBag.Name_Session + " thêm mới sản phẩm vào hệ thống";
                    var wlog = new Log_AdminDAO().InsertLog(logEntity);
                    if (id > 0)
                    {
                        return RedirectToAction("Index", "Product");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Thêm sản phẩm thành công");
                    }
                }
            }
            GetProductCategory();
            return View("Create");
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult Update(Product pr, HttpPostedFileBase file)
        {
            GetViewBag_Session();
            if (ModelState.IsValid)
            {
                var dao = new ProductDAO();
                #region Add Image
                try
                {
                    if (file.FileName != null && file.ContentLength > 0)
                    {
                        if (file.ContentType == "image/png" || file.ContentType == "image/jpeg")
                        {
                            if (file.ContentLength < 2000000)
                            {
                                string path = Path.Combine(Server.MapPath("~/images/images/Products/"), Path.GetFileName(file.FileName));
                                file.SaveAs(path);
                                pr.Image = file.FileName;
                            }
                            else
                            {
                                ModelState.AddModelError("", "Ảnh đại diện phải có kích thước nhỏ hơn 2MB");
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("", "Ảnh đại diện phải có định dạng *png hoặc *jpg");
                        }
                    }
                }
                catch { }
                #endregion
                pr.ModifiedBy = ViewBag.Name_Session;
                pr.ModifiedDate = DateTime.Now;
                var metaTitle = Common.Tool.ChangeText(pr.MetaTitle);
                pr.MetaTitle = metaTitle;
                var id = dao.Update(pr);
                // Written log
                var logEntity = new Log_Admin();
                logEntity.createBy = ViewBag.Name_Session;
                logEntity.time = DateTime.Now;
                logEntity.action = "Cập nhật sản phẩm " + "<a href = \"/Admin/Product/Update/" + pr.ID + "\">" + pr.ID + "</a>";
                logEntity.description = ViewBag.Name_Session + " cập nhật sản phẩm trong hệ thống";
                var wlog = new Log_AdminDAO().InsertLog(logEntity);
                if (id)
                {
                    return RedirectToAction("Index", "Product");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhập sản phẩm thành công");
                }
            }
            GetProductCategory(pr.CategoryID);
            return View("Update");
        }

        #region Ajax
        [HttpDelete]
        public ActionResult Delete(long id)
        {
            new ProductDAO().Delete(id);
            return RedirectToAction("Index", "Product");
        }

        public void GetProductCategory(long? selectedID = null)
        {
            var dao = new ProductCategoryDAO();
            ViewBag.CategoryID = new SelectList(dao.ListAllToDropDown(), "ID", "Name", selectedID);
        }

        [HttpPost]
        public JsonResult ChangeStatus(long id)
        {
            var result = new ProductDAO().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }

        [HttpPost]
        public JsonResult CheckNameProduct(string name)
        {
            var res = new ProductDAO().Check_Name_Product(name);
            return Json(new
            {
                status = res
            });
        }

        [HttpPost]
        public JsonResult DeleteAjax(long id)
        {
            var res = new ProductDAO().Delete(id);

            return Json(new
            {
                status = res
            });
        }

        [HttpGet]
        public JsonResult GetDataBindToAutoComplete()
        {
            List<string> arr_name = new List<string>();
            var result = new ProductDAO().GetAllProduct();
            foreach(var item in result)
            {
                arr_name.Add(item.Name);
            }
            return Json(new
            {
                data = arr_name
            },JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SaveMoreImages(long id,string images)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            var listImages = js.Deserialize <List<string >>(images);
            XElement xEmlement = new XElement("Images");

            foreach(var item in listImages)
            {
                var itemsub = item.Substring(21);
                xEmlement.Add(new XElement("Images", itemsub));
            }

            try
            {
                new ProductDAO().UpdateMoreImages(id, xEmlement.ToString());
                return Json(new
                {
                    status = true
                });
            }
            catch (Exception)
            {
                return Json(new
                {
                    status = false
                });
            }

           
        }

        [HttpGet]
        public JsonResult LoadImages(long id)
        {
            var model = new ProductDAO().GetDetail(id);
            var images = model.MoreImages;
            XElement xElement = XElement.Parse(images);
            List<string> listImages = new List<string>();

            foreach(var item in xElement.Elements())
            {
                listImages.Add(item.Value);
            }
            return Json(new
            {
                data = listImages
            },JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Export product to excel
        [HttpPost]
        public ActionResult ExportDataToExcel()
        {
            String constring = ConfigurationManager.ConnectionStrings["TravelDbContext"].ConnectionString;
            SqlConnection con = new SqlConnection(constring);
            string query = "select * From Product";
            DataTable dt = new DataTable();
            dt.TableName = "Product";
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            da.Fill(dt);
            con.Close();

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                wb.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                wb.Style.Font.Bold = true;

                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename= Product_Report.xlsx");

                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }
            return View();
        }
        #endregion
    }
}