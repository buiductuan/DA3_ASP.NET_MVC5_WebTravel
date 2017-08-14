namespace Model.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Domain")]
    public partial class Domain
    {
        public int ID { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        public bool Status { get; set; }

        public bool Just { get; set; }
    }
}
