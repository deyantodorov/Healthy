namespace HealthySystem.Web.Infrastructure.Images
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Web.Hosting;
    using HealthySystem.Common;

    public class Images
    {
        /// <summary>
        /// Create if not exist image and save it to Cache folder.
        /// </summary>
        /// <param name="path">Path of upload directory</param>
        /// <param name="newWidth">desired Min Width when resized</param>
        /// <param name="maxHeight">desired Max Height when resized</param>
        /// <returns>Path of resized and saved image in Cache folder</returns>
        public static string GetImageFromCache(string path, int newWidth, int maxHeight)
        {
            if (string.IsNullOrEmpty(path))
            {
                return path;
            }

            string serverPath = HostingEnvironment.MapPath("~/");

            string originalPath = path;
            string cachePath = originalPath.Replace("/Uploads/", "Cache/");

            if (File.Exists(serverPath + cachePath.Replace("/", "\\")))
            {
                return "/" + cachePath;
            }

            string currentFolder = DateTime.Now.Year + "_" + DateTime.Now.Month;
            string hddPath = serverPath + WebConstants.DirectoryCache + "\\" + currentFolder;

            if (!Directory.Exists(hddPath))
            {
                Directory.CreateDirectory(hddPath);
            }

            ResizeAndSaveImage(originalPath, cachePath, newWidth, maxHeight, true);

            cachePath = "/" + cachePath;

            return cachePath;
        }

        private static void ResizeAndSaveImage(string uploadFilePath, string cacheFilePath, int newWidth, int maxHeight, bool onlyResizeIfWider)
        {
            // Set current location source path
            string host = HostingEnvironment.MapPath("~/");
            uploadFilePath = uploadFilePath.Substring(1, uploadFilePath.Length - 1).Replace("/", "\\");
            uploadFilePath = host + uploadFilePath;

            Image fullsizeImage = Image.FromFile(uploadFilePath);

            // Prevent using images internal thumbnail
            fullsizeImage.RotateFlip(RotateFlipType.Rotate180FlipNone);
            fullsizeImage.RotateFlip(RotateFlipType.Rotate180FlipNone);

            if (onlyResizeIfWider)
            {
                if (fullsizeImage.Width <= newWidth)
                {
                    newWidth = fullsizeImage.Width;
                }
            }

            int newHeight = fullsizeImage.Height * newWidth / fullsizeImage.Width;

            if (newHeight > maxHeight)
            {
                // Resize with height instead
                newWidth = fullsizeImage.Width * maxHeight / fullsizeImage.Height;
                newHeight = maxHeight;
            }

            Image newImage = fullsizeImage.GetThumbnailImage(newWidth, newHeight, null, IntPtr.Zero);

            // Clear handle to original file so that we can overwrite it if necessary
            fullsizeImage.Dispose();

            // Save resized picture
            newImage.Save(host + cacheFilePath.Replace("/", "\\"), ImageFormat.Jpeg);
        }
    }
}