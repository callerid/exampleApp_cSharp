//  This source code is free of charge and distibution is royalty free.
//  It is designed to be used with any Ethernet enabled CallerID.com hardware
//  Microsoft .NET framework 3.5 or above is required. 


using System;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Drawing;

namespace ExampleApplication_Csharp
{
    public partial class frmMain : Form
    {
        // Receiving Thread setup
        public static UdpReceiverClass UdpReceiver = new UdpReceiverClass();
        Thread UdpReceiveThread = new Thread(UdpReceiver.UdpIdleReceive);
        
        // Public variables
        public static string StoredName;
        public static Boolean UseStoredName;
        public static Boolean SearchContactFound;
        public static string SearchContact;
        public static string MyLine;
        public static string MyType;
        public static string MyDate;
        public static string MyTime;
        public static string MyCheckSum;
        public static string MyRings;
        public static string MyDuration;
        public static string MyIndicator;
        public static string MyNumber;
        public static string MyName;
        public static int FoundIndex;

        // Start record variables
        public static string SMyLine;
        public static string SMyTime;
        public static string SMyDate;
        public static string SMyNumber;

        // Create all forms that will be used
        frmLogFile frmLog = new frmLogFile();
        frmContacts frmContact = new frmContacts();
        frmAddContact frmAdd = new frmAddContact();
        frmChangeContact frmChange = new frmChangeContact();
        
        public frmMain()
        {
            InitializeComponent();

            // Start listener for UDP traffic
            Subscribe(UdpReceiver);
            
        }

        // Wait for a certain amount of time
        private void WaitFor(int milliSeconds)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            while(sw.ElapsedMilliseconds<milliSeconds)
            {
                Application.DoEvents();
            }
            sw.Stop();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {

            // Check whether two or more instances of the application are running
            // and close out of opening app if already running
			if(AlreadyRunning()==true)
			{
			    const string messageString = "Application already running...";
                MessageBox.Show(messageString);
                Application.Exit();
            }

            // Check if ELPopup is running and suspend if so.  
			// (ELPopup is a CallerID.com free software app that users sometimes inadvertantly load)
            Boolean elpopupFound = false;
            // Get all processes with name of 'elpopup'
            Process[] procList = Process.GetProcessesByName("elpopup");
            if(procList.Length>0)
            {
                string strProcName = procList[0].ProcessName;
                int iProcID = procList[0].Id;

                if(File.Exists(procList[0].MainModule.FileName))
                {
                    elpopupFound = true;
                    ProcessStartInfo processProperties = new ProcessStartInfo();
                    processProperties.FileName = procList[0].MainModule.FileName;
                    processProperties.Arguments = "-pause";
                    Process myProcess = Process.Start(processProperties);
                }

            }
            
            // IF ELPopup is found give program 2 seconds to attempt to pause ELPopup then show message
            if(elpopupFound==true)
            {
                WaitFor(2000);
                const string messageToShow =
                    "ELPopup found and will be suspended if possible. It is important that these two programs do not" +
                    " run at the same time. Failure to do this can cause this program to crash. Make sure ELPopup is closed.";
                MessageBox.Show(messageToShow);
            }
             
            // Start Receiver thread
            UdpReceiveThread.IsBackground = true;
            UdpReceiveThread.Start();

            // Check for database and create it if not found
            Directory.SetCurrentDirectory(Application.StartupPath + "\\");

            //--------------------------- Log File database---------------------
            if(File.Exists("callsDatabase.db3")==false)
            {
                // Create new SQLite (db3) file for new database since none exist
				// You could use any database type for logging.  We used SQLite since one only one DLL file
				// is required for installation of this database type. 
								
                SQLiteConnection.CreateFile("callsDatabase.db3");

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

                // Create all needed columns in new table called 'calls'
                SQLiteCommand myCommand = new SQLiteCommand("CREATE TABLE calls(Date varchar(10),Time varchar(10),Line varchar(10),Type varchar(10),Indicator varchar(10),Duration varchar(10),Checksum varchar(10),Rings varchar(10),Number varchar(15),Name varchar(20));", myConnection);
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

            }

            // --------------------------- Database for contacts ------------------------------------
			// In order to show sample code for databse lookups, we created an extremely simple contact database.  // It only contains name and phone number.  Your existing contact manager database would be acessed to 
			// find matching records based on phone numbers.
            
			if (File.Exists("contactsDatabase.db3") == false)
            {
                // Create new SQLite (db3) file for new database since none exist
                SQLiteConnection.CreateFile("contactsDatabase.db3");

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

                // Create all needed columns in new table called 'contacts'
                SQLiteCommand myCommand = new SQLiteCommand("CREATE TABLE contacts(Name varchar(20),Phone varchar(15));CREATE TABLE contactsTemp(Name varchar(20),Phone varchar(15));", myConnection);
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

            }

        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            // End Receiver thread
            if(UdpReceiveThread.IsAlive)
            {
                UdpReceiveThread.Abort();
            }
        }
        
