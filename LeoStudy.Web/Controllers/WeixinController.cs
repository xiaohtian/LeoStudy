using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LeoStudy.Web.Helper;
using LeoStudy.Web.Models;
using LeoStudy.WeixinApplication.MessageHandlers;
using Senparc.Weixin.MP;
using Senparc.Weixin.MP.Entities.Request;

namespace LeoStudy.Web.Controllers
{
    public class WeixinController : Controller
    {
        //
        // GET: /Weixin/
        [HttpGet]
        [ActionName("Index")]
        public ActionResult Get(PostModel postModel, string echoStr)
        {
            //通过验证
            if (CheckSignature.Check(postModel.Signature, postModel.Timestamp, postModel.Nonce, ConfigHelper.Token))
            {
                if (!String.IsNullOrWhiteSpace(echoStr))
                {
                    return Content(echoStr);
                }
            }
            return Content("参数错误！");

        }

        [HttpPost]
        [ActionName("Index")]
        public ActionResult Post(PostModel postModel)
        {
            //通过验证
            if (!CheckSignature.Check(postModel.Signature, postModel.Timestamp, postModel.Nonce, ConfigHelper.Token))
            {
                return Content("参数错误！");
            }
            var messageHandler = new CustomMessageHandler(Request.InputStream, postModel, 10);
            messageHandler.Execute();
            return Content(messageHandler.ResponseDocument.ToString());//v0.7-
        }
    }
}
