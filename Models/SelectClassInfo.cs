using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using 学生选课信息管理系统.Models;
using System.Collections;

namespace 学生选课信息管理系统.Models
{
    public class SelectClassInfo
    {
        #region 属性字段
       
        public int Id { get; set; }//选课信息表编号

        public string CourseNum { get; set; }//课程编号

        public string StuNum { get; set; }//学生学号

        public string TeacherNum { get; set; }//教师编号

        public int Grade { get; set; }//总评成绩

        public int Grade1 { get; set; }//平时成绩

        public int Grade2 { get; set; }//期中成绩

        public int Grade3 { get; set; }//实验成绩

        public int Grade4 { get; set; }//期末成绩

        public int Grade5 { get; set; }//补考成绩

        public int Grade6 { get; set; }//重修成绩


        public int IsEnd { get; set; }//课程是否结束

        public string State { get; set; }//状态

        public DateTime SubDate { get; set; }//提交时间

        public int DelFlag { get; set; }//删除标识

        public string Remark { get; set; }//备注

        //附加属性

        public string TeacherName { get; set; }//教师名字
        //public CourseInfo courseInfo= new CourseInfo();//课程信息
        public string CourseName { get; set; }//课程名字

        public int Credit { get; set; }//学分

        public string CourseType { get; set; }//课程类型

        public string ClassDate { get; set; }//上课时间段

        public string ClassTime { get; set; }//上课时间

        public string ClassRoom { get; set; }//上课地点

        public string CheckWay { get; set; }//考察方式

        public string LearnWeeks { get; set; }//上课周数

        #endregion

        /// <summary>
        /// 获取学生的选课信息
        /// </summary>
        /// <param name="stuNum">学号</param>
        /// <returns>学生已选课信息表</returns>
        public static List<SelectClassInfo> GetStuSelectClassInfoByStuNum(string stuNum)
        {
            //string sql = "Select res.*,c.CourseName,c.Credit,c.CourseType,c.ClassDate,ClassTime,c.ClassRoom,c.CheckWay from( Select sc.*,t.TeacherName from SelectClassInfo as sc Left join TeacherInfo as t on sc.TeacherNum = t.TeacherNum where StuNum = @StuNum and sc.DelFlag = 0 and sc.State =@State and sc.IsEnd = 1 ) as res Left join CourseInfo as c on res.CourseNum = c.CourseNum ";
            //string sql = "Select res.*,c.CourseName,c.Credit,c.CourseType,c.ClassDate,ClassTime,c.ClassRoom,c.CheckWay from( Select sc.*,t.TeacherName from SelectClassInfo as sc Left join TeacherInfo as t on sc.TeacherNum = t.TeacherNum where StuNum = @StuNum and sc.DelFlag = 0 and sc.State =@State  ) as res Left join CourseInfo as c on res.CourseNum = c.CourseNum and c.IsEnd = 1";
            string sql = "select *,t.TeacherName from( Select s.*,c.CourseName,c.Credit,c.CourseType,c.ClassDate,ClassTime,c.ClassRoom,c.CheckWay  from SelectClassInfo as s,CourseInfo c where s.StuNum=@StuNum and s.CourseNum = c.CourseNum and c.IsEnd = 1 and s.DelFlag = 0 and s.State = @State) as res Left join TeacherInfo as t on res.TeacherNum = t.TeacherName";

            SqlParameter[] param = {
                                     new SqlParameter("@StuNum",stuNum),
                                     new SqlParameter("@State","正常")
                                 };

            List<SelectClassInfo> list = new List<SelectClassInfo>();
            DataTable dt = new DataTable();
            
            

            dt = SqlHelper.ExecuteTable(sql, param);

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    SelectClassInfo sc = new SelectClassInfo();
                    #region 课程信息填装
                    //课程编号
                    if (dr["CourseNum"] is DBNull)
                    {
                        sc.CourseNum = "";
                    }
                    else
                    {
                        sc.CourseNum = dr["CourseNum"].ToString();
                    }

                    //学号
                    if (dr["StuNum"] is DBNull)
                    {
                        sc.StuNum = "";
                    }
                    else
                    {
                        sc.StuNum = dr["StuNum"].ToString();
                    }

                    //教师编号
                    if (dr["TeacherNum"] is DBNull)
                    {
                        sc.TeacherNum = "";
                    }
                    else
                    {
                        sc.TeacherNum = dr["TeacherNum"].ToString();
                    }

                    //教师名字
                    if (dr["TeacherName"] is DBNull)
                    {
                        sc.TeacherName = "";
                    }
                    else
                    {
                        sc.TeacherName = dr["TeacherName"].ToString();
                    }

                    //成绩信息暂时不读取

                    //备注
                    if (dr["Remark"] is DBNull)
                    {
                        sc.Remark = "";
                    }
                    else
                    {
                        sc.Remark = dr["Remark"].ToString();
                    }

                    if (dr["CourseName"] is DBNull)
                    {
                        sc.CourseName = "";
                    }
                    else
                    {
                        sc.CourseName = dr["CourseName"].ToString();
                    }

                    if (dr["CourseType"] is DBNull)
                    {
                        sc.CourseType = "";
                    }
                    else
                    {
                        sc.CourseType = dr["CourseType"].ToString();
                    }

                    if (dr["CheckWay"] is DBNull)
                    {
                        sc.CheckWay = "";
                    }
                    else
                    {
                        sc.CheckWay = dr["CheckWay"].ToString();
                    }

                    if (dr["Credit"] is DBNull)
                    {
                        sc.Credit = 0;
                    }
                    else
                    {
                        sc.Credit = Convert.ToInt32(dr["Credit"]);
                    }

                    //Count, SelectCount

                    if (dr["ClassDate"] is DBNull)
                    {
                        sc.ClassDate = "";
                    }
                    else
                    {
                        string str = dr["ClassDate"].ToString();
                        sc.LearnWeeks = str.Substring(10, 2) + " - ";
                        str = str.Substring(12, 2);
                        sc.LearnWeeks += str;
      
                    }

                    if (dr["ClassTime"] is DBNull)
                    {
                        sc.ClassTime = "";
                    }
                    else
                    {
                        sc.ClassTime = dr["ClassTime"].ToString();
                    }

                    if (dr["ClassRoom"] is DBNull)
                    {
                        sc.ClassRoom = "";
                    }
                    else
                    {
                        sc.ClassRoom = dr["ClassRoom"].ToString();
                    }
#endregion
                    list.Add(sc);
                }
            }

