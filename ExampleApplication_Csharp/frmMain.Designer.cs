namespace ExampleApplication_Csharp
{
    partial class frmMain
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
            this.components = new System.ComponentModel.Container();
            this.panLine1 = new System.Windows.Forms.Panel();
            this.picDatabaseLine1 = new System.Windows.Forms.PictureBox();
            this.lbLine1Name = new System.Windows.Forms.Label();
            this.lbLine1Number = new System.Windows.Forms.Label();
            this.lbLine1Time = new System.Windows.Forms.Label();
            this.lbLine1 = new System.Windows.Forms.Label();
            this.picPhoneLine1 = new System.Windows.Forms.PictureBox();
            this.panLine2 = new System.Windows.Forms.Panel();
            this.picDatabaseLine2 = new System.Windows.Forms.PictureBox();
            this.lbLine2Name = new System.Windows.Forms.Label();
            this.lbLine2Number = new System.Windows.Forms.Label();
            this.lbLine2Time = new System.Windows.Forms.Label();
            this.lbLine2 = new System.Windows.Forms.Label();
            this.picPhoneLine2 = new System.Windows.Forms.PictureBox();
            this.panLine3 = new System.Windows.Forms.Panel();
            this.picDatabaseLine3 = new System.Windows.Forms.PictureBox();
            this.lbLine3Name = new System.Windows.Forms.Label();
            this.lbLine3Number = new System.Windows.Forms.Label();
            this.lbLine3Time = new System.Windows.Forms.Label();
            this.lbLine3 = new System.Windows.Forms.Label();
            this.picPhoneLine3 = new System.Windows.Forms.PictureBox();
            this.panLine4 = new System.Windows.Forms.Panel();
            this.picDatabaseLine4 = new System.Windows.Forms.PictureBox();
            this.lbLine4Name = new System.Windows.Forms.Label();
            this.lbLine4Number = new System.Windows.Forms.Label();
            this.lbLine4Time = new System.Windows.Forms.Label();
            this.lbLine4 = new System.Windows.Forms.Label();
            this.picPhoneLine4 = new System.Windows.Forms.PictureBox();
            this.btLog = new System.Windows.Forms.Button();
            this.btContacts = new System.Windows.Forms.Button();
            this.timerDuplicateHandling = new System.Windows.Forms.Timer(this.components);
            this.panLine1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picDatabaseLine1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPhoneLine1)).BeginInit();
            this.panLine2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picDatabaseLine2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPhoneLine2)).BeginInit();
            this.panLine3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picDatabaseLine3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPhoneLine3)).BeginInit();
            this.panLine4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picDatabaseLine4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPhoneLine4)).BeginInit();
            this.SuspendLayout();
            // 
            // panLine1
            // 
            this.panLine1.BackColor = System.Drawing.Color.Silver;
            this.panLine1.Controls.Add(this.picDatabaseLine1);
            this.panLine1.Controls.Add(this.lbLine1Name);
            this.panLine1.Controls.Add(this.lbLine1Number);
            this.panLine1.Controls.Add(this.lbLine1Time);
            this.panLine1.Controls.Add(this.lbLine1);
            this.panLine1.Controls.Add(this.picPhoneLine1);
            this.panLine1.Location = new System.Drawing.Point(12, 12);
            this.panLine1.Name = "panLine1";
            this.panLine1.Size = new System.Drawing.Size(591, 43);
            this.panLine1.TabIndex = 0;
            // 
            // picDatabaseLine1
            // 
            this.picDatabaseLine1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picDatabaseLine1.Image = global::ExampleApplication_Csharp.Properties.Resources.databaseIdle;
            this.picDatabaseLine1.Location = new System.Drawing.Point(549, 3);
            this.picDatabaseLine1.Name = "picDatabaseLine1";
            this.picDatabaseLine1.Size = new System.Drawing.Size(39, 40);
            this.picDatabaseLine1.TabIndex = 5;
            this.picDatabaseLine1.TabStop = false;
            this.picDatabaseLine1.Tag = "idle";
            this.picDatabaseLine1.Click += new System.EventHandler(this.picDatabaseLine1_Click);
            // 
            // lbLine1Name
            // 
            this.lbLine1Name.AutoSize = true;
            this.lbLine1Name.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbLine1Name.Location = new System.Drawing.Point(321, 13);
            this.lbLine1Name.Name = "lbLine1Name";
            this.lbLine1Name.Size = new System.Drawing.Size(51, 20);
            this.lbLine1Name.TabIndex = 4;
            this.lbLine1Name.Text = "Name";
            // 
            // lbLine1Number
            // 
            this.lbLine1Number.AutoSize = true;
            this.lbLine1Number.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbLine1Number.Location = new System.Drawing.Point(182, 13);
            this.lbLine1Number.Name = "lbLine1Number";
            this.lbLine1Number.Size = new System.Drawing.Size(65, 20);
            this.lbLine1Number.TabIndex = 3;
            this.lbLine1Number.Text = "Number";
            // 
            // lbLine1Time
            // 
            this.lbLine1Time.AutoSize = true;
            this.lbLine1Time.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbLine1Time.Location = new System.Drawing.Point(88, 13);
            this.lbLine1Time.Name = "lbLine1Time";
            this.lbLine1Time.Size = new System.Drawing.Size(43, 20);
            this.lbLine1Time.TabIndex = 2;
            this.lbLine1Time.Text = "Time";
            // 
            // lbLine1
            // 
            this.lbLine1.AutoSize = true;
            this.lbLine1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbLine1.Location = new System.Drawing.Point(49, 13);
            this.lbLine1.Name = "lbLine1";
            this.lbLine1.Size = new System.Drawing.Size(27, 20);
            this.lbLine1.TabIndex = 1;
            this.lbLine1.Text = "01";
            // 
            // picPhoneLine1
            // 
            this.picPhoneLine1.Image = global::ExampleApplication_Csharp.Properties.Resources.phoneOnHook;
            this.picPhoneLine1.Location = new System.Drawing.Point(4, 3);
            this.picPhoneLine1.Name = "picPhoneLine1";
            this.picPhoneLine1.Size = new System.Drawing.Size(39, 40);
            this.picPhoneLine1.TabIndex = 0;
            this.picPhoneLine1.TabStop = false;
            // 
            // panLine2
            // 
            this.panLine2.BackColor = System.Drawing.Color.Silver;
            this.panLine2.Controls.Add(this.picDatabaseLine2);
            this.panLine2.Controls.Add(this.lbLine2Name);
            this.panLine2.Controls.Add(this.lbLine2Number);
            this.panLine2.Controls.Add(this.lbLine2Time);
            this.panLine2.Controls.Add(this.lbLine2);
            this.panLine2.Controls.Add(this.picPhoneLine2);
            this.panLine2.Location = new System.Drawing.Point(12, 61);
            this.panLine2.Name = "panLine2";
            this.panLine2.Size = new System.Drawing.Size(591, 43);
            this.panLine2.TabIndex = 1;
            // 
            // picDatabaseLine2
            // 
            this.picDatabaseLine2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picDatabaseLine2.Image = global::ExampleApplication_Csharp.Properties.Resources.databaseIdle;
            this.picDatabaseLine2.Location = new System.Drawing.Point(549, 3);
            this.picDatabaseLine2.Name = "picDatabaseLine2";
            this.picDatabaseLine2.Size = new System.Drawing.Size(39, 40);
            this.picDatabaseLine2.TabIndex = 6;
            this.picDatabaseLine2.TabStop = false;
            this.picDatabaseLine2.Tag = "idle";
            this.picDatabaseLine2.Click += new System.EventHandler(this.picDatabaseLine2_Click);
            // 
            // lbLine2Name
            // 
            this.lbLine2Name.AutoSize = true;
            this.lbLine2Name.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbLine2Name.Location = new System.Drawing.Point(321, 14);
            this.lbLine2Name.Name = "lbLine2Name";
            this.lbLine2Name.Size = new System.Drawing.Size(51, 20);
            this.lbLine2Name.TabIndex = 5;
            this.lbLine2Name.Text = "Name";
            // 
            // lbLine2Number
            // 
            this.lbLine2Number.AutoSize = true;
            this.lbLine2Number.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbLine2Number.Location = new System.Drawing.Point(182, 14);
            this.lbLine2Number.Name = "lbLine2Number";
            this.lbLine2Number.Size = new System.Drawing.Size(65, 20);
            this.lbLine2Number.TabIndex = 4;
            this.lbLine2Number.Text = "Number";
            // 
            // lbLine2Time
            // 
            this.lbLine2Time.AutoSize = true;
            this.lbLine2Time.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbLine2Time.Location = new System.Drawing.Point(88, 14);
            this.lbLine2Time.Name = "lbLine2Time";
            this.lbLine2Time.Size = new System.Drawing.Size(43, 20);
            this.lbLine2Time.TabIndex = 3;
            this.lbLine2Time.Text = "Time";
            // 
            // lbLine2
            // 
            this.lbLine2.AutoSize = true;
            this.lbLine2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbLine2.Location = new System.Drawing.Point(49, 14);
            this.lbLine2.Name = "lbLine2";
            this.lbLine2.Size = new System.Drawing.Size(27, 20);
            this.lbLine2.TabIndex = 2;
            this.lbLine2.Text = "02";
            // 
            // picPhoneLine2
            // 
            this.picPhoneLine2.Image = global::ExampleApplication_Csharp.Properties.Resources.phoneOnHook;
            this.picPhoneLine2.Location = new System.Drawing.Point(4, 3);
            this.picPhoneLine2.Name = "picPhoneLine2";
            this.picPhoneLine2.Size = new System.Drawing.Size(39, 40);
            this.picPhoneLine2.TabIndex = 1;
            this.picPhoneLine2.TabStop = false;
            // 
            // panLine3
            // 
            this.panLine3.BackColor = System.Drawing.Color.Silver;
            this.panLine3.Controls.Add(this.picDatabaseLine3);
            this.panLine3.Controls.Add(this.lbLine3Name);
            this.panLine3.Controls.Add(this.lbLine3Number);
            this.panLine3.Controls.Add(this.lbLine3Time);
            this.panLine3.Controls.Add(this.lbLine3);
            this.panLine3.Controls.Add(this.picPhoneLine3);
            this.panLine3.Location = new System.Drawing.Point(12, 110);
            this.panLine3.Name = "panLine3";
            this.panLine3.Size = new System.Drawing.Size(591, 43);
            this.panLine3.TabIndex = 2;
            // 
            // picDatabaseLine3
            // 
            this.picDatabaseLine3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picDatabaseLine3.Image = global::ExampleApplication_Csharp.Properties.Resources.databaseIdle;
            this.picDatabaseLine3.Location = new System.Drawing.Point(549, 3);
            this.picDatabaseLine3.Name = "picDatabaseLine3";
            this.picDatabaseLine3.Size = new System.Drawing.Size(39, 40);
            this.picDatabaseLine3.TabIndex = 7;
            this.picDatabaseLine3.TabStop = false;
            this.picDatabaseLine3.Tag = "idle";
            this.picDatabaseLine3.Click += new System.EventHandler(this.picDatabaseLine3_Click);
            // 
            // lbLine3Name
            // 
            this.lbLine3Name.AutoSize = true;
            this.lbLine3Name.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbLine3Name.Location = new System.Drawing.Point(321, 12);
            this.lbLine3Name.Name = "lbLine3Name";
            this.lbLine3Name.Size = new System.Drawing.Size(51, 20);
            this.lbLine3Name.TabIndex = 6;
            this.lbLine3Name.Text = "Name";
            // 
            // lbLine3Number
            // 
            this.lbLine3Number.AutoSize = true;
            this.lbLine3Number.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbLine3Number.Location = new System.Drawing.Point(182, 12);
            this.lbLine3Number.Name = "lbLine3Number";
            this.lbLine3Number.Size = new System.Drawing.Size(65, 20);
            this.lbLine3Number.TabIndex = 5;
            this.lbLine3Number.Text = "Number";
            // 
            // lbLine3Time
            // 
            this.lbLine3Time.AutoSize = true;
            this.lbLine3Time.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbLine3Time.Location = new System.Drawing.Point(88, 12);
            this.lbLine3Time.Name = "lbLine3Time";
            this.lbLine3Time.Size = new System.Drawing.Size(43, 20);
            this.lbLine3Time.TabIndex = 4;
            this.lbLine3Time.Text = "Time";
            // 
            // lbLine3
            // 
            this.lbLine3.AutoSize = true;
            this.lbLine3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbLine3.Location = new System.Drawing.Point(49, 12);
            this.lbLine3.Name = "lbLine3";
            this.lbLine3.Size = new System.Drawing.Size(27, 20);
            this.lbLine3.TabIndex = 3;
            this.lbLine3.Text = "03";
            // 
            // picPhoneLine3
            // 
            this.picPhoneLine3.Image = global::ExampleApplication_Csharp.Properties.Resources.phoneOnHook;
            this.picPhoneLine3.Location = new System.Drawing.Point(4, 3);
            this.picPhoneLine3.Name = "picPhoneLine3";
            this.picPhoneLine3.Size = new System.Drawing.Size(39, 40);
            this.picPhoneLine3.TabIndex = 2;
            this.picPhoneLine3.TabStop = false;
            // 
            // panLine4
            // 
            this.panLine4.BackColor = System.Drawing.Color.Silver;
            this.panLine4.Controls.Add(this.picDatabaseLine4);
            this.panLine4.Controls.Add(this.lbLine4Name);
            this.panLine4.Controls.Add(this.lbLine4Number);
            this.panLine4.Controls.Add(this.lbLine4Time);
            this.panLine4.Controls.Add(this.lbLine4);
            this.panLine4.Controls.Add(this.picPhoneLine4);
            this.panLine4.Location = new System.Drawing.Point(12, 159);
            this.panLine4.Name = "panLine4";
            this.panLine4.Size = new System.Drawing.Size(591, 43);
            this.panLine4.TabIndex = 3;
            // 
            // picDatabaseLine4
            // 
            this.picDatabaseLine4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picDatabaseLine4.Image = global::ExampleApplication_Csharp.Properties.Resources.databaseIdle;
            this.picDatabaseLine4.Location = new System.Drawing.Point(549, 3);
            this.picDatabaseLine4.Name = "picDatabaseLine4";
            this.picDatabaseLine4.Size = new System.Drawing.Size(39, 40);
            this.picDatabaseLine4.TabIndex = 8;
            this.picDatabaseLine4.TabStop = false;
            this.picDatabaseLine4.Tag = "idle";
            this.picDatabaseLine4.Click += new System.EventHandler(this.picDatabaseLine4_Click);
            // 
            // lbLine4Name
            // 
            this.lbLine4Name.AutoSize = true;
            this.lbLine4Name.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbLine4Name.Location = new System.Drawing.Point(321, 13);
            this.lbLine4Name.Name = "lbLine4Name";
            this.lbLine4Name.Size = new System.Drawing.Size(51, 20);
            this.lbLine4Name.TabIndex = 7;
            this.lbLine4Name.Text = "Name";
            // 
            // lbLine4Number
            // 
            this.lbLine4Number.AutoSize = true;
            this.lbLine4Number.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbLine4Number.Location = new System.Drawing.Point(182, 13);
            this.lbLine4Number.Name = "lbLine4Number";
            this.lbLine4Number.Size = new System.Drawing.Size(65, 20);
            this.lbLine4Number.TabIndex = 6;
            this.lbLine4Number.Text = "Number";
            // 
            // lbLine4Time
            // 
            this.lbLine4Time.AutoSize = true;
            this.lbLine4Time.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbLine4Time.Location = new System.Drawing.Point(88, 13);
            this.lbLine4Time.Name = "lbLine4Time";
            this.lbLine4Time.Size = new System.Drawing.Size(43, 20);
            this.lbLine4Time.TabIndex = 5;
            this.lbLine4Time.Text = "Time";
            // 
            // lbLine4
            // 
            this.lbLine4.AutoSize = true;
            this.lbLine4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbLine4.Location = new System.Drawing.Point(49, 13);
            this.lbLine4.Name = "lbLine4";
            this.lbLine4.Size = new System.Drawing.Size(27, 20);
            this.lbLine4.TabIndex = 4;
            this.lbLine4.Text = "04";
            // 
            // picPhoneLine4
            // 
            this.picPhoneLine4.Image = global::ExampleApplication_Csharp.Properties.Resources.phoneOnHook;
            this.picPhoneLine4.Location = new System.Drawing.Point(4, 3);
            this.picPhoneLine4.Name = "picPhoneLine4";
            this.picPhoneLine4.Size = new System.Drawing.Size(39, 40);
            this.picPhoneLine4.TabIndex = 3;
            this.picPhoneLine4.TabStop = false;
            // 
            // btLog
            // 
            this.btLog.Location = new System.Drawing.Point(438, 208);
            this.btLog.Name = "btLog";
            this.btLog.Size = new System.Drawing.Size(78, 27);
            this.btLog.TabIndex = 4;
            this.btLog.Text = "Open Log";
            this.btLog.UseVisualStyleBackColor = true;
            this.btLog.Click += new System.EventHandler(this.btLog_Click);
            // 
            // btContacts
            // 
            this.btContacts.Location = new System.Drawing.Point(522, 208);
            this.btContacts.Name = "btContacts";
            this.btContacts.Size = new System.Drawing.Size(78, 27);
            this.btContacts.TabIndex = 5;
            this.btContacts.Text = "Contacts";
            this.btContacts.UseVisualStyleBackColor = true;
            this.btContacts.Click += new System.EventHandler(this.btContacts_Click);
            // 
            // timerDuplicateHandling
            // 
            this.timerDuplicateHandling.Enabled = true;
            this.timerDuplicateHandling.Interval = 1000;
            this.timerDuplicateHandling.Tick += new System.EventHandler(this.timerDuplicateHandling_Tick);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(615, 239);
            this.Controls.Add(this.btContacts);
            this.Controls.Add(this.btLog);
            this.Controls.Add(this.panLine4);
            this.Controls.Add(this.panLine3);
            this.Controls.Add(this.panLine2);
            this.Controls.Add(this.panLine1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmMain";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Example Application C#";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmMain_FormClosed);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.panLine1.ResumeLayout(false);
            this.panLine1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picDatabaseLine1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPhoneLine1)).EndInit();
            this.panLine2.ResumeLayout(false);
            this.panLine2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picDatabaseLine2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPhoneLine2)).EndInit();
            this.panLine3.ResumeLayout(false);
            this.panLine3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picDatabaseLine3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPhoneLine3)).EndInit();
            this.panLine4.ResumeLayout(false);
            this.panLine4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picDatabaseLine4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPhoneLine4)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panLine1;
        private System.Windows.Forms.Panel panLine2;
        private System.Windows.Forms.Panel panLine3;
        private System.Windows.Forms.Panel panLine4;
        private System.Windows.Forms.PictureBox picPhoneLine1;
        private System.Windows.Forms.PictureBox picPhoneLine2;
        private System.Windows.Forms.PictureBox picPhoneLine3;
        private System.Windows.Forms.PictureBox picPhoneLine4;
        private System.Windows.Forms.Label lbLine1;
        private System.Windows.Forms.Label lbLine2;
        private System.Windows.Forms.Label lbLine3;
        private System.Windows.Forms.Label lbLine4;
        private System.Windows.Forms.Label lbLine1Time;
        private System.Windows.Forms.Label lbLine2Time;
        private System.Windows.Forms.Label lbLine3Time;
        private System.Windows.Forms.Label lbLine4Time;
        private System.Windows.Forms.Label lbLine1Number;
        private System.Windows.Forms.Label lbLine2Number;
        private System.Windows.Forms.Label lbLine3Number;
        private System.Windows.Forms.Label lbLine4Number;
        private System.Windows.Forms.Label lbLine1Name;
        private System.Windows.Forms.Label lbLine2Name;
        private System.Windows.Forms.Label lbLine3Name;
        private System.Windows.Forms.Label lbLine4Name;
        private System.Windows.Forms.PictureBox picDatabaseLine1;
        private System.Windows.Forms.PictureBox picDatabaseLine2;
        private System.Windows.Forms.PictureBox picDatabaseLine3;
        private System.Windows.Forms.PictureBox picDatabaseLine4;
        private System.Windows.Forms.Button btLog;
        private System.Windows.Forms.Button btContacts;
        private System.Windows.Forms.Timer timerDuplicateHandling;
    }
}

