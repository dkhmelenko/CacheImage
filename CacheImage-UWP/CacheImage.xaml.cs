using System;
using System.Diagnostics;
using System.IO;
using System.IO.IsolatedStorage;
using System.Net.NetworkInformation;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.Web.Http;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace CacheImage
{
    public sealed partial class CacheImage : UserControl
    {
        private const int DefaultHeight = 250;
        private const int DefaultWidth = 250;

        #region Dependency Properties

        // URL
        public string Url
        {
            get { return GetValue(UrlProperty) as string; }
            set { SetValue(UrlProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Url.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UrlProperty =
            DependencyProperty.RegisterAttached("Url", typeof(string), typeof(CacheImage), new PropertyMetadata(string.Empty, OnUrlChanged));


        // Placeholder
        public string Placeholder
        {
            get { return GetValue(PlaceholderProperty) as string; }
            set { SetValue(PlaceholderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Placeholder.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PlaceholderProperty =
            DependencyProperty.RegisterAttached("Placeholder", typeof(string), typeof(CacheImage), new PropertyMetadata(string.Empty, OnPlaceholderChanged));


        // DecodePixelHeight
        public int DecodePixelHeight
        {
            get { return (int)GetValue(DecodePixelHeightProperty); }
            set { SetValue(DecodePixelHeightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DecodePixelHeight.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DecodePixelHeightProperty =
            DependencyProperty.RegisterAttached("DecodePixelHeight", typeof(int), typeof(CacheImage), new PropertyMetadata(DefaultHeight, OnDecodePixelHeightChanged));


        // DecodePixelWidth
        public int DecodePixelWidth
        {
            get { return (int)GetValue(DecodePixelWidthProperty); }
            set { SetValue(DecodePixelWidthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DecodePixelWidth.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DecodePixelWidthProperty =
            DependencyProperty.RegisterAttached("DecodePixelWidth", typeof(int), typeof(CacheImage), new PropertyMetadata(DefaultWidth, OnDecodePixelWidthChanged));


        // DecodePixelWIdth for Placeholder
        public int DecodePixelWidthPlaceholder
        {
            get { return (int)GetValue(DecodePixelWidthPlaceholderProperty); }
            set { SetValue(DecodePixelWidthPlaceholderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DecodePixelWidthPlaceholder.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DecodePixelWidthPlaceholderProperty =
            DependencyProperty.RegisterAttached("DecodePixelWidthPlaceholder", typeof(int), typeof(CacheImage), new PropertyMetadata(DefaultWidth, OnDecodePixelWidthPlaceholderChanged));



        // DecodePixelHeight for Placeholder
        public int DecodePixelHeightPlaceholder
        {
            get { return (int)GetValue(DecodePixelHeightPlaceholderProperty); }
            set { SetValue(DecodePixelHeightPlaceholderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DecodePixelHeightPlaceholder.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DecodePixelHeightPlaceholderProperty =
            DependencyProperty.RegisterAttached("DecodePixelHeightPlaceholder", typeof(int), typeof(CacheImage), new PropertyMetadata(DefaultHeight, OnDecodePixelHeightPlaceholderChanged));


        // Stretch
        public Stretch Stretch
        {
            get { return (Stretch)GetValue(StretchProperty); }
            set { SetValue(StretchProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Stretch.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StretchProperty =
            DependencyProperty.RegisterAttached("Stretch", typeof(Stretch), typeof(CacheImage), new PropertyMetadata(Stretch.UniformToFill, OnStretchChanged));


        // Stretch for placeholder
        public Stretch StretchPlaceholder
        {
            get { return (Stretch)GetValue(StretchPlaceholderProperty); }
            set { base.SetValue(StretchPlaceholderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for StretchPlaceholder.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StretchPlaceholderProperty =
            DependencyProperty.RegisterAttached("StretchPlaceholder", typeof(Stretch), typeof(CacheImage), new PropertyMetadata(Stretch.UniformToFill, OnStretchPlaceholderChanged));


        #endregion


        private const string ImageStorageFolder = "ImagesCache";

        public CacheImage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Called when bitmap Url changed
        /// </summary>
        private static async void OnUrlChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            // put the task into the queue
            await CoreWindow.GetForCurrentThread().Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                var control = d as CacheImage;
                if (control != null)
                {
                    control.Visibility = Visibility.Visible;

                    string url = e.NewValue.ToString();
                    Uri imageUri = new Uri(url);
                    if (imageUri.Scheme == "http" || imageUri.Scheme == "https")
                    {
                        var storage = IsolatedStorageFile.GetUserStoreForApplication();
                        if (storage.FileExists(control.GetFileNameInIsolatedStorage(imageUri)))
                        {
                            control.LoadFromLocalStorage(imageUri, control.bitmapImage);
                        }
                        else
                        {
                            if (NetworkInterface.GetIsNetworkAvailable())
                            {
                                control.LoadFromWebAndCache(imageUri, control.bitmapImage);
                            }
                        }
                    }
                    else
                    {
                        control.bitmapImage.UriSource = imageUri;
                    }
                }
            });
        }

        /// <summary>
        /// Called when placeholder changed
        /// </summary>
        private static async void OnPlaceholderChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            await CoreWindow.GetForCurrentThread().Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
             {
                 var control = d as CacheImage;
                 if (control != null)
                 {
                     var placeholderPath = e.NewValue.ToString();
                     control.placeholderImage.UriSource = new Uri(placeholderPath, UriKind.RelativeOrAbsolute);
                 }
             });

        }

        /// <summary>
        /// Called when DecodePixelHeight changed
        /// </summary>
        private static void OnDecodePixelHeightChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as CacheImage;
            var newValue = e.NewValue as int?;
            if (newValue.HasValue && control != null)
            {
                control.bitmapImage.DecodePixelHeight = newValue.Value;
            }
        }

        /// <summary>
        /// Called when DecodePixelWidth changed
        /// </summary>
        private static void OnDecodePixelWidthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as CacheImage;
            var newValue = e.NewValue as int?;
            if (newValue.HasValue && control != null)
            {
                control.bitmapImage.DecodePixelWidth = newValue.Value;
            }
        }

        /// <summary>
        /// Called when DecodePixelHeightPlaceholder changed
        /// </summary>
        private static void OnDecodePixelHeightPlaceholderChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as CacheImage;
            var newValue = e.NewValue as int?;
            if (newValue.HasValue && control != null)
            {
                control.placeholderImage.DecodePixelHeight = newValue.Value;
            }
        }

        /// <summary>
        /// Called when DecodePixelWidthPlaceholder changed
        /// </summary>
        private static void OnDecodePixelWidthPlaceholderChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as CacheImage;
            var newValue = e.NewValue as int?;
            if (newValue.HasValue && control != null)
            {
                control.placeholderImage.DecodePixelWidth = newValue.Value;
            }
        }

        /// <summary>
        /// Called when image property Stretch changed
        /// </summary>
        private static void OnStretchChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as CacheImage;
            var newValue = e.NewValue as Stretch?;
            if (newValue.HasValue && control != null)
            {
                control.image.Stretch = newValue.Value;
            }
        }

        /// <summary>
        /// Called when placeholder property Stretch changed
        /// </summary>
        private static void OnStretchPlaceholderChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as CacheImage;
            var newValue = e.NewValue as Stretch?;
            if (newValue.HasValue && control != null)
            {
                control.placeholder.Stretch = newValue.Value;
            }
        }

        /// <summary>
        /// Loads an image from web and caches it
        /// </summary>
        /// <param name="imageUri">Image URI</param>
        /// <param name="bitmap">Bitmap for loaded image</param>
        private async void LoadFromWebAndCache(Uri imageUri, BitmapImage bitmap)
        {
            using (HttpClient client = new HttpClient())
            {
                using (var response = await client.GetAsync(imageUri))
                {
                    response.EnsureSuccessStatusCode();

                    using (var inputStream = await response.Content.ReadAsInputStreamAsync())
                    {
                        // save image
                        WriteToIsolatedStorage(inputStream.AsStreamForRead(), GetFileNameInIsolatedStorage(imageUri));

                        // apply image 
                        var randomAccessStream = inputStream.AsStreamForRead().AsRandomAccessStream();
                        await bitmap.SetSourceAsync(randomAccessStream);


                        HidePlaceholder();
                    }
                }
            }
        }

        /// <summary>
        /// Hides placeholder
        /// </summary>
        private void HidePlaceholder()
        {
            placeholder.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// Loads an image from the local storage
        /// </summary>
        /// <param name="imageUri">Image URI</param>
        /// <param name="bitmap">Bitmap image</param>
        /// <returns>Loaded image</returns>
        private async void LoadFromLocalStorage(Uri imageUri, BitmapImage bitmap)
        {
            string isolatedStoragePath = GetFileNameInIsolatedStorage(imageUri);
            var storage = IsolatedStorageFile.GetUserStoreForApplication();
            using (var sourceFile = storage.OpenFile(isolatedStoragePath, FileMode.Open, FileAccess.Read))
            {
                await bitmap.SetSourceAsync(sourceFile.AsRandomAccessStream());
            }

            HidePlaceholder();
        }

        /// <summary>
        /// Writes the stream data to isolated storage
        /// </summary>
        /// <param name="inputStream">Input stream</param>
        /// <param name="fileName">File name</param>
        private void WriteToIsolatedStorage(Stream inputStream, string fileName)
        {
            var storage = IsolatedStorageFile.GetUserStoreForApplication();
            IsolatedStorageFileStream outputStream = null;
            try
            {
                if (!storage.DirectoryExists(ImageStorageFolder))
                {
                    storage.CreateDirectory(ImageStorageFolder);
                }
                if (storage.FileExists(fileName))
                {
                    storage.DeleteFile(fileName);
                }
                outputStream = storage.CreateFile(fileName);
                byte[] buffer = new byte[32768];
                int read;
                while ((read = inputStream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    outputStream.Write(buffer, 0, read);
                }
            }
            catch (Exception e)
            {
                // ignore exceptions
                Debug.WriteLine(e);
            }
            finally
            {
                if (outputStream != null)
                {
                    outputStream.Flush();
                    outputStream.Dispose();
                }
            }
        }

        /// <summary>
        /// Gets the file name in isolated storage for the Uri specified. This name should be used to search in the isolated storage.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <returns></returns>
        public string GetFileNameInIsolatedStorage(Uri uri)
        {
            return ImageStorageFolder + "\\" + uri.AbsoluteUri.GetHashCode() + ".img";
        }
    }
}
