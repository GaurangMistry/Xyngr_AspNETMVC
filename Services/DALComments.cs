using Xyngr.helper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using xyngr.Models;

namespace xyngr.Services
{
    public class DALComments : DataAccess
    {
        #region Variable Declaration
        CommentsViewModel objCommentsViewModel;
        List<CommentsViewModel> objCommentsViewModellist;
        #endregion
        #region Helper Functions
        /// <summary>
        /// Save Comments Information
        /// </summary>
        public Int64 SaveComments(CommentsViewModel objCommentsViewModel, string strMode)
        {
            try
            {
                GetConnection();
                if (!strMode.Equals("INSERT", StringComparison.CurrentCultureIgnoreCase))
                    SetSQLCommandParameterWithValue("@CommentID", objCommentsViewModel.CommentID);

                
                SetSQLCommandParameterWithValue("@BusinessID", objCommentsViewModel.BusinessID);
                SetSQLCommandParameterWithValue("@BranchID", objCommentsViewModel.BranchID);
                SetSQLCommandParameterWithValue("@CommentWriterID", objCommentsViewModel.CommentWriterID);
                SetSQLCommandParameterWithValue("@Comments", objCommentsViewModel.Comments);
                SetSQLCommandParameterWithValue("@Status", objCommentsViewModel.Status);
                if (strMode.Equals("INSERT", StringComparison.CurrentCultureIgnoreCase))
                {
                    return GetExecuteScalarByCommand("spI_Comments");
                }
                else
                {
                    return GetExecuteScalarByCommand("spU_Comments");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Get all Comments Information
        /// </summary>
        public List<CommentsViewModel> GetAllComments(String SortBy, String SortOrder, String SearchField, String SearchValue)
        {
            try
            {
                GetConnection();
                SetSQLCommandParameterWithValue("@sortby", SortBy);
                SetSQLCommandParameterWithValue("@sortorder", SortOrder);
                SetSQLCommandParameterWithValue("@searchfield", SearchField);
                SetSQLCommandParameterWithValue("@searchvalue", SearchValue);
                IDataReader objIDataReader = GetReaderByCmd("spS_All_Comments");
                return GetCommentsParameters(objIDataReader);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get all ImageGallery Information
        /// </summary>
        public List<CommentsViewModel> GetAllCommentsByBranchID(Int64 BranchID)
        {
            try
            {
                GetConnection();
                SetSQLCommandParameterWithValue("@BranchID", BranchID);
                IDataReader objIDataReader = GetReaderByCmd("spS_CommentsByBranchId");
                return GetCommentsParameters(objIDataReader);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get all ImageGallery Information
        /// </summary>
        public List<CommentsViewModel> GetAllCommentsByBranchIDForFront(Int64 BranchID)
        {
            try
            {
                GetConnection();
                SetSQLCommandParameterWithValue("@BranchID", BranchID);
                IDataReader objIDataReader = GetReaderByCmd("spS_CommentsByBranchIdForFront");
                return GetCommentsParameters(objIDataReader);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get all Comments Information
        /// </summary>
        public List<CommentsViewModel> GetAllCommentsForFront(String SortBy, String SortOrder, String SearchField, String SearchValue)
        {
            try
            {
                GetConnection();
                SetSQLCommandParameterWithValue("@sortby", SortBy);
                SetSQLCommandParameterWithValue("@sortorder", SortOrder);
                SetSQLCommandParameterWithValue("@searchfield", SearchField);
                SetSQLCommandParameterWithValue("@searchvalue", SearchValue);
                IDataReader objIDataReader = GetReaderByCmd("spS_All_CommentsForFront");
                return GetCommentsParameters(objIDataReader);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Get Comments Information by CommentID
        /// </summary>
        public CommentsViewModel GetCommentsByID(Int64 CommentID)
        {
            try
            {
                GetConnection();
                SetSQLCommandParameterWithValue("@CommentID", CommentID);
                IDataReader objIDataReader = GetReaderByCmd("spS_CommentsById");
                List<CommentsViewModel> objCommentsViewModellist = new List<CommentsViewModel>();
                objCommentsViewModellist = GetCommentsParameters(objIDataReader);
                if (objCommentsViewModellist != null && objCommentsViewModellist.Count > 0)
                {
                    return objCommentsViewModellist[0];
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
        /// Get Comments Information by CommentID
        /// </summary>
        //public CommentsViewModel GetCommentsByDesireUrl(String DesireUrl)
        //{
        //    try
        //    {
        //        GetConnection();
        //        SetSQLCommandParameterWithValue("@DesireUrl", DesireUrl);
        //        IDataReader objIDataReader = GetReaderByCmd("spS_CommentsByDesireUrl");
        //        List<CommentsViewModel> objCommentsViewModellist = new List<CommentsViewModel>();
        //        objCommentsViewModellist = GetCommentsParameters(objIDataReader);
        //        if (objCommentsViewModellist != null && objBOCommentslist.Count > 0)
        //        {
        //            return objCommentsViewModellist[0];
        //        }
        //        else
        //        {
        //            return null;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        /// <summary>
        /// MultiProcess Like DELETE,ACTIVE,INACTIVE for Comments
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
                    strSPName = "spUS_Comments";
                }
                else if (strMode.ToLower().Equals("multidelete"))
                {
                    strSPName = "spD_Comments";
                }
                else
                {
                    SetSQLCommandParameterWithValue("@Status", "True");
                    strSPName = "spUS_Comments";
                }
                return GetExecuteScalarByCommandInt32(strSPName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Get all values for Comments
        /// </summary>
        public List<CommentsViewModel> GetCommentsParameters(IDataReader objIDataReader)
        {
            if (objIDataReader != null)
            {
                objCommentsViewModellist = new List<CommentsViewModel>();
                using (objIDataReader)
                {
                    while (objIDataReader.Read())
                    {
                        objCommentsViewModel = new CommentsViewModel();
                        objCommentsViewModel.CommentID = sqlhelper.GetDBInt64Value(objIDataReader["CommentID"], 0);
                        objCommentsViewModel.BusinessID = sqlhelper.GetDBInt64Value(objIDataReader["BusinessID"], 0);
                        objCommentsViewModel.BranchID = sqlhelper.GetDBInt64Value(objIDataReader["BranchID"], 0);
                        objCommentsViewModel.CommentWriterID = sqlhelper.GetDBInt64Value(objIDataReader["CommentWriterID"], 0);
                        objCommentsViewModel.Comments = sqlhelper.GetDBStringValue(objIDataReader["Comments"], string.Empty);
                        objCommentsViewModel.CreatedDate = sqlhelper.GetDBDateTimeValue(objIDataReader["CreatedDate"]);
                        objCommentsViewModel.CreatedBy = sqlhelper.GetDBInt64Value(objIDataReader["CreatedBy"], 0);
                        objCommentsViewModel.ModifiedDate = sqlhelper.GetDBDateTimeValue(objIDataReader["ModifiedDate"]);
                        objCommentsViewModel.ModifiedBy = sqlhelper.GetDBInt64Value(objIDataReader["ModifiedBy"], 0);
                        objCommentsViewModel.Status = sqlhelper.GetDBBoolValue(objIDataReader["Status"], false);

                        try
                        {
                            objCommentsViewModel.Business = sqlhelper.GetDBStringValue(objIDataReader["Business"], string.Empty);
                            objCommentsViewModel.Branch = sqlhelper.GetDBStringValue(objIDataReader["Branch"], string.Empty);
                        }
                        catch(Exception ex) { }

                        try
                        {
                            objCommentsViewModel.CommentWriterName = sqlhelper.GetDBStringValue(objIDataReader["CommentWriterName"], string.Empty);
                            objCommentsViewModel.CommentWriterImageUrl = sqlhelper.GetDBStringValue(objIDataReader["CommentWriterImage"], string.Empty);

                            if (!objCommentsViewModel.CommentWriterImageUrl.Equals(string.Empty))
                            {
                                objCommentsViewModel.CommentWriterImageUrl = string.Format("/{0}/{1}/{2}", ConfigurationManager.AppSettings["CommentWriterImagePath"], "imagepath", objCommentsViewModel.CommentWriterImageUrl);
                            }
                            else
                            {
                                objCommentsViewModel.CommentWriterImageUrl = "/images/noimage.png";
                            }
                        }
                        catch (Exception ex) { }

                        objCommentsViewModellist.Add(objCommentsViewModel);
                    }
                }
                return objCommentsViewModellist;
            }
            else
            {
                return null;
            }
        }
        #endregion
    }
}
