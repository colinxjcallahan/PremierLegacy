using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Telerik.Windows.Controls;

namespace eRIS
{
    public partial class LoginImagesPane : RadDocumentPane 
    {
        private MainPage pPage = Application.Current.RootVisual as MainPage;
        public LoginImagesPane()
        {
            InitializeComponent();
            imageLeft.Source = new BitmapImage(new Uri("https://eris.premierradiology.com/Images/LoginLeft.jpg", UriKind.Absolute));
            imageRight.Source = new BitmapImage(new Uri("https://eris.premierradiology.com/Images/LoginRight.jpg", UriKind.Absolute));
        }
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            RadWindow.Alert(new DialogParameters { Content = "This isn't the real login screen!" });
        }
        private void radUploadLeft_FileUploadStarting(object sender, FileUploadStartingEventArgs e)
        {
            e.UploadData.FileName = "LoginLeft.jpg";
        }
        private void radUploadRight_FileUploadStarting(object sender, FileUploadStartingEventArgs e)
        {
            e.UploadData.FileName = "LoginRight.jpg";
        }
        private void radUploadLeft_FileUploaded(object sender, FileUploadedEventArgs e)
        {
            imageRight.Source = new BitmapImage(new Uri("https://eris.premierradiology.com/Images/LoginRight.jpg", UriKind.Absolute));
            imageLeft.Source = new BitmapImage(new Uri("https://eris.premierradiology.com/Images/LoginLeft.jpg", UriKind.Absolute));
            radUploadLeft.CancelUpload();
        }
        private void radUploadRight_FileUploaded(object sender, FileUploadedEventArgs e)
        {
            imageLeft.Source = new BitmapImage(new Uri("https://eris.premierradiology.com/Images/LoginLeft.jpg", UriKind.Absolute));
            imageRight.Source = new BitmapImage(new Uri("https://eris.premierradiology.com/Images/LoginRight.jpg", UriKind.Absolute));
            radUploadRight.CancelUpload();
        }
    }
}
