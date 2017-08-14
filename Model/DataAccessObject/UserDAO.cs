using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EntityFramework;
using PagedList.Mvc;
using PagedList;

namespace Model.DataAccessObject
{
    public class UserDAO
    {
        TravelDbContext db = null;
        public UserDAO()
        {
            db = new TravelDbContext();
        }
        public long InsertUser(User entity)
        {
            db.User.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }
        public bool Update(User entity)
        {
            try
            {
                var user = db.User.Find(entity.ID);
                user.Name = entity.Name;
                if (!string.IsNullOrEmpty(entity.Password))
                {
                    user.Password = entity.Password;
                }
                user.Email = entity.Email;
                user.Address = entity.Address;
                if (!string.IsNullOrEmpty(entity.Image))
                {
                    user.Image = entity.Image;
                }
                user.ModifiedBy = entity.ModifiedBy;
                user.ModifiedDate = DateTime.Now;
                user.Phone = entity.Phone;
                user.Note = entity.Note;
                user.About = entity.About;
                user.Address = entity.Address;
                db.SaveChanges();
                return true;

            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<User> ListAll(int page, int pageSize)
        {
            return db.User.OrderBy(t => t.CreatedDate).ToPagedList(page, pageSize);
        }

        public User GetById(string UserName)
        {
            return db.User.SingleOrDefault(t => t.UserName == UserName);
        }

        public User ViewDetail(int id)
        {
            return db.User.Find(id);
        }
        public List<string> GetListCredential(string userName)
        {
            var data = (from a in db.Credential join
                         b in db.UserGroup on a.UserGroupID equals b.ID
                         join c in db.User on b.ID equals c.GroupID
                         where c.UserName == userName
                         select new
                         {
                             RoleID = a.RoleID,
                             UserGroupID = b.ID
                         }).AsEnumerable().Select(x => new Credential()
                         {
                             RoleID = x.RoleID,
                             UserGroupID = x.UserGroupID
                         });
            return data.Select(x => x.RoleID).ToList();
        }
        public bool Delete(int id)
        {
            try
            {
                var user = db.User.Find(id);
                db.User.Remove(user);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public int CheckLogin(string UserName, string Password)
        {
            var result = db.User.SingleOrDefault(t => t.UserName == UserName);
            if (result == null)
                return -1;
            else
            {
                if (result.Password != Password)
                    return -2;
                else
                {

                    if (result.Status == false)
                        return 0;
                    else
                        return 1;
                }
            }
        }
        public bool Check_UserName(string userName)
        {
            var user = db.User.SingleOrDefault(x => x.UserName == userName);
            if (user != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void TimeActiveUser(long id)
        {
            var user = db.User.Find(id);
            user.TimeActive = DateTime.Now;
            db.SaveChanges();
        }
    }
}
