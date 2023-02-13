# RAMASP ROSE-AP
RAM ASP is a 3D printing project that aims to automate, consolidate and make more reliable 3D Printing.
The 3D printers composing the system are supposed to be organized in an orderly and regular physical scheme.
To obtain the maximum degree of automation, the system should be equipped with a robotic arm. This arm will execute manual operations such as load and unloads of 3D printing plates and control vision of 3D printing.

## Contents
- [How it works](#how-it-works)
- [Install]
- [Usage]
- [Components](#3DPRN_Fiware)

## How it works

This repository contains some software to automate the 3D printing system and make it "powered by FIWARE".
The full-experience is obtained with 3DPRN-LAB 3D printers, but any 3D printer running an instance of Octoprint can be controlled.

The functional scheme of the production plant is:

![image](https://user-images.githubusercontent.com/8396924/201058215-62d6d6bb-f549-4d2f-92b2-e526f0b9f8d8.png)


## 3DPRN_Fiware

This is an NGSIAgent running on each printer.
What this specifically do is talk locally with the specific printer and export data towards an Orion Context Broker. The centralized OCB will have a set of context data coming from all the printers; this will provide a full detailed situation of the current 3D printing process status.
Read the specific documentation to have details on how to configure this instance.

## 3DPRN_Fiware_Houston

This is an NGSIAgent running on the pc where the centralized software is running.
What this specifically do is talk locally with the centralized software and export data towards an Orion Context Broker. The centralized OCB will have the context data coming from this software, providing details about workings flow, workings tails... .
Read the specific documentation to have details on how to configure this agent.

## 3DPRN-WALL - Centralized software

This software orchestrate the work of the printers.
If you have 1000 printers attached to the system, you'll only need to select the gcode to print, and this software will print it considering current workings, the states of the printers and other context process informations.
Read the specific documentation to have details on how to configure this software.

## IoT platform

This software package needs to be installed and run on a centralized server.
Main components of this IoT platform are:

- Orion Context Broker on default port 1026
- Grafana data visualisation on default port 3000
- IoT Agent on port 4041
- Crate DB on port 4200

You can install this platform on Windows OS through Docker. See specific documentation on how to run and configure the IoT platform.
