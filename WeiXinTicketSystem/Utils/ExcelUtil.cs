using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.IO;
using System.Reflection;

namespace WeiXinTicketSystem.Utils
{
    public static class ExcelUtil
    {
        public static FileStream WriteFile<T>(string filePath,IList<T> Data, List<string> propertyNameList)
        {
            FileStream fs = System.IO.File.Create(filePath);

            //创建Excel文件的对象
            NPOI.HSSF.UserModel.HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();
            //添加一个sheet
            NPOI.SS.UserModel.ISheet sheet1 = book.CreateSheet("Sheet1");
            ////给sheet1添加第一行的头部标题
            NPOI.SS.UserModel.IRow row1 = sheet1.CreateRow(0);
            ////row1.RowStyle.FillBackgroundColor = "";

            if(Data.Count()>0)
            {
                //通过反射得到对象的属性集合
                PropertyInfo[] propertys = Data[0].GetType().GetProperties();
                //遍历属性集合生成excel的表头标题
                for (int i = 0; i < propertys.Count(); i++)
                {
                    //判断此属性是否是用户定义属性
                    if (propertyNameList.Count() == 0)
                    {
                        row1.CreateCell(i).SetCellValue(propertys[i].Name);
                    }
                    else
                    {
                        row1.CreateCell(i).SetCellValue(propertyNameList[i]);
                    }
                }
                //将数据逐步写入sheet1各个行
                for (int i = 0; i < Data.Count(); i++)
                {
                    NPOI.SS.UserModel.IRow rowtemp = sheet1.CreateRow(i + 1);
                    for (int j = 0; j < propertys.Count(); j++)
                    {
                        object obj = propertys[j].GetValue(Data[i], null);
                        if (obj != null)
                        {
                            rowtemp.CreateCell(j).SetCellValue(obj.ToString());
                        }
                        else
                        {
                            rowtemp.CreateCell(j).SetCellValue("");
                        }
                    }
                }
            }
            // 写入到客户端 
            book.Write(fs);
            fs.Seek(0, SeekOrigin.Begin);
            return fs;
        }
    }
}