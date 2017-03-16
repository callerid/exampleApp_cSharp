//  This source code is free of charge and distibution is royalty free.
//  It is designed to be used with any Ethernet enabled CallerID.com hardware
//  Microsoft .NET framework 3.5 or above is required. 

// This form changes the name of a current contact in the database with
// the text of a user's input.

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
    public partial class frmChangeContact : Form
    {
        public frmChangeContact()
        {
            InitializeComponent();
        }

        private void btChange_Click(object sender, EventArgs e)
        {
            // -------------------------- Update contacts database ---------------------------
            SQLiteConnection myConnectionContacts = new SQLiteConnection();
            myConnectionContacts.ConnectionString = @"Data Source=" + Application.StartupPath + "\\contactsDatabase.db3;";

            // Log into log database
            try
            {
                myConnectionContacts.Open();

            }
            catch (Exception ex)
            {
                MessageBox.Show("SQL exception: " + ex.ToString());
            }

            // Set new name to number stored in database
            SQLiteCommand myCommandContacts = new SQLiteCommand("UPDATE contacts SET Name='" + tbName.Text + "' WHERE Phone='" + tbPhone.Text + "';", myConnectionContacts);
            if (myConnectionContacts.State == ConnectionState.Open)
            {
                myCommandContacts.ExecuteNonQuery();
            }

            try
            {
                myConnectionContacts.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("SQL Exception: " + ex.ToString());
            }

            //------------------------------------ Update calls database -----------------------------
            // --------------- Change all records in callsdatabse to show new name for number ----------------------
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

            // Update calls table with new name for that number
            SQLiteCommand myCommand = new SQLiteCommand("UPDATE calls SET Name='" + tbName.Text + "' WHERE Number='" + tbPhone.Text + "';", myConnection);
            if (myConnection.State == ConnectionState.Open)
            {
                myCommand.ExecuteNonQuery();
            }

            try
            {
                myConnection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("SQL Exception: " + ex.ToString());
            }

            // Set stored name for changing of display after name change
            frmMain.StoredName = tbName.Text;

            // Set used stored name variable to true so method knows to use storedName value
            frmMain.UseStoredName = true;

            // Hide form
            this.Hide();
        }

        private void frmChangeContact_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Hide form instead of closing it
            this.Visible = false;
            frmMain.UseStoredName = false;
            e.Cancel = true;
        }

        public void insertValues(string myName, string myNumber)
        {
            // Insert data from call line number
            tbName.Text = myName;
            tbPhone.Text = myNumber;
        }

        private void tbName_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Do NOT allow quotes of any kind, since it would mess up SQL commands
            if (e.KeyChar == '\"' || e.KeyChar == '\'') 
            {
                e.Handled = true; //Reject the input of ' and "
            }
        }

    }
}
