using Microsoft.Win32;
using Social_Publisher.models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    /// Interaction logic for SelectImagesPage.xaml
    /// </summary>
    public partial class SelectImagesPage : Page
    {
        private ObservableCollection<ImageItem> imageItems;
        public SelectImagesPage()
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
        public List<ImageItem>? postsData()
        {
            List<ImageItem> allImages = new List<ImageItem>(imageItems);
            if (allImages.Count == 0)
            {
                MessageBox.Show("Select at least 1 Image");
                return null;
            }

          
            return allImages;
        }
        public void removeData()
        {
            imageItems = null; 
            imageListBox = null;
        }
    }
}
