CallerID.com Example C# Application
----------------------------------------------------------

This source code is free of charge and distibution is royalty free.
It is designed to be used with any Ethernet enabled CallerID.com hardware
Microsoft .NET framework 3.5 or above is required.

---------------------------------------------------------

Many software designers envision a Caller ID system in which the customer record automatically pops 
up for employee without any user intervention. While this scheme might be appropriate in a single 
phone line, single computer environment, there is a far better method being used in a multiple line,
multiple workstation scenario. It involves allowing the user to select which call to pop up and when.

When users are allowed to control the pop up screens, a host of benefits can be derived from the
fact that any workstation can pop up any phone call, on any line, at any time.

  1. Calls placed on hold can be serviced from any workstation.
    
  2. Customer records for transferred calls can be seen at both the original
     computer and the destination computer.
    
  3. A manager or employee can pull up the customer record on a neighboring
     workstation in an attempt to assist the employee on the phone call.
    
  4. Multiple calls arriving simultaneously on multiple lines are easily handled
     by the employee.
	
  5. They simply notice which phone line they're on and select it on screen.

There is an Example application on the website (http://www.callerid.com/developer/example-application/)
that provides the software designer a graphical representation of what type of information the user might
see and how they would invoke a customer record pop up in a multi-line environment.

This application uses SQLite since it is built in to the .NET Framwork. Only a small .DLL file is require
in the project application folder.  SQLite contains all the needed functions for this simple application. 
The code dealing with SQLite is provided to show how one would implement into a database for popup screens
and logging.  Since your database type may be different, the methods presented may not be appropriate.  

