using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;

namespace WinTracert
{
    /// <summary>
    /// Interaction logic for HopList.xaml
    /// </summary>
    public partial class HopList : Window
    {
        public const int MaxCount = 9;

        private string ipRegex =
            @"^(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$";

        private Regex regex;

        public HopList(string ipList)
        {
            InitializeComponent();
            HopText.Text = ipList;
            regex = new Regex(ipRegex);
        }

        public string IpList { get; private set; } = "";

        private void Apply(object sender, RoutedEventArgs e)
        {
            string IpListCandidate = HopText.Text;
            string[] ips = IpListCandidate.Split('\n', ',');
            if (ips.Length > 9)
            {
                MessageBox.Show("Too many IPs! Max is 9.");
                return;
            }
            foreach (var ipCandidate in ips)
            {
                Match match = regex.Match(ipCandidate.Trim());
                if (match.Success)
                {
                    if (String.IsNullOrEmpty(IpList)) IpList = match.Value;
                    else IpList = IpList + ", " + match.Value;
                }
            }
            Close();
        }

    }
}
