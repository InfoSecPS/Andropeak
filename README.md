# AndroPeak

AndroPeak is a helper in a single EXE to run tasks for an Android test that can become mundane or get lost in terminal windows. 
The software was designed to be a one stop shop for repetitive tasks in Android that take up some terminal windows that over the course of an engagement, can get lost and closed by mistake. 


## Installation

You can build from source, import the project to Visual Studio 2022 and have a play, improve or export the binary. 

## Pre-requesites 

- ADB downloaded and in the environmental path for Windows
- Apktool.bat downloaded and stored in `C:\Windows\apktool.bat` or point the code to your path by modifying [line 90 here](https://github.com/LaresLLC/Andropeak/blob/master/Andropeak/Form1.cs#L90)
(I'll figure out a way to pick where you have Apktool installed or the path, but for now, please put it where it expects it :))

 ## OnLoad

- Device connects to AndroPeak automatically and runs `adb devices`, which displays the other UI elements.
- Program issues a check to see if the `http_proxy` is set on the device, and if the device returns anything other than `:0` it means the proxy is set, and will issue `adb shell settings put global http_proxy :0` to reset the `http_proxy` on the device.
- `adb reverse tcp:localPort tcp:remotePort` thankfully resets when the connection over USB is lost, so the program resests the UI elements for both reverse socket and Proxy connections. 

## Overview of Functionality

- Choose a working directory (UI Interactive elements appear) 
- List APKs installed on the device
- Pull the selected APK
- Load locally downloaded APKs from working directory
- Decompile the selected APK
- Copy `base.apk` to an `unzipped` folder, rename the file to `base.zip` and unzip the archive in the `unzipped` folder.
- List the Application permissions. Results are shown in a message box. `CTRL+C` to copy to clipboard. 
- If the App is configured with dangerous permissions as listed on the Android Developers portal, it will append `- Dangerous` to the end of the set permission. 
- Set/Unset reverse socket connection over USB using `adb reverse tcp:localPort tcp:remotePort` set in the background.
- Set/Unset device proxy using ADB. This doesn't update the UI within the network connection, so it's wise to Unset this as you won't get internet connectivity if your Proxy isn't live.
- ADB Shell: One click access to a terminal on the device so you don't have to open a terminal window and type `adb devices`
- Device Screenshot: Takes a screenshot on the device, and saves the output `.png` to the `workingDirectory`

  ## Error checking

 - localPort needs to be the same on both reverse socket connection and device proxy. Program will inform you if it's not. If they are different, there's no route to the listening Burp proxy on a PC. 
 - Device USB cable is unplugged, there's a message showing "Device disconnected", and the reverse socket and Proxy group UI elements reset.
 - When the USB cable is attached, the message displays what device has connected, and the `http_proxy` is reset to `:0` and the UI elements reset. 

## Usage

Connect to Device: 

Runs `adb devices` in the background and displays a success or failure. 

### List APKs: 

Essentially runs `adb shell pm list packages -f | findstr base` to load the installed packages to a selectable combo box

### Pull APK:

Pulls the selected package APK and saves to your choosen working directory.

### List Local APKs: 

Lists the already pulled APKs stored in the chosen working directory - Selection is passed to decompilation. 

### Decompile Selection: 

Decompiles the selected APK using Apktool, which is assumed to already be downloaded in working directory.

### App Permissions

Checks the application permissions from the selected local APK, and compares them against dangerous permissions. Lists the whole permission set, and adds `- Dangerous` to the end that match the dangerous permissions set by Android. 

### Unzip APK:

Takes the `base.apk` file pulled to local storage, creates an `unzipped` folder, renames `base.apk` to `base.zip` and unzips the archive. 

### Open Folder: 

Opens the working directory because it's nice to have a quick way to open that folder.

### Device Port Reverse: 

Usually, to send traffic to a local proxy over USB, you would issue the command `adb reverse tcp:localPort tcp:remotePort` in the command line, so this does that for you. There's a `Set` and an `Unset` button. 
Once you `Set`, you can only `Unset` and vice/versa. 
If a `config.json` file exists and `localPort` and `remotePort` are set, the UI elements will show the ports set, and disable the `Set` button. 
It's still needs work to check the device configuration on this, but this serves as a reminder for now. 

### Device Proxy: 

To save entering the convoluted Android UI to set the proxy manually, you can do so with ADB. `Set` issues the command `adb shell settings put global http_proxy 127.0.0.1:localPort` while `Unset` removes 
this global setting by issuing `adb shell settings put global http_proxy :0` - These settings override the manual setting of the proxy in the UI, and disappear with a device restart. 
Also worth noting that if you don't `Unset` this, you won't have internet access if the device isn't connected over USB, and the reverse tcp connection is set. 

### ADB Shell: 

Opens a separate terminal window with a shell on the device, instead of having to open CMD, then type `adb shell`. You still need to issue `su root` to get a root shell because the ADB daemon doesn't run as root natively. 

### Device Screenshot

Button to take a screencap of the device screen, and save it to the defined `workingDirectory` 

## Create Self Contained EXE file:

There's a debug version of the EXE file in the Project Folder `bin` directory, but it needs the supporting dlls to run, where as the link below, shows how to publish a self contained EXE file in Visual Studio. It differes slightly for Visual Studio 2022, but easy enough to modify and follow. 

[Publish self contained exe in Visual Studio](https://learn.microsoft.com/en-us/dotnet/core/deploying/single-file/overview?tabs=vs)

## Things to do

- Check the reverse socket connection at the device level using `adb reverse --list` and updating the UI with the output at FormLoad.
- Figure out a way to interact with the device over the network if USB isn't an option.
- Add another form tab to explore more exploitative functionality in Android tests, for example: Pull all XML/DB files to a local folder


