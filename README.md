# IGroupPolicyObject2
Load group policy's registry hive to a temporary registry key path, then you can edit it by normal external command.

When you use Group Policy on Windows 7/10, not in a AD server, you can not use
Powershell's Group Policy Module due to it only works on AD's group policy environment.

How can i edit the Group Policy's registry hive on local machine?

Here is the way:

- Use the IGroupPolicyObject2 to load registry hive
```
IGroupPolicyObject2 gpo = (IGroupPolicyObject2)new GroupPolicyClass();
gpo.OpenLocalMachineGPO(GroupPolicyFlags.LoadRegistryInformation);
```
- Get its mapped registry key path
```
StringBuilder rootRegPath = new StringBuilder(1024);
gpo.GetRegistryKeyPath(GroupPolicySection.User, rootRegPath, rootRegPath.Capacity);

Console.WriteLine("REG:" + rootRegPath);
```
- Then you are free to use any command or external tool, such as
  - regedit.exe or reg.exe, then goto the location printed above, then edit it.
  - Powershell's cd and New-Item or New-ItemProperty cmdlets

Good luck.
