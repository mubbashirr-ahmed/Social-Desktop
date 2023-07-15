using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Social_Publisher.View;

namespace Social_Publisher
{
    /// <summary>
    /// Interaction logic for OptionsWindow.xaml
    /// </summary>
    public partial class OptionsWindow : Window
    {
        public OptionsWindow()
        {
            InitializeComponent();
        }

        private void bSettings_Click(object sender, RoutedEventArgs e)
        {
            if (isCredsAvailable())
            {
                PlannerWindow main = new PlannerWindow();
                this.Close();
                main.Show();
            }
            
        }

        private bool isCredsAvailable()
        {
            string pageID = Properties.Settings.Default.pageID;
            string access = Properties.Settings.Default.access_token;
            string endpoints = Properties.Settings.Default.awsURL;

            if (access == "empty" || pageID == "empty")
            {
                MessageBox.Show("First go to settings and add credentials!");
                return false;
            }
            if (endpoints == "empty")
            {
                MessageBox.Show("First go to settings and verify AWS credentials!");
                return false;
            }
            return true;
           
        }

        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            SettingsWindow settingsWindow = new SettingsWindow();
            settingsWindow.Show();

        }
    }
}
