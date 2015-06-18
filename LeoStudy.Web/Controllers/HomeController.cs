using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using LeoStudy.Web.Models;

namespace LeoStudy.Web.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }

        public void Valid(WeChatRequestModel model)
        {
            //获取请求来的echostr参数
            string echoStr = model.Echostr;
            //通过验证
            if (CheckSignature(model))
            {
                if (!String.IsNullOrWhiteSpace(echoStr))
                {
                    //将随机生成的echostr参数原样输出
                    Response.Write(echoStr);
                    //截止输出流
                    Response.End();
                }
            }
        }

        #region Private Methods

        private const string Token = "LeoStudy";

        private bool CheckSignature(WeChatRequestModel model)
        {
            string signature, timestamp, nonce, tempStr;
            //获取请求来的参数
            signature = model.Signature;
            timestamp = model.Timestamp;
            nonce = model.Nonce;
            //创建数组，将Token,timestamp,nonce三个参数加入数组
            string[] array = { Token, timestamp, nonce };
            //进行排序
            Array.Sort(array);
            //拼接为一个字符串
            tempStr = String.Join("", array);
            //对字符串进行SHA1加密
            tempStr = FormsAuthentication.HashPasswordForStoringInConfigFile(tempStr, "SHA1").ToLower();
            //判断signature是否正确
            if (tempStr.Equals(signature))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion

    }
}
