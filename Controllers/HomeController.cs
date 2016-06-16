using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using 学生选课信息管理系统.Models;
using System.Web.SessionState;
using System.Data;
using System.Data.SqlClient;

namespace 学生选课信息管理系统.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return RedirectToAction("Login");
        }

        /// <summary>
        /// 展示登录页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Login()
        {
            
            return View();
        }

        /// <summary>
        /// 验证登录
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="usertype">角色</param>
        /// <returns></returns>
        public ActionResult CheckLogin(string username, string password, string usertype)
        {

            if (usertype.Trim() == "1")//游客
            {
                return JavaScript("alert('对不起，本系统暂时不对游客开放，请登录！');return;");
            }
            else if (usertype == "2")//学生
            {
                #region 学生登录检验

                //StuInfo stu = new StuInfo();
                //stu = stu.CheckLogin(username, password);
                
                //string checkLoginResult = s1.CheckLogin(username,password);


                string sql = "Select * from StuInfo where StuNum = @StuNum and  StuPwd = @StuPwd and DelFlag = 0";
                SqlParameter[] param = {
                                           new SqlParameter("@StuNum",username),
                                           new SqlParameter("@StuPwd",password)
                                       };

                string checkLoginResult = MyPublicClass.CheckLogin(sql, param);
               

                if (checkLoginResult == "Success")
                {
                    //ViewBag.Stu = stu; 
                    Session["UserType"] = "Student";
                    Session["UserNum"] = username;

                    return Content("Student_Success");
                }
                else if (checkLoginResult == "Error_State")//该用户被禁用，请与管理员联系！
                {
                    return Content("Error2");
                }
                else if (checkLoginResult == "Error_UserNotExist")//用户名或者密码错误！
                {
                    return Content("Error1");
                }
                else if (checkLoginResult == "Error_Check")//登录验证出错，返回404
                {
                    return Content("Error3");
                }
                else if (checkLoginResult == "ReadyToCheck")//待检验状态
                {
                    return Content("Error4");
                }
                else
                {
                    return Content("Error5");
                }

                #endregion

            }
            else if (usertype == "3")//教师
            {
                #region 教师登录检验

                //验证登录的SQL语句
                string sql = "Select * from TeacherInfo where TeacherNum = @TeacherNum and  TeacherPwd = @TeacherPwd and DelFlag = 0";
                
                //参数列表
                SqlParameter[] param = {
                                           new SqlParameter("@TeacherNum",username),
                                           new SqlParameter("@TeacherPwd",password)
                                       };

                //接收登录验证的结果
                string checkLoginResult = MyPublicClass.CheckLogin(sql, param);

                //对结果进行检查
                if (checkLoginResult == "Success")
                {
                    //ViewBag.Stu = stu; 
                    Session["UserType"] = "Teacher";
                    Session["UserNum"] = username;

                    return Content("Teacher_Success");
                }
                else if (checkLoginResult == "Error_State")//该用户被禁用，请与管理员联系！
                {
                    return Content("Error2");
                }
                else if (checkLoginResult == "Error_UserNotExist")//用户名或者密码错误！
                {
                    return Content("Error1");
                }
                else if (checkLoginResult == "Error_Check")//登录验证出错，返回404
                {
                    return Content("Error3");
                }
                else if (checkLoginResult == "ReadyToCheck")//待检验状态
                {
                    return Content("Error4");
                }
                else
                {
                    return Content("Error5");
                }

                #endregion

            }
            else if (usertype == "4")//管理员
            {
                #region 管理员登录检验

                //验证登录的SQL语句
                string sql = "Select * from AdminInfo where AdminNum = @AdminNum and  AdminPwd = @AdminPwd and DelFlag = 0";

                //参数列表
                SqlParameter[] param = {
                                           new SqlParameter("@AdminNum",username),
                                           new SqlParameter("@AdminPwd",password)
                                       };

                //接收登录验证的结果
                string checkLoginResult = MyPublicClass.CheckLogin(sql, param);

                //对结果进行检查
                if (checkLoginResult == "Success")
                {
                  
                    Session["UserType"] = "Admin";
                    Session["UserNum"] = username;

                    return Content("Admin_Success");
                }
                else if (checkLoginResult == "Error_State")//该用户被禁用，请与管理员联系！
                {
                    return Content("Error2");
                }
                else if (checkLoginResult == "Error_UserNotExist")//用户名或者密码错误！
                {
                    return Content("Error1");
                }
                else if (checkLoginResult == "Error_Check")//登录验证出错，返回404
                {
                    return Content("Error3");
                }
                else if (checkLoginResult == "ReadyToCheck")//待检验状态
                {
                    return Content("Error4");
                }
                else
                {
                    return Content("Error5");
                }

                #endregion

                
            }
            else
            {
                return View();
            }

            

            
        }


     
        /// <summary>
        /// 【退出】
        /// </summary>
        /// <returns></returns>
        public ActionResult Exist()
        {
            Session.Remove("UserNum");
            Session.Remove("UserType");
            Session.Remove("AdminLevel");

            return RedirectToAction("Login");
        }

        public ActionResult Error404()
        {
            string requesUrl = Request.Url.ToString();
            ViewData["requestUrl"] = requesUrl;
            return View();
        }

    }
}