            return list;

        }

        /// <summary>
        /// 学生【退选】课程信息
        /// </summary>
        /// <param name="stuNum">学号</param>
        /// <param name="courseNum">课程号</param>
        /// <returns></returns>
        public static int DeleteSelectClassByCourseNum(string stuNum, string courseNum)
        {
            //string sql = "Begin Transaction  Update SelectClassInfo set DelFlag = 1 where StuNum = @StuNum and CourseNum = @CourseNum and IsEnd = 1   Update CourseInfo set SelectCount -= 1 where CourseNum = @CourseNum   Update CourseInfo set SelectCount += 1 where CourseNum = @CourseNum   Commit Transaction";
            string sql = "Begin Transaction  Update SelectClassInfo set DelFlag = 1 where StuNum = @StuNum and CourseNum = @CourseNum     Update CourseInfo set SelectCount -= 1 where CourseNum = @CourseNum   Update CourseInfo set SelectCount += 1 where CourseNum = @CourseNum   Commit Transaction";
            SqlParameter[] param = {
                                       new SqlParameter("@StuNum",stuNum),
                                       new SqlParameter("@CourseNum",courseNum)
                                   };

            return SqlHelper.ExecuteNonQuery(sql, param);                      

        }//

        /// <summary>
        /// 【插入】学生选课信息
        /// </summary>
        /// <param name="stuNum">学号</param>
        /// <param name="courseNum">课程编号</param>
        /// <param name="teacherNum">教师编号</param>
        /// <returns></returns>
        public static int ExecuteSelelctClass(string stuNum, string courseNum, string teacherNum)
        {
            //读取学生已选课的信息
           string sql1 = "Select * from SelectClassInfo where StuNum = @StuNum and IsEnd = 1";
            SqlParameter[] param = {
                                   new SqlParameter("@StuNum",stuNum)
                                   };

            DataTable dt = new DataTable();
            dt = SqlHelper.ExecuteTable(sql1, param);
            ArrayList strs = new ArrayList(); //保存未删除的
            ArrayList strs2 = new ArrayList();//保存删除的
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["CourseNum"] is DBNull)
                    {
                        break;
                    }
                    else
                    {
                        if (Convert.ToInt32(dr["DelFlag"]) == 0)//0---未删除
                        {
                            strs.Add(dr["CourseNum"].ToString().Trim());
                        }
                        else
                        {
                            strs2.Add(dr["CourseNum"].ToString().Trim());
                        }
                    }
                }
            }

            if (strs.Count == 0)//全部都是已经删除的，没有已选项
            {
                #region
                for (int t = 0; t < strs2.Count; t++)
                {
                    if (strs2[t].ToString() == courseNum)//已选，已删除
                    {
                        //string sql = "Begin Transaction Update SelectClassInfo set DelFlag = 0 where CourseNum = @CourseNum and StuNum = @StuNum and IsEnd = 1  Update CourseInfo set SelectCount -= 1 where CourseNum = @CourseNum  Commit Transaction";
                        string sql = "Begin Transaction Update SelectClassInfo set DelFlag = 0 where CourseNum = @CourseNum and StuNum = @StuNum Update CourseInfo set SelectCount -= 1 where CourseNum = @CourseNum  Commit Transaction";

                        SqlParameter[] param2 = {
                                                            new SqlParameter("@CourseNum",courseNum),
                                                            new SqlParameter("@StuNum",stuNum)
                                                        };

                        return SqlHelper.ExecuteNonQuery(sql, param2);
                    }
                    else
                    {
                        if (t == strs2.Count - 1)//未选过，直接插入
                        {
                            string sql2 = "Begin Transaction Insert into SelectClassInfo(CourseNum, StuNum, TeacherNum, State, SubDate, DelFlag ) values(@CourseNum, @StuNum, @TeacherNum, @State, @SubDate, 0 )   Update CourseInfo set SelectCount -= 1 where CourseNum = @CourseNum Commit Transaction";

                            SqlParameter[] param2 = {
                                                new SqlParameter("@CourseNum",courseNum),
                                                new SqlParameter("@StuNum",stuNum),
                                                new SqlParameter("@TeacherNum",teacherNum),
                                                new SqlParameter("@State","正常"),
                                                new SqlParameter("@SubDate",DateTime.Now)
                                                //new SqlParameter("@DelFlag",0)
                                     };
                            return SqlHelper.ExecuteNonQuery(sql2, param2);
                        }
                    }
                }//
                #endregion
            }
            else
            {

                #region
                for (int k = 0; k < strs.Count; k++)
                {
                    if (strs[k].ToString() == courseNum)
                    {
                        return -1;//已选，未删除
                    }
                    else//一旦发现有选过的课程就直接返回
                    {
                        if (k == strs.Count - 1)
                        {

                            for (int t = 0; t < strs2.Count; t++)
                            {
                                if (strs2[t].ToString() == courseNum)//已选，已删除
                                {
                                    string sql = "Begin Transaction Update SelectClassInfo set DelFlag = 0 where CourseNum = @CourseNum and StuNum = @StuNum and IsEnd = 1  Update CourseInfo set SelectCount += 1 where CourseNum = @CourseNum  Commit Transaction";

                                    SqlParameter[] param2 = {
                                                            new SqlParameter("@CourseNum",courseNum),
                                                            new SqlParameter("@StuNum",stuNum)
                                                        };

                                    return SqlHelper.ExecuteNonQuery(sql, param2);
                                }
                                else
                                {
                                    if (t == strs2.Count - 1)//未选过，直接插入
                                    {
                                        string sql2 = "Begin Transaction Insert into SelectClassInfo(CourseNum, StuNum, TeacherNum, State, SubDate, DelFlag ) values(@CourseNum, @StuNum, @TeacherNum, @State, @SubDate, 0 )   Update CourseInfo set SelectCount += 1 where CourseNum = @CourseNum Commit Transaction";
                                        SqlParameter[] param2 = {
                                                new SqlParameter("@CourseNum",courseNum),
                                                new SqlParameter("@StuNum",stuNum),
                                                new SqlParameter("@TeacherNum",teacherNum),
                                                new SqlParameter("@State","正常"),
                                                new SqlParameter("@SubDate",DateTime.Now)
                                                //new SqlParameter("@DelFlag",0)
                                     };
                                        return SqlHelper.ExecuteNonQuery(sql2, param2);
                                    }
                                }
                            }//



                        }


                    }
                }
                #endregion
            }
            return 0;

        }//

        /// <summary>
        /// 【教师】获取学生的信息
        /// </summary>
        /// <returns></returns>
        public static List<StuInfo> GetStuInfoByCourseNumAndTeacherNum(string courseNum, string teacherNum)
        {
            string sql = "select s.* from SelectClassInfo sc, StuInfo s, CourseInfo c where sc.StuNum = s.StuNum and sc.DelFlag = 0 and sc.CourseNum = c.CourseNum and c.IsEnd = 1 and sc.CourseNum = @CourseNum and sc.TeacherNum = @TeacherNum";

            SqlParameter[] param = {
                                       new SqlParameter("@CourseNum",courseNum),
                                       new SqlParameter("@TeacherNum",teacherNum)
                                   };//参数列表

            DataTable dt = SqlHelper.ExecuteTable(sql, param);//执行数据库操作，读取学生信息
            List<StuInfo> list = new List<StuInfo>();//用来放学生信息的一个列表（容器）

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)//遍历每行数据
                {
                    StuInfo s = new StuInfo();

                    #region 填充学生个人信息

                    //学号
                    if (dr["StuNum"] is DBNull)
                    {
                        s.StuNum = "";
                    }
                    else
                    {
                        s.StuNum = dr["StuNum"].ToString();
                    }

                    //学生名字
                    if (dr["StuName"] is DBNull)
                    {
                        s.StuName = "";
                    }
                    else
                    {
                        s.StuName = dr["StuName"].ToString();
                    }

                    //性别
                    if (dr["Sex"] is DBNull)
                    {
                        s.Sex = "";
                    }
                    else
                    {
                        s.Sex = dr["Sex"].ToString();
                    }

                    //身份证号码
                    if (dr["IdentityCard"] is DBNull)
                    {
                        s.IdentityCard = "";
                    }
                    else
                    {
                        s.IdentityCard = dr["IdentityCard"].ToString();
                    }

                    //专业
                    if (dr["Major"] is DBNull)
                    {
                        s.Major = "";
                    }
                    else
                    {
                        s.Major = dr["Major"].ToString();
                    }

                    //班级
                    if (dr["ClassName"] is DBNull)
                    {
                        s.ClassName = "";
                    }
                    else
                    {
                        s.ClassName = dr["ClassName"].ToString();
                    }

                    //学院编号
                    if (dr["InstituteNum"] is DBNull)
                    {
                        s.InstituteNum = "";
                    }
                    else
                    {
                        s.InstituteNum = dr["InstituteNum"].ToString();
                    }

                    //入学日期
                    if (dr["EnrollmentDate"] is DBNull)
                    {
                        s.EnrollmentDate = DateTime.Now;
                    }
                    else
                    {
                        s.EnrollmentDate = Convert.ToDateTime( dr["EnrollmentDate"] );
                    }

                    //民族
                    if (dr["Nation"] is DBNull)
                    {
                        s.Nation = "";
                    }
                    else
                    {
                        s.Nation = dr["Nation"].ToString();
                    }

                    //籍贯
                    if (dr["Native"] is DBNull)
                    {
                        s.Native = "";
                    }
                    else
                    {
                        s.Native = dr["Native"].ToString();
                    }

                    //联系电话
                    if (dr["Phone"] is DBNull)
                    {
                        s.Phone = "";
                    }
                    else
                    {
                        s.Phone = dr["Phone"].ToString();
                    }

                    //邮箱
                    if (dr["Email"] is DBNull)
                    {
                        s.Email = "";
                    }
                    else
                    {
                        s.Email = dr["Email"].ToString();
                    }

                    //状态
                    if (dr["State"] is DBNull)
                    {
                        s.State = "";
                    }
                    else
                    {
                        s.State = dr["State"].ToString();
                    }

                    ////提交时间
                    //if (dr["SubDate"] is DBNull)
                    //{
                    //    s.SubDate = DateTime.Now;
                    //}
                    //else
                    //{
                    //    s.SubDate = Convert.ToDateTime(dr["SubDate"]);
                    //}

                    //备注
                    if (dr["Remark"] is DBNull)
                    {
                        s.Remark = "";
                    }
                    else
                    {
                        s.Remark = dr["Remark"].ToString();
                    }

                    #endregion

                    list.Add(s);
                }
            }

            return list;

        }

        /// <summary>
        /// 【教师】获取学生成绩信息
        /// </summary>
        /// <param name="stuNum">学号</param>
        /// <param name="courseNum">课程号</param>
        /// <returns></returns>
        public static SelectClassInfo TGetStuScoreInfo(string stuNum, string courseNum)
        {
            string sql = "Select * from SelectClassInfo where StuNum = @StuNum and CourseNum = @CourseNum and DelFlag = 0";

            SqlParameter[] param = {
                                       new SqlParameter("@StuNum",stuNum),
                                       new SqlParameter("@CourseNum",courseNum)
                                   };

            DataTable dt = SqlHelper.ExecuteTable(sql, param);
            SelectClassInfo sc = new SelectClassInfo();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    #region
                    //平时成绩
                    if (dr["Grade1"] is DBNull)
                    {
                        sc.Grade1 = 0;
                    }
                    else
                    {
                        sc.Grade1 = Convert.ToInt32(dr["Grade1"]);
                    }

                    //期中成绩
                    if (dr["Grade2"] is DBNull)
                    {
                        sc.Grade2 = 0;
                    }
                    else
                    {
                        sc.Grade2 = Convert.ToInt32(dr["Grade2"]);
                    }

                    //实验成绩
                    if (dr["Grade3"] is DBNull)
                    {
                        sc.Grade3 = 0;
                    }
                    else
                    {
                        sc.Grade3 = Convert.ToInt32(dr["Grade3"]);
                    }

                    //期末成绩
                    if (dr["Grade4"] is DBNull)
                    {
                        sc.Grade4 = 0;
                    }
                    else
                    {
                        sc.Grade4 = Convert.ToInt32(dr["Grade4"]);
                    }

                    //总评成绩
                    if (dr["Grade"] is DBNull)
                    {
                        sc.Grade = 0;
                    }
                    else
                    {
                        sc.Grade = Convert.ToInt32(dr["Grade"]);
                    }

                    #endregion



                }
            }

            return sc;
        }

        /// <summary>
        /// 【教师】保存学生成绩
        /// </summary>
        /// <param name="sc"></param>
        /// <returns></returns>
        public static int TSvaeStuScore(SelectClassInfo sc)
        {
            string sql = "Update SelectClassInfo set Grade1 = @Grade1 , Grade2 = @Grade2 , Grade3 = @Grade3 , Grade4 = @Grade4 , Grade = @Grade where StuNum = @stuNum and CourseNum = @CourseNum";

            SqlParameter[] param = {
                                   new SqlParameter("@Grade1",sc.Grade1),
                                   new SqlParameter("@Grade2",sc.Grade2),
                                   new SqlParameter("@Grade3",sc.Grade3),
                                   new SqlParameter("@Grade4",sc.Grade4),
                                   new SqlParameter("@Grade",sc.Grade),
                                    new SqlParameter("@StuNum",sc.StuNum),
                                     new SqlParameter("@CourseNum",sc.CourseNum)
                                   };

            return SqlHelper.ExecuteNonQuery(sql, param);
        }

    }
}