using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LeoStudy.Web.Models
{
    public class WeChatRequestModel
    {
        public string Signature { get; set; }
        public string Timestamp { get; set; }
        public string Nonce { get; set; }
        public string Echostr { get; set; }
    }
}