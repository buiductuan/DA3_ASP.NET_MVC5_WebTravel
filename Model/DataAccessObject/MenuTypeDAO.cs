using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EntityFramework;
namespace Model.DataAccessObject
{
   public class MenuTypeDAO
    {
        TravelDbContext db = null;
        public MenuTypeDAO()
        {
            db = new TravelDbContext();
        }
        #region CRUD
        public int Insert(MenuType entity)
        {
            db.MenuType.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }
        public List<MenuType> BindDropDown()
        {
            return db.MenuType.ToList();
        }
        public bool Update(MenuType entity)
        {
            try
            {
                var model = db.MenuType.Find(entity.ID);
                model.Name = entity.Name;
                db.SaveChanges();
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }
        public bool Delete(int id)
        {
            try
            {
                var model = db.MenuType.Find(id);
                db.MenuType.Remove(model);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion

        public bool Check_Name(string name)
        {
            var mn = db.MenuType.SingleOrDefault(x => x.Name == name);
            if (mn != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
