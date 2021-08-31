using System;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;

namespace Xyngr.helper
{
	/// <summary>
	/// Data Access
	/// </summary>
	public abstract class DataAccess : IDisposable
	{
		#region Declaration
		private string mstrConnectionString;
		private SqlConnection mobjSqlConnection;
		private SqlCommand mobjSqlCommand;
		private int mintCommandTimeout = 30;
		#endregion

		#region Destructor
		// NOTE: Leave out the finalizer altogether if this class doesn't 
		// own unmanaged resources itself, but leave the other methods
		// exactly as they are. 

		/// <summary>
		/// Finalizes an instance of the <see cref="DataAccess" /> class.
		/// </summary>
		~DataAccess()
		{
			// Finalizer calls Dispose(false)
			this.Dispose(false);
		}
		#endregion

		#region Properties

		/// <summary>
		/// Gets or sets  connection Object
		/// </summary>
		public SqlConnection MobjSqlConnection
		{
			get
			{
				return this.mobjSqlConnection;
			}

			set
			{
				this.mobjSqlConnection = value;
			}
		}

		/// <summary>
		/// Gets or sets Connection String
		/// </summary>
		public string MstrConnectionString
		{
			get
			{
				return this.mstrConnectionString;
			}

			set
			{
				this.mstrConnectionString = value;
			}
		}

		/// <summary>
		/// Gets or sets Command Timeout
		/// </summary>
		public int MintCommandTimeout
		{
			get
			{
				return this.mintCommandTimeout;
			}

			set
			{
				this.mintCommandTimeout = value;
			}
		}

		/// <summary>
		/// Gets or sets  Command
		/// </summary>
		public SqlCommand MobjSqlCommand
		{
			get
			{
				return this.mobjSqlCommand;
			}

			set
			{
				this.mobjSqlCommand = value;
			}
		}

		#endregion

		#region Used Methods

		/// <summary>
		/// Gets the output para by command.
		/// </summary>
		/// <param name="command">The command.</param>
		/// <returns>returns Integer</returns>
		public int GetOutputParaByCommand(string command)
		{
			object identity = 0;
			try
			{
				this.mobjSqlCommand.CommandText = command;
				SqlParameter objSqlParameter = new SqlParameter("returnVal", SqlDbType.Int);
				objSqlParameter.Direction = ParameterDirection.ReturnValue;
				this.mobjSqlCommand.Parameters.Add(objSqlParameter);
				this.mobjSqlConnection.Open();
				this.mobjSqlCommand.Connection = this.mobjSqlConnection;
				this.mobjSqlCommand.ExecuteScalar();
				identity = Convert.ToInt32(objSqlParameter.Value);
				this.CloseConnection();

			}
			catch (Exception ex)
			{
				CommonLogic.SendMailOnError(ex);
				this.CloseConnection();
			}
			finally
			{
				this.CloseConnection();
				if (this.mobjSqlCommand != null)
				{
					this.mobjSqlCommand.Dispose();
				}
				if (this.MobjSqlConnection != null)
				{
					this.MobjSqlConnection.Dispose();
				}
			}
			return Convert.ToInt32(identity);
		}

		/// <summary>
		/// Gets the execute non query by command.
		/// </summary>
		/// <param name="command">The command.</param>
		public void GetExecuteNonQueryByCommand(string command)
		{
			try
			{
				this.mobjSqlCommand.CommandText = command;
				////mobj_SqlConnection.Open();
				this.mobjSqlCommand.Connection = this.mobjSqlConnection;
				this.mobjSqlCommand.ExecuteNonQuery();
				this.CloseConnection();
			}
			catch (Exception ex)
			{
				this.CloseConnection();
				throw ex;
			}
		}

		/// <summary>
		/// Gets the reader by SQL.
		/// </summary>
		/// <param name="strSQL">The string SQL.</param>
		/// <returns>returns Data Reader</returns>
		public IDataReader GetReaderBySQL(string strSQL)
		{
			this.mobjSqlConnection.Open();
			try
			{
				this.mobjSqlCommand = new SqlCommand(strSQL, this.mobjSqlConnection);
				return this.mobjSqlCommand.ExecuteReader(CommandBehavior.CloseConnection);
			}
			catch (Exception ex)
			{
				this.CloseConnection();
				throw ex;
			}
			finally
			{
				if (this.mobjSqlCommand != null)
				{
					this.mobjSqlCommand.Dispose();
				}
			}
		}

