using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace GroupPolicy
{
    [ComImport, Guid("EA502722-A23D-11d1-A7D3-0000F87571E3")]
    public class GroupPolicyClass
    {
    }

    [ComImport, Guid("7E37D5E7-263D-45CF-842B-96A95C63E46C"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IGroupPolicyObject2
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords")]
        uint New(
              [MarshalAs(UnmanagedType.LPWStr)] string domainName,
              [MarshalAs(UnmanagedType.LPWStr)] string displayName,
              uint flags);

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        uint OpenDSGPO(
            [MarshalAs(UnmanagedType.LPWStr)] string path,
            uint flags);

        uint OpenLocalMachineGPO(
            uint flags);

        uint OpenRemoteMachineGPO(
            [MarshalAs(UnmanagedType.LPWStr)] string computerName,
            uint flags);

        uint Save(
            [MarshalAs(UnmanagedType.Bool)] bool machine,
            [MarshalAs(UnmanagedType.Bool)] bool add,
            [MarshalAs(UnmanagedType.LPStruct)] Guid extension,
            [MarshalAs(UnmanagedType.LPStruct)] Guid app);

        uint Delete();

        uint GetName(
            [MarshalAs(UnmanagedType.LPWStr)] StringBuilder name,
            int maxLength);

        uint GetDisplayName(
            [MarshalAs(UnmanagedType.LPWStr)] StringBuilder name,
            int maxLength);

        uint SetDisplayName(
            [MarshalAs(UnmanagedType.LPWStr)] string name);

        uint GetPath(
            [MarshalAs(UnmanagedType.LPWStr)] StringBuilder path,
            int maxPath);

        uint GetDSPath(
            uint section,
            [MarshalAs(UnmanagedType.LPWStr)] StringBuilder path,
            int maxPath);

        uint GetFileSysPath(
            uint section,
            [MarshalAs(UnmanagedType.LPWStr)] StringBuilder path,
            int maxPath);

        uint GetRegistryKey(
            uint section,
            out IntPtr key);

        uint GetOptions(out uint options);

        uint SetOptions(
            uint options,
            uint mask);

        uint GetType(
            out IntPtr gpoType
        );

        uint GetMachineName(
            [MarshalAs(UnmanagedType.LPWStr)] StringBuilder name,
            int maxLength);

        uint GetPropertySheetPages(
            out IntPtr pages);

        uint OpenLocalMachineGPOForPrincipal(
            [MarshalAs(UnmanagedType.LPWStr)] string pszLocalUserOrGroupSID,
            uint dwFlags
        );

        uint GetRegistryKeyPath(
            uint section,
            [MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszRegistryKeyPath,
            int maxPath);
    }

    public class GroupPolicyFlags
    {
        public const uint LoadRegistryInformation = 0x00000001;
        public const uint Readonly = 0x00000002;
    }

    public class GroupPolicySection
    {
        public const uint User = 1;
        public const uint Machine = 2;
    }

    public class GroupPolicyExtensionGuids
    {
        /// <summary>
        /// The snap-in that processes .pol files
        /// </summary>
        public static readonly Guid Registry = new Guid(0x35378EAC, 0x683F, 0x11D2, 0xA8, 0x9A, 0x00, 0xC0, 0x4F, 0xBB, 0xCF, 0xA2);
    }
}
