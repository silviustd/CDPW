using System;

namespace CDPW.BLL
{
    public class TextUtils
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        #region Text transforming functions
        /// <summary>
        /// Formating data to insert in the database. It checks if the text is empty or null. If it's null, it insert in database DBNull.
        /// </summary>
        /// <param name="text">The text to format.</param>
        public static object ReturnToDB(string text, bool isDateMMddYYYY = false, bool isDateyyyy_mm_dd = false, bool isDateDDmmYYYY = false, bool isCurrency = false, bool isBit = false)
        {
            if (log.IsInfoEnabled) log.Info(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Enter");
            var toReturn = new object();

            if (string.IsNullOrEmpty(text))
            {
                toReturn = DBNull.Value; //string.Empty;
            }
            else
            {
                if (isDateMMddYYYY)
                {
                    //toReturn = Convert.ToDateTime(text).ToString("MM/dd/yyyy", new System.Globalization.CultureInfo("en-US"));
                    string[] dt = text.Trim().Split('/');
                    toReturn = Convert.ToDateTime(dt[2] + "-" + dt[0] + "-" + dt[1]);
                }
                else if (isDateyyyy_mm_dd)
                {
                    toReturn = Convert.ToDateTime(text).ToString("yyyy-MM-dd");
                }
                else if (isDateDDmmYYYY)
                {
                    string[] dt = text.Trim().Split('/');
                    toReturn = Convert.ToDateTime(dt[2] + "-" + dt[1] + "-" + dt[0]);
                    //toReturn = Convert.ToDateTime(text).ToString("dd/MM/yyyy");
                }
                else if (isCurrency)
                {
                    toReturn = Convert.ToDecimal(text, new System.Globalization.CultureInfo("en-US")); //string.Format("{0:#.##}", Convert.ToDecimal(text));
                }
                else if (isBit)
                {
                    toReturn = text.Equals(0) ? false : true;
                }
                else
                {
                    toReturn = text;
                }
            }
            if (log.IsInfoEnabled) log.Info(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Exit");
            return toReturn;
        }

        /// <summary>
        /// Formating data to insert in the database. It checks if the text is empty or null. If it's null, it insert in database DBNull.
        /// </summary>
        /// <param name="text">The text to format.</param>
        public static object ReturnIntToDB(string text, bool isInt16 = false, bool isInt32 = false, bool isInt64 = false, bool isByte = false)
        {
            if (log.IsInfoEnabled) log.Info(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Enter");
            var toReturn = new object();

            if (string.IsNullOrWhiteSpace(text))
            {
                toReturn = DBNull.Value; //string.Empty;
            }
            else
            {
                if (isInt16)
                {
                    toReturn = Convert.ToInt16(text);
                }
                else if (isInt32)
                {
                    toReturn = Convert.ToInt32(text);
                }
                else if (isInt64)
                {
                    toReturn = Convert.ToInt64(text);
                }
                else if (isByte)
                {
                    toReturn = Convert.ToByte(text);
                }
                else
                {
                    toReturn = text;
                }
            }
            if (log.IsInfoEnabled) log.Info(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Exit");
            return toReturn;
        }

        /// <summary>
        /// Formating data received from database.
        /// </summary>
        /// <param name="text">The text to show on page.</param>
        /// <param name="isDate">True if shoudl return a formated date.</param>
        public static string ReturnFromDB(string text, bool isDateMMddYYYY = false, bool isDateyyyy_mm_dd = false, bool isDateDDmmYYYY = false, bool isCurrency = false)
        {
            if (log.IsInfoEnabled) log.Info(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Enter");
            string toReturn = string.Empty;

            if (string.IsNullOrEmpty(text))
            {
                toReturn = string.Empty;
            }
            else
            {
                if (isDateMMddYYYY)
                {
                    toReturn = Convert.ToDateTime(text).ToString("MM/dd/yyyy", new System.Globalization.CultureInfo("en-US"));
                }
                else if (isDateyyyy_mm_dd)
                {
                    toReturn = Convert.ToDateTime(text).ToString("yyyy-MM-dd");
                }
                else if (isDateDDmmYYYY)
                {
                    toReturn = Convert.ToDateTime(text).ToString("dd/MM/yyyy", new System.Globalization.CultureInfo("fr-FR"));
                }
                else if (isCurrency)
                {
                    toReturn = string.Format(new System.Globalization.CultureInfo("en-US"), "{0:#.##}", text);
                    //string.Format("{0:#.00}", text.Replace(",", ".")); 
                    //Convert.ToDecimal(text, new System.Globalization.CultureInfo("en-US")).ToString(); //Convert.ToDecimal(text, new System.Globalization.CultureInfo("en-US")).ToString(); //string.Format("{0:#.##}", Convert.ToDecimal(text));
                }
                else
                {
                    toReturn = text;
                }
            }
            if (log.IsInfoEnabled) log.Info(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Exit");
            return toReturn.ToString();
        }


        /// <summary>
        /// Formating data received from database.
        /// </summary>
        /// <param name="FromDB">Data from database</param>
        public static string ReturnBitFromDB(bool FromDB)
        {
            if (log.IsInfoEnabled) log.Info(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Enter");
            if (log.IsInfoEnabled) log.Info(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Exit");
            return (FromDB) ? "1" : "0";
        }

        public static Object ReturnBitToDB(String ToDB)
        {
            Object _returnBitToDB = DBNull.Value;

            if (log.IsInfoEnabled) log.Info(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name + "-" + System.Reflection.MethodBase.GetCurrentMethod().Name + " - Enter/Exit");

            if (!String.IsNullOrWhiteSpace(ToDB)) {
                _returnBitToDB = Convert.ToByte(ToDB);
            }

            return _returnBitToDB;
        }

        #endregion
    }      
}
