# RAMASP
RAM ASP is a 3D printing project that aims to automate, consolidate and make more reliable 3D Printing.
The 3D printers composing the system are supposed to be organized in an orderly and regular physical scheme.
To obtain the maximum degree of automation, the system should be equipped with a robotic arm. This arm will execute manual operations such as load and unloads of 3D printing plates and control vision of 3D printing.

This repository contains some software to automate the 3D printing system and make it "powered by FIWARE".
The full-experience is obtained with 3DPRN-LAB 3D printers, but any 3D printer running an instance of Octoprint can be controlled.

**3DPRN_Fiware**

This is an NGSIAgent running on each printer.
What this specifically do is talk locally with the specific printer and export data towards an Orion Context Broker. The centralized OCB will have a set of context data coming from all the printers; this will provide a full detailed situation of the current 3D printing process status.
Read the specific documentation to have details on how to configure this instance.

**3DPRN_Fiware_Houston**

This is an NGSIAgent running on the pc where the centralized software is running.
What this specifically do is talk locally with the centralized software and export data towards an Orion Context Broker. The centralized OCB will have the context data coming from all the printers; this will provide a full detailed situation of the working queue and robot status.
Read the specific documentation to have details on how to configure this instance.

**3DPRN-WALL - Centralized software**

This software orchestrate the work of the printers
