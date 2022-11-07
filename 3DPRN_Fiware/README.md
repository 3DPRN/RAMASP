**3D Printer custom NGSI Agent**

This software is needed to let your printer communicate with the IoT platform powered by fiware.

Once you have compiled the software you will have to copy this to the pc or raspberry of the specific printer you want to update in IoT.



**Configure**__
In the software folder you'll find a .config file (3DPRN_Fiware.exe.config). This file is the configuration file and needs to be configured as follow:
__
IP_PRN : Ip address of the printer. Usually local host 127.0.0.1 if you are running this agent in local.__
Port_PRN :  Usually 13000. Used for 3DPRN-ONBOARD Gui instance, replaces Octoprint.__
OrionClientConfig_BaseUrl : Http address of the running Orion context broker. This address needs to be checked on the centralized server running the IoT platform.__
OrionClientConfig_Token : API token of your specific running Orion Context broker. To be founded on the docker instance on the server running the IoT platform.__
__
prn_ip: ip of the printer. Usually local host 127.0.0.1 if the NGSI Agent runs locally.
prn_port: used from 3DPRN-ONBOARD Gui which can replace Octoprint. Usually 13000.
fiware_hash: hash of the Orion Context Broker running in LAN. To be found in Docker on the centralized server.
fiware_address: http-based address of the Orion Context Broker.
octopi_hash: hash code of the Octoprint instance running on the printer. To be found on octoprint API settings.

##Example##
<_3DPRN_Fiware.Properties.Settings>
<setting name="prn_ip" serializeAs="String">
  <value>127.0.0.1</value>
</setting>
<setting name="prn_port" serializeAs="String">
      <value>13000</value>
</setting>
<setting name="fiware_hash" serializeAs="String">
  <value ABCDEFG123/>
</setting>
<setting name="fiware_address" serializeAs="String">
  <value>http://192.168.0.100:1026/</value>
</setting>
<setting name="octopi_hash" serializeAs="String">
  <value ABCDEFG123/>
</setting>
</_3DPRN_Fiware.Properties.Settings>



**Run**
To run the software just launch it or on Debian launch the command "sudo mono 3DPRN_Fiware.exe" (Mono required: https://www.mono-project.com/).
Put it in autolaunch to make it automatic.
