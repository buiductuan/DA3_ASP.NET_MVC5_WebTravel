using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Model.DataAccessObject;
namespace Travel
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            MenuDAO mn = new MenuDAO();
            var product = mn.GetDetail(26);
            var service = mn.GetDetail(27);
            var about = mn.GetDetail(28);
            var contact = mn.GetDetail(29);
            var news = mn.GetDetail(30);


            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //URL Search product
            routes.MapRoute(
               name: "SearchProduct",
               url: "search/",
               defaults: new { controller = "Home", action = "Search"},
               namespaces: new[] { "Travel.Controllers" }
           );

            //URL product tags
            routes.MapRoute(
               name: "TagByProduct",
               url: "hashtag/{tagID}",
               defaults: new { controller = "Home", action = "HashTagProduct", id = UrlParameter.Optional },
               namespaces: new[] { "Travel.Controllers" }
           );

            //URL news tags
            routes.MapRoute(
               name: "TagByNews",
               url: "hashtags/{tagID}",
               defaults: new { controller = "Home", action = "HashTagNews", id = UrlParameter.Optional },
               namespaces: new[] { "Travel.Controllers" }
           );

            //URL Booking tour
            routes.MapRoute(
               name: "Booking",
               url: "booking/{metatitle}-{id}.html",
               defaults: new { controller = "Book", action = "Index", id = UrlParameter.Optional },
               namespaces: new[] { "Travel.Controllers" }
           );
            //URL Sign in
            routes.MapRoute(
               name: "SignIn",
               url: "dang-nhap",
               defaults: new { controller = "Registration", action = "SignIn", id = UrlParameter.Optional },
               namespaces: new[] { "Travel.Controllers" }
           );
            //URL Sign up
            routes.MapRoute(
               name: "SignUp",
               url: "dang-ky.html",
               defaults: new { controller = "Registration", action = "SignUp", id = UrlParameter.Optional },
               namespaces: new[] { "Travel.Controllers" }
           );
            //URL Product Detail
            routes.MapRoute(
               name: "ProductDetail",
               url: "" + product.Link.Replace("/", "") + "/{metatile}-{id}.html",
               defaults: new { controller = "Product", action = "Detail", id = UrlParameter.Optional },
               namespaces: new[] { "Travel.Controllers" }
           );
            //URL Product
            routes.MapRoute(
               name: "ProductList",
               url: "" + product.Link.Replace("/", "") + ".html",
               defaults: new { controller = "Product", action = "Index", id = UrlParameter.Optional },
               namespaces: new[] { "Travel.Controllers" }
           );
            //URL News
            routes.MapRoute(
                name: "Newsletter",
                url: "" + news.Link.Replace("/", "") + ".html",
                defaults: new { controller = "Newsletter", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "Travel.Controllers" }
            );
            //URL News Detail
            routes.MapRoute(
               name: "NewsDetail",
               url: "" + news.Link.Replace("/", "") + "/{metatile}-{id}.html",
               defaults: new { controller = "Newsletter", action = "Detail", id = UrlParameter.Optional },
               namespaces: new[] { "Travel.Controllers" }
           );
            //URL Contact
            routes.MapRoute(
                name: "Contact",
                url: "" + contact.Link.Replace("/", "") + ".html",
                defaults: new { controller = "Contact", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "Travel.Controllers" }
            );
            //URL Feature
            routes.MapRoute(
               name: "Feature",
               url: "" + about.Link.Replace("/", "") + ".html",
               defaults: new { controller = "Feature", action = "Index", id = UrlParameter.Optional },
               namespaces: new[] { "Travel.Controllers" }
           );
            //URL Default
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "Travel.Controllers" }
            );
        }
    }
}
