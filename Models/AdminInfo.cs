using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace 学生选课信息管理系统.Models
{
    public class AdminInfo
    {
        public int Id { get; set; }//管理员信息表编号

        public string AdminNum { get; set; }//管理员编号

        public string AdminPwd { get; set;}//密码

        public string AdminName { get; set; }//姓名

        public string Sex { get; set; }//性别

        public string Email { get; set; }//邮箱

        public string Phone { get; set; }//联系电话号码

        public string Level { get; set; }//级别

        public string State { get; set; }//状态

        public DateTime SubDate { get; set; }//提交时间

        public string Remark { get; set; }//备注

        /// <summary>
        /// 返回管理员的信息
        /// </summary>
        /// <param name="adminNum">管理员的账号</param>
        /// <returns></returns>
        public static AdminInfo GetAdminInfo(string adminNum)
        {
            AdminInfo a1 = new AdminInfo();
            try
            {
                string sql = "Select * from AdminInfo where adminNum = @adminNum";
                SqlParameter[] param = {
                                       new SqlParameter("@adminNum",adminNum)
                                   };
                DataTable dt = SqlHelper.ExecuteTable(sql, param);

                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    a1 = RowToEntity(dr);
                }

                return a1;
            }
            catch
            {
                return a1;
            }

            
        }

        /// <summary>
        /// 行记录转换成一个实体
        /// </summary>
        /// <param name="dr">一行记录</param>
        /// <returns></returns>
        public static AdminInfo RowToEntity(DataRow dr)
        {
            AdminInfo a1 = new AdminInfo();

            #region 数据填充
            //管理员编号
            if (dr["AdminNum"] is DBNull)
            {
                a1.AdminNum = "";
            }
            else
            {
                a1.AdminNum = dr["AdminNum"].ToString();
            }

            //管理员姓名
            if (dr["AdminName"] is DBNull)
            {
                a1.AdminName = "";
            }
            else
            {
                a1.AdminName = dr["AdminName"].ToString();
            }

            //性别
            if (dr["Sex"] is DBNull)
            {
                a1.Sex = "";
            }
            else
            {
                a1.Sex = dr["Sex"].ToString();
            }

            //邮箱
            if (dr["Email"] is DBNull)
            {
                a1.Email = "";
            }
            else
            {
                a1.Email = dr["Email"].ToString();
            }

            //联系电话
            if (dr["Phone"] is DBNull)
            {
                a1.Phone = "";
            }
            else
            {
                a1.Phone = dr["Phone"].ToString();
            }

            //级别
            if (dr["Level"] is DBNull)
            {
                a1.Level = "";
            }
            else
            {
                a1.Level = dr["Level"].ToString();
            }

            //状态
            if (dr["State"] is DBNull)
            {
                a1.State = "";
            }
            else
            {
                a1.State = dr["State"].ToString();
            }

            //注册时间
            if (dr["SubDate"] is DBNull)
            {
                a1.SubDate = DateTime.Now;
            }
            else
            {
                a1.SubDate = Convert.ToDateTime(dr["SubDate"].ToString());
            }

            //备注
            if (dr["Remark"] is DBNull)
            {
                a1.Remark = "";
            }
            else
            {
                a1.Remark = dr["Remark"].ToString();
            }
            #endregion

            return a1;

        }


        /// <summary>
        /// 根据条件获取管理员信息列表
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="param">参数</param>
        public static List<AdminInfo> GetAllAdminInfo(string sql, params SqlParameter[] param)
        {
            DataTable dt = SqlHelper.ExecuteTable(sql,param);
            List<AdminInfo> list = new List<AdminInfo>();

            if(dt.Rows.Count > 0)
            {
                foreach(DataRow dr in dt.Rows )
                {
                    AdminInfo a1= RowToEntity(dr);
                    list.Add(a1);
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
        public static int DeleteAdminInfo(string sql, params SqlParameter[] param)
        {
            return SqlHelper.ExecuteNonQuery(sql, param);
        }
    }
}