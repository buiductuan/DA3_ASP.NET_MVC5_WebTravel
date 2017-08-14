using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EntityFramework;
using PagedList;

namespace Model.DataAccessObject
{
   public class FeedBackDAO
    {
        TravelDbContext db = null;
        public FeedBackDAO()
        {
            db = new TravelDbContext();
        }

        public int Insert(FeedBack entity)
        {
            entity.Status = false;
            entity.CreateDate = DateTime.Now;
            db.FeedBack.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }

        public IEnumerable<FeedBack> GetList()
        {
            return db.FeedBack.Where(x=>x.ShowOnPage==true).OrderByDescending(x=>x.CreateDate).ToList();
        }
        public FeedBack ViewDetail(int id)
        {
            return db.FeedBack.Find(id);
        }
        public int CountNewsFeedback()
        {
            return db.FeedBack.Where(x=>x.Status == false).Count(x => x.CreateDate == DateTime.Now);
        }
        public bool ChangeRead(int id)
        {
            var model = db.FeedBack.Find(id);
            model.Status = true;
            db.SaveChanges();
            return model.Status;
        }
        public bool ChangeShow(int id)
        {
            var model = db.FeedBack.Find(id);
            model.ShowOnPage = !model.ShowOnPage;
            db.SaveChanges();
            return model.ShowOnPage;
        }
        public bool Delete(int id)
        {
            try
            {
                var model = db.FeedBack.Find(id);
                db.FeedBack.Remove(model);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public IEnumerable<FeedBack> ListAll(string q, int page, int pageSize)
        {
            IQueryable<FeedBack> model = db.FeedBack;
            if (!string.IsNullOrEmpty(q))
            {
                model = model.Where(x => x.Name.Contains(q) || x.Email.Contains(q));
            }
            return model.OrderByDescending(x => x.CreateDate).ToPagedList(page, pageSize);
        }
    }
}
