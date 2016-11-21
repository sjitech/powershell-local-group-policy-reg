﻿using System;
using System.Text;
using GroupPolicy;

namespace ConsoleApplication1
{
    class Program
    {
        static Guid myGuid = Guid.NewGuid();

        static void Main(string[] args)
        {
            IGroupPolicyObject2 gpo = (IGroupPolicyObject2)new GroupPolicyClass();
            try
            {
                gpo.OpenLocalMachineGPO(GroupPolicyFlags.LoadRegistryInformation);

                StringBuilder rootRegPath = new StringBuilder(1024);
                gpo.GetRegistryKeyPath(GroupPolicySection.User, rootRegPath, rootRegPath.Capacity);

                Console.WriteLine("REG:" + rootRegPath);

                //do your work here, change the registry 

                gpo.Save(/*machine part:*/false, /*extension add:*/true, GroupPolicyExtensionGuids.Registry, myGuid);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}