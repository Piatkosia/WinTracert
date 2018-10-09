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

        public bool DParamIsSet
        {
            get => _dParamIsSet;
            set
            {
                if (value == _dParamIsSet) return;
                _dParamIsSet = value;
                OnPropertyChanged();
            }
        }

        public bool HParamIsSet
        {
            get => _hParamIsSet;
            set
            {
                if (value == _hParamIsSet) return;
                _hParamIsSet = value;
                OnPropertyChanged();
            }
        }

        public uint MaxHops
        {
            get => _maxHops;
            set
            {
                if (value == _maxHops) return;
                _maxHops = value;
                OnPropertyChanged();
            }
        }

        public bool JParamIsSet
        {
            get => _jParamIsSet;
            set
            {
                if (value == _jParamIsSet) return;
                _jParamIsSet = value;
                OnPropertyChanged();
            }
        }

        public string HostList
        {
            get => _hostList;
            set
            {
                if (value == _hostList) return;
                _hostList = value;
                OnPropertyChanged();
            }
        }

        public bool WParamIsSet
        {
            get => _wParamIsSet;
            set
            {
                if (value == _wParamIsSet) return;
                _wParamIsSet = value;
                OnPropertyChanged();
            }
        }

        public uint Timeout
        {
            get => _timeout;
            set
            {
                if (value == _timeout) return;
                _timeout = value;
                OnPropertyChanged();
            }
        }

        public bool RParamIsSet
        {
            get => _rParamIsSet;
            set
            {
                if (value == _rParamIsSet) return;
                _rParamIsSet = value;
                OnPropertyChanged();
            }
        }

        public bool SParamIsSet
        {
            get => _sParamIsSet;
            set
            {
                if (value == _sParamIsSet) return;
                _sParamIsSet = value;
                OnPropertyChanged();
            }
        }

        public string SrcAddr
        {
            get => _srcAddr;
            set
            {
                if (value == _srcAddr) return;
                _srcAddr = value;
                OnPropertyChanged();
            }
        }

        public bool FourParamIsSet
        {
            get => _fourParamIsSet;
            set
            {
                if (value == _fourParamIsSet) return;
                _fourParamIsSet = value;
                OnPropertyChanged();
            }
        }

        public bool SixParamIsSet
        {
            get => _sixParamIsSet;
            set
            {
                if (value == _sixParamIsSet) return;
                _sixParamIsSet = value;
                OnPropertyChanged();
            }
        }

        public string TargetName
        {
            get => _targetName;
            set
            {
                if (value == _targetName) return;
                _targetName = value;
                OnPropertyChanged();
            }
        }

        public string ErrorState
        {
            get => _errorState;
            internal set
            {
                if (value == _errorState) return;
                _errorState = value;
                OnPropertyChanged();
            }
        }

        private string _outText = "";
        private bool _dParamIsSet;
        private bool _hParamIsSet;
        private uint _maxHops;
        private bool _jParamIsSet;
        private string _hostList;
        private bool _wParamIsSet;
        private uint _timeout;
        private bool _rParamIsSet;
        private bool _sParamIsSet;
        private string _srcAddr;
        private bool _fourParamIsSet;
        private bool _sixParamIsSet;
        private string _targetName;
        private string _errorState;

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
