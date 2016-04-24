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
            image.Url = "https://www.petfinder.com/wp-content/uploads/2012/11/99233806-bringing-home-new-cat-632x475.jpg";
            list.Add(image);

            image = new Image();
            image.Url = "https://media-cdn.tripadvisor.com/media/photo-s/01/90/78/c5/sunset-over-dinner-nice.jpg";
            list.Add(image);

            image = new Image();
            image.Url = "https://upload.wikimedia.org/wikipedia/commons/1/1e/Large_Siamese_cat_tosses_a_mouse.jpg";
            list.Add(image);

            image = new Image();
            image.Url = "http://www.hotel-negresco-nice.com/wp-content/uploads/sites/35/2012/06/hotel-negresco-nice-exterieur2-e1343309117394.jpg";
            list.Add(image);

            image = new Image();
            image.Url = "http://animaliaz-life.com/data_images/cat/cat2.jpg";
            list.Add(image);

            image = new Image();
            image.Url = "http://en.aegeanair.com/-/media/images/heroimages/nice_final_cover.jpg?aria-hidden=true&w=1920&h=800";
            list.Add(image);

            image = new Image();
            image.Url = "http://www.zastavki.com/pictures/1600x1200/2010/Animals_Cats_Nice_kittens_023054_.jpg";
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
            image.Url = "https://i.ytimg.com/vi/icqDxNab3Do/maxresdefault.jpg";
            list.Add(image);

            image = new Image();
            image.Url = "http://d2118lkw40i39g.cloudfront.net/wp-content/uploads/2015/06/cats.jpg";
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
            image.Url = "https://www.petfinder.com/wp-content/uploads/2012/11/101438745-cat-conjunctivitis-causes.jpg";
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
            image.Url = "https://i.ytimg.com/vi/gxmHOwWh-i4/maxresdefault.jpg";
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
