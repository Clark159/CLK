namespace ExecuteReaderSample
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
            this.DisplayGridView = new System.Windows.Forms.DataGridView();
            this.UserId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UserName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QueryPartButton = new System.Windows.Forms.Button();
            this.IndexTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.CountTextBox = new System.Windows.Forms.TextBox();
            this.QueryAllButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.DisplayGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // DisplayGridView
            // 
            this.DisplayGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DisplayGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.UserId,
            this.UserName});
            this.DisplayGridView.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.DisplayGridView.Location = new System.Drawing.Point(0, 109);
            this.DisplayGridView.Name = "DisplayGridView";
            this.DisplayGridView.RowTemplate.Height = 24;
            this.DisplayGridView.Size = new System.Drawing.Size(710, 373);
            this.DisplayGridView.TabIndex = 10;
            // 
            // UserId
            // 
            this.UserId.DataPropertyName = "Id";
            this.UserId.HeaderText = "Id";
            this.UserId.Name = "UserId";
            // 
            // UserName
            // 
            this.UserName.DataPropertyName = "Name";
            this.UserName.HeaderText = "Name";
            this.UserName.Name = "UserName";
            // 
            // QueryPartButton
            // 
            this.QueryPartButton.Location = new System.Drawing.Point(328, 12);
            this.QueryPartButton.Name = "QueryPartButton";
            this.QueryPartButton.Size = new System.Drawing.Size(121, 91);
            this.QueryPartButton.TabIndex = 12;
            this.QueryPartButton.Text = "QueryPart";
            this.QueryPartButton.UseVisualStyleBackColor = true;
            this.QueryPartButton.Click += new System.EventHandler(this.QueryPartButton_Click);
            // 
            // IndexTextBox
            // 
            this.IndexTextBox.Location = new System.Drawing.Point(74, 12);
            this.IndexTextBox.Name = "IndexTextBox";
            this.IndexTextBox.Size = new System.Drawing.Size(121, 26);
            this.IndexTextBox.TabIndex = 14;
            this.IndexTextBox.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(13, 15);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 19);
            this.label3.TabIndex = 13;
            this.label3.Text = "Index";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 48);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 19);
            this.label1.TabIndex = 15;
            this.label1.Text = "Count";
            // 
            // CountTextBox
            // 
            this.CountTextBox.Location = new System.Drawing.Point(74, 45);
            this.CountTextBox.Name = "CountTextBox";
            this.CountTextBox.Size = new System.Drawing.Size(121, 26);
            this.CountTextBox.TabIndex = 16;
            this.CountTextBox.Text = "10";
            // 
            // QueryAllButton
            // 
            this.QueryAllButton.Location = new System.Drawing.Point(201, 12);
            this.QueryAllButton.Name = "QueryAllButton";
            this.QueryAllButton.Size = new System.Drawing.Size(121, 91);
            this.QueryAllButton.TabIndex = 17;
            this.QueryAllButton.Text = "QueryAll";
            this.QueryAllButton.UseVisualStyleBackColor = true;
            this.QueryAllButton.Click += new System.EventHandler(this.QueryAllButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(710, 482);
            this.Controls.Add(this.QueryAllButton);
            this.Controls.Add(this.CountTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.IndexTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.QueryPartButton);
            this.Controls.Add(this.DisplayGridView);
            this.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Form1";
            this.Text = "ExecuteReaderSample";
            ((System.ComponentModel.ISupportInitialize)(this.DisplayGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView DisplayGridView;
        private System.Windows.Forms.Button QueryPartButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn UserId;
        private System.Windows.Forms.DataGridViewTextBoxColumn UserName;
        private System.Windows.Forms.TextBox IndexTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox CountTextBox;
        private System.Windows.Forms.Button QueryAllButton;
    }
}

