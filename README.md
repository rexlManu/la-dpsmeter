# Lostark DPS Meter

A fork of [https://github.com/karaeren/LostArkLogger](https://github.com/karaeren/LostArkLogger) with the capability to remote log packets via winpcap for docker.

## Setup

### Main machine (windows)

- Install winpcap
- Create a batch file with the following content:

```shell
@REM change directory to C:\Program Files (x86)\WinPcap
@cd C:\Program Files (x86)\WinPcap
@REM start the rpcapd service
@start rpcapd.exe -p 1337 -n
```
- Open your port on the firewall (1337 in this example)
- Execute the batch file every time you want to have dps meter

### Remote server
Default `config.yml`
```yaml
region: Steam
p-cap-address: 192.168.178.99
p-cap-interface: ''
p-cap-port: 1337
web-port: 1338
```

You just have to edit the `p-cap-address` to the ip of your pc of running rpcapd.

#### Run the container

```bash
  docker run -it -v $(pwd)/config.yml:/app/config.yml -p 1338:1338 rexlmanu/la-dpsmeter:latest
```

If the container could connect to your machine you can run the container in detached (`-d`) mode.

# WARNING
This is not endorsed by Smilegate or AGS. Usage of this tool isn't defined by Smilegate or AGS. I do not save your personal identifiable data. Having said that, the .pcap generated can potentially contain sensitive information (specifically, a one-time use token)
  