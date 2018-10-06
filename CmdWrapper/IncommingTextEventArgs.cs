using System;

namespace CmdWrapper
{
    public class IncommingTextEventArgs : EventArgs
    {
        public string IncommingText { get; private set; }

        public IncommingTextEventArgs(string incommingText)
        {
            IncommingText = incommingText;
        }
    }
}