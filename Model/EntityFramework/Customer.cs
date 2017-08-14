
namespace Model.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Customer")]
   public partial class Customer
    {
        public long ID { get; set; }

        [StringLength(100)]
        [Required(ErrorMessage ="Tên không được trống")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email không được trống")]
        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(50)]
        public string Phone { get; set; }

        [Column(TypeName ="nvarchar(MAX)")]
        public string Image { get; set; }

        [StringLength(250)]
        public string Address { get; set; }

        public long? OrderID { get; set; }

        [Column(TypeName ="ntext")]
        public string Note { get; set; }

        [StringLength(100)]
        [Required(ErrorMessage = "Tài khoản không được trống")]
        public string UserName { get; set; }

        [StringLength(50)]
        public string Password { get; set; }

        public bool Status { get; set; }
    }
}
