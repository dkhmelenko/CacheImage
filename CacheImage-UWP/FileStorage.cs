using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;

namespace KhmelenkoLab
{
    /// <summary>
    /// Provides methods for managing storage files
    /// </summary>
    internal static class FileStorage
    {
        private const string StorageFolder = "ImageCaches";

        /// <summary>
        /// Creates new folder or open existing folder
        /// </summary>
        /// <returns>Created/Open folder</returns>
        private static async Task<StorageFolder> CreateOrOpenFolder()
        {
            var localFolder = ApplicationData.Current.LocalFolder;
            return await localFolder.CreateFolderAsync(StorageFolder, CreationCollisionOption.OpenIfExists);
        }

        /// <summary>
        /// Writes buffer data to the file
        /// </summary>
        /// <param name="filename">Filename</param>
        /// <param name="bufferData">Buffer data</param>
        public static async Task WriteFile(string filename, IBuffer bufferData)
        {
            var localFolder = await CreateOrOpenFolder();

            // create file 
            var file = await localFolder.CreateFileAsync(filename, CreationCollisionOption.ReplaceExisting);

            // write content
            await FileIO.WriteBufferAsync(file, bufferData);
        }

        /// <summary>
        /// Reads file and returns buffer data
        /// </summary>
        /// <param name="filename">File name for reading</param>
        /// <returns>Buffer with data or null</returns>
        public static async Task<IBuffer> ReadFile(string filename)
        {
            var localFolder = await CreateOrOpenFolder();

            var file = await localFolder.GetFileAsync(filename);

            IBuffer readResult = null;
            if (file != null)
            {
                readResult = await FileIO.ReadBufferAsync(file);
            }
            return readResult;
        }

        /// <summary>
        /// Checks whether file exists or not
        /// </summary>
        /// <param name="filename">File name</param>
        /// <returns>True, if file exists</returns>
        public static async Task<bool> FileExists(string filename)
        {
            var localFolder = await CreateOrOpenFolder();
            var files = await localFolder.GetFilesAsync();
            var foundFile = files.FirstOrDefault(x => x.Name == filename);
            return foundFile != null;
        }
    }
}