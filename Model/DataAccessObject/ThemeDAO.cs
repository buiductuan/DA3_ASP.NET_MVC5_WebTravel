using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EntityFramework;
namespace Model.DataAccessObject
{
  public class ThemeDAO
    {
        TravelDbContext db = null;
        public ThemeDAO()
        {
            db = new TravelDbContext();
        }
       
        public Theme ViewDetail(long id)
        {
            return db.Theme.Find(id);
        }
        public bool Update(Theme entity)
        {
            try
            {
                var model = db.Theme.Find(1);
                if (string.IsNullOrEmpty(entity.Header_image))
                {
                    entity.Header_image = "slide.jpg";
                }
                if(string.IsNullOrEmpty(entity.Home_Number_Blog.ToString()))
                {
                    entity.Home_Number_Blog = 3;
                }
                if (string.IsNullOrEmpty(entity.Home_Number_P.ToString()))
                {
                    entity.Home_Number_Blog = 3;
                }
                model.Header_image = entity.Header_image;
                model.Header_Description = entity.Header_Description;
                model.Home_Title_P = entity.Home_Title_P;
                model.Home_Description_P = entity.Home_Description_P;
                model.Home_Title_Blog = entity.Home_Title_Blog;
                model.Home_Description_Blog = entity.Home_Description_Blog;
                model.Product_Title = entity.Product_Title;
                model.Product_Description = entity.Product_Description;
                model.BLog_Title = entity.BLog_Title;
                model.Blog_Description = entity.Blog_Description;
                model.Feedback_Title = entity.Feedback_Title;
                model.Feedback_Description = entity.Feedback_Description;
                model.Footer_About = entity.Footer_About;
                model.Footer_facebook_Link = entity.Footer_facebook_Link;
                model.Footer_twitter_Link = entity.Footer_twitter_Link;
                model.Footer_youtube_Link = entity.Footer_youtube_Link;
                model.Footer_instagram_Link = entity.Footer_instagram_Link;
                model.Footer_Copyright = entity.Footer_Copyright;
                model.Footer_Author = entity.Footer_Author;
                model.Home_Number_Blog = entity.Home_Number_Blog;
                model.Home_Number_P = entity.Home_Number_P;
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
