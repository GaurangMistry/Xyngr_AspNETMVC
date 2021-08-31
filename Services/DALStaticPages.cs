using Xyngr.helper;
using System;
using System.Collections.Generic;
using System.Data;

using xyngr.Models;

namespace xyngr.Services
{
    public class DALStaticPages : DataAccess
    {
        #region Variable Declaration
        StaticPagesViewModel objStaticPagesViewModel;
        List<StaticPagesViewModel> objStaticPagesViewModellist;
        #endregion
        #region Helper Functions
        /// <summary>
        /// Save StaticPages Information
        /// </summary>
        public Int64 SaveStaticPages(StaticPagesViewModel objStaticPagesViewModel, string strMode)
        {
            try
            {
                GetConnection();
                if (!strMode.Equals("INSERT", StringComparison.CurrentCultureIgnoreCase))
                    SetSQLCommandParameterWithValue("@PageId", objStaticPagesViewModel.PageId);
                SetSQLCommandParameterWithValue("@PageName", objStaticPagesViewModel.PageName);
                SetSQLCommandParameterWithValue("@PageCode", objStaticPagesViewModel.PageCode);
                SetSQLCommandParameterWithValue("@PageContent", objStaticPagesViewModel.PageContent);
                SetSQLCommandParameterWithValue("@Image", objStaticPagesViewModel.Image);
                SetSQLCommandParameterWithValue("@MetaAuthorContent", objStaticPagesViewModel.MetaAuthorContent);
                SetSQLCommandParameterWithValue("@MetaDescContent", objStaticPagesViewModel.MetaDescContent);
                SetSQLCommandParameterWithValue("@MetaKeyWordContent", objStaticPagesViewModel.MetaKeyWordContent);
                SetSQLCommandParameterWithValue("@Status", objStaticPagesViewModel.Status);
                if (strMode.Equals("INSERT", StringComparison.CurrentCultureIgnoreCase))
                {
                    return GetExecuteScalarByCommand("spI_StaticPages");
                }
                else
                {
                    return GetExecuteScalarByCommand("spU_StaticPages");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Get all StaticPages Information
        /// </summary>
        public List<StaticPagesViewModel> GetAllStaticPages(String SortBy, String SortOrder, String SearchField, String SearchValue)
        {
            try
            {
                GetConnection();
                SetSQLCommandParameterWithValue("@sortby", SortBy);
                SetSQLCommandParameterWithValue("@sortorder", SortOrder);
                SetSQLCommandParameterWithValue("@searchfield", SearchField);
                SetSQLCommandParameterWithValue("@searchvalue", SearchValue);
                IDataReader objIDataReader = GetReaderByCmd("spS_All_StaticPages");
                return GetStaticPagesParameters(objIDataReader);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Get all StaticPages Information
        /// </summary>
        public List<StaticPagesViewModel> GetAllStaticPagesForFront(String SortBy, String SortOrder, String SearchField, String SearchValue)
        {
            try
            {
                GetConnection();
                SetSQLCommandParameterWithValue("@sortby", SortBy);
                SetSQLCommandParameterWithValue("@sortorder", SortOrder);
                SetSQLCommandParameterWithValue("@searchfield", SearchField);
                SetSQLCommandParameterWithValue("@searchvalue", SearchValue);
                IDataReader objIDataReader = GetReaderByCmd("spS_All_StaticPagesForFront");
                return GetStaticPagesParameters(objIDataReader);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Get StaticPages Information by PageId
        /// </summary>
        public StaticPagesViewModel GetStaticPagesByID(Int64 PageId)
        {
            try
            {
                GetConnection();
                SetSQLCommandParameterWithValue("@PageId", PageId);
                IDataReader objIDataReader = GetReaderByCmd("spS_StaticPagesById");
                List<StaticPagesViewModel> objStaticPagesViewModellist = new List<StaticPagesViewModel>();
                objStaticPagesViewModellist = GetStaticPagesParameters(objIDataReader);
                if (objStaticPagesViewModellist != null && objStaticPagesViewModellist.Count > 0)
                {
                    return objStaticPagesViewModellist[0];
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get StaticPages Information by PageId
        /// </summary>
        public StaticPagesViewModel GetStaticPagesByPageCode(string  PageCode)
        {
            try
            {
                GetConnection();
                SetSQLCommandParameterWithValue("@PageCode", PageCode);
                IDataReader objIDataReader = GetReaderByCmd("spS_StaticPagesByPageCode");
                List<StaticPagesViewModel> objStaticPagesViewModellist = new List<StaticPagesViewModel>();
                objStaticPagesViewModellist = GetStaticPagesParameters(objIDataReader);
                if (objStaticPagesViewModellist != null && objStaticPagesViewModellist.Count > 0)
                {
                    return objStaticPagesViewModellist[0];
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// MultiProcess Like DELETE,ACTIVE,INACTIVE for StaticPages
        /// </summary>
        public int MultiProcess(string strMode, string MultiIDs)
        {
            GetConnection();
            try
            {
                SetSQLCommandParameterWithValue("@MultiIDs", MultiIDs);
                string strSPName = string.Empty;
                if (strMode.ToLower().Equals("multiinactive"))
                {
                    SetSQLCommandParameterWithValue("@Status", "False");
                    strSPName = "spUS_StaticPages";
                }
                else if (strMode.ToLower().Equals("multidelete"))
                {
                    strSPName = "spD_StaticPages";
                }
                else
                {
                    SetSQLCommandParameterWithValue("@Status", "True");
                    strSPName = "spUS_StaticPages";
                }
                return GetExecuteScalarByCommandInt32(strSPName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Get all values for StaticPages
        /// </summary>
        public List<StaticPagesViewModel> GetStaticPagesParameters(IDataReader objIDataReader)
        {
            if (objIDataReader != null)
            {
                objStaticPagesViewModellist = new List<StaticPagesViewModel>();
                using (objIDataReader)
                {
                    while (objIDataReader.Read())
                    {
                        objStaticPagesViewModel = new StaticPagesViewModel();
                        objStaticPagesViewModel.PageId = sqlhelper.GetDBInt64Value(objIDataReader["PageId"], 0);
                        objStaticPagesViewModel.PageName = sqlhelper.GetDBStringValue(objIDataReader["PageName"], string.Empty);
                        objStaticPagesViewModel.PageCode = sqlhelper.GetDBStringValue(objIDataReader["PageCode"], string.Empty);
                        objStaticPagesViewModel.PageContent = sqlhelper.GetDBStringValue(objIDataReader["PageContent"], string.Empty);
                        objStaticPagesViewModel.Image = sqlhelper.GetDBStringValue(objIDataReader["Image"], string.Empty);
                        objStaticPagesViewModel.MetaAuthorContent = sqlhelper.GetDBStringValue(objIDataReader["MetaAuthorContent"], string.Empty);
                        objStaticPagesViewModel.MetaDescContent = sqlhelper.GetDBStringValue(objIDataReader["MetaDescContent"], string.Empty);
                        objStaticPagesViewModel.MetaKeyWordContent = sqlhelper.GetDBStringValue(objIDataReader["MetaKeyWordContent"], string.Empty);
                        objStaticPagesViewModel.CreatedDate = sqlhelper.GetDBDateTimeValue(objIDataReader["CreatedDate"]);
                        objStaticPagesViewModel.CreatedBy = sqlhelper.GetDBInt64Value(objIDataReader["CreatedBy"], 0);
                        objStaticPagesViewModel.ModifiedDate = sqlhelper.GetDBDateTimeValue(objIDataReader["ModifiedDate"]);
                        objStaticPagesViewModel.ModifiedBy = sqlhelper.GetDBInt64Value(objIDataReader["ModifiedBy"], 0);
                        objStaticPagesViewModel.Status = sqlhelper.GetDBBoolValue(objIDataReader["Status"], false);
                        objStaticPagesViewModellist.Add(objStaticPagesViewModel);
                    }
                }
                return objStaticPagesViewModellist;
            }
            else
            {
                return null;
            }
        }
        #endregion
    }
}
