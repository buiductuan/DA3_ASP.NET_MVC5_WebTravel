namespace Model.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Menu")]
    public partial class Menu
    {
        public int ID { get; set; }

        [Required(ErrorMessage ="Tên không được trống")]
        [MaxLength(ErrorMessage ="Tên chỉ chứa tối đa 100 ký tự")]
        [StringLength(100)]
        public string Text { get; set; }

        public int? DisplayOrder { get; set; }

        [MaxLength(ErrorMessage = "Đường dẫn chỉ chứa tối đa 100 ký tự")]
        [StringLength(100)]
        public string Link { get; set; }

        [StringLength(50)]
        public string Target { get; set; }

        public bool Status { get; set; }

        public int? TypeID { get; set; }
    }
}
