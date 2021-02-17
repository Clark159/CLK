namespace SqlTransactionScopeSample
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
            this.Insert_NormalButton = new System.Windows.Forms.Button();
            this.DisplayGridView = new System.Windows.Forms.DataGridView();
            this.Insert_TransactionRollbackButton = new System.Windows.Forms.Button();
            this.Insert_TransactionCommitButton = new System.Windows.Forms.Button();
            this.UserId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UserName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.DisplayGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // Insert_NormalButton
            // 
            this.Insert_NormalButton.Location = new System.Drawing.Point(12, 12);
            this.Insert_NormalButton.Name = "Insert_NormalButton";
            this.Insert_NormalButton.Size = new System.Drawing.Size(217, 91);
            this.Insert_NormalButton.TabIndex = 9;
            this.Insert_NormalButton.Text = "Insert\r\n(Normal)";
            this.Insert_NormalButton.UseVisualStyleBackColor = true;
            this.Insert_NormalButton.Click += new System.EventHandler(this.Insert_NormalButton_Click);
            // 
            // DisplayGridView
            // 
            this.DisplayGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DisplayGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.UserId,
            this.UserName});
            this.DisplayGridView.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.DisplayGridView.Location = new System.Drawing.Point(0, 122);
            this.DisplayGridView.Name = "DisplayGridView";
            this.DisplayGridView.RowTemplate.Height = 24;
            this.DisplayGridView.Size = new System.Drawing.Size(691, 360);
            this.DisplayGridView.TabIndex = 10;
            // 
            // Insert_TransactionRollbackButton
            // 
            this.Insert_TransactionRollbackButton.Location = new System.Drawing.Point(458, 12);
            this.Insert_TransactionRollbackButton.Name = "Insert_TransactionRollbackButton";
            this.Insert_TransactionRollbackButton.Size = new System.Drawing.Size(217, 91);
            this.Insert_TransactionRollbackButton.TabIndex = 11;
            this.Insert_TransactionRollbackButton.Text = "Insert\r\n(Transaction.Rollback)";
            this.Insert_TransactionRollbackButton.UseVisualStyleBackColor = true;
            this.Insert_TransactionRollbackButton.Click += new System.EventHandler(this.Insert_TransactionRollbackButton_Click);
            // 
            // Insert_TransactionCommitButton
            // 
            this.Insert_TransactionCommitButton.Location = new System.Drawing.Point(235, 12);
            this.Insert_TransactionCommitButton.Name = "Insert_TransactionCommitButton";
            this.Insert_TransactionCommitButton.Size = new System.Drawing.Size(217, 91);
            this.Insert_TransactionCommitButton.TabIndex = 12;
            this.Insert_TransactionCommitButton.Text = "Insert\r\n(Transaction.Commit)";
            this.Insert_TransactionCommitButton.UseVisualStyleBackColor = true;
            this.Insert_TransactionCommitButton.Click += new System.EventHandler(this.Insert_TransactionCommitButton_Click);
            // 
            // UserId
            // 
            this.UserId.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.UserId.DataPropertyName = "Id";
            this.UserId.HeaderText = "Id";
            this.UserId.Name = "UserId";
            this.UserId.Width = 52;
            // 
            // UserName
            // 
            this.UserName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.UserName.DataPropertyName = "Name";
            this.UserName.HeaderText = "Name";
            this.UserName.Name = "UserName";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(691, 482);
            this.Controls.Add(this.Insert_TransactionCommitButton);
            this.Controls.Add(this.Insert_TransactionRollbackButton);
            this.Controls.Add(this.DisplayGridView);
            this.Controls.Add(this.Insert_NormalButton);
            this.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Form1";
            this.Text = "SqlTransactionScopeSample";
            ((System.ComponentModel.ISupportInitialize)(this.DisplayGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Insert_NormalButton;
        private System.Windows.Forms.DataGridView DisplayGridView;
        private System.Windows.Forms.Button Insert_TransactionRollbackButton;
        private System.Windows.Forms.Button Insert_TransactionCommitButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn UserId;
        private System.Windows.Forms.DataGridViewTextBoxColumn UserName;
    }
}

