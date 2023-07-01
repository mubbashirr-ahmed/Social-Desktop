using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Facebook;
using System.Threading.Tasks;
using System.Windows;

namespace Social_Publisher.controller
{
    public class FacebookManager
    {
        public void PostImageToFacebook(string accessToken, string imagePath)
        {
            try
            {
                var fb = new FacebookClient(accessToken);
                dynamic parameters = new System.Dynamic.ExpandoObject();
                parameters.message = "Another automated post";
                parameters.source = new FacebookMediaObject
                {
                    ContentType = "image/jpg",
                    FileName = imagePath
                }.SetValue(System.IO.File.ReadAllBytes(imagePath));

                fb.Post("/me/feed", parameters);
                MessageBox.Show("Image posted successfully!");
            }
            catch (FacebookOAuthException ex)
            {
                MessageBox.Show($"Facebook API Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }
    }

}
