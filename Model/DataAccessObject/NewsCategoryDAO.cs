using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EntityFramework;
using PagedList;

namespace Model.DataAccessObject
{
   public class NewsCategoryDAO
    {
        TravelDbContext db = null;
        public NewsCategoryDAO()
        {
            db = new TravelDbContext();
        }
        #region CRUD
        public long Insert(NewsCategory entity)
        {
            if (string.IsNullOrEmpty(entity.Image))
            {
                entity.Image = "none.png";
            }
            entity.CreatedDate = DateTime.Now;
            db.NewsCategory.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }
        public IEnumerable<NewsCategory> ListAll(string q , int page, int pageSize)
        {
            IQueryable<NewsCategory> model = db.NewsCategory;
            if (!string.IsNullOrEmpty(q))
            {
                model = model.Where(x => x.Name.Contains(q));
            }
            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        }
        public List<NewsCategory> ListAllToDropDown()
        {
            return db.NewsCategory.Where(x => x.Status == true).ToList();
        }
        public bool Update(NewsCategory entity)
        {
            try
            {
              var newsCategory =   db.NewsCategory.Find(entity.ID);
                newsCategory.Name = entity.Name;
                if (!string.IsNullOrEmpty(entity.Image))
                {
                    newsCategory.Image = entity.Image;
                }
                newsCategory.Description = entity.Description;
                newsCategory.ModifiedBy = entity.ModifiedBy;
                newsCategory.ModifiedDate = DateTime.Now;
                newsCategory.Status = entity.Status;
                newsCategory.MetaTitle = entity.MetaTitle;
                newsCategory.MetaDescription = entity.MetaDescription;
                newsCategory.MetaKeywords = entity.MetaKeywords;
                newsCategory.SeoTitle = entity.SeoTitle;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public NewsCategory ViewDetail(long id)
        {
            return db.NewsCategory.Find(id);
        }
        public bool Delete(long id)
        {
            try
            {
                var model = db.NewsCategory.Find(id);
                db.NewsCategory.Remove(model);
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
            var newsCateogory = db.NewsCategory.Find(id);
            newsCateogory.Status = !newsCateogory.Status;
            db.SaveChanges();
            return newsCateogory.Status;
        }
        #endregion

        #region Check
        //Check image
        public bool Check_Image_NewsCategory(string nameFile)
        {
            var image = db.NewsCategory.SingleOrDefault(x => x.Image == nameFile);
            if (image != null)
                return true;
            else
                return false;
        }
        #endregion
    }
}
