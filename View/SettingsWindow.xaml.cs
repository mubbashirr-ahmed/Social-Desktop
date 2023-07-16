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
            string endpoints = Properties.Settings.Default.awsURL;

            if (access != "empty" || pageID != "empty")
            {
                access_token.Text = access;
                tPageID.Text = pageID;
            }
            if (endpoints != "empty")
            {
                endpoint.Text = endpoints;
            }
        }

        private async void fbSave_Click(object sender, RoutedEventArgs e)
        {
            string accessKey = access_token.Text.ToString();
            string pageID = tPageID.Text.ToString();
            if(string.IsNullOrEmpty(accessKey) || string.IsNullOrEmpty(pageID) )
            {
                MessageBox.Show("Fill All Details");
                return;
            }
            string apiURl = Properties.Settings.Default.awsURL;
            if (apiURl == "empty")
            {
                MessageBox.Show("AWS endpoint not provided!");
                return;
            }
            fbSave.IsEnabled = false;
            await callApi(apiURl, pageID, accessKey);
            fbSave.IsEnabled = true;
        }

        private async Task callApi(string endPoint, string pageID, string access_token)
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
                        bool isCreated = await createCredTable(endPoint, access_token, pageID);
                        Properties.Settings.Default.access_token = access_token;
                        Properties.Settings.Default.pageID = pageID;
                        Properties.Settings.Default.Save();
                        MessageBox.Show("Account Verified!");
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
        private async Task<bool> createCredTable(string awsendpoint, string access_token, string pageID)
        {
            string url = $"{awsendpoint}store_credentials";
            using (HttpClient client = new HttpClient())
            {
                MultipartFormDataContent formContent = new MultipartFormDataContent();
                formContent.Add(new StringContent(access_token), "access_token");
                formContent.Add(new StringContent(pageID), "page_id");

                HttpResponseMessage response = await client.PostAsync(url, formContent);
                if (response.IsSuccessStatusCode)
                {
                    string responseData = await response.Content.ReadAsStringAsync();
                    if (responseData.Contains("created"))
                    {
                        return true;
                    }
                    return false;
                }
                return false;
            }
        }




        private void bAWSVerify_Click(object sender, RoutedEventArgs e)
        {
            string awsendpoint = endpoint.Text.ToString().Trim();
            if (string.IsNullOrEmpty(awsendpoint))
            {
                MessageBox.Show("URL is required");
                return;
            }
            verifyAWSendPoint(awsendpoint);
            
        }
        private async Task<bool> createTable(string awsendpoint)
        {
            string url = $"{awsendpoint}create_table";

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string responseData = await response.Content.ReadAsStringAsync();
                    if (responseData.Contains("created "))
                    {
                        return true;
                    }
                    
                    return false;
                }
            }
            return false;
        }
        private async Task verifyAWSendPoint(string awsendpoint)
        {
            string url = $"{awsendpoint}verify";

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string responseData = await response.Content.ReadAsStringAsync();
                    if (responseData.Contains("Working"))
                    {
                        bool res = await createTable(awsendpoint);
                        if (res)
                        {
                            Properties.Settings.Default.awsURL = awsendpoint;
                            Properties.Settings.Default.Save();
                            MessageBox.Show("URL Verified!");
                            return;
                        }
                        MessageBox.Show("Database Doesnot exist!");
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
        private void bLogout_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.Reset();
            access_token.Text = "";
            tPageID.Text = "";
            endpoint.Text = "";
        }
    }
}
