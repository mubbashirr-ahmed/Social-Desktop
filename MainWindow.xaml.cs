using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Net.Http;
using System.Windows;
using Microsoft.Win32;
using Social_Publisher.models;
using System.Threading.Tasks;
using System.Net.Http.Headers;

namespace Social_Publisher
{
    public partial class MainWindow : Window
    {
        private ObservableCollection<ImageItem> imageItems;

        public MainWindow()
        {

            InitializeComponent();
            imageItems = new ObservableCollection<ImageItem>();
            imageListBox.ItemsSource = imageItems;
        }

        private void LoadImages_Click(object sender, RoutedEventArgs e)
        {

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            openFileDialog.Filter = "Image Files (*.jpg;*.png)|*.jpg;*.png|All Files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                foreach (string filePath in openFileDialog.FileNames)
                {
                    imageItems.Add(new ImageItem { ImageSource = filePath });
                }
            }
        }
        private void bPublish_Click(object sender, RoutedEventArgs e)
        {
            /* List<ImageItem> allImages = new List<ImageItem>(imageItems);
             if (allImages.Count == 0){
                 MessageBox.Show("Select at least 1 Image");
                 return;
             }

             string pageID = Properties.Settings.Default.pageID;
             string access = Properties.Settings.Default.access_token;

             if(access == "empty" || pageID == "empty")
             {
                 MessageBox.Show("You must verify your page before punlishing content in Settings window!");
                 return;
             }

             PublishPost newWindow = new PublishPost(allImages, access, pageID);
             newWindow.Show();*/
            string mes = "This Post was scheduled on 25 June for 26 June to Test scheduling";

            //tw(mes);
            //put();
            //fun();
        }
        private async Task fun()
        {
            string apiUrl = "http://13.232.7.148";  // Replace with your API URL

            // Create a new HttpClient instance
            using (HttpClient client = new HttpClient())
            {

                string pageId = "116178198171553";
                string accessToken = "EAAJKzgVilBABAJTJhcKilvAAZBtlKwitfzGTuaihPIDrIeg4S0d1nwgFzbYIsZCueecWsDUERfDpZBHH94YqB9Y9GeAyOVvFr3HELtrhFHMzZAYiAeRgzbKE3LiMVFA95HkbAIVfvRmFCohxz6Ak3Y7mJagK7V9GZAr93gsF7YowDHZCZCZBRtYWelUNBNYBfcg2KQDE3lYNJQqFiFsHmLs2";
                string message = "Posted From AWS API";
                string timestamp = DateTime.Now.ToString();
                byte[] imageBytes = File.ReadAllBytes(@"C:\Users\Hp\Downloads\ok.jpg"); 


                MultipartFormDataContent formContent = new MultipartFormDataContent();
                formContent.Add(new StringContent(pageId), "page_id");
                formContent.Add(new StringContent(accessToken), "access_token");
                formContent.Add(new StringContent(message), "message");
                formContent.Add(new StringContent(timestamp), "timestamp");
                formContent.Add(new ByteArrayContent(imageBytes), "image", "image.jpg");

                HttpResponseMessage response = await client.PostAsync($"{apiUrl}/post_message", formContent);


                string responseBody = await response.Content.ReadAsStringAsync();

                MessageBox.Show("Response: " + responseBody);
            }

        }
        private async Task tw(string message)
        {
            string pageId = "103430702795110";
            string accessToken = "EAANPtqZCDHIoBAMZAJuhZCAePO6i0kZADDQrfJYfjhyHOiopprbRgEUdS9E40C3vGzVbWC6WZAlp7xu7tiIOZB0JeOe8gVJhMZB4dvjVlFZCu3FQABBncm9yuDyjlPeXgrN6ZAgyZC5B7dCoVVZBbEJAmpQF9jkWtELPwwhBqFOZACnthGWNFpxRAZANI";

            string apiUrl = "http://127.0.0.1:5000/post_message";

            using (var httpClient = new HttpClient())
            {
                // Create the multipart form data content
                using (var formContent = new MultipartFormDataContent())
                {
                    formContent.Add(new StringContent(pageId), "page_id");
                    formContent.Add(new StringContent(accessToken), "access_token");
                    formContent.Add(new StringContent(message), "message");

                    // Read the image file into byte array
                    byte[] imageBytes = File.ReadAllBytes(@"C:\Users\Hp\Downloads\ok.jpg");
                    var imageContent = new ByteArrayContent(imageBytes);
                    formContent.Add(imageContent, "image", "image.jpg");
                    var response = await httpClient.PostAsync(apiUrl, formContent);
                    var responseContent = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Message posted successfully.");
                        MessageBox.Show("API Response: " + responseContent);
                    }
                    else
                    {
                        MessageBox.Show("Error posting message.");
                        MessageBox.Show("API Response: " + responseContent);
                    }
                }
            }
        }

        private async Task put()
        {
            string apiUrl = "http://127.0.0.1:5000/post_image";
            string pageAccessToken = "EAAJKzgVilBABANsykahZBWiCOZA1wnAZBb7KHWzEiTNDRF0H0YpIZBZBRlahzFZB5k0fFjr2FzZADsa3cZBvMT5kqvOSjgMYZC5P5MMx5SpHTEVhTf490hZAJzZAWwZAmECShFfnCdHZBWWqaNQjvbgo5KBDYuJTWMIXZC2gh1S3dp1tZBtYDVlZCwOR5Ng5";
            string imagePath1 = @"C:\Users\Hp\Downloads\chat.png";
            string imagePath2 = @"C:\Users\Hp\Downloads\internet.png";
            string timestamp1 = "2023-06-24 17:55:00";
            string timestamp2 = "2023-06-24 17:55:00";

            using (var httpClient = new HttpClient())
            {
                var formData = new MultipartFormDataContent();

                formData.Add(new StringContent(pageAccessToken), "page_access_token");

                formData.Add(CreateFileContent(imagePath1), "image", Path.GetFileName(imagePath1));
                formData.Add(new StringContent(timestamp1), "timestamp");

                formData.Add(CreateFileContent(imagePath2), "image", Path.GetFileName(imagePath2));
                formData.Add(new StringContent(timestamp2), "timestamp");

                var response = await httpClient.PostAsync(apiUrl, formData);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Images scheduled for publishing!");
                }
                else
                {
                    MessageBox.Show($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                }
            }

        }
        private static StreamContent CreateFileContent(string filePath)
        {
            var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            var streamContent = new StreamContent(fileStream);
            streamContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            return streamContent;
        }

        private static string GetImageTimestamp(string imagePath)
        {
        
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
}

