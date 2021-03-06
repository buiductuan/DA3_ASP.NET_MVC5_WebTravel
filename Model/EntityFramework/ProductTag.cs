﻿namespace Model.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ProductTag")]
   public class ProductTag
    {
        [Key]
        [Column(Order = 0)]
        public long ProductID { get; set; }


        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [StringLength(50)]
        public string TagID { get; set; }
    }
}
