using System;
using Microsoft.Win32;

namespace Reg
{
    class Program
    {
        static void Main(string[] args)
        {
            RegistryKey rk = Registry.LocalMachine;
            GetKeys(rk);
        }

        private static void GetKeys(RegistryKey rk)
        {
            String[] names = rk.GetSubKeyNames();
            Console.WriteLine("Subkeys of " + rk.Name);
            Console.WriteLine("-----------------------------------------------");
            foreach (String s in names)
            {
                Console.WriteLine(s);
            }

            Console.ReadKey();
        }
    }
}
