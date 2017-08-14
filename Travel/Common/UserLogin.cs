using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Travel.Common
{
    [Serializable]
    public class UserLogin
    {
        public long UserID { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string UserGroup { get; set; }
    }
}