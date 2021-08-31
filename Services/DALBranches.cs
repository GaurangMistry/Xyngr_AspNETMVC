using Xyngr.helper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using xyngr.Models;

namespace xyngr.Services
{
    public class DALBranches : DataAccess
    {
        #region Variable Declaration
        BranchesViewModel objBranchesViewModel;
        List<BranchesViewModel> objBranchesViewModellist;
        #endregion
        #region Helper Functions
        /// <summary>
        /// Save Branches Information
        /// </summary>
        public Int64 SaveBranches(BranchesViewModel objBranchesViewModel, string strMode)
        {
            try
            {
                GetConnection();
                if (!strMode.Equals("INSERT", StringComparison.CurrentCultureIgnoreCase))
                    SetSQLCommandParameterWithValue("@BranchID", objBranchesViewModel.BranchID);

                SetSQLCommandParameterWithValue("@BusinessID", objBranchesViewModel.BusinessID);
                SetSQLCommandParameterWithValue("@Address1", objBranchesViewModel.Address1);
                SetSQLCommandParameterWithValue("@Address2", objBranchesViewModel.Address2);
                SetSQLCommandParameterWithValue("@City", objBranchesViewModel.City);
                SetSQLCommandParameterWithValue("@State", objBranchesViewModel.State);
                SetSQLCommandParameterWithValue("@Country", objBranchesViewModel.Country);
                SetSQLCommandParameterWithValue("@Longitude", objBranchesViewModel.Longitude);
                SetSQLCommandParameterWithValue("@Latitude", objBranchesViewModel.Latitude);
                SetSQLCommandParameterWithValue("@Phone", objBranchesViewModel.Phone);
                SetSQLCommandParameterWithValue("@Email", objBranchesViewModel.Email);
                SetSQLCommandParameterWithValue("@SmallDescription", objBranchesViewModel.SmallDescription);
                SetSQLCommandParameterWithValue("@Description", objBranchesViewModel.Description);
                SetSQLCommandParameterWithValue("@OpeningHours", objBranchesViewModel.OpeningHours);
                SetSQLCommandParameterWithValue("@Rating", objBranchesViewModel.Rating);
                SetSQLCommandParameterWithValue("@IsFeatured", objBranchesViewModel.IsFeatured);
                SetSQLCommandParameterWithValue("@IsNew", objBranchesViewModel.IsNew);
                SetSQLCommandParameterWithValue("@SalePeriod", objBranchesViewModel.SalePeriod);
                SetSQLCommandParameterWithValue("@ProfileImage", objBranchesViewModel.ProfileImage);
                SetSQLCommandParameterWithValue("@ExternalURL", objBranchesViewModel.ExternalURL);
                SetSQLCommandParameterWithValue("@BannerID", objBranchesViewModel.BannerID);
                SetSQLCommandParameterWithValue("@MetaAuthorContent", objBranchesViewModel.MetaAuthorContent);
                SetSQLCommandParameterWithValue("@MetaDescContent", objBranchesViewModel.MetaDescContent);
                SetSQLCommandParameterWithValue("@MetaKeyWordContent", objBranchesViewModel.MetaKeyWordContent);
                SetSQLCommandParameterWithValue("@Status", objBranchesViewModel.Status);
                if (strMode.Equals("INSERT", StringComparison.CurrentCultureIgnoreCase))
                {
                    return GetExecuteScalarByCommand("spI_Branches");
                }
                else
                {
                    return GetExecuteScalarByCommand("spU_Branches");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Get all Branches Information
        /// </summary>
        public List<BranchesViewModel> GetAllBranches(String SortBy, String SortOrder, String SearchField, String SearchValue)
        {
            try
            {
                GetConnection();
                SetSQLCommandParameterWithValue("@sortby", SortBy);
                SetSQLCommandParameterWithValue("@sortorder", SortOrder);
                SetSQLCommandParameterWithValue("@searchfield", SearchField);
                SetSQLCommandParameterWithValue("@searchvalue", SearchValue);
                IDataReader objIDataReader = GetReaderByCmd("spS_All_Branches");
                return GetBranchesParameters(objIDataReader);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get all ImageGallery Information
        /// </summary>
        public List<BranchesViewModel> GetAllBranchByBusinessID(Int64 BusinessID)
        {
            try
            {
                GetConnection();
                SetSQLCommandParameterWithValue("@BusinessID", BusinessID);
                IDataReader objIDataReader = GetReaderByCmd("spS_BranchByBusinessId");
                return GetBranchesParameters(objIDataReader);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get Branch For Notification
        /// </summary>
        public List<BranchesViewModel> GetBranchForNotification()
        {
            try
            {
                GetConnection();

                IDataReader objIDataReader = GetReaderByCmd("spS_BranchForNotification");
                return GetBranchesParameters(objIDataReader);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get all ImageGallery Information
        /// </summary>
        public List<BranchesViewModel> GetAllBranchByBusinessIDForFront(Int64 BusinessID)
        {
            try
            {
                GetConnection();
                SetSQLCommandParameterWithValue("@BusinessID", BusinessID);
                IDataReader objIDataReader = GetReaderByCmd("spS_BranchByBusinessIdForFront");
                return GetBranchesParameters(objIDataReader);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get all Branches Information
        /// </summary>
        public List<BranchesViewModel> GetAllBranchesForFront(Int64 ParentCategoryID, Int64 CategoryID, string Location)
        {
            try
            {
                GetConnection();
                SetSQLCommandParameterWithValue("@ParentCategoryID", ParentCategoryID);
                SetSQLCommandParameterWithValue("@CategoryID", CategoryID);
                SetSQLCommandParameterWithValue("@Location", Location);

                IDataReader objIDataReader = GetReaderByCmd("spS_All_BranchesForFront");
                return GetBranchesParameters(objIDataReader);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get all Branches Information
        /// </summary>
        public List<BranchesViewModel> GetAllBranchesForFront(string ParentCategory, string Category, string Location)
        {
            try
            {
                GetConnection();
                SetSQLCommandParameterWithValue("@ParentCategory", ParentCategory);
                SetSQLCommandParameterWithValue("@Category", Category);
                SetSQLCommandParameterWithValue("@Location", Location);

                IDataReader objIDataReader = GetReaderByCmd("spS_All_BranchesForFront");
                return GetBranchesParameters(objIDataReader);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get all Branches Information
        /// </summary>
        public List<BranchesViewModel> GetPopularBranchesForFrontHome()
        {
            try
            {
                GetConnection();
                
                IDataReader objIDataReader = GetReaderByCmd("spS_GetPopularBranchesForFrontHome");
                return GetBranchesParameters(objIDataReader);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<BranchesViewModel> GetPopularBranchesForFrontDetailPage(int branchID , int categoryID)
        {
            try
            {
                GetConnection();
                SetSQLCommandParameterWithValue("@CategoryID", categoryID);
                SetSQLCommandParameterWithValue("@BranchID", branchID);

                IDataReader objIDataReader = GetReaderByCmd("spS_GetPopularBranchesForFrontDetailPage");
                return GetBranchesParameters(objIDataReader);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<BranchesViewModel> GetRelatedBranchesForFrontDetailPage(int branchID, int categoryID , string location)
        {
            try
            {
                GetConnection();
                SetSQLCommandParameterWithValue("@CategoryID", categoryID);
                SetSQLCommandParameterWithValue("@BranchID", branchID);
                SetSQLCommandParameterWithValue("@Location", location);

                IDataReader objIDataReader = GetReaderByCmd("spS_GetRelatedBranchesForFrontDetailPage");
                return GetBranchesParameters(objIDataReader);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Get all Branches Information
        /// </summary>
        public List<BranchesViewModel> GetFeaturedBranchesForFrontHome()
        {
            try
            {
                GetConnection();

                IDataReader objIDataReader = GetReaderByCmd("spS_GetFeaturedBranchesForFrontHome");
                return GetBranchesParameters(objIDataReader);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get all Branches Information
        /// </summary>
        public List<BranchesViewModel> GetNewBranchesForFrontHome()
        {
            try
            {
                GetConnection();

                IDataReader objIDataReader = GetReaderByCmd("spS_GetNewBranchesForFrontHome");
                return GetBranchesParameters(objIDataReader);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get Branches Information by BranchID
        /// </summary>
        public BranchesViewModel GetBranchesByID(Int64 BranchID)
        {
            try
            {
                GetConnection();
                SetSQLCommandParameterWithValue("@BranchID", BranchID);
                IDataReader objIDataReader = GetReaderByCmd("spS_BranchesById");
                List<BranchesViewModel> objBranchesViewModellist = new List<BranchesViewModel>();
                objBranchesViewModellist = GetBranchesParameters(objIDataReader);
                if (objBranchesViewModellist != null && objBranchesViewModellist.Count > 0)
                {
                    return objBranchesViewModellist[0];
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
        /// Get Branches Information by BranchID
        /// </summary>
        //public BranchesViewModel GetBranchesByDesireUrl(String DesireUrl)
        //{
        //    try
        //    {
        //        GetConnection();
        //        SetSQLCommandParameterWithValue("@DesireUrl", DesireUrl);
        //        IDataReader objIDataReader = GetReaderByCmd("spS_BranchesByDesireUrl");
        //        List<BranchesViewModel> objBranchesViewModellist = new List<BranchesViewModel>();
        //        objBranchesViewModellist = GetBranchesParameters(objIDataReader);
        //        if (objBranchesViewModellist != null && objBOBrancheslist.Count > 0)
        //        {
        //            return objBranchesViewModellist[0];
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
        /// MultiProcess Like DELETE,ACTIVE,INACTIVE for Branches
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
                    strSPName = "spUS_Branches";
                }
                else if (strMode.ToLower().Equals("multidelete"))
                {
                    strSPName = "spD_Branches";
                }
                else
                {
                    SetSQLCommandParameterWithValue("@Status", "True");
                    strSPName = "spUS_Branches";
                }
                return GetExecuteScalarByCommandInt32(strSPName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Get all values for Branches
        /// </summary>
        public List<BranchesViewModel> GetBranchesParameters(IDataReader objIDataReader)
        {
            if (objIDataReader != null)
            {
                objBranchesViewModellist = new List<BranchesViewModel>();
                using (objIDataReader)
                {
                    while (objIDataReader.Read())
                    {
                        objBranchesViewModel = new BranchesViewModel();
                        objBranchesViewModel.BranchID = sqlhelper.GetDBInt64Value(objIDataReader["BranchID"], 0);
                        objBranchesViewModel.BusinessID = sqlhelper.GetDBInt64Value(objIDataReader["BusinessID"], 0);
                        objBranchesViewModel.Address1 = sqlhelper.GetDBStringValue(objIDataReader["Address1"], string.Empty);
                        objBranchesViewModel.Address2 = sqlhelper.GetDBStringValue(objIDataReader["Address2"], string.Empty);
                        objBranchesViewModel.City = sqlhelper.GetDBStringValue(objIDataReader["City"], string.Empty);
                        objBranchesViewModel.State = sqlhelper.GetDBStringValue(objIDataReader["State"], string.Empty);
                        objBranchesViewModel.Country = sqlhelper.GetDBStringValue(objIDataReader["Country"], string.Empty);
                        objBranchesViewModel.Longitude = sqlhelper.GetDBStringValue(objIDataReader["Longitude"], string.Empty);
                        objBranchesViewModel.Latitude = sqlhelper.GetDBStringValue(objIDataReader["Latitude"], string.Empty);
                        objBranchesViewModel.Phone = sqlhelper.GetDBStringValue(objIDataReader["Phone"], string.Empty);
                        objBranchesViewModel.Email = sqlhelper.GetDBStringValue(objIDataReader["Email"], string.Empty);
                        objBranchesViewModel.SmallDescription = sqlhelper.GetDBStringValue(objIDataReader["SmallDescription"], string.Empty);
                        objBranchesViewModel.Description = sqlhelper.GetDBStringValue(objIDataReader["Description"], string.Empty);
                        objBranchesViewModel.OpeningHours = sqlhelper.GetDBStringValue(objIDataReader["OpeningHours"], string.Empty);
                        objBranchesViewModel.Rating = sqlhelper.GetDBIntValue(objIDataReader["Rating"], 0);
                        objBranchesViewModel.IsFeatured = sqlhelper.GetDBBoolValue(objIDataReader["IsFeatured"], false);
                        objBranchesViewModel.IsNew = sqlhelper.GetDBBoolValue(objIDataReader["IsNew"], false);
                        objBranchesViewModel.SalePeriod = sqlhelper.GetDBStringValue(objIDataReader["SalePeriod"], string.Empty);
                        objBranchesViewModel.ProfileImage = sqlhelper.GetDBStringValue(objIDataReader["ProfileImage"], string.Empty);
                        objBranchesViewModel.ExternalURL = sqlhelper.GetDBStringValue(objIDataReader["ExternalURL"], string.Empty);
                        objBranchesViewModel.MetaAuthorContent = sqlhelper.GetDBStringValue(objIDataReader["MetaAuthorContent"], string.Empty);
                        objBranchesViewModel.MetaDescContent = sqlhelper.GetDBStringValue(objIDataReader["MetaDescContent"], string.Empty);
                        objBranchesViewModel.MetaKeyWordContent = sqlhelper.GetDBStringValue(objIDataReader["MetaKeyWordContent"], string.Empty);
                        objBranchesViewModel.CreatedDate = sqlhelper.GetDBDateTimeValue(objIDataReader["CreatedDate"]);
                        objBranchesViewModel.CreatedBy = sqlhelper.GetDBInt64Value(objIDataReader["CreatedBy"], 0);
                        objBranchesViewModel.ModifiedDate = sqlhelper.GetDBDateTimeValue(objIDataReader["ModifiedDate"]);
                        objBranchesViewModel.ModifiedBy = sqlhelper.GetDBInt64Value(objIDataReader["ModifiedBy"], 0);
                        objBranchesViewModel.Status = sqlhelper.GetDBBoolValue(objIDataReader["Status"], false);

                        try
                        {
                            objBranchesViewModel.BannerID = sqlhelper.GetDBInt64Value(objIDataReader["BannerID"], 0);
                        }
                        catch(Exception ex) { }
                        try
                        {
                            if(!objBranchesViewModel.ProfileImage.Equals(string.Empty))
                            {
                                objBranchesViewModel.ImageURL = string.Format("/{0}/{1}/{2}", ConfigurationManager.AppSettings["BranchImagePath"], "branchpath", objBranchesViewModel.ProfileImage);
                            }
                            else
                            {
                                objBranchesViewModel.ImageURL = "/images/noimage.png";
                            }
                        }
                        catch(Exception ex) { }

                        try
                        {
                            objBranchesViewModel.BusinessName = sqlhelper.GetDBStringValue(objIDataReader["BusinessName"], string.Empty);
                            objBranchesViewModel.ParentCategory = sqlhelper.GetDBStringValue(objIDataReader["ParentCategory"], string.Empty);
                            objBranchesViewModel.Category = sqlhelper.GetDBStringValue(objIDataReader["Category"], string.Empty);
                            objBranchesViewModel.CategoryID = sqlhelper.GetDBIntValue(objIDataReader["CategoryID"], 0);
                            
                        }
                        catch(Exception ex) { }

                        try
                        {
                            objBranchesViewModel.TotalImage = sqlhelper.GetDBIntValue(objIDataReader["TotalImage"], 0);
                            objBranchesViewModel.TotalMedia = sqlhelper.GetDBIntValue(objIDataReader["TotalMedia"], 0);
                            objBranchesViewModel.TotalComment = sqlhelper.GetDBIntValue(objIDataReader["TotalComment"], 0);
                        }
                        catch(Exception ex) { }

                        try
                        {
                            objBranchesViewModel.Comment = sqlhelper.GetDBStringValue(objIDataReader["Comment"], string.Empty);
                        }
                        catch (Exception ex) { }

                        objBranchesViewModellist.Add(objBranchesViewModel);
                    }
                }
                return objBranchesViewModellist;
            }
            else
            {
                return null;
            }
        }
        #endregion
    }
}
