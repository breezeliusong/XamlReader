using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace FreeImageUWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
            
        public MainPage()
        {
            this.InitializeComponent();

            string imgStr = "<Image Name=\"image\" xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\" Source=\"ms-appdata:///local/Projects/Project1/1.jpg\"/>";
            object img = XamlReader.Load(imgStr);
            cnvImageArea.Children.Add(img as UIElement);

            string xaml = "<Ellipse Name=\"EllipseAdded\" Width=\"300.5\" Height=\"200\" Fill =\"Red\" xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\"/>";
            object ellipse = XamlReader.Load(xaml);
            //stackPanelRoot is the visual root of a Page in existing XAML markup already loaded by the appmodel
            cnvImageArea.Children.Add(ellipse as UIElement);
            //walk the tree using XLinq result and cast back to a XAML type to set a property on it at runtime
            var result = (from item in cnvImageArea.Children
                          where (item is FrameworkElement)
                          && ((FrameworkElement)item).Name == "EllipseAdded"
                          select item as FrameworkElement).FirstOrDefault();
            ((Ellipse)result).Fill = new SolidColorBrush(Colors.Yellow);
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            //get the Image control
            var Image = (from item in cnvImageArea.Children
                          where (item is FrameworkElement)
                          && ((FrameworkElement)item).Name == "image"
                          select item as FrameworkElement).FirstOrDefault();
            //release the file
            ((Image)Image).Source = null;

            //get the image file
            var file =await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appdata:///local/Projects/Project1/1.jpg"));
            //rename the file
            await file.RenameAsync("2.jpg");
            //delete the file 
            //await file.DeleteAsync();
        }
    }
}
