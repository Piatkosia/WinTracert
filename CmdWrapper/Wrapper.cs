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
        public event EventHandler<IncommingTextEventArgs> Failed;
        public async Task<int> RunCmdProcess(string command, string parameters, string workingDirPath = "", Encoding encoding = null)
        {
            int ExitCode = 0;
            ProcessStartInfo ProcessInfo;
            Process process;

            ProcessInfo = new ProcessStartInfo(command, parameters);
            ProcessInfo.CreateNoWindow = true;
            ProcessInfo.UseShellExecute = false;
            ProcessInfo.RedirectStandardOutput = true;
            ProcessInfo.RedirectStandardError = true;
            if (encoding != null)
            {
                ProcessInfo.StandardErrorEncoding = encoding;
                ProcessInfo.StandardOutputEncoding = encoding;
            }

            if (!string.IsNullOrEmpty(workingDirPath))
            {
                ProcessInfo.WorkingDirectory = workingDirPath;
            }

            try
            {
                process = Process.Start(ProcessInfo);
                if (process != null)
                {
                    process.EnableRaisingEvents = true;
                    process.OutputDataReceived += new DataReceivedEventHandler(OutputHandler);
                    process.ErrorDataReceived += new DataReceivedEventHandler(OutputHandler);
                    process.Exited += Process_Exited;
                    process.BeginOutputReadLine();
                    process.BeginErrorReadLine();
                    process.WaitForExit();
                    ExitCode = process.ExitCode;
                    process.Dispose();
                }
            }
            catch (Exception e)
            {
                if (Failed != null)
                {
                    Failed(this, new IncommingTextEventArgs($"{e.Message}\n Command: {command} Parameters: {parameters} WorkingDirPath: {workingDirPath} Debug info: \n {e.StackTrace}"));
                    return -999;
                }
            }

           
            return ExitCode;
        }

        private void Process_Exited(object sender, EventArgs e)
        {
            if (Exited != null)
            {
                Exited(sender, new EventArgs());
            }
        }

        private void OutputHandler(object sender, DataReceivedEventArgs e)
        {
            if (OnIncommingText != null)
            {
                OnIncommingText(sender, new IncommingTextEventArgs(e.Data));
            }
        }
    }
}
