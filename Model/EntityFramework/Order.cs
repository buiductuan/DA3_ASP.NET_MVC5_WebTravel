namespace Model.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Order")]
    public class Order
    {
        public long ID { get; set; }

        [StringLength(50)]
        public string Code { get; set; }

        [StringLength(250)]
        public string NameCustomer { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(50)]
        public string Phone { get; set; }

        [StringLength(250)]
        public string Address { get; set; }

        [StringLength(500)]
        public string Note { get; set; }

        public int Adult { get; set; }

        public int Children { get; set; }

        public int Kid { get; set; }

        public int Baby { get; set; }

        [StringLength(250)]
        public string Payment { get; set; }

        [Column(TypeName ="xml")]
        public string List_tours { get; set; }

        public long ProductID { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifiedDate { get; set; }

        [StringLength(100)]
        public string ModifiedBy { get; set; }

        public bool StatePay { get; set; }

        public bool Status { get; set; }
    }
}
