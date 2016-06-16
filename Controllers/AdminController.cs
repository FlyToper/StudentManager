using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using 学生选课信息管理系统.Models;
using System.Text;
using NPOI;
using NPOI.HSSF.UserModel;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.HSSF.Util;
using 学生选课信息管理系统.Common;

namespace 学生选课信息管理系统.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/

        /// <summary>
        /// 管理员的【首页】
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            if (Session["UserType"] != null && Session["UserType"].ToString() == "Admin")
            {
                if (Session["UserNum"] != null)
                {
                    AdminInfo a1 = AdminInfo.GetAdminInfo(Session["UserNum"].ToString());

                    if (a1.Level == "超级管理员")//超级管理员
                    {
                        Session["AdminLevel"] = "0";
                    }
                    else//普通管理员
                    {
                        Session["AdminLevel"] = "1";
                    }
                    ViewBag.Admin = a1;
                    return View();
                }
                else
                {
                    return RedirectToAction("../Home/Login");
                }
            }
            else
            {
                return RedirectToAction("../Home/Login");
            }
        }


        /// <summary>
        /// 【管理员】更新个人信息
        /// </summary>
        /// <param name="phone">联系电话号码</param>
        /// <param name="email">邮箱</param>
        /// <returns></returns>
        public ActionResult UpdateAdminInfo(string phone, string email)
        {
            //①首先判断是否登陆
            if (Session["UserNum"] != null)
            {
                //②然后判断用户类型
                if (Session["UserType"] != null && Session["UserType"].ToString() == "Admin")
                {
                    //③判断获取的数据是否合法
                    #region 执行修改操作
                    try
                    {
                        string sql = "";
                        if (!string.IsNullOrEmpty(phone) && !string.IsNullOrEmpty(email))
                        {
                            sql = "Update AdminInfo set Phone = @Phone , Email = @Email where AdminNum = @AdminNum";

                        }
                        else
                        {
                            return Content("error");
                        }




                        if (sql != "")
                        {
                            SqlParameter[] param = { 
                                                       new SqlParameter("@Phone",phone),
                                                       new SqlParameter("@Email",email),
                                                       new SqlParameter("@AdminNum",Session["UserNum"].ToString())
                                                   };
                            if (SqlHelper.ExecuteNonQuery(sql, param) > 0)
                            {
                                return Content("success");
                            }
                            else
                            {
                                return Content("error");
                            }
                        }
                        else
                        {
                            return Content("error");
                        }
                    }//
                    catch
                    {
                        return RedirectToAction("../Home/Error404");
                    }
                    #endregion

                }
                else
                {
                    return RedirectToAction("../Home/Login");
                }
            }
            else
            {
                return RedirectToAction("../Home/Login");
            }
        }

        /// <summary>
        /// 【用户管理页面】
        /// </summary>
        /// <returns></returns>
        public ActionResult UserManager()
        {
            if (Session["UserType"] != null && Session["UserType"].ToString() == "Admin")
            {
                if (Session["UserNum"] != null)
                {
                    //①加载管理员的个人信息
                    AdminInfo a1 = AdminInfo.GetAdminInfo(Session["UserNum"].ToString());
                    ViewBag.Admin = a1;

                    //②加载所有待验证的注册用户的信息
                    List<AllUserInfo> AllList = AllUserInfo.GetAllRegisterUserInfo(Session["AdminLevel"].ToString());
                    int pageSize = Request["pageSize"] == null ? 10 : int.Parse(Request["pageSize"]);
                    int pageIndex = Request["pageIndex"] == null ? 1 : int.Parse(Request["pageIndex"]);

                    int total = AllList.Count;
                    ViewData["pageSize"] = pageSize;
                    ViewData["pageIndex"] = pageIndex;
                    ViewData["total"] = total;
                    List<AllUserInfo> RetList = new List<AllUserInfo>();
                    for (int i = pageSize * (pageIndex - 1); i < pageSize*(pageIndex - 1)+ pageSize  ; i++)
                    {
                        if (i == AllList.Count )
                        {
                            break;
                        }
                        RetList.Add(AllList[i]);
                            
                    }
                    ViewBag.AllRegisterUser = RetList;
                    return View();
                }
                else
                {
                    return RedirectToAction("../Home/Login");
                }
            }
            else
            {
                return RedirectToAction("../Home/Login");
            }
        }

        /// <summary>
        /// 管理员获取【所有准备注册用户信息】
        /// </summary>
        /// <returns></returns>
        public ActionResult GetAllRegisterUserInfo()
        {
            if (Session["UserType"] != null && Session["UserType"].ToString() == "Admin")
            {
                if (Session["UserNum"] != null)
                { 
                    /*
                     * 这里除了一次性第一次无条件查询之外，还可以处理根据不同条件去查询
                     * 
                     * searchWay（0--无条件查询，即系查询所有，1--按角色查询，2--按账号查询，3---按名字查询）
                     * condition（0---无条件查询.。如果按角色查询（1---按学生角色查询，2---按教师角色查询，3--按管理员角色查询）。如果是按账号查询，则此时的condition就是需要查询的账号。如果为按名字查询，则此时的condition就是需要查询的名字）
                     * 
                     */
                    //②加载【所有待验证的注册用户】的信息
                    List<AllUserInfo> AllList = AllUserInfo.GetAllRegisterUserInfo(Session["AdminLevel"].ToString());
                    List<AllUserInfo> AllList2 = new List<AllUserInfo>();//放筛选之后的数据
                    int pageSize = Request["pageSize"] == null ? 10 : int.Parse(Request["pageSize"]);
                    int pageIndex = Request["pageIndex"] == null ? 1 : int.Parse(Request["pageIndex"]);
                    
                    //判断是否是条件筛选
                    string searchWay = Request["searchWay"];
                    string condtition = Request["condition"];
                    if (!string.IsNullOrEmpty(searchWay) && !string.IsNullOrEmpty(condtition))//条件筛选
                    {
                        int k1 = 1;//记录序号
                        if (searchWay == "0" && condtition == "0")//都是0的代表是无条件查询
                        {
                            AllList2 = AllList;//无条件查询
                        }
                        else if (searchWay == "1")//按角色去筛选
                        {
                            #region 筛选数据
                            string selectCondition;
                            
                            if (condtition == "1")//【学生】
                            {
                                selectCondition = "学生";
                            }
                            else if (condtition == "2")//【教师】
                            {
                                selectCondition = "教师";
                            }
                            else if (condtition == "3")//【管理员】
                            {
                                selectCondition = "管理员";
                            }
                            else
                            {
                                return Content("没找到相关数据！");
                            }

                            //遍历，筛选数据
                            foreach (AllUserInfo use in AllList)
                            {
                                if (use.Role == selectCondition)
                                {
                                    use.ListID = k1;
                                    AllList2.Add(use);
                                    k1++;
                                }
                            }
                            #endregion

                        }
                        else if (searchWay == "2" && !string.IsNullOrEmpty(Request["selectCondition"].ToString()))//按账号查询
                        {
                           
                            #region 遍历，筛选数据
                            string selectCondition = Request["selectCondition"];
                            foreach (AllUserInfo use in AllList)
                            {
                                if (use.UserNum == selectCondition)
                                {
                                    use.ListID = k1;
                                    AllList2.Add(use);
                                    k1++;
                                }
                            }
                            #endregion
                        }
                        else if (searchWay == "3" && !string.IsNullOrEmpty(Request["selectCondition"].ToString()))//按名字进行查找
                        {
                            #region 遍历，筛选数据
                            string selectCondition = Request["selectCondition"];
                            foreach (AllUserInfo use in AllList)
                            {
                                if (use.UserName == selectCondition)
                                {
                                    use.ListID = k1;
                                    AllList2.Add(use);
                                    k1++;
                                }
                            }
                            #endregion
                        }
                        else
                        {
                            return Content("没找到相关数据！");
                        }
                    }
                    else
                    {
                        //searchWay = " ";
                        //condtition = " ";
                        //AllList2 = AllList;
                    }

                    int total = AllList2.Count;//获取筛选之后的全部数据总的个数
                    ViewData["pageSize"] = pageSize;//每一页显示的条数
                    ViewData["pageIndex"] = pageIndex;//当前需要显示的页码
                    ViewData["total"] = total;

                    List<AllUserInfo> RetList = new List<AllUserInfo>();//这个列表放需要显示页的数据
                    for (int i = pageSize * (pageIndex - 1); i < pageSize * (pageIndex - 1) + pageSize; i++)//根据页码和页的大小截取数据记录，然后放在RetList中
                    {
                        if (i == AllList2.Count)//如果到达列表结尾，直接跳出
                        {
                            break;
                        }
                        RetList.Add(AllList2[i]);

                    }

                    var json = new//格式化返回数据，转换成json
                    {
                        Total = RetList.Count,//返回数据的条数
                        Row = RetList.ToList(),//返回数据集合
                        PageNumber = Page.ShowPageNavigate(pageIndex,pageSize,total,searchWay,condtition)//这个方法为分页导航条的字符串
                    };

                    
                    return Json(json, JsonRequestBehavior.AllowGet);//返回数据
                }
                else
                {
                    return RedirectToAction("../Home/Login");
                }
            }
            else
            {
                return RedirectToAction("../Home/Login");
            }
        }


        /// <summary>
        /// 【删除】和【通过】注册用户
        /// </summary>
        /// <param name="method">方法选择</param>
        /// <param name="userNum">用户账号</param>
        /// <param name="role">角色</param>
        /// <returns></returns>
        public ActionResult DeletAndPassRegisterUserInfo(string method, string userNum, string role)
        {
            if (Session["UserType"] != null && Session["UserType"].ToString() == "Admin")
            {
                if (Session["UserNum"] != null)
                {
                    try
                    {
                        if (!string.IsNullOrEmpty(method))
                        {
                            if (method == "Delete")//删除注册用户【软删除】
                            {
                                return Content(AllUserInfo.DeleteRegisterUser(userNum, role));//执行删除注册用户信息
                            }
                            else if (method == "Pass")//通过检验
                            {
                                return Content(AllUserInfo.PassRegisterUser(userNum, role));//执行通过注册用户
                            }
                            else
                            {
                                return Content("Error");
                            }
                        }
                        else
                        {
                            return Content("Error");
                        }
                    }
                    catch
                    {
                        return RedirectToAction("../Home/Error404");
                    }
                }
                else
                {
                    return RedirectToAction("../Home/Login");
                }
            }
            else
            {
                return RedirectToAction("../Home/Login");
            }
        }

        //禁用的方法
        //private FileResult ExportExcel()
        //{
        //    var sbHtml = new StringBuilder();
        //    sbHtml.Append("<table border='1' cellspacing='0' cellpadding='0'>");
        //    sbHtml.Append("<tr>");
        //    var lstTitle = new List<string> { "编号", "姓名", "年龄", "创建时间" };
        //    foreach (var item in lstTitle)
        //    {
        //        sbHtml.AppendFormat("<td style='font-size: 14px;text-align:center;background-color: yellow; font-weight:bold;' height='25'>{0}</td>", item);
        //    }
        //    sbHtml.Append("</tr>");

        //    for (int i = 0; i < 1000; i++)
        //    {
        //        sbHtml.Append("<tr>");
        //        sbHtml.AppendFormat("<td style='font-size: 12px;height:20px;'>{0}</td>", i);
        //        sbHtml.AppendFormat("<td style='font-size: 12px;height:20px;'>屌丝{0}号</td>", i);
        //        sbHtml.AppendFormat("<td style='font-size: 12px;height:20px;'>{0}</td>", new Random().Next(20, 30) + i);
        //        sbHtml.AppendFormat("<td style='font-size: 12px;height:20px;'>{0}</td>", DateTime.Now);
        //        sbHtml.Append("</tr>");
        //    }
        //    sbHtml.Append("</table>");

        //    //第一种:使用FileContentResult
        //    //byte[] fileContents = Encoding.Default.GetBytes(sbHtml.ToString());
        //    byte[] fileContents = Encoding.UTF8.GetBytes(sbHtml.ToString());
        //    return File(fileContents, "application/ms-excel", "fileContents.xls");

        //    ////第二种:使用FileStreamResult
        //    //var fileStream = new MemoryStream(fileContents);
        //    //return File(fileStream, "application/ms-excel", "fileStream.xls");

        //    ////第三种:使用FilePathResult
        //    ////服务器上首先必须要有这个Excel文件,然会通过Server.MapPath获取路径返回.
        //    //var fileName = Server.MapPath("~/Files/fileName.xls");
        //    //return File(fileName, "application/ms-excel", "fileName.xls");
        //}


        /// <summary>
        /// 【下载】个人信息
        /// </summary>
        /// <returns></returns>
        public ActionResult DownLoadSelfInfon()
        {
            if (Session["UserType"] != null && Session["UserType"].ToString() == "Admin")
            {
                if (Session["UserNum"] != null)
                {
                    AdminInfo a1 = AdminInfo.GetAdminInfo(Session["UserNum"].ToString());
                    List<AdminInfo> List = new List<AdminInfo>();
                    List.Add(a1);
                    byte[] fileContents = DownLoadExcel.Admin_ExecuteDownLoad(List);

                    //Response.AddHeader("Content-Disposition", string.Format("attachment; filename={0}个人信息表.xls", a1.AdminName));
                    //Response.BinaryWrite(fileContents);

                    return File(fileContents, "application/ms-excel", string.Format("{0}个人信息表{1}.xls", a1.AdminName, DateTime.Now.ToShortDateString()));
                }
                else
                {
                    return RedirectToAction("../Home/Login");
                }
            }
            else
            {
                return RedirectToAction("../Home/Login");
            }

             
        }

    }
}
