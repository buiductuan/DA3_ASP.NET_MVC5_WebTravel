namespace Model.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MenuType")]
    public partial class MenuType
    {
        public int ID { get; set; }

        [Required(ErrorMessage ="Tên không được trống")]
        [MaxLength(ErrorMessage ="Tên không vượt quá 50 ký tự")]
        [StringLength(50)]
        public string Name { get; set; }
    }
}
