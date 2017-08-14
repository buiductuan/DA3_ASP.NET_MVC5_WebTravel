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
    public class NewsletterDAO
    {
        TravelDbContext db = null;
        public NewsletterDAO()
        {
            db = new TravelDbContext();
        }
        #region CRUD
        public long InsertNewsletter(Newsletter entity)
        {
            if (string.IsNullOrEmpty(entity.Image))
            {
                entity.Image = "none.png";
            }
            entity.CreatedDate = DateTime.Now;
            entity.ViewCount = 0;
            entity.Like = 0;
            entity.Angry = 0;
            entity.Sad = 0;
            entity.Glad = 0;
            entity.Love = 0;
            entity.Enjoy = 0;
            entity.Scare = 0;
            db.Newsletter.Add(entity);
            db.SaveChanges();

            //Add tags
            if (!string.IsNullOrEmpty(entity.Tags))
            {
                string[] tags = entity.Tags.Split(',');

                foreach (var tag in tags)
                {
                    var tagID = StringHelper.ChangeText(tag);
                    if (!CheckTag(tagID))
                    {
                        this.InsertTag(tagID, tag);
                    }
                    if (!CheckTagInNewsTag(entity.ID, tagID))
                    {
                        InsertNewsletterTag(entity.ID, tagID);
                    }
                }
            }
            return entity.ID;
        }

        //Tags
        public void InsertTag(string id, string name)
        {
            var tag = new Tag();
            tag.TagID = id;
            tag.Name = name;
            db.Tag.Add(tag);
            db.SaveChanges();
        }
        public void InsertNewsletterTag(long newsID, string tagID)
        {
            var newsTag = new NewsletterTag();
            newsTag.TagID = tagID;
            newsTag.NewsletterID = newsID;
            db.NewsletterTag.Add(newsTag);
            db.SaveChanges();
        }
        public bool CheckTag(string id)
        {
            return db.Tag.Count(x => x.TagID == id) > 0;
        }
        public bool CheckTagInNewsTag(long newsID, string tags)
        {
            return db.NewsletterTag.Count(x => x.NewsletterID == newsID && x.TagID == tags) > 0;
        }
        public string GetNameTagByID(string id)
        {
            return db.Tag.SingleOrDefault(x => x.TagID == id).Name;
        }
        public List<Tag> GetListTagInNewsletter(long newsleterID)
        {
            var lst = (from a in db.Tag
                       join b in db.NewsletterTag
                        on a.TagID equals b.TagID
                       where b.NewsletterID == newsleterID
                       select new
                       {
                           TagID = b.TagID,
                           Name = a.Name
                       }).AsEnumerable().Select(x => new Tag()
                       {
                           TagID = x.TagID,
                           Name = x.Name
                       });
            return lst.ToList();
        }
        public List<Newsletter> GetListNewByTags(string tagId)
        {
            var res = (from a in db.Tag
                       join
                          b in db.NewsletterTag on
                          a.TagID equals b.TagID
                       join c in db.Newsletter
                       on b.NewsletterID equals c.ID
                       where b.TagID == tagId
                       select new
                       {
                           Id = c.ID,
                           Name = c.Name,
                           Image = c.Image,
                           Description = c.Description,
                           CreateDate = c.CreatedDate,
                           MetaTitle = c.MetaTitle,

                       }).AsEnumerable().Select(x => new Newsletter
                       {
                           ID= x.Id,
                           Name = x.Name,
                           Image= x.Image,
                           Description = x.Description,
                           CreatedDate = x.CreateDate,
                           MetaTitle= x.MetaTitle
                       }).ToList();
            return res;
        }

        //Get list news
        public IEnumerable<Newsletter> ListAll(string q, int page, int pageSize)
        {
            IQueryable<Newsletter> model = db.Newsletter;
            if (!string.IsNullOrEmpty(q))
            {
                model = model.Where(x => x.Name.Contains(q) || x.Tags.Contains(q));
            }
            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        }
        public IEnumerable<Newsletter> GetListInPage(int page, int pageSize)
        {
            return db.Newsletter.Where(x => x.Status == true).OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        }
        public IEnumerable<Newsletter> GetNews()
        {
            return db.Newsletter.Where(x => x.Status == true).OrderByDescending(x => x.CreatedDate);
        }
        public IEnumerable<Newsletter> GetAllNews()
        {
            return db.Newsletter;
        }
        public IEnumerable<Newsletter> GetHotNews()
        {
            return db.Newsletter.Where(x => x.Status == true).OrderByDescending(x => x.ViewCount);
        }
        public IEnumerable<Newsletter> GetNewsShowOnHome()
        {
            return db.Newsletter.Where(x => x.ShowOnHome == true).OrderByDescending(x => x.CreatedDate);
        }
        public Newsletter GetDetail(long id)
        {
            return db.Newsletter.Find(id);
        }
        public long CountBlog()
        {
            return db.Newsletter.ToList().Count;
        }
        public bool Update(Newsletter entity)
        {
            var newsletter = db.Newsletter.Find(entity.ID);
            try
            {
                newsletter.Description = entity.Description;
                newsletter.Name = entity.Name;
                if (!string.IsNullOrEmpty(entity.Image))
                {
                    newsletter.Image = entity.Image;
                }
                newsletter.MetaTitle = entity.MetaTitle;
                newsletter.ModifiedBy = entity.ModifiedBy;
                newsletter.ModifiedDate = DateTime.Now;
                newsletter.Status = entity.Status;
                newsletter.Detail = entity.Detail;
                newsletter.ShowOnHome = entity.ShowOnHome;

                if (!string.IsNullOrEmpty(entity.Tags))
                {
                    newsletter.Tags = entity.Tags;
                    string[] tags = entity.Tags.Split(',');

                    foreach (var tag in tags)
                    {
                        var tagID = StringHelper.ChangeText(tag);
                        if (!CheckTag(tagID))
                        {
                            InsertTag(tagID, tag);
                        }
                        if (!CheckTagInNewsTag(entity.ID, tagID))
                        {
                            InsertNewsletterTag(entity.ID, tagID);
                        }
                    }
                }
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public void ViewUp(long id)
        {
            var entity = db.Newsletter.Find(id);
            entity.ViewCount = entity.ViewCount + 1;
            db.SaveChanges();
        }
        public bool Delete(long id)
        {
            try
            {
                var news = db.Newsletter.Find(id);
                db.Newsletter.Remove(news);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool ChangeStatus(long id)
        {
            var news = db.Newsletter.Find(id);
            news.Status = !news.Status;
            db.SaveChanges();
            return news.Status;
        }
        #endregion

        #region Feedback emoij 
        public bool Update_emoij(int id, string text_emoij)
        {
            try
            {
                var model = db.Newsletter.Find(id);
                if (text_emoij == "like")
                {
                    model.Like += 1;
                }
                else if (text_emoij == "angry")
                {
                    model.Angry += 1;
                }
                else if (text_emoij == "sad")
                {
                    model.Sad += 1;
                }
                else if (text_emoij == "glad")
                {
                    model.Glad += 1;
                }
                else if (text_emoij == "love")
                {
                    model.Love += 1;
                }
                else if (text_emoij == "scare")
                {
                    model.Scare += 1;
                }
                else if (text_emoij == "enjoy")
                {
                    model.Enjoy += 1;
                }
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        #endregion

        //count news by createby
        public long CountNewsletterByCreateby(string name)
        {
            return db.Newsletter.Where(x => x.CreatedBy == name).ToList().Count;
        }

        //get list news by Createby
        public IEnumerable<Newsletter> GetListNewsByCreateby(string name)
        {
            return db.Newsletter.Where(x => x.CreatedBy == name);
        }
    }
}
