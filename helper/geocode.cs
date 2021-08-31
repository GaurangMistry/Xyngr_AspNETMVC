using System;
using System.Net;
using System.Web;
using System.Globalization;

namespace Xyngr.helper
{
    public interface ISpatialCoordinate
    {
        double Latitude { get; set; }
        double Longitude { get; set; }
    }

    /// <summary>
    /// Coordinate structure. Holds Latitude and Longitude.
    /// </summary>
    public struct Coordinate : ISpatialCoordinate
    {
        private double latitude;
        private double longitude;

        public Coordinate(double latitude, double longitude)
        {
			this.latitude = latitude;
			this.longitude = longitude;
        }

        #region ISpatialCoordinate Members

        public double Latitude
        {
            get
            {
				return this.latitude;
            }
            set
            {
                this.latitude = value;
            }
        }

        public double Longitude
        {
            get
            {
				return this.longitude;
            }
            set
            {
                this.longitude = value;
            }
        }

        #endregion
    }

    public class Geocode
    {
        private const string _googleUri = "http://maps.google.com/maps/geo?q=";
        private const string _outputType = "csv"; // Available options: csv, xml, kml, json

        /// <summary>
        /// Returns a Uri of the Google code Geocoding Uri.
        /// </summary>
        /// <param name="address">The address to get the geocode for.</param>
        /// <returns>A new Uri</returns>
        private static Uri GetGeocodeUri(string address)
        {
            string googleKey = CommonLogic.GetConfigValue("GMapApiKey");
            address = HttpUtility.UrlEncode(address);
            return new Uri(string.Format("{0}{1}&output={2}&key={3}", _googleUri, address, _outputType, googleKey));
        }

		/// <summary>
		/// Gets a Coordinate from a address.
		/// </summary>
		/// <param name="address">An address.
		/// <remarks>
		/// <example>
		/// 3276 Westchester Ave, Bronx, NY 10461
		/// or
		/// New York, NY
		/// or
		/// 10461  (just a Zip-Code)
		/// </example>
		/// </remarks></param>
		/// <returns>
		/// A spatial coordinate that contains the latitude and longitude of the address.
		/// </returns>
        public static Coordinate GetCoordinates(string address)
        {
            WebClient client = new WebClient();
            Uri uri = GetGeocodeUri(address);

            /*  The first number is the status code, 
             * the second is the accuracy, 
             * the third is the latitude, 
             * the fourth one is the longitude.
             */
            string[] geocodeInfo = client.DownloadString(uri).Split(',');
            CultureInfo culture = new CultureInfo("en-US");
            return new Coordinate(Convert.ToDouble(geocodeInfo[2], culture), Convert.ToDouble(geocodeInfo[3], culture));
        }
    }
}
