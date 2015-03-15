using System;
using System.Data;
using System.Data.SqlClient;

namespace CDPW.DAL
{
    public class Lists
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        /// <summary>
        /// Generates a DATASET with Countries to be used in forms.
        /// </summary>
        public static DataSet Countries()
        {
            if (log.IsInfoEnabled) log.Info(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Enter");
            DataSet ds;

            using (SqlConnection dbConnection = new SqlConnection(DBConnection.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("Country_Province_List", dbConnection);
                cmd.CommandType = CommandType.StoredProcedure;

                // Add parameters to SPROC
                cmd.Parameters.Add("@Action", SqlDbType.TinyInt).Value = 99;

                dbConnection.Open();

                SqlDataAdapter daUser = new SqlDataAdapter(cmd);
                ds = new DataSet();
                daUser.Fill(ds);

                daUser.Dispose();
                cmd.Dispose();
            }
            if (log.IsInfoEnabled) log.Info(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Exit");
            return ds;
        }


        /// <summary>
        /// Generates a DATASET with Regions to be used in forms.
        /// </summary>
        /// <param name="CountryID">The country ID for which the list will be generated.</param>
        public static DataSet Regions(int CountryID)
        {
            if (log.IsInfoEnabled) log.Info(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Enter");
            DataSet ds;

            using (SqlConnection dbConnection = new SqlConnection(DBConnection.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("Country_Province_List", dbConnection);
                cmd.CommandType = CommandType.StoredProcedure;

                // Add parameters to SPROC
                cmd.Parameters.Add("@Action", SqlDbType.TinyInt).Value = 0;
                cmd.Parameters.Add("@CountryId", SqlDbType.SmallInt).Value = CountryID;

                dbConnection.Open();

                SqlDataAdapter daUser = new SqlDataAdapter(cmd);
                ds = new DataSet();
                daUser.Fill(ds);

                daUser.Dispose();
                cmd.Dispose();
            }
            if (log.IsInfoEnabled) log.Info(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Exit");
            return ds;
        }
    }
}
