namespace Model.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UserLog")]
    public partial class UserLog
    {
        public long ID { get; set; }

        public long UserID { get; set; }

        [StringLength(50)]
        public string IP { get; set; }

        [StringLength(250)]
        public string Agent { get; set; }

        public DateTime TimeActive { get; set; }
    }
}
