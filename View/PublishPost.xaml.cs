using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.IO;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Navigation;
using Facebook;
using Microsoft.Win32;
using Social_Publisher.controller;
using Social_Publisher.models;

namespace Social_Publisher
{
    /// <summary>
    /// Interaction logic for PublishPost.xaml
    /// </summary>
    public partial class PublishPost : Window
    {
        private FacebookClient fb;
        private const string appId = "932070981573770";
        private const string appSecret = "a37cbaee1485b53312afb1905db439d8";
        private const string redirectUri = "https://www.google.com/";
        private const string extendedPermissions = "email,public_profile";
        private string pageIds = "103430702795110"; // Replace with your Facebook page ID
        private FacebookManager facebookManager;
        private List<ImageItem> allImages;
        private string access;
        private string pageID;

        public PublishPost(List<ImageItem> allImages, string access, string pageID)
        {
            InitializeComponent();
            fb = new FacebookClient();
            this.allImages = allImages;
            facebookManager = new FacebookManager();
            this.access = access;
            this.pageID = pageID;
        }

        private void AuthenticateButton_Click(object sender, RoutedEventArgs e)
        {
            schedulePhoto(access, pageID, allImages);

            //var loginUrl = GenerateLoginUrl();
            //webBrowser.Navigate(loginUrl);
        }

        private Uri GenerateLoginUrl()
        {
            var loginParameters = new Dictionary<string, object>
            {
                { "client_id", appId },
                { "redirect_uri", redirectUri },
                { "response_type", "token" },
                { "display", "popup" },
                { "scope", extendedPermissions }
            };

            var loginUrl = fb.GetLoginUrl(loginParameters);
            return loginUrl;
        }
        private void webBrowser_Navigated(object sender, NavigationEventArgs e)
        {
            var responseUrl = e.Uri;
            var accessToken = ExtractAccessTokenFromUrl(responseUrl);
            if (!string.IsNullOrEmpty(accessToken))
            {
                MessageBox.Show("Access token: " + accessToken);
                GetPageAccessTokenAndPost(accessToken);
            }
        }
        private void GetPageAccessTokenAndPost(string userAccessToken)
        {
            try
            {
                dynamic result = fb.Get("oauth/access_token", new
                {
                    client_id = appId,
                    client_secret = appSecret,
                    grant_type = "fb_exchange_token",
                    fb_exchange_token = userAccessToken
                });

                var longLivedAccessToken = result.access_token;

                // Step 2: Get the page access token
                var pageAccessTokenRequest = new FacebookClient(longLivedAccessToken);
                dynamic pageAccessTokenResult = pageAccessTokenRequest.Get($"/{pageIds}?fields=access_token");

                var pageAccessToken = pageAccessTokenResult.access_token;

                //PublishPhoto(pageAccessToken);
                string acc = "EAANPtqZCDHIoBAMZAJuhZCAePO6i0kZADDQrfJYfjhyHOiopprbRgEUdS9E40C3vGzVbWC6WZAlp7xu7tiIOZB0JeOe8gVJhMZB4dvjVlFZCu3FQABBncm9yuDyjlPeXgrN6ZAgyZC5B7dCoVVZBbEJAmpQF9jkWtELPwwhBqFOZACnthGWNFpxRAZANI";
                //schedulePhoto(pageAccessToken, allImages);
            }
            catch (FacebookOAuthException ex)
            {
                MessageBox.Show("Failed to obtain page access token. Error message: " + ex.Message);
            }
        }
        private async Task schedulePhoto(dynamic pageAccessToken, string pageID,  List<ImageItem> allImages)
        {
            foreach (ImageItem imageItem in allImages)
            {
                string path = imageItem.ImageSource;
                byte[] imageData = File.ReadAllBytes(path);
                ByteArrayContent imageContent = new ByteArrayContent(imageData);
                string lices = "";
                string apiUrl = $"https://api.gumroad.com/v2/licenses/verify/product_id=wewewe&license_key={lices}";

                using (HttpClient client = new HttpClient())
                {
                    try
                    {
                        dynamic parameters = new ExpandoObject();
                        parameters.sc = "as";
                        parameters.message = "";
                       
                        MultipartFormDataContent multipartContent = new MultipartFormDataContent();
                        multipartContent.Add(imageContent, "source", Path.GetFileName(path));
                        multipartContent.Add(new StringContent("false"), "published");
                        multipartContent.Add(new StringContent(imageItem.content), "message");
                        multipartContent.Add(new StringContent("1687270332"), "scheduled_publish_time");
                        multipartContent.Add(new StringContent(pageAccessToken), "access_token");
                       

                        HttpResponseMessage response = await client.PostAsync(apiUrl, multipartContent);

                        if (response.IsSuccessStatusCode)
                        {
                            string responseContent = await response.Content.ReadAsStringAsync();
                            MessageBox.Show("Post SCheduled with ID: " + responseContent);
                        }
                        else
                        {
                            MessageBox.Show($"Request failed with status code {response.RequestMessage}");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"An error occurred: {ex.Message}");
                    }
                }
            }
        }



        private void PublishPhoto(string accessToken)
        {
            string photoPath = @"C:\Users\Hp\Downloads\warning.png";
            string message = "another automated";

            try
            {
                FacebookClient fb = new FacebookClient(accessToken);
                dynamic parameters = new ExpandoObject();
                parameters.message = message;
                parameters.source = new FacebookMediaObject
                {
                    ContentType = "image/png",
                    FileName = Path.GetFileName(photoPath)
                }.SetValue(File.ReadAllBytes(photoPath));

                dynamic result = fb.Post("me/photos", parameters);
                MessageBox.Show("Photo published successfully!");
            }
            catch (FacebookOAuthException ex)
            {
                MessageBox.Show("Failed to publish photo. Error: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error publishing photo: " + ex.Message);
            }
        }
        private string? ExtractAccessTokenFromUrl(Uri responseUrl)
        {
            var fragment = responseUrl.Fragment;
            var query = responseUrl.Query;

            if (!string.IsNullOrEmpty(fragment) && fragment.StartsWith("#access_token"))
            {
                var accessTokenRegex = new Regex(@"access_token=([^&]+)");
                var match = accessTokenRegex.Match(fragment);
                if (match.Success)
                    return match.Groups[1].Value;
            }
            else if (!string.IsNullOrEmpty(query))
            {
                var accessTokenRegex = new Regex(@"access_token=([^&]+)");
                var match = accessTokenRegex.Match(query);
                if (match.Success)
                    return match.Groups[1].Value;
            }

            return null;
        }
    }
}
