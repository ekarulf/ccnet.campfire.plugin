# What is this?

A custom [CruiseControl.NET](http://ccnet.thoughtworks.com/) plugin which will post build results to a [Campfire](http://www.campfirenow.com) chatroom.

# How do I use it?

* Clone this project.
* Compile it using Visual Studio. The NAnt script in the project root is not for compiling this project - I set it up as a test build so I could test the plugin running inside CruiseControl.NET.
* Copy src\ccnet.campfire.plugin\bin\Debug\ccnet.campfire.plugin.dll to c:\Program Files\CruiseControl.NET\server (or wherever your ccnet is installed).
* Add an element to the publishers block in your ccnet project configuration. For example:

	...
	...
    <publishers>
	  <campfire 
		account-name="the bit before .campfirenow in your campfire URL" 
		auth-token="the campfire API token of the user that the plugin should post as" 
		room-id="number at end of campfire room URL"
		is-https="true|false, depending on your Campfire account settings" />
	  <xmllogger />
	  ...
	  ...
    </publishers>
	...
	...
    
# Compatibility

This project was built against [CruiseControl.NET 1.4.4 SP1 (build number 1.4.4.83)](http://confluence.public.thoughtworks.org/display/CCNET/2009/06/08/CruiseControl.NET+1.4.4+SP1+Released). I built in in Visual Studio 2008 against .NET 3.5 SP1. I have not tested it against other versions, so your mileage may vary. It makes the correct Campfire API calls as of 13 January 2010.