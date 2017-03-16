//  This source code is free of charge and distibution is royalty free.
//  It is designed to be used with any Ethernet enabled CallerID.com hardware
//  Microsoft .NET framework 3.5 or above is required. 

//  This form displays all the contacts in a datagrid view for easy viewing.

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
    public partial class frmContacts : Form
    {
        public frmContacts()
        {
            InitializeComponent();
        }

        private void frmContacts_Load(object sender, EventArgs e)
        {
            // Populate data grid view with data from database
            refreshDGV();
        }

        public void refreshDGV()
        {
            // Connect to database
            SQLiteConnection myConnection = new SQLiteConnection();
            myConnection.ConnectionString = @"Data Source=" + Application.StartupPath + "\\contactsDatabase.db3;";

            // Log into log database
            try
            {
                myConnection.Open();

            }
            catch (Exception ex)
            {
                MessageBox.Show("SQL exception: " + ex.ToString());
            }

            SQLiteCommand myCommand = new SQLiteCommand("SELECT * FROM contacts", myConnection);
            if (myConnection.State == ConnectionState.Open)
            {
                // Bind sql data to data grid view to display all data in table format
                SQLiteDataAdapter datAdapter = new SQLiteDataAdapter(myCommand);
                DataSet datSet = new DataSet();
                datAdapter.Fill(datSet, "contacts");
                BindingSource myBind = new BindingSource(datSet, "contacts");
                dgvContacts.DataSource = myBind;
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
            if (this.Visible == true)
            {
                dgvContacts.Sort(dgvContacts.Columns[0], ListSortDirection.Ascending);
            }
        }

        private void frmContacts_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Hide form instead of closing it
            this.Visible = false;
            e.Cancel = true;
        }
    }
}
