namespace ExecuteQueryCountSample
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器
        /// 修改這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.QueryCountButton = new System.Windows.Forms.Button();
            this.DisplayTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // QueryCountButton
            // 
            this.QueryCountButton.Location = new System.Drawing.Point(12, 12);
            this.QueryCountButton.Name = "QueryCountButton";
            this.QueryCountButton.Size = new System.Drawing.Size(121, 91);
            this.QueryCountButton.TabIndex = 9;
            this.QueryCountButton.Text = "QueryCount";
            this.QueryCountButton.UseVisualStyleBackColor = true;
            this.QueryCountButton.Click += new System.EventHandler(this.QueryCountButton_Click);
            // 
            // DisplayTextBox
            // 
            this.DisplayTextBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.DisplayTextBox.Font = new System.Drawing.Font("Consolas", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DisplayTextBox.Location = new System.Drawing.Point(0, 109);
            this.DisplayTextBox.Multiline = true;
            this.DisplayTextBox.Name = "DisplayTextBox";
            this.DisplayTextBox.Size = new System.Drawing.Size(710, 373);
            this.DisplayTextBox.TabIndex = 14;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(710, 482);
            this.Controls.Add(this.DisplayTextBox);
            this.Controls.Add(this.QueryCountButton);
            this.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Form1";
            this.Text = "ExecuteQueryCountSample";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button QueryCountButton;
        private System.Windows.Forms.TextBox DisplayTextBox;
    }
}

