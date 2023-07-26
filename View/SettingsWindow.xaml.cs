using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Social_Publisher.View
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        OptionsWindow optionsWindow;
        public SettingsWindow(OptionsWindow optionsWindow)
        {
            InitializeComponent();
            this.optionsWindow = optionsWindow;
            checkPrefs();
        }

        private void checkPrefs()
        {
            string pageID = Properties.Settings.Default.pageID;
            string access = Properties.Settings.Default.access_token;
            string endpoints = Properties.Settings.Default.awsURL;
            string twVerify = Properties.Settings.Default.tCred;
            if (access != "empty" || pageID != "empty")
            {
                access_token.Text = access;
                tPageID.Text = pageID;
            }
            if (endpoints != "empty")
            {
                endpoint.Text = endpoints;
            }
            if(twVerify != "empty")
            { 
                tapiKey.Text = Properties.Settings.Default.tApi;
                tapiKeySecret.Text = Properties.Settings.Default.tAS;
                taccessTokenTwitter.Text =  Properties.Settings.Default.tCK;
                taccessTokenSecretTwitter.Text = Properties.Settings.Default.tCKS;
                tuserName.Text = Properties.Settings.Default.tUser;
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
            LoginWindow loginWindow = new LoginWindow();
            this.Close();
            loginWindow.Show();
            
        }

        private void bTWSave_Click(object sender, RoutedEventArgs e)
        {
            string apikey = tapiKey.Text.Trim();
            string apisecret = tapiKeySecret.Text.Trim();
            string consumerkey = taccessTokenTwitter.Text.Trim();
            string consumersecret = taccessTokenSecretTwitter.Text.Trim();
            string username = tuserName.Text.Trim();
            if (string.IsNullOrEmpty(apikey) ||
                string.IsNullOrEmpty(apisecret) ||
                string.IsNullOrEmpty(consumerkey) ||
                string.IsNullOrEmpty(consumersecret) ||
                string.IsNullOrEmpty(username))
            {
                MessageBox.Show("All fields are required");
                return;
            }

            string apiURl = Properties.Settings.Default.awsURL;
            if (apiURl == "empty")
            {
                MessageBox.Show("AWS endpoint not provided!");
                return;
            }

            bTWSave.IsEnabled = false; 
            verifyTwitter(apikey, apisecret, consumerkey, consumersecret, username, apiURl);
                   
        }

        private async Task createTwitterTable(string apikey, string apisecret, string consumerkey, string consumersecret, string username, string apiURl)
        {
            string url = $"{apiURl}store_tw_credentials";
            using (HttpClient client = new HttpClient())
            {
                MultipartFormDataContent formContent = new MultipartFormDataContent();
                formContent.Add(new StringContent(apikey), "consumer_key");
                formContent.Add(new StringContent(apisecret), "consumer_secret");
                formContent.Add(new StringContent(consumerkey), "access_token");
                formContent.Add(new StringContent(consumersecret), "access_token_secret");

                HttpResponseMessage response = await client.PostAsync(url, formContent);
                if (response.IsSuccessStatusCode)
                {
                    return;
                }
            }
        }

        private async Task verifyTwitter(string apikey, string apisecret, string consumerkey, string consumersecret, string username, string apiURL)
        {
            string url = $"{apiURL}verify_twitter";
            using (HttpClient client = new HttpClient())
            {
                MultipartFormDataContent formContent = new MultipartFormDataContent();
                formContent.Add(new StringContent(apikey), "apikey");
                formContent.Add(new StringContent(apisecret), "apikeysecret");
                formContent.Add(new StringContent(consumerkey), "consumerkey");
                formContent.Add(new StringContent(consumersecret), "consumerkeysecret");
                formContent.Add(new StringContent(username), "username");

                HttpResponseMessage response = await client.PostAsync(url, formContent);
                if (response.IsSuccessStatusCode)
                {
                    await createTwitterTable(apikey, apisecret, consumerkey, consumersecret, username, apiURL);
                    MessageBox.Show("Data Verified and saved!");
                    saveCreds(apikey, apisecret, consumerkey, consumersecret, username, apiURL);

                    bTWSave.IsEnabled = true;
                    return;
                }
                MessageBox.Show("Error: " + response);
                bTWSave.IsEnabled = true;
                return;
            }
        }

        private void saveCreds(string apikey, string apisecret, string consumerkey, string consumersecret, string username, string apiURL)
        {
            Properties.Settings.Default.tCred = "available";
            Properties.Settings.Default.tApi = apikey;
            Properties.Settings.Default.tAS = apisecret;
            Properties.Settings.Default.tCK = consumerkey;
            Properties.Settings.Default.tCKS = consumersecret;
            Properties.Settings.Default.tUser = username;
            Properties.Settings.Default.Save();
        }
    }
}
