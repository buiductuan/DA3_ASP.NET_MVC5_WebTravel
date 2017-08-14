namespace Model.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Discount")]
    public partial class Discount
    {
        [Required(ErrorMessage ="Vui lòng điền mã khuyến mãi hoặc chọn mã tự động")]
        [MaxLength(ErrorMessage ="Mã khuyến mãi chứa tối đa 10 ký tự")]
        public string ID { get; set; }

        public long ProductCategoryID { get; set; }

        [Column(TypeName ="xml")]
        public string ListProduct { get; set; }

        [Required(ErrorMessage = "Vui lòng điền số % được giảm")]
        public double Dispercent { get; set; }

        [Required(ErrorMessage = "Vui lòng điền số giá được giảm")]
        public double Disprice { get; set; }

        [Required(ErrorMessage = "Vui lòng điền ngày bắt đầu")]
        public DateTime DayStart { get; set; }

        [Required(ErrorMessage = "Vui lòng điền ngày kết thúc ")]
        public DateTime DayEnd { get; set; }

        [Required(ErrorMessage = "Vui lòng điền số lượng")]
        public int Amount { get; set; }

        public bool Status { get; set; }
        
    }
}
