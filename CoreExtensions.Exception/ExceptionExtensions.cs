using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CoreExtensions
{
    public static class ExceptionExtensions
    {
        public static string AsyncTrace(this Exception ex)
        {
            if (!(ex.Data["_AsyncStackTrace"] is IList<string> trace))
                return string.Empty;
            return string.Join("\n", trace);
        }

        public static string FullTrace(this Exception ex)
        {
            if (!(ex.Data["_AsyncStackTrace"] is IList<string> trace))
                return string.Empty;
            return string.Format("{0}\n--- Async stack trace:\n\t", ex)
                   + string.Join("\n\t", trace);
        }

        public static string ToLogString(this Exception ex, string additionalMessage)
        {
            StringBuilder msg = new StringBuilder();
            if (!string.IsNullOrEmpty(additionalMessage))
            {
                msg.Append(additionalMessage);
                msg.Append(Environment.NewLine);
            }
            if (ex != null)
            {
                try
                {
                    msg.Append(Environment.NewLine);
                    Exception orgEx = ex;
                    msg.Append("Exception:");
                    msg.Append(Environment.NewLine);
                    while (orgEx != null)
                    {
                        msg.Append(orgEx.Message);
                        msg.Append(Environment.NewLine);
                        orgEx = orgEx.InnerException;
                    }
                    if (ex.Data != null)
                    {
                        foreach (object i in ex.Data)
                        {
                            msg.Append(Environment.NewLine);
                            msg.Append("Data :");
                            msg.Append(i.ToString());
                            msg.Append(Environment.NewLine);
                        }
                    }
                    if (ex.StackTrace != null)
                    {
                        msg.Append(Environment.NewLine);
                        msg.Append("StackTrace:");
                        msg.Append(Environment.NewLine);
                        msg.Append(ex.StackTrace.ToString());
                        msg.Append(Environment.NewLine);
                    }
                    if (ex.Source != null)
                    {
                        msg.Append(Environment.NewLine);
                        msg.Append("Source:");
                        msg.Append(Environment.NewLine);
                        msg.Append(ex.Source);
                        msg.Append(Environment.NewLine);
                    }
                    if (ex.TargetSite != null)
                    {
                        msg.Append(Environment.NewLine);
                        msg.Append("TargetSite:");
                        msg.Append(Environment.NewLine);
                        msg.Append(ex.TargetSite.ToString());
                        msg.Append(Environment.NewLine);
                    }
                    Exception baseException = ex.GetBaseException();
                    if (baseException != null)
                    {
                        msg.Append(Environment.NewLine);
                        msg.Append("BaseException:");
                        msg.Append(Environment.NewLine);
                        msg.Append(ex.GetBaseException());
                    }
                }
                finally
                {
                }
            }
            return msg.ToString();
        }

        public static void ToLogText(this Exception ex, string additionalMessage, string path, bool append = false)
        {
            string log = ex.ToLogString(additionalMessage);

            TextWriter textWriter = new StreamWriter(path, append);
            textWriter.Write(log);
            textWriter.Close();
        }
    }
}
