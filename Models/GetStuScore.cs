using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using 学生选课信息管理系统.Models;
using System.Data.SqlClient;
using System.Configuration;

namespace 学生选课信息管理系统.Models
{
    public class GetStuScore
    {
        #region 属性字段
        public string LearnYear { get; set; }//学年

        public string LearnQi { get; set; }//学期

        public string CourseName { get; set; }//课程名称

        public string CourseType { get; set; }//课程类型

        public string TeacherName { get; set; }//教师名字

        public string CheckWay { get; set; }//考核方式

        public string Grade1 { get; set; }//平时成绩

        public string Grade2 { get; set; }//期中成绩

        public string Grade3 { get; set; }//实验成绩

        public string Grade4 { get; set; }//期末成绩

        public string Grade { get; set; }//总评成绩

        public string Grade5 { get; set; }//补考成绩

        public string Grade6 { get; set; }//重修成绩

        public string Credit { get; set; }//应得学分 
        #endregion


        public static List<GetStuScore> GetStuScoreInfoAsList(string stuNum)
        {
            string sql = " Select c.ClassDate, c.CourseName,s.StuNum,t.TeacherName, c.CheckWay ,c.CourseType,s.Grade1,s.Grade2,s.Grade3,s.Grade4,s.Grade,s.Grade5,s.Grade6,c.Credit from SelectClassInfo as s, CourseInfo as c, TeacherInfo as t  where s.StuNum = @StuNum and s.CourseNum = c.CourseNum and s.TeacherNum = t.TeacherNum and s.State=@State and s.DelFlag =0 and s.IsEnd = 0";
            SqlParameter[] param = { 
                                       new SqlParameter("@StuNum",stuNum),
                                       new SqlParameter("@State","正常")
                                   };

            DataTable dt = new DataTable();
            List<GetStuScore> list = new List<GetStuScore>();

            dt = SqlHelper.ExecuteTable(sql, param);

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    GetStuScore gs = new GetStuScore();

                    #region 填充信息
                    //学年
                    if (dr["ClassDate"] is DBNull)
                    {
                        gs.LearnYear = "";
                    }
                    else
                    {
                        string s = "";

                        s = dr["ClassDate"].ToString().Substring(0, 4);
                        gs.LearnYear = s + "-";
                        s = dr["ClassDate"].ToString().Substring(4, 4);
                        gs.LearnYear += s;

                        s = dr["ClassDate"].ToString().Substring(9, 1);
                        gs.LearnQi = s;
                    }

                    //学期
                    if (dr["CourseName"] is DBNull)
                    {
                        gs.CourseName = "";
                    }
                    else
                    {
                        gs.CourseName = dr["CourseName"].ToString();
                    }

                    //课程类型
                    if (dr["CourseType"] is DBNull)
                    {
                        gs.CourseType = "";
                    }
                    else
                    {
                        gs.CourseType = dr["CourseType"].ToString();
                    }

                    //教师名字
                    if (dr["TeacherName"] is DBNull)
                    {
                        gs.TeacherName = "";
                    }
                    else
                    {
                        gs.TeacherName = dr["TeacherName"].ToString();
                    }

                    //考察方式
                    if (dr["CheckWay"] is DBNull)
                    {
                        gs.CheckWay = "";
                    }
                    else
                    {
                        gs.CheckWay = dr["CheckWay"].ToString();
                    }

                    //平时成绩
                    if (dr["Grade1"] is DBNull)
                    {
                        gs.Grade1 = "";
                    }
                    else
                    {
                        gs.Grade1 = dr["Grade1"].ToString();
                    }

                    //期中成绩
                    if (dr["Grade2"] is DBNull)
                    {
                        gs.Grade2 = "";
                    }
                    else
                    {
                        gs.Grade2 = dr["Grade2"].ToString();
                    }

                    //实验成绩
                    if (dr["Grade3"] is DBNull)
                    {
                        gs.Grade3 = "";
                    }
                    else
                    {
                        gs.Grade3 = dr["Grade3"].ToString();
                    }

                    //期末成绩
                    if (dr["Grade4"] is DBNull)
                    {
                        gs.Grade4 = "";
                    }
                    else
                    {
                        gs.Grade4 = dr["Grade4"].ToString();
                    }

                    //总评成绩
                    if (dr["Grade"] is DBNull)
                    {
                        gs.Grade = "";
                    }
                    else
                    {
                        gs.Grade = dr["Grade"].ToString();
                    }

                    //补考成绩
                    if (dr["Grade5"] is DBNull)
                    {
                        gs.Grade5 = "";
                    }
                    else
                    {
                        gs.Grade5 = dr["Grade5"].ToString();
                    }

                    //重修成绩
                    if (dr["Grade6"] is DBNull)
                    {
                        gs.Grade6 = "";
                    }
                    else
                    {
                        gs.Grade6 = dr["Grade6"].ToString();
                    }

                    //学分
                    if (dr["Credit"] is DBNull)
                    {
                        gs.Credit = "";
                    }
                    else
                    {
                        gs.Credit = dr["Credit"].ToString();
                    } 
                    #endregion
                    list.Add(gs);

                }//
            }//

            return list;
            
        }
    }
}