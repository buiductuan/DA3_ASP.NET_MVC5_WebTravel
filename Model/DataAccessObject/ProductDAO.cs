using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EntityFramework;
using PagedList;
using ClosedXML.Excel;
using System.Data;
using System.ComponentModel;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;
using Common;
using Model.Views;

namespace Model.DataAccessObject
{
    public class ProductDAO
    {
        TravelDbContext db = null;
        public ProductDAO()
        {
            db = new TravelDbContext();
        }

        #region CRUD
        public long Insert(Product entity)
        {
            var systemConfig = new SystemConfigDAO().GetDetail(1);
            entity.CreatedDate = DateTime.Now;
            if (string.IsNullOrEmpty(entity.Image))
            {
                entity.Image = "none.png";
            }
            entity.ViewCount = 0;
            db.Product.Add(entity);
            db.SaveChanges();
            AddCode(entity.ID, entity.Duration, systemConfig.Prefix, systemConfig.Suffix, entity.Departure.ToShortDateString().Replace("/", ""));

            //Add tags
            if (!string.IsNullOrEmpty(entity.Tags))
            {
                string[] tags = entity.Tags.Split(',');

                foreach (var tag in tags)
                {
                    var tagID = StringHelper.ChangeText(tag);
                    if (!CheckTag(tagID))
                    {
                        this.InsertTag(tagID, tag);
                    }

                    this.InsertTagInProductTag(entity.ID, tagID);
                }
            }
            return entity.ID;
        }

        //Add tags
        public void InsertTag(string tagID, string name)
        {
            var entity = new Tag();
            entity.TagID = tagID;
            entity.Name = name;
            db.Tag.Add(entity);
            db.SaveChanges();
        }
        public void InsertTagInProductTag(long id, string tagid)
        {
            var entity = new ProductTag();
            entity.ProductID = id;
            entity.TagID = tagid;
            db.ProductTag.Add(entity);
            db.SaveChanges();
        }
        public bool CheckTag(string tagID)
        {
            var model = db.Tag.Find(tagID);
            if (model != null)
            {
                return true;
            }
            return false;
        }
        public bool CheckTagInProductTag(long productId, string tagID)
        {
            var model = db.ProductTag.Find(productId, tagID);
            if (model != null)
            {
                return true;
            }
            return false;
        }
        public List<Product> GetListProductByTags(string tagId)
        {
            var res = (from a in db.Tag
                       join
                          b in db.ProductTag on
                          a.TagID equals b.TagID
                       join c in db.Product
                       on b.ProductID equals c.ID
                       where b.TagID == tagId
                       select new
                       {
                           Id = c.ID,
                           Name = c.Name,
                           Image = c.Image,
                           price = c.Price,
                           promotionPrice = c.PromotionPrice,
                           MetaTitle = c.MetaTitle,

                       }).AsEnumerable().Select(x => new Product
                       {
                           ID = x.Id,
                           Name = x.Name,
                           Image = x.Image,
                           MetaTitle = x.MetaTitle,
                           Price = x.price,
                           PromotionPrice = x.promotionPrice
                       }).ToList();
            return res;
        }

        public List<ProductTag_View> GetTagInProduct()
        {
            var res = (from a in db.Tag
                       join b in db.ProductTag
                         on a.TagID equals b.TagID
                       select new
                       {
                           name = a.Name,
                           id = b.TagID
                       }).AsEnumerable().Select(x => new ProductTag_View
                       {
                           Name = x.name,
                           TagID = x.id,
                           quantity = CountProductInTag(x.id)
                       }).ToList();
            return res;
        }
       
        public int CountProductInTag(string tagid)
        { 
            int s = (from a in db.ProductTag
                     where a.TagID == tagid
                     select a).Count();
            return s;
        }
        //Get list
        public IEnumerable<Product> ListAll(string q, int page, int pageSize)
        {
            IQueryable<Product> model = db.Product;
            if (!string.IsNullOrEmpty(q))
            {
                model = model.Where(x => x.Name.Contains(q) || x.Code.Contains(q));
            }
            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        }
        public IEnumerable<Product> GetListProductPaging(int page, int pageSize)
        {
            return db.Product.Where(x => x.Status == true).OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        }
        public Product GetDetail(long id)
        {
            return db.Product.Find(id);
        }
        public IEnumerable<Product> GetListProductAtHome()
        {
            return db.Product.Where(x => x.Status == true).Where(x => x.ShowOnHome == true);
        }
        public IEnumerable<Product> GetAllProduct()
        {
            return db.Product;
        }
        public long CountProduct()
        {
            return db.Product.ToList().Count;
        }

