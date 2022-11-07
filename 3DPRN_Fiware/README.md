**3D Printer custom NGSI Agent**

This software is needed to let your printer communicate with the IoT platform powered by fiware.

Once you have compiled the software you will have to copy this to the pc or raspberry of the specific printer you want to update in IoT.



**Configure**
In the software folder you'll find a .INI file. This file is the configuration file and needs to be configured as follow (example):

[FIWARE_PRN]
IP_PRN=127.0.0.1
Port_PRN=13000
OrionClientConfig_BaseUrl=http://192.168.0.33:1026/
OrionClientConfig_Token=ABCDEFGHIJKLMNOPQRSTUVWXYZ

IP_PRN : Ip address of the printer. Usually local host 127.0.0.1 if you are running this agent in local.
Port_PRN :  Usually 13000. Used for 3DPRN-ONBOARD Gui instance, replaces Octoprint.
OrionClientConfig_BaseUrl : Http address of the running Orion context broker. This address needs to be checked on the centralized server running the IoT platform.
OrionClientConfig_Token : API token of your specific running Orion Context broker. To be founded on the docker instance on the server running the IoT platform.


**Run**
To run the software just launch it or on Debian launch the command "sudo mono 3DPRN_Fiware.exe" (Mono required: https://www.mono-project.com/)