        public void Subscribe(UdpReceiverClass u)
        {
            // If UDP event occurs run HeardIt method
            u.DataReceived += new UdpReceiverClass.UdpEventHandler(HeardIt);
        }

        public void updateRecord()
        {
            // Create database connection
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

            // Alter data already in database to new values
            SQLiteCommand myCommand = new SQLiteCommand("UPDATE calls SET Duration='" + MyDuration + "', Rings='" + MyRings + "', Indicator='" + MyIndicator + "', Name='" + MyName + 
                "' WHERE Line='" + SMyLine + "' AND Time='" + SMyTime + "' AND Date='" + SMyDate + "' AND Number='" +SMyNumber + "';", myConnection);
            if (myConnection.State == ConnectionState.Open)
            {
                myCommand.ExecuteNonQuery();
            }

            // Close connection to database
            try
            {
                myConnection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("SQL Exception: " + ex.ToString());
            }

            // Update logfile form
            frmLog.refreshDGV();

        }

        public void logCall()
        {
            // Create connection to database
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

            // Insert new data into database
            SQLiteCommand myCommand = new SQLiteCommand("INSERT INTO calls(Line,Type,Indicator,Duration,Checksum,Rings,Date,Time,Number,Name) Values ('" + MyLine + "','" + MyType + "','" + MyIndicator + "','" + MyDuration + "','" + MyCheckSum + "','" + MyRings + "','" + MyDate + "','" + MyTime + "','" + MyNumber + "','" + MyName + "')", myConnection);
            if(myConnection.State==ConnectionState.Open)
            {
                myCommand.ExecuteNonQuery();    
            }
            
            // Close connection to database
            try
            {
                myConnection.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show("SQL Exception: " + ex.ToString());
            }

            // Update logfile form
            frmLog.refreshDGV();

        }

        public void setVars()
           {

            // Clear all variables
            SearchContactFound = false;
            SearchContact = "";
            MyLine = "";
            MyType = "";
            MyDate = "";
            MyTime = "";
            MyCheckSum = "";
            MyRings = "";
            MyDuration = "";
            MyIndicator = "";
            MyNumber = "";
            MyName = "";
            FoundIndex = -1;

            // Get UDP received message from event
            string receivedMessage = UdpReceiverClass.ReceivedMessage;

            // Remove header
            string rawData = receivedMessage.Substring(21, receivedMessage.Length - 21);
            int index = rawData.IndexOf(" ");

			//---- EXTRACTING INDIVIDUAL FIELDS FROM CALL RECORDS -------                   
            
			// Deluxe units are capable of sending both Start and End Records on Incoming and Outgoing calls
			// Deluxe units can also send detail records such as Ring, On-Hook and Off Hook.
			// This section extracts data from fields and places it into varibles
            // The code allows for all types of call records that can be sent.  			
			// 
			// Note: Basic unit units only send Start Records on Incoming Calls.
			
			
            // Get Line number from string
            MyLine = rawData.Substring(0, index);

            // Update Raw data
            rawData = rawData.Substring(index, rawData.Length - index);
            index = rawData.IndexOf(" ");

            while (rawData.IndexOf(" ") == 0)
            {
                rawData = rawData.Substring(1, rawData.Length - 1);
            }

            // Get type from string.  
			// For Deluxe units, the Data type can be either [I]nbound, [O]utbound, [R]ing, o[N]-hook, of[F]-hook
			// For Basic units, the only Data type will be "I".
			
            index = rawData.IndexOf(" ");
            MyType = rawData.Substring(0, index);

            // Update Raw data
            rawData = rawData.Substring(index, rawData.Length - index);
            index = rawData.IndexOf(" ");

            while (rawData.IndexOf(" ") == 0)
            {
                rawData = rawData.Substring(1, rawData.Length - 1);
            }

			// Check whether the record is a Incoming/Outgoing.  If not, it is a detail record
			// which needs to be processed diffrently below.
		    if (MyType == "I" || MyType == "O")
            {

                // Get start or end indicator
                index = rawData.IndexOf(" ");
                MyIndicator = rawData.Substring(0, index);

                // Update Raw data
                rawData = rawData.Substring(index, rawData.Length - index);
                index = rawData.IndexOf(" ");

                while (rawData.IndexOf(" ") == 0)
                {
                    rawData = rawData.Substring(1, rawData.Length - 1);
                }

                // Get duration from string
                index = rawData.IndexOf(" ");
                MyDuration = rawData.Substring(0, index);

                // Update Raw data
                rawData = rawData.Substring(index, rawData.Length - index);
                index = rawData.IndexOf(" ");

                while (rawData.IndexOf(" ") == 0)
                {
                    rawData = rawData.Substring(1, rawData.Length - 1);
                }

                // Get checksum from string
                index = rawData.IndexOf(" ");
                MyCheckSum = rawData.Substring(0, index);

                // Update Raw data
                rawData = rawData.Substring(index, rawData.Length - index);
                index = rawData.IndexOf(" ");

                while (rawData.IndexOf(" ") == 0)
                {
                    rawData = rawData.Substring(1, rawData.Length - 1);
                }

                // Get rings from string
                index = rawData.IndexOf(" ");
                MyRings = rawData.Substring(0, index);

                // Update Raw data
                rawData = rawData.Substring(index, rawData.Length - index);
                index = rawData.IndexOf(" ");

                while (rawData.IndexOf(" ") == 0)
                {
                    rawData = rawData.Substring(1, rawData.Length - 1);
                }

                // Get date from string
                index = rawData.IndexOf(" ");
                MyDate = rawData.Substring(0, index);

                // Update Raw data
                rawData = rawData.Substring(index, rawData.Length - index);
                index = rawData.IndexOf(" ");

                while (rawData.IndexOf(" ") == 0)
                {
                    rawData = rawData.Substring(1, rawData.Length - 1);
                }

                // Get time from string
                index = rawData.IndexOf(" ");
                MyTime = rawData.Substring(0, index);

                // Update Raw data
                rawData = rawData.Substring(index, rawData.Length - index);
                index = rawData.IndexOf(" ");

                while (rawData.IndexOf(" ") == 0)
                {
                    rawData = rawData.Substring(1, rawData.Length - 1);
                }

                // Get am/pm from string
                index = rawData.IndexOf(" ");
                MyTime += rawData.Substring(0, index);

                // Update Raw data
                rawData = rawData.Substring(index, rawData.Length - index);
                index = rawData.IndexOf(" ");

                while (rawData.IndexOf(" ") == 0)
                {
                    rawData = rawData.Substring(1, rawData.Length - 1);
                }

                // Get number from string
                index = rawData.IndexOf(" ");
                MyNumber = rawData.Substring(0, index);

                // Update Raw data
                rawData = rawData.Substring(index, rawData.Length - index);
                index = rawData.IndexOf(" ");

                while (rawData.IndexOf(" ") == 0)
                {
                    rawData = rawData.Substring(1, rawData.Length - 1);
                }

                // Get name from string
                MyName = rawData;
                
            }
                //----------------EXTRACT DATA FROM ON-HOOK, OFF-HOOK, AND RING DATA TYPES-----------
            else if (MyType == "N" || MyType == "F" || MyType == "R") 
            {
                // Get date from string
                index = rawData.IndexOf(" ");
                MyDate = rawData.Substring(0, index);

                // Update Raw data
                rawData = rawData.Substring(index, rawData.Length - index);
                index = rawData.IndexOf(" ");

                while (rawData.IndexOf(" ") == 0)
                {
                    rawData = rawData.Substring(1, rawData.Length - 1);
                }

                // Get time from string (in case we need the time from these real-time events)
                if ((rawData.IndexOf(" ")) == -1)
                {
                    MyTime = rawData;
                }
                else
                {
                    MyTime = rawData.Substring(0, index);
                }

            }
        }

