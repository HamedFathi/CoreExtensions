using Polly;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using CoreUtilities;

namespace CoreExtensions
{
    public static class FileInfoExtensions
    {
        /// <summary>
        ///     A FileInfo extension method that appends all lines.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="contents">The contents.</param>
        public static void AppendAllLines(this FileInfo @this, IEnumerable<String> contents)
        {
            File.AppendAllLines(@this.FullName, contents);
        }

        /// <summary>
        ///     A FileInfo extension method that appends all lines.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="contents">The contents.</param>
        /// <param name="encoding">The encoding.</param>
        public static void AppendAllLines(this FileInfo @this, IEnumerable<String> contents, Encoding encoding)
        {
            File.AppendAllLines(@this.FullName, contents, encoding);
        }

        /// <summary>
        ///     Opens a file, appends the specified string to the file, and then closes the file. If the file does not exist,
        ///     this method creates a file, writes the specified string to the file, then closes the file.
        /// </summary>
        /// <param name="this">The file to append the specified string to.</param>
        /// <param name="contents">The string to append to the file.</param>
        /// ###
        /// <exception cref="T:System.ArgumentException">
        ///     <paramref name="this" /> is a zero-length string, contains only
        ///     white space, or contains one or more invalid characters as defined by
        ///     <see
        ///         cref="F:System.IO.Path.InvalidPathChars" />
        ///     .
        /// </exception>
        /// ###
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="this" /> is null.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.PathTooLongException">
        ///     The specified @this, file name, or both exceed the system-
        ///     defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file
        ///     names must be less than 260 characters.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.DirectoryNotFoundException">
        ///     The specified @this is invalid (for example, the
        ///     directory doesn?t exist or it is on an unmapped drive).
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.IOException">An I/O error occurred while opening the file.</exception>
        /// ###
        /// <exception cref="T:System.UnauthorizedAccessException">
        ///     <paramref name="this" /> specified a file that is
        ///     read-only.-or- This operation is not supported on the current platform.-or-
        ///     <paramref
        ///         name="this" />
        ///     specified a directory.-or- The caller does not have the required permission.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.FileNotFoundException">
        ///     The file specified in <paramref name="this" /> was not
        ///     found.
        /// </exception>
        /// ###
        /// <exception cref="T:System.NotSupportedException">
        ///     <paramref name="this" /> is in an invalid format.
        /// </exception>
        /// ###
        /// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
        public static void AppendAllText(this FileInfo @this, String contents)
        {
            File.AppendAllText(@this.FullName, contents);
        }

        /// <summary>
        ///     Appends the specified string to the file, creating the file if it does not already exist.
        /// </summary>
        /// <param name="this">The file to append the specified string to.</param>
        /// <param name="contents">The string to append to the file.</param>
        /// <param name="encoding">The character encoding to use.</param>
        /// ###
        /// <exception cref="T:System.ArgumentException">
        ///     <paramref name="this" /> is a zero-length string, contains only
        ///     white space, or contains one or more invalid characters as defined by
        ///     <see
        ///         cref="F:System.IO.Path.InvalidPathChars" />
        ///     .
        /// </exception>
        /// ###
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="this" /> is null.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.PathTooLongException">
        ///     The specified @this, file name, or both exceed the system-
        ///     defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file
        ///     names must be less than 260 characters.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.DirectoryNotFoundException">
        ///     The specified @this is invalid (for example, the
        ///     directory doesn?t exist or it is on an unmapped drive).
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.IOException">An I/O error occurred while opening the file.</exception>
        /// ###
        /// <exception cref="T:System.UnauthorizedAccessException">
        ///     <paramref name="this" /> specified a file that is
        ///     read-only.-or- This operation is not supported on the current platform.-or-
        ///     <paramref
        ///         name="this" />
        ///     specified a directory.-or- The caller does not have the required permission.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.FileNotFoundException">
        ///     The file specified in <paramref name="this" /> was not
        ///     found.
        /// </exception>
        /// ###
        /// <exception cref="T:System.NotSupportedException">
        ///     <paramref name="this" /> is in an invalid format.
        /// </exception>
        /// ###
        /// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
        public static void AppendAllText(this FileInfo @this, String contents, Encoding encoding)
        {
            File.AppendAllText(@this.FullName, contents, encoding);
        }

        /// <summary>
        ///     Changes the files extension.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="newExtension">The new extension.</param>
        /// <returns>The renamed file</returns>
        /// <example>
        ///     <code>
        /// 		var file = new FileInfo(@"c:\test.txt");
        /// 		file.ChangeExtension("xml");
        /// 	</code>
        /// </example>
        public static FileInfo ChangeExtension(this FileInfo file, String newExtension)
        {
            newExtension = newExtension.StartsWith(".") ? newExtension : string.Concat(".", newExtension);
            var fileName = String.Concat(Path.GetFileNameWithoutExtension(file.FullName), newExtension);
            file.Rename(fileName);
            return file;
        }

        /// <summary>
        ///     Changes the extensions of several files at once.
        /// </summary>
        /// <param name="files">The files.</param>
        /// <param name="newExtension">The new extension.</param>
        /// <returns>The renamed files</returns>
        /// <example>
        ///     <code>
        /// 		var files = directory.GetFiles("*.txt", "*.xml");
        /// 		files.ChangeExtensions("tmp");
        /// 	</code>
        /// </example>
        public static FileInfo[] ChangeExtensions(this FileInfo[] files, String newExtension)
        {
            return files.Select(f => f.ChangeExtension(newExtension)).ToArray();
        }

        /// <summary>
        ///     Copies several files to a new folder at once and consolidates any exceptions.
        /// </summary>
        /// <param name="files">The files.</param>
        /// <param name="targetPath">The target path.</param>
        /// <returns>The newly created file copies</returns>
        /// <example>
        ///     <code>
        /// 		var files = directory.GetFiles("*.txt", "*.xml");
        /// 		var copiedFiles = files.CopyTo(@"c:\temp\");
        /// 	</code>
        /// </example>
        public static FileInfo[] CopyTo(this FileInfo[] files, String targetPath)
        {
            return files.CopyTo(targetPath, true);
        }

