using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace 学生选课信息管理系统.Models
{
    public class AllUserInfo
    {
        public int ListID { get; set; }//表的编号
        public string UserNum { get; set; }//用户账号

        public string UserName { get; set; }//用户姓名

        public string Sex { get; set; }//性别

        public string IdentityCard { get; set; }//身份证号码

        public string Role { get; set; }//角色

        /// <summary>
        /// 【管理员】获取所有注册待验证的用户
        /// </summary>
        /// <param name="level">管理员级别</param>
        /// <returns></returns>
        public static List<AllUserInfo> GetAllRegisterUserInfo(string level)
        {
            List<AllUserInfo> allList = new List<AllUserInfo>();
            int k = 1;
            //①加载学生信息列表
            //①--------1读取信息
            string sql1 = "Select s.*,a.InstituteName from StuInfo s, InstituteInfo a where s.InstituteNum = a.InstituteNum and s.State = @State and s.DelFlag = 0";
            SqlParameter[] p1 = { new SqlParameter("@State", "待检验") };
            List<StuInfo> stuList = StuInfo.GetAllStuInfo(sql1, p1);
            //①--------2添加
            foreach(StuInfo stu in stuList)
            {
                AllUserInfo all = new AllUserInfo();
                all.ListID = k;
                all.UserNum = stu.StuNum;
                all.UserName = stu.StuName;
                all.Sex = stu.Sex;
                all.IdentityCard = stu.IdentityCard;
                all.Role = "学生";
                allList.Add(all);
                k++;
            }

            //②加载教师信息列表
            //②-----------1读取信息
            string sql2 = "Select t.*, a.InstituteName from TeacherInfo t, InstituteInfo a where t.State = @State and t.DelFlag = 0 and t.InstituteNum = a.InstituteNum";
            SqlParameter p2 = new SqlParameter("@State", "待检验");
            List<TeacherInfo> teaList = TeacherInfo.GetAllTeacherInfo(sql2, p2);
            //②------------2添加
            foreach (TeacherInfo teacher in teaList)
            {
                AllUserInfo all = new AllUserInfo();
                all.ListID = k;
                all.UserNum = teacher.TeacherNum;
                all.UserName = teacher.TeacherName;
                all.Sex = teacher.Sex;
                all.IdentityCard = teacher.IdentityCard;
                all.Role = "教师";
                allList.Add(all);
                k++;
            }

            //③加载管理员信息列表
            if (level == "0")
            {
                //③-------------1读取信息
                string sql3 = "Select * from AdminInfo where State = @State and DelFlag = 0";
                SqlParameter p3 = new SqlParameter("@State","待检验");
                List<AdminInfo> adminList = new List<AdminInfo>();
                adminList = AdminInfo.GetAllAdminInfo(sql3, p3);

                //③-------------2添加
                foreach (AdminInfo a in adminList)
                {
                    AllUserInfo all = new AllUserInfo();
                    all.ListID = k;
                    all.UserNum = a.AdminNum;
                    all.UserName = a.AdminName;
                    all.Sex = a.Sex;
                    all.IdentityCard = "";
                    all.Role = "管理员";
                    allList.Add(all);
                    k++;

                }
            }

            return allList;
        }

        /// <summary>
        /// 【删除】注册待检验用户
        /// </summary>
        /// <param name="userNum">用户帐号</param>
        /// <param name="role">角色</param>
        /// <returns>成功与否的字符串</returns>
        public static string DeleteRegisterUser(string userNum, string role)
        {
            if (!string.IsNullOrEmpty(role) && !string.IsNullOrEmpty(userNum))
            {
                if (role == "学生")
                {
                    #region 执行删除学生
                    string sql = "Update StuInfo set DelFlag = 1 where StuNum = @StuNum";
                    SqlParameter[] param = {
                                               new SqlParameter("@StuNum",userNum)
                                           };

                    if (StuInfo.DeleteStuInfo(sql, param) > 0)
                    {
                        return "Success";
                    }
                    else
                    {
                        return "Error";
                    }
                    #endregion
                }
                else if (role == "教师")
                {
                    #region 执行删除教师
                    string sql = "Update TeacherInfo set DelFlag = 1 where TeacherNum = @TeacherNum";
                    SqlParameter[] param = {
                                               new SqlParameter("@TeacherNum",userNum)
                                           };

                    if (TeacherInfo.DeleteTeacherInfo(sql, param) > 0)
                    {
                        return "Success";
                    }
                    else
                    {
                        return "Error";
                    }
                    #endregion
                }
                else if (role == "管理员")
                {
                    #region 执行删除管理员
                    string sql = "Update AdminInfo set DelFlag = 1 where AdminNum = @AdminNum";
                    SqlParameter[] param = {
                                               new SqlParameter("@AdminNum",userNum)
                                           };

                    if (AdminInfo.DeleteAdminInfo(sql, param) > 0)
                    {
                        return "Success";
                    }
                    else
                    {
                        return "Error";
                    }
                    #endregion
                }
                else
                {
                    return "Error";//【Role】的值不明确
                }
            }
            else
            {
                return "Error";//【Role】或者【userNum】的值为空或者是null
            }
        }


        /// <summary>
        /// 【通过】注册用户
        /// </summary>
        /// <param name="userNum">用户账号</param>
        /// <param name="role">角色</param>
        /// <returns>成功与否的字符串</returns>
        public static string PassRegisterUser(string userNum, string role)
        {
            if (!string.IsNullOrEmpty(role) && !string.IsNullOrEmpty(userNum))
            {
                if (role == "学生")
                {
                    #region 执行删除学生
                    string sql = "Update StuInfo set State = @State where StuNum = @StuNum";
                    SqlParameter[] param = {
                                               new SqlParameter("@StuNum",userNum),
                                               new SqlParameter("@State","正常")
                                           };

                    if (StuInfo.DeleteStuInfo(sql, param) > 0)
                    {
                        return "Success";
                    }
                    else
                    {
                        return "Error";
                    }
                    #endregion
                }
                else if (role == "教师")
                {
                    #region 执行删除教师
                    string sql = "Update TeacherInfo set State = @State where TeacherNum = @TeacherNum";
                    SqlParameter[] param = {
                                               new SqlParameter("@TeacherNum",userNum),
                                               new SqlParameter("@State","正常")
                                           };

                    if (TeacherInfo.DeleteTeacherInfo(sql, param) > 0)
                    {
                        return "Success";
                    }
                    else
                    {
                        return "Error";
                    }
                    #endregion
                }
                else if (role == "管理员")
                {
                    #region 执行删除管理员
                    string sql = "Update AdminInfo set State = @State where AdminNum = @AdminNum";
                    SqlParameter[] param = {
                                               new SqlParameter("@AdminNum",userNum),
                                               new SqlParameter("@State","正常")
                                           };

                    if (AdminInfo.DeleteAdminInfo(sql, param) > 0)
                    {
                        return "Success";
                    }
                    else
                    {
                        return "Error";
                    }
                    #endregion
                }
                else
                {
                    return "Error";//【Role】的值不明确
                }
            }
            else
            {
                return "Error";//【Role】或者【userNum】的值为空或者是null
            }
        }


    }
}