        private void HeardIt(UdpReceiverClass u, EventArgs e)
        {
            // Extract all variables from incoming data string
            this.Invoke((MethodInvoker) (() => setVars()));
            
            // ----------THIS SECTION HANDLES ALLL THE CALLER ID WINDOW VISUALS--------------
			// The code below could easily be condensed into one method handling different line numbers.
			// We used 4 occurances of the same method for 4 lines hoping that clarity could be provided.
            switch (MyLine)
            {
                case "01":

                    switch (MyType + MyIndicator)
                    {
                        case "R":

                            //-----------------------INCOMING CALL--------------------
                            //--------------------------------------------------------
                            // Change picture of phone to ringing
                            picPhoneLine1.Image = ExampleApplication_Csharp.Properties.Resources.phoneRing;

                            // Light-up panel green for incoming call
                            panLine1.BackColor = Color.LightGreen;

                            // Show time on panel
                            lbLine1Time.Invoke((MethodInvoker)(() => lbLine1Time.Text = MyTime));

                            // Show name and number of panel
                            lbLine1Number.Invoke((MethodInvoker)(() => lbLine1Number.Text = MyNumber));
                            lbLine1Name.Invoke((MethodInvoker)(() => lbLine1Name.Text = MyName));

                            break;
                        case "IS":

                            // ----------------------Ring answered------------------
                            //------------------------------------------------------
                            // Light-up panel green for incoming call
                            panLine1.BackColor = Color.LightGreen;

                            // Show time on panel
                            lbLine1Time.Invoke((MethodInvoker)(() => lbLine1Time.Text = MyTime));

                            // Show name and number of panel
                            lbLine1Number.Invoke((MethodInvoker)(() => lbLine1Number.Text = MyNumber));
                            lbLine1Name.Invoke((MethodInvoker)(() => lbLine1Name.Text = MyName));

                            break;
                        case "F":

                            //-----------------------Phone pickup-------------------
                            //------------------------------------------------------
                            picPhoneLine1.Image = ExampleApplication_Csharp.Properties.Resources.phoneOffHook;

                            break;
                        case "N":

                            //-----------------------Phone hangup--------------------
                            //-------------------------------------------------------

                            // Change panel color
                            panLine1.BackColor = Color.Silver;

                            // Change picture of phone to not-ringing
                            picPhoneLine1.Image = ExampleApplication_Csharp.Properties.Resources.phoneOnHook;

                            break;
                        case "IE":
                            break;
                        case "OS":

                            //-----------------------Outgoing CALL--------------------
                            //--------------------------------------------------------
                            // Change picture of phone to ringing
                            picPhoneLine1.Image = ExampleApplication_Csharp.Properties.Resources.phoneOffHook;

                            // Light-up panel blue for incoming call
                            panLine1.BackColor = Color.LightBlue;

                            // Show time on panel
                            lbLine1Time.Invoke((MethodInvoker)(() => lbLine1Time.Text = MyTime));

                            // Show name and number of panel
                            lbLine1Number.Invoke((MethodInvoker)(() => lbLine1Number.Text = MyNumber));
                            lbLine1Name.Invoke((MethodInvoker)(() => lbLine1Name.Text = MyName));

                            break;
                        case "OE":
                            break;
                        default:
                            break;
                    }

                    break;

                case "02":

                    switch (MyType + MyIndicator)
                    {
                        case "R":

                            //-----------------------INCOMING CALL--------------------
                            //--------------------------------------------------------
                            // Change picture of phone to ringing
                            picPhoneLine2.Image = ExampleApplication_Csharp.Properties.Resources.phoneRing;

                            // Light-up panel green for incoming call
                            panLine2.BackColor = Color.LightGreen;

                            // Show time on panel
                            lbLine2Time.Invoke((MethodInvoker)(() => lbLine2Time.Text = MyTime));

                            // Show name and number of panel
                            lbLine2Number.Invoke((MethodInvoker)(() => lbLine2Number.Text = MyNumber));
                            lbLine2Name.Invoke((MethodInvoker)(() => lbLine2Name.Text = MyName));

                            break;
                        case "IS":

                            // ----------------------Ring answered------------------
                            //------------------------------------------------------
                           // Light-up panel green for incoming call
                            panLine2.BackColor = Color.LightGreen;

                            // Show time on panel
                            lbLine2Time.Invoke((MethodInvoker)(() => lbLine2Time.Text = MyTime));

                            // Show name and number of panel
                            lbLine2Number.Invoke((MethodInvoker)(() => lbLine2Number.Text = MyNumber));
                            lbLine2Name.Invoke((MethodInvoker)(() => lbLine2Name.Text = MyName));

                            break;
                        case "F":

                            //-----------------------Phone pickup-------------------
                            //------------------------------------------------------
                            picPhoneLine2.Image = ExampleApplication_Csharp.Properties.Resources.phoneOffHook;

                            break;
                        case "N":

                            //-----------------------Phone hangup--------------------
                            //-------------------------------------------------------

                            // Change panel color
                            panLine2.BackColor = Color.Silver;

                            // Change picture of phone to not-ringing
                            picPhoneLine2.Image = ExampleApplication_Csharp.Properties.Resources.phoneOnHook;

                            break;
                        case "IE":
                            break;
                        case "OS":

                            //-----------------------Outgoing CALL--------------------
                            //--------------------------------------------------------
                            // Change picture of phone to ringing
                            picPhoneLine2.Image = ExampleApplication_Csharp.Properties.Resources.phoneOffHook;

                            // Light-up panel blue for incoming call
                            panLine2.BackColor = Color.LightBlue;

                            // Show time on panel
                            lbLine2Time.Invoke((MethodInvoker)(() => lbLine2Time.Text = MyTime));

                            // Show name and number of panel
                            lbLine2Number.Invoke((MethodInvoker)(() => lbLine2Number.Text = MyNumber));
                            lbLine2Name.Invoke((MethodInvoker)(() => lbLine2Name.Text = MyName));

                            break;
                        case "OE":
                            break;
                        default:
                            break;
                    }

                    break;

                case "03":

                    switch (MyType + MyIndicator)
                    {
                        case "R":

                            //-----------------------INCOMING CALL--------------------
                            //--------------------------------------------------------
                            // Change picture of phone to ringing
                            picPhoneLine3.Image = ExampleApplication_Csharp.Properties.Resources.phoneRing;

                            // Light-up panel green for incoming call
                            panLine3.BackColor = Color.LightGreen;

                            // Show time on panel
                            lbLine3Time.Invoke((MethodInvoker)(() => lbLine3Time.Text = MyTime));

                            // Show name and number of panel
                            lbLine3Number.Invoke((MethodInvoker)(() => lbLine3Number.Text = MyNumber));
                            lbLine3Name.Invoke((MethodInvoker)(() => lbLine3Name.Text = MyName));

                            break;
                        case "IS":

                            // ----------------------Ring answered------------------
                            //------------------------------------------------------
                            // Light-up panel green for incoming call
                            panLine3.BackColor = Color.LightGreen;

                            // Show time on panel
                            lbLine3Time.Invoke((MethodInvoker)(() => lbLine3Time.Text = MyTime));

                            // Show name and number of panel
                            lbLine3Number.Invoke((MethodInvoker)(() => lbLine3Number.Text = MyNumber));
                            lbLine3Name.Invoke((MethodInvoker)(() => lbLine3Name.Text = MyName));

                            break;
                        case "F":

                            //-----------------------Phone pickup-------------------
                            //------------------------------------------------------
                            picPhoneLine3.Image = ExampleApplication_Csharp.Properties.Resources.phoneOffHook;

                            break;
                        case "N":

                            //-----------------------Phone hangup--------------------
                            //-------------------------------------------------------

                            // Change panel color
                            panLine3.BackColor = Color.Silver;

                            // Change picture of phone to not-ringing
                            picPhoneLine3.Image = ExampleApplication_Csharp.Properties.Resources.phoneOnHook;

                            break;
                        case "IE":
                            break;
                        case "OS":

                            //-----------------------Outgoing CALL--------------------
                            //--------------------------------------------------------
                            // Change picture of phone to ringing
                            picPhoneLine3.Image = ExampleApplication_Csharp.Properties.Resources.phoneOffHook;

                            // Light-up panel blue for incoming call
                            panLine3.BackColor = Color.LightBlue;

                            // Show time on panel
                            lbLine3Time.Invoke((MethodInvoker)(() => lbLine3Time.Text = MyTime));

                            // Show name and number of panel
                            lbLine3Number.Invoke((MethodInvoker)(() => lbLine3Number.Text = MyNumber));
                            lbLine3Name.Invoke((MethodInvoker)(() => lbLine3Name.Text = MyName));

                            break;
                        case "OE":
                            break;
                        default:
                            break;
                    }

                    break;

                case "04":

                    switch (MyType + MyIndicator)
                    {
                        case "R":

                            //-----------------------INCOMING CALL--------------------
                            //--------------------------------------------------------
                            // Change picture of phone to ringing
                            picPhoneLine4.Image = ExampleApplication_Csharp.Properties.Resources.phoneRing;

                            // Light-up panel green for incoming call
                            panLine4.BackColor = Color.LightGreen;

                            // Show time on panel
                            lbLine4Time.Invoke((MethodInvoker)(() => lbLine4Time.Text = MyTime));

                            // Show name and number of panel
                            lbLine4Number.Invoke((MethodInvoker)(() => lbLine4Number.Text = MyNumber));
                            lbLine4Name.Invoke((MethodInvoker)(() => lbLine4Name.Text = MyName));

                            break;
                        case "IS":

                            // ----------------------Ring answered------------------
                            //------------------------------------------------------
                            // Light-up panel green for incoming call
                            panLine4.BackColor = Color.LightGreen;

                            // Show time on panel
                            lbLine4Time.Invoke((MethodInvoker)(() => lbLine4Time.Text = MyTime));

                            // Show name and number of panel
                            lbLine4Number.Invoke((MethodInvoker)(() => lbLine4Number.Text = MyNumber));
                            lbLine4Name.Invoke((MethodInvoker)(() => lbLine4Name.Text = MyName));

                            break;
                        case "F":

                            //-----------------------Phone pickup-------------------
                            //------------------------------------------------------
                            picPhoneLine4.Image = ExampleApplication_Csharp.Properties.Resources.phoneOffHook;

                            break;
                        case "N":

                            //-----------------------Phone hangup--------------------
                            //-------------------------------------------------------

                            // Change panel color
                            panLine4.BackColor = Color.Silver;

                            // Change picture of phone to not-ringing
                            picPhoneLine4.Image = ExampleApplication_Csharp.Properties.Resources.phoneOnHook;

                            break;
                        case "IE":
                            break;
                        case "OS":

                            //-----------------------Outgoing CALL--------------------
                            //--------------------------------------------------------
                            // Change picture of phone to ringing
                            picPhoneLine4.Image = ExampleApplication_Csharp.Properties.Resources.phoneOffHook;

                            // Light-up panel blue for incoming call
                            panLine4.BackColor = Color.LightBlue;

                            // Show time on panel
                            lbLine4Time.Invoke((MethodInvoker) (() => lbLine4Time.Text = MyTime));

                            // Show name and number of panel
                            lbLine4Number.Invoke((MethodInvoker)(() => lbLine4Number.Text = MyNumber));
                            lbLine4Name.Invoke((MethodInvoker)(() => lbLine4Name.Text = MyName));

                            break;
                        case "OE":
                            break;
                        default:
                            break;
                    }

                    break;

                default:
                    break;
            }
			// ---------------------------------------------
			//         START & END RECORD PROCESSING 
			// ---------------------------------------------
			// Start Records will be processed below for 3 reasons:
			//   1. To perform a customer lookup.
			//	 2. Change visuals on the Caller ID main screen.
			//   3. Add phone call records to the log file properly.
			//		- Data will be stored for Start Records such that  
			//		  corresponding End records will replace them in log file. 
			
			// Combine MyType and MyIndicator to create a 'command' variable that allows us to 
			// determine if the call is an Incoming or Outgoing Start record.
			string command = MyType + MyIndicator;
			
			// If 'command' is Start record
            if(command=="IS"||command=="OS")
            {
                // Set values to be checked against database during end record
                SMyLine = MyLine;
                SMyTime = MyTime;
                SMyDate = MyDate;
                SMyNumber = MyNumber;

                // Look for phone number in database
                this.Invoke((MethodInvoker)(() => searchContacts()));

                // If found in database
                if (SearchContactFound == true)
                {
                    // Number found: change icon for contacts to found
                    switch (MyLine)
                    {
                        case "01":
							// Change image to show that contact was found
							picDatabaseLine1.Image = Properties.Resources.databaseFound;
							
							// Change tag to 'change' so when clicked we update contact
							// instead of creating new one
                            picDatabaseLine1.Tag = "change";
							
							// Change name on display for current line
                            lbLine1Name.Invoke((MethodInvoker)(() => lbLine1Name.Text = SearchContact));
                            break;

                        case "02":
                            // Change image to show that contact was found
							picDatabaseLine2.Image = Properties.Resources.databaseFound;
                            
							// Change tag to 'change' so when clicked we update contact
							// instead of creating new one
							picDatabaseLine2.Tag = "change";
							
							// Change name on display for current line
                            lbLine2Name.Invoke((MethodInvoker)(() => lbLine2Name.Text = SearchContact));
                            break;

                        case "03":
							// Change image to show that contact was found
                            picDatabaseLine3.Image = Properties.Resources.databaseFound;
                            
							// Change tag to 'change' so when clicked we update contact
							// instead of creating new one
							picDatabaseLine3.Tag = "change";
                            
							// Change name on display for current line
							lbLine3Name.Invoke((MethodInvoker)(() => lbLine3Name.Text = SearchContact));
                            break;

                        case "04":
                            // Change image to show that contact was found
							picDatabaseLine4.Image = Properties.Resources.databaseFound;
                            
							// Change tag to 'change' so when clicked we update contact
							// instead of creating new one
							picDatabaseLine4.Tag = "change";
							
							// Change name on display for current line
                            lbLine4Name.Invoke((MethodInvoker)(() => lbLine4Name.Text = SearchContact));
                            break;

                    }

                    // Change Name to name found in database
                    MyName = SearchContact;
                }
                else
                {
                    // Not found: change icon for contacts to insert
                    switch (MyLine)
                    {
                        case "01":
							// Change image to show contact not found
                            picDatabaseLine1.Image = Properties.Resources.databaseInsert;
							
							// Change tag to 'insert' so program knows to add new contact
							// if button is clicked.
                            picDatabaseLine1.Tag = "insert";
							break;

                        case "02":
							// Change image to show contact not found
                            picDatabaseLine2.Image = Properties.Resources.databaseInsert;
                            
							// Change tag to 'insert' so program knows to add new contact
							// if button is clicked.
							picDatabaseLine2.Tag = "insert";
                            break;

                        case "03":
                            // Change image to show contact not found
							picDatabaseLine3.Image = Properties.Resources.databaseInsert;
                            
							// Change tag to 'insert' so program knows to add new contact
							// if button is clicked.
							picDatabaseLine3.Tag = "insert";
                            break;

                        case "04":
							// Change image to show contact not found
                            picDatabaseLine4.Image = Properties.Resources.databaseInsert;
                            
							// Change tag to 'insert' so program knows to add new contact
							// if button is clicked.
							picDatabaseLine4.Tag = "insert";
                            break;

                    }
                }

                // Log start record into database
                this.Invoke((MethodInvoker)(() => logCall()));
            }
                
				// ----------END RECORD PROCESSING----------
								
				// if incoming or outgoing end record
            else if (command=="IE"||command=="OE")
            {

                // Look for phone number in database
                this.Invoke((MethodInvoker)(() => searchContacts()));

                // If found in database
                if (SearchContactFound == true)
                {
                    // Take database name and use it for updating
                    MyName = SearchContact;
                }

                // On deluxe unit the End Record replaces the
                // Start record to provide database with more
                // accurate information (duration,rings,name,etc.)
                
                // Updates record to new duration count for deluxe units
                this.Invoke((MethodInvoker)(() => updateRecord()));

            }

      }
			// Finds contact name from database that corresponds to Caller ID number
			// and store into global varible. 
			
