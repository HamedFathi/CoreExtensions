using CoreUtilities;
using System;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace CoreExtensions
{
    public static class StreamExtensions
    {
        /// <summary>
        ///     Copy from one stream to another.
        ///     Example:
        ///     using(var stream = response.GetResponseStream())
        ///     using(var ms = new MemoryStream())
        ///     {
        ///     stream.CopyTo(ms);
        ///     // Do something with copied data
        ///     }
        /// </summary>
        /// <param name="fromStream">From stream.</param>
        /// <param name="toStream">To stream.</param>
        public static void CopyTo(this Stream fromStream, Stream toStream)
        {
            if (fromStream == null)
                throw new ArgumentNullException(nameof(fromStream));
            if (toStream == null)
                throw new ArgumentNullException(nameof(toStream));
            var bytes = new byte[8092];
            int dataRead;
            while ((dataRead = fromStream.Read(bytes, 0, bytes.Length)) > 0)
                toStream.Write(bytes, 0, dataRead);
        }

        /// <summary>
        ///     Copies one stream into a another one.
        /// </summary>
        /// <param name="stream">The source stream.</param>
        /// <param name="targetStream">The target stream.</param>
        /// <param name="bufferSize">The buffer size used to read / write.</param>
        /// <returns>The source stream.</returns>
        public static Stream CopyTo(this Stream stream, Stream targetStream, int bufferSize)
        {
            if (stream.CanRead == false)
                throw new InvalidOperationException("Source stream does not support reading.");
            if (targetStream.CanWrite == false)
                throw new InvalidOperationException("Target stream does not support writing.");

            var buffer = new byte[bufferSize];
            int bytesRead;

            while ((bytesRead = stream.Read(buffer, 0, bufferSize)) > 0)
                targetStream.Write(buffer, 0, bytesRead);
            return stream;
        }

        /// <summary>
        ///     Copies any stream into a local MemoryStream
        /// </summary>
        /// <param name="stream">The source stream.</param>
        /// <returns>The copied memory stream.</returns>
        public static MemoryStream CopyToMemory(this Stream stream)
        {
            var memoryStream = new MemoryStream((int)stream.Length);
            stream.CopyTo(memoryStream);
            return memoryStream;
        }

        public static Encoding GetEncoding(this FileStream file)
        {
            // Read the BOM
            var bom = new byte[4];
            file.Read(bom, 0, 4);
            // Analyze the BOM
            if (bom[0] == 0x2b && bom[1] == 0x2f && bom[2] == 0x76) return Encoding.UTF7;
            if (bom[0] == 0xef && bom[1] == 0xbb && bom[2] == 0xbf) return Encoding.UTF8;
            if (bom[0] == 0xff && bom[1] == 0xfe) return Encoding.Unicode; //UTF-16LE
            if (bom[0] == 0xfe && bom[1] == 0xff) return Encoding.BigEndianUnicode; //UTF-16BE
            if (bom[0] == 0 && bom[1] == 0 && bom[2] == 0xfe && bom[3] == 0xff) return Encoding.UTF32;
            return Encoding.ASCII;
        }

        /// <summary>
        ///     Opens a StreamReader using the default encoding.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <returns>The stream reader</returns>
        public static StreamReader GetReader(this Stream stream)
        {
            return stream.GetReader(null);
        }

        /// <summary>
        ///     Opens a StreamReader using the specified encoding.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="encoding">The encoding.</param>
        /// <returns>The stream reader</returns>
        public static StreamReader GetReader(this Stream stream, Encoding encoding)
        {
            if (stream.CanRead == false)
                throw new InvalidOperationException("Stream does not support reading.");

            encoding = encoding ?? Encoding.UTF8;
            return new StreamReader(stream, encoding);
        }

        /// <summary>
        ///     Opens a StreamWriter using the default encoding.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <returns>The stream writer</returns>
        public static StreamWriter GetWriter(this Stream stream)
        {
            return stream.GetWriter(null);
        }

        /// <summary>
        ///     Opens a StreamWriter using the specified encoding.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="encoding">The encoding.</param>
        /// <returns>The stream writer</returns>
        public static StreamWriter GetWriter(this Stream stream, Encoding encoding)
        {
            if (stream.CanWrite == false)
                throw new InvalidOperationException("Stream does not support writing.");

            encoding = encoding ?? Encoding.UTF8;
            return new StreamWriter(stream, encoding);
        }

        public static Boolean IsNullOrEmpty(this Stream str)
        {
            if (str == null)
                return true;
            return (str.Length <= 0);
        }

        /// <summary>
        ///     Reads the entire stream and returns a byte array.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <returns>The byte array</returns>
        /// <remarks>
        ///     Thanks to EsbenCarlsen  for providing an update to this method.
        /// </remarks>
        public static byte[] ReadAllBytes(this Stream stream)
        {
            using (var memoryStream = stream.CopyToMemory())
                return memoryStream.ToArray();
        }

        /// <summary>
        ///     Reads a fixed number of bytes.
        /// </summary>
        /// <param name="stream">The stream to read from</param>
        /// <param name="bufsize">The number of bytes to read.</param>
        /// <returns>the read byte[]</returns>
        public static byte[] ReadFixedBuffersize(this Stream stream, int bufsize)
        {
            var buf = new byte[bufsize];
            int offset = 0, cnt;
            do
            {
                cnt = stream.Read(buf, offset, bufsize - offset);
                if (cnt == 0)
                    return null;
                offset += cnt;
            } while (offset < bufsize);

            return buf;
        }

        /// <summary>
        ///     A Stream extension method that reads a stream to the end.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>
        ///     The rest of the stream as a string, from the current position to the end. If the current position is at the
        ///     end of the stream, returns an empty string ("").
        /// </returns>
        public static string ReadToEnd(this Stream @this)
        {
            using (var sr = new StreamReader(@this, Encoding.UTF8))
            {
                return sr.ReadToEnd();
            }
        }

        /// <summary>
        ///     A Stream extension method that reads a stream to the end.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="encoding">The encoding.</param>
        /// <returns>
        ///     The rest of the stream as a string, from the current position to the end. If the current position is at the
        ///     end of the stream, returns an empty string ("").
        /// </returns>
        public static string ReadToEnd(this Stream @this, Encoding encoding)
        {
            using (var sr = new StreamReader(@this, encoding))
            {
                return sr.ReadToEnd();
            }
        }

        /// <summary>
        ///     A Stream extension method that reads a stream to the end.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="position">The position.</param>
        /// <returns>
        ///     The rest of the stream as a string, from the current position to the end. If the current position is at the
        ///     end of the stream, returns an empty string ("").
        /// </returns>
        public static string ReadToEnd(this Stream @this, long position)
        {
            @this.Position = position;

            using (var sr = new StreamReader(@this, Encoding.UTF8))
            {
                return sr.ReadToEnd();
            }
        }

        /// <summary>
        ///     A Stream extension method that reads a stream to the end.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="encoding">The encoding.</param>
        /// <param name="position">The position.</param>
        /// <returns>
        ///     The rest of the stream as a string, from the current position to the end. If the current position is at the
        ///     end of the stream, returns an empty string ("").
        /// </returns>
        public static string ReadToEnd(this Stream @this, Encoding encoding, long position)
        {
            @this.Position = position;

            using (var sr = new StreamReader(@this, encoding))
            {
                return sr.ReadToEnd();
            }
        }

        /// <summary>
        ///     Sets the stream cursor to the beginning of the stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <returns>The stream</returns>
        public static Stream SeekToBegin(this Stream stream)
        {
            if (stream.CanSeek == false)
                throw new InvalidOperationException("Stream does not support seeking.");

            stream.Seek(0, SeekOrigin.Begin);
            return stream;
        }

        /// <summary>
        ///     Sets the stream cursor to the end of the stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <returns>The stream</returns>
        public static Stream SeekToEnd(this Stream stream)
        {
            if (stream.CanSeek == false)
                throw new InvalidOperationException("Stream does not support seeking.");

            stream.Seek(0, SeekOrigin.End);
            return stream;
        }

        public static Assembly ToAssembly(this FileStream stream, LoadContextUtility loadContext = null)
        {
            if (loadContext == null)
            {
                var context = new LoadContextUtility();
                return context.LoadFromStream(stream);
            }
            return loadContext.LoadFromStream(stream);
        }

        /// <summary>
        ///     A Stream extension method that converts the Stream to a byte array.
        /// </summary>
        /// <param name="this">The Stream to act on.</param>
        /// <returns>The Stream as a byte[].</returns>
        public static byte[] ToByteArray(this Stream @this)
        {
            using (var ms = new MemoryStream())
            {
                @this.CopyTo(ms);
                return ms.ToArray();
            }
        }

        /// <summary>
        ///     A Stream extension method that converts the @this to a md 5 hash.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>@this as a string.</returns>
        public static string ToMD5Hash(this Stream @this)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] hashBytes = md5.ComputeHash(@this);
                var sb = new StringBuilder();
                foreach (byte bytes in hashBytes)
                {
                    sb.Append(bytes.ToString("X2"));
                }

                return sb.ToString();
            }
        }

        public static MemoryStream ToMemoryStream(this Stream stream)
        {
            MemoryStream ret = new MemoryStream();
            byte[] buffer = new byte[8192];
            int bytesRead;
            while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
                ret.Write(buffer, 0, bytesRead);
            ret.Position = 0;
            return ret;
        }

        public static string ToString(this Stream stream)
        {
            using (var reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }

        /// <summary>
        ///     Writes all passed bytes to the specified stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="bytes">The byte array / buffer.</param>
        public static void Write(this Stream stream, byte[] bytes)
        {
            stream.Write(bytes, 0, bytes.Length);
        }
    }
}
