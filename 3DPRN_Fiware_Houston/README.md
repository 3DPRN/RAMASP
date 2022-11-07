# Centralized software custom NGSI Agent

This software is needed to let the centralized software communicate with the IoT platform powered by fiware.  
Once you have compiled the software you will have to copy this to the pc running the centralized software.  
  
## Configure  
In the software folder you'll find a .config file (3DPRN_Fiware.exe.config). This file is the configuration file and needs to be configured as follow:  
  
**prn_ip** : ip of the pc running the software. Usually local host 127.0.0.1 if the NGSI Agent runs locally.  
**prn_port** : Port for the communication with centralized software. Usually 13000.  
**fiware_hash** : Hash of the Orion Context Broker running in LAN. To be found in Docker on the centralized server.  
**fiware_address** : http-based address of the Orion Context Broker.  
  
*Example*  
  
```
<_3DPRN_Fiware.Properties.Settings>  
  <setting name="prn_ip" serializeAs="String">  
    <value>127.0.0.1</value>  
  </setting>  
  <setting name="prn_port" serializeAs="String">  
    <value>13000</value>  
  </setting>  
  <setting name="OCB_ip" serializeAs="String">  
    <value>http://127.0.0.1:1026/</value>  
  </setting>  
  <setting name="OCB_token" serializeAs="String">  
    <value ABCDE/>  
  </setting>  
</_3DPRN_Fiware.Properties.Settings>  
```
  
## Run  
To run the software just launch it through the "3DPRN_Fiware_Houston.exe" executable.  
Put it in autolaunch to make it automatic.  
