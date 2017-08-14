namespace Model.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Log_Admin")]
    public partial class Log_Admin
    {
        public long ID { get; set; }

        [StringLength(250)]
        public string action { get; set; }

        public DateTime time { get; set; }

        [StringLength(500)]
        public string description { get; set; }

        [StringLength(250)]
        public string createBy { get; set; }
    }
}
