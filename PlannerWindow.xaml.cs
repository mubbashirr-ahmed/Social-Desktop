using Social_Publisher.View.Tabs;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace Social_Publisher
{
    /// <summary>
    /// Interaction logic for PlannerWindow.xaml
    /// </summary>
    public partial class PlannerWindow : Window
    {
        SelectImagesPage selectImagesPage;
        public PlannerWindow()
        {
            InitializeComponent();
            selectImagesPage = new SelectImagesPage();
            mainFrame.Content = selectImagesPage;
        }

        private void tab1_MouseUp(object sender, MouseButtonEventArgs e)
        {
            mainFrame.Content = new SelectImagesPage();
        }

        private void tab2_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if(selectImagesPage.postsData() == null)
            {
                return;
            }
            mainFrame.Content = new SchedulerPage(selectImagesPage.postsData(), this);
        }
    }
}
