using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace 学生选课信息管理系统.Models
{
    public class TeacherInfo
    {
        public int Id { get; set; }//教师信息表编号

        public string TeacherNum { get; set; }//教师编号

        public string TeacherPwd { get; set; }//密码

        public string TeacherName { get; set; }//教师姓名

        public string Sex { get; set; }//性别

        public string IdentityCard { get; set; }//身份证号码

        public string Education { get; set; }//学历

        public string Position { get; set; }//职位

        public string Major { get; set; }//专业

        public string InstituteNum { get; set; }//学院编号

        public string Email { get; set; }//邮箱

        public string Phone { get; set; }//联系电话

        public string CourseName1 { get; set; }//主讲课程一名称

        public string CourseName2 { get; set; }//主讲课程二名称

        public string CourseName3 { get; set; }//主讲课程三名称

        public string State { get; set; }//状态

        public DateTime SubDate { get; set; }//注册时间

        public int DelFlag { get; set; }//删除标识

        public string Remark { get; set; }//备注

        //附加属性
        public string IsLogin { get; set; }//验证标识

        public string InstituteName { get; set; }//学院名字

       



        //连接字符串
        private static readonly string connStr = ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;

        /// <summary>
        /// 获取单个教师信息
        /// </summary>
        /// <param name="username">教师编号</param>
        /// <returns></returns>
        public static TeacherInfo GetTeacherInfo(string username)
        {
            TeacherInfo teacher = new TeacherInfo();
            string sql = "Select * from TeacherInfo a,InstituteInfo b  where a.InstituteNum = b.InstituteNum and a.TeacherNum = @TeacherNum and a.DelFlag = 0 ";

            //string sql = "Select  a.*, b.InstituteName,c.CourseName from TeacherInfo a,InstituteInfo b  , CourseInfo c where a.InstituteNum = b.InstituteNum and a.TeacherNum =@TeacherNum and a.DelFlag = 0 and c.CourseNum in (a.CourseNum1,a.CourseNum2,a.CourseNum3)";


            try
            {
                using (SqlConnection con = new SqlConnection(connStr))
                {
                    con.Open();
                    using (SqlCommand cmd = con.CreateCommand())
                    {
                        cmd.CommandText = sql;
                        cmd.Parameters.Add(new SqlParameter("@TeacherNum", username));
                        //cmd.Parameters.Add(new SqlParameter("@StuPwd", password));


                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string state = reader.GetString(reader.GetOrdinal("State"));

                                if (state == "正常")
                                {
                                    teacher.IsLogin = "Success";
                                    #region 填充教师信息

                                    //教师编号
                                    teacher.TeacherNum = username;


                                    //姓名
                                    if (reader["TeacherName"] is DBNull)
                                    {
                                        teacher.TeacherName = "";
                                    }
                                    else
                                    {
                                        teacher.TeacherName = reader.GetString(reader.GetOrdinal("TeacherName"));
                                    }

                                    //性别
                                    if (reader["Sex"] is DBNull)
                                    {
                                        teacher.Sex = "";
                                    }
                                    else
                                    {
                                        teacher.Sex = reader.GetString(reader.GetOrdinal("Sex"));
                                    }

                                    //身份证号码
                                    if (reader["IdentityCard"] is DBNull)
                                    {
                                        teacher.IdentityCard = "";
                                    }
                                    else
                                    {
                                        teacher.IdentityCard = reader.GetString(reader.GetOrdinal("IdentityCard"));
                                    }

                                    //学历
                                    if (reader["Education"] is DBNull)
                                    {
                                        teacher.Education = "";
                                    }
                                    else
                                    {
                                        teacher.Education = reader.GetString(reader.GetOrdinal("Education"));
                                    }

                                    //职业
                                    if (reader["Position"] is DBNull)
                                    {
                                        teacher.Position = "";
                                    }
                                    else
                                    {
                                        teacher.Position = reader.GetString(reader.GetOrdinal("Position"));
                                    }

                                    //专业
                                    if (reader["Major"] is DBNull)
                                    {
                                        teacher.Major = "";
                                    }
                                    else
                                    {
                                        teacher.Major = reader.GetString(reader.GetOrdinal("Major"));
                                    }


                                    //学院编码
                                    if (reader["InstituteNum"] is DBNull)
                                    {
                                        teacher.InstituteNum = "";
                                    }
                                    else
                                    {
                                        teacher.InstituteNum = reader.GetString(reader.GetOrdinal("InstituteNum"));
                                    }

                                    //学院名字
                                    if (reader["InstituteName"] is DBNull)
                                    {
                                        teacher.InstituteName = "";
                                    }
                                    else
                                    {
                                        teacher.InstituteName = reader.GetString(reader.GetOrdinal("InstituteName"));
                                    }

                                    //联系电话
                                    if (reader["Phone"] is DBNull)
                                    {
                                        teacher.Phone = "";
                                    }
                                    else
                                    {
                                        teacher.Phone = reader.GetString(reader.GetOrdinal("Phone"));
                                    }

                                    //邮箱
                                    if (reader["Email"] is DBNull)
                                    {
                                        teacher.Email = "";
                                    }
                                    else
                                    {
                                        teacher.Email = reader.GetString(reader.GetOrdinal("Email"));
                                    }

                                    //主讲课程一
                                    if (reader["CourseName1"] is DBNull)
                                    {
                                        teacher.CourseName1 = "";
                                    }
                                    else
                                    {
                                        teacher.CourseName1 = reader.GetString(reader.GetOrdinal("CourseName1"));
                                    }

                                    //主讲课程二
                                    if (reader["CourseName2"] is DBNull)
                                    {
                                        teacher.CourseName2 = "";
                                    }
                                    else
                                    {
                                        teacher.CourseName2 = reader.GetString(reader.GetOrdinal("CourseName2"));
                                    }

                                    //主讲课程三
                                    if (reader["CourseName3"] is DBNull)
                                    {
                                        teacher.CourseName3 = "";
                                    }
                                    else
                                    {
                                        teacher.CourseName3 = reader.GetString(reader.GetOrdinal("CourseName3"));
                                    }

                                    //状态
                                    if (reader["State"] is DBNull)
                                    {
                                        teacher.State = "正常";
                                    }
                                    else
                                    {
                                        teacher.State = reader.GetString(reader.GetOrdinal("State"));
                                    }

                                    //注册时间
                                    if (reader["SubDate"] is DBNull)
                                    {
                                        teacher.SubDate = DateTime.Now;
                                    }
                                    else
                                    {
                                        teacher.SubDate = reader.GetDateTime(reader.GetOrdinal("SubDate"));
                                    }

                                    //删除标识
                                    if (reader["DelFlag"] is DBNull)
                                    {
                                        teacher.DelFlag = 0;
                                    }
                                    else
                                    {
                                        teacher.DelFlag = reader.GetInt32(reader.GetOrdinal("DelFlag"));
                                    }

                                    //备注
                                    if (reader["Remark"] is DBNull)
                                    {
                                        teacher.Remark = "";
                                    }
                                    else
                                    {
                                        teacher.Remark = reader.GetString(reader.GetOrdinal("Remark"));
                                    }


                                    #endregion

                                    return teacher;
                                }
                                else
                                {
                                    teacher.IsLogin = "Error_State";
                                    return teacher;
                                }
                            }
                            else//用户不存在
                            {
                                teacher.IsLogin = "Error_UserNotExist";
                                return teacher;
                            }
                        }//

                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }//


        /// <summary>
        /// 根据条件获取教师信息列表
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="param">参数</param>
        /// <returns>教师信息列表</returns>
        public static List<TeacherInfo> GetAllTeacherInfo(string sql, params SqlParameter[] param)
        {
            DataTable dt = new DataTable();
            List<TeacherInfo> list = new List<TeacherInfo>();

            dt = SqlHelper.ExecuteTable(sql, param);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    TeacherInfo teacher = new TeacherInfo();
                    teacher = RowToEntity(dr);

                    list.Add(teacher);
                }
            }

            return list;
        }

        
        /// <summary>
        /// 【DataRow转TeacherInfo】行转实体
        /// </summary>
        /// <param name="dr">一行记录</param>
        /// <returns>一个实体</returns>
        public static TeacherInfo RowToEntity(DataRow dr)
        {
            TeacherInfo teacher = new TeacherInfo();
              #region 填充教师信息

                    //教师编号
                    if (dr["TeacherNum"] is DBNull)
                    {
                        teacher.TeacherNum = "";
                    }
                    else
                    {
                        teacher.TeacherNum = dr["TeacherNum"].ToString();
                    }

                    //姓名
                    if (dr["TeacherName"] is DBNull)
                    {
                        teacher.TeacherName = "";
                    }
                    else
                    {
                        teacher.TeacherName = dr["TeacherName"].ToString();
                    }

                    //性别
                    if (dr["Sex"] is DBNull)
                    {
                        teacher.Sex = "";
                    }
                    else
                    {
                        teacher.Sex = dr["Sex"].ToString();
                    }

                    //身份证号码
                    if (dr["IdentityCard"] is DBNull)
                    {
                        teacher.IdentityCard = "";
                    }
                    else
                    {
                        teacher.IdentityCard = dr["IdentityCard"].ToString();
                    }

                    //学历
                    if (dr["Education"] is DBNull)
                    {
                        teacher.Education = "";
                    }
                    else
                    {
                        teacher.Education = dr["Education"].ToString();
                    }

                    //职业
                    if (dr["Position"] is DBNull)
                    {
                        teacher.Position = "";
                    }
                    else
                    {
                        teacher.Position = dr["Position"].ToString();
                    }

                    //专业
                    if (dr["Major"] is DBNull)
                    {
                        teacher.Major = "";
                    }
                    else
                    {
                        teacher.Major = dr["Major"].ToString();
                    }


                    //学院编码
                    if (dr["InstituteNum"] is DBNull)
                    {
                        teacher.InstituteNum = "";
                    }
                    else
                    {
                        teacher.InstituteNum = dr["InstituteNum"].ToString();
                    }

                    //学院名字
                    if (dr["InstituteName"] is DBNull)
                    {
                        teacher.InstituteName = "";
                    }
                    else
                    {
                        teacher.InstituteName = dr["InstituteName"].ToString();
                    }

                    //联系电话
                    if (dr["Phone"] is DBNull)
                    {
                        teacher.Phone = "";
                    }
                    else
                    {
                        teacher.Phone = dr["Phone"].ToString();
                    }

                    //邮箱
                    if (dr["Email"] is DBNull)
                    {
                        teacher.Email = "";
                    }
                    else
                    {
                        teacher.Email = dr["Email"].ToString();
                    }

                    //主讲课程一
                    if (dr["CourseName1"] is DBNull)
                    {
                        teacher.CourseName1 = "";
                    }
                    else
                    {
                        teacher.CourseName1 = dr["CourseName1"].ToString();
                    }

                    //主讲课程二
                    if (dr["CourseName2"] is DBNull)
                    {
                        teacher.CourseName2 = "";
                    }
                    else
                    {
                        teacher.CourseName2 = dr["CourseName2"].ToString();
                    }

                    //主讲课程三
                    if (dr["CourseName3"] is DBNull)
                    {
                        teacher.CourseName3 = "";
                    }
                    else
                    {
                        teacher.CourseName3 = dr["CourseName3"].ToString();
                    }

                    //状态
                    if (dr["State"] is DBNull)
                    {
                        teacher.State = "正常";
                    }
                    else
                    {
                        teacher.State = dr["State"].ToString();
                    }

                    //注册时间
                    if (dr["SubDate"] is DBNull)
                    {
                        teacher.SubDate = DateTime.Now;
                    }
                    else
                    {
                        teacher.SubDate = Convert.ToDateTime(dr["SubDate"]);
                    }

                    //删除标识
                    if (dr["DelFlag"] is DBNull)
                    {
                        teacher.DelFlag = 0;
                    }
                    else
                    {
                        teacher.DelFlag = Convert.ToInt32(dr["DelFlag"]);
                    }

                    //备注
                    if (dr["Remark"] is DBNull)
                    {
                        teacher.Remark = "";
                    }
                    else
                    {
                        teacher.Remark = dr["Remark"].ToString();
                    }


                    #endregion

            return teacher;
        }


        /// <summary>
        /// 【删除】注册用户
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="param">参数列表</param>
        /// <returns>受影响行数</returns>
        public static int DeleteTeacherInfo(string sql, params SqlParameter[] param)
        {
            return SqlHelper.ExecuteNonQuery(sql, param);
        }
    
    }
}