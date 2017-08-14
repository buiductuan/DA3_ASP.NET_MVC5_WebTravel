using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EntityFramework;
using PagedList;

namespace Model.DataAccessObject
{
   public class MenuDAO
    {
        TravelDbContext db = null;
        public MenuDAO()
        {
            db = new TravelDbContext();
        }
        #region CRUD
        public int Insert(Menu mn)
        {
            if (string.IsNullOrEmpty(mn.Link))
            {
                mn.Link = "/";
                mn.Target = "_blank";
            }
            else
            {
                mn.Link = "/" + mn.Link;
                mn.Target = "_self";
            }
            db.Menu.Add(mn);
            db.SaveChanges();
            return mn.ID;
        }
        public IEnumerable<Menu> ListAll(int page,int pageSize)
        {
            return db.Menu.OrderBy(x => x.ID).ToPagedList(page, pageSize);
        }
        public List<Menu> ListMenu(int group)
        {
            return db.Menu.Where(x => x.TypeID == group).Where(x=>x.Status == true).ToList();
        }
        public Menu GetDetail(int id)
        {
            return db.Menu.Find(id);
        }
        public bool Update(Menu mn)
        {
            try
            {
                var model = db.Menu.Find(mn.ID);
                if (mn.Link[0] != '/')
                {
                    mn.Link ="/" + mn.Link;
                }
                model.Link = mn.Link;
                if (!string.IsNullOrEmpty(mn.Target))
                {
                    model.Target = mn.Target;
                }
                model.TypeID = mn.TypeID;
                model.Text = mn.Text;
                model.Status = mn.Status;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool Delete(int id)
        {
            try
            {
               var mn =   db.Menu.Find(id);
                db.Menu.Remove(mn);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool ChangeStatus(int id)
        {
            var entity = db.Menu.Find(id);
            entity.Status = !entity.Status;
            db.SaveChanges();
            return entity.Status;
        }
        #endregion
    }
}
