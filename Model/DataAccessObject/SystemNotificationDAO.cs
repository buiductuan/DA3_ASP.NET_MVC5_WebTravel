using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EntityFramework;
namespace Model.DataAccessObject
{
   public class SystemNotificationDAO
    {
        TravelDbContext db = null;
        public SystemNotificationDAO()
        {
            db = new TravelDbContext();
        }

        #region CRUD
        public bool Update(SystemNotification entity)
        {
            try
            {
                var model = db.SystemNotification.Find(entity.ID);
                model.Pattern = entity.Pattern;
                model.Content = entity.Content;
                model.EmailTitle = entity.EmailTitle;
                model.EmailContent = entity.EmailContent;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public List<SystemNotification> ListAll()
        {
            return db.SystemNotification.ToList();
        }
        public SystemNotification ViewDetail(int id)
        {
            return db.SystemNotification.Find(id);
        }
        #endregion
    }
}
