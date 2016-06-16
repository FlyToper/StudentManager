using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;

namespace 学生选课信息管理系统.Models
{
    public class StuInfo
    {
        public int Id { get; set; }//编号

        public string StuNum { get; set; }//学号

        public string StuPwd { get; set; }//密码

        public string StuName { get; set; }//姓名

        public string Sex { get; set; }//性别

        public string IdentityCard { get; set; }//身份证号码

        public string Major { get; set; }//专业

        public string ClassName { get; set; }//班级名称

        public string InstituteNum { get; set; }//学院编号

        public DateTime EnrollmentDate { get; set; }//入学日期

        public string Nation { get; set; }//民族

        public string Native { get; set; }//籍贯

        public string Email { get; set; }//邮箱

        public string Phone { get; set; }//联系电话

        public string State { get; set; }//状态

        public DateTime SubDate { get; set; }//提交时间

        public int DelFlag { get; set; }//删除标识

        public string Remark { get; set; }//备注

        //附加属性

       public string IsLogin { get; set; }//验证标识

        public string InstituteName { get; set; }//学院名字



       // private static readonly string connStr = "Data Source=PC-201310041430;Initial Catalog=SCDB;Integrated Security=True";
        private static readonly string connStr = ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;
        /// <summary>
        /// 登录验证
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <returns>响应信息</returns>
        public static StuInfo GetStuInfo(string username)
        {
            StuInfo stu = new StuInfo();
            string sql = "Select * from StuInfo a,InstituteInfo b  where a.InstituteNum = b.InstituteNum and StuNum = @StuNum and a.DelFlag = 0 ";

            try
            {
                using (SqlConnection con = new SqlConnection(connStr))
                {
                    con.Open();
                    using (SqlCommand cmd = con.CreateCommand())
                    {
                        cmd.CommandText = sql;
                        cmd.Parameters.Add(new SqlParameter("@StuNum", username));
                        //cmd.Parameters.Add(new SqlParameter("@StuPwd", password));

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string state = reader.GetString(reader.GetOrdinal("State"));

                                if (state == "正常")
                                {
                                    stu.IsLogin = "Success";
                                    #region 填充学生信息

                                    stu.StuNum = username;


                                    //姓名
                                    if (reader["StuName"] is DBNull)
                                    {
                                        stu.StuName = "";
                                    }
                                    else
                                    {
                                        stu.StuName = reader.GetString(reader.GetOrdinal("StuName"));
                                    }

                                    //性别
                                    if (reader["Sex"] is DBNull)
                                    {
                                        stu.Sex = "";
                                    }
                                    else
                                    {
                                        stu.Sex = reader.GetString(reader.GetOrdinal("Sex"));
                                    }

                                    //身份证号码
                                    if (reader["IdentityCard"] is DBNull)
                                    {
                                        stu.IdentityCard = "";
                                    }
                                    else
                                    {
                                        stu.IdentityCard = reader.GetString(reader.GetOrdinal("IdentityCard"));
                                    }

                                    //专业
                                    if (reader["Major"] is DBNull)
                                    {
                                        stu.Major = "";
                                    }
                                    else
                                    {
                                        stu.Major = reader.GetString(reader.GetOrdinal("Major"));
                                    }

                                    //班级名称
                                    if (reader["ClassName"] is DBNull)
                                    {
                                        stu.ClassName = "";
                                    }
                                    else
                                    {
                                        stu.ClassName = reader.GetString(reader.GetOrdinal("ClassName"));
                                    }

                                    //学院编码
                                    if (reader["InstituteNum"] is DBNull)
                                    {
                                        stu.InstituteNum = "";
                                    }
                                    else
                                    {
                                        stu.InstituteNum = reader.GetString(reader.GetOrdinal("InstituteNum"));
                                    }

                                    //学院名字
                                    if (reader["InstituteName"] is DBNull)
                                    {
                                        stu.InstituteName = "";
                                    }
                                    else
                                    {
                                        stu.InstituteName = reader.GetString(reader.GetOrdinal("InstituteName"));
                                    }

                                    //入学时间
                                    if (reader["EnrollmentDate"] is DBNull)
                                    {
                                        stu.EnrollmentDate = DateTime.Now;
                                    }
                                    else
                                    {
                                        stu.EnrollmentDate = reader.GetDateTime(reader.GetOrdinal("EnrollmentDate"));
                                    }

                                    //民族
                                    if (reader["Nation"] is DBNull)
                                    {
                                        stu.Nation = "";
                                    }
                                    else
                                    {
                                        stu.Nation = reader.GetString(reader.GetOrdinal("Nation"));
                                    }

                                    //籍贯
                                    if (reader["Native"] is DBNull)
                                    {
                                        stu.Native = "";
                                    }
                                    else
                                    {
                                        stu.Native = reader.GetString(reader.GetOrdinal("Native"));
                                    }

                                    //联系电话
                                    if (reader["Phone"] is DBNull)
                                    {
                                        stu.Phone = "";
                                    }
                                    else
                                    {
                                        stu.Phone = reader.GetString(reader.GetOrdinal("Phone"));
                                    }

                                    //邮箱
                                    if (reader["Email"] is DBNull)
                                    {
                                        stu.Email = "";
                                    }
                                    else
                                    {
                                        stu.Email = reader.GetString(reader.GetOrdinal("Email"));
                                    }

                                    //状态
                                    if (reader["State"] is DBNull)
                                    {
                                        stu.State = "正常";
                                    }
                                    else
                                    {
                                        stu.State = reader.GetString(reader.GetOrdinal("State"));
                                    }

                                    //提交时间
                                    if (reader["SubDate"] is DBNull)
                                    {
                                        stu.SubDate = DateTime.Now;
                                    }
                                    else
                                    {
                                        stu.SubDate = reader.GetDateTime(reader.GetOrdinal("SubDate"));
                                    }

                                    //删除标识
                                    if (reader["DelFlag"] is DBNull)
                                    {
                                        stu.DelFlag = 0;
                                    }
                                    else
                                    {
                                        stu.DelFlag = reader.GetInt32(reader.GetOrdinal("DelFlag"));
                                    }

                                    //备注
                                    if (reader["Remark"] is DBNull)
                                    {
                                        stu.Remark = "";
                                    }
                                    else
                                    {
                                        stu.Remark = reader.GetString(reader.GetOrdinal("Remark"));
                                    }


                                    #endregion

                                    return stu;
                                }
                                else
                                {
                                    stu.IsLogin = "Error_State";
                                    return stu;
                                }
                            }
                            else//用户不存在
                            {
                                stu.IsLogin = "Error_UserNotExist";
                                return stu;
                            }
                        }

                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }//

        /// <summary>
        ///【 行转实体】
        /// </summary>
        /// <param name="dr">行记录</param>
        /// <returns></returns>
        public static StuInfo RowToEntity(DataRow dr)
        {
            StuInfo s = new StuInfo();

            #region 数据填充

            //学号
            if (dr["StuNum"] is DBNull)
            {
                s.StuNum = "";
            }
            else
            {
                s.StuNum = dr["StuNum"].ToString();
            }

            //姓名
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

            //班级名字
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

            //学院名字
            if (dr["InstituteName"] is DBNull)
            {
                s.InstituteName = "";
            }
            else
            {
                s.InstituteName = dr["InstituteName"].ToString();
            }

            //入学时间
            if (dr["EnrollmentDate"] is DBNull)
            {
                s.EnrollmentDate = DateTime.Now;
            }
            else
            {
                s.EnrollmentDate =Convert.ToDateTime( dr["EnrollmentDate"]);
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

            return s;

        }

        //public string CheckLogin(string username, string passwrord)
        //{
        //    try
        //    {
        //        string sql = "Select * from StuInfo where StuNum = @StuNum and  StuPwd = @StuPwd and DelFlag = 0";

        //        using (SqlConnection con = new SqlConnection(connStr))
        //        {
        //            con.Open();
        //            using (SqlCommand cmd = con.CreateCommand())
        //            {
        //                cmd.CommandText = sql;
        //                cmd.Parameters.Add(new SqlParameter("@StuNum", username));
        //                cmd.Parameters.Add(new SqlParameter("@StuPwd", passwrord));

        //                using (SqlDataReader reader = cmd.ExecuteReader())
        //                {
        //                    if (reader.Read())
        //                    {
        //                        string state = reader.GetString(reader.GetOrdinal("State"));

        //                        if (state != "禁用")
        //                        {
        //                            return "Success";
        //                        }
        //                        else
        //                        {
        //                            return "Error_State";
        //                        }
        //                    }
        //                    else
        //                    {
        //                        return "Error_UserNotExist";
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch
        //    {
        //        return "Error_Check";
        //    }
        //}//

        /// <summary>
        /// 根据条件获取学生的列表
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="param">参数</param>
        /// <returns>学生信息表数组</returns>
        public static List<StuInfo> GetAllStuInfo(string sql, SqlParameter[] param)
        {
            DataTable dt = SqlHelper.ExecuteTable(sql, param);
            List<StuInfo> list = new List<StuInfo>();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    StuInfo s = RowToEntity(dr);

                    list.Add(s);
                }
            }

            return list;
        }

        /// <summary>
        /// 【删除】注册用户
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="param">参数列表</param>
        /// <returns>受影响行数</returns>
        public static int DeleteStuInfo(string sql, params SqlParameter[] param)
        {
            return SqlHelper.ExecuteNonQuery(sql, param);
        }
    }
}