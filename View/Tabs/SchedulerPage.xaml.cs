using Social_Publisher.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net;

namespace Social_Publisher.View.Tabs
{
    /// <summary>
    /// Interaction logic for SchedulerPage.xaml
    /// </summary>
    public partial class SchedulerPage : Page
    {
        List<ImageItem> allImages;
        private List<long> unixTimestamps;
        PlannerWindow planner;
        public SchedulerPage(List<ImageItem> allImages, PlannerWindow planner)
        {
            InitializeComponent();
            checkPrefs();
            this.allImages = allImages;
            unixTimestamps = new List<long>();
            this.planner = planner;
        }

        private void checkPrefs()
        {
            string fbCred = Properties.Settings.Default.access_token;
            string twCred = Properties.Settings.Default.tCred;
            if(fbCred == "empty")
            {
                cFacebook.Visibility = Visibility.Hidden;
            }
            if(twCred == "empty")
            {
                cTwitter.Visibility = Visibility.Hidden;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DateTime startDate = startDatePicker.SelectedDate.GetValueOrDefault();
            DateTime endDate = endDatePicker.SelectedDate.GetValueOrDefault();

            if (startDate < DateTime.Today)
            {
                MessageBox.Show("Start date cannot be before the current date.");
                return;
            }
            else if (startDate >= endDate)
            {
                MessageBox.Show("End date must be larger than the start date.");
                return;
            }

            int numberOfDates = allImages.Count;
            Random random = new Random();

            for (int i = 0; i < numberOfDates; i++)
            {
                TimeSpan timeSpan = endDate - startDate;
                int totalDays = timeSpan.Days;
                int randomDays = random.Next(totalDays);

                DateTime randomDate = startDate.AddDays(randomDays);
                DateTimeOffset dateTimeOffset = new DateTimeOffset(randomDate);
                long unixTimestamp = dateTimeOffset.ToUnixTimeSeconds();
                unixTimestamps.Add(unixTimestamp);
            }
            
            startPublish(allImages, unixTimestamps);
                       
        }

        private async Task startPublish(List<ImageItem> allImages, List<long> unixTimestamps)
        {
            bPublish.IsEnabled = false;

            if (cFacebook.IsChecked == true)
            {
               await publishFBPosts(allImages, unixTimestamps);
            }
            if (cTwitter.IsChecked == true)
            {
                await publishTWPosts(allImages, unixTimestamps);
            }

            MessageBox.Show("Your Posts have been Scheduled");
            planner.Close();

        }

        private async Task publishTWPosts(List<ImageItem> allImages, List<long> unixTimestamps)
        {
            progressTW.Visibility = Visibility.Visible;
            string endpoint = Properties.Settings.Default.awsURL;
            int cnt = allImages.Count;
            for (int i = 0; i < cnt; i++)
            {
                ImageItem item = allImages[i];
                string content = item.twContent;
                if (string.IsNullOrEmpty(content))
                {
                    content = "";
                }
                await postToTWAWS(item.ImageSource, content, unixTimestamps[i], endpoint);
                progressTW.Content = $"Scheduled {i + 1} out of {allImages.Count} twitter posts";
            }
        }

        private async Task postToTWAWS(string imageSource, string content, long timestamp, string endpoint)
        {
            byte[] imageBytes = File.ReadAllBytes(imageSource);
            string imageBase64 = Convert.ToBase64String(imageBytes);
            using (HttpClient client = new HttpClient())
            {

                MultipartFormDataContent formContent = new MultipartFormDataContent();

                formContent.Add(new StringContent(timestamp.ToString()), "timestamp");
                formContent.Add(new StringContent(content), "message");
                formContent.Add(new StringContent(imageBase64), "image");

                HttpResponseMessage response = await client.PostAsync($"{endpoint}/tw_upload", formContent);
                string responseBody = await response.Content.ReadAsStringAsync();
            }
        }

        private async Task publishFBPosts(List<ImageItem> allPosts, List<long> allUnixTimestamps)
        {
            string pageID = Properties.Settings.Default.pageID;
            string access = Properties.Settings.Default.access_token;
            string endpoint = Properties.Settings.Default.awsURL;
            progressFB.Visibility = Visibility.Visible;
            
            for (int i = 0; i < allPosts.Count; i++)
            {
                string content = allPosts[i].fbContent;
                if (string.IsNullOrEmpty(content))
                {
                    content = "";
                }
                await postToFBAWS(allPosts[i].ImageSource, content, allUnixTimestamps[i], access, pageID, endpoint);
                progressFB.Content = $"Scheduled {i + 1} out of {allImages.Count} facebook posts";
            }
            
            
        }
        private async Task postToFBAWS(string path, string message, long timestamp, string accessToken, string pageId, string apiUrl)
        {   
            byte[] imageBytes = File.ReadAllBytes(path);
            string imageBase64 = Convert.ToBase64String(imageBytes);
            using (HttpClient client = new HttpClient())
            {

                MultipartFormDataContent formContent = new MultipartFormDataContent();

                formContent.Add(new StringContent(timestamp.ToString()), "timestamp");
                formContent.Add(new StringContent(message), "message");
                formContent.Add(new StringContent(accessToken), "access_token");
                formContent.Add(new StringContent(pageId), "page_id");
                formContent.Add(new StringContent(imageBase64), "image");

                HttpResponseMessage response = await client.PostAsync($"{apiUrl}/upload", formContent);
                string responseBody = await response.Content.ReadAsStringAsync();
                //MessageBox.Show(responseBody);
            }

        }
    }
}
