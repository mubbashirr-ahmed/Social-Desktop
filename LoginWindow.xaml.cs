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
using Newtonsoft.Json;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;
using Social_Publisher.models;

namespace Social_Publisher
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();          
        }
        private async void bLogin_Click(object sender, RoutedEventArgs e)
        {
            string email = tEmail.Text;
            string key = tKey.Text;
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(key))
            {
                MessageBox.Show("Fill all details!");
                return;
            }
            bLogin.IsEnabled = false;
            await verify(email, key);
        }
        private async Task verify(string email, string licenseKey)
        {
            string apiUrl = "https://api.gumroad.com/v2/licenses/verify";
            string productId = "yUW5zHj_A-KaS9UYxsCDjA==";
            using (HttpClient client = new HttpClient())
            {
                string requestUrl = $"{apiUrl}?product_id={productId}&license_key={licenseKey}";

                try
                {
                    HttpResponseMessage response = await client.PostAsync(requestUrl, null);
                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();

                        var apiResponse = JsonConvert.DeserializeObject<GumRoadResponse>(responseBody);


                        if (apiResponse.purchase.email == email.Trim())
                        {
                            Properties.Settings.Default.loggedin = "ok";
                            Properties.Settings.Default.Save();
                            OptionsWindow mainWindow = new OptionsWindow();
                            this.Close();
                            mainWindow.Show();
                            return;
                        }
                        MessageBox.Show("Email or License Key is Invalid!");
                        bLogin.IsEnabled = true;
                        return;
                    }
                    else
                    {
                        MessageBox.Show($"Request failed with status code {response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}");
                }
                bLogin.IsEnabled = true;
            }
        }
    }
}
