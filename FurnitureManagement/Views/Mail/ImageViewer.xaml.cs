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

namespace FurnitureManagement.Views.Mail
{
    /// <summary>
    /// Interaction logic for ImageViewer.xaml
    /// </summary>
    public partial class ImageViewer : Window
    {
        public ImageViewer(string path)
        {
            InitializeComponent();
            if (path == "")
                return;
            var uriSource = new Uri(path, UriKind.Absolute);
            ImageControl.Source = new BitmapImage(uriSource);
        }

        private void SV_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            HRect.Width = SV.ViewportWidth / Zoom.Value;
            HRect.Height = SV.ViewportHeight / Zoom.Value;
            HRect.SetValue(Canvas.LeftProperty, SV.ContentHorizontalOffset / Zoom.Value);
            HRect.SetValue(Canvas.TopProperty, SV.ContentVerticalOffset / Zoom.Value);
        }

        private void Image_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Point P = e.GetPosition(Canv);
                SV.ScrollToHorizontalOffset((P.X * Zoom.Value) - HRect.Width / 2);
                SV.ScrollToVerticalOffset((P.Y * Zoom.Value) - HRect.Height / 2);
            }
        }  
    }
}