        private void searchContacts()
        {
            // Create connection to database
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

            // Search if phone number already in database
            SQLiteCommand myCommand = new SQLiteCommand("SELECT Name FROM contacts WHERE Phone='" + MyNumber + "';", myConnection);
            if (myConnection.State == ConnectionState.Open)
            {
                SQLiteDataReader reader = myCommand.ExecuteReader();
                
                // If phone found return the name matching the number
                if(reader.HasRows)
                {
                    SearchContactFound = true;
                    while(reader.Read())
                    {
                        SearchContact = reader.GetString(0);
                    }

                }
                else
                {
                    SearchContactFound = false;
                }

                // Close data reader
                reader.Close();
                
            }

            try
            {
                myConnection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("SQL Exception: " + ex.ToString());
            }
            
        }

        private void btLog_Click(object sender, EventArgs e)
        {
            if (frmLog.Visible == false)
            {
                // Show call log database file
                frmLog.Show();
                frmLog.refreshDGV();
                frmLog.Focus();

            }
        }

        private void btContacts_Click(object sender, EventArgs e)
        {

            if (frmContact.Visible == false)
            {
                // Show call log database file
                frmContact.Show();
                frmContact.refreshDGV();
                frmContact.Focus();

            }

        }

        private void picDatabaseLine1_Click(object sender, EventArgs e)
        {

            // Set using stored name for false
            UseStoredName = false;

            // Check if adding or changing
            if (picDatabaseLine1.Tag.ToString() == "insert")
            {
                if (frmAdd.Visible == false)
                {
                    // Load add form
                    frmAdd.Show();
                    frmAdd.insertValues(lbLine1Name.Text, lbLine1Number.Text);
                    frmAdd.Focus();
                    while (frmAdd.Visible == true)
                    {
                        WaitFor(1000);
                    }
                    if(UseStoredName==true)
                    {
                        // Update icons
                        changeIcons("01");
                        frmLog.refreshDGV();
                        frmContact.refreshDGV(); 
                    }
                    
                }
            }
            else if (picDatabaseLine1.Tag.ToString() == "change")
            {
                if (frmChange.Visible == false)
                {
                    // Load change form
                    frmChange.Show();
                    frmChange.insertValues(lbLine1Name.Text, lbLine1Number.Text);
                    frmChange.Focus();
                    while (frmChange.Visible == true)
                    {
                        WaitFor(1000);
                    }
                    if (UseStoredName == true)
                    {
                        // Update icons
                        changeIcons("01");
                        frmLog.refreshDGV();
                        frmContact.refreshDGV();
                    };
                }
            }
        }

