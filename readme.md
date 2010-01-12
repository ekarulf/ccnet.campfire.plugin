# What is this?

A custom [CruiseControl.NET](http://ccnet.thoughtworks.com/) plugin which will post build results to a [Campfire](http://www.campfirenow.com) chatroom.

# How do I use it?

* Clone this project.
* Compile it.
* Copy src\ccnet.campfire.plugin\bin\Debug\ccnet.campfire.plugin.dll to c:\Program Files\CruiseControl.NET\server (or wherever your ccnet is installed).
* Add an element to your <publishers> block in your ccnet project configuration. For example:

    <cruisecontrol>
	  <project name="whatever">
	      ...
		  ...
	    <publishers>
	      <campfire 
		    account-name="the bit before .campfirenow in your campfire URL" 
			auth-token="the campfire API token of the user that the plugin should post as" 
			room-id="number at end of campfire room URL" />
	      <xmllogger />
	    </publishers>
      </project>
    </cruisecontrol>