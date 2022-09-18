# Requires -RunAsAdministrator
# Port for rpcapd, must be equal to p-cap-port in config.yml
$Port = 1337

# Dir to save rpcapd service
$InstallDir = "C:\Program Files\rpcapd"
$Npcap = $false
$Winpcap = $false

# URL to latest libpcap release with rpcapd
$LibpcapRelease = "https://github.com/guy0090/libpcap/releases/latest/download/Build.zip"

if (-not ([Security.Principal.WindowsPrincipal][Security.Principal.WindowsIdentity]::GetCurrent()).IsInRole([Security.Principal.WindowsBuiltInRole] "Administrator")) {
    # Restart as administrator
    Start-Process powershell.exe -Verb RunAs -ArgumentList $MyInvocation.MyCommand.Definition
    # Exit
    Exit
}

$CurrentPrincipal = New-Object Security.Principal.WindowsPrincipal([Security.Principal.WindowsIdentity]::GetCurrent())

if ($CurrentPrincipal.IsInRole([Security.Principal.WindowsBuiltInRole]::Administrator)) {
    Write-Host "Running as administrator" -ForegroundColor Green
    # Check if npcap or winpcap is installed
    if (Get-ItemProperty -Path "HKLM:\SOFTWARE\WOW6432Node\Npcap" -ErrorAction SilentlyContinue) {
        $Npcap = $true
        $InstallDir = Get-ItemProperty -Path "HKLM:\SOFTWARE\WOW6432Node\Npcap" | Select-Object -ExpandProperty "(default)"
    } elseif (Get-ItemProperty -Path "HKLM:\SOFTWARE\WOW6432Node\WinPcap" -ErrorAction SilentlyContinue) {
        $Winpcap = $true
        $InstallDir = Get-ItemProperty -Path "HKLM:\SOFTWARE\WOW6432Node\WinPcap" | Select-Object -ExpandProperty "(default)"
    } else {
        Write-Host "Npcap or Winpcap is not installed. Please install Npcap or Winpcap first." -ForegroundColor Red
        exit
    }

    # Setup rpcapd service
    $Service = Get-Service -Name rpcapd -ErrorAction SilentlyContinue
    if ($null -eq $Service) {
        Write-Host "`nNo rpcapd service found, downloading and installing it to $InstallDir"
        
        # If rpcapd isn't present, download and install it as a service
        # Grab latest libpcap release
        Invoke-WebRequest -Uri $LibpcapRelease -OutFile Build.zip
        Expand-Archive -Force -Path Build.zip -DestinationPath $InstallDir -ErrorAction SilentlyContinue
        
        # Install rpcapd as a service
        New-Service -Name rpcapd -Description "Remote Packet Capture Protocol v.0 (experimental)" -BinaryPathName "$InstallDir\rpcapd.exe -d -p $Port -n" -StartupType Automatic
        
        # Cleanup build files
        Remove-Item -Path Build.zip -Force -ErrorAction SilentlyContinue
        
        # Start rpcapd
        Start-Service rpcapd
        
        Write-Host "`nrpcapd service installed and started`n"
    } elseif ($Service.Status -ne "Running") {
        Write-Host "`nStarting rpcapd service`n"
        Start-Service -Name rpcapd
    } else {
        Write-Host "`nrpcapd service is already running`n"
    }
  
} else {
  Write-Host "You must run this script as administrator." -ForegroundColor Red;
}

# keep the window open
Read-Host -Prompt "Press Enter to exit"
 #>