using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;

namespace ConvertDic.dicConvert
{
    public class DicConvert
    {
        private DataSet ds = new DataSet();
        public Dictionary<string, string>[] GetKV(string path)
        {
            int moNum = 0;
            for (int j = 0; j < ds.Tables["ModuleData"].Columns.Count; j++)
            {
                if (!string.IsNullOrEmpty(ds.Tables["ModuleData"].Columns[j].ColumnName.ToString()))
                {
                    moNum += 1;
                }
            }
            Dictionary<string, string>[] dicMo = new Dictionary<string, string>[moNum-1];

            int ttt = 0;
            for (int j = 0; j < ds.Tables["'2Data$"].Columns.Count; j++)
            {
                if (!string.IsNullOrEmpty(ds.Tables["'2Data$"].Columns[j].ColumnName.ToString()))
                {
                    ttt += 1;
                }
            }
            Dictionary<string, string>[] dicttt = new Dictionary<string, string>[ttt-1];

            int rrr = 0;
            for (int j = 0; j < ds.Tables["'3Data$"].Columns.Count; j++)
            {
                if (!string.IsNullOrEmpty(ds.Tables["'3Data$"].Columns[j].ColumnName.ToString()))
                {
                    rrr += 1;
                }
            }
            Dictionary<string, string>[] dicrrr = new Dictionary<string, string>[rrr-1];

            getEveryKv(ref dicMo, "ModuleData");
            getEveryKv(ref dicttt, "'2Data$");
            getEveryKv(ref dicrrr, "'3Data$");

            string tsNum = "";
            generateTsType(dicMo, dicttt, dicrrr, ref tsNum);
            string[] tsTp = (tsNum.Substring(0, tsNum.Length-1)).Split(',');
            generateTs(dicMo, dicttt, dicrrr, tsTp, path);


            return null;
        }

        public void generateTsType(Dictionary<string, string>[] dicMo, Dictionary<string, string>[] dicttt, Dictionary<string, string>[] dicrrr,ref string tsNum)
        {
            setTsType(dicMo,ref tsNum);
            setTsType(dicttt, ref tsNum);
            setTsType(dicrrr, ref tsNum);
        }

        public void generateTs(Dictionary<string, string>[] dicMo, Dictionary<string, string>[] dicttt, Dictionary<string, string>[] dicrrr, string[] tsTp,string path)
        {
            for (int i = 0; i < tsTp.Length; i++)
            {
                string fileContaner = "";
                string tsType = tsTp[i].ToString();
                fileContaner += "uuuuuuu\r\n";
                fileContaner += "{\r\n";
                for (int j = 0; j < dicMo.Length; j++)
                {
                    if (dicMo[j]["Parameter"]== tsType)
                    {
                        foreach(var kv in dicMo[j])
                        {
                            fileContaner +="llll"+ kv.Key + "222"+ kv.Value+ "3333\r\n";
                        }
                    }
                }
                fileContaner += "}\r\n";

                fileContaner += "uuuuuuu\r\n";
                fileContaner += "{\r\n";
                for (int j = 0; j < dicttt.Length; j++)
                {
                    if (dicttt[j]["Parameter"] == tsType)
                    {
                        foreach (var kv in dicttt[j])
                        {
                            fileContaner += "llll" + kv.Key + "222" + kv.Value + "3333\r\n";
                        }
                    }
                }
                fileContaner += "}\r\n";

                fileContaner += "uuuuuuu\r\n";
                fileContaner += "{\r\n";
                for (int j = 0; j < dicrrr.Length; j++)
                {
                    if (dicrrr[j]["Parameter"] == tsType)
                    {
                        foreach (var kv in dicrrr[j])
                        {
                            fileContaner += "llll" + kv.Key + "222" + kv.Value + "3333\r\n";
                        }
                    }
                }
                fileContaner += "}\r\n";

                FileStream fs = new FileStream(path.Replace("TC Online Sample3.xlsx", tsType+".txt"), FileMode.OpenOrCreate);
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine(fileContaner);
                sw.Close();
            }
            
        }

        public void setTsType(Dictionary<string, string>[] dic,ref string tsNum)
        {
            for (int i = 0; i < dic.Length; i++)
            {
                if (!tsNum.Contains(dic[i]["Parameter"]))
                {
                    tsNum += dic[i]["Parameter"] + ",";
                }
            }
        }

        public void getEveryKv(ref Dictionary<string, string>[] dicName,string sheetName)
        {
            for (int i = 0; i < ds.Tables[sheetName].Rows.Count; i++)
            {
                for (int j = 0; j < ds.Tables[sheetName].Columns.Count; j++)
                {
                    string ColumnName = ds.Tables[sheetName].Columns[j].ColumnName;//取出列名
                    if (i < 1 && ColumnName != "Parameter")
                    {
                        Dictionary<string, string> dic = new Dictionary<string, string>();
                        dic.Add("Parameter", ColumnName);
                        dicName[j - 1] = dic;
                    }
                    if (j > 0 && ColumnName != "Parameter")
                    {
                        dicName[j - 1].Add(ds.Tables[sheetName].Rows[i]["Parameter"].ToString(), ds.Tables[sheetName].Rows[i][ColumnName].ToString());
                    }
                }
            }
        }

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
    }
}
