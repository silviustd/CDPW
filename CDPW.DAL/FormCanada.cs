using System;
using System.Data;
using System.Data.SqlClient;
using CDPW.BLL;

namespace CDPW.DAL
{
    public class FormCanada
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        #region "FORM - ADD INFO IN DATABASE"

            /// <summary>
            /// Add general info from Canada form.
            /// </summary>
            public static string Form_AddInfo(Int64 userID, Int64[] PIds ,string userName, string StreetAddress, string City, string Province, string Country, string PostalCode, string ArrivingBy, string TranspId ,string PurposeTrip, string ArrivingFrom, string Firearms, string CommercialGoods, string MeatProducts, string Currency, string GoodsShipped, string FarmVisit, string Duration, string ExceededDutyFree, string ExceededExemptions, String dDate = "")
            {
                if (log.IsInfoEnabled) log.Info(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Enter");
                Int64 TripInfoID;
                Int32 AddressID;
                string WAppUserIdNoSignUp;

                using (SqlConnection dbConnection = new SqlConnection(DBConnection.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("CanadaForm_Add", dbConnection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add parameters to SPROC
                    cmd.Parameters.Add("@WAppUserId", SqlDbType.BigInt).Value = userID;
                    cmd.Parameters.Add("@P1Id", SqlDbType.BigInt).Value = PIds[0];
                    //PIds
                    cmd.Parameters.Add("@WAppUserName", SqlDbType.NVarChar, 50).Value = userName;
                    //
                    cmd.Parameters.Add("@AddressName", SqlDbType.NVarChar, 200).Value = TextUtils.ReturnToDB(StreetAddress);
                    cmd.Parameters.Add("@CityName", SqlDbType.NVarChar, 50).Value = TextUtils.ReturnToDB(City);
                    cmd.Parameters.Add("@ProvinceID", SqlDbType.Int).Value = TextUtils.ReturnIntToDB(Province, false, true);
                    cmd.Parameters.Add("@CountryId", SqlDbType.Int).Value = TextUtils.ReturnIntToDB(Country, false, true);
                    cmd.Parameters.Add("@PostalCode", SqlDbType.NVarChar, 10).Value = TextUtils.ReturnToDB(PostalCode);
                    //
                    cmd.Parameters.Add("@ArrivingBy", SqlDbType.TinyInt).Value = TextUtils.ReturnIntToDB(ArrivingBy, true);
                    cmd.Parameters.Add("@TransportationId", SqlDbType.NVarChar, 100).Value = TextUtils.ReturnToDB(TranspId);
                    cmd.Parameters.Add("@PurposeTrip", SqlDbType.TinyInt).Value = TextUtils.ReturnIntToDB(PurposeTrip, true);
                    cmd.Parameters.Add("@ArrivingFrom", SqlDbType.TinyInt).Value = TextUtils.ReturnIntToDB(ArrivingBy, true);
                    cmd.Parameters.Add("@Firearms", SqlDbType.Char, 1).Value = TextUtils.ReturnBitToDB(Firearms);
                    cmd.Parameters.Add("@CommercialGoods", SqlDbType.Char, 1).Value = TextUtils.ReturnBitToDB(CommercialGoods);
                    cmd.Parameters.Add("@MeatProducts", SqlDbType.Char, 1).Value = TextUtils.ReturnBitToDB(MeatProducts);
                    cmd.Parameters.Add("@Currency", SqlDbType.Char, 1).Value = TextUtils.ReturnBitToDB(Currency);
                    cmd.Parameters.Add("@GoodsShipped", SqlDbType.Char, 1).Value = TextUtils.ReturnBitToDB(GoodsShipped);
                    cmd.Parameters.Add("@FarmVisit", SqlDbType.Char, 1).Value = TextUtils.ReturnBitToDB(FarmVisit);
                    cmd.Parameters.Add("@Duration", SqlDbType.SmallInt).Value = TextUtils.ReturnIntToDB(Duration, true);
                    cmd.Parameters.Add("@ExceededDutyFree", SqlDbType.Char, 1).Value = TextUtils.ReturnBitToDB(ExceededDutyFree);
                    cmd.Parameters.Add("@ExceededExemptions", SqlDbType.Char, 1).Value = TextUtils.ReturnBitToDB(ExceededExemptions);
                    cmd.Parameters.Add("@dDate", SqlDbType.SmallDateTime).Value = TextUtils.ReturnToDB(dDate, true);
                    //
                    cmd.Parameters.Add("@WTripInfoCANId", SqlDbType.BigInt).Direction = ParameterDirection.Output;
                    //cmd.Parameters.Add("@AddressID", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@WAppUserIdNoSignUp", SqlDbType.BigInt).Direction = ParameterDirection.Output;

                    dbConnection.Open();
                    cmd.ExecuteNonQuery();

                    TripInfoID = (Int64)cmd.Parameters["@WTripInfoCANId"].Value;
                    AddressID = 0; //(Int32)cmd.Parameters["@AddressID"].Value;
                    WAppUserIdNoSignUp = string.IsNullOrEmpty(cmd.Parameters["@WAppUserIdNoSignUp"].Value.ToString()) ? "0" : cmd.Parameters["@WAppUserIdNoSignUp"].Value.ToString();
                }
                if (log.IsInfoEnabled) log.Info(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Exit");
                return string.Format("{0},{1},{2}", TripInfoID, AddressID, WAppUserIdNoSignUp);
            }


            /// <summary>
            /// ADD INFO ABOUT PERSONS.
            /// </summary>
            public static void Form_AddInfo_Person(Int64 userID, Int64[] PIds, string person1Name, string person1DoB, string person1Citizenship, string person1DateLeft, string person1ValueGoods, string person2Name, string person2DoB, string person2Citizenship, string person2DateLeft, string person2ValueGoods, string person3Name, string person3DoB, string person3Citizenship, string person3DateLeft, string person3ValueGoods, string person4Name, string person4DoB, string person4Citizenship, string person4DateLeft, string person4ValueGoods, int AddressID, Int64 TripInfoID)
            {
                if (log.IsInfoEnabled) log.Info(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Enter");
                using (SqlConnection dbConnection = new SqlConnection(DBConnection.ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("CanadaForm_Add_Person", dbConnection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add parameters to SPROC
                    cmd.Parameters.Add("@WAppUserId", SqlDbType.BigInt).Value = userID;
                    //
                    //if (!string.IsNullOrEmpty(person1Name)){
                    
                        string[] person1 = person1Name.Trim().Split(' ');
                        cmd.Parameters.Add("@P1Id", SqlDbType.BigInt).Value = PIds[0]; 
                        cmd.Parameters.Add("@person1LastName", SqlDbType.NVarChar, 50).Value = person1[0];
                        cmd.Parameters.Add("@person1FirstName", SqlDbType.NVarChar, 50).Value = (person1.Length > 1) ? person1[1] : string.Empty;
                        cmd.Parameters.Add("@person1MiddleName", SqlDbType.NVarChar, 50).Value = (person1.Length > 2) ? person1[2] : string.Empty;
                        cmd.Parameters.Add("@person1DoB", SqlDbType.Date).Value = TextUtils.ReturnToDB(person1DoB, true);
                        cmd.Parameters.Add("@person1Citizenship", SqlDbType.NVarChar, 50).Value = TextUtils.ReturnToDB(person1Citizenship);
                        cmd.Parameters.Add("@person1DateLeft", SqlDbType.Date).Value = TextUtils.ReturnToDB(person1DateLeft, false, true);
                        cmd.Parameters.Add("@person1ValueGoods", SqlDbType.SmallMoney).Value = TextUtils.ReturnToDB(person1ValueGoods, false, false, false, true);
                    //}
                    //
                    //if (!string.IsNullOrEmpty(person2Name)) {
                        string[] person2 = person2Name.Trim().Split(' ');
                        cmd.Parameters.Add("@P2Id", SqlDbType.BigInt).Value = PIds[1]; 
                        cmd.Parameters.Add("@person2LastName", SqlDbType.NVarChar, 50).Value = person2[0];
                        cmd.Parameters.Add("@person2FirstName", SqlDbType.NVarChar, 50).Value = (person2.Length > 1) ? person2[1] : string.Empty; ;
                        cmd.Parameters.Add("@person2MiddleName", SqlDbType.NVarChar, 50).Value = (person2.Length > 2) ? person2[2] : string.Empty;
                        cmd.Parameters.Add("@person2DoB", SqlDbType.Date).Value = TextUtils.ReturnToDB(person2DoB, true);
                        cmd.Parameters.Add("@person2Citizenship", SqlDbType.NVarChar, 50).Value = TextUtils.ReturnToDB(person2Citizenship);
                        cmd.Parameters.Add("@person2DateLeft", SqlDbType.Date).Value = TextUtils.ReturnToDB(person2DateLeft, false, true);
                        cmd.Parameters.Add("@person2ValueGoods", SqlDbType.SmallMoney).Value = TextUtils.ReturnToDB(person2ValueGoods, false, false, false, true);
                    //}
                    //
                    //if (!string.IsNullOrEmpty(person3Name)){
                    
                        string[] person3 = person3Name.Trim().Split(' ');
                        cmd.Parameters.Add("@P3Id", SqlDbType.BigInt).Value = PIds[2]; 
                        cmd.Parameters.Add("@person3LastName", SqlDbType.NVarChar, 50).Value = person3[0];
                        cmd.Parameters.Add("@person3FirstName", SqlDbType.NVarChar, 50).Value = (person3.Length > 1) ? person3[1] : string.Empty;
                        cmd.Parameters.Add("@person3MiddleName", SqlDbType.NVarChar, 50).Value = (person3.Length > 2) ? person3[2] : string.Empty;
                        cmd.Parameters.Add("@person3DoB", SqlDbType.Date).Value = TextUtils.ReturnToDB(person3DoB, true);
                        cmd.Parameters.Add("@person3Citizenship", SqlDbType.NVarChar, 50).Value = TextUtils.ReturnToDB(person3Citizenship);
                        cmd.Parameters.Add("@person3DateLeft", SqlDbType.Date).Value = TextUtils.ReturnToDB(person3DateLeft, false, true);
                        cmd.Parameters.Add("@person3ValueGoods", SqlDbType.SmallMoney).Value = TextUtils.ReturnToDB(person3ValueGoods, false, false, false, true);
                    //}
                    //
                    //if (!string.IsNullOrEmpty(person4Name)){
                        string[] person4 = person4Name.Trim().Split(' ');
                        cmd.Parameters.Add("@P4Id", SqlDbType.BigInt).Value = PIds[3]; 
                        cmd.Parameters.Add("@person4LastName", SqlDbType.NVarChar, 50).Value = person4[0];
                        cmd.Parameters.Add("@person4FirstName", SqlDbType.NVarChar, 50).Value = (person4.Length > 1) ? person4[1] : string.Empty;
                        cmd.Parameters.Add("@person4MiddleName", SqlDbType.NVarChar, 50).Value = (person4.Length > 2) ? person4[2] : string.Empty;
                        cmd.Parameters.Add("@person4DoB", SqlDbType.Date).Value = TextUtils.ReturnToDB(person4DoB, true);
                        cmd.Parameters.Add("@person4Citizenship", SqlDbType.NVarChar, 50).Value = TextUtils.ReturnToDB(person4Citizenship);
                        cmd.Parameters.Add("@person4DateLeft", SqlDbType.Date).Value = TextUtils.ReturnToDB(person4DateLeft, false, true);
                        cmd.Parameters.Add("@person4ValueGoods", SqlDbType.SmallMoney).Value = TextUtils.ReturnToDB(person4ValueGoods, false, false, false, true);
                    //}
                    //
                    //cmd.Parameters.Add("@AddressID", SqlDbType.Int).Value = AddressID;
                    cmd.Parameters.Add("@WTripInfoCANId", SqlDbType.BigInt).Value = TripInfoID;
                    

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
                public static DataSet Form_Get(Int64 WAppUserId, Int64 PGId )
                {
                    if (log.IsInfoEnabled) log.Info(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Enter");
                    DataSet dsCanForm;

                    using (SqlConnection dbConnection = new SqlConnection(DBConnection.ConnectionString))
                    {
                        SqlCommand cmd = new SqlCommand("CanadaForm_Get", dbConnection);
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Add parameters to SPROC
                        cmd.Parameters.Add("@WAppUserId", SqlDbType.BigInt).Value = WAppUserId;
                        cmd.Parameters.Add("@PGId", SqlDbType.BigInt).Value = PGId;

                        dbConnection.Open();

                        SqlDataAdapter daCanForm = new SqlDataAdapter(cmd);
                        dsCanForm = new DataSet();
                        daCanForm.Fill(dsCanForm);

                        daCanForm.Dispose();
                        cmd.Dispose();
                    }
                    if (log.IsInfoEnabled) log.Info(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Exit");
                    return dsCanForm;
                }

            #endregion
    }
}
