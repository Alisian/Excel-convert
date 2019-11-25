using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace WindowsFormsApp1.FileConvert
{
    public class FileCon
    {
        private DataSet ds = new DataSet();
        #region 获取Excel工作薄中Sheet页(工作表)名集合
        /// <summary> 
        /// 获取Excel工作薄中Sheet页(工作表)名集合
        /// </summary> 
        /// <param name="excelFile">Excel文件名及路径,EG:C:\Users\JK\Desktop\导入测试.xls</param> 
        /// <returns>Sheet页名称集合</returns> 
        public String[] GetExcelSheetNames(string fileName)
        {
            OleDbConnection objConn = null;
            System.Data.DataTable dt = null;
            try
            {
                string connString = string.Empty;
                string FileType = fileName.Substring(fileName.LastIndexOf("."));
                if (FileType == ".xls")
                    connString = "Provider=Microsoft.Jet.OLEDB.4.0;" +
                       "Data Source=" + fileName + ";Extended Properties=Excel 8.0;";
                else//.xlsx
                    connString = "Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=" + fileName + ";" + ";Extended Properties=\"Excel 12.0;HDR=YES;IMEX=1\"";
                // 创建连接对象 
                objConn = new OleDbConnection(connString);
                // 打开数据库连接 
                objConn.Open();
                // 得到包含数据架构的数据表 
                dt = objConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                if (dt == null)
                {
                    return null;
                }
                String[] excelSheets = new String[dt.Rows.Count];
                int i = 0;
                // 添加工作表名称到字符串数组 
                foreach (DataRow row in dt.Rows)
                {
                    string strSheetTableName = row["TABLE_NAME"].ToString();
                    string sql = "select * from [" + strSheetTableName + "]";
                    OleDbDataAdapter adapter = new OleDbDataAdapter(sql, objConn);
                    
                    //过滤无效SheetName
                    if (strSheetTableName.Contains("$") && strSheetTableName.Replace("'", "").EndsWith("$"))
                    {
                        excelSheets[i] = strSheetTableName.Substring(0, strSheetTableName.Length - 1);
                        adapter.Fill(ds, excelSheets[i]);
                    }
                    i++;
                }
                return excelSheets;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }
            finally
            {
                // 清理 
                if (objConn != null)
                {
                    objConn.Close();
                    objConn.Dispose();
                }
                if (dt != null)
                {
                    dt.Dispose();
                }
            }
        }
        #endregion

        public Dictionary<string, string> GetKV()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            string ColumnName = "";
            string ColumnName2 = "";
            string SJ = "";//单元格数据
            for (int j = 0; j < ds.Tables.Count; j++)//遍历sheet页数据
            {
                if (ds.Tables[j].TableName.Contains("Data"))
                {
                    for (int g = 0; g <= ds.Tables[j].Rows.Count; g++)//遍历每一行数据
                    {
                        for (int k = 0; k < ds.Tables[j].Columns.Count; k++)//遍历每单元格数据
                        {
                            ColumnName = ds.Tables[j].Columns[k].ColumnName;//取出列名
                            ColumnName2 = ds.Tables[j].Columns[k+1].ColumnName;//取出列名
                            SJ = ds.Tables[j].Rows[g][ColumnName].ToString();//获取指定单元格单元格数据
                            if (SJ.Contains("%"))
                            {
                                dic.Add(SJ, ds.Tables[j].Rows[g][ColumnName2].ToString());
                            }
                        }
                    }
                }
                
            }
            return dic;
        }
    }
}
