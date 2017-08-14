using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EntityFramework;
using PagedList;
using PagedList.Mvc;
namespace Model.DataAccessObject
{
  public  class ProductCategoryDAO
    {
        TravelDbContext db = null;
        public ProductCategoryDAO()
        {
            db = new TravelDbContext();
        }

        #region CRUD
        //Create
        public long InsertProductCategory(ProductCategory entity)
        {
            if (string.IsNullOrEmpty(entity.Image))
            {
                entity.Image = "none.png";
            }
            entity.CreatedDate = DateTime.Now;
            db.ProductCategory.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }
        //Read and paging
        public IEnumerable<ProductCategory> ListAll(string q,int page,int pageSize)
        {
            IQueryable<ProductCategory> model = db.ProductCategory;
            if (!string.IsNullOrEmpty(q))
            {
                model = model.Where(x => x.Name.Contains(q));
            }
            return model.OrderByDescending(t => t.CreatedDate).ToPagedList(page, pageSize);
        }
        //Update
        public bool UpdateProductCategory(ProductCategory entity)
        {
            try
            {
                var productCategory = db.ProductCategory.Find(entity.ID);
                productCategory.Name = entity.Name;
                if (!string.IsNullOrEmpty(entity.Image))
                {
                    productCategory.Image = entity.Image;
                }
                productCategory.ModifiedBy = entity.ModifiedBy;
                productCategory.Description = entity.Description;
                productCategory.ModifiedDate = DateTime.Now;
                productCategory.Status = entity.Status;
                productCategory.MetaTitle = entity.MetaTitle;
                productCategory.MetaDescription = entity.MetaDescription;
                productCategory.MetaKeywords = entity.MetaKeywords;
                productCategory.SeoTitle = entity.SeoTitle;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public ProductCategory ViewDetail(long id)
        {
            return db.ProductCategory.Find(id);
        }
        //Delete
        public bool DeleteProductCategory(long id)
        {
            try
            {
                var productCategory = db.ProductCategory.Find(id);
                db.ProductCategory.Remove(productCategory);
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
            var productCategory = db.ProductCategory.Find(id);
            productCategory.Status = !productCategory.Status;
            db.SaveChanges();
            return productCategory.Status;
        }
        //bind dropdownlist
        public List<ProductCategory> ListAllToDropDown()
        {
            return db.ProductCategory.Where(x => x.Status == true).OrderByDescending(x=>x.CreatedDate).ToList();
        }
        #endregion

        // Get name
        public List<string> GetNameProductCategory()
        {
            List<string> lst = new List<string>();
            var res = db.ProductCategory;
            foreach(var item in res)
            {
                lst.Add(item.Name);
            }
            return lst;
        }
        #region Check
        //check name
        public bool Check_Name_ProductCategory(string name)
        {
            var result = db.ProductCategory.SingleOrDefault(t => t.Name==name);
            if(result != null)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        #endregion

        public long GetIdCateByName(string name)
        {
            return db.ProductCategory.SingleOrDefault(x => x.Name == name).ID;
        }
    }
}
