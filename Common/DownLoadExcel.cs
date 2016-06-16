using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.SS.Formula.Functions;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using 学生选课信息管理系统.Models;

namespace 学生选课信息管理系统.Common
{
    public class DownLoadExcel
    {
        /// <summary>
        /// 【管理员信息表下载】
        /// </summary>
        /// <param name="DataList">信息表</param>
        /// <returns></returns>
        public static byte[] Admin_ExecuteDownLoad(List<AdminInfo> DataList)
        {
            //【①】定义一个工作簿
            HSSFWorkbook book = new HSSFWorkbook();

            //【②】新建一个sheet表格
            ISheet sheet1 = book.CreateSheet("sheet1");

            //【③】设置标题行
            //【③--①】新建标题行【第0行】row0
            IRow row0 = sheet1.CreateRow(0);//sheet的【第一行】

            //【③--②】设置标题行相关样式 style1
            ICellStyle style1 = book.CreateCellStyle();//创建样式对象
            IFont font1 = book.CreateFont(); //创建一个字体样式对象
            font1.FontName = "宋体"; //和excel里面的字体对应
            font1.Color = new HSSFColor.BLUE_GREY().GetIndex();//颜色参考NPOI的颜色对照表(替换掉PINK())
            //font.IsItalic = false; //斜体
            font1.FontHeightInPoints = 16;//字体大小
            font1.Boldweight = short.MaxValue;//字体加粗
            style1.SetFont(font1); //将字体样式赋给样式对象
            style1.Alignment = HorizontalAlignment.CENTER;//居中对齐
            //cell.CellStyle = style; //把样式赋给单元格

            //【③--①】设置标题的值【第0行】SetCellValue
            List<string> titleList = new List<string> { "账号", "姓名", "性别", "邮箱","联系电话","管理级别","注册时间","备注"};
            for (int i = 0; i < titleList.Count; i++)
            {
                ICell cell = row0.CreateCell(i);
                cell.SetCellValue(titleList[i]);
                cell.CellStyle = style1;
            }


            //【④】设置数据行
            //【④---①】设置样式 style2
            ICellStyle style2 = book.CreateCellStyle();//创建样式对象
            IFont font2 = book.CreateFont(); //创建一个字体样式对象
            font2.FontName = "宋体"; //和excel里面的字体对应
            font2.Color = new HSSFColor.BLACK().GetIndex();//颜色参考NPOI的颜色对照表(替换掉PINK())
            //font.IsItalic = false; //斜体
            font2.FontHeightInPoints = 12;//字体大小
            font2.Boldweight = short.MaxValue;//字体加粗
            style2.SetFont(font2); //将字体样式赋给样式对象
            style2.Alignment = HorizontalAlignment.CENTER;//居中对齐

            //【④-----②】设置列宽
            int[] columnWidths = { 20, 15, 15, 30, 30, 15, 30, 15 };
            //【④-----③】设置数值
            for (int i = 0; i < DataList.Count; i++)
            {
                string[] a1 = {DataList[i].AdminNum, DataList[i].AdminName, DataList[i].Sex, DataList[i].Email, DataList[i].Phone, DataList[i].Level, DataList[i].SubDate.ToString(), DataList[i].Remark};//转换成数据，方便读取操作

                IRow rows = sheet1.CreateRow(i + 1);
                for (int j = 0; j < titleList.Count; j++)
                {
                    ICell cells = rows.CreateCell(j);
                    cells.SetCellValue(a1[j]);
                    cells.CellStyle = style2;
                    sheet1.SetColumnWidth(j, columnWidths[j]*256);
                }
            }


            // 写入到客户端  
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            book.Write(ms);
            //Response.AddHeader("Content-Disposition", string.Format("attachment; filename={0}.xls", DateTime.Now.ToString("yyyyMMddHHmmssfff")));
            //Response.BinaryWrite(ms.ToArray());

            //byte[] fileContents = Encoding.UTF8.GetBytes(book.ToString());

            //return File(fileContents, "application/ms-excel", "newfileContents.xls");

            byte[] fileContents = ms.ToArray();
            book = null;
            ms.Close();
            ms.Dispose();

            return fileContents;
        }
    }
}