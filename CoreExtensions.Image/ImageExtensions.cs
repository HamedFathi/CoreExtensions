using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace CoreExtensions
{
    public static class ImageExtensions
    {
        /// <summary>
        /// Return new Bitmap from Byte Array
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="imageBytes">The image bytes.</param>
        /// <returns></returns>
        public static Bitmap FromBytes(this Bitmap value, Byte[] imageBytes)
        {
            using (MemoryStream ms = new MemoryStream(imageBytes))
            {
                return new Bitmap(ms);
            }
        }

        /// <summary>
        /// Return Byte Array from Bitmap
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="format">The format.</param>
        /// <returns></returns>
        public static Byte[] GetBytes(this Bitmap value, ImageFormat format)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                value.Save(ms, format);
                return ms.GetBuffer();
            }
        }

        public static string ToBase64(this Image image, ImageFormat format)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, format);
                byte[] imageBytes = ms.ToArray();
                string base64String = Convert.ToBase64String(imageBytes);
                return base64String;
            }
        }

        public static byte[] ToByteArray(this Image imageIn, ImageFormat imgFormat)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, imgFormat);
            return ms.ToArray();
        }
    }
}
