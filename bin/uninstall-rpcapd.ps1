# Requires -RunAsAdministrator

if (-not ([Security.Principal.WindowsPrincipal][Security.Principal.WindowsIdentity]::GetCurrent()).IsInRole([Security.Principal.WindowsBuiltInRole] "Administrator")) {
    # Restart as administrator
    Start-Process powershell.exe -Verb RunAs -ArgumentList $MyInvocation.MyCommand.Definition
    # Exit
    Exit
}

$Service = Get-Service -Name rpcapd -ErrorAction SilentlyContinue
if ($Service -eq $null)
{
    Write-Host "rpcapd service not found. Exiting..."
} else {
    # Stop the service
    Write-Host "Stopping rpcapd service..."
    Stop-Service -Name rpcapd -Force
    # Remove the service
    Write-Host "Removing rpcapd service..."
    sc.exe delete rpcapd
    Write-Host "rpcapd service deleted."
}

# Hold the screen
Read-Host -Prompt "Press Enter to exit..."
