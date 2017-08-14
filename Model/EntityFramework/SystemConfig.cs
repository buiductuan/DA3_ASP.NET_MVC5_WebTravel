namespace Model.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SystemConfig")]
    public partial class SystemConfig
    {
        public int ID { get; set; }

        [StringLength(50)]
        public string NameWebsite { get; set; }

        [StringLength(50)]
        public string MetaWebsite { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [StringLength(50)]
        public string EmailManage { get; set; }

        [StringLength(50)]
        public string EmailNotification { get; set; }

        [StringLength(250)]
        public string NameCompany { get; set; }

        [StringLength(50)]
        public string Phone { get; set; }

        [StringLength(400)]
        public string Location { get; set; }

        [StringLength(250)]
        public string Province { get; set; }

        [StringLength (100)]
        public string Country { get; set; }

        [StringLength(300)]
        public string Timezone { get; set; }

        [StringLength(200)]
        public string Currency { get; set; }

        [StringLength(50)]
        public string Prefix { get; set; }

        [StringLength(50)]
        public string Suffix { get; set; }

        [StringLength(350)]
        public string CodeAnalytics { get; set; }

        [Column(TypeName ="ntext")]
        public string Payment_terms { get; set; }
    }
}
