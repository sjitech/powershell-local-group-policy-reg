# powershell-local-group-policy-reg
Load group policy's registry hive to a temporary registry key path, then you can edit it by normal external command.

When you use Group Policy on Windows 7/10, not in a AD server, you can not use
Powershell's Group Policy Module due to it only works on AD's group policy environment.

How can i edit the Group Policy's registry hive on local machine?

Here is the way:

### Use the IGroupPolicyObject2 to load registry hive and Get its mapped registry key path

With PowerShell, you can import C# source to get a GroupPolicy.Reg class as defined in [IGroupPolicyObject2.cs](IGroupPolicyObject2.cs).
```
Add-Type -Path IGroupPolicyObject2.cs
```
Then you load registry hive of local group policy:
```
[GroupPolicy.Reg]::Load()
```
Then you can see where the registry hive is mapped into your real registry.
```
[GroupPolicy.Reg]::userRegPath
[GroupPolicy.Reg]::machineRegPath
```
Sample output:
```
Software\Microsoft\Windows\CurrentVersion\Group Policy Objects\{722642C1-7DCB-475B-96EA-16BB4B899EA7}User
Software\Microsoft\Windows\CurrentVersion\Group Policy Objects\{722642C1-7DCB-475B-96EA-16BB4B899EA7}Machine
```
Both are relative to HKEY_CURRENT_USER, you can see it in regedit tool.

### Then you are free to use any command or external tool
  - regedit.exe or reg.exe, then goto the location printed above, then edit it.
  - Powershell's cd and New-Item or New-ItemProperty ... cmdlets

Powershell:
```
cd HKCU:\"$([GroupPolicy.Reg]::userRegPath)"
#Use New-Item or Set-Item or Set-ItemProperty ...  to manupilate registry
```

### Call GroupPolicy.Reg.Save() to save to registry hive.
```
[GroupPolicy.Reg]::Save()
```

### If you'v finished all work, then you should call Unload to unmap the registry hive.
```
[GroupPolicy.Reg]::Hive()
```

### And do not forget to use gpupdate to apply group policy immdiately
```
gpupdate
```


### Sample powershell script:
```
Add-Type -Path c:\files\IGroupPolicyObject2.cs

[GroupPolicy.Reg]::Load()

cd HKCU:\"$([GroupPolicy.Reg]::userRegPath)"

New-Item .\Software\Microsoft\Windows\CurrentVersion\Policies\System\_ -Force
Remove-Item .\Software\Microsoft\Windows\CurrentVersion\Policies\System\_

Set-ItemProperty -Path .\Software\Microsoft\Windows\CurrentVersion\Policies\System -Name Wallpaper -Value C:\files\WiFiConnected.png
Set-ItemProperty -Path .\Software\Microsoft\Windows\CurrentVersion\Policies\System -Name WallpaperStyle -Type DWORD -Value 2

Remove-ItemProperty -Path .\Software\Microsoft\Windows\CurrentVersion\Policies\System -Name **del.Wallpaper
Remove-ItemProperty -Path .\Software\Microsoft\Windows\CurrentVersion\Policies\System -Name **del.WallpaperStyle

[GroupPolicy.Reg]::Save()
[GroupPolicy.Reg]::Unload()

gpupdate
```

Good luck.



