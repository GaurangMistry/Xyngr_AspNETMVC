using Xyngr.helper;
using System;
using System.Collections.Generic;
using System.Data;
using xyngr.Models;

namespace xyngr.Services
{
    public class DALMediaGallery : DataAccess
    {
        #region Variable Declaration
        MediaGalleryViewModel objMediaGalleryViewModel;
        List<MediaGalleryViewModel> objMediaGalleryViewModellist;
        #endregion

        #region Helper Functions
        /// <summary>
        /// Save MediaGallery Information
        /// </summary>
        public Int64 SaveMediaGallery(MediaGalleryViewModel objMediaGalleryViewModel, string strMode)
        {
            try
            {
                GetConnection();
                if (!strMode.Equals("INSERT", StringComparison.CurrentCultureIgnoreCase))
                    SetSQLCommandParameterWithValue("@MediaID", objMediaGalleryViewModel.MediaID);
                SetSQLCommandParameterWithValue("@BusinessID", objMediaGalleryViewModel.BusinessID);
                SetSQLCommandParameterWithValue("@BranchID", objMediaGalleryViewModel.BranchID);
                SetSQLCommandParameterWithValue("@VideoType", objMediaGalleryViewModel.VideoType);
                SetSQLCommandParameterWithValue("@VideoCode", objMediaGalleryViewModel.VideoCode);
                SetSQLCommandParameterWithValue("@Status", objMediaGalleryViewModel.Status);
                if (strMode.Equals("INSERT", StringComparison.CurrentCultureIgnoreCase))
                {
                    return GetExecuteScalarByCommand("spI_MediaGallery");
                }
                else
                {
                    return GetExecuteScalarByCommand("spU_MediaGallery");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Get all MediaGallery Information
        /// </summary>
        public List<MediaGalleryViewModel> GetAllMediaGallery(String SortBy, String SortOrder, String SearchField, String SearchValue)
        {
            try
            {
                GetConnection();
                SetSQLCommandParameterWithValue("@sortby", SortBy);
                SetSQLCommandParameterWithValue("@sortorder", SortOrder);
                SetSQLCommandParameterWithValue("@searchfield", SearchField);
                SetSQLCommandParameterWithValue("@searchvalue", SearchValue);
                IDataReader objIDataReader = GetReaderByCmd("spS_All_MediaGallery");
                return GetMediaGalleryParameters(objIDataReader);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get all ImageGallery Information
        /// </summary>
        public List<MediaGalleryViewModel> GetAllMediaGalleryByBranchID(Int64 BranchID)
        {
            try
            {
                GetConnection();
                SetSQLCommandParameterWithValue("@BranchID", BranchID);
                IDataReader objIDataReader = GetReaderByCmd("spS_MediaGalleryByBranchId");
                return GetMediaGalleryParameters(objIDataReader);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get all ImageGallery Information
        /// </summary>
        public List<MediaGalleryViewModel> GetAllMediaGalleryByBranchIDForFront(Int64 BranchID)
        {
            try
            {
                GetConnection();
                SetSQLCommandParameterWithValue("@BranchID", BranchID);
                IDataReader objIDataReader = GetReaderByCmd("spS_MediaGalleryByBranchIdForFront");
                return GetMediaGalleryParameters(objIDataReader);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Get all MediaGallery Information
        /// </summary>
        public List<MediaGalleryViewModel> GetAllMediaGalleryForFront(String SortBy, String SortOrder, String SearchField, String SearchValue)
        {
            try
            {
                GetConnection();
                SetSQLCommandParameterWithValue("@sortby", SortBy);
                SetSQLCommandParameterWithValue("@sortorder", SortOrder);
                SetSQLCommandParameterWithValue("@searchfield", SearchField);
                SetSQLCommandParameterWithValue("@searchvalue", SearchValue);
                IDataReader objIDataReader = GetReaderByCmd("spS_All_MediaGalleryForFront");
                return GetMediaGalleryParameters(objIDataReader);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Get MediaGallery Information by MediaID
        /// </summary>
        public MediaGalleryViewModel GetMediaGalleryByID(Int64 MediaID)
        {
            try
            {
                GetConnection();
                SetSQLCommandParameterWithValue("@MediaID", MediaID);
                IDataReader objIDataReader = GetReaderByCmd("spS_MediaGalleryById");
                List<MediaGalleryViewModel> objMediaGalleryViewModellist = new List<MediaGalleryViewModel>();
                objMediaGalleryViewModellist = GetMediaGalleryParameters(objIDataReader);
                if (objMediaGalleryViewModellist != null && objMediaGalleryViewModellist.Count > 0)
                {
                    return objMediaGalleryViewModellist[0];
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
        /// Get MediaGallery Information by MediaID
        /// </summary>
        //public MediaGalleryViewModel GetMediaGalleryByDesireUrl(String DesireUrl)
        //{
        //    try
        //    {
        //        GetConnection();
        //        SetSQLCommandParameterWithValue("@DesireUrl", DesireUrl);
        //        IDataReader objIDataReader = GetReaderByCmd("spS_MediaGalleryByDesireUrl");
        //        List<MediaGalleryViewModel> objMediaGalleryViewModellist = new List<MediaGalleryViewModel>();
        //        objMediaGalleryViewModellist = GetMediaGalleryParameters(objIDataReader);
        //        if (objMediaGalleryViewModellist != null && objBOMediaGallerylist.Count > 0)
        //        {
        //            return objMediaGalleryViewModellist[0];
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
        /// MultiProcess Like DELETE,ACTIVE,INACTIVE for MediaGallery
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
                    strSPName = "spUS_MediaGallery";
                }
                else if (strMode.ToLower().Equals("multidelete"))
                {
                    strSPName = "spD_MediaGallery";
                }
                else
                {
                    SetSQLCommandParameterWithValue("@Status", "True");
                    strSPName = "spUS_MediaGallery";
                }
                return GetExecuteScalarByCommandInt32(strSPName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Get all values for MediaGallery
        /// </summary>
        public List<MediaGalleryViewModel> GetMediaGalleryParameters(IDataReader objIDataReader)
        {
            if (objIDataReader != null)
            {
                objMediaGalleryViewModellist = new List<MediaGalleryViewModel>();
                using (objIDataReader)
                {
                    while (objIDataReader.Read())
                    {
                        objMediaGalleryViewModel = new MediaGalleryViewModel();
                        objMediaGalleryViewModel.MediaID = sqlhelper.GetDBInt64Value(objIDataReader["MediaID"], 0);
                        objMediaGalleryViewModel.BusinessID = sqlhelper.GetDBInt64Value(objIDataReader["BusinessID"], 0);
                        objMediaGalleryViewModel.BranchID = sqlhelper.GetDBInt64Value(objIDataReader["BranchID"], 0);
                        objMediaGalleryViewModel.VideoType = sqlhelper.GetDBStringValue(objIDataReader["VideoType"], string.Empty);
                        objMediaGalleryViewModel.VideoCode = sqlhelper.GetDBStringValue(objIDataReader["VideoCode"], string.Empty);
                        objMediaGalleryViewModel.CreatedDate = sqlhelper.GetDBDateTimeValue(objIDataReader["CreatedDate"]);
                        objMediaGalleryViewModel.CreatedBy = sqlhelper.GetDBInt64Value(objIDataReader["CreatedBy"], 0);
                        objMediaGalleryViewModel.ModifiedDate = sqlhelper.GetDBDateTimeValue(objIDataReader["ModifiedDate"]);
                        objMediaGalleryViewModel.ModifiedBy = sqlhelper.GetDBInt64Value(objIDataReader["ModifiedBy"], 0);
                        objMediaGalleryViewModel.Status = sqlhelper.GetDBBoolValue(objIDataReader["Status"], false);

                        try
                        {
                            objMediaGalleryViewModel.Business = sqlhelper.GetDBStringValue(objIDataReader["Business"], string.Empty);
                            objMediaGalleryViewModel.Branch = sqlhelper.GetDBStringValue(objIDataReader["Branch"], string.Empty);
                        }
                        catch(Exception ex) { }
                        objMediaGalleryViewModellist.Add(objMediaGalleryViewModel);
                    }
                }
                return objMediaGalleryViewModellist;
            }
            else
            {
                return null;
            }
        }
        #endregion
    }
}
