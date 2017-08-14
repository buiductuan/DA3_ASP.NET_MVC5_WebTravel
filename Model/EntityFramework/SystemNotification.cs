namespace Model.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SystemNotification")]
    public partial class SystemNotification
    {
        public int ID { get; set; }

        [StringLength(2000)]
        public string Pattern { get; set; }

        [Column(TypeName = "ntext")]
        public string Content { get; set; }

        [StringLength(250)]
        public string EmailTitle { get; set; }

        [Column(TypeName = "ntext")]
        public string EmailContent { get; set; }
    }
}
