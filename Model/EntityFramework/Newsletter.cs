namespace Model.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Newsletter")]
    public partial class Newsletter
    {
        public long ID { get; set; }

        [Required (ErrorMessage ="Tiêu đề không được trống")]
        [MaxLength(ErrorMessage ="Tiêu đề chỉ chứa tối đa 250 ký tự")]
        [StringLength(250)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Thẻ tiêu đề không được trống")]
        [MaxLength(ErrorMessage = "Thẻ tiêu đề chỉ chứa tối đa 250 ký tự")]
        [StringLength(250)]
        public string MetaTitle { get; set; }

        [Required(ErrorMessage = "Mô tả không được trống")]
        [MaxLength(ErrorMessage = "Mô tả chỉ chứa tối đa 500 ký tự")]
        [StringLength(500)]
        public string Description { get; set; }

        [StringLength(250)]
        public string Image { get; set; }

        [Column(TypeName = "xml")]
        public string MoreImages { get; set; }

        public long? CategoryID { get; set; }

        [Required(ErrorMessage = "Nội dung không được trống")]
        [Column(TypeName = "ntext")]
        public string Detail { get; set; }

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

        public int? ViewCount { get; set; }

        [StringLength(500)]
        public string Tags { get; set; }

        public bool ShowOnHome { get; set; }

        public int Like { get; set; }

        public int Angry { get; set; }

        public int Sad { get; set; }

        public int Glad { get; set; }

        public int Love { get; set; }

        public int Enjoy { get; set; }

        public int Scare { get; set; }

        [StringLength(2)]
        public string Language { get; set; }
    }
}
