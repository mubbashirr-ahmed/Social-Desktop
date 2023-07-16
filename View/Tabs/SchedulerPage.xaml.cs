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
            this.allImages = allImages;
            unixTimestamps = new List<long>();
            this.planner = planner;
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

            publishPosts(allImages, unixTimestamps);
        }

        private async void publishPosts(List<ImageItem> allPosts, List<long> allUnixTimestamps)
        {
            string pageID = Properties.Settings.Default.pageID;
            string access = Properties.Settings.Default.access_token;
            string endpoint = Properties.Settings.Default.awsURL;
            lMessage.Visibility = Visibility.Visible;
            progress.Visibility = Visibility.Visible;
            
            for (int i = 0; i < allPosts.Count; i++)
            {
                string content = allPosts[i].content;
                if (string.IsNullOrEmpty(content))
                {
                    content = "";
                }
                await postToAWS(allPosts[i].ImageSource, content, allUnixTimestamps[i], access, pageID, endpoint);
                progress.Content = $"Published {i + 1} out of {allImages.Count} posts";
            }
            MessageBox.Show("Your Posts have been Scheduled");
            planner.Close();
            
        }
        private async Task postToAWS(string path, string message, long timestamp, string accessToken, string pageId, string apiUrl)
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
