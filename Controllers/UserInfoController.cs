using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using 学生选课信息管理系统.Models;
using System.Web.SessionState;
using System.Data.SqlClient;
using System.Data;
using System.Web.Script.Serialization;

namespace 学生选课信息管理系统.Controllers
{
    public class UserInfoController : Controller
    {
        //
        // GET: /UserInfo/

        //public ActionResult Index()
        //{
        //    return View();
        //}
        /// <summary>
        /// 展示学生基本信息
        /// </summary>
        /// <returns>学生信息页面视图</returns>
        public ActionResult Student()
        {
            if (Session["UserType"] != null && Session["UserType"].ToString() == "Student")
            {
                StuInfo s1 = new StuInfo();
                if (Session["UserNum"] != null)
                {
                    s1 = StuInfo.GetStuInfo(Session["UserNum"].ToString());
                    ViewBag.Stu = s1;
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
        /// 密码修改页面html字符串
        /// </summary>
        /// <returns></returns>
        public ActionResult HtmlStringUpdatePwd()
        {
            //修改密码页面的部分代码
            string htmlStr = @"<form id='UpdatePwdForm'><div class='myTr myMargin '><span><span class='myLabel'>旧&nbsp;&nbsp;&nbsp;&nbsp;密&nbsp;&nbsp;&nbsp;&nbsp;码：</span><span><input id='txtOldPwd' class='myInput myBlackColor' type='password' name='oldPwd' ></span></span></div><div class='myTr'><span><span class='myLabel'>新&nbsp;&nbsp;&nbsp;&nbsp;密&nbsp;&nbsp;&nbsp;&nbsp;码：</span><span><input id='txtNewPwd1' type='password' class='myInput' name='newPwd1'></span></span></div><div class='myTr '><span><span class='myLabel'>再输入密码：</span><span><input id='txtNewPwd2' type='password' class='myInput' name='newPwd2'></span></span></div> <div class='myTr'><input type='button' class='myBtn2' onclick='cancelBtn()' value='取消'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input type='button' class='myBtn2' onclick='subBtn()' value='确认'> </div></form> <div class='myTr' id='errorMsg'></div>";

            return Content(htmlStr);  
        }


        /// <summary>
        /// 更改用户密码
        /// </summary>
        /// <param name="oldPwd">旧密码</param>
        /// <param name="newPwd1">新密码1</param>
        /// <param name="newPwd2">新密码2</param>
        /// <returns></returns>
        public ActionResult UpdateUserPwd(string oldPwd, string newPwd1, string newPwd2)
        {
            if (Session["UserType"] != null && Session["UserType"].ToString() == "Student")//学生更改密码
            {
                if (Session["UserNum"] != null)
                {
                    #region 执行学生更改密码操作
                    if (oldPwd != "" && newPwd1 != "" && newPwd2 != "")//判断从客户端发送的数据是否为空
                    {
                        if (newPwd1 == newPwd2)//判断2次新密码是否一致
                        {
                            string sql = "Update StuInfo set StuPwd = @newStuPwd where DelFlag = 0 and StuNum = @StuNum and StuPwd = @oldStuPwd and State = @State";//更新密码的sql语句
                            SqlParameter[] param =  {
                            new SqlParameter("@newStuPwd",newPwd1),
                            new SqlParameter("@StuNum",Session["UserNum"].ToString()),
                            new SqlParameter("@oldStuPwd",oldPwd),
                            new SqlParameter("@State","正常")
                            };//参数列表

                            try
                            {
                                int k = MyPublicClass.UpdateUserPwd(sql, param);//执行数据库更新操作，返回结果是受影响行数，大于0代表成功
                                if (k == 1)
                                {
                                    return Content("Success");
                                }
                                else
                                {
                                    return Content("Error2");
                                }
                            }
                            catch
                            {
                                return RedirectToAction("../Home/Error404");
                            }

                        }
                        else
                        {
                            return Content("Error1");//表单出错
                        }
                    }
                    else
                    {
                        return Content("Error1");//表单出错
                    }
                    #endregion
                }
                else
                {
                    return RedirectToAction("../Home/Login");
                }
            }
            else if (Session["UserType"] != null && Session["UserType"].ToString() == "Teacher")//教师更改密码
            {
                if (Session["UserNum"] != null)
                {
                    #region 执行教师更改密码操作
                    if (oldPwd != "" && newPwd1 != "" && newPwd2 != "")
                    {
                        if (newPwd1 == newPwd2)
                        {
                            string sql = "Update TeacherInfo set TeacherPwd = @newTeacherPwd where DelFlag = 0 and TeacherNum = @TeacherNum and TeacherPwd = @oldTeacherPwd and State = @State";
                            SqlParameter[] param =  {
                            new SqlParameter("@newTeacherPwd",newPwd1),
                            new SqlParameter("@TeacherNum",Session["UserNum"].ToString()),
                            new SqlParameter("@oldTeacherPwd",oldPwd),
                            new SqlParameter("@State","正常")
                            };

                            try
                            {
                                int k = MyPublicClass.UpdateUserPwd(sql, param);
                                if (k == 1)
                                {
                                    return Content("Success");
                                }
                                else
                                {
                                    return Content("Error2");
                                }
                            }
                            catch (Exception e)
                            {
                                throw e;
                            }

                        }
                        else
                        {
                            return Content("Error1");//表单出错
                        }
                    }
                    else
                    {
                        return Content("Error1");//表单出错
                    }
                    #endregion
                }
                else
                {
                    return RedirectToAction("../Home/Login");
                }
            }
            else if (Session["UserType"] != null && Session["UserType"].ToString() == "Admin")
            {
                if (Session["UserNum"] != null)
                {
                    #region 执行管理员更改密码操作
                    if (oldPwd != "" && newPwd1 != "" && newPwd2 != "")
                    {
                        if (newPwd1 == newPwd2)
                        {
                            string sql = "Update AdminInfo set AdminPwd = @newAdminPwd where DelFlag = 0 and AdminNum = @AdminNum and AdminPwd = @oldAdminPwd and State = @State";
                            SqlParameter[] param =  {
                            new SqlParameter("@newAdminPwd",newPwd1),
                            new SqlParameter("@AdminNum",Session["UserNum"].ToString()),
                            new SqlParameter("@oldAdminPwd",oldPwd),
                            new SqlParameter("@State","正常")
                            };

                            try
                            {
                                int k = MyPublicClass.UpdateUserPwd(sql, param);
                                if (k == 1)
                                {
                                    return Content("Success");
                                }
                                else
                                {
                                    return Content("Error2");
                                }
                            }
                            catch (Exception e)
                            {
                                throw e;
                            }

                        }
                        else
                        {
                            return Content("Error1");//表单出错
                        }
                    }
                    else
                    {
                        return Content("Error1");//表单出错
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
        /// 展示学生成绩信息
        /// </summary>
        /// <returns>学生成绩视图</returns>
        public ActionResult ScoreInfo()
        {
            if (Session["UserType"] != null && Session["UserType"].ToString() == "Student")
            {
                if (Session["UserNum"] != null)
                {
                    StuInfo s1 = new StuInfo();
                    s1 = StuInfo.GetStuInfo(Session["UserNum"].ToString());
                    ViewBag.Stu = s1;

                    List<GetStuScore> list = null;
                    list = GetStuScore.GetStuScoreInfoAsList(Session["UserNum"].ToString());
                    if (list != null)
                    {
                        ViewBag.StuScoreList = list;
                        return View();
                    }
                    else
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

        /// <summary>
        /// 选课课程信息展示
        /// </summary>
        /// <returns></returns>
        public ActionResult ShowSelectClassInfo()
        {
            if (Session["UserType"] != null && Session["UserType"].ToString() == "Student")
            {
                if (Session["UserNum"] != null)
                {
                    //学生信息展示
                    StuInfo s1 = new StuInfo();
                    s1 = StuInfo.GetStuInfo(Session["UserNum"].ToString());
                    ViewBag.Stu = s1;

                    //①展示选课课程信息
                    List<CourseInfo> list = null;
                    list = CourseInfo.ShowCourseInfo();

                    //②展示学生已经选好的课程信息
                    List<SelectClassInfo> list2 = null;
                    list2 = SelectClassInfo.GetStuSelectClassInfoByStuNum(Session["UserNum"].ToString());
                    if (list != null && list2 != null)
                    {
                        ViewBag.CourseInfo = list;//选课课程信息
                        ViewBag.SelectClassInfo = list2;//学生已选课程信息
                        return View();
                    }
                    else
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

        /// <summary>
        /// 退选
        /// </summary>
        /// <param name="courseNum">课程编号</param>
        /// <returns></returns>
        public ActionResult DeleteSelectClass(string courseNum)
        {
            if (Session["UserType"] != null && Session["UserType"].ToString() == "Student")
            {
                if (Session["UserNum"] != null)
                {
                    if (SelectClassInfo.DeleteSelectClassByCourseNum(Session["UserNum"].ToString(),courseNum) > 0)
                    {
                        //return RedirectToAction("ShowSelectClassInfo");
                        return Content("Success");
                    }
                    else
                    {
                        return Content("Error");
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


        

        /// <summary>
        /// 插入选课信息
        /// </summary>
        /// <param name="courseNum">课程号</param>
        /// <param name="teacherNum">教师编号</param>
        /// <returns></returns>
        public ActionResult InsertSelectClass( string courseNum, string teacherNum)
        {
            if (Session["UserType"] != null && Session["UserType"].ToString() == "Student")
            {
                if (Session["UserNum"] != null)
                {
                    int k = SelectClassInfo.ExecuteSelelctClass(Session["UserNum"].ToString(), courseNum, teacherNum);

                    if (k > 0)
                    {
                        //return Content("选课成功！");
                        return Content("Success");
                    }
                    else if (k == -1)//已存在的选课信息
                    {
                        //return Content("课程已经选过了");
                        return Content("Error1");
                    }
                    else// k == 0,选课失败或者插入异常
                    {
                        //return Content("选课失败");
                        return Content("Error2");
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


        //---------------------------------公共【Public】的Action（开始）-----------------------------------//

        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="usertype">用户类型</param>
        /// <param name="item">修改项</param>
        /// <param name="data">新的值</param>
        /// <returns></returns>
        public ActionResult UpdateUserInfo(string usertype, string item, string data)
        {
            //①首先判断是否登陆
            if (Session["UserNum"] != null)
            {
                //②然后判断用户类型
                if (Session["UserType"] != null && Session["UserType"].ToString() == "Teacher" && usertype == "Teacher")
                {
                    //③判断获取的数据是否合法
                    #region 执行修改操作
                    try
                    {
                        string sql = "";

                        if (item == "Email")
                        {
                            sql = "Update TeacherInfo set Email = @data where TeacherNum = @TeacherNum and DelFlag = 0";

                        }
                        else if (item == "Phone")
                        {
                            sql = "Update TeacherInfo set Phone =@data where TeacherNum = @TeacherNum and DelFlag = 0";
                        }
                        else if (item == "CourseName1")
                        {
                            sql = "Update TeacherInfo set CourseName1 =@data where TeacherNum = @TeacherNum and DelFlag = 0";
                        }
                        else if (item == "CourseName2")
                        {
                            sql = "Update TeacherInfo set CourseName2 =@data where TeacherNum = @TeacherNum and DelFlag = 0";
                        }
                        else if (item == "CourseName3")
                        {
                            sql = "Update TeacherInfo set CourseName3 =@data where TeacherNum = @TeacherNum and DelFlag = 0";
                        }

                        if (sql != "")
                        {
                            SqlParameter[] param = { 
                                                       new SqlParameter("@data",data),
                                                       new SqlParameter("@TeacherNum",Session["UserNum"].ToString())
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
                else if (Session["UserType"] != null && Session["UserType"].ToString() == "Teacher" && usertype == "Student")
                {
                    //③判断获取的数据是否合法
                    #region 执行修改操作
                    try
                    {
                        string sql = "";

                        if (item == "Email")
                        {
                            sql = "Update StuInfo set Email = @data where StuNum = @StuNum and DelFlag = 0";

                        }
                        else if (item == "Phone")
                        {
                            sql = "Update StuInfo set Phone =@data where StuNum = @StuNum and DelFlag = 0";
                        }
                        else
                        {
                            return Content("error");
                        }

                        if (sql != "")
                        {
                            SqlParameter[] param = { 
                                                       new SqlParameter("@data",data),
                                                       new SqlParameter("@StuNum",Session["UserNum"].ToString())
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
        /// 【学生】更新个人信息
        /// </summary>
        /// <param name="phone">联系电话号码</param>
        /// <param name="email">邮箱</param>
        /// <returns></returns>
        public ActionResult UpdateStuInfo(string phone, string email)
        {
            //①首先判断是否登陆
            if (Session["UserNum"] != null)
            {
                //②然后判断用户类型
                if (Session["UserType"] != null && Session["UserType"].ToString() == "Student")
                {
                    //③判断获取的数据是否合法
                    #region 执行修改操作
                    try
                    {
                        string sql = "";
                        if (!string.IsNullOrEmpty(phone) && !string.IsNullOrEmpty(email))
                        {
                            sql = "Update StuInfo set Phone = @Phone , Email = @Email where StuNum = @StuNum";

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
                                                       new SqlParameter("@StuNum",Session["UserNum"].ToString())
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
        /// 【教师】更改个人信息
        /// </summary>
        /// <param name="phone">联系电话</param>
        /// <param name="email">邮箱</param>
        /// <param name="courseName1">主讲课程一</param>
        /// <param name="courseName2">主讲课程二</param>
        /// <param name="courseName3">主讲课程三</param>
        /// <returns></returns>
        public ActionResult UpdateTeacherInfo(string phone, string email ,string courseName1, string courseName2, string courseName3)
        {
            //①首先判断是否登陆
            if (Session["UserNum"] != null)
            {
                //②然后判断用户类型
                if (Session["UserType"] != null && Session["UserType"].ToString() == "Teacher")
                {
                    //③判断获取的数据是否合法
                    #region 执行修改操作
                    try
                    {
                        if (string.IsNullOrEmpty(courseName1))//主讲课程一
                        {
                            courseName1 = "";
                        }
                        if (string.IsNullOrEmpty(courseName2))//主讲课程二
                        {
                            courseName2 = "";
                        }
                        if (string.IsNullOrEmpty(courseName3))//主讲课程三
                        {
                            courseName3 = "";
                        }
                        string sql = "";
                        if (!string.IsNullOrEmpty(phone) && !string.IsNullOrEmpty(email))
                        {
                            sql = "Update TeacherInfo set Phone = @Phone , Email = @Email,CourseName1 = @CourseName1, CourseName2 = @CourseName2, CourseName3 = @CourseName3 where TeacherNum = @TeacherNum";

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
                                                       new SqlParameter("@TeacherNum",Session["UserNum"].ToString()),
                                                       new SqlParameter("@CourseName1",courseName1),
                                                       new SqlParameter("@CourseName2",courseName2),
                                                       new SqlParameter("@CourseName3",courseName3)
                  
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
        /// 检查提交的表单
        /// </summary>
        /// <param name="form">用户提交的表单</param>
        /// <returns></returns>
        private bool CheckCourseInfoForm(FormCollection form)
        {
            if (form["CourseName"] != "" && form["CourseType"] != "" && form["CheckType"] != "" && form["Credit"] != "" && form["Count"] != "" && form["ClassRoom"] != "" && form["BeginClassYear"] != "" && form["EndClassYear"] != "" && form["BeginWeek"] != "" && form["EndWeek"] != "" && form["ClassDay"] != "" && form["BeginClassNumber"] != "" && form["EndClassNumber"] != "" && form["BeginClassYear"] != "" && form["BeginClassMonth"] != "" && form["BeginClassDay"] != "" && form["BeginClassHour"] != "" && form["BeginClassMinute"] != ""  && form["EndClassYear"] != "" && form["EndClassMonth"] != "" && form["EndClassDay"] != "" && form["EndClassHour"] != "" && form["EndClassMinute"] != "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 把表单转换成类
        /// </summary>
        /// <param name="form">表单</param>
        /// <returns></returns>
        private CourseInfo FormToCourseInfo(FormCollection form)
        {
            CourseInfo c1 = new CourseInfo();

            //验证填装课程信息
            if (CheckCourseInfoForm(form))
            {
                #region 用户信息填装
                //①用户名
                c1.CourseName = form["CourseName"];

                //课程类型
                if (Convert.ToInt32(form["CourseType"]) == 1)
                {
                    c1.CourseType = "必修课";
                }
                else if (Convert.ToInt32(form["CourseType"]) == 2)
                {
                    c1.CourseType = "选修课";
                }
                else if (Convert.ToInt32(form["CourseType"]) == 3)
                {
                    c1.CourseType = "通识课";
                }
                else
                {
                    c1.CourseType = "";
                }

                //考察方式
                if (Convert.ToInt32(form["CheckType"]) == 1)
                {
                    c1.CheckWay = "考试";
                }
                else if (Convert.ToInt32(form["CheckType"]) == 2)
                {
                    c1.CheckWay = "考察";
                }
                else if (Convert.ToInt32(form["CheckType"]) == 3)
                {
                    c1.CheckWay = "其他";
                }
                else
                {
                    c1.CheckWay = "";
                }

                //学分
                c1.Credit = Convert.ToInt32(form["Credit"]);

                //人数

                c1.Count = Convert.ToInt32(form["Count"]);

                //已选人数，初始值
                c1.SelectCount = 0;

                //教师编号
                c1.TeacherNum = Session["UserNum"].ToString();

                //教室
                c1.ClassRoom = form["ClassRoom"];

                //上课时间1
                //ClassDate -------- （20142015020218）(30)

                c1.ClassDate = form["BeginClassYear"] + form["EndClassYear"] + AddZero(form["Team"]) + AddZero(form["BeginWeek"]) + AddZero(form["EndWeek"]);

                //上课时间2
                //ClassTime -------- （周4第9,10节|周）
                c1.ClassTime = "周" + form["ClassDay"] + "第" + form["BeginClassNumber"] + "," + form["EndClassNumber"] + "节|周";

                //选课时间
                //SelectDate ------- （201509181830-201509201830）


                c1.SelectDate = form["BeginSelectYear"] + AddZero(form["BeginSelectMonth"]) + AddZero(form["BeginSelectDay"]) + AddZero(form["BeginSelectHour"]) + AddZero(form["BeginSelectMinute"]) + "-" + form["EndSelectYear"] + AddZero(form["EndSelectMonth"]) + AddZero(form["EndSelectDay"]) + AddZero(form["EndSelectHour"]) + AddZero(form["EndSelectMinute"]);


                //状态
                c1.State = "待检验";
                //提交时间
                c1.SubDate = DateTime.Now;

                c1.DelFlag = 0;
                //备注
                c1.Remark = form["Remark"];

                c1.IsCheck = true;
                #endregion
            }//
            else
            {
                c1.IsCheck = false;
            }
            return c1;
        }//

        /// <summary>
        /// 为 0~9 的数字添加 0，补足2位数
        /// </summary>
        /// <param name="s1"></param>
        /// <returns></returns>
        private string AddZero(string s1)
        {
            if (Convert.ToInt32(s1) >= 0 && Convert.ToInt32(s1) < 10 && s1.Length == 1)
            {
                s1 = "0" + s1;
            }

            return s1;
        }

        //---------------------------------公共【Public】的Action（结束）-----------------------------------//


        //----------------------------------------教师的Action（开始）------------------------------------------------------------------//

        /// <summary>
        /// 展示教师主页信息
        /// </summary>
        /// <returns></returns>
        public ActionResult Teacher()
        {
            if (Session["UserType"] != null && Session["UserType"].ToString() == "Teacher")
            {
                TeacherInfo t1 = new TeacherInfo();
                if (Session["UserNum"] != null)
                {
                    t1 = TeacherInfo.GetTeacherInfo(Session["UserNum"].ToString());
                    ViewBag.Teacher = t1;
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
        /// 修改个人信息
        /// </summary>
        /// <param name="usertype">用户类型</param>
        /// <param name="item">修改的项</param>
        /// <param name="data">数据的值</param>
        /// <returns></returns>
     

        /// <summary>
        /// 展示插入选课课程信息的页面
        /// </summary>
        /// <returns></returns>
        public ActionResult SubCourseInfo()
        {
            if (Session["UserType"] != null && Session["UserType"].ToString() == "Teacher")
            {
                TeacherInfo t1 = new TeacherInfo();
                if (Session["UserNum"] != null)
                {
                    t1 = TeacherInfo.GetTeacherInfo(Session["UserNum"].ToString());
                    ViewBag.Teacher = t1;
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
        }//

        /// <summary>
        /// 插入课程信息或者修改课程信息
        /// </summary>
        /// <param name="form">课程信息表单</param>
        /// <returns></returns>
        public ActionResult ExecuteInsertAndUpdateCourseInfo(FormCollection form) 
        {
            if (Session["UserType"] != null && Session["UserType"].ToString() == "Teacher")
            {
                if (Session["UserNum"] != null)
                {

                    if (form["Action"] == "New")
                    {
                        CourseInfo c1 = FormToCourseInfo(form);
                        if (c1.IsCheck)
                        {
                            if (CourseInfo.ExecuteInsertCourseInfo(c1) > 0)
                            {
                                return Content("Success");
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
                    else if (form["Action"] == "Save")
                    {
                        CourseInfo c1 = FormToCourseInfo(form);
                        if (c1.IsCheck)
                        {
                            c1.CourseNum = form["CourseNum"];
                            if (CourseInfo.UpdateCourseInfo(c1) > 0)
                            {
                                return Content("Success");
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
                    else
                    {
                        return Content("Error");
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

       
        /// <summary>
        /// 展示教师修改课程信息
        /// </summary>
        /// <returns></returns>
        public ActionResult UpdateCourseInfo()
        {
            if (Session["UserType"] != null && Session["UserType"].ToString() == "Teacher")
            {
                if (Session["UserNum"] != null)
                {
                    TeacherInfo t1 = TeacherInfo.GetTeacherInfo(Session["UserNum"].ToString());

                    string sql = "Select * from CourseInfo where TeacherNum = @TeacherNum and DelFlag = 0";
                    List<CourseInfo> list = CourseInfo.GetCourseInfoByTeacherNum(Session["UserNum"].ToString(), sql);

                    ViewBag.Teacher = t1;
                    ViewBag.CourseList = list;
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
        /// 教师删除提交的课程信息
        /// </summary>
        /// <param name="courseNum"></param>
        /// <returns></returns>
        public ActionResult DelCourseInfo(string courseNum)
        {
            if (Session["UserType"] != null && Session["UserType"].ToString() == "Teacher")
            {
                if (Session["UserNum"] != null)
                {
                    if(courseNum != "")
                    {
                        //执行删除操作
                        if (CourseInfo.DelCourseInfoByCourseNum(courseNum, Session["UserNUm"].ToString()) > 0)
                        {
                            return Content("Success");
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
        /// 获取完整的课程信息，方便教师去修改
        /// </summary>
        /// <param name="courseNum">课程号</param>
        /// <returns></returns>
        public ActionResult GetFullCourseInfoByCourseNum(string courseNum, string state)
        {
            if (Session["UserType"] != null && Session["UserType"].ToString() == "Teacher")
            {
                if (Session["UserNum"] != null)
                {
                    if (courseNum != "")
                    {
                        //执行相关操作
                        if (state == "待检验")
                        {
                            CourseInfo c1 = new CourseInfo();
                            c1 = CourseInfo.GetFullCourseInfoByCourseNumAndTeacherNum(courseNum, Session["UserNum"].ToString());
                            if (c1.State == "待检验")
                            {
                                var tempObj = new
                                {
                                    #region 转换成json数据
                                    //课程名
                                    CourseName = c1.CourseName,

                                    //课程号
                                    CourseNum = c1.CourseNum,

                                    //课程类型
                                    CourseType = c1.CourseType,

                                    //考察类型
                                    CheckWay = c1.CheckWay,

                                    //先修课程
                                    FirstCourseNum = c1.FirstCourseNum,

                                    //学分
                                    Credit = c1.Credit,

                                    //开班人数
                                    Count = c1.Count,

                                    //已选人数
                                    SelectCount = c1.SelectCount,

                                    //教师编号
                                    TeacherNum = c1.TeacherNum,

                                    //上课时间 ClassDate（20142015020218）

                                    BeginClassYear = c1.ClassDate.Substring(0, 4),
                                    EndClassYear = c1.ClassDate.Substring(4, 4),
                                    Team = c1.ClassDate.Substring(8, 2),
                                    BeginWeek = c1.ClassDate.Substring(10, 2),
                                    EndWeek = c1.ClassDate.Substring(12, 2),

                                    //具体上课时间 ClassTime 周4第9,10节|周
                                    ClassDay = c1.ClassTime.Substring(1, 1),
                                    BeginClassNumber = c1.ClassTime.Substring(3, 1),
                                    EndClassNumber = c1.ClassTime.Substring(5, 1),

                                    //选课时间 SelectDate 201509181830-201509201830
                                    BeginSelectYear = c1.SelectDate.Substring(0, 4),
                                    BeginSelecMonth = c1.SelectDate.Substring(4, 2),
                                    BeginSelectDay = c1.SelectDate.Substring(6, 2),
                                    BeginSelectHour = c1.SelectDate.Substring(8, 2),
                                    BeginSelectMinute = c1.SelectDate.Substring(10, 2),

                                    EndSelectYear = c1.SelectDate.Substring(13, 4),
                                    EndSelectMonth = c1.SelectDate.Substring(17, 2),
                                    EndSelecDay = c1.SelectDate.Substring(19, 2),
                                    EndSelectHour = c1.SelectDate.Substring(21, 2),
                                    EndSelectMinute = c1.SelectDate.Substring(23, 2),

                                    //上课地点 
                                    ClassRoom = c1.ClassRoom,


                                    #endregion
                                };

                                //返回json数据
                                return Json(tempObj, JsonRequestBehavior.DenyGet);
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
                    else
                    {
                        return Content("Error");
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


        /// <summary>
        /// 【学生管理】
        /// </summary>
        /// <returns></returns>
        public ActionResult StudentManage() 
        {
            if (Session["UserType"] != null && Session["UserType"].ToString() == "Teacher")
            {
                if (Session["UserNum"] != null)
                {
                    TeacherInfo t1 = TeacherInfo.GetTeacherInfo(Session["UserNum"].ToString());

                    //获取教师发布的课程，并且通过管理员的验证的（State：待检验-----> 正常）
                    string sql = "Select * from CourseInfo where TeacherNum = @TeacherNum and DelFlag = 0 and State = '正常' ";
                    List<CourseInfo> list = CourseInfo.GetCourseInfoByTeacherNum(Session["UserNum"].ToString(), sql);

                    ViewBag.Teacher = t1;
                    ViewBag.CourseList = list;
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
        /// 【教师】获取选课的详细的【学生的信息】
        /// </summary
        /// <returns></returns>
        public ActionResult GetStudentInfoInSelectClass(string courseNum)
        {

            if (Session["UserType"] != null && Session["UserType"].ToString() == "Teacher")
            {
                if (Session["UserNum"] != null)
                {
                    List<StuInfo> list = SelectClassInfo.GetStuInfoByCourseNumAndTeacherNum(courseNum, Session["UserNum"].ToString());

                    var json = new
                    {
                        total = list.Count,
                        Row = list.ToArray()

                    };

                    return Json(json, JsonRequestBehavior.DenyGet);
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
        /// 【教师】获取学生当前的成绩
        /// </summary>
        /// <param name="stuNum"></param>
        /// <param name="coureNum"></param>
        /// <returns></returns>
        public ActionResult TGetStuScoreInfo(string stuNum, string courseNum)
        {
            if (Session["UserType"] != null && Session["UserType"].ToString() == "Teacher")
            {
                if (Session["UserNum"] != null)
                {
                    SelectClassInfo sc = SelectClassInfo.TGetStuScoreInfo(stuNum, courseNum);

                    var json = new 
                    {
                        Grade1 = sc.Grade1,
                        Grade2 = sc.Grade2,
                        Grade3 = sc.Grade3,
                        Grade4= sc.Grade4,
                        Grade = sc.Grade
                    };

                    return Json(json, JsonRequestBehavior.DenyGet);
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
        /// 【教师】提交学生成绩
        /// </summary>
        /// <param name="stuNum">学号</param>
        /// <returns></returns>
        public ActionResult TSvaeStuScore(string stuNum,string courseNum, string grade1,string grade2,string grade3,string grade4,string grade)
        {
            if (Session["UserType"] != null && Session["UserType"].ToString() == "Teacher")
            {
                if (Session["UserNum"] != null)
                {
                    //数据填充
                    SelectClassInfo sc = new SelectClassInfo();
                    sc.StuNum = stuNum;
                    sc.CourseNum = courseNum;
                    sc.Grade1 = Convert.ToInt32(grade1);
                    sc.Grade2 = Convert.ToInt32(grade2);
                    sc.Grade3 = Convert.ToInt32(grade3);
                    sc.Grade4 = Convert.ToInt32(grade4);
                    sc.Grade = Convert.ToInt32(grade);

                    //执行
                    int result = SelectClassInfo.TSvaeStuScore(sc);

                    //比较执行结果
                    if (result > 0)
                    {
                        return Content("Success");
                    }
                    else
                    {
                        return Content("Error");
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

        //-----------------------------------------教师的Action（结束）-------------------------------------------------------//

    }
}
