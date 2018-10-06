using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WinTracert.Annotations;

namespace WinTracert
{
    public class TracertModel: INotifyPropertyChanged
    {
        public static string commandName = "tracert.exe";
        public bool DParamIsSet { get; set; }
        public bool HParamIsSet { get; set; }
        public uint MaxHops { get; set; }
        public bool JParamIsSet { get; set; }
        public string HostList { get; set; } 
        public bool WParamIsSet { get; set; }
        public uint Timeout { get; set; }
        public bool RParamIsSet { get; set; }
        public bool SParamIsSet { get; set; }
        public string SrcAddr { get; set; }
        public bool FourParamIsSet { get; set; }
        public bool SixParamIsSet { get; set; }
        public string TargetName { get; set; }
        public string ErrorState { get; internal set; }
        private string _outText = "";

        public string OutText
        {
            get { return _outText; }
            set
            {
                _outText = value;
                OnPropertyChanged(nameof(OutText));
            }
        }

        internal bool IsValid()
        {
            //more criteria will be added later
            if (JParamIsSet && SixParamIsSet)
            {
                ErrorState = "IPv4 and IPv6 can't be forced at once";
                return false;
            }

            if (FourParamIsSet)
            {
                if (RParamIsSet || SParamIsSet)
                {
                    ErrorState= "IPv6 only params is set but force IPv4 is requested";
                }
            }
            return true;
        }

        internal string GetTextCommand()
        {
            if (IsValid())
            {
                string command = "";
                if (DParamIsSet)
                {
                    command += " /d ";
                }

                if (HParamIsSet)
                {
                    command += " /h " + MaxHops;
                }

                if (JParamIsSet)
                {
                    command += " /j " + HostList.Replace(',', ' ');
                }

                if (WParamIsSet)
                {
                    command += " /w " + Timeout;
                }

                if (RParamIsSet)
                {
                    command += " /R ";
                }

                if (SParamIsSet)
                {
                    command += " /S " + SrcAddr;
                }

                if (FourParamIsSet)
                {
                    command += " /4 ";
                }
                if (SixParamIsSet)
                {
                    command += " /4 ";
                }

                if (!string.IsNullOrEmpty(TargetName))
                {
                    command += TargetName;
                }

                return command;
            }
            else return "";
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
