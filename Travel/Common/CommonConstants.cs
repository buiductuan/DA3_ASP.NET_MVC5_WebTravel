using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Travel.Common
{
    public static class CommonConstants
    {
        public static  string  USER_SESSION = "USER_SESSION";
        public static string CUSTOMER_SESSION = "CUSTOMER_SESSION";
        public static string TOUR_REVIEWED_SESSION = "TOUR_REVIEWED_SESSION";
        public static string CREDENTIALS_SESSION = "CREDENTIALS_SESSION";
        public static string USER_ADMIN = "ADMIN";
        public static string CurrentCulture { get; set; }
    }
}