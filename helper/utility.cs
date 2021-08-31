using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web.UI.WebControls;

namespace Xyngr.helper
{
	/// <summary>
	/// Utility Class
	/// </summary>
	/// <typeparam name="T">Use iComparer</typeparam>
    public class utility<T> : IComparer<T>
    {
		private SortDirection sortDirection;
		public SortDirection SortDirection
		{
			get { return this.sortDirection; }
			set { this.sortDirection = value; }
		}

		private string sortExpression;
		#region IComparer<T> Members

		/// <summary>
		/// Compares the specified x.
		/// </summary>
		/// <param name="x">The x.</param>
		/// <param name="y">The y.</param>
		/// <returns>Return Integer</returns>
		public int Compare(T x, T y)
		{
			PropertyInfo propertyInfo = typeof(T).GetProperty(this.sortExpression);
			IComparable obj1 = (IComparable)propertyInfo.GetValue(x, null);
			IComparable obj2 = (IComparable)propertyInfo.GetValue(y, null);

			if (SortDirection == SortDirection.Ascending)
			{
				return obj1.CompareTo(obj2);
			}
			else
			{
				return obj2.CompareTo(obj1);
			}
		}
		#endregion

		/// <summary>
		/// Initializes a new instance of the <see cref="utility{T}" /> class.
		/// </summary>
        public utility()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="utility{T}" /> class.
        /// </summary>
        /// <param name="sortExpression">The sort expression.</param>
        /// <param name="sortDirection">The sort direction.</param>
        public utility(string sortExpression, string sortDirection)
        {
            this.sortExpression = sortExpression;
            if (sortDirection == "DESC")
            {
                this.sortDirection = SortDirection.Descending;
            }
            else
            {
                this.sortDirection = SortDirection.Ascending;
            }
        }

       
    }
}
