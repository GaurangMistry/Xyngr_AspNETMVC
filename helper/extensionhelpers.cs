using System;
using System.Globalization;
using System.Web.SessionState;


namespace Xyngr.helper
{
    /// <summary>
    /// Contains various extension methods
    /// </summary>
    public static class ExtensionHelpers
    {
        #region Session Extensions

		/// <summary>
		/// Gets the value.
		/// </summary>
		/// <typeparam name="T">value Selector</typeparam>
		/// <param name="session">The session.</param>
		/// <param name="key">The key.</param>
		/// <param name="valueSelector">The value selector.</param>
		
		public static T GetValue<T>(this HttpSessionState session, string key, Func<object, T> valueSelector)
        {
            return valueSelector(session[key]);
        }

        /// <summary>
        /// Gets the session string.
        /// </summary>
        /// <param name="session">The session.</param>
        /// <param name="key">The key.</param>
        /// <returns>Returns String</returns>
        //public static string GetSessionString(this HttpSessionState session, string key)
        //{
        //    return session.GetValue(key, x => (x ?? string.Empty).ToString());
        //}

        /// <summary>
        /// Gets the session object.
        /// </summary>
        /// <param name="session">The session.</param>
        /// <param name="key">The key.</param>
        /// <returns>Returns object</returns>
        //public static object GetSessionObject(this HttpSessionState session, string key)
        //{
        //    return session.GetValue(key, x => (x ?? string.Empty));
        //}
        #endregion

        #region String Extensions

        /// <summary>
        /// Gets the string.
        /// </summary>
        /// <param name="original">The original.</param>
        /// <returns>Returns String</returns>
        public static string GetString(this object original)
        {
            return Convert.ToString(original).Trim();
        }

        /// <summary>
        /// Determines whether [is null or empty] [the specified original].
        /// </summary>
        /// <param name="original">The original.</param>
        /// <returns>Returns Boolean</returns>
        public static bool IsNullOrEmpty(this string original)
        {
            return string.IsNullOrEmpty(original);
        }

        /// <summary>
        /// Parse the native Integer.
        /// </summary>
        /// <param name="original">The original.</param>
        /// <returns>Returns Integer</returns>
        public static int ParseNativeInt(this string original)
        {
            int ni;
			int.TryParse(original, NumberStyles.Integer, CultureInfo.CurrentCulture, out ni); // use default locale setting
            ////System.Int32.TryParse(original, NumberStyles.Integer, CultureInfo.CurrentCulture, out ni); // use default locale setting
            return ni;
        }

        /// <summary>
        /// Parses the native long.
        /// </summary>
        /// <param name="original">The original.</param>
        /// <returns>Returns Integer</returns>
        public static long ParseNativeInt64(this string original)
        {
            long ni;
			long.TryParse(original, NumberStyles.Integer, CultureInfo.CurrentCulture, out ni);
            ////System.Int64.TryParse(original, NumberStyles.Integer, CultureInfo.CurrentCulture, out ni);
            return ni;
        }

        /// <summary>
        /// Parses the native double.
        /// </summary>
        /// <param name="original">The original.</param>
        /// <returns>Returns Double</returns>
        public static double ParseNativeDouble(this string original)
        {
            double ni;
			double.TryParse(original, NumberStyles.Any, CultureInfo.CurrentCulture, out ni);
            ////System.Double.TryParse(original, NumberStyles.Any, CultureInfo.CurrentCulture, out ni);
            return ni;
        }

        /// <summary>
        /// Parses the native boolean.
        /// </summary>
        /// <param name="original">The original.</param>
        /// <returns>Returns Boolean</returns>
        public static bool ParseNativeBool(this string original)
        {
            bool ni;
			bool.TryParse(original, out ni); // use default locale setting
			////System.Boolean.TryParse(original, out ni); 
            return ni;
        }

        /// <summary>
        /// Parses the native decimal.
        /// </summary>
        /// <param name="original">The original.</param>
        /// <returns>Returns Decimal</returns>
        static public decimal ParseNativeDecimal(this string original)
        {
            decimal nd;
			decimal.TryParse(original, NumberStyles.Number, CultureInfo.CurrentCulture, out nd);
            ////System.Decimal.TryParse(original, NumberStyles.Number, CultureInfo.CurrentCulture, out nd);
            return nd;
        }

        /// <summary>
        /// Gets the date time value.
        /// </summary>
        /// <param name="original">The original.</param>
        /// <returns>Returns Date Time</returns>
        public static DateTime GetDateTimeValue(this string original)
        {
            if (original == null || original.Equals(string.Empty))
                return System.DateTime.MinValue;
            else
                return Convert.ToDateTime(original);
        }

        /// <summary>
        /// Gets the date time null value.
        /// </summary>
        /// <param name="original">The original.</param>
        /// <returns>Returns Null-able Date-time</returns>
        public static DateTime? GetDateTimeNullValue(this string original)
        {
            if (original == null || original.Equals(string.Empty))
                return null;
            else
                return Convert.ToDateTime(original);
        }

        /// <summary>
        /// Gets the date time exact.
        /// </summary>
        /// <param name="original">The original.</param>
        /// <returns>Returns Date Time</returns>
        public static DateTime GetDateTimeExact(this string original)
        {
            if (original == null || original.Equals(string.Empty))
            {
                return System.DateTime.MinValue;
            }
            else
            {
                DateTime nd;
                DateTime.TryParse(original, CultureInfo.GetCultureInfo(CommonLogic.strEnglishLanguage), DateTimeStyles.None, out nd);
                return nd;
            }
        }

        /// <summary>
        /// Truncates the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="maxLength">Length of the max.</param>
        /// <returns></returns>
        public static string Truncate(this string value, int maxLength)
        {
            return value.Length <= maxLength ? value : value.Substring(0, maxLength) + "...";
        }
        #endregion
    }
}