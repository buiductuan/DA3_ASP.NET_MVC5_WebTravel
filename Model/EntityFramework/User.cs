namespace Model.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("User")]
    public partial class User
    {
        public long ID { get; set; }

        [Required(ErrorMessage = "(*) Tên tài khoản không được trống")]
        [StringLength(50)]
        [Display(Name="Tên tài khoản")]
        public string UserName { get; set; }

        [StringLength(32)]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }

        [StringLength(50)]
        public string GroupID { get; set; }

        [Required(ErrorMessage = "(*) Họ và tên không được trống")]
        [StringLength(50)]
        [Display(Name = "Họ và tên")]
        public string Name { get; set; }

        [StringLength(250)]
        [Display(Name = "Ảnh đại diện")]
        public string Image { get; set; }

        [Required(ErrorMessage = "(*) Email không được trống")]
        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(50)]
        [Display(Name = "Điện thoại")]
        public string Phone { get; set; }

        [StringLength(50)]
        [Display(Name = "Địa chỉ")]
        public string Address { get; set; }

        [Column(TypeName ="ntext")]
        public string Note { get; set; }

        [Column(TypeName = "ntext")]
        public string About { get; set; }

        public DateTime? CreatedDate { get; set; }

        [StringLength(100)]
        public string CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [StringLength(100)]
        public string ModifiedBy { get; set; }

        public DateTime? TimeActive { get; set; }

        [Display(Name ="Trạng thái")]
        public bool Status { get; set; }
    }
}
