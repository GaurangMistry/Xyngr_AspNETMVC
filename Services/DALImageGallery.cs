using Xyngr.helper;
using System;
using System.Collections.Generic;
using System.Data;

using xyngr.Models;
using System.Configuration;

namespace xyngr.Services
{
    public class DALImageGallery : DataAccess
    {
        #region Variable Declaration
        ImageGalleryViewModel objImageGalleryViewModel;
        List<ImageGalleryViewModel> objImageGalleryViewModellist;
        #endregion
        #region Helper Functions
        /// <summary>
        /// Save ImageGallery Information
        /// </summary>
        public Int64 SaveImageGallery(ImageGalleryViewModel objImageGalleryViewModel, string strMode)
        {
            try
            {
                GetConnection();
                if (!strMode.Equals("INSERT", StringComparison.CurrentCultureIgnoreCase))
                    SetSQLCommandParameterWithValue("@ImageID", objImageGalleryViewModel.ImageID);
                SetSQLCommandParameterWithValue("@BusinessID", objImageGalleryViewModel.BusinessID);
                SetSQLCommandParameterWithValue("@BranchID", objImageGalleryViewModel.BranchID);
                SetSQLCommandParameterWithValue("@Title", objImageGalleryViewModel.Title);
                SetSQLCommandParameterWithValue("@Image", objImageGalleryViewModel.Image);
                SetSQLCommandParameterWithValue("@Status", objImageGalleryViewModel.Status);
                if (strMode.Equals("INSERT", StringComparison.CurrentCultureIgnoreCase))
                {
                    return GetExecuteScalarByCommand("spI_ImageGallery");
                }
                else
                {
                    return GetExecuteScalarByCommand("spU_ImageGallery");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Get all ImageGallery Information
        /// </summary>
        public List<ImageGalleryViewModel> GetAllImageGallery(String SortBy, String SortOrder, String SearchField, String SearchValue)
        {
            try
            {
                GetConnection();
                SetSQLCommandParameterWithValue("@sortby", SortBy);
                SetSQLCommandParameterWithValue("@sortorder", SortOrder);
                SetSQLCommandParameterWithValue("@searchfield", SearchField);
                SetSQLCommandParameterWithValue("@searchvalue", SearchValue);
                IDataReader objIDataReader = GetReaderByCmd("spS_All_ImageGallery");
                return GetImageGalleryParameters(objIDataReader);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Get all ImageGallery Information
        /// </summary>
        public List<ImageGalleryViewModel> GetAllImageGalleryForFront(String SortBy, String SortOrder, String SearchField, String SearchValue)
        {
            try
            {
                GetConnection();
                SetSQLCommandParameterWithValue("@sortby", SortBy);
                SetSQLCommandParameterWithValue("@sortorder", SortOrder);
                SetSQLCommandParameterWithValue("@searchfield", SearchField);
                SetSQLCommandParameterWithValue("@searchvalue", SearchValue);
                IDataReader objIDataReader = GetReaderByCmd("spS_All_ImageGalleryForFront");
                return GetImageGalleryParameters(objIDataReader);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get all ImageGallery Information
        /// </summary>
        public List<ImageGalleryViewModel> GetAllImageGalleryByBranchID(Int64 BranchID)
        {
            try
            {
                GetConnection();
                SetSQLCommandParameterWithValue("@BranchID", BranchID);
                IDataReader objIDataReader = GetReaderByCmd("spS_ImageGalleryByBranchId");
                return GetImageGalleryParameters(objIDataReader);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get all ImageGallery Information
        /// </summary>
        public List<ImageGalleryViewModel> GetAllImageGalleryByBranchIDForFront(Int64 BranchID)
        {
            try
            {
                GetConnection();
                SetSQLCommandParameterWithValue("@BranchID", BranchID);
                IDataReader objIDataReader = GetReaderByCmd("spS_ImageGalleryByBranchIdForFront");
                return GetImageGalleryParameters(objIDataReader);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Get ImageGallery Information by ImageID
        /// </summary>
        public ImageGalleryViewModel GetImageGalleryByID(Int64 ImageID)
        {
            try
            {
                GetConnection();
                SetSQLCommandParameterWithValue("@ImageID", ImageID);
                IDataReader objIDataReader = GetReaderByCmd("spS_ImageGalleryById");
                List<ImageGalleryViewModel> objImageGalleryViewModellist = new List<ImageGalleryViewModel>();
                objImageGalleryViewModellist = GetImageGalleryParameters(objIDataReader);
                if (objImageGalleryViewModellist != null && objImageGalleryViewModellist.Count > 0)
                {
                    return objImageGalleryViewModellist[0];
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
        /// Get ImageGallery Information by ImageID
        /// </summary>
        //public ImageGalleryViewModel GetImageGalleryByDesireUrl(String DesireUrl)
        //{
        //    try
        //    {
        //        GetConnection();
        //        SetSQLCommandParameterWithValue("@DesireUrl", DesireUrl);
        //        IDataReader objIDataReader = GetReaderByCmd("spS_ImageGalleryByDesireUrl");
        //        List<ImageGalleryViewModel> objImageGalleryViewModellist = new List<ImageGalleryViewModel>();
        //        objImageGalleryViewModellist = GetImageGalleryParameters(objIDataReader);
        //        if (objImageGalleryViewModellist != null && objBOImageGallerylist.Count > 0)
        //        {
        //            return objImageGalleryViewModellist[0];
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
        /// MultiProcess Like DELETE,ACTIVE,INACTIVE for ImageGallery
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
                    strSPName = "spUS_ImageGallery";
                }
                else if (strMode.ToLower().Equals("multidelete"))
                {
                    strSPName = "spD_ImageGallery";
                }
                else
                {
                    SetSQLCommandParameterWithValue("@Status", "True");
                    strSPName = "spUS_ImageGallery";
                }
                return GetExecuteScalarByCommandInt32(strSPName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Get all values for ImageGallery
        /// </summary>
        public List<ImageGalleryViewModel> GetImageGalleryParameters(IDataReader objIDataReader)
        {
            if (objIDataReader != null)
            {
                objImageGalleryViewModellist = new List<ImageGalleryViewModel>();
                using (objIDataReader)
                {
                    while (objIDataReader.Read())
                    {
                        objImageGalleryViewModel = new ImageGalleryViewModel();
                        objImageGalleryViewModel.ImageID = sqlhelper.GetDBInt64Value(objIDataReader["ImageID"], 0);
                        objImageGalleryViewModel.BusinessID = sqlhelper.GetDBInt64Value(objIDataReader["BusinessID"], 0);
                        objImageGalleryViewModel.BranchID = sqlhelper.GetDBInt64Value(objIDataReader["BranchID"], 0);
                        
                        objImageGalleryViewModel.Image = sqlhelper.GetDBStringValue(objIDataReader["Image"], string.Empty);
                        objImageGalleryViewModel.Title = sqlhelper.GetDBStringValue(objIDataReader["Title"], string.Empty);
                        objImageGalleryViewModel.CreatedDate = sqlhelper.GetDBDateTimeValue(objIDataReader["CreatedDate"]);
                        objImageGalleryViewModel.CreatedBy = sqlhelper.GetDBInt64Value(objIDataReader["CreatedBy"], 0);
                        objImageGalleryViewModel.ModifiedDate = sqlhelper.GetDBDateTimeValue(objIDataReader["ModifiedDate"]);
                        objImageGalleryViewModel.ModifiedBy = sqlhelper.GetDBInt64Value(objIDataReader["ModifiedBy"], 0);
                        objImageGalleryViewModel.Status = sqlhelper.GetDBBoolValue(objIDataReader["Status"], false);

                        try
                        {
                            if (!objImageGalleryViewModel.Image.Equals(string.Empty))
                            {
                                objImageGalleryViewModel.ImageURL = string.Format("/{0}/{1}/{2}", ConfigurationManager.AppSettings["GalleryImagePath"], "gallerypath", objImageGalleryViewModel.Image);
                            }
                            else
                            {
                                objImageGalleryViewModel.ImageURL = "/images/noimage.png";
                            }
                        }
                        catch (Exception ex) { }

                        try
                        {
                            objImageGalleryViewModel.Business = sqlhelper.GetDBStringValue(objIDataReader["Business"], string.Empty);
                            objImageGalleryViewModel.Branch = sqlhelper.GetDBStringValue(objIDataReader["Branch"], string.Empty);
                        }
                        catch(Exception ex) { }

                        objImageGalleryViewModellist.Add(objImageGalleryViewModel);
                    }
                }
                return objImageGalleryViewModellist;
            }
            else
            {
                return null;
            }
        }
        #endregion
    }
}
