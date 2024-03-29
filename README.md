# RAMASP ROSE-AP

This project is part of [DIH^2](https://www.dih-squared.eu/). For more information check the RAMP Catalogue entry for the components.

## Contents
- [What is RAM ASP](#RAM-ASP)
- [How it works](#how-it-works)
- [Setup and usage](#setup-and-usage)
- [Components](#3DPRN_Fiware)

## RAM ASP

RAM ASP is a 3D printing project that aims to automate, consolidate and make more reliable 3D Printing.
The 3D printers composing the system are supposed to be organized in an orderly and regular physical scheme.
To obtain the maximum degree of automation, the system should be equipped with a robotic arm. This arm will execute manual operations such as load and unloads of 3D printing plates and control vision of 3D printing.

## How it works

This repository contains some software to automate the 3D printing system and make it "powered by FIWARE".
The full-experience is obtained with 3DPRN-LAB 3D printers, but any 3D printer running an instance of Octoprint can be controlled.

The functional scheme of the production plant is:

![image](https://user-images.githubusercontent.com/8396924/201058215-62d6d6bb-f549-4d2f-92b2-e526f0b9f8d8.png)

## Setup and usage
 
1. Install IoT platform ([IOT platform](IoT-Platform))
2. Install and configure NGSI IoT Agents on your printers ([IOT Agents](3DPRN_Fiware))
3. Install and configure 3DPRN-WALL centralized software on a computer ([3DPRN-WALL](https://github.com/3DPRN/RAMASP/releases/tag/v1.0.0), [Parameters reference](https://github.com/3DPRN/RAMASP/blob/master/RAM%20ASP%20-%20Parameters%20reference.pdf))
4. Install and configure the centralized software's agent ([IOT Agent](3DPRN_Fiware_Houston))

Check the setup video from Youtube: https://youtu.be/W2af16RZACQ

## 3DPRN_Fiware

This is an NGSIAgent running on each printer.
What this specifically do is talk locally with the specific printer and export data towards an Orion Context Broker. The centralized OCB will have a set of context data coming from all the printers; this will provide a full detailed situation of the current 3D printing process status.
Read the [specific documentation](3DPRN_Fiware) to have details on how to configure this instance.

## 3DPRN_Fiware_Houston

This is an NGSIAgent running on the pc where the centralized software is running.
What this specifically do is talk locally with the centralized software and export data towards an Orion Context Broker. The centralized OCB will have the context data coming from this software, providing details about workings flow, workings tails... .
Read the [specific documentation](3DPRN_Fiware_Houston) to have details on how to configure this agent.

## 3DPRN-WALL - Centralized software

This software orchestrate the work of the printers.
If you have 1000 printers attached to the system, you'll only need to select the gcode to print, and this software will print it considering current workings, the states of the printers and other context process informations.
Read the [specific documentation](https://github.com/3DPRN/RAMASP/blob/master/RAM%20ASP%20-%20Parameters%20reference.pdf) to have details on how to configure this software.

## IoT platform

This software package needs to be installed and run on a centralized server.
Main components of this IoT platform are:

- Orion Context Broker on default port 1026
- Grafana data visualisation on default port 3000
- IoT Agent on port 4041
- Crate DB on port 4200

You can install this platform on Windows OS through Docker. See [specific documentation](IoT-Platform) on how to run and configure the IoT platform.