        private void picDatabaseLine2_Click(object sender, EventArgs e)
        {

            // Set using stored name for false
            UseStoredName = false;

            // Check if adding or changing
            if(picDatabaseLine2.Tag.ToString()=="insert")
            {
                if(frmAdd.Visible==false)
                {
                    // Load add form
                    frmAdd.Show();
                    frmAdd.insertValues(lbLine2Name.Text, lbLine2Number.Text);
                    frmAdd.Focus();
                    while(frmAdd.Visible==true)
                    {
                        WaitFor(1000);
                    }
                    if (UseStoredName == true)
                    {
                        // Update icons
                        changeIcons("02");
                        frmLog.refreshDGV();
                        frmContact.refreshDGV();
                    }
                }
            }else if(picDatabaseLine2.Tag.ToString()=="change")
            {
                if(frmChange.Visible==false)
                {
                    // Load change form
                    frmChange.Show();
                    frmChange.insertValues(lbLine2Name.Text, lbLine2Number.Text);
                    frmChange.Focus();
                    while (frmChange.Visible == true)
                    {
                        WaitFor(1000);
                    }
                    if (UseStoredName == true)
                    {
                        // Update icons
                        changeIcons("02");
                        frmLog.refreshDGV();
                        frmContact.refreshDGV();
                    }
                }
            }

        }

