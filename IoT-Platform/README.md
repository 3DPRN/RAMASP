**IoT-Platform**

![image](https://user-images.githubusercontent.com/8396924/200279488-d9918641-4bfb-4b9b-9b2e-c71ea4a07f5a.png)

This platform runs instances of:
Orion Context Broker
Generic Fiware IoT agent
Quantum Leap
Keyrock
Crate-DB
Grafana

*Setup and configuration (Windows):*

- Setup docker on your pc (https://www.docker.com/)
- Setup a Linux distro, like UBUNTU (https://www.microsoft.com/it-it/p/ubuntu/9nblggh4msv6?rtc=1)
- Copy the IoT-Platform folder in a local folder of your pc and execute a Powershell session as administrator in this folder
- Launch command "docker-compose up -d"
- You're done, IoT components will be running in the proper container in Docker on default ports
