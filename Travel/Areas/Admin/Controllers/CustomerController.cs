using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.EntityFramework;
using Model.DataAccessObject;
namespace Travel.Areas.Admin.Controllers
{
    public class CustomerController : BaseController
    {
        // GET: Admin/Customer
        public ActionResult Index(string q, int page =1, int pageSize = 5)
        {
            GetViewBag_Session();
            var dao = new CustomerDAO();
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

        [HttpPost]
        public ActionResult Create(Customer entity)
        {
            if (ModelState.IsValid)
            {

            }
            return View("Create");
        }
    }
}