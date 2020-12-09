ARothacker Web.Applications Learning

# TrafficPi - Infrastructure & Software Setup

## Prerequisites

### Hardware

- Desktop PC / Laptop with Windows 10
- USB microSD Card Reader
- Raspberry Pi 3 or 4
- microSD Card \>=8GB
- Power supply cable
  - Micro-USB (Pi 3)
  - USB-C (Pi 4)
  - ~ 5V 3A
- Breadboard
- Jumper Wires
- 2x LEDs of each red, yellow and green
- 6x 1kΩ resistors

Additionally to the Pi of your choice you might consider something like this:  
https://smile.amazon.de/gp/product/B01MQSWUGY

### Software

- [Raspberry Pi Imager](https://www.raspberrypi.org/software/)
- [Visual Studio Code](https://code.visualstudio.com/Download)
- [PuTTY](https://www.putty.org/)

## Raspberry Pi Setup

### Install the Operating System

1. Insert your SD Card to your SD Card Reader and connect it to your PC
2. Install and open Raspberry Pi Imager
3. Select "Rasperry Pi OS (other) / Raspberry Pi OS Lite" as OS
4. Select the SD card drive as destination
5. Select "Write" to write the image to the SD Card - wait...
6. Save an empty file named "ssh" into the boot partition's root folder of the SD Card
7. Connect your assets to the Raspberry Pi
   - SD Card
   - LAN cable
   - [optionally] HDMI cable (to watch the status as well as the PI's IP address)
   - Power supply

The Raspberry Pi is now starting...

### Setup Raspberry Pi OS

1. Install and open PuTTY on your PC
2. Get the IP of your Pi from your monitor or DHCP server device
3. Connect with PuTTY to your Pi using IP address and "SSH"
4. Login with user "pi" and password "raspberry"
5. Run `sudo raspi-config` to do the basic OS setup
6. Navigate through the menu to set your desired options
   - Select "1 System Options"
     - Select "S1 Wireless LAN"
       - Choose country ("DE Germany")
       - Set SSID of your WLAN
       - Set password of your WLAN
     - Select "S3 Password"
       - Type in a new password for the Pi user "pi" (twice)
     - Select "S4 Hostname"
       - Type in a new hostname
   - Select "6 Advanced Options"
     - Select "A1 Expand Filesystem"
7. Reboot your Pi (run `sudo reboot` if not already asked for reboot)

### Install .NET 5

First of all: Many thanks to Pete Gallagher for his helpful [talk](https://www.youtube.com/watch?v=l8CXgvKe314) and [blog post](https://www.petecodes.co.uk/install-and-use-microsoft-dot-net-5-with-the-raspberry-pi/)!

1. Update all OS packages  
   `sudo apt update`
2. Install dependencies  
   `sudo apt-get -y install libunwind8 gettext`
3. Download .NET 5 SDK  
   `wget https://download.visualstudio.microsoft.com/download/pr/567a64a8-810b-4c3f-85e3-bc9f9e06311b/02664afe4f3992a4d558ed066d906745/dotnet-sdk-5.0.101-linux-arm.tar.gz`  
   You might want to get the current version [from here](https://dotnet.microsoft.com/download/dotnet/5.0) while selecting "SDK 5.\*.\* / Linux / Arm32"
4. Download ASP.NET Core Runtime 5  
   `wget https://download.visualstudio.microsoft.com/download/pr/11977d43-d937-4fdb-a1fb-a20d56f1877d/73aa09b745586ac657110fd8b11c0275/aspnetcore-runtime-5.0.1-linux-arm.tar.gz`  
   You might want to get current version [from here](https://dotnet.microsoft.com/download/dotnet/5.0) while selecting "ASP.NET Core Runtime 5.\*.\* / Linux / Arm32"
5. Create .NET main directory  
   `sudo mkdir /opt/dotnet`
6. Extract downloads (adjust file names if you chose a newer version!)  
   `sudo tar -xvf dotnet-sdk-5.0.101-linux-arm.tar.gz -C /opt/dotnet/`  
   and  
   `sudo tar -xvf aspnetcore-runtime-5.0.1-linux-arm.tar.gz -C /opt/dotnet/`
7. Link binaries to user profile
   `sudo ln -s /opt/dotnet/dotnet /usr/local/bin`
8. Make link permanent
   `echo 'export DOTNET_ROOT=/opt/dotnet' >> /home/pi/.bashrc`
9. Check installation  
   `dotnet --info`  
   which should show something like that:

   ```
   .NET SDK (reflecting any global.json):
   Version:   5.0.101
   Commit:    d05174dc5a

   Runtime Environment:
   OS Name:     raspbian
   OS Version:  10
   OS Platform: Linux
   RID:         linux-arm
   Base Path:   /opt/dotnet/sdk/5.0.101/

   Host (useful for support):
     Version: 5.0.1
     Commit:  b02e13abab

   .NET SDKs installed:
     5.0.101 [/opt/dotnet/sdk]

   .NET runtimes installed:
     Microsoft.AspNetCore.App 5.0.1 [/opt/dotnet/shared/Microsoft.AspNetCore.App]
     Microsoft.NETCore.App 5.0.1 [/opt/dotnet/shared/Microsoft.NETCore.App]
   ```

10. Clean up (don't forget to adjust your file names if necessary!)  
    `rm dotnet-sdk-5.0.101-linux-arm.tar.gz`  
    `rm aspnetcore-runtime-5.0.1-linux-arm.tar.gz`

### Install More Tools

1. Install your favorite version-control system (of course: git)  
   `sudo apt install git`  
   Check installation with  
   `git --version`  
   which should show something like that:
   ```
   git version 2.20.1
   ```
2.

## Clone & Run Your Application

1. Clone the application from your (or my) repository like  
   `git clone https://github.com/ARothacker/traffic-pi.git`
2. Switch into the application folder  
   `cd traffic-pi/console`
3. Run the application  
   `dotnet run`  
   which showed something like that at the time of writing this tutorial:
   ```
   Hello World! ;-)
   ```