        private void picDatabaseLine3_Click(object sender, EventArgs e)
        {

            // Set using stored name for false
            UseStoredName = false;

            // Check if adding or changing
            if (picDatabaseLine3.Tag.ToString() == "insert")
            {
                if (frmAdd.Visible == false)
                {
                    // Load add form
                    frmAdd.Show();
                    frmAdd.insertValues(lbLine3Name.Text, lbLine3Number.Text);
                    frmAdd.Focus();
                    while (frmAdd.Visible == true)
                    {
                        WaitFor(1000);
                    }
                    if (UseStoredName == true)
                    {
                        // Update icons
                        changeIcons("03");
                        frmLog.refreshDGV();
                        frmContact.refreshDGV();
                    }
                }
            }
            else if (picDatabaseLine3.Tag.ToString() == "change")
            {
                if (frmChange.Visible == false)
                {
                    // Load change form
                    frmChange.Show();
                    frmChange.insertValues(lbLine3Name.Text, lbLine3Number.Text);
                    frmChange.Focus();
                    while (frmChange.Visible == true)
                    {
                        WaitFor(1000);
                    }
                    if (UseStoredName == true)
                    {
                        // Update icons
                        changeIcons("03");
                        frmLog.refreshDGV();
                        frmContact.refreshDGV();
                    }
                }
            }
        }