		/// <summary>
		/// Gets the reader by CMD.
		/// </summary>
		/// <param name="command">The command.</param>
		/// <returns>returns Data Reader</returns>
		public IDataReader GetReaderByCmd(string command)
		{
			IDataReader objIDataReader = null;
			try
			{
				this.mobjSqlCommand.CommandText = command;
				objIDataReader = this.mobjSqlCommand.ExecuteReader(CommandBehavior.CloseConnection);
				return objIDataReader;
			}
			catch (Exception ex)
			{
				CommonLogic.SendMailOnError(ex);
				this.CloseConnection();
				if (this.mobjSqlCommand != null)
				{
					this.mobjSqlCommand.Dispose();
				}
				return null;
			}
		}

		/// <summary>
		/// Gets the reader with output by CMD.
		/// </summary>
		/// <param name="command">The command.</param>
		/// <returns>returns Data Reader</returns>
		public IDataReader GetReaderWithOutputByCmd(string command)
		{
			IDataReader objIDataReader = null;
			try
			{
				this.mobjSqlCommand.CommandText = command;
				this.mobjSqlConnection.Open();

				SqlParameter objSqlParameter = new SqlParameter("@totalRec", SqlDbType.Int);
				objSqlParameter.Direction = ParameterDirection.Output;
				this.mobjSqlCommand.Parameters.Add(objSqlParameter);

				////identity = Convert.ToInt32(SQP.Value);
				objIDataReader = this.mobjSqlCommand.ExecuteReader(CommandBehavior.CloseConnection);
				////totalRecordCount = Convert.ToInt32(SQP.Value);
				return objIDataReader;
			}
			catch (Exception ex)
			{
				this.CloseConnection();
				throw ex;
			}
			finally
			{
				if (this.mobjSqlCommand != null)
				{
					this.mobjSqlCommand.Dispose();
				}
			}
		}

		/// <summary>
		/// Sets the SQL command parameter with value.
		/// </summary>
		/// <param name="parameterName">Name of the parameter.</param>
		/// <param name="value">The value.</param>
		public void SetSQLCommandParameterWithValue(string parameterName, object value)
		{
			this.mobjSqlCommand.Parameters.AddWithValue(parameterName, value);
		}

		/// <summary>
		/// Transactions the begin.
		/// </summary>
		public void TransactionBegin()
		{
			this.mobjSqlConnection.Open();
			SqlTransaction transaction = this.mobjSqlConnection.BeginTransaction();
			this.mobjSqlCommand.Transaction = transaction;
		}

		/// <summary>
		/// Transactions the commit.
		/// </summary>
		public void TransactionCommit()
		{
			this.mobjSqlCommand.Transaction.Commit();
		}

		/// <summary>
		/// Performs the SQL.
		/// </summary>
		/// <param name="command">The command.</param>
		/// <returns>returns Integer</returns>
		public int PerformSQL(string command)
		{
			this.mobjSqlConnection.Open();
			SqlTransaction transaction = this.mobjSqlConnection.BeginTransaction();
			object identity = 0;
			try
			{
				this.mobjSqlCommand.CommandText = command;
				this.mobjSqlCommand.Connection = this.mobjSqlConnection;
				this.mobjSqlCommand.Transaction = transaction;
				identity = this.mobjSqlCommand.ExecuteScalar();
				transaction.Commit();
			}
			catch (Exception ex)
			{
				CommonLogic.SendMailOnError(ex);
				transaction.Rollback();
			}
			finally
			{
				transaction.Dispose();
				this.CloseConnection();
				if (this.mobjSqlCommand != null)
				{
					this.mobjSqlCommand.Dispose();
				}
				if (this.MobjSqlConnection != null)
				{
					this.MobjSqlConnection.Dispose();
				}
			}

			return Convert.ToInt32(identity);
		}

		/// <summary>
		/// Gets the data table from excel.
		/// </summary>
		/// <param name="filePath">The file path.</param>
		/// <param name="strTableName">Name of the string table.</param>
		/// <returns>returns Data Table</returns>
		public DataTable GetDataTableFromExcel(string filePath, string strTableName)
		{
			string excelConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath + ";Extended Properties='Excel 8.0;HDR=Yes;IMEX=1'";
			////string XLSConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FilePath + ";Extended Properties='Excel 12.0;HDR=Yes;IMEX=1'";
			OleDbConnection excelCon = new OleDbConnection(excelConnectionString);
			DataTable excelData = null;
			try
			{
				using (excelData = new DataTable())
				{
					using (OleDbDataAdapter excelDataAdp = new OleDbDataAdapter())
					{
						excelDataAdp.SelectCommand = new OleDbCommand();
						excelDataAdp.SelectCommand.Connection = excelCon;
						excelCon.Open();
						excelDataAdp.SelectCommand.CommandText = "SELECT * FROM [" + strTableName + "$]";
						excelDataAdp.Fill(excelData);
					}
				}

				return excelData;
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				excelCon.Close();
			}
		}
		#endregion

