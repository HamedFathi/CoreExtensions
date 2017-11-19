using System;
using System.IO;
using System.Text;

namespace CoreExtensions
{
    public static class ByteArrayExtensions
    {
        /// <summary>
        /// 	Find the first occurence of an byte[] in another byte[]
        /// </summary>
        /// <param name = "buf1">the byte[] to search in</param>
        /// <param name = "buf2">the byte[] to find</param>
        /// <returns>the first position of the found byte[] or -1 if not found</returns>
        /// <remarks>
        /// 	Contributed by blaumeister, http://www.codeplex.com/site/users/view/blaumeiser
        /// </remarks>
        public static int FindArrayInArray(this byte[] buf1, byte[] buf2)
        {
            if (buf2 == null)
                throw new ArgumentNullException(nameof(buf2));

            if (buf1 == null)
                throw new ArgumentNullException(nameof(buf1));

            if (buf2.Length == 0)
                return 0;		// by definition empty sets match immediately

            int j = -1;
            int end = buf1.Length - buf2.Length;
            while ((j = Array.IndexOf(buf1, buf2[0], j + 1)) <= end && j != -1)
            {
                int i = 1;
                while (buf1[j + i] == buf2[i])
                {
                    if (++i == buf2.Length)
                        return j;
                }
            }
            return -1;
        }

        /// <summary>
        ///     A byte[] extension method that resizes the byte[].
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="newSize">Size of the new.</param>
        /// <returns>A byte[].</returns>
        public static byte[] Resize(this byte[] @this, int newSize)
        {
            Array.Resize(ref @this, newSize);
            return @this;
        }

        /// <summary>
        ///     Converts a subset of an 8-bit unsigned integer array to an equivalent subset of a Unicode character array
        ///     encoded with base-64 digits. Parameters specify the subsets as offsets in the input and output arrays, and
        ///     the number of elements in the input array to convert.
        /// </summary>
        /// <param name="inArray">An input array of 8-bit unsigned integers.</param>
        /// <param name="offsetIn">A position within .</param>
        /// <param name="length">The number of elements of  to convert.</param>
        /// <param name="outArray">An output array of Unicode characters.</param>
        /// <param name="offsetOut">A position within .</param>
        /// <returns>A 32-bit signed integer containing the number of bytes in .</returns>
        public static Int32 ToBase64CharArray(this Byte[] inArray, Int32 offsetIn, Int32 length, Char[] outArray, Int32 offsetOut)
        {
            return Convert.ToBase64CharArray(inArray, offsetIn, length, outArray, offsetOut);
        }

        /// <summary>
        ///     Converts an array of 8-bit unsigned integers to its equivalent string representation that is encoded with
        ///     base-64 digits.
        /// </summary>
        /// <param name="inArray">An array of 8-bit unsigned integers.</param>
        /// <returns>The string representation, in base 64, of the contents of .</returns>
        public static String ToBase64String(this Byte[] inArray)
        {
            return Convert.ToBase64String(inArray);
        }

        /// <summary>
        ///     Converts a subset of an array of 8-bit unsigned integers to its equivalent string representation that is
        ///     encoded with base-64 digits. Parameters specify the subset as an offset in the input array, and the number of
        ///     elements in the array to convert.
        /// </summary>
        /// <param name="inArray">An array of 8-bit unsigned integers.</param>
        /// <param name="offset">An offset in .</param>
        /// <param name="length">The number of elements of  to convert.</param>
        /// <returns>The string representation in base 64 of  elements of , starting at position .</returns>
        public static String ToBase64String(this Byte[] inArray, Int32 offset, Int32 length)
        {
            return Convert.ToBase64String(inArray, offset, length);
        }

        /// <summary>
        /// Converts a byte array into a hex string
        /// </summary>
        public static string ToHex(this byte[] bytes)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < bytes.Length; ++i)
            {
                sb.Append(bytes[i].ToString("x2"));
            }
            return sb.ToString();
        }

        public static System.Drawing.Image ToImage(this byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            System.Drawing.Image returnImage = System.Drawing.Image.FromStream(ms);
            return returnImage;
        }

        /// <summary>
        ///     A byte[] extension method that converts the @this to a memory stream.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>@this as a MemoryStream.</returns>
        public static MemoryStream ToMemoryStream(this Byte[] buffer)
        {
            using (MemoryStream ms = new MemoryStream(buffer) { Position = 0 })
            {
                return ms;
            }
        }

        /// <summary>
        /// Converts a byte array to a <see cref="Stream"/>
        /// </summary>
        /// <param name="input">The byte array being converted.</param>
        /// <returns>A <see cref="Stream"/> representing the contents of a byte array.</returns>
        public static Stream ToStream(this byte[] input)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            using (var fileStream = new MemoryStream(input) { Position = 0 })
            {
                return fileStream;
            }
        }
    }
}
