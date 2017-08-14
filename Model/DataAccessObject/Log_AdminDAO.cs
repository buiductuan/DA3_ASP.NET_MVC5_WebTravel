using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EntityFramework;
using PagedList;
namespace Model.DataAccessObject
{
   public class Log_AdminDAO
    {
        TravelDbContext db = null;
        public Log_AdminDAO()
        {
            db = new TravelDbContext();
        }
        //Written log in admin pages when user using System admin 
        public long InsertLog(Log_Admin entity)
        {
            db.Log_Admin.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }

        public IEnumerable<Log_Admin> ListLog(int page,int pageSize)
        {
            return db.Log_Admin.OrderByDescending(x => x.ID).ToPagedList(page, pageSize);
        }
    }
}
