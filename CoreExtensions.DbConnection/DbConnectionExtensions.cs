using System.Data;
using System.Data.Common;

namespace CoreExtensions
{
    public static class DbConnectionExtensions
    {
        public static bool IsServerAvailable(this IDbConnection connection)
        {
            bool status;
            try
            {
                connection.Open();
                status = true;
                connection.Close();
            }
            catch
            {
                status = false;
            }
            return status;
        }

        /// <summary>A DbConnection extension method that queries if a connection is open.</summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>true if a connection is open, false if not.</returns>
        public static bool IsConnectionOpen(this System.Data.Common.DbConnection @this)
        {
            return @this.State == ConnectionState.Open;
        }

        public static void SafeClose(this DbConnection toClose, bool dispose = false)
        {
            if (toClose == null)
            {
                return;
            }
            if (toClose.State != ConnectionState.Closed)
            {
                toClose.Close();
            }
            if (dispose)
            {
                toClose.Dispose();
            }
        }
    }
}
