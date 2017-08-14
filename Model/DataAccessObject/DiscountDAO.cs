using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EntityFramework;
using PagedList;

namespace Model.DataAccessObject
{
   public class DiscountDAO
    {
        TravelDbContext db = null;
        public DiscountDAO()
        {
            db = new TravelDbContext();
        }

        #region CRUD
        public bool Insert(Discount entity)
        {
            try
            {
                db.Discount.Add(entity);
                entity.Status = true;
                db.SaveChanges();
                if(entity.Status == true)
                {
                    // change discount price in product
                    var product = db.Product.Where(x => x.CategoryID == entity.ProductCategoryID);
                    if (entity.Disprice > 0)
                    {
                        foreach (var item in product)
                        {
                            item.PromotionPrice = (decimal)item.Price - (decimal)entity.Disprice;
                        }
                    }
                    else if (entity.Dispercent > 0 && entity.Dispercent <= 100)
                    {
                        foreach (var item in product)
                        {
                            item.PromotionPrice = item.Price - ((item.Price * (decimal)entity.Dispercent) / 100);
                        }
                    }
                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public IEnumerable<Discount> ListAll(string q, int page,int pageSize)
        {
            IQueryable<Discount> model = db.Discount;
            if (!string.IsNullOrEmpty(q))
            {
               model = model.Where(x => x.ID.Contains(q));
            }
            return model.OrderByDescending(x => x.DayStart).ToPagedList(page, pageSize);
        }
        public bool Delete(string id)
        {
            try
            {
                var model = db.Discount.Find(id);
                db.Discount.Remove(model);
                db.SaveChanges();
                // change discount price in product
                var product = db.Product.Where(x => x.CategoryID == model.ProductCategoryID);
                if (model.Disprice > 0)
                {
                    foreach (var item in product)
                    {
                        item.Price = (decimal)item.Price + (decimal)model.Disprice;
                        item.PromotionPrice = null;
                    }
                }
                else if (model.Dispercent > 0 && model.Dispercent <= 100)
                {
                    foreach (var item in product)
                    {
                        item.Price = item.Price + ((item.Price * (decimal)model.Dispercent) / 100);
                        item.PromotionPrice = null;
                    }
                }
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void Check_Status_Discount()
        {
           
        }

        public bool ChangeStatus(string id)
        {
            var a = db.Discount.Find(id);
            a.Status = !a.Status;
            db.SaveChanges();

            var model = db.Discount.ToList();
            foreach (var item in model)
            {
                if (item.Status == false)
                {
                    // change discount price in product
                    var product = db.Product.Where(x => x.CategoryID == item.ProductCategoryID);
                    if (item.Disprice > 0)
                    {
                        foreach (var i in product)
                        {
                            i.Price = (decimal)i.PromotionPrice + (decimal)item.Disprice;
                            i.PromotionPrice = null;
                        }
                    }
                    else if (item.Dispercent > 0 && item.Dispercent <= 100)
                    {
                        foreach (var i in product)
                        {
                            i.Price = i.PromotionPrice + ((i.Price * (decimal)item.Dispercent) / 100);
                            i.PromotionPrice = null;
                        }
                    }
                    db.SaveChanges();
                }
                else
                {
                    // change discount price in product
                    var product = db.Product.Where(x => x.CategoryID == item.ProductCategoryID);
                    if (item.Disprice > 0)
                    {
                        foreach (var i in product)
                        {
                            i.PromotionPrice = (decimal)i.Price - (decimal)item.Disprice;
                        }
                    }
                    else if (item.Dispercent > 0 && item.Dispercent <= 100)
                    {
                        foreach (var i in product)
                        {
                            i.PromotionPrice = i.Price - ((i.Price * (decimal)item.Dispercent) / 100);
                        }
                    }
                    db.SaveChanges();
                }
            }

            return a.Status;
        }
        #endregion
    }
}
