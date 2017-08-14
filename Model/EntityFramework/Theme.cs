
namespace Model.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Theme")]
    public partial class Theme
    {
        public long ID { get; set; }
        
        [StringLength(8000)]
        public string Header_image { get; set; }

        [StringLength(200)]
        public string Header_Description { get; set; }

        [StringLength(200)]
        public string Home_Title_P { get; set; }

        [StringLength(200)]
        public string Home_Description_P { get; set; }

        public int Home_Number_P { get; set; }

        [StringLength(200)]
        public string Home_Title_Blog { get; set; }

        [StringLength(200)]
        public string Home_Description_Blog { get; set; }

        public int Home_Number_Blog { get; set; }

        [StringLength(200)]
        public string Product_Title { get; set; }

        [StringLength(200)]
        public string Product_Description { get; set; }

        [StringLength(200)]
        public string BLog_Title { get; set; }

        [StringLength(200)]
        public string Blog_Description { get; set; }

        [StringLength(200)]
        public string Feedback_Title { get; set; }

        [StringLength(200)]
        public string Feedback_Description { get; set; }

        [StringLength(500)]
        public string Footer_About { get; set; }

        [StringLength(200)]
        public string Footer_facebook_Link { get; set; }

        [StringLength(200)]
        public string Footer_twitter_Link { get; set; }

        [StringLength(200)]
        public string Footer_instagram_Link { get; set; }

        [StringLength(200)]
        public string Footer_youtube_Link { get; set; }

        [StringLength(200)]
        public string Footer_Copyright { get; set; }

        [StringLength(100)]
        public string Footer_Author { get; set; }
    }
}
