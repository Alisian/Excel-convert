using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.FileConvert;

namespace WindowsFormsApp1
{
    
    public partial class ConvertForm : Form
    {
        private FileCon fc = new FileCon();
        public ConvertForm()
        {
            InitializeComponent();
        }

        private void BtnConvert_Click(object sender, EventArgs e)
        {
            string[] sheetGroup = fc.GetExcelSheetNames(textPath.Text);
            Dictionary<string, string>dic = new Dictionary<string, string>();
            dic = GetDicKV();
        }

        private Dictionary<string, string>  GetDicKV()
        {
            return fc.GetKV();
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
    }
}
