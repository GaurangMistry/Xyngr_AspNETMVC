using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Reflection;
using System.IO;
using System.Xml;
using System.Web;

#region CSV Helper

namespace demo.App_Code.Helper
{
    public static class CSVHelper
    {
        /// <summary>
        /// Exports a CSV by writing to the current HttpResponse
        /// </summary>
        /// <param name="csv">The CSV data as a string</param>
        /// <param name="filename">The filename for the exported file</param>
        public static void ExportCSV(string csv, string filename)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.AddHeader("content-disposition", string.Format("attachment; filename={0}.csv", filename));
            HttpContext.Current.Response.ContentType = "text/csv";
            HttpContext.Current.Response.AddHeader("Pragma", "public");
            HttpContext.Current.Response.Write(csv);
            HttpContext.Current.Response.End();
        }
    }

#endregion

    #region Extension Methods

    public static class IEnumerableExtensions
    {
        /// <summary>
        /// Exporter extension method for all IEnumerableOfT
        /// </summary>
        public static CSVExporter<T> GetCSVExporter<T>(
            this IEnumerable<T> source, String seperator = ",") where T : class
        {
            return new CSVExporter<T>(source, seperator);
        }
    }

    /// <summary>
    /// Represents custom exportable column with a expression for the property name and a custom format string
    /// </summary>
    public class ExportableColumn<T>
    {
        public Expression<Func<T, Object>> Func { get; private set; }
        public String HeaderString { get; private set; }
        public String CustomFormatString { get; private set; }

        public ExportableColumn(
            Expression<Func<T, Object>> func,
            String headerString = "",
            String customFormatString = "")
        {
            this.Func = func;
            this.HeaderString = headerString;
            this.CustomFormatString = customFormatString;
        }
    }

    /// <summary>
    /// Exporter that uses Expression tree parsing to work out what values to export for 
    /// columns, and will use additional data as specified in the List of ExportableColumn
    /// which defines whethere to use custom headers, or formatted output
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CSVExporter<T> where T : class
    {
        private List<ExportableColumn<T>> columns = new List<ExportableColumn<T>>();
        private Dictionary<Expression<Func<T, Object>>, Func<T, Object>> compiledFuncLookup =
            new Dictionary<Expression<Func<T, Object>>, Func<T, Object>>();
        private List<String> headers = new List<String>();
        private IEnumerable<T> sourceList;
        private String seperator;
        private bool doneHeaders;

        public CSVExporter(IEnumerable<T> sourceList, String seperator = ",")
        {
            this.sourceList = sourceList;
            this.seperator = seperator;
        }

        public CSVExporter<T> AddExportableColumn(
                Expression<Func<T, Object>> func,
                String headerString = "",
                String customFormatString = "")
        {
            columns.Add(new ExportableColumn<T>(func, headerString, customFormatString));
            return this;
        }

        /// <summary>
        /// Exports the CSV.
        /// </summary>
        /// <param name="filename">The filename.</param>
        //public void ExportCSV(string filename)
        //{
        //    string csv = GetCSVString();
        //    CSVHelper.ExportCSV(csv, filename);
        //}

        /// <summary>
        /// Export all specified columns as a string, using seperator and column data provided where we may use custom or default headers 
        /// </summary>
        //private string GetCSVString()
        //{
        //    StringBuilder sb = new StringBuilder();
        //    if (columns.Count > 0)
        //    {

        //        foreach (T item in sourceList)
        //        {
        //            List<String> values = new List<String>();
        //            foreach (ExportableColumn<T> exportableColumn in columns)
        //            {
        //                if (!doneHeaders)
        //                {
        //                    if (String.IsNullOrEmpty(exportableColumn.HeaderString))
        //                    {
        //                        headers.Add(GetPropertyName(exportableColumn.Func));
        //                    }
        //                    else
        //                    {
        //                        headers.Add(exportableColumn.HeaderString);
        //                    }

        //                    Func<T, Object> func = exportableColumn.Func.Compile();
        //                    compiledFuncLookup.Add(exportableColumn.Func, func);
        //                    if (!String.IsNullOrEmpty(exportableColumn.CustomFormatString))
        //                    {
        //                        var value = func(item);
        //                        values.Add(value != null ?
        //                            String.Format(exportableColumn.CustomFormatString, value.ToString()) : "");

        //                    }
        //                    else
        //                    {
        //                        var value = func(item);
        //                        if (exportableColumn.HeaderString.Equals("Start Date") || exportableColumn.HeaderString.Equals("End Date"))
        //                        {
        //                            values.Add(value != null ? Convert.ToString(Convert.ToDateTime(value).ToString("dd-MMM-yyyy")) : "");
        //                        }
        //                        else
        //                        {
        //                            values.Add(value != null ? value.ToString() : "");
        //                        }
        //                    }
        //                }
        //                else
        //                {
        //                    if (!String.IsNullOrEmpty(exportableColumn.CustomFormatString))
        //                    {
        //                        var value = compiledFuncLookup[exportableColumn.Func](item);
        //                        values.Add(value != null ?
        //                            String.Format(exportableColumn.CustomFormatString, value.ToString()) : "");
        //                    }
        //                    else
        //                    {
        //                        var value = compiledFuncLookup[exportableColumn.Func](item);
        //                        if (exportableColumn.HeaderString.Equals("Start Date") || exportableColumn.HeaderString.Equals("End Date"))
        //                        {
        //                            values.Add(value != null ? Convert.ToString(Convert.ToDateTime(value).ToString("dd-MMM-yyyy")) : "");
        //                        }
        //                        else
        //                        {
        //                            values.Add(value != null ? value.ToString() : "");
        //                        }
        //                    }
        //                }
        //            }
        //            if (!doneHeaders)
        //            {
        //                sb.AppendLine(headers.Aggregate((start, end) => start + seperator + end));
        //                doneHeaders = true;
        //            }

        //            sb.AppendLine(values.Aggregate((start, end) => start + seperator + end));
        //        }
        //    }
        //    return sb.ToString();
        //}

        /// <summary>
        /// Exports the CSV.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <param name="totalAmt">The total amt.</param>
        /// <param name="totalCommission">The total commission.</param>
        //public void ExportCSV(string filename, string totalAmt, string totalCommission)
        //{
        //    string csv = GetCSVString(totalAmt, totalCommission);
        //    CSVHelper.ExportCSV(csv, filename);
        //}

        /// <summary>
        /// Export all specified columns as a string, using seperator and column data provided where we may use custom or default headers 
        /// </summary>
        //private string GetCSVString(string totalAmount, string totalCommission)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    if (columns.Count > 0)
        //    {

        //        foreach (T item in sourceList)
        //        {
        //            List<String> values = new List<String>();
        //            foreach (ExportableColumn<T> exportableColumn in columns)
        //            {
        //                if (!doneHeaders)
        //                {
        //                    if (String.IsNullOrEmpty(exportableColumn.HeaderString))
        //                    {
        //                        headers.Add(GetPropertyName(exportableColumn.Func));
        //                    }
        //                    else
        //                    {
        //                        headers.Add(exportableColumn.HeaderString);
        //                    }

        //                    Func<T, Object> func = exportableColumn.Func.Compile();
        //                    compiledFuncLookup.Add(exportableColumn.Func, func);
        //                    if (!String.IsNullOrEmpty(exportableColumn.CustomFormatString))
        //                    {
        //                        var value = func(item);
        //                        values.Add(value != null ?
        //                            String.Format(exportableColumn.CustomFormatString, value.ToString()) : "");

        //                    }
        //                    else
        //                    {
        //                        var value = func(item);
        //                        if (exportableColumn.HeaderString.Equals("Check In") || exportableColumn.HeaderString.Equals("Check Out") || exportableColumn.HeaderString.Equals("Purchase Date"))
        //                        {
        //                            values.Add(value != null ? Convert.ToString(Convert.ToDateTime(value).ToString("dd-MMM-yyyy")) : "");
        //                        }
        //                        else
        //                        {
        //                            values.Add(value != null ? value.ToString() : "");
        //                        }
        //                    }
        //                }
        //                else
        //                {
        //                    if (!String.IsNullOrEmpty(exportableColumn.CustomFormatString))
        //                    {
        //                        var value = compiledFuncLookup[exportableColumn.Func](item);
        //                        values.Add(value != null ?
        //                            String.Format(exportableColumn.CustomFormatString, value.ToString()) : "");
        //                    }
        //                    else
        //                    {
        //                        var value = compiledFuncLookup[exportableColumn.Func](item);
        //                        if (exportableColumn.HeaderString.Equals("Check In") || exportableColumn.HeaderString.Equals("Check Out") || exportableColumn.HeaderString.Equals("Purchase Date"))
        //                        {
        //                            values.Add(value != null ? Convert.ToString(Convert.ToDateTime(value).ToString("dd-MMM-yyyy")) : "");
        //                        }
        //                        else
        //                        {
        //                            values.Add(value != null ? value.ToString() : "");
        //                        }
        //                    }
        //                }
        //            }
        //            if (!doneHeaders)
        //            {
        //                sb.AppendLine(headers.Aggregate((start, end) => start + seperator + end));
        //                doneHeaders = true;
        //            }

        //            sb.AppendLine(values.Aggregate((start, end) => start + seperator + end));
        //        }

        //        sb.Append("\n\n");
        //        sb.Append("Total Amount:");
        //        sb.Append(totalAmount.Replace("€", " Euro ")).Append("\n");
        //        sb.Append("Total Commission:");
        //        sb.Append(totalCommission.Replace("€", " Euro "));
        //    }
        //    return sb.ToString();
        //}

        /// <summary>
        /// Gets a Name from an expression tree that is assumed to be a
        /// MemberExpression
        /// </summary>
        //private static string GetPropertyName<T>(
        //    Expression<Func<T, Object>> propertyExpression)
        //{
        //    var lambda = propertyExpression as LambdaExpression;
        //    MemberExpression memberExpression;
        //    if (lambda.Body is UnaryExpression)
        //    {
        //        var unaryExpression = lambda.Body as UnaryExpression;
        //        memberExpression = unaryExpression.Operand as MemberExpression;
        //    }
        //    else
        //    {
        //        memberExpression = lambda.Body as MemberExpression;
        //    }

        //    var propertyInfo = memberExpression.Member as PropertyInfo;

        //    return propertyInfo.Name;
        //}

        private string MakeXMLNameLegal(string aString)
        {
            StringBuilder newName = new StringBuilder();

            if (!char.IsLetter(aString[0]))
                newName.Append("_");

            // Must start with a letter or underscore.
            for (int i = 0; i <= aString.Length - 1; i++)
            {
                if (char.IsLetter(aString[i]) || char.IsNumber(aString[i]))
                {
                    newName.Append(aString[i]);
                }
                else
                {
                    newName.Append("_");
                }
            }
            return newName.ToString();
        }
    }
}
    #endregion
