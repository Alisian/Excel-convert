namespace WindowsFormsApp1
{
    partial class ConvertForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.Path = new System.Windows.Forms.Label();
            this.btnConvert = new System.Windows.Forms.Button();
            this.textPath = new System.Windows.Forms.TextBox();
            this.btnSelectPath = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Path
            // 
            this.Path.AutoSize = true;
            this.Path.Location = new System.Drawing.Point(113, 142);
            this.Path.Name = "Path";
            this.Path.Size = new System.Drawing.Size(41, 12);
            this.Path.TabIndex = 0;
            this.Path.Text = "Path：";
            // 
            // btnConvert
            // 
            this.btnConvert.Location = new System.Drawing.Point(311, 191);
            this.btnConvert.Name = "btnConvert";
            this.btnConvert.Size = new System.Drawing.Size(75, 23);
            this.btnConvert.TabIndex = 1;
            this.btnConvert.Text = "Convert";
            this.btnConvert.UseVisualStyleBackColor = true;
            this.btnConvert.Click += new System.EventHandler(this.BtnConvert_Click);
            // 
            // textPath
            // 
            this.textPath.Location = new System.Drawing.Point(160, 138);
            this.textPath.Name = "textPath";
            this.textPath.Size = new System.Drawing.Size(344, 21);
            this.textPath.TabIndex = 2;
            // 
            // btnSelectPath
            // 
            this.btnSelectPath.Location = new System.Drawing.Point(509, 137);
            this.btnSelectPath.Name = "btnSelectPath";
            this.btnSelectPath.Size = new System.Drawing.Size(33, 23);
            this.btnSelectPath.TabIndex = 3;
            this.btnSelectPath.Text = "....";
            this.btnSelectPath.UseVisualStyleBackColor = true;
            this.btnSelectPath.Click += new System.EventHandler(this.BtnSelectPath_Click);
            // 
            // ConvertForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnSelectPath);
            this.Controls.Add(this.textPath);
            this.Controls.Add(this.btnConvert);
            this.Controls.Add(this.Path);
            this.Name = "ConvertForm";
            this.Text = "ConvertForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Path;
        private System.Windows.Forms.Button btnConvert;
        private System.Windows.Forms.TextBox textPath;
        private System.Windows.Forms.Button btnSelectPath;
    }
}

