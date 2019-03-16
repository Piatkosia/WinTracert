using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CmdWrapper;

namespace WrapperExample
{
    class Program
    {
        static void Main(string[] args)
        {
           Wrapper wrapper = new Wrapper();
           wrapper.Failed += Wrapper_Failed;
           wrapper.OnIncommingText += Wrapper_OnIncommingText;
           wrapper.Exited += Wrapper_Exited;
           Console.Write("command: ");
           string command = Console.ReadLine();
           Console.Write("parameters: ");
           string parameters = Console.ReadLine();
           wrapper.RunCmdProcess(command, parameters).Wait();
        }

        private static void Wrapper_Exited(object sender, EventArgs e)
        {
            Console.WriteLine("EOT");
        }

        private static void Wrapper_OnIncommingText(object sender, IncommingTextEventArgs e)
        {
            Console.WriteLine("> " + e.IncommingText);
        }

        private static void Wrapper_Failed(object sender, IncommingTextEventArgs e)
        {
            Console.WriteLine("F> " + e.IncommingText);
        }
    }
}
