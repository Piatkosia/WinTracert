using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinTracert
{
    public class TracertModel
    {
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

        internal bool IsValid()
        {
            //Sprawdzenie parametrów

            throw new NotImplementedException();
        }

        internal string GetTextCommand()
        {
            if (IsValid())
            {
                string command = "tracert ";
                if (DParamIsSet)
                {
                    command += " -d ";
                }

                return command;
            }
            else return "";
        }
    }
}
