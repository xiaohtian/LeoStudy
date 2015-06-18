using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace LeoStudy.Common.Controls
{
    public static class PagerControl
    {
        public static MvcHtmlString Pager(this HtmlHelper helper, int currentPageIndex, int recordCount, int pageSize = 0)
        {
            if (pageSize == 0)
            {
                var pageSizeString = ConfigurationManager.AppSettings["PageSize"];
                int.TryParse(pageSizeString, out pageSize);
            }

            int pageCount = (recordCount % pageSize == 0 ? recordCount / pageSize : recordCount / pageSize + 1);
            var url = new StringBuilder();
            url.Append(HttpContext.Current.Request.Url.AbsolutePath + "?PageIndex={0}");
            NameValueCollection collection = HttpContext.Current.Request.QueryString;
            string[] keys = collection.AllKeys;
            for (int i = 0; i < keys.Length; i++)
            {
                if (keys[i].ToLower() != "pageindex")
                    url.AppendFormat("&{0}={1}", keys[i], collection[keys[i]]);
            }
            var sb = new StringBuilder();
            //sb.AppendFormat("<span>总共{0}条记录,共{1}页,当前第{2}页</span>", recordCount, pageCount, currentPageIndex);
            sb.Append("<nav><ul class='pagination'>");
            if (currentPageIndex == 1)
                sb.Append("<li><span>首页</span></li>");
            else
            {
                string urlFirst = string.Format(url.ToString(), 1);
                sb.AppendFormat("<li><span><a href='{0}'>首页</a></span>&nbsp;</li>", urlFirst);
            }
            if (currentPageIndex > 1)
            {
                string urlPre = string.Format(url.ToString(), currentPageIndex - 1);
                sb.AppendFormat("<li><a href='{0}' aria-label='Previous'><span aria-hidden='true'>&laquo;</span></a></li>", urlPre);
            }
            else
            {
                sb.Append("<li><span aria-hidden='true'>&laquo;</span></li>");
            }

            sb.Append(GetNumericPage(currentPageIndex, pageSize, recordCount, pageCount, url.ToString()));
            if (currentPageIndex < pageCount)
            {
                string urlNext = string.Format(url.ToString(), currentPageIndex + 1);
                sb.AppendFormat("<li><a href='{0}' aria-label='Next'><span aria-hidden='true'>&raquo;</span></a></li>", urlNext);
            }
            else
            {
                sb.Append("<li><span aria-hidden='true'>&raquo;</span></li>");
            }

            if (currentPageIndex == pageCount)
                sb.Append("<li><span>末页</span></li>");
            else
            {
                string urlLast = string.Format(url.ToString(), pageCount);
                sb.AppendFormat("<li><span><a href='{0}'>末页</a></span></li>", urlLast);
            }
            sb.Append("</ul></nav>");
            return new MvcHtmlString(sb.ToString());
        }


        private static string GetNumericPage(int currentPageIndex, int pageSize, int recordCount, int pageCount, string url)
        {
            int k = currentPageIndex / 10;
            int m = currentPageIndex % 10;
            var sb = new StringBuilder();
            if (currentPageIndex / 10 == pageCount / 10)
            {
                if (m == 0)
                {
                    k--;
                    m = 10;
                }
                else
                    m = pageCount % 10;
            }
            else
                m = 10;
            for (int i = k * 10 + 1; i <= k * 10 + m; i++)
            {
                if (i == currentPageIndex)
                    sb.AppendFormat("<li class='active'><span>{0}</span></li>", i);
                else
                {
                    string urlResult = string.Format(url, i);
                    sb.AppendFormat("<li><a href='{0}'>{1}</a></li>", urlResult, i);
                }
            }

            return sb.ToString();
        }
    }
}
