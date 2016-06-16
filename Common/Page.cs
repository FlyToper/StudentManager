using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace 学生选课信息管理系统.Common
{
    public class Page
    {
        public static string ShowPageNavigate( int currentPage, int pageSize, int totalCount,string searchWay, string condition)
        {
            pageSize = pageSize == 0 ? 3 : pageSize;
            var totalPages = Math.Max((totalCount + pageSize - 1) / pageSize, 1); //总页数
            var output = new StringBuilder();
            if (totalPages > 1)
            {
                //if (currentPage != 1)
                {//处理首页连接
                    output.AppendFormat("<a class='pageLink' href='javascript:void(0);' onclick='LoadRegisterUserInfo({0},{1},{2},{3})'>首页</a> ", 1, pageSize,searchWay, condition);
                }
                if (currentPage > 1)
                {//处理上一页的连接

                    output.AppendFormat("<a class='pageLink' href='javascript:void(0);' onclick='LoadRegisterUserInfo({0},{1},{2},{3})'>上一页</a> ", currentPage - 1, pageSize,searchWay, condition);
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
                            output.AppendFormat("<a   href='javascript:void(0);'><span class='currentPage'>{0}</span></a> ", currentPage);
                            //output.AppendFormat("<a class='active' href='javascript:void(0);'>{0}</a> ", currentPage);
                        }
                        else
                        {//一般页处理

                            output.AppendFormat("<a class='pageLink'  href='javascript:void(0);' onclick='LoadRegisterUserInfo({0},{1},{2},{3})'>{4}</a> ", currentPage + i - currint, pageSize,searchWay, condition, currentPage + i - currint);
                        }
                    }
                    output.Append(" ");
                }
                if (currentPage < totalPages)
                {//处理下一页的链接

                    output.AppendFormat("<a class='pageLink' href='javascript:void(0);' onclick='LoadRegisterUserInfo({0},{1},{2},{3})'>下一页</a> ",  currentPage + 1, pageSize,searchWay,condition);
                }
                else
                {
                    //output.Append("<span class='pageLink'>下一页</span>");
                }
                output.Append(" ");
                if (currentPage != totalPages)
                {

                    output.AppendFormat("<a class='pageLink' href='javascript:void(0);' onclick='LoadRegisterUserInfo({0},{1},{2},{3})'>末页</a> ", totalPages, pageSize,searchWay,condition);
                }
                output.Append(" ");
            }
            output.AppendFormat("第{0}页 / 共{1}页", currentPage, totalPages);//这个统计加不加都行

            return output.ToString();
        }
    }
}