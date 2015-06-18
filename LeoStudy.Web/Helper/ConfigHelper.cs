using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace LeoStudy.Web.Helper
{
    public static class ConfigHelper
    {
        public static string AppId
        {
            get { return ConfigurationManager.AppSettings["AppId"]; }
        }
        public static string EncodingAESKey
        {
            get { return ConfigurationManager.AppSettings["EncodingAESKey"]; }
        }
        public static string Token
        {
            get { return ConfigurationManager.AppSettings["Token"]; }
        }
    }
}