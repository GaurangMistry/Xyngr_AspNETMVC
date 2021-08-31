using System;

namespace Xyngr.helper
{
    /// <summary>
    /// SQL helper Class
    /// </summary>
    public class sqlhelper
    {
        #region DB VALUE CONVERSION SUPPORTED FUNCTIONS

        /// <summary>
        /// Gets the DB integer value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>returns Integer</returns>
        public static int GetDBIntValue(object value, int defaultValue)
        {
            return Convert.IsDBNull(value) ? defaultValue : Convert.ToInt32(value);
        }

        /// <summary>
        /// Gets the DB long value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>returns Integer</returns>
        public static long GetDBInt64Value(object value, long defaultValue)
        {
            return Convert.IsDBNull(value) ? defaultValue : Convert.ToInt64(value);
        }

        /// <summary>
        /// Gets the DB string value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>returns String</returns>
        public static string GetDBStringValue(object value, string defaultValue)
        {
            return Convert.IsDBNull(value) ? defaultValue : Convert.ToString(value);
        }

        /// <summary>
        /// Gets the DB boolean value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="defaultValue">if set to <c>true</c> [default value].</param>
        /// <returns>returns Boolean</returns>
        public static bool GetDBBoolValue(object value, bool defaultValue)
        {
            return Convert.IsDBNull(value) ? defaultValue : Convert.ToBoolean(value);
        }

        /// <summary>
        /// Gets the DB decimal value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>returns Decimal</returns>
        public static decimal GetDBDecimalValue(object value, decimal defaultValue)
        {
            return Convert.IsDBNull(value) ? defaultValue : Convert.ToDecimal(value);
        }

        /// <summary>
        /// Gets the DB double value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>returns Double</returns>
        public static double GetDBDoubleValue(object value, double defaultValue)
        {
            return Convert.IsDBNull(value) ? defaultValue : Convert.ToDouble(value);
        }

        /// <summary>
        /// Gets the DB date time value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>returns Date-time</returns>
        public static DateTime? GetDBDateTimeValue(object value)
        {
            if (Convert.IsDBNull(value))
            {
                return null;
            }
            else
            {
                try
                {
                    return System.DateTime.Parse(value.ToString());
                }
                catch
                {
                    return null;
                }
            }
        }

        #endregion
    }
}
