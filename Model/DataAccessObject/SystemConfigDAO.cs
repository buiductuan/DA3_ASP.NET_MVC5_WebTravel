using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EntityFramework;
namespace Model.DataAccessObject
{
   public class SystemConfigDAO
    {
        TravelDbContext db = null;
        public SystemConfigDAO()
        {
            db = new TravelDbContext();
        }
        public SystemConfig GetDetail(int id)
        {
            return db.SystemConfig.Find(id);
        }
        public bool Update(SystemConfig sys)
        {
            try
            {
                var model = db.SystemConfig.Find(1);
                model.NameWebsite = sys.NameWebsite;
                model.MetaWebsite = sys.MetaWebsite;
                model.Description = sys.Description;
                model.EmailManage = sys.EmailManage;
                model.EmailNotification = sys.EmailNotification;
                model.NameCompany = sys.NameCompany;
                model.Phone = sys.Phone;
                model.Location = sys.Location;
                model.Province = sys.Province;
                model.Country = sys.Country;
                model.Timezone = sys.Timezone;
                model.Currency = sys.Currency;
                model.Prefix = sys.Prefix;
                model.Suffix = sys.Suffix;
                model.CodeAnalytics = sys.CodeAnalytics;
                model.Payment_terms = sys.Payment_terms;
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