        /// <summary>
        ///     Copies several files to a new folder at once and optionally consolidates any exceptions.
        /// </summary>
        /// <param name="files">The files.</param>
        /// <param name="targetPath">The target path.</param>
        /// <param name="consolidateExceptions">
        ///     if set to <c>true</c> exceptions are consolidated and the processing is not
        ///     interrupted.
        /// </param>
        /// <returns>The newly created file copies</returns>
        /// <example>
        ///     <code>
        /// 		var files = directory.GetFiles("*.txt", "*.xml");
        /// 		var copiedFiles = files.CopyTo(@"c:\temp\");
        /// 	</code>
        /// </example>
        public static FileInfo[] CopyTo(this FileInfo[] files, String targetPath, bool consolidateExceptions)
        {
            var copiedfiles = new List<FileInfo>();
            List<Exception> exceptions = null;

            foreach (var file in files)
            {
                try
                {
                    var fileName = Path.Combine(targetPath, file.Name);
                    copiedfiles.Add(file.CopyTo(fileName));
                }
                catch (Exception e)
                {
                    if (consolidateExceptions)
                    {
                        if (exceptions == null)
                            exceptions = new List<Exception>();
                        exceptions.Add(e);
                    }
                    else
                        throw;
                }
            }

            if ((exceptions != null) && (exceptions.Count > 0))
                throw new CombinedException(
                    "Error while copying one or several files, see InnerExceptions array for details.", exceptions.ToArray());

            return copiedfiles.ToArray();
        }

        /// <summary>
        ///     Creates all directories and subdirectories in the specified @this.
        /// </summary>
        /// <param name="this">The directory @this to create.</param>
        /// <returns>An object that represents the directory for the specified @this.</returns>
        /// ###
        /// <exception cref="T:System.IO.IOException">
        ///     The directory specified by <paramref name="this" /> is a file.-or-The
        ///     network name is not known.
        /// </exception>
        /// ###
        /// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
        /// ###
        /// <exception cref="T:System.ArgumentException">
        ///     <paramref name="this" /> is a zero-length System.String, contains only
        ///     white space, or contains one or more invalid characters as defined by
        ///     <see
        ///         cref="F:System.IO.Path.InvalidPathChars" />
        ///     .-or-<paramref name="this" /> is prefixed with, or contains only a colon character (:).
        /// </exception>
        /// ###
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="this" /> is null.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.PathTooLongException">
        ///     The specified @this, file name, or both exceed the system-
        ///     defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters and file
        ///     names must be less than 260 characters.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.DirectoryNotFoundException">
        ///     The specified @this is invalid (for example, it is on
        ///     an unmapped drive).
        /// </exception>
        /// ###
        /// <exception cref="T:System.NotSupportedException">
        ///     <paramref name="this" /> contains a colon character (:) that
        ///     is not part of a drive label ("C:\").
        /// </exception>
        public static DirectoryInfo CreateDirectory(this FileInfo @this)
        {
            return Directory.CreateDirectory(@this.Directory.FullName);
        }

        /// <summary>
        ///     A FileInfo extension method that creates a zip file.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        public static void CreateGZip(this FileInfo @this)
        {
            using (FileStream originalFileStream = @this.OpenRead())
            {
                using (FileStream compressedFileStream = File.Create(@this.FullName + ".gz"))
                {
                    using (var compressionStream = new GZipStream(compressedFileStream, CompressionMode.Compress))
                    {
                        originalFileStream.CopyTo(compressionStream);
                    }
                }
            }
        }

        /// <summary>
        ///     A FileInfo extension method that creates a zip file.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="destination">Destination for the zip.</param>
        public static void CreateGZip(this FileInfo @this, String destination)
        {
            using (FileStream originalFileStream = @this.OpenRead())
            {
                using (FileStream compressedFileStream = File.Create(destination))
                {
                    using (var compressionStream = new GZipStream(compressedFileStream, CompressionMode.Compress))
                    {
                        originalFileStream.CopyTo(compressionStream);
                    }
                }
            }
        }

        /// <summary>
        ///     A FileInfo extension method that creates a zip file.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="destination">Destination for the zip.</param>
        public static void CreateGZip(this FileInfo @this, FileInfo destination)
        {
            using (FileStream originalFileStream = @this.OpenRead())
            {
                using (FileStream compressedFileStream = File.Create(destination.FullName))
                {
                    using (var compressionStream = new GZipStream(compressedFileStream, CompressionMode.Compress))
                    {
                        originalFileStream.CopyTo(compressionStream);
                    }
                }
            }
        }

        /// <summary>
        ///     Deletes several files at once and consolidates any exceptions.
        /// </summary>
        /// <param name="files">The files.</param>
        /// <example>
        ///     <code>
        /// 		var files = directory.GetFiles("*.txt", "*.xml");
        /// 		files.Delete()
        /// 	</code>
        /// </example>
        public static void Delete(this FileInfo[] files)
        {
            files.Delete(true);
        }

        /// <summary>
        ///     Deletes several files at once and optionally consolidates any exceptions.
        /// </summary>
        /// <param name="files">The files.</param>
        /// <param name="consolidateExceptions">
        ///     if set to <c>true</c> exceptions are consolidated and the processing is not
        ///     interrupted.
        /// </param>
        /// <example>
        ///     <code>
        /// 		var files = directory.GetFiles("*.txt", "*.xml");
        /// 		files.Delete()
        /// 	</code>
        /// </example>
        public static void Delete(this FileInfo[] files, bool consolidateExceptions)
        {
            if (consolidateExceptions)
            {
                var exceptions = new List<Exception>();

                foreach (var file in files)
                {
                    try
                    {
                        file.Delete();
                    }
                    catch (Exception e)
                    {
                        exceptions.Add(e);
                    }
                }
                if (exceptions.Any())
                    throw CombinedException.Combine(
                        "Error while deleting one or several files, see InnerExceptions array for details.", exceptions);
            }
            else
            {
                foreach (var file in files)
                {
                    file.Delete();
                }
            }
        }

