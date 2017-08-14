using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Configuration;
using System.Security.Cryptography;
using System.Globalization;
using System.Web;
using System.Net;
using System.Net.Sockets;

namespace Travel.Common
{
    public class Tool
    {
        public static string ChangeText(string input)
        {
            //Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            //string temp = s.Normalize(NormalizationForm.FormD);
            //return regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D').Replace(" ", "-").ToLower();
            input = input.Trim();
            for (int i = 0x20; i < 0x30; i++)
            {
                input = input.Replace(((char)i).ToString(), " ");
            }
            input = input.Replace(".", "-");
            input = input.Replace("*", "-");
            input = input.Replace("&", "-");
            input = input.Replace("#", "-");
            input = input.Replace("(", "-");
            input = input.Replace(")", "-");
            input = input.Replace("[", "-");
            input = input.Replace("]", "-");
            input = input.Replace(" ", "-");
            input = input.Replace(",", "-");
            input = input.Replace(";", "-");
            input = input.Replace(":", "-");
            input = input.Replace("  ", "-");
            Regex regex = new Regex(@"\p{IsCombiningDiacriticalMarks}+");
            string str = input.Normalize(NormalizationForm.FormD);
            string str2 = regex.Replace(str, string.Empty).Replace('đ', 'd').Replace('Đ', 'D');
            while (str2.IndexOf("?") >= 0)
            {
                str2 = str2.Remove(str2.IndexOf("?"), 1);
            }
            while (str2.Contains("--"))
            {
                str2 = str2.Replace("--", "-").ToLower();
            }
            return str2;
        }
        public bool IsNumber(string pText)
        {
            //Regex regex = new Regex(@"^[-+]?[0-9]*\.?[0-9]+$");
            Regex regex = new Regex(@"^\d+$");
            return regex.IsMatch(pText);
        }

        //Radom Avatar Customer
        public static string Radom_Avatar_Customer()
        {
            string[] name_Avatar = new string[]
            {
                "a.png","b.png","c.png",
                "d.png","e.png","f.png",
                "g.png","h.png","i.png",
                "j.png","k.png","l.png",
                "m.png","n.png","o.png",
                "p.png","q.png","t.png",
                "u.png","v.png","x.png",
                "y.png","z.png"
            };
            Random ran = new Random();
            int em = ran.Next(0, name_Avatar.Length);
            return name_Avatar[em];
        }
        //Radom Avatar Customer
        public static string Random_Label_Tag()
        {
            string[] arr = new string[]
            {
                "label-danger","label-primary",
                "label-success","label-default","label-warning","label-info"
            };
            Random ran = new Random();
            int em = ran.Next(0, arr.Length);
            return arr[em];
        }
        public string FormatPrice(int price)
        {
            CultureInfo original = new CultureInfo("vi-VN", false);
            NumberFormatInfo vietNfi = (NumberFormatInfo)original.NumberFormat.Clone();
            vietNfi.CurrencySymbol = "₫";
            string[] a = price.ToString("C", vietNfi).Split(',');
            return a[0] + "\0 ₫";
        }
        //Get IP
        public static string GetIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("Local IP Address Not Found!");
        }
        //Get Agent
        //public static string GetAgent()
        //{
        //}
    }
}
