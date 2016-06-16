using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace 学生选课信息管理系统.Models
{
    public class CourseInfo
    {
        public int Id { get; set; }//编号

        public string CourseNum { get; set; }//课程号

        public string CourseName { get; set; }//课程名

        public string CourseType { get; set; }//课程类型

        public string CheckWay { get; set; }//考察方式

        public string FirstCourseNum { get; set; }//先修课

        public int Credit { get; set; }//学分

        public int Count { get; set; }//人数

        public int SelectCount { get; set; }//已选人数

        public string TeacherNum { get; set; }//教师编号

        public string ClassDate { get; set; }//开课时间

        public string ClassTime { get; set; }//上课时间

        public string SelectDate { get; set; }//选课时间

        public string ClassRoom { get; set; }//教室


        public int IsEnd { get; set; }//是否结束（本学期是否结束）

        public string State { get; set; }//状态

        public DateTime SubDate { get; set; }//提交时间

        public int DelFlag { get; set; }//删除标识

        public string Remark { get; set; }//备注


        //附加属性
        public string TeacherName { get; set; }//老师名字

        public string LearnWeeks { get; set; }//起始周

        public bool IsCheck{ get; set; }//插入信息是否已经检查

        public string FullClassTime { get; set; }//完整的混合的上课时间【教师查看课程】


        /// <summary>
        /// 获取选课课程信息表
        /// </summary>
        /// <returns></returns>
        public static List<CourseInfo> ShowCourseInfo()
        {
            string sql = "select c.*,t.TeacherName from CourseInfo as c Left join TeacherInfo as t on c.TeacherNum = t.TeacherNum where c.DelFlag = 0 and c.State =@State and IsEnd = 1";

            SqlParameter[] param = {
                                       new SqlParameter("@State","正常")
                                   };
            List<CourseInfo> list = new List<CourseInfo>();
            DataTable dt = new DataTable();
            //CourseInfo c = new CourseInfo();

            dt = SqlHelper.ExecuteTable(sql,param);

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    #region 填充信息
                    CourseInfo c = new CourseInfo();
                    if (dr["SelectDate"] is DBNull)
                    {

                    }
                    else
                    {
                        ////201509181830-201509201830
                        //string selectDate = dr["SelectDate"].ToString();
                        //c.SelectDate = dr["SelectDate"].ToString();

                        ////开始时间
                        //string beginYear = selectDate.Substring(0,4);
                        //string beginMonth = selectDate.Substring(4,2);
                        //string beginDay = selectDate.Substring(6,2);
                        ////string beginTime = selectDate.Substring(8,4);
                        //string beginHour = selectDate.Substring(8, 2);
                        //string beginMinute = selectDate.Substring(10,2);

                        //string time1 = string.Format("{0}-{1}-{2} {3}:{4}:{5}",beginYear,beginMonth,beginDay,beginHour,beginMinute,"00");
                        //DateTime beginDT = Convert.ToDateTime(time1);

                        ////结束时间
                        //string endYear = selectDate.Substring(13,4);
                        //string endMonth = selectDate.Substring(17,2);
                        //string endDay = selectDate.Substring(19,2);
                        ////string endTime = selectDate.Substring(21, 4);
                        //string endHour = selectDate.Substring(21, 2);
                        //string endMinute = selectDate.Substring(23, 2);


                        //string time2 = string.Format("{0}-{1}-{2} {3}:{4}:{5}", endYear, endMonth, endDay, endHour, endMinute, "00");
                        //DateTime endDT = Convert.ToDateTime(time2);


                        //if(beginDT <= DateTime.Now && DateTime.Now <= endDT){
                                            #region 正式填充
                                            
                                            //课程号
                                            if (dr["CourseNum"] is DBNull)
                                            {
                                                c.CourseNum = "";
                                            }
                                            else
                                            {
                                                c.CourseNum = dr["CourseNum"].ToString();
                                            }

                                            //课程名
                                            if (dr["CourseName"] is DBNull)
                                            {
                                                c.CourseName = "";
                                            }
                                            else
                                            {
                                                c.CourseName = dr["CourseName"].ToString();
                                            }

                                            //课程类型
                                            if (dr["CourseType"] is DBNull)
                                            {
                                                c.CourseType = "";
                                            }
                                            else
                                            {
                                                c.CourseType = dr["CourseType"].ToString();
                                            }

                                            //考察方式
                                            if (dr["CheckWay"] is DBNull)
                                            {
                                                c.CheckWay = "";
                                            }
                                            else
                                            {
                                                c.CheckWay = dr["CheckWay"].ToString();
                                            }

                                            //先修课程号
                                            if (dr["FirstCourseNum"] is DBNull)
                                            {
                                                c.FirstCourseNum = "";
                                            }
                                            else
                                            {
                                                c.FirstCourseNum = dr["FirstCourseNum"].ToString();
                                            }

                                            //学分
                                            if (dr["Credit"] is DBNull)
                                            {
                                                c.Credit = 0;
                                            }
                                            else
                                            {
                                                c.Credit = Convert.ToInt32( dr["Credit"]);
                                            }

                                            //人数
                                            if (dr["Count"] is DBNull)
                                            {
                                                c.Count = 0;
                                            }
                                            else
                                            {
                                                c.Count = Convert.ToInt32(dr["Count"]);
                                            }

                                            //已选人数
                                            if (dr["SelectCount"] is DBNull)
                                            {
                                                c.SelectCount = 0;
                                            }
                                            else
                                            {
                                                c.SelectCount = Convert.ToInt32(dr["SelectCount"]);
                                            }

                                            //教师编号
                                            if (dr["TeacherNum"] is DBNull)
                                            {
                                                c.TeacherNum = "";
                                            }
                                            else
                                            {
                                                c.TeacherNum = dr["TeacherNum"].ToString();
                                            }

                                            //教师名字
                                            if (dr["TeacherName"] is DBNull)
                                            {
                                                c.TeacherName = "";
                                            }
                                            else
                                            {
                                                c.TeacherName = dr["TeacherName"].ToString();
                                            }

                                            //上课时间段
                                            if (dr["ClassDate"] is DBNull)
                                            {
                                                c.ClassDate = "";
                                                c.LearnWeeks = "02 - 18";
                                            }
                                            else
                                            {
                                                c.ClassDate = dr["ClassDate"].ToString();
                                                string str1 = dr["ClassDate"].ToString();
                                                string str2 = str1.Substring(10, 2);

                                                c.LearnWeeks = str2 + " - ";
                                                str2 = str1.Substring(12, 2);
                                                c.LearnWeeks += str2;
                                            }

                                            //上课时间
                                            if (dr["ClassTime"] is DBNull)
                                            {
                                                c.ClassTime = "";
                                            }
                                            else
                                            {
                                                c.ClassTime = dr["ClassTime"].ToString();
                                            }

                                            //教室
                                            if (dr["ClassRoom"] is DBNull)
                                            {
                                                c.ClassRoom = "";
                                            }
                                            else 
                                            {
                                                c.ClassRoom = dr["ClassRoom"].ToString();
                                            }


                                            //课程是否结束
                                            if (dr["IsEnd"] is DBNull)
                                            {
                                                c.IsEnd = 1;

                                            }
                                            else 
                                            {
                                                c.IsEnd = Convert.ToInt32(dr["IsEnd"]);
                                            }

                                            //状态
                                            if (dr["State"] is DBNull)
                                            {
                                                c.State = "";
                                            }
                                            else
                                            {
                                                c.State = dr["State"].ToString();
                                            }

                                            //提交日期
                                            if (dr["SubDate"] is DBNull)
                                            {
                                                c.SubDate = DateTime.Now;
                                            }
                                            else
                                            {
                                                c.SubDate = Convert.ToDateTime(dr["SubDate"]);
                                            }

                                            //删除标识
                                            if (dr["DelFlag"] is DBNull)
                                            {
                                                c.DelFlag = 0;
                                            }
                                            else
                                            {
                                                c.DelFlag = Convert.ToInt32( dr["DelFlag"] );
                                            }

                                            //备注
                                            if (dr["Remark"] is DBNull)
                                            {
                                                c.Remark = "";
                                            }
                                            else
                                            {
                                                c.Remark = dr["Remark"].ToString();
                                            }

                                            #endregion

                                            list.Add(c);
                       // }
                        //                }//
                        //            }
                        //        }
                        //    }
                        //}//
                    }

                    #endregion
                }
            }


            return list;
        


        }

        /// <summary>
        ///插入教师提交的课程信息
        /// </summary>
        /// <param name="c1">课程信息表</param>
        /// <returns></returns>
        public static int ExecuteInsertCourseInfo(CourseInfo c1)
        {
            string newCourseNum = c1.GetCourseNum();

            if( newCourseNum != "")
            {
                string sql = "Insert into CourseInfo(CourseNum, CourseName, CourseType, CheckWay, Credit, Count, SelectCount, TeacherNum, ClassDate, ClassTime, SelectDate, ClassRoom, State,SubDate, DelFlag, Remark) values(@CourseNum, @CourseName, @CourseType, @CheckWay, @Credit, @Count, @SelectCount, @TeacherNum, @ClassDate, @ClassTime, @SelectDate, @ClassRoom, @State,@SubDate, @DelFlag, @Remark)";

                SqlParameter[] param = {
                                           new SqlParameter("@CourseNum", newCourseNum),
                                           new SqlParameter("@CourseName",c1.CourseName),
                                           new SqlParameter("@CourseType", c1.CourseType),
                                           new SqlParameter("@CheckWay",c1.CheckWay),
                                           new SqlParameter("@Credit",c1.Credit),
                                           new SqlParameter("@Count", c1.Count),
                                           new SqlParameter("@SelectCount", c1.SelectCount),
                                           new SqlParameter("@TeacherNum", c1.TeacherNum),
                                           new SqlParameter("@ClassDate", c1.ClassDate),
                                           new SqlParameter("@ClassTime",c1.ClassTime),
                                           new SqlParameter("@SelectDate", c1.SelectDate),
                                           new SqlParameter("@ClassRoom",c1.ClassRoom),
                                           new SqlParameter("@State",c1.State),
                                           new SqlParameter("@SubDate",c1.SubDate),
                                           new SqlParameter("@DelFlag",c1.DelFlag),
                                           new SqlParameter("@Remark",c1.Remark)
                                        };

                return SqlHelper.ExecuteNonQuery(sql, param);
            }
            else
            {
                return -1;
            }
            
        }

        /// <summary>
        /// 获取一个新的课程编号
        /// </summary>
        /// <returns></returns>
        public string GetCourseNum()
        {
            //sql语句
            string sql = "select CourseNum from CourseInfo";
            
            DataTable dt =  SqlHelper.ExecuteTable(sql);

            string newCourseNum = "";

            if (dt.Rows.Count > 0)
            {
                int index = dt.Rows.Count;
                
                int k = -1;

                //判断是否已经存在
                do
                {
                    //newCourseNum = dt.Rows[index - 1].ToString().Trim();
                    newCourseNum = dt.Rows[index - 1]["CourseNum"].ToString();
                    string s1 = newCourseNum.Substring(1);

                    newCourseNum =(Convert.ToInt32(s1) + 1).ToString();

                    s1 = "C" + newCourseNum;

                    newCourseNum = s1;


                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i].ToString() == newCourseNum)
                        {
                            k = i;
                            break;
                        }
                    }

                    index--;
                } while (k != -1 && index != 0);

                
            }
            return newCourseNum;
        }

        /// <summary>
        /// 获取【某个教师】提交的【课程信息】
        /// </summary>
        /// <param name="teacherNum">教师编号</param>
        /// <returns></returns>
        public static List<CourseInfo> GetCourseInfoByTeacherNum(string teacherNum, string sql)
        {
            //string sql = "Select * from CourseInfo where TeacherNum = @TeacherNum and DelFlag = 0";

            SqlParameter[] param = {
                                       new SqlParameter("@TeacherNum",teacherNum)
                                   };

            List<CourseInfo> list = new List<CourseInfo>();

            DataTable dt = SqlHelper.ExecuteTable(sql, param);

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                   
                    CourseInfo c = new CourseInfo();

                    c = c.DataRow_To_CourseInfo(dr);

                    list.Add(c);

                }
            }

            return list;
        }

        /// <summary>
        /// 删除课程信息
        /// </summary>
        /// <param name="courseNum">课程编号</param>
        /// <param name="teacherNum">教师编号</param>
        /// <returns></returns>
        public static int DelCourseInfoByCourseNum(string courseNum, string teacherNum)
        {
            string sql = "Update CourseInfo set DelFlag = 1 where CourseNum = @CourseNum and TeacherNum = @TeacherNum and DelFlag = 0";

            SqlParameter[] param = {
                                   new SqlParameter("@CourseNum",courseNum),
                                   new SqlParameter("@TeacherNum",teacherNum)
                                   };

            return SqlHelper.ExecuteNonQuery(sql, param);
        }

        /// <summary>
        /// 单个获取完整的【课程信息】
        /// </summary>
        /// <param name="courseNum">课程号</param>
        /// <param name="teacherNum">教师编号</param>
        /// <returns></returns>
        public static CourseInfo GetFullCourseInfoByCourseNumAndTeacherNum(string courseNum, string teacherNum)
        {
            string sql = "Select * from CourseInfo where CourseNum = @CourseNum and TeacherNum = @TeacherNum and DelFlag = 0";

            SqlParameter[] param = {
                                       new SqlParameter("@CourseNum",courseNum),
                                       new SqlParameter("@TeacherNum", teacherNum)
                                      
                                   };

            DataTable dt = SqlHelper.ExecuteTable(sql, param);
            CourseInfo c1 = new CourseInfo();

            if(dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];

                c1 = c1.DataRow_To_CourseInfo(dr);
            }

            return c1;
        }


        /// <summary>
        /// 公共方法，把【数据行装换成数据类】
        /// </summary>
        /// <param name="dr">数据行</param>
        /// <returns></returns>
        private CourseInfo DataRow_To_CourseInfo(DataRow dr)
        {
            CourseInfo c = new CourseInfo();

            #region 正式填充

            //课程号
            if (dr["CourseNum"] is DBNull)
            {
                c.CourseNum = "";
            }
            else
            {
                c.CourseNum = dr["CourseNum"].ToString();
            }

            //课程名
            if (dr["CourseName"] is DBNull)
            {
                c.CourseName = "";
            }
            else
            {
                c.CourseName = dr["CourseName"].ToString();
            }

            //课程类型
            if (dr["CourseType"] is DBNull)
            {
                c.CourseType = "";
            }
            else
            {
                c.CourseType = dr["CourseType"].ToString();
            }

            //考察方式
            if (dr["CheckWay"] is DBNull)
            {
                c.CheckWay = "";
            }
            else
            {
                c.CheckWay = dr["CheckWay"].ToString();
            }

            //先修课程号
            if (dr["FirstCourseNum"] is DBNull)
            {
                c.FirstCourseNum = "";
            }
            else
            {
                c.FirstCourseNum = dr["FirstCourseNum"].ToString();
            }

            //学分
            if (dr["Credit"] is DBNull)
            {
                c.Credit = 0;
            }
            else
            {
                c.Credit = Convert.ToInt32(dr["Credit"]);
            }

            //人数
            if (dr["Count"] is DBNull)
            {
                c.Count = 0;
            }
            else
            {
                c.Count = Convert.ToInt32(dr["Count"]);
            }

            //已选人数
            if (dr["SelectCount"] is DBNull)
            {
                c.SelectCount = 0;
            }
            else
            {
                c.SelectCount = Convert.ToInt32(dr["SelectCount"]);
            }

            //教师编号
            if (dr["TeacherNum"] is DBNull)
            {
                c.TeacherNum = "";
            }
            else
            {
                c.TeacherNum = dr["TeacherNum"].ToString();
            }



            //上课时间段
            if (dr["ClassDate"] is DBNull)
            {
                c.ClassDate = "";
                c.LearnWeeks = "02 - 18";
            }
            else
            {
                c.ClassDate = dr["ClassDate"].ToString();
                string str1 = dr["ClassDate"].ToString();
                string str2 = str1.Substring(10, 2);

                c.LearnWeeks = str2 + " - ";
                str2 = str1.Substring(12, 2);
                c.LearnWeeks += str2;
            }

            //上课时间
            if (dr["ClassTime"] is DBNull)
            {
                c.ClassTime = "";
            }
            else
            {
                c.ClassTime = dr["ClassTime"].ToString();
            }

            //选课时间
            if (dr["SelectDate"] is DBNull)
            {
                c.SelectDate = "";
            }
            else
            {
                c.SelectDate = dr["SelectDate"].ToString();
            }

            //教室
            if (dr["ClassRoom"] is DBNull)
            {
                c.ClassRoom = "";
            }
            else
            {
                c.ClassRoom = dr["ClassRoom"].ToString();
            }

            //课程结束标识
            if (dr["IsEnd"] is DBNull)
            {
                c.IsEnd = 1;
            }
            else
            {
                c.IsEnd = Convert.ToInt32(dr["IsEnd"]);
            }

            //状态
            if (dr["State"] is DBNull)
            {
                c.State = "";
            }
            else
            {
                c.State = dr["State"].ToString();
            }

            //该课程是否结束
            if (dr["IsEnd"] is DBNull)
            {
 
            }

            //提交日期
            if (dr["SubDate"] is DBNull)
            {
                c.SubDate = DateTime.Now;
            }
            else
            {
                c.SubDate = Convert.ToDateTime(dr["SubDate"]);
            }

            //删除标识
            if (dr["DelFlag"] is DBNull)
            {
                c.DelFlag = 0;
            }
            else
            {
                c.DelFlag = Convert.ToInt32(dr["DelFlag"]);
            }

            //备注
            if (dr["Remark"] is DBNull)
            {
                c.Remark = "";
            }
            else
            {
                c.Remark = dr["Remark"].ToString();
            }


            //完整混合的上课时间【教师查看课程】
            if (dr["ClassDate"] is DBNull)
            {
                if (dr["ClassTime"] is DBNull)
                {
                    c.FullClassTime = "";
                }
                else
                {
                    c.FullClassTime = dr["ClassTime"].ToString();
                }
            }
            else
            {
                if (dr["ClassTime"] is DBNull)
                {
                    c.FullClassTime = dr["ClassDate"].ToString();
                }
                else
                {
                    string s = dr["ClassDate"].ToString();

                    c.FullClassTime = s.Substring(0,4) + "-";
                    c.FullClassTime += s.Substring(4, 4)+"第"+s.Substring(9,1)+"学期，" + dr["ClassTime"].ToString();
                    
                }
            }

            #endregion

            return c;
 
        }

        /// <summary>
        /// 教师更改课程信息
        /// </summary>
        /// <param name="c1"></param>
        /// <returns></returns>
        public static int UpdateCourseInfo(CourseInfo c1)
        {

            string sql = "Update CourseInfo set  CourseName = @CourseName, CourseType = @CourseType, CheckWay = @CheckWay, Credit = @Credit, Count = @Count, SelectCount = @SelectCount,  ClassDate = @ClassDate, ClassTime = @ClassTime, SelectDate = @SelectDate, ClassRoom = @ClassRoom, SubDate = @SubDate, DelFlag = @DelFlag, Remark = @Remark where CourseNum = @CourseNum and @TeacherNum = @TeacherNum and State = @State";

            SqlParameter[] param = {
                                        new SqlParameter("@CourseNum",c1.CourseNum),
                                        new SqlParameter("@CourseName",c1.CourseName),
                                        new SqlParameter("@CourseType", c1.CourseType),
                                        new SqlParameter("@CheckWay",c1.CheckWay),
                                        new SqlParameter("@Credit",c1.Credit),
                                        new SqlParameter("@Count", c1.Count),
                                        new SqlParameter("@SelectCount", c1.SelectCount),
                                        new SqlParameter("@TeacherNum", c1.TeacherNum),
                                        new SqlParameter("@ClassDate", c1.ClassDate),
                                        new SqlParameter("@ClassTime",c1.ClassTime),
                                        new SqlParameter("@SelectDate", c1.SelectDate),
                                        new SqlParameter("@ClassRoom",c1.ClassRoom),
                                        new SqlParameter("@State","待检验"),
                                        new SqlParameter("@SubDate",c1.SubDate),
                                        new SqlParameter("@DelFlag",c1.DelFlag),
                                        new SqlParameter("@Remark",c1.Remark)
                                    };

            return SqlHelper.ExecuteNonQuery(sql, param);
          
        }
    }
}