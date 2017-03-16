//  This source code is free of charge and distibution is royalty free.
//  It is designed to be used with any Ethernet enabled CallerID.com hardware
//  Microsoft .NET framework 3.5 or above is required. 

// This form displays all the logged calls in a data grid view for easy viewing.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ExampleApplication_Csharp
{
    public partial class frmLogFile : Form
    {
        public frmLogFile()
        {
            InitializeComponent();
        }

        private void frmLogFile_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Hide form instead of closing it
            this.Visible = false;
            e.Cancel = true;
        }

        private void frmLogFile_Load(object sender, EventArgs e)
        {
            // Populate data grid view with database records
            refreshDGV();
        }

        public void refreshDGV()
        {
            
            // Connect to database
            SQLiteConnection myConnection = new SQLiteConnection();
            myConnection.ConnectionString = @"Data Source=" + Application.StartupPath + "\\callsDatabase.db3;";

            // Log into log database
            try
            {
                myConnection.Open();

            }
            catch (Exception ex)
            {
                MessageBox.Show("SQL exception: " + ex.ToString());
            }

            SQLiteCommand myCommand = new SQLiteCommand("SELECT * FROM calls", myConnection);
            if (myConnection.State == ConnectionState.Open)
            {
                // Bind sql database to data grid view to display all fields in table format
                SQLiteDataAdapter datAdapter = new SQLiteDataAdapter(myCommand);
                DataSet datSet = new DataSet();
                datAdapter.Fill(datSet, "calls");
                BindingSource myBind = new BindingSource(datSet, "calls");
                dgvLogFile.DataSource = myBind;
            }

            // Close connection
            try
            {
                myConnection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("SQL Exception: " + ex.ToString());
            }

            // Sort by time (most recent at top)
            if(this.Visible==true)
            {
                dgvLogFile.Sort(dgvLogFile.Columns[1], ListSortDirection.Descending);
            }
            
        }
    }
}
