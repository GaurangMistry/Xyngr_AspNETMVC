using Xyngr.helper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;

using xyngr.Models;

namespace xyngr.Services
{
    public class DALCommentWriter : DataAccess
    {
        #region Variable Declaration
        CommentWriterViewModel objCommentWriterViewModel;
        List<CommentWriterViewModel> objCommentWriterViewModellist;
        #endregion

        #region Helper Functions
        /// <summary>
        /// Save CommentWriter Information
        /// </summary>
        public Int64 SaveCommentWriter(CommentWriterViewModel objCommentWriterViewModel, string strMode)
        {
            try
            {
                GetConnection();
                if (!strMode.Equals("INSERT", StringComparison.CurrentCultureIgnoreCase))
                    SetSQLCommandParameterWithValue("@CommentWriterID", objCommentWriterViewModel.CommentWriterID);
                SetSQLCommandParameterWithValue("@FirstName", objCommentWriterViewModel.FirstName);
                SetSQLCommandParameterWithValue("@LastName", objCommentWriterViewModel.LastName);
                SetSQLCommandParameterWithValue("@Email", objCommentWriterViewModel.Email);
                SetSQLCommandParameterWithValue("@PhoneNo", objCommentWriterViewModel.PhoneNo);
                SetSQLCommandParameterWithValue("@Image", objCommentWriterViewModel.Image);
                SetSQLCommandParameterWithValue("@Status", objCommentWriterViewModel.Status);
                if (strMode.Equals("INSERT", StringComparison.CurrentCultureIgnoreCase))
                {
                    return GetExecuteScalarByCommand("spI_CommentWriter");
                }
                else
                {
                    return GetExecuteScalarByCommand("spU_CommentWriter");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Get all CommentWriter Information
        /// </summary>
        public List<CommentWriterViewModel> GetAllCommentWriter(String SortBy, String SortOrder, String SearchField, String SearchValue)
        {
            try
            {
                GetConnection();
                SetSQLCommandParameterWithValue("@sortby", SortBy);
                SetSQLCommandParameterWithValue("@sortorder", SortOrder);
                SetSQLCommandParameterWithValue("@searchfield", SearchField);
                SetSQLCommandParameterWithValue("@searchvalue", SearchValue);
                IDataReader objIDataReader = GetReaderByCmd("spS_All_CommentWriter");
                return GetCommentWriterParameters(objIDataReader);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Get all CommentWriter Information
        /// </summary>
        public List<CommentWriterViewModel> GetAllCommentWriterForFront(String SortBy, String SortOrder, String SearchField, String SearchValue)
        {
            try
            {
                GetConnection();
                SetSQLCommandParameterWithValue("@sortby", SortBy);
                SetSQLCommandParameterWithValue("@sortorder", SortOrder);
                SetSQLCommandParameterWithValue("@searchfield", SearchField);
                SetSQLCommandParameterWithValue("@searchvalue", SearchValue);
                IDataReader objIDataReader = GetReaderByCmd("spS_All_CommentWriterForFront");
                return GetCommentWriterParameters(objIDataReader);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Get CommentWriter Information by CommentWriterID
        /// </summary>
        public CommentWriterViewModel GetCommentWriterByID(Int64 CommentWriterID)
        {
            try
            {
                GetConnection();
                SetSQLCommandParameterWithValue("@CommentWriterID", CommentWriterID);
                IDataReader objIDataReader = GetReaderByCmd("spS_CommentWriterById");
                List<CommentWriterViewModel> objCommentWriterViewModellist = new List<CommentWriterViewModel>();
                objCommentWriterViewModellist = GetCommentWriterParameters(objIDataReader);
                if (objCommentWriterViewModellist != null && objCommentWriterViewModellist.Count > 0)
                {
                    return objCommentWriterViewModellist[0];
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
        /// Get CommentWriter Information by CommentWriterID
        /// </summary>
        public CommentWriterViewModel GetCommentWriterByDesireUrl(String DesireUrl)
        {
            try
            {
                GetConnection();
                SetSQLCommandParameterWithValue("@DesireUrl", DesireUrl);
                IDataReader objIDataReader = GetReaderByCmd("spS_CommentWriterByDesireUrl");
                List<CommentWriterViewModel> objCommentWriterViewModellist = new List<CommentWriterViewModel>();
                objCommentWriterViewModellist = GetCommentWriterParameters(objIDataReader);
                if (objCommentWriterViewModellist != null && objCommentWriterViewModellist.Count > 0)
                {
                    return objCommentWriterViewModellist[0];
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
        /// MultiProcess Like DELETE,ACTIVE,INACTIVE for CommentWriter
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
                    strSPName = "spUS_CommentWriter";
                }
                else if (strMode.ToLower().Equals("multidelete"))
                {
                    strSPName = "spD_CommentWriter";
                }
                else
                {
                    SetSQLCommandParameterWithValue("@Status", "True");
                    strSPName = "spUS_CommentWriter";
                }
                return GetExecuteScalarByCommandInt32(strSPName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Get all values for CommentWriter
        /// </summary>
        public List<CommentWriterViewModel> GetCommentWriterParameters(IDataReader objIDataReader)
        {
            if (objIDataReader != null)
            {
                objCommentWriterViewModellist = new List<CommentWriterViewModel>();
                using (objIDataReader)
                {
                    while (objIDataReader.Read())
                    {
                        objCommentWriterViewModel = new CommentWriterViewModel();
                        objCommentWriterViewModel.CommentWriterID = sqlhelper.GetDBInt64Value(objIDataReader["CommentWriterID"], 0);
                        objCommentWriterViewModel.FirstName = sqlhelper.GetDBStringValue(objIDataReader["FirstName"], string.Empty);
                        objCommentWriterViewModel.LastName = sqlhelper.GetDBStringValue(objIDataReader["LastName"], string.Empty);
                        objCommentWriterViewModel.Email = sqlhelper.GetDBStringValue(objIDataReader["Email"], string.Empty);
                        objCommentWriterViewModel.PhoneNo = sqlhelper.GetDBStringValue(objIDataReader["PhoneNo"], string.Empty);
                        objCommentWriterViewModel.Image = sqlhelper.GetDBStringValue(objIDataReader["Image"], string.Empty);
                        objCommentWriterViewModel.CreatedDate = sqlhelper.GetDBDateTimeValue(objIDataReader["CreatedDate"]);
                        objCommentWriterViewModel.CreatedBy = sqlhelper.GetDBInt64Value(objIDataReader["CreatedBy"], 0);
                        objCommentWriterViewModel.ModifiedDate = sqlhelper.GetDBDateTimeValue(objIDataReader["ModifiedDate"]);
                        objCommentWriterViewModel.ModifiedBy = sqlhelper.GetDBInt64Value(objIDataReader["ModifiedBy"], 0);
                        objCommentWriterViewModel.Status = sqlhelper.GetDBBoolValue(objIDataReader["Status"], false);
                        objCommentWriterViewModellist.Add(objCommentWriterViewModel);
                    }
                }
                return objCommentWriterViewModellist;
            }
            else
            {
                return null;
            }
        }
        #endregion
    }
}