        /// <summary>
        ///     Creates all directories and subdirectories in the specified @this if the directory doesn't already exists.
        ///     This methods is the same as FileInfo.CreateDirectory however it's less ambigues about what happen if the
        ///     directory already exists.
        /// </summary>
        /// <param name="this">The directory @this to create.</param>
        /// <returns>An object that represents the directory for the specified @this.</returns>
        /// ###
        /// <exception cref="T:System.IO.IOException">
        ///     The directory specified by <paramref name="this" /> is a file.-or-The
        ///     network name is not known.
        /// </exception>
        /// ###
        /// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
        /// ###
        /// <exception cref="T:System.ArgumentException">
        ///     <paramref name="this" /> is a zero-length System.String, contains only
        ///     white space, or contains one or more invalid characters as defined by
        ///     <see
        ///         cref="F:System.IO.Path.InvalidPathChars" />
        ///     .-or-<paramref name="this" /> is prefixed with, or contains only a colon character (:).
        /// </exception>
        /// ###
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="this" /> is null.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.PathTooLongException">
        ///     The specified @this, file name, or both exceed the system-
        ///     defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters and file
        ///     names must be less than 260 characters.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.DirectoryNotFoundException">
        ///     The specified @this is invalid (for example, it is on
        ///     an unmapped drive).
        /// </exception>
        /// ###
        /// <exception cref="T:System.NotSupportedException">
        ///     <paramref name="this" /> contains a colon character (:) that
        ///     is not part of a drive label ("C:\").
        /// </exception>
        public static DirectoryInfo EnsureDirectoryExists(this FileInfo @this)
        {
            return Directory.CreateDirectory(@this.Directory.FullName);
        }

        /// <summary>
        ///     A FileInfo extension method that extracts the g zip to directory described by
        ///     @this.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        public static void ExtractGZipToDirectory(this FileInfo @this)
        {
            using (FileStream originalFileStream = @this.OpenRead())
            {
                String newFileName = Path.GetFileNameWithoutExtension(@this.FullName);

                using (FileStream decompressedFileStream = File.Create(newFileName))
                {
                    using (var decompressionStream = new GZipStream(originalFileStream, CompressionMode.Decompress))
                    {
                        decompressionStream.CopyTo(decompressedFileStream);
                    }
                }
            }
        }

        /// <summary>
        ///     A FileInfo extension method that extracts the g zip to directory described by
        ///     @this.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="destination">Destination for the.</param>
        public static void ExtractGZipToDirectory(this FileInfo @this, String destination)
        {
            using (FileStream originalFileStream = @this.OpenRead())
            {
                using (FileStream compressedFileStream = File.Create(destination))
                {
                    using (var compressionStream = new GZipStream(compressedFileStream, CompressionMode.Compress))
                    {
                        originalFileStream.CopyTo(compressionStream);
                    }
                }
            }
        }

        /// <summary>
        ///     A FileInfo extension method that extracts the g zip to directory described by
        ///     @this.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="destination">Destination for the.</param>
        public static void ExtractGZipToDirectory(this FileInfo @this, FileInfo destination)
        {
            using (FileStream originalFileStream = @this.OpenRead())
            {
                using (FileStream compressedFileStream = File.Create(destination.FullName))
                {
                    using (var compressionStream = new GZipStream(compressedFileStream, CompressionMode.Compress))
                    {
                        originalFileStream.CopyTo(compressionStream);
                    }
                }
            }
        }

        /// <summary>
        ///     A FileInfo extension method that gets directory full name.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The directory full name.</returns>
        public static String GetDirectoryFullName(this FileInfo @this)
        {
            return @this.Directory.FullName;
        }

        /// <summary>
        ///     A FileInfo extension method that gets directory name.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>The directory name.</returns>
        public static String GetDirectoryName(this FileInfo @this)
        {
            return @this.Directory.Name;
        }

        /// <summary>
        ///     Returns the file name of the specified @this System.String without the extension.
        /// </summary>
        /// <param name="this">The @this of the file.</param>
        /// <returns>
        ///     The System.String returned by <see cref="M:System.IO.Path.GetFileName(System.System.String)" />, minus the last period (.)
        ///     and all characters following it.
        /// </returns>
        /// ###
        /// <exception cref="T:System.ArgumentException">
        ///     <paramref name="this" /> contains one or more of the invalid
        ///     characters defined in
        ///     <see
        ///         cref="M:System.IO.Path.GetInvalidPathChars" />
        ///     .
        /// </exception>
        public static String GetFileNameWithoutExtension(this FileInfo @this)
        {
            return Path.GetFileNameWithoutExtension(@this.FullName);
        }

        /// <summary>
        ///     Gets the root directory information of the specified @this.
        /// </summary>
        /// <param name="this">The @this from which to obtain root directory information.</param>
        /// <returns>
        ///     The root directory of <paramref name="this" />, such as "C:\", or null if <paramref name="this" /> is null,
        ///     or an empty System.String if
        ///     <paramref
        ///         name="this" />
        ///     does not contain root directory information.
        /// </returns>
        /// ###
        /// <exception cref="T:System.ArgumentException">
        ///     <paramref name="this" /> contains one or more of the invalid
        ///     characters defined in
        ///     <see
        ///         cref="M:System.IO.Path.GetInvalidPathChars" />
        ///     .-or- <see cref="F:System.System.String.Empty" /> was passed to
        ///     <paramref
        ///         name="this" />
        ///     .
        /// </exception>
        public static String GetPathRoot(this FileInfo @this)
        {
            return Path.GetPathRoot(@this.FullName);
        }

        /// <summary>
        ///     Determines whether a @this includes a file name extension.
        /// </summary>
        /// <param name="this">The @this to search for an extension.</param>
        /// <returns>
        ///     true if the characters that follow the last directory separator (\\ or /) or volume separator (:) in the
        ///     @this include a period (.) followed by one or more characters; otherwise, false.
        /// </returns>
        /// ###
        /// <exception cref="T:System.ArgumentException">
        ///     <paramref name="this" /> contains one or more of the invalid
        ///     characters defined in
        ///     <see
        ///         cref="M:System.IO.Path.GetInvalidPathChars" />
        ///     .
        /// </exception>
        public static Boolean HasExtension(this FileInfo @this)
        {
            return Path.HasExtension(@this.FullName);
        }

        public static bool IsFileLocked(this FileInfo file)
        {
            FileStream stream = null;
            try
            {
                stream = file.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            }
            catch (IOException)
            {
                return true;
            }
            finally
            {
                stream?.Dispose();
            }
            return false;
        }

