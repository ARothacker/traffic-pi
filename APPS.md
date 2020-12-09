ARothacker Web.Applications Learning

# TrafficPi - Applications

## Clone Repository & Run Apps

Clone the application from your (or my) repository like  
`git clone https://github.com/ARothacker/traffic-pi.git`

## Hello World Console App

A plain Hello World app to test .NET 5

1. Switch into the application folder  
   `cd ~/traffic-pi/console-app`
2. Run the application  
   `dotnet run`  
   which should show this:
   ```
   Hello World! ;-)
   ```
   On errors like  
   `A fatal error occurred. The required library libhostfxr.so could not be found.`  
   you have to set the .NET root variable again with  
   `export DOTNET_ROOT=/opt/dotnet`
3. Want it faster? First build and then run the compiled DLL using  
   `dotnet build`  
   `dotnet bin/Debug/net5.0/console-app.dll`

## Simple LED Console App

An app to test the GPIO basics of your Raspberry Pi connected to a simple breadboard setup

1. Set up your breadboard like described [here](#)
2. Switch into the application folder  
   `cd ~/traffic-pi/led-console-app`
3. Run the application  
   `dotnet run`
   which should make your LED blinking
