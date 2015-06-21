using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Media.Imaging;
using System.IO.IsolatedStorage;
using System.IO;
using Microsoft.Phone.Net.NetworkInformation;
using System.Diagnostics;

namespace KhmelenkoLab
{
    public partial class CacheImage : UserControl
    {

        #region Dependency Properties

        // URL
        public static string GetUrl(DependencyObject obj)
        {
            return (string)obj.GetValue(UrlProperty);
        }

        public static void SetUrl(DependencyObject obj, string value)
        {
            obj.SetValue(UrlProperty, value);
        }

        // Using a DependencyProperty as the backing store for Url.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UrlProperty =
            DependencyProperty.RegisterAttached("Url", typeof(string), typeof(CacheImage), new PropertyMetadata(OnUrlChanged));


        // Placeholder
        public static string GetPlaceholder(DependencyObject obj)
        {
            return (string)obj.GetValue(PlaceholderProperty);
        }

        public static void SetPlaceholder(DependencyObject obj, string value)
        {
            obj.SetValue(PlaceholderProperty, value);
        }

        // Using a DependencyProperty as the backing store for Placeholder.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PlaceholderProperty =
            DependencyProperty.RegisterAttached("Placeholder", typeof(string), typeof(CacheImage), new PropertyMetadata(OnPlaceholderChanged));


        // DecodePixelHeight
        public static int GetDecodePixelHeight(DependencyObject obj)
        {
            return (int)obj.GetValue(DecodePixelHeightProperty);
        }

        public static void SetDecodePixelHeight(DependencyObject obj, int value)
        {
            obj.SetValue(DecodePixelHeightProperty, value);
        }

        // Using a DependencyProperty as the backing store for DecodePixelHeight.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DecodePixelHeightProperty =
            DependencyProperty.RegisterAttached("DecodePixelHeight", typeof(int), typeof(CacheImage), new PropertyMetadata(OnDecodePixelHeightChanged));


        // DecodePixelWidth
        public static int GetDecodePixelWidth(DependencyObject obj)
        {
            return (int)obj.GetValue(DecodePixelWidthProperty);
        }

        public static void SetDecodePixelWidth(DependencyObject obj, int value)
        {
            obj.SetValue(DecodePixelWidthProperty, value);
        }

        // Using a DependencyProperty as the backing store for DecodePixelWidth.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DecodePixelWidthProperty =
            DependencyProperty.RegisterAttached("DecodePixelWidth", typeof(int), typeof(CacheImage), new PropertyMetadata(OnDecodePixelWidthChanged));


        #endregion


        private const string imageStorageFolder = "ImagesCache";

        private bool _imageLoaded = false;

        public CacheImage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Called when bitmap Url changed
        /// </summary>
        private static void OnUrlChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            // put the task into the queue
            Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    var control = d as CacheImage;

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

                });
        }

        /// <summary>
        /// Called when placeholder changed
        /// </summary>
        private static void OnPlaceholderChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as CacheImage;
            var placeholderPath = e.NewValue.ToString();
            if (!control._imageLoaded)
            {
                control.bitmapImage.UriSource = new Uri(placeholderPath, UriKind.RelativeOrAbsolute);
            }
        }

        /// <summary>
        /// Called when DecodePixelHeight changed
        /// </summary>
        private static void OnDecodePixelHeightChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as CacheImage;
            var newValue = e.NewValue as int?;
            if (newValue.HasValue)
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
            if (newValue.HasValue)
            {
                control.bitmapImage.DecodePixelWidth = newValue.Value;
            }
        }

        /// <summary>
        /// Loads an image from web and caches it
        /// </summary>
        /// <param name="imageUri">Image URI</param>
        /// <param name="bitmap">Bitmap for loaded image</param>
        private void LoadFromWebAndCache(Uri imageUri, BitmapImage bitmap)
        {
            WebClient webClient = new WebClient();
            webClient.OpenReadCompleted += (o, e) =>
            {
                if (e.Error != null || e.Cancelled)
                    return;

                WriteToIsolatedStorage(e.Result, GetFileNameInIsolatedStorage(imageUri));
                bitmap.SetSource(e.Result);
                e.Result.Close();

                _imageLoaded = true;
            };
            webClient.OpenReadAsync(imageUri);
        }

        /// <summary>
        /// Loads an image from the local storage
        /// </summary>
        /// <param name="imageUri">Image URI</param>
        /// <returns>Loaded image</returns>
        private void LoadFromLocalStorage(Uri imageUri, BitmapImage bitmap)
        {
            string isolatedStoragePath = GetFileNameInIsolatedStorage(imageUri);
            var storage = IsolatedStorageFile.GetUserStoreForApplication();
            using (var sourceFile = storage.OpenFile(isolatedStoragePath, FileMode.Open, FileAccess.Read))
            {
                bitmap.SetSource(sourceFile);

                _imageLoaded = true;
            }
        }

        /// <summary>
        /// Writes the stream data to isolated storage
        /// </summary>
        /// <param name="inputStream">Input stream</param>
        /// <param name="fileName">File name</param>
        private void WriteToIsolatedStorage(System.IO.Stream inputStream, string fileName)
        {
            var storage = IsolatedStorageFile.GetUserStoreForApplication();
            IsolatedStorageFileStream outputStream = null;
            try
            {
                if (!storage.DirectoryExists(imageStorageFolder))
                {
                    storage.CreateDirectory(imageStorageFolder);
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
                    outputStream.Close();
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
            return imageStorageFolder + "\\" + uri.AbsoluteUri.GetHashCode() + ".img";
        }
    }
}
