using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EntityFramework;
using PagedList;
using Common;
namespace Model.DataAccessObject
{
   public class CustomerDAO
    {
        TravelDbContext db = null;
        public CustomerDAO()
        {
            db = new TravelDbContext();
        }

        public long Insert(Customer entity)
        {
            if (string.IsNullOrEmpty(entity.Image))
            {
                entity.Image = StringHelper.Radom_Avatar_Customer();
            }
            entity.Status = true;
            db.Customer.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }
        public long InsertForFacebook(Customer entity)
        {
            var user = db.Customer.SingleOrDefault(x => x.UserName == entity.UserName);
            if(user == null)
            {
                if (string.IsNullOrEmpty(entity.Image))
                {
                    entity.Image = StringHelper.Radom_Avatar_Customer();
                }
                entity.Status = true;
                db.Customer.Add(entity);
                db.SaveChanges();
                return entity.ID;
            }
            else
            {
                return entity.ID;
            }
            
        }
        public IEnumerable<Customer> ListAll(string q, int page,int pageSize)
        {
            IQueryable<Customer> model = db.Customer;
            if (!string.IsNullOrEmpty(q))
            {
                model = model.Where(x => x.Name.Contains(q) || x.UserName.Contains(q));
            }

            return model.OrderByDescending(x => x.ID).ToPagedList(page, pageSize);
        }
        public bool Update(Customer entity)
        {
            try
            {
                var model = db.Customer.Find(entity.ID);
                model.Name = entity.Name;
                model.Phone = entity.Phone;
                model.Address = entity.Address;
                model.Email = entity.Email;
                if (!string.IsNullOrEmpty(entity.Image))
                {
                    model.Image = entity.Image;
                }
                model.Status = entity.Status;
                model.Note = entity.Note;
                model.Password = entity.Password;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete(long id)
        {
            try
            {
                var model = db.Customer.Find(id);
                db.Customer.Remove(model);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public int Check_SignIn(string UserName, string password)
        {
            var model = db.Customer.SingleOrDefault(x => x.UserName == UserName);
            if(model == null)
            {
                return -1;
            }
            else
            {
                if(model.Password != password)
                {
                    return 0;
                }
                else
                {
                    return 1;
                }
            }
        }

        public Customer GetCustomerByUserName(string userName)
        {
            return db.Customer.SingleOrDefault(x => x.UserName == userName);
        }
        public long CountCustomer()
        {
            return db.Customer.Where(x=>x.Status==true).ToList().Count;
        }

        public bool Check_UserName(string username)
        {
            return db.Customer.Count(x => x.UserName == username) > 0;
        }

        public bool Check_Email(string email)
        {
            return db.Customer.Count(x => x.Email == email) > 0;
        }
    }
}
