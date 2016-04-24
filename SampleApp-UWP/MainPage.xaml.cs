using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace SampleApp_UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            Images = new ObservableCollection<Image>(GenerateData());

            DataContext = this;

            
        }

        public ObservableCollection<Image> Images { get; set; }

        private List<Image> GenerateData()
        {
            var list = new List<Image>();

            var image = new Image();
            image.Url = "https://www.newton.ac.uk/files/covers/968361.jpg";
            list.Add(image);

            image = new Image();
            image.Url = "https://www.moooi.com/sites/default/files/styles/large/public/product-images/random_detail.jpg?itok=ErJveZTY";
            list.Add(image);

            image = new Image();
            image.Url = "https://www.random.org/analysis/randbitmap-rdo.png";
            list.Add(image);

            image = new Image();
            image.Url = "http://e2ua.com/data/wallpapers/22/WDF_701465.jpg";
            list.Add(image);

            image = new Image();
            image.Url = "http://www.randomwebsite.com/images/head.jpg";
            list.Add(image);

            image = new Image();
            image.Url = "http://www.hotel-negresco-nice.com/wp-content/uploads/sites/35/2012/06/hotel-negresco-nice-exterieur2-e1343309117394.jpg";
            list.Add(image);

            image = new Image();
            image.Url = "http://e2ua.com/data/wallpapers/110/WDF_1528766.jpg";
            list.Add(image);

            image = new Image();
            image.Url = "http://en.aegeanair.com/-/media/images/heroimages/nice_final_cover.jpg?aria-hidden=true&w=1920&h=800";
            list.Add(image);

            image = new Image();
            image.Url = "http://e2ua.com/data/wallpapers/70/WDF_1153959.jpg";
            list.Add(image);

            image = new Image();
            image.Url = "http://www.tunliweb.no/Bilder_SM/_album_nice/a1b_nice.JPG";
            list.Add(image);

            image = new Image();
            image.Url = "https://www.europcar.com/files/live/sites/seo/files/contributed/images/location%20images/corporate%20countries/France%20resized/nice-1160x400.jpg";
            list.Add(image);

            image = new Image();
            image.Url = "https://www.bhmpics.com/walls/holidays_top_nice-other.jpg";
            list.Add(image);

            image = new Image();
            image.Url = "http://nice.boscolohotels.com/sites/nice.boscolo-hotels.ogilvy.it/files/promo-nice-lifestyle.jpg";
            list.Add(image);

            image = new Image();
            image.Url = "https://www.b-europe.com/~/media/ImagesNew/Bestemmingen/Nice/nice_500x338.ashx";
            list.Add(image);

            image = new Image();
            image.Url = "http://media.tumblr.com/tumblr_m8g1ukyeFd1qhsyf1.jpg";
            list.Add(image);

            image = new Image();
            image.Url = "http://exp.cdn-hotels.com/hotels/1000000/30000/28700/28621/28621_89_z.jpg";
            list.Add(image);

            image = new Image();
            image.Url = "http://e2ua.com/data/wallpapers/11/WDF_595834.jpg";
            list.Add(image);

            image = new Image();
            image.Url = "http://7-themes.com/data_images/out/72/7016712-nice-love-wallpaper-27705.jpg";
            list.Add(image);

            image = new Image();
            image.Url = "http://images.trvl-media.com/media/content/shared/images/travelguides/Nice-and-vicinity-180102-smallTabletRetina.jpg";
            list.Add(image);

            image = new Image();
            image.Url = "http://en.nicetourisme.com/resources/sliders/photos/99/thumbs/9_0_56a61a2233e02_9a807bcc318477489cbebcd3077c01fc7d88b00c.jpg";
            list.Add(image);

            image = new Image();
            image.Url = "http://www.djoybeat.com/wp-content/uploads/2013/01/nice.png";
            list.Add(image);

            image = new Image();
            image.Url = "http://feelgrafix.com/data_images/out/12/861226-nice-wallpaper.jpg";
            list.Add(image);

            image = new Image();
            image.Url = "http://www.planwallpaper.com/static/images/2ba7dbaa96e79e4c81dd7808706d2bb7_large.jpg";
            list.Add(image);

            image = new Image();
            image.Url = "http://www.wallpapereast.com/static/images/spring-in-nature-wide-wallpaper-603794.jpg";
            list.Add(image);

            image = new Image();
            image.Url = "http://www.planwallpaper.com/static/images/hd_nature_wallpaper.jpg";
            list.Add(image);

            image = new Image();
            image.Url = "http://pic.1fotonin.com/data/wallpapers/99/WDF_1427414.jpg";
            list.Add(image);

            image = new Image();
            image.Url = "http://cdn.wonderfulengineering.com/wp-content/uploads/2016/01/nature-wallpapers-26.jpg";
            list.Add(image);

            image = new Image();
            image.Url = "http://feelgrafix.com/data_images/out/24/944669-nature.jpg";
            list.Add(image);

            image = new Image();
            image.Url = "http://www.natureasia.com/common/img/splash/thailand.jpg";
            list.Add(image);

            image = new Image();
            image.Url = "http://www.wallpapereast.com/static/images/Wallpaper-Nature-8B71.jpg";
            list.Add(image);

            return list;
        }
    }

    public class Image
    {
        public string Url { get; set; }
    }
}