        private void picDatabaseLine4_Click(object sender, EventArgs e)
        {

            // Set using stored name for false
            UseStoredName = false;

            // Check if adding or changing
            if (picDatabaseLine4.Tag.ToString() == "insert")
            {
                if (frmAdd.Visible == false)
                {
                    // Load add form
                    frmAdd.Show();
                    frmAdd.insertValues(lbLine4Name.Text, lbLine4Number.Text);
                    frmAdd.Focus();
                    while (frmAdd.Visible == true)
                    {
                        WaitFor(1000);
                    }
                    if (UseStoredName == true)
                    {
                        // Update icons
                        changeIcons("04");
                        frmLog.refreshDGV();
                        frmContact.refreshDGV();
                    }
                }
            }
            else if (picDatabaseLine4.Tag.ToString() == "change")
            {
                if (frmChange.Visible == false)
                {
                    // Load change form
                    frmChange.Show();
                    frmChange.insertValues(lbLine4Name.Text, lbLine4Number.Text);
                    frmChange.Focus();
                    while (frmChange.Visible == true)
                    {
                        WaitFor(1000);
                    }
                    if (UseStoredName == true)
                    {
                        // Update icons
                        changeIcons("04");
                        frmLog.refreshDGV();
                        frmContact.refreshDGV();
                    }
                }
            }
        }
		// ------UPDATE ICON IF CALLER ID NAME HAS BEEN CHANGED BY USER---------
        public void changeIcons(string myLine)
        {
            // After adding into database, change icon to found icon
            switch(myLine)
            {
                case "01":
                    picDatabaseLine1.Image = Properties.Resources.databaseFound;
                    picDatabaseLine1.Tag = "change";
                    lbLine1Name.Text = StoredName;
                    break;
                case "02":
                    picDatabaseLine2.Image = Properties.Resources.databaseFound;
                    picDatabaseLine2.Tag = "change";
                    lbLine2Name.Text = StoredName;
                    break;
                case "03":
                    picDatabaseLine3.Image = Properties.Resources.databaseFound;
                    picDatabaseLine3.Tag = "change";
                    lbLine3Name.Text = StoredName;
                    break;
                case "04":
                    picDatabaseLine4.Image = Properties.Resources.databaseFound;
                    picDatabaseLine4.Tag = "change";
                    lbLine4Name.Text = StoredName;
                    break;
            }
        }
			// To prevent more than one instance of the program from running
        private Boolean AlreadyRunning()
        {
            // Get current process
            Process myProc = Process.GetCurrentProcess();
            // Get current process name
            string myProcessName = myProc.ProcessName;
            // Get all process of current process name
            Process[] procs = Process.GetProcessesByName(myProcessName);

            // If only one process exist return true
            if(procs.Length==1)
            {
                return false;
            }

            // Loop through all processes to find if process started first
            for (int i = 0; i < procs.Length;i++ )
            {
                // If process started first, return true
                if(procs[i].StartTime<myProc.StartTime)
                {
                    return true;
                }
            }

            // If code gets to here then return false
            return false;
            
        }

