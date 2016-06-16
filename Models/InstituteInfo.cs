using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace 学生选课信息管理系统.Models
{
    public class InstituteInfo
    {
        public int Id { get; set; }//学院信息表编号

        public string InstituteNum { get; set; }//学院编号

        public string InstituteName { get; set; }//学院名字

        public string InstituteManagerNum { get; set; }//系主任编号（教师编号）

        public string State { get; set; }//状态

        public int DelFlag { get; set; }//删除标识

        public string Remark { get; set; }//备注
    }
}