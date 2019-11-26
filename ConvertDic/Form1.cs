using ConvertDic.dicConvert;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConvertDic
{
    public partial class Form1 : Form
    {
        private DicConvert dc = new DicConvert();
        public Form1()
        {
            InitializeComponent();
        }

        private void BtnSelectPath_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = true;
            fileDialog.Title = "请选择文件";
            fileDialog.Filter = "所有文件(*.*)|*.*";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                string file = fileDialog.FileName;
                textPath.Text = file;
            }
        }

        private void BtnConvert_Click(object sender, EventArgs e)
        {
            string[] sheetGroup = dc.GetExcelSheetNames(textPath.Text);
            //Dictionary<string, string>[] dic = new Dictionary<string, string>[];
            //dic = 
                GetDicKV();
        }

        private Dictionary<string, string>[] GetDicKV()
        {
            return dc.GetKV(textPath.Text);
        }
    }
}
