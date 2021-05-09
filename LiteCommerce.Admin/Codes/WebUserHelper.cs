using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiteCommerce.Admin
{
    public static class WebUserHelper
    {
        public static string GetCurrentUserName
        {
            get
            {
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    string[] infos = HttpContext.Current.User.Identity.Name.Split('|');
                    return infos[1];
                }
                return "";
            }
        }
        public static string GetCurrentUserID
        {
            get
            {
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    string[] infos = HttpContext.Current.User.Identity.Name.Split('|');
                    return infos[0];
                }
                return "";
            }
        }

        public static string GetCurrentUserBirthDay
        {
            get
            {
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    string[] infos = HttpContext.Current.User.Identity.Name.Split('|');
                    return infos[4];
                }
                return "";
            }
        }
        public static string GetCurrentUserEmail
        {
            get
            {
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    string[] infos = HttpContext.Current.User.Identity.Name.Split('|');
                    return infos[2];
                }
                return "";
            }
        }
        public static string GetCurrentUserTitle
        {
            get
            {
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    string[] infos = HttpContext.Current.User.Identity.Name.Split('|');
                    return infos[3];
                }
                return "";
            }
        }
        public static string GetCurrentUserPWd
        {
            get
            {
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    string[] infos = HttpContext.Current.User.Identity.Name.Split('|');
                    return infos[5];
                }
                return "";
            }
        }
        public static string GetCurrentUserAddress
        {
            get
            {
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    string[] infos = HttpContext.Current.User.Identity.Name.Split('|');
                    return infos[6];
                }
                return "";
            }
        }
    }
}