        /// <summary>
        ///     Gets a value indicating whether the specified @this System.String contains a root.
        /// </summary>
        /// <param name="this">The @this to test.</param>
        /// <returns>
        ///     true if <paramref name="this" /> contains a root; otherwise, false.
        /// </returns>
        /// ###
        /// <exception cref="T:System.ArgumentException">
        ///     <paramref name="this" /> contains one or more of the invalid
        ///     characters defined in
        ///     <see
        ///         cref="M:System.IO.Path.GetInvalidPathChars" />
        ///     .
        /// </exception>
        public static Boolean IsPathRooted(this FileInfo @this)
        {
            return Path.IsPathRooted(@this.FullName);
        }

        /// <summary>
        ///     Moves several files to a new folder at once and optionally consolidates any exceptions.
        /// </summary>
        /// <param name="files">The files.</param>
        /// <param name="targetPath">The target path.</param>
        /// <returns>The moved files</returns>
        /// <example>
        ///     <code>
        /// 		var files = directory.GetFiles("*.txt", "*.xml");
        /// 		files.MoveTo(@"c:\temp\");
        /// 	</code>
        /// </example>
        public static FileInfo[] MoveTo(this FileInfo[] files, String targetPath)
        {
            return files.MoveTo(targetPath, true);
        }

        /// <summary>
        ///     Movies several files to a new folder at once and optionally consolidates any exceptions.
        /// </summary>
        /// <param name="files">The files.</param>
        /// <param name="targetPath">The target path.</param>
        /// <param name="consolidateExceptions">
        ///     if set to <c>true</c> exceptions are consolidated and the processing is not
        ///     interrupted.
        /// </param>
        /// <returns>The moved files</returns>
        /// <example>
        ///     <code>
        /// 		var files = directory.GetFiles("*.txt", "*.xml");
        /// 		files.MoveTo(@"c:\temp\");
        /// 	</code>
        /// </example>
        public static FileInfo[] MoveTo(this FileInfo[] files, String targetPath, bool consolidateExceptions)
        {
            List<Exception> exceptions = null;

            foreach (var file in files)
            {
                try
                {
                    var fileName = Path.Combine(targetPath, file.Name);
                    file.MoveTo(fileName);
                }
                catch (Exception e)
                {
                    if (consolidateExceptions)
                    {
                        if (exceptions == null)
                            exceptions = new List<Exception>();
                        exceptions.Add(e);
                    }
                    else
                        throw;
                }
            }

            if ((exceptions != null) && (exceptions.Count > 0))
                throw new CombinedException(
                    "Error while moving one or several files, see InnerExceptions array for details.", exceptions.ToArray());

            return files;
        }

