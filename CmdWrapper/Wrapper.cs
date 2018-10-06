﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;

namespace CmdWrapper
{
    public class Wrapper
    {
        public const int MaxCommandLength = 8191;
        public event EventHandler<IncommingTextEventArgs> OnIncommingText;
        public event EventHandler<EventArgs> Exited;
        public async Task<int> RunCmdProcess(string filename, string parameters)
        {
            int ExitCode = 0;
            ProcessStartInfo ProcessInfo;
            Process process;

            ProcessInfo = new ProcessStartInfo(filename, parameters);
            ProcessInfo.CreateNoWindow = true;
            ProcessInfo.UseShellExecute = false;
            ProcessInfo.RedirectStandardOutput = true;
            process = Process.Start(ProcessInfo);
            
            if (process != null)
            {
                process.EnableRaisingEvents = true;
                process.OutputDataReceived += new DataReceivedEventHandler(OutputHandler);
                process.ErrorDataReceived += new DataReceivedEventHandler(OutputHandler);
                process.Exited += Process_Exited;
                process.BeginOutputReadLine();
                process.WaitForExit();
                ExitCode = process.ExitCode;
            }
            return ExitCode;
        }

        private void Process_Exited(object sender, EventArgs e)
        {
            if (Exited != null)
            {
                Exited(null, new EventArgs());
            }
        }

        private void OutputHandler(object sender, DataReceivedEventArgs e)
        {
            if (OnIncommingText != null)
            {
                OnIncommingText(null, new IncommingTextEventArgs(e.Data));
            }
        }
    }
}
