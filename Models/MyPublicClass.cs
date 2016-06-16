using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using 学生选课信息管理系统.Models;

namespace 学生选课信息管理系统.Models
{
    public class MyPublicClass
    {
        //连接字符串
        private static readonly string connStr = ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;

        /// <summary>
        /// 登陆验证
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="param">参数列表</param>
        /// <returns></returns>
        public static string CheckLogin(string sql ,params SqlParameter[] param)
        {
            try
            {
                //string sql = "Select * from StuInfo where StuNum = @StuNum and  StuPwd = @StuPwd and DelFlag = 0";

                using (SqlConnection con = new SqlConnection(connStr))
                {
                    con.Open();
                    using (SqlCommand cmd = con.CreateCommand())
                    {
                        cmd.CommandText = sql;
                        //cmd.Parameters.Add(new SqlParameter("@StuNum", username));
                        //cmd.Parameters.Add(new SqlParameter("@StuPwd", passwrord));
                        cmd.Parameters.AddRange(param);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string state = reader.GetString(reader.GetOrdinal("State"));

                                if (state == "正常")
                                {
                                    return "Success";
                                }
                                else if (state == "禁用")//禁用状态
                                {
                                    return "Error_State";
                                }
                                else//待检验状态
                                {
                                    return "ReadyToCheck";
                                }
                            }
                            else
                            {
                                return "Error_UserNotExist";
                            }
                        }
                    }
                }
            }
            catch//(Exception e)
            {
                return "Error_Check";
                //throw e;
            }
        }//

        /// <summary>
        /// 更改用户密码
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="param">参数列表</param>
        /// <returns></returns>
        public static int UpdateUserPwd( string sql, params SqlParameter[] param)
        {
            try
            {
                return SqlHelper.ExecuteNonQuery(sql, param);

            }
            catch(Exception e)
            {
                throw e;
            }
        }

    }
}