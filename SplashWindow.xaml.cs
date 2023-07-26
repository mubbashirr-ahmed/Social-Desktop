using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Social_Publisher
{
    /// <summary>
    /// Interaction logic for SplashWindow.xaml
    /// </summary>
    public partial class SplashWindow : Window
    {
        OptionsWindow optionsWindow;
        public SplashWindow(OptionsWindow optionsWindow)
        {
            InitializeComponent();
            navigateToScreen();

        }

        private void navigateToScreen()
        {

            string isLoggedIn = CheckIfUserIsLoggedIn();
            Thread.Sleep(2000);
            if (isLoggedIn=="ok")
            {
                var optionsWindow = new OptionsWindow();
                this.Close();
                optionsWindow.Show();
            }
            else
            {
                var loginWindow = new LoginWindow();
                this.Close();
                loginWindow.Show();
            }
        }

        private string CheckIfUserIsLoggedIn()
        {
            string isLoggedIn = Properties.Settings.Default.loggedin;
            return isLoggedIn;
        }
    }
}
