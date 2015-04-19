using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CDPW.BLL;

namespace CDPW.DAL
{
    public class Users
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        #region "---   USER LOGIN   ---"

            // 
            /// <summary>
            /// Checking if the user it was activated / account confirmed.
            /// </summary>
            /// <param name="username">Username to check for.</param>
            /// <param name="password">The password to check for.</param>
            public static DataSet Login_Authenticate(string username, string password)
            {
                if (log.IsInfoEnabled) log.Info(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Enter");
                DataSet ds;

                using (SqlConnection dbConnection = new SqlConnection(DBConnection.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("WAppsUser_Login", dbConnection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add parameters to SPROC
                    cmd.Parameters.Add("@Action", SqlDbType.TinyInt).Value = 99;
                    cmd.Parameters.Add("@username", SqlDbType.NVarChar, 50).Value = username;
                    cmd.Parameters.Add("@userpasswd", SqlDbType.NVarChar, 50).Value = password;

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
        #endregion

        #region "---   USER REGISTRATION   ---"

            /// <summary>
            /// Check if there is another account with the same username.
            /// </summary>
            /// <param name="username">The username to check after.</param>
            public static bool Check_Username(string username)
            {
                if (log.IsInfoEnabled) log.Info(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Enter");
                bool userExists = false;

                using (SqlConnection dbConnection = new SqlConnection(DBConnection.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("WAppsUser_Add", dbConnection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add parameters to SPROC
                    cmd.Parameters.Add("@Action", SqlDbType.TinyInt).Value = 0;
                    cmd.Parameters.Add("@username", SqlDbType.NVarChar, 50).Value = username;

                    dbConnection.Open();

                    SqlDataReader drUser = cmd.ExecuteReader();
                    if (drUser.HasRows) { userExists = true; }

                    drUser.Close();
                }
                if (log.IsInfoEnabled) log.Info(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Exit");
                return userExists;
            }

            /// <summary>
            /// Check if there is another account with the same email address.
            /// </summary>
            /// <param name="email">The e-mail to check after.</param>
            public static bool Check_Email(string email)
            {
                if (log.IsInfoEnabled) log.Info(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Enter");
                bool emailExists = false;

                using (SqlConnection dbConnection = new SqlConnection(DBConnection.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("WAppsUser_Add", dbConnection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add parameters to SPROC
                    cmd.Parameters.Add("@Action", SqlDbType.TinyInt).Value = 1;
                    cmd.Parameters.Add("@useremail", SqlDbType.NVarChar, 50).Value = email;

                    dbConnection.Open();

                    SqlDataReader drEmail = cmd.ExecuteReader();
                    if (drEmail.HasRows)
                    {   emailExists = true;
                    }
                        

                    drEmail.Close();
                }
                if (log.IsInfoEnabled) log.Info(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Exit");
                return emailExists;
            }


            public static bool Check_Email(string email, ref string uName)
            {
                if (log.IsInfoEnabled) log.Info(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Enter");
                bool emailExists = false;

                using (SqlConnection dbConnection = new SqlConnection(DBConnection.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("WAppsUser_Add", dbConnection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add parameters to SPROC
                    cmd.Parameters.Add("@Action", SqlDbType.TinyInt).Value = 1;
                    cmd.Parameters.Add("@useremail", SqlDbType.NVarChar, 50).Value = email;

                    dbConnection.Open();

                    SqlDataReader drEmail = cmd.ExecuteReader();
                    drEmail.Read(); 
                    if (drEmail.HasRows)
                    {
                        emailExists = true;
                        uName = String.IsNullOrEmpty(TextUtils.ReturnFromDB(drEmail["FirstName"].ToString())) ? TextUtils.ReturnFromDB(drEmail["WAppUserName"].ToString()) : TextUtils.ReturnFromDB(drEmail["FirstName"].ToString());
                    }


                    drEmail.Close();
                }
                if (log.IsInfoEnabled) log.Info(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Exit");
                return emailExists;
            }

            public static bool Check_Email(string email,out bool EmailActivated)
            {
                if (log.IsInfoEnabled) log.Info(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Enter");
                
                bool emailExists = false;
                EmailActivated = false;

                using (SqlConnection dbConnection = new SqlConnection(DBConnection.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("WAppsUser_Add", dbConnection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add parameters to SPROC
                    cmd.Parameters.Add("@Action", SqlDbType.TinyInt).Value = 1;
                    cmd.Parameters.Add("@useremail", SqlDbType.NVarChar, 50).Value = email;

                    dbConnection.Open();

                    SqlDataReader drEmail = cmd.ExecuteReader();
                    while (drEmail.Read())
                    {
                        emailExists = true;
                        EmailActivated = String.IsNullOrWhiteSpace(TextUtils.ReturnFromDB(drEmail["BeginDate"].ToString())) ? false : true;
                    }

                    drEmail.Close();
                }
                if (log.IsInfoEnabled) log.Info(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Exit");
                return emailExists;
            }


            // Add user in database
            /// <summary>
            /// Add user in database.
            /// </summary>
            /// <param name="username">Username chosen by user.</param>
            /// <param name="email">E-mail address.</param>
            /// <param name="password">Password to access the account.</param>
            public static void Add(string username, string email, string password)
            {
                if (log.IsInfoEnabled) log.Info(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Enter");
                using (SqlConnection dbConnection = new SqlConnection(DBConnection.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("WAppsUser_Add", dbConnection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add parameters to SPROC
                    cmd.Parameters.Add("@Action", SqlDbType.TinyInt).Value = 2;
                    cmd.Parameters.Add("@username", SqlDbType.NVarChar, 50).Value = username;
                    cmd.Parameters.Add("@useremail", SqlDbType.NVarChar, 50).Value = email;
                    cmd.Parameters.Add("@userpasswd", SqlDbType.NVarChar, 50).Value = password;

                    dbConnection.Open();

                    cmd.ExecuteNonQuery();
                }
                if (log.IsInfoEnabled) log.Info(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Exit");
            }
        #endregion

        #region "---   ACCOUNT CONFIRMATION   ---"
            public static void UserAccount_Confirm(string email)
            {
                if (log.IsInfoEnabled) log.Info(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Enter");
                using (SqlConnection dbConnection = new SqlConnection(DBConnection.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("WAppsUser_Login", dbConnection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add parameters to SPROC
                    cmd.Parameters.Add("@Action", SqlDbType.TinyInt).Value = 1;
                    cmd.Parameters.Add("@useremail", SqlDbType.NVarChar, 50).Value = email;

                    dbConnection.Open();
                    cmd.ExecuteNonQuery();
                }
                if (log.IsInfoEnabled) log.Info(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Exit");
            }
        #endregion

            #region "---   PASSWORD RESET   ---"
                public static void Password_Reset(string Email, string Password)
                {
                    if (log.IsInfoEnabled) log.Info(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Enter");
                    using (SqlConnection dbConnection = new SqlConnection(DBConnection.ConnectionString))
                    {
                        SqlCommand cmd = new SqlCommand("WAppsUser_Login", dbConnection);
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Add parameters to SPROC
                        cmd.Parameters.Add("@Action", SqlDbType.TinyInt).Value = 2;
                        cmd.Parameters.Add("@useremail", SqlDbType.NVarChar, 50).Value = Email;
                        cmd.Parameters.Add("@userpasswd", SqlDbType.NVarChar, 50).Value = Password;

                        dbConnection.Open();
                        cmd.ExecuteNonQuery();
                    }
                    if (log.IsInfoEnabled) log.Info(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Exit");
                }
            #endregion

            #region "---   USER NO SIGNUP   ---"

            // 
            /// <summary>
            /// Adding a no signup user
            /// </summary>
            /// <param name="username">Username to check for.</param>
            /// <param name="password">The password to check for.</param>
            public static DataSet Add_NoUser()
            {
                if (log.IsInfoEnabled) log.Info(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Enter");
                DataSet ds;

                using (SqlConnection dbConnection = new SqlConnection(DBConnection.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("WAppsNoUser_Add", dbConnection);
                    cmd.CommandType = CommandType.StoredProcedure;


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
            #endregion

    }
}