        // Resume ELpopup if it was suspended 
        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Process[] procList = Process.GetProcessesByName("elpopup");
            if (procList.Length > 0)
            {
                string strProcName = procList[0].ProcessName;
                int iProcID = procList[0].Id;

                if (File.Exists(procList[0].MainModule.FileName))
                {
                    ProcessStartInfo processProperties = new ProcessStartInfo();
                    processProperties.FileName = procList[0].MainModule.FileName;
                    processProperties.Arguments = "-resume";
                    Process myProcess = Process.Start(processProperties);
                }

            }
        }

    }
            
}

// -----------------------UDP Receiver Class----------------------------------------
// This is absolutely vital in order to receive UPD packets into an application 
// If you are not familar with these functions, you may want to consider simply pasting into your code.

public class UdpReceiverClass
{
    // Declare variables
    public static Boolean Done;
    public static string ReceivedMessage;
    public static byte[] ReceviedMessageByte;
    public static int NListenPort = 3520;

    // Define event
    public delegate void UdpEventHandler(UdpReceiverClass u,EventArgs e);
    public event UdpEventHandler DataReceived;

    // Idle listening
    public void UdpIdleReceive()
    {

        // Set done to false so loop will begin
        Done = false;

        // Setup filter for too small messages
        const int filterMessageSmallerThan = 4;

        // Setup socket for listening
        UdpClient listener = new UdpClient(NListenPort);
        IPEndPoint groupEP = new IPEndPoint(IPAddress.Any, NListenPort);

        // Wait for broadcast
        try
        {
            while (!Done)
            {
                // Receive broadcast
                ReceviedMessageByte = listener.Receive(ref groupEP);
                ReceivedMessage = Encoding.UTF7.GetString(ReceviedMessageByte, 0, ReceviedMessageByte.Length);

                // Filter smaller messages););
                if (ReceviedMessageByte.Length > filterMessageSmallerThan)
                {
                    DataReceived(this, EventArgs.Empty);
                }

            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }

        listener.Close();
    }

}