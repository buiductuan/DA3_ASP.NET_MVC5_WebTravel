using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EntityFramework;
namespace Model.DataAccessObject
{
    
   public class PayDAO
    {
        TravelDbContext db = null;
        public PayDAO()
        {
            db = new TravelDbContext();
        }
        public IEnumerable<Pay> ViewDetail()
        {
            return db.Pay;
        }
        public Pay GetDetail(int id)
        {
            return db.Pay.Find(id);
        }
        public bool Update(Pay entity)
        {
            try
            {
                var model = db.Pay.Find(entity.ID);
                model.Name = entity.Name;
                model.WebCode = entity.WebCode;
                model.EmailPay = entity.EmailPay;
                model.AccessCode = entity.AccessCode;
                model.GuidePay = entity.GuidePay;
                model.CodeAuthencation = entity.CodeAuthencation;
                model.CodeRepeat = entity.CodeRepeat;
                model.AIPUserName = entity.AIPUserName;
                model.AIPPassword = entity.AIPPassword;
                model.AIPSignature = entity.AIPSignature;
                model.AIPKey = entity.AIPKey;
                model.Currency = entity.Currency;
                model.Terminalld = entity.Terminalld;
                model.MerchantAccount = entity.MerchantAccount;
                model.HashCode = entity.HashCode;
                model.Disable = entity.Disable;
                model.Enable = entity.Enable;
                model.Visitor = entity.Visitor;
                model.Status = entity.Status;
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
