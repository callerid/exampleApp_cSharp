//  This source code is free of charge and distibution is royalty free.
//  It is designed to be used with any Ethernet enabled CallerID.com hardware
//  Microsoft .NET framework 3.5 or above is required. 

// This form adds the user input for a name that matches a number
// into the database.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ExampleApplication_Csharp
{
    public partial class frmAddContact : Form
    {

        // Globals
        public static string MyLine;

        public frmAddContact()
        {
            InitializeComponent();
        }

        private void btAdd_Click(object sender, EventArgs e)
        {
            // --------------- Insert contact into contacts database ----------------------
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

            // Set new name to number stored in database for certain phone number
            SQLiteCommand myCommandContacts = new SQLiteCommand("INSERT INTO contacts(Name,Phone) Values ('" + tbName.Text + "','" + tbPhone.Text + "')", myConnectionContacts);
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

            // Update calls table to have new number for phone number
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

            // --------------- Delete any duplicates in database ----------------------
            SQLiteConnection myConnectionContactsD = new SQLiteConnection();
            myConnectionContactsD.ConnectionString = @"Data Source=" + Application.StartupPath + "\\contactsDatabase.db3;";

            // Log into log database
            try
            {
                myConnectionContactsD.Open();

            }
            catch (Exception ex)
            {
                MessageBox.Show("SQL exception: " + ex.ToString());
            }

            // Remove all records in temp contacts table
            string commandString = "DELETE FROM contactsTEMP;";
            // Copy only unique records into new table
            commandString += "INSERT INTO contactsTemp SELECT * FROM contacts GROUP BY Phone;";
            // Delete all data from contacts table
            commandString += "DELETE FROM contacts;";
            // Copy all data back from temp contacts table to remove all dupilcates
            commandString += "INSERT INTO contacts SELECT * FROM contactsTemp;";
            SQLiteCommand myCommandContactsD = new SQLiteCommand(commandString, myConnectionContactsD);
            if (myConnectionContactsD.State == ConnectionState.Open)
            {
                myCommandContactsD.ExecuteNonQuery();
            }

            try
            {
                myConnectionContactsD.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("SQL Exception: " + ex.ToString());
            }

            // Set stored name for changing of display after name change
            frmMain.StoredName = tbName.Text;

            // Set using stored name variable to true to tell method to use it
            frmMain.UseStoredName = true;

            // ----------------- Hide form --------------------
            this.Hide();
        }

        private void tbPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow only numbers into textbox
           if (e.KeyChar >= '0' && e.KeyChar <= '9' || e.KeyChar == '-' || e.KeyChar==(char)0x08) //The  character represents a backspace
            {
                e.Handled = false; //Do not reject the input
            }
            else
            {
                e.Handled = true; //Reject the input
            }
       }

        public void insertValues(string myName, string myNumber)
        {
            // Populate textboxes with data from phone line number
            tbName.Text = myName;
            tbPhone.Text = myNumber;
        }

        private void frmAddContact_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Hide form instead of closing it
            this.Visible = false;
            frmMain.UseStoredName = false;
            e.Cancel = true;
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
