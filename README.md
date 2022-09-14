# Lostark DPS Meter

A fork of [https://github.com/karaeren/LostArkLogger](https://github.com/karaeren/LostArkLogger) with the capability to
remote log packets via winpcap for docker.

![Image](https://safe.manu.moe/9Sxwowoi.jpg)

Zero risk made possible with [tabfloater](https://www.tabfloater.io).

## Download

### Binaries

You can always find the newest binaries [here](https://github.com/rexlManu/la-dpsmeter/actions/workflows/main.yml).

Just click on the latest Build and scroll down. You will find the binaries in the Artifacts section.

Provided binaries are for Windows, Linux and Linux ARM.

## Setup

You have the following options to run this.

### Single computer setup

If you only have access to a single computer, you have the following options:

- Run the docker container on the same computer as the game
- Run it directly on the computer as the game
- Run it on a vm on the same computer as the game

### Multi computer setup

If you have access to multiple computers, you have the following options:

- Run the tool on your second computer on windows
- Run the tool on your server on linux
- Run it on a raspberry pi on linux
- Run it on your smartphone on android

#### Run it directly on your computer

This is the most risky option, but it's the easiest to setup.
Download the latest binaries, check the binaries section for more information.

Run the `dps-meter.exe` executable file.
The first time a config file will be created in the same directory as the executable.
You can edit the config file to your liking.
Start the file again and you should be good to go.

#### Run it in a docker container

This is the safest option, but it's a bit more complicated to setup.

You have to install [docker desktop](https://www.docker.com/). This requires you to enable virtualization in your BIOS.
If you have docker desktop installed, head over to the docker section.

#### Run it in a vm

You could also run it in a windows or linux vm. For windows, follow the instructions you already read or follow the
linux setup here under.

### Preparation

#### Install pcap on your main computer

##### Outdated (it works but should only work on windows 10)

You have to install winpcap on your main computer. You can download it
from [here](https://www.winpcap.org/install/default.htm).

After you have installed it, you need to create a `start-winpcap.bat` file somewhere on your computer.
This file should contain the following:

```shell
@REM change directory to C:\Program Files (x86)\WinPcap
@cd C:\Program Files (x86)\WinPcap
@REM start the rpcapd service
@start rpcapd.exe -p 1337 -n
```

You can change the port to whatever you want, but you have to change it in the docker container as well.
Every time you want to start the winpcap service, you have to run this file.
Of course you could also add it to your startup folder.

##### recommended: npcap (works on windows 10 and 11)

You have to install npcap on your main computer. You can download it
from [here](https://nmap.org/npcap/).

Make sure to have the option `Install Npcap in WinPcap API-compatible Mode` checked.

After you have to download the `rpcapd` archive from [here](https://github.com/guy0090/libpcap/releases/tag/0.0.0).
You have to extract the archive in the same directory as the `npcap` installation directory.
This folder is usually located in `C:\Program Files\Npcap`.

After you have installed it, you need to create a `start-npcap.bat` file somewhere on your computer.

This file should contain the following:

```shell
@REM change directory to C:\Program Files\Npcap
@cd C:\Program Files\Npcap
@REM start the rpcapd service
@start rpcapd.exe -p 1337 -n
```

You can change the port to whatever you want, but you have to change it in the docker container as well.
Every time you want to start the npcap service, you have to run this file.
Of course you could also add it to your startup folder.

#### Install dps-meter on windows

Download the latest binaries, check the binaries section for more information, extract it somewhere.
Copy the [default.config.yml](default.config.yml) file to the same directory and rename it to `config.yml`.

Change the `p-cap-address` to the ip address of your main computer.
You can find out your ip address by running `ipconfig` in a command prompt.
And change the `p-cap-port` to the port you used in the `start-winpcap.bat` file.

You can run the `dps-meter.exe` file to start the tool.

#### Install dps-meter on linux

Download the latest binaries, check the binaries section for more information.
Extract it somewhere after you have downloaded it.
Copy the [default.config.yml](default.config.yml) file to the same directory and rename it to `config.yml`.

Change the `p-cap-address` to the ip address of your main computer.

You can start the tool by running `dotnet dps-meter.dll`.

#### Run it in a docker container

##### Windows

You have to install [docker desktop](https://www.docker.com/).
This requires you to enable virtualization in your BIOS.

##### Linux

You have to install [docker](https://docs.docker.com/engine/install/).

##### Run the container

You have to clone this repository and build the docker image.

- `git clone https://github.com/rexlManu/la-dpsmeter.git`
- `cd la-dpsmeter`
- `docker build -t la-dpsmeter .`

After you have built the image, you can run the container.
Before you run the container, you have to create a `config.yml` file.
You can copy the [default.config.yml](default.config.yml) file and change the `p-cap-address` to the ip address of your
main computer.

###### Bash

```bash
  docker run -d --name dps-meter --restart=unless-stopped -v $(pwd)/config.yml:/app/config.yml -p 1338:1338 la-dpsmeter
```

###### Powershell

```bash
  docker run -d --name dps-meter --restart=unless-stopped -v ${PWD}/config.yml:/app/config.yml -p 1338:1338 la-dpsmeter
```

###### Batch (cmd)

```bash
  docker run -d --name dps-meter --restart=unless-stopped -v %cd%/config.yml:/app/config.yml -p 1338:1338 la-dpsmeter
```

## DPS Meter Overlay

You can find the dps meter at `http://server-ip:1338` (port 1338 in this example).

## Support

We have a [discord server](https://discord.gg/bm8ntsjveb) where you can ask questions or report bugs.

# WARNING

This is not endorsed by Smilegate or AGS. Usage of this tool isn't defined by Smilegate or AGS. I do not save your
personal identifiable data. Having said that, the .pcap generated can potentially contain sensitive information (
specifically, a one-time use token)
  