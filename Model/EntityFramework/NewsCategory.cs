namespace Model.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NewsCategory")]
    public partial class NewsCategory
    {
        public long ID { get; set; }

        [Required(ErrorMessage ="Tên danh mục không được trống")]
        [MaxLength(ErrorMessage ="Tên danh mục chỉ chứa tối đa 250 ký tự")]
        [StringLength(250)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Thẻ tiêu đề không được trống")]
        [MaxLength(ErrorMessage = "Thẻ tiêu đề mục chỉ chứa tối đa 250 ký tự")]
        [StringLength(250)]
        public string MetaTitle { get; set; }

        public long? ParentID { get; set; }

        public int? DisplayOrder { get; set; }

        [StringLength(500)]
        public string Image { get; set; }

        [Required(ErrorMessage ="Mô tả không được trống")]
        [Column(TypeName ="ntext")]
        public string Description { get; set; }

        [StringLength(250)]
        public string SeoTitle { get; set; }

        public DateTime? CreatedDate { get; set; }

        [StringLength(100)]
        public string CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [StringLength(100)]
        public string ModifiedBy { get; set; }

        [StringLength(250)]
        public string MetaKeywords { get; set; }

        [StringLength(250)]
        public string MetaDescription { get; set; }

        public bool Status { get; set; }
    }
}
