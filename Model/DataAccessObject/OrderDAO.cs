using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EntityFramework;
using Model.Views;
using PagedList;

namespace Model.DataAccessObject
{
    public class OrderDAO
    {
        TravelDbContext db = null;
        public OrderDAO()
        {
            db = new TravelDbContext();
        }

        public long Insert(Order entity)
        {
            entity.CreateDate = DateTime.Now;
            entity.ModifiedDate = DateTime.Now;
            entity.Adult = 0;
            entity.Baby = 0;
            entity.Children = 0;
            entity.Kid = 0;
            entity.Status = false;
            entity.Code = entity.ProductID + entity.CreateDate.ToShortDateString().Replace("/", " ")+entity.Phone;
            db.Order.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }
        public long Count_Order()
        {
            return db.Order.ToList().Count;
        }
        public IEnumerable<ProductOrder_View> ListPaging(string q, int page, int pageSize)
        {
            IQueryable<ProductOrder_View> model = (from a in db.Product
                                                   join b in db.Order
                                                    on a.ID equals b.ProductID
                                                   select new ProductOrder_View()
                                                   {
                                                       ID = b.ID,
                                                       NameCustomer = b.NameCustomer,
                                                       CreateDate = b.CreateDate,
                                                       Note = b.Note,
                                                       ProductName = a.Name,
                                                       Status = b.Status
                                                   });

            if (!string.IsNullOrEmpty(q))
            {
                model = model.Where(x => x.NameCustomer.Contains(q) || x.ID.ToString().Contains(q));
            }
            return model.OrderByDescending(x => x.CreateDate).ToPagedList(page, pageSize);
        }
        public bool Update(Order entity)
        {
            try
            {
                var model = db.Order.Find(entity.ID);
                model.ModifiedBy = entity.ModifiedBy;
                model.ModifiedDate = entity.ModifiedDate;
                model.NameCustomer = entity.NameCustomer;
                model.Address = entity.Address;
                model.Adult = entity.Adult;
                model.Kid = entity.Kid;
                model.Children = entity.Children;
                model.Baby = entity.Baby;
                model.Payment = entity.Payment;
                model.Phone = entity.Phone;
                model.Note = entity.Note;
                model.Email = entity.Email;
                model.Status = entity.Status;
                model.StatePay = entity.StatePay;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool Delete(long id)
        {
            try
            {
                var model = db.Order.Find(id);
                db.Order.Remove(model);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
