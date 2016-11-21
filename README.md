# IGroupPolicyObject2
Load group policy's registry hive to a temporary registry key path, then you can edit it by normal external command.

When you use Group Policy on Windows 7/10, not in a AD server, you can not use
Powershell's Group Policy Module due to it only works on AD's group policy environment.

How can i edit the Group Policy's registry hive on local machine?

Here is the way:


- Use the IGroupPolicyObject2 to load registry hive and Get its mapped registry key path
This is done automatically, you use call GroupPolicy.Reg.userRegPath or machineRegPath to get the reg path.

Use PowerShell, you do not need the DLL, just import C# source is OK.
```
Add-Type -Path thePathOfIGroupPolicyObject2.cs
```
Then you can see type `GroupPolicy.Reg` which have two static var.
```
[GroupPolicy.Reg]::userRegPath
[GroupPolicy.Reg]::machineRegPath
```
Sample output
```
Software\Microsoft\Windows\CurrentVersion\Group Policy Objects\{722642C1-7DCB-475B-96EA-16BB4B899EA7}User
Software\Microsoft\Windows\CurrentVersion\Group Policy Objects\{722642C1-7DCB-475B-96EA-16BB4B899EA7}Machine
```
- Then you are free to use any command or external tool, such as
  - regedit.exe or reg.exe, then goto the location printed above, then edit it.
  - Powershell's cd and New-Item or New-ItemProperty cmdlets

Powershell:
```
cd HKCU:\"$([GroupPolicy.Reg]::userRegPath)"
Use Add-Item or Set-Item or Set-ItemProperty ...  to manupilate registry
```

Good luck.
