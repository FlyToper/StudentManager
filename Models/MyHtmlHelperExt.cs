using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
//using System.Web.Mvc;

namespace System.Web.Mvc
{
    public static class MyHtmlHelperExt
    {
        


        // shit  <span>shit</sapn>
        public static string MyLable(this HtmlHelper helper,string txt)
        {
            return string.Format("<span style='color:red'>{0}</span>",txt);
        }

        //通过参考微软的代码知道，返回MvcHtmlString会让我们的字符串不被编码化
        public static MvcHtmlString MyMvcHtmlLabel(this HtmlHelper helper, string txt)
        {
            var str = string.Format("<span style='color:red'>{0}</span>", txt);

            //MvcHtmlString mvchtml = new  MvcHtmlString(str);


            return MvcHtmlString.Create(str);

            //创建一个类型的实例：  new，最低级别，一般菜鸟

            // 如果不能new一个实例的时候可以去 看一下此类型里面有没有静态方法，静态方法返回当前类型的实例。

            // 如果没有提供这样的方法，最后一招使用反射。

            // 如果上面还不行，肯定有个工厂返回当前一个实例。

            //return mvchtml;
        }


        /// <summary>
        /// 分页栏展示
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="currentPage">当前页</param>
        /// <param name="pageSize">每页显示的数量</param>
        /// <param name="totalCount">总共的数量</param>
        /// <returns></returns>
        public static HtmlString ShowPageNavigate(this HtmlHelper htmlHelper, int currentPage, int pageSize, int totalCount)
        {
            var redirectTo = htmlHelper.ViewContext.RequestContext.HttpContext.Request.Url.AbsolutePath;
            pageSize = pageSize == 0 ? 3 : pageSize;
            var totalPages = Math.Max((totalCount + pageSize - 1) / pageSize, 1); //总页数
            var output = new StringBuilder();
            if (totalPages > 1)
            {
                //if (currentPage != 1)
                {//处理首页连接
                    output.AppendFormat("<a class='pageLink' href='{0}?pageIndex=1&pageSize={1}'>首页</a> ", redirectTo, pageSize);
                }
                if (currentPage > 1)
                {//处理上一页的连接
                    output.AppendFormat("<a class='pageLink' href='{0}?pageIndex={1}&pageSize={2}'>上一页</a> ", redirectTo, currentPage - 1, pageSize);
                }
                else
                {
                    // output.Append("<span class='pageLink'>上一页</span>");
                }

                output.Append(" ");
                int currint = 5;
                for (int i = 0; i <= 10; i++)
                {//一共最多显示10个页码，前面5个，后面5个
                    if ((currentPage + i - currint) >= 1 && (currentPage + i - currint) <= totalPages)
                    {
                        if (currint == i)
                        {//当前页处理
                            //{0}?pageIndex={1}&pageSize={2}
                            //output.Append(string.Format("[{0}]", currentPage));
                            output.AppendFormat("<a class=' disabled'  href='javascript:void(0);'><span class='currentPage'>{3}</span></a> ", redirectTo, currentPage, pageSize, currentPage);
                            //output.AppendFormat("<a class='active' href='javascript:void(0);'>{0}</a> ", currentPage);
                        }
                        else
                        {//一般页处理
                            output.AppendFormat("<a class='pageLink' href='{0}?pageIndex={1}&pageSize={2}'>{3}</a> ", redirectTo, currentPage + i - currint, pageSize, currentPage + i - currint);
                        }
                    }
                    output.Append(" ");
                }
                if (currentPage < totalPages)
                {//处理下一页的链接
                    output.AppendFormat("<a class='pageLink' href='{0}?pageIndex={1}&pageSize={2}'>下一页</a> ", redirectTo, currentPage + 1, pageSize);
                }
                else
                {
                    //output.Append("<span class='pageLink'>下一页</span>");
                }
                output.Append(" ");
                if (currentPage != totalPages)
                {
                    output.AppendFormat("<a class='pageLink' href='{0}?pageIndex={1}&pageSize={2}'>末页</a> ", redirectTo, totalPages, pageSize);
                }
                output.Append(" ");
            }
            output.AppendFormat("第{0}页 / 共{1}页", currentPage, totalPages);//这个统计加不加都行

            return new HtmlString(output.ToString());
        }


    }
}