using System;
using System.Data;
using System.Data.SqlClient;
using CDPW.BLL;

namespace CDPW.DAL
{
    public class FormUSA
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        #region "---   FORM - ADD INFO IN DATABASE"
        /// <summary>
        /// Add general info from USA form.
        /// </summary>
        public static string Form_AddInfo(Int64 userID, Int64 UPId, string userName, string StreetAddress, string City, string ProvinceID, string NoFamMembers, string CountryResidence, string ContriesVisited, string ContriesVisited2, string TransportationId, string TripPPurposeBusiness, string BringFruits, string BringMeats, string BringDiseaseAgents, string BringSoil, string Livestock, string CurrencyValue, string CommercialMerchandise, string RGoodsValue, string VGoodsValue, string TripDate)
        {
            if (log.IsInfoEnabled) log.Info(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Enter");
            Int64 TripInfoID;
            Int32 AddressID;
            string WAppUserIdNoSignUp;

            using (SqlConnection dbConnection = new SqlConnection(DBConnection.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("USAForm_Add", dbConnection);
                cmd.CommandType = CommandType.StoredProcedure;

                // Add parameters to SPROC
                cmd.Parameters.Add("@WAppUserId", SqlDbType.BigInt).Value = userID;
                cmd.Parameters.Add("@PId", SqlDbType.BigInt).Value = UPId;
                cmd.Parameters.Add("@WAppUserName", SqlDbType.NVarChar, 50).Value = userName;
                //
                cmd.Parameters.Add("@AddressName", SqlDbType.NVarChar, 200).Value = StreetAddress;
                cmd.Parameters.Add("@CityName", SqlDbType.NVarChar, 50).Value = City;
                cmd.Parameters.Add("@ProvinceID", SqlDbType.Int).Value = Convert.ToInt32(ProvinceID);
                //cmd.Parameters.Add("@CountryId", SqlDbType.Int).Value = Convert.ToInt32(Country);
                //
                cmd.Parameters.Add("@NoFamMembers", SqlDbType.TinyInt).Value = TextUtils.ReturnToDB(NoFamMembers); // string.IsNullOrEmpty(NoFamMembers) ? 0 : Convert.ToInt16(NoFamMembers);
                cmd.Parameters.Add("@CountryResidence", SqlDbType.SmallInt).Value = Convert.ToInt16(CountryResidence);
                cmd.Parameters.Add("@ContriesVisited", SqlDbType.NVarChar, 300).Value = TextUtils.ReturnToDB(ContriesVisited);
                cmd.Parameters.Add("@ContriesVisited2", SqlDbType.NVarChar, 300).Value = TextUtils.ReturnToDB(ContriesVisited2);
                cmd.Parameters.Add("@TransportationId", SqlDbType.NVarChar, 100).Value = TextUtils.ReturnToDB(TransportationId);
                cmd.Parameters.Add("@TripPPurposeBusiness", SqlDbType.Char, 1).Value = TripPPurposeBusiness;
                cmd.Parameters.Add("@BringFruits", SqlDbType.Char, 1).Value = BringFruits;
                cmd.Parameters.Add("@BringMeats", SqlDbType.Char, 1).Value = BringMeats;
                cmd.Parameters.Add("@BringDiseaseAgents", SqlDbType.Char, 1).Value = BringDiseaseAgents;
                cmd.Parameters.Add("@BringSoil", SqlDbType.Char, 1).Value = BringSoil;
                cmd.Parameters.Add("@Livestock", SqlDbType.SmallInt).Value = Convert.ToInt16(Livestock);
                cmd.Parameters.Add("@CurrencyValue", SqlDbType.Char, 1).Value = CurrencyValue;
                cmd.Parameters.Add("@CommercialMerchandise", SqlDbType.Char, 1).Value = CommercialMerchandise;
                cmd.Parameters.Add("@RGoodsValue", SqlDbType.SmallMoney).Value = TextUtils.ReturnToDB(RGoodsValue, false, false, false, true);
                cmd.Parameters.Add("@VGoodsValue", SqlDbType.SmallMoney).Value = TextUtils.ReturnToDB(VGoodsValue, false, false, false, true);
                cmd.Parameters.Add("@TripDate", SqlDbType.Date).Value = TextUtils.ReturnToDB(TripDate, false, false, true); //( DateTime.Parse(TripDate, new System.Globalization.CultureInfo("en-US", true));
                //
                cmd.Parameters.Add("@WTripInfoUSAId", SqlDbType.BigInt).Direction = ParameterDirection.Output;
                //cmd.Parameters.Add("@AddressID", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@WAppUserIdNoSignUp", SqlDbType.BigInt).Direction = ParameterDirection.Output;

                dbConnection.Open();
                cmd.ExecuteNonQuery();

                TripInfoID = (Int64)cmd.Parameters["@WTripInfoUSAId"].Value;
                AddressID = 0; //(Int32)cmd.Parameters["@AddressID"].Value;
                WAppUserIdNoSignUp = string.IsNullOrEmpty(cmd.Parameters["@WAppUserIdNoSignUp"].Value.ToString()) ? "0" : cmd.Parameters["@WAppUserIdNoSignUp"].Value.ToString();
            }
            if (log.IsInfoEnabled) log.Info(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Exit");
            return string.Format("{0},{1},{2}", TripInfoID, AddressID, WAppUserIdNoSignUp);
        }




        /// <summary>
        /// ADD INFO ABOUT PERSONS.
        /// </summary>
        public static void Form_AddInfo_Person(Int64 userID, Int64 UPId , string LastName, string FirstName, string MiddleName, string DoB, string CountryPassport, string PassportIssueAuthority, string PassportNo, string TripInfoDetail1, string TripInfoDetailValue1, string TripInfoDetail2, string TripInfoDetailValue2, string TripInfoDetail3, string TripInfoDetailValue3, string TripInfoDetail4, string TripInfoDetailValue4, string TripInfoDetail5, string TripInfoDetailValue5, string TripInfoDetail6, string TripInfoDetailValue6, string AddressID, string TripInfoID)
        {
            if (log.IsInfoEnabled) log.Info(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Enter");
            using (SqlConnection dbConnection = new SqlConnection(DBConnection.ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("USAForm_Add_Person", dbConnection);
                cmd.CommandType = CommandType.StoredProcedure;

                // Add parameters to SPROC
                cmd.Parameters.Add("@WAppUserId", SqlDbType.BigInt).Value = userID;
                cmd.Parameters.Add("@PId", SqlDbType.BigInt).Value = UPId;
                //
                //string[] person1 = person1Name.Split(' ');
                cmd.Parameters.Add("@LastName", SqlDbType.NVarChar, 50).Value = LastName;
                cmd.Parameters.Add("@FirstName", SqlDbType.NVarChar, 50).Value = FirstName;
                cmd.Parameters.Add("@MiddleName", SqlDbType.NVarChar, 50).Value = TextUtils.ReturnToDB(MiddleName);
                cmd.Parameters.Add("@DoB", SqlDbType.Date).Value = TextUtils.ReturnToDB(DoB, true); // DateTime.Parse(DoB, new System.Globalization.CultureInfo("en-US", true));
                //
                cmd.Parameters.Add("@CountryPassport", SqlDbType.SmallInt).Value = Convert.ToInt16(CountryPassport);
                cmd.Parameters.Add("@PassportIssueAuthority", SqlDbType.NVarChar, 50).Value = TextUtils.ReturnToDB(PassportIssueAuthority);
                cmd.Parameters.Add("@PassportNo", SqlDbType.NVarChar, 50).Value = PassportNo; //string.IsNullOrEmpty(PassportNo) ? "" : PassportNo;
                //
                //if (!string.IsNullOrEmpty(TripInfoDetail1)){
                
                    cmd.Parameters.Add("@TripInfoDetail1", SqlDbType.NVarChar, 50).Value = TextUtils.ReturnToDB(TripInfoDetail1);
                    cmd.Parameters.Add("@TripInfoDetailValue1", SqlDbType.SmallMoney).Value = TextUtils.ReturnToDB(TripInfoDetailValue1, false, false, false, true);
                //}
                //
                //if (!string.IsNullOrEmpty(TripInfoDetail2)){
                
                    cmd.Parameters.Add("@TripInfoDetail2", SqlDbType.NVarChar, 50).Value = TextUtils.ReturnToDB(TripInfoDetail2);
                    cmd.Parameters.Add("@TripInfoDetailValue2", SqlDbType.SmallMoney).Value = TextUtils.ReturnToDB(TripInfoDetailValue2, false, false, false, true);
                //}
                //
                //if (!string.IsNullOrEmpty(TripInfoDetail3)){
                
                    cmd.Parameters.Add("@TripInfoDetail3", SqlDbType.NVarChar, 50).Value = TextUtils.ReturnToDB(TripInfoDetail3);
                    cmd.Parameters.Add("@TripInfoDetailValue3", SqlDbType.SmallMoney).Value = TextUtils.ReturnToDB(TripInfoDetailValue3, false, false, false, true);
                //}
                //
                //if (!string.IsNullOrEmpty(TripInfoDetail4)){
                
                    cmd.Parameters.Add("@TripInfoDetail4", SqlDbType.NVarChar, 50).Value = TextUtils.ReturnToDB(TripInfoDetail4);
                    cmd.Parameters.Add("@TripInfoDetailValue4", SqlDbType.SmallMoney).Value = TextUtils.ReturnToDB(TripInfoDetailValue4, false, false, false, true);
                //}
                //
                //if (!string.IsNullOrEmpty(TripInfoDetail5)){
                
                    cmd.Parameters.Add("@TripInfoDetail5", SqlDbType.NVarChar, 50).Value = TextUtils.ReturnToDB(TripInfoDetail5);
                    cmd.Parameters.Add("@TripInfoDetailValue5", SqlDbType.SmallMoney).Value = TextUtils.ReturnToDB(TripInfoDetailValue5, false, false, false, true);
                //}
                //
                //if (!string.IsNullOrEmpty(TripInfoDetail6)){
                
                    cmd.Parameters.Add("@TripInfoDetail6", SqlDbType.NVarChar, 50).Value = TextUtils.ReturnToDB(TripInfoDetail6);
                    cmd.Parameters.Add("@TripInfoDetailValue6", SqlDbType.SmallMoney).Value = TextUtils.ReturnToDB(TripInfoDetailValue6, false, false, false, true);
                //}
                
                //
                //cmd.Parameters.Add("@AddressID", SqlDbType.Int).Value = Convert.ToInt32(AddressID);
                cmd.Parameters.Add("@WTripInfoUSAId", SqlDbType.BigInt).Value = Convert.ToInt64(TripInfoID);

                dbConnection.Open();
                cmd.ExecuteNonQuery();
            }
            if (log.IsInfoEnabled) log.Info(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Exit");
        }

        #endregion


        #region "---   FORM - SHOW INFO ---"

            /// <summary>
            /// Get info from DB and fill the form.
            /// </summary>
            /// <param name="WAppUserId">Web app user id.</param>
            public static DataSet Form_Get(Int64 WAppUserId, Int64 PGId)
            {
                if (log.IsInfoEnabled) log.Info(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Enter");
                DataSet dsUsaForm;

                using (SqlConnection dbConnection = new SqlConnection(DBConnection.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("USAForm_Get", dbConnection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add parameters to SPROC
                    cmd.Parameters.Add("@WAppUserId", SqlDbType.BigInt).Value = WAppUserId;
                    cmd.Parameters.Add("@PGId", SqlDbType.BigInt).Value = PGId;

                    dbConnection.Open();

                    SqlDataAdapter daUsaForm = new SqlDataAdapter(cmd);
                    dsUsaForm = new DataSet();
                    daUsaForm.Fill(dsUsaForm);

                    daUsaForm.Dispose();
                    cmd.Dispose();
                }
                if (log.IsInfoEnabled) log.Info(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Exit");
                return dsUsaForm;
            }
        #endregion


        // helper function for inserting records in DB
        //private static object ReturnToDB(string text)
        //{
        //    var toReturn = new object();
        //    if (string.IsNullOrEmpty(text))
        //    {
        //        toReturn = DBNull.Value;
        //    }
        //    else
        //    {
        //        toReturn = text;
        //    }

        //    return toReturn;
        //}
    }
}
