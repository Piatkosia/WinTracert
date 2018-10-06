using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using CmdWrapper;

namespace WinTracert
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private TracertModel commandModel;
        public MainWindow()
        {
            InitializeComponent();
            commandModel = new TracertModel();
            DataContext = commandModel;
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void SetHopList(object sender, RoutedEventArgs e)
        {
            HopList hopList = new HopList(commandModel.HostList);
            hopList.ShowDialog();
            commandModel.HostList = hopList.IpList;
            JParamButton.ToolTip = commandModel.HostList;
        }

        private void RunCommand(object sender, RoutedEventArgs e)
        {
            if (commandModel.IsValid())
            {
                string cmdText = commandModel.GetTextCommand();
                if (cmdText.Length < CmdWrapper.Wrapper.MaxCommandLength)
                {
                    RunButton.IsEnabled = false;
                     Task.Run(()=> Run(cmdText));
                }
                else MessageBox.Show("Command is too long");


            }
            else MessageBox.Show("Invalid command" + commandModel.ErrorState);
        }

        private async Task Run(string cmdText)
        {
            Wrapper wrapper = new Wrapper();
            wrapper.OnIncommingText += Wrapper_OnIncommingText;
            wrapper.Exited += Wrapper_Exited;
            int errorCode = await wrapper.RunCmdProcess(TracertModel.commandName, cmdText);
        }

        private void Wrapper_Exited(object sender, EventArgs e)
        {
            RunButton.Dispatcher.BeginInvoke((Action)(() => RunButton.IsEnabled = true));            
        }

        private void Wrapper_OnIncommingText(object sender, IncommingTextEventArgs e)
        {
            commandModel.OutText += e.IncommingText + System.Environment.NewLine;
        }

        private void Param4_OnChecked(object sender, RoutedEventArgs e)
        {
            param6.IsChecked = false;
            paramS.IsChecked = false;
            paramR.IsChecked = false;
        }

        private void Param6_OnChecked(object sender, RoutedEventArgs e)
        {
            param4.IsChecked = false;
            paramJ.IsChecked = false;
        }

        private void ClearButton_OnClick(object sender, RoutedEventArgs e)
        {
            commandModel.OutText = "";
        }
    }
}
