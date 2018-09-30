using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
            //tracert [/d] [/h <MaximumHops>] [/j <Hostlist>] [/w <timeout>] [/R] [/S <Srcaddr>] [/4][/6] <TargetName>
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
            }
            else MessageBox.Show("Invalid command" + commandModel.ErrorState);
        }
    }
}