		#region Not Used Methods

		/// <summary>
		/// Gets the dataset by command.
		/// </summary>
		/// <param name="command">The command.</param>
		/// <returns>returns Data Set</returns>
		public DataSet GetDatasetByCommand(string command)
		{
			DataSet ds = null;
			try
		    {
		        this.mobjSqlCommand.CommandText = command;
		        this.mobjSqlCommand.CommandTimeout = this.mintCommandTimeout;
		        this.mobjSqlCommand.CommandType = CommandType.StoredProcedure;
				using (ds = new DataSet())
				{
					using (SqlDataAdapter adpt = new SqlDataAdapter(this.mobjSqlCommand))
					{
						adpt.Fill(ds);
					}
				}

		        return ds;
		    }
		    catch (Exception ex)
		    {
		        throw ex;
		    }
		    finally
		    {
		        this.CloseConnection();
				if (this.MobjSqlConnection != null)
				{
					this.MobjSqlConnection.Dispose();
				}
				if (this.mobjSqlCommand != null)
				{
					this.mobjSqlCommand.Dispose();
				}
		    }
		}

		/// <summary>
		/// Returns the get execute non query by command.
		/// </summary>
		/// <param name="command">The command.</param>
		/// <returns>returns Integer</returns>
		public int ReturnGetExecuteNonQueryByCommand(string command)
		{
			try
			{
				this.mobjSqlCommand.CommandText = command;
				////mobj_SqlConnection.Open();
				this.mobjSqlCommand.Connection = this.mobjSqlConnection;
				int res = this.mobjSqlCommand.ExecuteNonQuery();
				return res;
			}
			catch (Exception ex)
			{
				this.CloseConnection();
				throw ex;
			}
			finally
			{
				this.CloseConnection();
				if (this.mobjSqlCommand != null)
				{
					this.mobjSqlCommand.Dispose();
				}
				if (this.MobjSqlConnection != null)
				{
					this.MobjSqlConnection.Dispose();
				}
			}
		}

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>
		/// Gets the connection.
		/// </summary>
		/// <exception cref="System.Exception">Error initializing data class. + Environment.NewLine + ex.Message</exception>
		protected void GetConnection()
		{
            

			try
			{
                this.mstrConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
                ////this.mstrConnectionString = CommonLogic.GetConfigValue("InstantASP_ConnectionString")
				this.mobjSqlConnection = new SqlConnection(this.mstrConnectionString);

				this.mobjSqlCommand = new SqlCommand();
				this.mobjSqlCommand.CommandTimeout = this.mintCommandTimeout;
				this.mobjSqlCommand.CommandType = CommandType.StoredProcedure;
				this.mobjSqlCommand.Connection = this.mobjSqlConnection;
				this.mobjSqlConnection.Open();
			}
			catch (Exception ex)
			{
				throw new Exception("Error initializing data class." + Environment.NewLine + ex.Message);
			}
		}

		/// <summary>
		/// Releases unmanaged and - optionally - managed resources.
		/// </summary>
		/// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
		/// <exception cref="System.Exception">Error disposing data class. + Environment.NewLine + ex.Message</exception>
		protected virtual void Dispose(bool disposing)
		{
			////try
			////{
				////Clean Up Connection Object
			if (disposing)
			{
				if (this.mobjSqlConnection != null)
				{
					if (this.mobjSqlConnection.State != ConnectionState.Closed)
					{
						this.mobjSqlConnection.Close();
					}

					this.mobjSqlConnection.Dispose();
				}

				////Clean Up Command Object
				if (this.mobjSqlCommand != null)
				{
					this.mobjSqlCommand.Dispose();
				}
			}
			////}
			////catch (Exception ex)
			////{
			////    throw new Exception("Error disposing data class." + Environment.NewLine + ex.Message);
			////}
		}

		/// <summary>
		/// Closes the connection.
		/// </summary>
		protected void CloseConnection()
		{
			if (this.mobjSqlConnection.State != ConnectionState.Closed)
			{
				this.mobjSqlConnection.Close();
			}
		}

