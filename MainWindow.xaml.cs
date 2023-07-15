using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Net.Http;
using System.Windows;
using Microsoft.Win32;
using Social_Publisher.models;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Collections.Generic;

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
             List<ImageItem> allImages = new List<ImageItem>(imageItems);
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


             fun();
        }
        private async Task fun()
        {
            string apiUrl = "http://13.232.7.148/";
            string pageId = "116178198171553";
            string accessToken = "EAAJKzgVilBABAHS4Vs5ojik3tu0LJdD6asGTaMIznz9Tx8oAE5Sl7O9t2ezMZAA4ToyGP6PtlD9Ph5UYOkVoYWxvjOIzCOcZAkqZCXaTeyZCf3RadNpOIX37UGZAf9k16wf6XZCbvL9mVm1GHJA0x1OETV0bK2vPdKDSnhP95STcvUZBabUC5TNqfjKHVeQETr2lm1idzoJCGZCr4ZAfAqKvD";
            string message = "ok";
            byte[] imageBytes = File.ReadAllBytes(@"C:\Users\Hp\Downloads\vibrate.png");
            string imageBase64 = Convert.ToBase64String(imageBytes);


            using (HttpClient client = new HttpClient())
            {
               
                MultipartFormDataContent formContent = new MultipartFormDataContent();
                
                formContent.Add(new StringContent("123465343"), "timestamp");
                formContent.Add(new StringContent(message), "message");
                formContent.Add(new StringContent(accessToken), "access_token");
                formContent.Add(new StringContent(pageId), "page_id");
                formContent.Add(new StringContent(imageBase64), "image");


                HttpResponseMessage response = await client.PostAsync($"{apiUrl}/upload", formContent);


                string responseBody = await response.Content.ReadAsStringAsync();

                MessageBox.Show("Response: " + responseBody);
            }

        }
        private async Task tw(string message)
        {
            string pageId = "";
            string accessToken = "";

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
                    byte[] imageBytes = File.ReadAllBytes(@"C:\Users\Hp\Downloads\vibrate.png");
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
    }
}