        public static string ReadAll(this FileInfo fileInfo)
        {
            using (var stream = new FileStream(fileInfo.FullName, FileMode.Open))
            {
                using (var reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        /// <summary>
        ///     Opens a binary file, reads the contents of the file into a byte array, and then closes the file.
        /// </summary>
        /// <param name="this">The file to open for reading.</param>
        /// <returns>A byte array containing the contents of the file.</returns>
        /// ###
        /// <exception cref="T:System.ArgumentException">
        ///     <paramref name="this" /> is a zero-length System.String, contains only
        ///     white space, or contains one or more invalid characters as defined by
        ///     <see
        ///         cref="F:System.IO.Path.InvalidPathChars" />
        ///     .
        /// </exception>
        /// ###
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="this" /> is null.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.PathTooLongException">
        ///     The specified @this, file name, or both exceed the system-
        ///     defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file
        ///     names must be less than 260 characters.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.DirectoryNotFoundException">
        ///     The specified @this is invalid (for example, it is on
        ///     an unmapped drive).
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.IOException">An I/O error occurred while opening the file.</exception>
        /// ###
        /// <exception cref="T:System.UnauthorizedAccessException">
        ///     This operation is not supported on the current
        ///     platform.-or- <paramref name="this" /> specified a directory.-or- The caller does not have the required
        ///     permission.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.FileNotFoundException">
        ///     The file specified in <paramref name="this" /> was not
        ///     found.
        /// </exception>
        /// ###
        /// <exception cref="T:System.NotSupportedException">
        ///     <paramref name="this" /> is in an invalid format.
        /// </exception>
        /// ###
        /// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
        public static Byte[] ReadAllBytes(this FileInfo @this)
        {
            return File.ReadAllBytes(@this.FullName);
        }

        /// <summary>
        ///     Opens a text file, reads all lines of the file, and then closes the file.
        /// </summary>
        /// <param name="this">The file to open for reading.</param>
        /// <returns>A System.String array containing all lines of the file.</returns>
        /// ###
        /// <exception cref="T:System.ArgumentException">
        ///     <paramref name="this" /> is a zero-length System.String, contains only
        ///     white space, or contains one or more invalid characters as defined by
        ///     <see
        ///         cref="F:System.IO.Path.InvalidPathChars" />
        ///     .
        /// </exception>
        /// ###
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="this" /> is null.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.PathTooLongException">
        ///     The specified @this, file name, or both exceed the system-
        ///     defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file
        ///     names must be less than 260 characters.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.DirectoryNotFoundException">
        ///     The specified @this is invalid (for example, it is on
        ///     an unmapped drive).
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.IOException">An I/O error occurred while opening the file.</exception>
        /// ###
        /// <exception cref="T:System.UnauthorizedAccessException">
        ///     <paramref name="this" /> specified a file that is
        ///     read-only.-or- This operation is not supported on the current platform.-or-
        ///     <paramref
        ///         name="this" />
        ///     specified a directory.-or- The caller does not have the required permission.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.FileNotFoundException">
        ///     The file specified in <paramref name="this" /> was not
        ///     found.
        /// </exception>
        /// ###
        /// <exception cref="T:System.NotSupportedException">
        ///     <paramref name="this" /> is in an invalid format.
        /// </exception>
        /// ###
        /// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
        public static String[] ReadAllLines(this FileInfo @this)
        {
            return File.ReadAllLines(@this.FullName);
        }

        /// <summary>
        ///     Opens a file, reads all lines of the file with the specified encoding, and then closes the file.
        /// </summary>
        /// <param name="this">The file to open for reading.</param>
        /// <param name="encoding">The encoding applied to the contents of the file.</param>
        /// <returns>A System.String array containing all lines of the file.</returns>
        /// ###
        /// <exception cref="T:System.ArgumentException">
        ///     <paramref name="this" /> is a zero-length System.String, contains only
        ///     white space, or contains one or more invalid characters as defined by
        ///     <see
        ///         cref="F:System.IO.Path.InvalidPathChars" />
        ///     .
        /// </exception>
        /// ###
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="this" /> is null.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.PathTooLongException">
        ///     The specified @this, file name, or both exceed the system-
        ///     defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file
        ///     names must be less than 260 characters.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.DirectoryNotFoundException">
        ///     The specified @this is invalid (for example, it is on
        ///     an unmapped drive).
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.IOException">An I/O error occurred while opening the file.</exception>
        /// ###
        /// <exception cref="T:System.UnauthorizedAccessException">
        ///     <paramref name="this" /> specified a file that is
        ///     read-only.-or- This operation is not supported on the current platform.-or-
        ///     <paramref
        ///         name="this" />
        ///     specified a directory.-or- The caller does not have the required permission.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.FileNotFoundException">
        ///     The file specified in <paramref name="this" /> was not
        ///     found.
        /// </exception>
        /// ###
        /// <exception cref="T:System.NotSupportedException">
        ///     <paramref name="this" /> is in an invalid format.
        /// </exception>
        /// ###
        /// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
        public static String[] ReadAllLines(this FileInfo @this, Encoding encoding)
        {
            return File.ReadAllLines(@this.FullName, encoding);
        }

        /// <summary>
        ///     Opens a text file, reads all lines of the file, and then closes the file.
        /// </summary>
        /// <param name="this">The file to open for reading.</param>
        /// <returns>A System.String containing all lines of the file.</returns>
        /// ###
        /// <exception cref="T:System.ArgumentException">
        ///     <paramref name="this" /> is a zero-length System.String, contains only
        ///     white space, or contains one or more invalid characters as defined by
        ///     <see
        ///         cref="F:System.IO.Path.InvalidPathChars" />
        ///     .
        /// </exception>
        /// ###
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="this" /> is null.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.PathTooLongException">
        ///     The specified @this, file name, or both exceed the system-
        ///     defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file
        ///     names must be less than 260 characters.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.DirectoryNotFoundException">
        ///     The specified @this is invalid (for example, it is on
        ///     an unmapped drive).
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.IOException">An I/O error occurred while opening the file.</exception>
        /// ###
        /// <exception cref="T:System.UnauthorizedAccessException">
        ///     <paramref name="this" /> specified a file that is
        ///     read-only.-or- This operation is not supported on the current platform.-or-
        ///     <paramref
        ///         name="this" />
        ///     specified a directory.-or- The caller does not have the required permission.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.FileNotFoundException">
        ///     The file specified in <paramref name="this" /> was not
        ///     found.
        /// </exception>
        /// ###
        /// <exception cref="T:System.NotSupportedException">
        ///     <paramref name="this" /> is in an invalid format.
        /// </exception>
        /// ###
        /// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
        public static String ReadAllText(this FileInfo @this)
        {
            return File.ReadAllText(@this.FullName);
        }

        /// <summary>
        ///     Opens a file, reads all lines of the file with the specified encoding, and then closes the file.
        /// </summary>
        /// <param name="this">The file to open for reading.</param>
        /// <param name="encoding">The encoding applied to the contents of the file.</param>
        /// <returns>A System.String containing all lines of the file.</returns>
        /// ###
        /// <exception cref="T:System.ArgumentException">
        ///     <paramref name="this" /> is a zero-length System.String, contains only
        ///     white space, or contains one or more invalid characters as defined by
        ///     <see
        ///         cref="F:System.IO.Path.InvalidPathChars" />
        ///     .
        /// </exception>
        /// ###
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="this" /> is null.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.PathTooLongException">
        ///     The specified @this, file name, or both exceed the system-
        ///     defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file
        ///     names must be less than 260 characters.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.DirectoryNotFoundException">
        ///     The specified @this is invalid (for example, it is on
        ///     an unmapped drive).
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.IOException">An I/O error occurred while opening the file.</exception>
        /// ###
        /// <exception cref="T:System.UnauthorizedAccessException">
        ///     <paramref name="this" /> specified a file that is
        ///     read-only.-or- This operation is not supported on the current platform.-or-
        ///     <paramref
        ///         name="this" />
        ///     specified a directory.-or- The caller does not have the required permission.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.FileNotFoundException">
        ///     The file specified in <paramref name="this" /> was not
        ///     found.
        /// </exception>
        /// ###
        /// <exception cref="T:System.NotSupportedException">
        ///     <paramref name="this" /> is in an invalid format.
        /// </exception>
        /// ###
        /// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
        public static String ReadAllText(this FileInfo @this, Encoding encoding)
        {
            return File.ReadAllText(@this.FullName, encoding);
        }

        /// <summary>
        ///     Reads the lines of a file.
        /// </summary>
        /// <param name="this">The file to read.</param>
        /// <returns>All the lines of the file, or the lines that are the result of a query.</returns>
        /// ###
        /// <exception cref="T:System.ArgumentException">
        ///     <paramref name="this" /> is a zero-length System.String, contains only
        ///     white space, or contains one or more invalid characters defined by the
        ///     <see
        ///         cref="M:System.IO.Path.GetInvalidPathChars" />
        ///     method.
        /// </exception>
        /// ###
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="this" /> is null.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.DirectoryNotFoundException">
        ///     <paramref name="this" /> is invalid (for example, it
        ///     is on an unmapped drive).
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.FileNotFoundException">
        ///     The file specified by <paramref name="this" /> was not
        ///     found.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.IOException">An I/O error occurred while opening the file.</exception>
        /// ###
        /// <exception cref="T:System.IO.PathTooLongException">
        ///     <paramref name="this" /> exceeds the system-defined
        ///     maximum length. For example, on Windows-based platforms, paths must be less than 248 characters and file names
        ///     must be less than 260 characters.
        /// </exception>
        /// ###
        /// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
        /// ###
        /// <exception cref="T:System.UnauthorizedAccessException">
        ///     <paramref name="this" /> specifies a file that is
        ///     read-only.-or-This operation is not supported on the current platform.-or-
        ///     <paramref
        ///         name="this" />
        ///     is a directory.-or-The caller does not have the required permission.
        /// </exception>
        public static IEnumerable<String> ReadLines(this FileInfo @this)
        {
            return File.ReadLines(@this.FullName);
        }

        /// <summary>
        ///     Read the lines of a file that has a specified encoding.
        /// </summary>
        /// <param name="this">The file to read.</param>
        /// <param name="encoding">The encoding that is applied to the contents of the file.</param>
        /// <returns>All the lines of the file, or the lines that are the result of a query.</returns>
        /// ###
        /// <exception cref="T:System.ArgumentException">
        ///     <paramref name="this" /> is a zero-length System.String, contains only
        ///     white space, or contains one or more invalid characters as defined by the
        ///     <see
        ///         cref="M:System.IO.Path.GetInvalidPathChars" />
        ///     method.
        /// </exception>
        /// ###
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="this" /> is null.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.DirectoryNotFoundException">
        ///     <paramref name="this" /> is invalid (for example, it
        ///     is on an unmapped drive).
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.FileNotFoundException">
        ///     The file specified by <paramref name="this" /> was not
        ///     found.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.IOException">An I/O error occurred while opening the file.</exception>
        /// ###
        /// <exception cref="T:System.IO.PathTooLongException">
        ///     <paramref name="this" /> exceeds the system-defined
        ///     maximum length. For example, on Windows-based platforms, paths must be less than 248 characters and file names
        ///     must be less than 260 characters.
        /// </exception>
        /// ###
        /// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
        /// ###
        /// <exception cref="T:System.UnauthorizedAccessException">
        ///     <paramref name="this" /> specifies a file that is
        ///     read-only.-or-This operation is not supported on the current platform.-or-
        ///     <paramref
        ///         name="this" />
        ///     is a directory.-or-The caller does not have the required permission.
        /// </exception>
        public static IEnumerable<String> ReadLines(this FileInfo @this, Encoding encoding)
        {
            return File.ReadLines(@this.FullName, encoding);
        }

        /// <summary>
        ///     A FileInfo extension method that reads the file to the end.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>
        ///     The rest of the stream as a System.String, from the current position to the end. If the current position is at the
        ///     end of the stream, returns an empty System.String ("").
        /// </returns>
        public static String ReadToEnd(this FileInfo @this)
        {
            using (FileStream stream = File.Open(@this.FullName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (var reader = new StreamReader(stream, Encoding.UTF8))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        /// <summary>
        ///     A FileInfo extension method that reads the file to the end.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="position">The position.</param>
        /// <returns>
        ///     The rest of the stream as a System.String, from the current position to the end. If the current position is at the
        ///     end of the stream, returns an empty System.String ("").
        /// </returns>
        public static String ReadToEnd(this FileInfo @this, long position)
        {
            using (FileStream stream = File.Open(@this.FullName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                stream.Position = position;

                using (var reader = new StreamReader(stream, Encoding.UTF8))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        /// <summary>
        ///     A FileInfo extension method that reads the file to the end.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="encoding">The encoding.</param>
        /// <returns>
        ///     The rest of the stream as a System.String, from the current position to the end. If the current position is at the
        ///     end of the stream, returns an empty System.String ("").
        /// </returns>
        public static String ReadToEnd(this FileInfo @this, Encoding encoding)
        {
            using (FileStream stream = File.Open(@this.FullName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (var reader = new StreamReader(stream, encoding))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        /// <summary>
        ///     A FileInfo extension method that reads the file to the end.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="encoding">The encoding.</param>
        /// <param name="position">The position.</param>
        /// <returns>
        ///     The rest of the stream as a System.String, from the current position to the end. If the current position is at the
        ///     end of the stream, returns an empty System.String ("").
        /// </returns>
        public static String ReadToEnd(this FileInfo @this, Encoding encoding, long position)
        {
            using (FileStream stream = File.Open(@this.FullName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                stream.Position = position;

                using (var reader = new StreamReader(stream, encoding))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        /// <summary>
        ///     Renames a file.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="newName">The new name.</param>
        /// <returns>The renamed file</returns>
        /// <example>
        ///     <code>
        /// 		var file = new FileInfo(@"c:\test.txt");
        /// 		file.Rename("test2.txt");
        /// 	</code>
        /// </example>
        public static FileInfo Rename(this FileInfo file, String newName)
        {
            var filePath = Path.Combine(Path.GetDirectoryName(file.FullName), newName);
            file.MoveTo(filePath);
            return file;
        }

        /// <summary>
        ///     Changes the extension of a @this System.String.
        /// </summary>
        /// <param name="this">
        ///     The @this information to modify. The @this cannot contain any of the characters defined in
        ///     <see
        ///         cref="M:System.IO.Path.GetInvalidPathChars" />
        ///     .
        /// </param>
        /// <param name="extension">
        ///     The new extension (with or without a leading period). Specify null to remove an existing
        ///     extension from
        ///     <paramref
        ///         name="this" />
        ///     .
        /// </param>
        /// ###
        /// <exception cref="T:System.ArgumentException">
        ///     <paramref name="this" /> contains one or more of the invalid
        ///     characters defined in
        ///     <see
        ///         cref="M:System.IO.Path.GetInvalidPathChars" />
        ///     .
        /// </exception>
        public static void RenameExtension(this FileInfo @this, String extension)
        {
            String filePath = Path.ChangeExtension(@this.FullName, extension);
            @this.MoveTo(filePath);
        }

        /// <summary>
        ///     A FileInfo extension method that rename file without extension.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="newName">Name of the new.</param>
        /// ###
        /// <returns>.</returns>
        public static void RenameFileWithoutExtension(this FileInfo @this, String newName)
        {
            String fileName = String.Concat(newName, @this.Extension);
            String filePath = Path.Combine(@this.Directory.FullName, fileName);
            @this.MoveTo(filePath);
        }

        /// <summary>
        ///     Sets file attributes for several files at once
        /// </summary>
        /// <param name="files">The files.</param>
        /// <param name="attributes">The attributes to be set.</param>
        /// <returns>The changed files</returns>
        /// <example>
        ///     <code>
        /// 		var files = directory.GetFiles("*.txt", "*.xml");
        /// 		files.SetAttributes(FileAttributes.Archive);
        /// 	</code>
        /// </example>
        public static FileInfo[] SetAttributes(this FileInfo[] files, FileAttributes attributes)
        {
            foreach (var file in files)
                file.Attributes = attributes;
            return files;
        }

        /// <summary>
        ///     Appends file attributes for several files at once (additive to any existing attributes)
        /// </summary>
        /// <param name="files">The files.</param>
        /// <param name="attributes">The attributes to be set.</param>
        /// <returns>The changed files</returns>
        /// <example>
        ///     <code>
        /// 		var files = directory.GetFiles("*.txt", "*.xml");
        /// 		files.SetAttributesAdditive(FileAttributes.Archive);
        /// 	</code>
        /// </example>
        public static FileInfo[] SetAttributesAdditive(this FileInfo[] files, FileAttributes attributes)
        {
            foreach (var file in files)
                file.Attributes = file.Attributes | attributes;
            return files;
        }

        public static Assembly TryLoadAssembly(this FileInfo fileInfo, out Exception exception, int? retryCount = null,
                    LoadContextUtility loadContext = null)
        {
            Policy policy = retryCount != null
                ? Policy.Handle<IOException>().Retry(retryCount > 0 ? retryCount.Value : 1)
                : Policy.Handle<IOException>().RetryForever();
            try
            {
                var assm = policy.Execute(() =>
                {
                    using (var fs = new FileStream(fileInfo.FullName, FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        return fs.ToAssembly(loadContext);
                    }
                });
                exception = null;
                return assm;
            }
            catch (Exception ex)
            {
                exception = ex;
                return null;
            }
        }

        public static Stream TryLoadStream(this FileInfo fileInfo, out Exception exception, int? retryCount = null)
        {
            Policy policy = retryCount != null
                ? Policy.Handle<IOException>().Retry(retryCount > 0 ? retryCount.Value : 1)
                : Policy.Handle<IOException>().RetryForever();
            try
            {
                var stream = policy.Execute(() => new FileStream(fileInfo.FullName, FileMode.Open, FileAccess.Read, FileShare.Read));
                exception = null;
                return stream;
            }
            catch (Exception ex)
            {
                exception = ex;
                return null;
            }
        }

        public static void TryWhenAccessible(this FileInfo fileInfo, Action action, out Exception exception, int? retryCount = null)
        {

            Policy policy = retryCount != null
               ? Policy.Handle<IOException>().Retry(retryCount > 0 ? retryCount.Value : 1)
               : Policy.Handle<IOException>().RetryForever();
            try
            {
                var fileStream = policy.Execute(() =>
                {
                    using (var fs = new FileStream(fileInfo.FullName, FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        return fs;
                    }
                });
                exception = null;
                if (fileStream != null && fileStream.Length > 0)
                    action();
            }
            catch (Exception ex)
            {
                exception = ex;
            }
        }

        public static void WaitForFileBeReady(this FileInfo file, Action action, int interval = 100)
        {
            var fileReady = false;
            while (!fileReady)
            {
                try
                {
                    using (file.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None))
                    {
                        fileReady = true;
                    }
                }
                catch
                {
                    // ignored
                }
                if (!fileReady)
                    Thread.Sleep(interval);
                else
                {
                    action();
                }
            }
        }

        /// <summary>
        ///     Creates a new file, writes the specified byte array to the file, and then closes the file. If the target file
        ///     already exists, it is overwritten.
        /// </summary>
        /// <param name="this">The file to write to.</param>
        /// <param name="bytes">The bytes to write to the file.</param>
        /// ###
        /// <exception cref="T:System.ArgumentException">
        ///     <paramref name="this" /> is a zero-length System.String, contains only
        ///     white space, or contains one or more invalid characters as defined by
        ///     <see
        ///         cref="F:System.IO.Path.InvalidPathChars" />
        ///     .
        /// </exception>
        /// ###
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="this" /> is null or the byte array is empty.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.PathTooLongException">
        ///     The specified @this, file name, or both exceed the system-
        ///     defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file
        ///     names must be less than 260 characters.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.DirectoryNotFoundException">
        ///     The specified @this is invalid (for example, it is on
        ///     an unmapped drive).
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.IOException">An I/O error occurred while opening the file.</exception>
        /// ###
        /// <exception cref="T:System.UnauthorizedAccessException">
        ///     <paramref name="this" /> specified a file that is
        ///     read-only.-or- This operation is not supported on the current platform.-or-
        ///     <paramref
        ///         name="this" />
        ///     specified a directory.-or- The caller does not have the required permission.
        /// </exception>
        /// ###
        /// <exception cref="T:System.NotSupportedException">
        ///     <paramref name="this" /> is in an invalid format.
        /// </exception>
        /// ###
        /// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
        public static void WriteAllBytes(this FileInfo @this, Byte[] bytes)
        {
            File.WriteAllBytes(@this.FullName, bytes);
        }

        /// <summary>
        ///     Creates a new file, write the specified System.String array to the file, and then closes the file.
        /// </summary>
        /// <param name="this">The file to write to.</param>
        /// <param name="contents">The System.String array to write to the file.</param>
        /// ###
        /// <exception cref="T:System.ArgumentException">
        ///     <paramref name="this" /> is a zero-length System.String, contains only
        ///     white space, or contains one or more invalid characters as defined by
        ///     <see
        ///         cref="F:System.IO.Path.InvalidPathChars" />
        ///     .
        /// </exception>
        /// ###
        /// <exception cref="T:System.ArgumentNullException">
        ///     Either <paramref name="this" /> or
        ///     <paramref name="contents" /> is null.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.PathTooLongException">
        ///     The specified @this, file name, or both exceed the system-
        ///     defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file
        ///     names must be less than 260 characters.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.DirectoryNotFoundException">
        ///     The specified @this is invalid (for example, it is on
        ///     an unmapped drive).
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.IOException">An I/O error occurred while opening the file.</exception>
        /// ###
        /// <exception cref="T:System.UnauthorizedAccessException">
        ///     <paramref name="this" /> specified a file that is
        ///     read-only.-or- This operation is not supported on the current platform.-or-
        ///     <paramref
        ///         name="this" />
        ///     specified a directory.-or- The caller does not have the required permission.
        /// </exception>
        /// ###
        /// <exception cref="T:System.NotSupportedException">
        ///     <paramref name="this" /> is in an invalid format.
        /// </exception>
        /// ###
        /// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
        public static void WriteAllLines(this FileInfo @this, String[] contents)
        {
            File.WriteAllLines(@this.FullName, contents);
        }

        /// <summary>
        ///     Creates a new file, writes the specified System.String array to the file by using the specified encoding, and then
        ///     closes the file.
        /// </summary>
        /// <param name="this">The file to write to.</param>
        /// <param name="contents">The System.String array to write to the file.</param>
        /// <param name="encoding">
        ///     An <see cref="T:System.Text.Encoding" /> object that represents the character encoding
        ///     applied to the System.String array.
        /// </param>
        /// ###
        /// <exception cref="T:System.ArgumentException">
        ///     <paramref name="this" /> is a zero-length System.String, contains only
        ///     white space, or contains one or more invalid characters as defined by
        ///     <see
        ///         cref="F:System.IO.Path.InvalidPathChars" />
        ///     .
        /// </exception>
        /// ###
        /// <exception cref="T:System.ArgumentNullException">
        ///     Either <paramref name="this" /> or
        ///     <paramref name="contents" /> is null.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.PathTooLongException">
        ///     The specified @this, file name, or both exceed the system-
        ///     defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file
        ///     names must be less than 260 characters.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.DirectoryNotFoundException">
        ///     The specified @this is invalid (for example, it is on
        ///     an unmapped drive).
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.IOException">An I/O error occurred while opening the file.</exception>
        /// ###
        /// <exception cref="T:System.UnauthorizedAccessException">
        ///     <paramref name="this" /> specified a file that is
        ///     read-only.-or- This operation is not supported on the current platform.-or-
        ///     <paramref
        ///         name="this" />
        ///     specified a directory.-or- The caller does not have the required permission.
        /// </exception>
        /// ###
        /// <exception cref="T:System.NotSupportedException">
        ///     <paramref name="this" /> is in an invalid format.
        /// </exception>
        /// ###
        /// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
        public static void WriteAllLines(this FileInfo @this, String[] contents, Encoding encoding)
        {
            File.WriteAllLines(@this.FullName, contents, encoding);
        }

        /// <summary>
        ///     Creates a new file, write the specified System.String array to the file, and then closes the file.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="contents">The System.String array to write to the file.</param>
        public static void WriteAllLines(this FileInfo @this, IEnumerable<String> contents)
        {
            File.WriteAllLines(@this.FullName, contents);
        }

        /// <summary>
        ///     Creates a new file, writes the specified System.String array to the file by using the specified encoding, and then
        ///     closes the file.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="contents">The System.String array to write to the file.</param>
        /// <param name="encoding">
        ///     An <see cref="T:System.Text.Encoding" /> object that represents the character encoding
        ///     applied to the System.String array.
        /// </param>
        public static void WriteAllLines(this FileInfo @this, IEnumerable<String> contents, Encoding encoding)
        {
            File.WriteAllLines(@this.FullName, contents, encoding);
        }

        /// <summary>
        ///     Creates a new file, writes the specified System.String to the file, and then closes the file. If the target file
        ///     already exists, it is overwritten.
        /// </summary>
        /// <param name="this">The file to write to.</param>
        /// <param name="contents">The System.String to write to the file.</param>
        /// ###
        /// <exception cref="T:System.ArgumentException">
        ///     <paramref name="this" /> is a zero-length System.String, contains only
        ///     white space, or contains one or more invalid characters as defined by
        ///     <see
        ///         cref="F:System.IO.Path.InvalidPathChars" />
        ///     .
        /// </exception>
        /// ###
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="this" /> is null or
        ///     <paramref name="contents" /> is empty.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.PathTooLongException">
        ///     The specified @this, file name, or both exceed the system-
        ///     defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file
        ///     names must be less than 260 characters.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.DirectoryNotFoundException">
        ///     The specified @this is invalid (for example, it is on
        ///     an unmapped drive).
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.IOException">An I/O error occurred while opening the file.</exception>
        /// ###
        /// <exception cref="T:System.UnauthorizedAccessException">
        ///     <paramref name="this" /> specified a file that is
        ///     read-only.-or- This operation is not supported on the current platform.-or-
        ///     <paramref
        ///         name="this" />
        ///     specified a directory.-or- The caller does not have the required permission.
        /// </exception>
        /// ###
        /// <exception cref="T:System.NotSupportedException">
        ///     <paramref name="this" /> is in an invalid format.
        /// </exception>
        /// ###
        /// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
        public static void WriteAllText(this FileInfo @this, String contents)
        {
            File.WriteAllText(@this.FullName, contents);
        }

        /// <summary>
        ///     Creates a new file, writes the specified System.String to the file using the specified encoding, and then closes the
        ///     file. If the target file already exists, it is overwritten.
        /// </summary>
        /// <param name="this">The file to write to.</param>
        /// <param name="contents">The System.String to write to the file.</param>
        /// <param name="encoding">The encoding to apply to the System.String.</param>
        /// ###
        /// <exception cref="T:System.ArgumentException">
        ///     <paramref name="this" /> is a zero-length System.String, contains only
        ///     white space, or contains one or more invalid characters as defined by
        ///     <see
        ///         cref="F:System.IO.Path.InvalidPathChars" />
        ///     .
        /// </exception>
        /// ###
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="this" /> is null or
        ///     <paramref name="contents" /> is empty.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.PathTooLongException">
        ///     The specified @this, file name, or both exceed the system-
        ///     defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file
        ///     names must be less than 260 characters.
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.DirectoryNotFoundException">
        ///     The specified @this is invalid (for example, it is on
        ///     an unmapped drive).
        /// </exception>
        /// ###
        /// <exception cref="T:System.IO.IOException">An I/O error occurred while opening the file.</exception>
        /// ###
        /// <exception cref="T:System.UnauthorizedAccessException">
        ///     <paramref name="this" /> specified a file that is
        ///     read-only.-or- This operation is not supported on the current platform.-or-
        ///     <paramref
        ///         name="this" />
        ///     specified a directory.-or- The caller does not have the required permission.
        /// </exception>
        /// ###
        /// <exception cref="T:System.NotSupportedException">
        ///     <paramref name="this" /> is in an invalid format.
        /// </exception>
        /// ###
        /// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission.</exception>
        public static void WriteAllText(this FileInfo @this, String contents, Encoding encoding)
        {
            File.WriteAllText(@this.FullName, contents, encoding);
        }
    }
}
