namespace Model.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NewsletterTag")]
    public partial class NewsletterTag
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(50)]
        public string TagID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long NewsletterID { get; set; }
    }
}
