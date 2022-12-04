# Lostark DPS Meter

A fork of [https://github.com/karaeren/LostArkLogger](https://github.com/karaeren/LostArkLogger) with the capability to
remote log packets via winpcap for docker.

![Image](https://safe.manu.moe/9Sxwowoi.jpg)

Zero risk made possible with [tabfloater](https://www.tabfloater.io).

## Setup

We call the pc where you run the game the main computer.

### First step: Setup docker

You have to install docker on the machine where you will be running the meter. You have three main options:
1. On your main computer (easiest but may carry higher risk, despite being abstracted in a Docker container)
2. On a Virtual Machine on your main computer (generally safe)
3. On a remote computer on the same network as your main computer (nice if you don't want the risk of option 1 or to deal with a VM)

For instructions on how to install docker, check [docker desktop](https://www.docker.com/).

### Second step: Setup rpcapd

You need to install [npcap](https://nmap.org/npcap/) on your main computer.

Make sure to have the option `Install Npcap in WinPcap API-compatible Mode` checked.

After that, you have to install rpcapd.

If your windows is blocked for install unsigned software, you have to disable it.

```powershell
Set-ExecutionPolicy Unrestricted
```
Then you can install rpcapd with the following command:

You can use the following [script to install it](bin/install-rpcapd.ps1).

This script install rpcapd as a service. You can check the service in your `services.msc` or with the following command:

```powershell
Get-Service rpcapd
```

### Third step: Configure and run the container

Currently this fork requires you to pull the code down, update the config.yml file, build the image locally, and run that local image, like so:

```powershell
git clone https://github.com/therealhumes/la-dpsmeter.git
```

You now have to change the `p-cap-address` to the ip address of your main computer in the `config.yml` file located in the root directory.

You can find out your ip address by running `ipconfig` in a command prompt. It's your local lan address, not your public ip.

Once done, make sure Docker from the first step is running, then you can continue with building the docker image and running it:

```powershell
cd la-dpsmeter
docker build -t la-dpsmeter .
docker run -d --name la-dpsmeter --restart unless-stopped -v ${pwd}/config.yml:/app/config.yml -v ${pwd}/logs:/mnt/raid1/apps/'Lost Ark Logs' -p 1338:1338 la-dpsmeter
```

Note that ${pwd}/config.yml assumes you're running the command while in the la-dpsmeter directory,  with the updated config.yml file.

### Fourth step: Access the overlay

You can access the overlay by opening the following url in your browser:

```
http://<remote-computer-ip>:1338
```

## Update

To update the container, delete the old container and follow the third step above again to pull the new code, re-build it, and re-run it.

To delete an old container:

```
docker rm -f la-dpsmeter
```

## Support & Troubleshooting

We have a [discord server](https://discord.gg/bM8NtsJVeb) where you can ask questions or report bugs.

# WARNING

This is not endorsed by Smilegate or AGS. Usage of this tool isn't defined by Smilegate or AGS. I do not save your
personal identifiable data. Having said that, the .pcap generated can potentially contain sensitive information (
specifically, a one-time use token)

### Archive

You can find the old installation instructions [here](.github/archive/INSTALLATION.md).
