namespace Model.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Product")]
    public partial class Product
    {
        public long ID { get; set; }

        [Required(ErrorMessage = ("Tên sản phẩm không được trống"))]
        [MaxLength(ErrorMessage =("Tên sản phẩm không vượt quá 250 ký tự"))]
        [StringLength(250)]
        public string Name { get; set; }

        [MaxLength(ErrorMessage = ("Mã code không vượt quá 50 ký tự"))]
        [StringLength(50)]
        public string Code { get; set; }

        [StringLength(250)]
        public string MetaTitle { get; set; }

        [Required(ErrorMessage = ("Mô tả không được trống"))]
        [MaxLength(ErrorMessage = ("Mô tả không vượt quá 250 ký tự"))]
        [StringLength(500)]
        public string Description { get; set; }

        [StringLength(250)]
        public string Image { get; set; }

        [Column(TypeName = "xml")]
        public string MoreImages { get; set; }

        public decimal? Price { get; set; }

        public decimal? PromotionPrice { get; set; }

        [RegularExpression(@"^\d+$",ErrorMessage =("Số người phải là số lớn hơn 0"))]
        public int Duration { get; set; }

        [StringLength(100)]
        public string PlaceofDeparture { get; set; }

        public DateTime Departure { get; set; }

        public int? Quantity { get; set; }

        public long? CategoryID { get; set; }

        [Column(TypeName = "ntext")]
        public string Detail { get; set; }

        [Column(TypeName ="ntext")]
        public string Note { get; set; }

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

        [StringLength (500)]
        public string Tags { get; set; }

        public bool ShowOnHome { get; set; }

        public bool ShowOnSlide { get; set; }
    }
}
