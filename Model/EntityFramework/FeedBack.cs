namespace Model.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("FeedBack")]
    public partial class FeedBack
    {
        public int ID { get; set; }

        [Required(ErrorMessage ="Tên không được trống")]
        [StringLength(100)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Nội dung được trống")]
        [Column(TypeName ="ntext")]
        public string Content { get; set; }

        [Required(ErrorMessage = "Email được trống")]
        [StringLength(100)]
        public string Email { get; set; }

        public DateTime? CreateDate { get; set; }

        public bool Status { get; set; }

        public bool ShowOnPage { get; set; }
    }
}
