using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

namespace Social_Publisher.View
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        public SettingsWindow()
        {
            InitializeComponent();
            checkPrefs();
        }

        private void checkPrefs()
        {
            string pageID = Properties.Settings.Default.pageID;
            string access = Properties.Settings.Default.access_token;
            string aID = Properties.Settings.Default.appID;

            if (access != "empty" || pageID != "empty")
            {
                access_token.Text = access;
                tPageID.Text = pageID;
                tAppID.Text = aID;
                return;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string accessKey = access_token.Text.ToString();
            string appID = tAppID.Text.ToString();
            string pageID = tPageID.Text.ToString();
            if(String.IsNullOrEmpty(accessKey) || String.IsNullOrEmpty(appID) || String.IsNullOrEmpty(pageID) )
            {
                MessageBox.Show("Fill All Details");
                return;
            }
            callApi(appID, pageID, accessKey);
        }

        private async Task callApi(string appID, string pageID, string access_token)
        {
            string url = $"https://graph.facebook.com/{pageID}?access_token={access_token}";

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string responseData = await response.Content.ReadAsStringAsync();
                    if (responseData.Contains(pageID))
                    {
                        MessageBox.Show("Account Verified!");
                        Properties.Settings.Default.access_token = access_token;
                        Properties.Settings.Default.pageID = pageID;
                        Properties.Settings.Default.appID = appID;
                        Properties.Settings.Default.Save();

                        return;
                    }
                    MessageBox.Show("Invalid Credentials");
                    return;
                }
                else
                {
                    MessageBox.Show("Request failed with status code: " + response.StatusCode);
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.Reset();
        }
    }
}
