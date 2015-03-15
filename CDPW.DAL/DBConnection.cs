using System;
using System.Configuration;
using System.Web.Configuration;

namespace CDPW.DAL
{
    public class DBConnection
    {
        /// <summary>
        /// Returning the database connection string.
        /// </summary>
        public static string ConnectionString
        {
            get
            {
                var conf = WebConfigurationManager.OpenWebConfiguration(System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath);
                var connections = (ConnectionStringsSection)conf.GetSection("connectionStrings");
                var connString = connections.ConnectionStrings["cdpWebConn"] != null ? connections.ConnectionStrings["cdpWebConn"].ConnectionString.ToString() : string.Empty;

                return connString;
            }
        }

    }
}
