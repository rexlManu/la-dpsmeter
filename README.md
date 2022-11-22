# Lostark DPS Meter

A fork of [https://github.com/karaeren/LostArkLogger](https://github.com/karaeren/LostArkLogger) with the capability to
remote log packets via winpcap for docker.

![Image](https://safe.manu.moe/9Sxwowoi.jpg)

Zero risk made possible with [tabfloater](https://www.tabfloater.io).

## Setup

We call the pc where you run the game the main computer and the pc where you run the dps meter the remote computer.
Both computers have to be in the same network.

It's possible but not recommended to run the dps meter on the main computer.

### First step: Setup docker
You have to install docker on the remote computer.

For instructions on how to install docker, check the [docker desktop](https://www.docker.com/).

### Second step: Setup rpcapd

You need to install [npcap](https://nmap.org/npcap/) on your main computer.

Make sure to have the option `Install Npcap in WinPcap API-compatible Mode` checked.

After that, you have to install rpcapd.
You can use the following [script to install it](bin/install-rpcapd.ps1).

This script install rpcapd as a service. You can check the service in your `services.msc` or with the following command:

```powershell
Get-Service rpcapd
```

### Third step: Configure and run the container

You have to pull the latest version of the docker image with the following command:

```bash
docker pull ghcr.io/rexlmanu/la-dpsmeter:main
```

Copy the template from [default.config.yml](default.config.yml) and rename it to `config.yml`.
You have to change the `p-cap-address` to the ip address of your main computer.

You can find out your ip address by running `ipconfig` in a command prompt. It's your local lan address, not your public ip.

After that, you can run the container with the following command:

If you're on powershell, replace `$(pwd)` with `${PWD}`. On cmd, replace `$(pwd)` with `%cd%`.

```bash
docker run -d \
  --name la-dpsmeter \
  --restart unless-stopped \
  -v $(pwd)/config.yml:/app/config.yml \
  -v $(pwd)/logs:/mnt/raid1/apps/'Lost Ark Logs' \
  ghcr.io/rexlmanu/la-dpsmeter:main
```

## Update

To update the container, you have to pull the latest version of the docker image with the following command:

```bash
docker pull ghcr.io/rexlmanu/la-dpsmeter:main
```

After that, you have to delete the container and recreate it with the same command as above (setup).

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