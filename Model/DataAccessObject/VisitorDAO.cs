using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EntityFramework;
namespace Model.DataAccessObject
{
   public class VisitorDAO
    {
        TravelDbContext db = null;
        public VisitorDAO()
        {
            db = new TravelDbContext();
        }
        public long Insert(Visitor entity)
        {
            db.Visitor.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }

        public void UpdateVisitor(int year)
        {
            var entity = db.Visitor.SingleOrDefault(x => x.Year == year);
            entity.NumberOfVisit++;
            db.SaveChanges();
        }

        public List<Visitor> ListAll()
        {
            return db.Visitor.ToList();
        }
    }
}