		/// <summary>
		/// Gets the multi execute scalar CMD.
		/// </summary>
		/// <param name="command">The command.</param>
		/// <returns>returns Integer</returns>
		protected int GetMultiExecuteScalarCmd(string command)
		{
			object identity = 0;
			try
			{
				this.mobjSqlCommand.CommandText = command;
				if (this.mobjSqlConnection.State == ConnectionState.Closed)
				{
					this.mobjSqlConnection.Open();
				}

				this.mobjSqlCommand.Connection = this.mobjSqlConnection;
				identity = this.mobjSqlCommand.ExecuteScalar();
			}
			catch (Exception ex)
			{
				CommonLogic.SendMailOnError(ex);
				this.mobjSqlCommand.Transaction.Rollback();
				this.CloseConnection();
			}
			finally
			{
				this.CloseConnection();
				if (this.mobjSqlCommand != null)
				{
					this.mobjSqlCommand.Dispose();
				}
				if (this.MobjSqlConnection != null)
				{
					this.MobjSqlConnection.Dispose();
				}
			}

			return Convert.ToInt32(identity);
		}

		/// <summary>
		/// Gets the execute scalar by command integer.
		/// </summary>
		/// <param name="command">The command.</param>
		/// <returns>returns Integer</returns>
		/// <exception cref="System.Exception">return exception</exception>
		protected int GetExecuteScalarByCommandInt32(string command)
		{
			object identity = 0;
			try
			{
				this.mobjSqlCommand.CommandText = command;
				////mobj_SqlConnection.Open();

				this.mobjSqlCommand.Connection = this.mobjSqlConnection;
				identity = this.mobjSqlCommand.ExecuteScalar();
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
			finally
			{
				this.CloseConnection();
				if (this.mobjSqlCommand != null)
				{
					this.mobjSqlCommand.Dispose();
				}
				if (this.MobjSqlConnection != null)
				{
					this.MobjSqlConnection.Dispose();
				}
			}

			return Convert.ToInt32(identity);
		}

		/// <summary>
		/// Gets the execute scalar by command.
		/// </summary>
		/// <param name="command">The command.</param>
		/// <returns>returns Integer</returns>
		/// <exception cref="System.Exception">return exception</exception>
		protected long GetExecuteScalarByCommand(string command)
		{
			object identity = 0;
			try
			{
				this.mobjSqlCommand.CommandText = command;
				////mobj_SqlConnection.Open();

				this.mobjSqlCommand.Connection = this.mobjSqlConnection;
				identity = this.mobjSqlCommand.ExecuteScalar();
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
			finally
			{
				this.CloseConnection();
				if (this.mobjSqlCommand != null)
				{
					this.mobjSqlCommand.Dispose();
				}
				if (this.MobjSqlConnection != null)
				{
					this.MobjSqlConnection.Dispose();
				}
			}

			return Convert.ToInt64(identity);
		}

		/// <summary>
		/// Gets the execute scalar by command string.
		/// </summary>
		/// <param name="command">The command.</param>
		/// <returns>returns string</returns>
		/// <exception cref="System.Exception">return exception</exception>
		protected string GetExecuteScalarByCommandString(string command)
		{
		    object identity = string.Empty;
		    try
		    {
		        this.mobjSqlCommand.CommandText = command;
		        ////mobj_SqlConnection.Open();

		        this.mobjSqlCommand.Connection = this.mobjSqlConnection;
		        identity = this.mobjSqlCommand.ExecuteScalar();
		    }
		    catch (Exception ex)
		    {
		        throw new Exception(ex.Message);
		    }
		    finally
		    {
		        this.CloseConnection();
				if (this.mobjSqlCommand != null)
				{
					this.mobjSqlCommand.Dispose();
				}
				if (this.MobjSqlConnection != null)
				{
					this.MobjSqlConnection.Dispose();
				}
		    }

		    return Convert.ToString(identity);
		}
		#endregion

        public DataSet GetDatasetByCommandText(string Command)
        {
            try
            {
                mobjSqlCommand.CommandText = Command;
                mobjSqlCommand.CommandTimeout = mintCommandTimeout;
                mobjSqlCommand.CommandType = CommandType.Text;

                //mobj_SqlConnection.Open();
                SqlDataAdapter adpt = new SqlDataAdapter(mobjSqlCommand);
                DataSet ds = new DataSet();
                adpt.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
        }

        protected String GetExecuteScalarByCommandText(string Command)
        {
            object identity = 0;
            try
            {
                mobjSqlCommand.CommandText = Command;
                //mobj_SqlConnection.Open();

                mobjSqlCommand.Connection = mobjSqlConnection;
                mobjSqlCommand.CommandType = CommandType.Text;
                identity = mobjSqlCommand.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                CloseConnection();
            }
            return Convert.ToString(identity);
        }

    }
}
