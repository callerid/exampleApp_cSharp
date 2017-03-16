namespace ExampleApplication_Csharp
{
    partial class frmLogFile
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dgvLogFile = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLogFile)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvLogFile
            // 
            this.dgvLogFile.AllowUserToAddRows = false;
            this.dgvLogFile.AllowUserToDeleteRows = false;
            this.dgvLogFile.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLogFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvLogFile.Location = new System.Drawing.Point(0, 0);
            this.dgvLogFile.Name = "dgvLogFile";
            this.dgvLogFile.ReadOnly = true;
            this.dgvLogFile.RowHeadersVisible = false;
            this.dgvLogFile.Size = new System.Drawing.Size(1046, 505);
            this.dgvLogFile.TabIndex = 2;
            // 
            // frmLogFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1046, 505);
            this.Controls.Add(this.dgvLogFile);
            this.Name = "frmLogFile";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Log File Database";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmLogFile_FormClosing);
            this.Load += new System.EventHandler(this.frmLogFile_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLogFile)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvLogFile;
    }
}