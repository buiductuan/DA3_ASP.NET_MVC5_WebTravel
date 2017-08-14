using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EntityFramework;
namespace Model.DataAccessObject
{
    public class DomainDAO
    {
        TravelDbContext db = null;
        public DomainDAO()
        {
            db = new TravelDbContext();
        }

        public int Insert(Domain entity)
        {
            db.Domain.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }
        public List<Domain> ListAll()
        {
            return db.Domain.ToList();
        }
        public bool Update(Domain entity)
        {
            try
            {
                var model = db.Domain.Find(entity.ID);
                model.Name = entity.Name;
                model.Status = entity.Status;
                model.Just = entity.Just;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool Delete(int id)
        {
            try
            {
                var model = db.Domain.Find(id);
                db.Domain.Remove(model);
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
