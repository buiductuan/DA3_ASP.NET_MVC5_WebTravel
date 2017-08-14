using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Views
{
   public class ProductOrder_View
    {
        public long ID { get; set; }

        public string NameCustomer { get; set; }

        public string Note { get; set; }

        public string ProductName { get; set; }

        public DateTime CreateDate { get; set; }

        public bool Status { get; set; }
    }
}
