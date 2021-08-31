using Xyngr.helper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using xyngr.Models;

namespace xyngr.Services
{
    public class DALBanners : DataAccess
    {
        #region Variable Declaration
        BannersViewModel objBannersViewModel;
        List<BannersViewModel> objBannersViewModellist;
        #endregion
        #region Helper Functions
        /// <summary>
        /// Save Banners Information
        /// </summary>
        public Int64 SaveBanners(BannersViewModel objBannersViewModel, string strMode)
        {
            try
            {
                GetConnection();
                if (!strMode.Equals("INSERT", StringComparison.CurrentCultureIgnoreCase))
                    SetSQLCommandParameterWithValue("@BannerID", objBannersViewModel.BannerID);
                SetSQLCommandParameterWithValue("@Rating", objBannersViewModel.Rating);
                SetSQLCommandParameterWithValue("@Title", objBannersViewModel.Title);
                SetSQLCommandParameterWithValue("@Comment", objBannersViewModel.Comment);
                SetSQLCommandParameterWithValue("@Image", objBannersViewModel.Image);
                SetSQLCommandParameterWithValue("@Status", objBannersViewModel.Status);
                if (strMode.Equals("INSERT", StringComparison.CurrentCultureIgnoreCase))
                {
                    return GetExecuteScalarByCommand("spI_Banners");
                }
                else
                {
                    return GetExecuteScalarByCommand("spU_Banners");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Get all Banners Information
        /// </summary>
        public List<BannersViewModel> GetAllBanners(String SortBy, String SortOrder, String SearchField, String SearchValue)
        {
            try
            {
                GetConnection();
                SetSQLCommandParameterWithValue("@sortby", SortBy);
                SetSQLCommandParameterWithValue("@sortorder", SortOrder);
                SetSQLCommandParameterWithValue("@searchfield", SearchField);
                SetSQLCommandParameterWithValue("@searchvalue", SearchValue);
                IDataReader objIDataReader = GetReaderByCmd("spS_All_Banners");
                return GetBannersParameters(objIDataReader);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Get all Banners Information
        /// </summary>
        public List<BannersViewModel> GetAllBannersForFront()
        {
            try
            {
                GetConnection();
                IDataReader objIDataReader = GetReaderByCmd("spS_All_BannersForFront");
                if (objIDataReader != null)
                {
                    objBannersViewModellist = new List<BannersViewModel>();
                    using (objIDataReader)
                    {
                        while (objIDataReader.Read())
                        {
                            objBannersViewModel = new BannersViewModel();
                            objBannersViewModel.Rating = sqlhelper.GetDBIntValue(objIDataReader["Rating"], 0);
                            objBannersViewModel.Title = sqlhelper.GetDBStringValue(objIDataReader["Title"], string.Empty);
                            objBannersViewModel.Comment = sqlhelper.GetDBStringValue(objIDataReader["Comment"], string.Empty);
                            objBannersViewModel.Image = sqlhelper.GetDBStringValue(objIDataReader["Image"], string.Empty);

                            objBannersViewModel.BranchID = sqlhelper.GetDBIntValue(objIDataReader["BranchID"], 0);
                            objBannersViewModel.Address1 = sqlhelper.GetDBStringValue(objIDataReader["Address1"], string.Empty);

                            if (!objBannersViewModel.Image.Equals(string.Empty))
                            {
                                objBannersViewModel.ImageURL = string.Format("/{0}/{1}/{2}", ConfigurationManager.AppSettings["BannerImagePath"], "imagepath", objBannersViewModel.Image);
                            }

                            objBannersViewModellist.Add(objBannersViewModel);
                        }
                    }
                    return objBannersViewModellist;
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
        /// Get Banners Information by BannerID
        /// </summary>
        public BannersViewModel GetBannersByID(Int64 BannerID)
        {
            try
            {
                GetConnection();
                SetSQLCommandParameterWithValue("@BannerID", BannerID);
                IDataReader objIDataReader = GetReaderByCmd("spS_BannersById");
                List<BannersViewModel> objBannersViewModellist = new List<BannersViewModel>();
                objBannersViewModellist = GetBannersParameters(objIDataReader);
                if (objBannersViewModellist != null && objBannersViewModellist.Count > 0)
                {
                    return objBannersViewModellist[0];
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
        /// MultiProcess Like DELETE,ACTIVE,INACTIVE for Banners
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
                    strSPName = "spUS_Banners";
                }
                else if (strMode.ToLower().Equals("multidelete"))
                {
                    strSPName = "spD_Banners";
                }
                else
                {
                    SetSQLCommandParameterWithValue("@Status", "True");
                    strSPName = "spUS_Banners";
                }
                return GetExecuteScalarByCommandInt32(strSPName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Get all values for Banners
        /// </summary>
        public List<BannersViewModel> GetBannersParameters(IDataReader objIDataReader)
        {
            if (objIDataReader != null)
            {
                objBannersViewModellist = new List<BannersViewModel>();
                using (objIDataReader)
                {
                    while (objIDataReader.Read())
                    {
                        objBannersViewModel = new BannersViewModel();
                        objBannersViewModel.BannerID = sqlhelper.GetDBInt64Value(objIDataReader["BannerID"], 0);
                        objBannersViewModel.Rating = sqlhelper.GetDBIntValue(objIDataReader["Rating"], 0);
                        objBannersViewModel.Title = sqlhelper.GetDBStringValue(objIDataReader["Title"], string.Empty);
                        objBannersViewModel.Comment = sqlhelper.GetDBStringValue(objIDataReader["Comment"], string.Empty);
                        objBannersViewModel.Image = sqlhelper.GetDBStringValue(objIDataReader["Image"], string.Empty);
                        objBannersViewModel.CreatedDate = sqlhelper.GetDBDateTimeValue(objIDataReader["CreatedDate"]);
                        objBannersViewModel.CreatedBy = sqlhelper.GetDBInt64Value(objIDataReader["CreatedBy"], 0);
                        objBannersViewModel.ModifiedDate = sqlhelper.GetDBDateTimeValue(objIDataReader["ModifiedDate"]);
                        objBannersViewModel.ModifiedBy = sqlhelper.GetDBInt64Value(objIDataReader["ModifiedBy"], 0);
                        objBannersViewModel.Status = sqlhelper.GetDBBoolValue(objIDataReader["Status"], false);
                        objBannersViewModellist.Add(objBannersViewModel);
                    }
                }
                return objBannersViewModellist;
            }
            else
            {
                return null;
            }
        }
        #endregion
    }
}
