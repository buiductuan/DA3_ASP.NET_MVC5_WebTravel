namespace Model.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Pay")]
    public class Pay
    {
        public int ID { get; set; }
        
        [StringLength(250)]
        public string Name { get; set; }

        [StringLength (150)]
        public string WebCode { get; set; }

        [StringLength(50)]
        public string EmailPay { get; set; }

        [StringLength(100)]
        public string AccessCode { get; set; }

        [Column(TypeName ="ntext")]
        public string GuidePay { get; set; }

        [StringLength(100)]
        public string CodeAuthencation { get; set; }

        [StringLength(100)]
        public string CodeRepeat { get; set; }

        [StringLength(100)]
        public string AIPUserName { get; set; }

        [StringLength(100)]
        public string AIPPassword { get; set; }

        [StringLength(100)]
        public string AIPSignature { get; set; }

        [StringLength(100)]
        public string Currency { get; set; }

        [StringLength(100)]
        public string AIPKey { get; set; }

        [StringLength(100)]
        public string Terminalld { get; set; }

        [StringLength(100)]
        public string MerchantAccount { get; set; }

        [StringLength(100)]
        public string HashCode { get; set; }

        public bool Disable { get; set; }

        public bool Enable { get; set; }

        public bool Visitor { get; set; }

        public bool Status { get; set; }
    }
}