        public void AddCode(long id, int duration, string prefix, string suffix, string date)
        {
            var model = db.Product.Find(id);
            model.Code = (prefix != "" ? (prefix + "-") : ("")) + date + "-" + duration + "-" + id + (suffix != "" ? "-" + suffix : "");
            db.SaveChanges();
        }
        public List<Tag> GetListTags(long id)
        {
            var res = (from a in db.Tag
                       join b in db.ProductTag on a.TagID equals b.TagID
                       where b.ProductID == id
                       select new
                       {
                           TagID = b.TagID,
                           Name = a.Name
                       }).AsEnumerable().Select(x => new Tag()
                       {
                           TagID = x.TagID,
                           Name  = x.Name
                       }).ToList();
            return res;
        }
        public List<string> GetPlaceOfDeparture()
        {
            List<string> lst = new List<string>();
            foreach (var item in db.Product)
            {
                lst.Add(item.PlaceofDeparture);
            }
            for(int i = 0; i < lst.Count-1; i++)
            {
                for(int j = i + 1; j < lst.Count; j++)
                {
                    if(lst[i].ToString() == lst[j].ToString())
                    {
                        lst.RemoveAt(j);
                    }
                }
            }
            return lst;
        }
        public bool Update(Product entity)
        {
            try
            {
                var model = db.Product.Find(entity.ID);
                model.Name = entity.Name;
                model.MetaTitle = entity.MetaTitle;
                if (!string.IsNullOrEmpty(entity.Image))
                {
                    model.Image = entity.Image;
                }
                model.MetaDescription = entity.MetaDescription;
                model.MetaKeywords = entity.MetaKeywords;
                model.ModifiedBy = entity.ModifiedBy;
                model.ModifiedDate = DateTime.Now;
                model.Price = entity.Price;
                model.PromotionPrice = entity.PromotionPrice;
                model.Status = entity.Status;
                model.CategoryID = entity.CategoryID;
                model.Duration = entity.Duration;
                model.PlaceofDeparture = entity.PlaceofDeparture;
                model.Quantity = entity.Quantity;
                model.Description = entity.Description;
                model.Detail = entity.Detail;
                model.Departure = entity.Departure;
                model.Note = entity.Note;
                model.ShowOnHome = entity.ShowOnHome;
                model.ShowOnSlide = entity.ShowOnSlide;
                //Add tags
                if (!string.IsNullOrEmpty(entity.Tags))
                {
                    model.Tags = entity.Tags;
                    string[] tags = entity.Tags.Split(',');

                    foreach (var tag in tags)
                    {
                        var tagID = StringHelper.ChangeText(tag);
                        if (!CheckTag(tagID))
                        {
                            this.InsertTag(tagID, tag);
                        }
                        if (!CheckTagInProductTag(entity.ID, tagID))
                        {
                            this.InsertTagInProductTag(entity.ID, tagID);
                        }
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
        public void UpdateMoreImages(long id, string images)
        {
            var model = db.Product.Find(id);
            model.MoreImages = images;
            db.SaveChanges();
        }
        public void UpdateViewCount(long id)
        {
            var model = db.Product.Find(id);
            model.ViewCount = model.ViewCount + 1;
            db.SaveChanges();
        }
        public bool Delete(long id)
        {
            try
            {
                var model = db.Product.Find(id);
                db.Product.Remove(model);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool ChangeStatus(long id)
        {
            var model = db.Product.Find(id);
            model.Status = !model.Status;
            db.SaveChanges();
            return model.Status;
        }
        #endregion

        #region Validation
        public bool Check_Name_Product(string name)
        {
            var res = db.Product.SingleOrDefault(x => x.Name == name);
            if (res != null)
                return true;
            else
                return false;
        }
        #endregion

        //bind dropdown
        public List<Product> ListAllDropDown()
        {
            return db.Product.OrderByDescending(x => x.CreatedDate).Where(x => x.Status == true).ToList();
        }

        // count product by createby
        public long CountProductByCreateby(string name)
        {
            return db.Product.Where(x => x.CreatedBy == name).ToList().Count;
        }

        //get listProductByUser
        public IEnumerable<Product> GetListProductByCreateby(string name)
        {
            return db.Product.Where(x => x.CreatedBy == name);
        }

        //search
        public IEnumerable<Product> SearchProduct(string der, string cateid, DateTime start)
        {
            long id = (new ProductCategoryDAO().GetIdCateByName(cateid));
            return db.Product.Where(x => x.PlaceofDeparture.Contains(der) && x.CategoryID == id  && x.Departure == start);
        }
    }
}
