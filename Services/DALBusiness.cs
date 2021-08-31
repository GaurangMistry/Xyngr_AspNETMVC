using Xyngr.helper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;

using xyngr.Models;

namespace xyngr.Services
{
    public class DALBusiness : DataAccess
    {
        #region Variable Declaration
        BusinessViewModel objBusinessViewModel;
        List<BusinessViewModel> objBusinessViewModellist;
        #endregion
        #region Helper Functions
        /// <summary>
        /// Save Business Information
        /// </summary>
        public Int64 SaveBusiness(BusinessViewModel objBusinessViewModel, string strMode)
        {
            try
            {
                GetConnection();
                if (!strMode.Equals("INSERT", StringComparison.CurrentCultureIgnoreCase))
                    SetSQLCommandParameterWithValue("@BusinessID", objBusinessViewModel.BusinessID);
                SetSQLCommandParameterWithValue("@UserID", objBusinessViewModel.UserID);
                SetSQLCommandParameterWithValue("@ParentCategoryID", objBusinessViewModel.ParentCategoryID);
                SetSQLCommandParameterWithValue("@CategoryID", objBusinessViewModel.CategoryID);
                SetSQLCommandParameterWithValue("@Name", objBusinessViewModel.Name);
                SetSQLCommandParameterWithValue("@Image", objBusinessViewModel.Image);
                SetSQLCommandParameterWithValue("@MetaAuthorContent", objBusinessViewModel.MetaAuthorContent);
                SetSQLCommandParameterWithValue("@MetaDescContent", objBusinessViewModel.MetaDescContent);
                SetSQLCommandParameterWithValue("@MetaKeyWordContent", objBusinessViewModel.MetaKeyWordContent);

                SetSQLCommandParameterWithValue("@Status", objBusinessViewModel.Status);
                if (strMode.Equals("INSERT", StringComparison.CurrentCultureIgnoreCase))
                {
                    return GetExecuteScalarByCommand("spI_Business");
                }
                else
                {
                    return GetExecuteScalarByCommand("spU_Business");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Get all Business Information
        /// </summary>
        public List<BusinessViewModel> GetAllBusiness(String SortBy, String SortOrder, String SearchField, String SearchValue)
        {
            try
            {
                GetConnection();
                SetSQLCommandParameterWithValue("@sortby", SortBy);
                SetSQLCommandParameterWithValue("@sortorder", SortOrder);
                SetSQLCommandParameterWithValue("@searchfield", SearchField);
                SetSQLCommandParameterWithValue("@searchvalue", SearchValue);
                IDataReader objIDataReader = GetReaderByCmd("spS_All_Business");
                return GetBusinessParameters(objIDataReader);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get all ImageGallery Information
        /// </summary>
        public List<BusinessViewModel> GetAllBusinessByCategoryID(Int64 categoryID)
        {
            try
            {
                GetConnection();
                SetSQLCommandParameterWithValue("@CategoryID", categoryID);
                IDataReader objIDataReader = GetReaderByCmd("spS_BusinessByCategoryId");
                return GetBusinessParameters(objIDataReader);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get all ImageGallery Information
        /// </summary>
        public List<BusinessViewModel> GetAllBusinessByCategoryIDForFront(Int64 categoryID)
        {
            try
            {
                GetConnection();
                SetSQLCommandParameterWithValue("@CategoryID", categoryID);
                IDataReader objIDataReader = GetReaderByCmd("spS_BusinessByCategoryIdForFront");
                return GetBusinessParameters(objIDataReader);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get all Business Information
        /// </summary>
        public List<BusinessViewModel> GetAllBusinessForFrontHome(int NoOfBusiness)
        {
            try
            {
                GetConnection();
                SetSQLCommandParameterWithValue("@NoOfBusiness", NoOfBusiness);
                IDataReader objIDataReader = GetReaderByCmd("spS_GetAllBusinessForFrontHome");
                return GetBusinessParameters(objIDataReader);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get all Business Information
        /// </summary>
        public List<BusinessViewModel> GetAllBusinessForFront()
        {
            try
            {
                GetConnection();
                IDataReader objIDataReader = GetReaderByCmd("spS_All_BusinessForFront");
                return GetBusinessParameters(objIDataReader);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Get Business Information by BusinessID
        /// </summary>
        public BusinessViewModel GetBusinessByID(Int64 BusinessID)
        {
            try
            {
                GetConnection();
                SetSQLCommandParameterWithValue("@BusinessID", BusinessID);
                IDataReader objIDataReader = GetReaderByCmd("spS_BusinessById");
                List<BusinessViewModel> objBusinessViewModellist = new List<BusinessViewModel>();
                objBusinessViewModellist = GetBusinessParameters(objIDataReader);
                if (objBusinessViewModellist != null && objBusinessViewModellist.Count > 0)
                {
                    return objBusinessViewModellist[0];
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
        /// Get Business Information by BusinessID
        /// </summary>
        //public BusinessViewModel GetBusinessByDesireUrl(String DesireUrl)
        //{
        //    try
        //    {
        //        GetConnection();
        //        SetSQLCommandParameterWithValue("@DesireUrl", DesireUrl);
        //        IDataReader objIDataReader = GetReaderByCmd("spS_BusinessByDesireUrl");
        //        List<BusinessViewModel> objBusinessViewModellist = new List<BusinessViewModel>();
        //        objBusinessViewModellist = GetBusinessParameters(objIDataReader);
        //        if (objBusinessViewModellist != null && objBOBusinesslist.Count > 0)
        //        {
        //            return objBusinessViewModellist[0];
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
        /// MultiProcess Like DELETE,ACTIVE,INACTIVE for Business
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
                    strSPName = "spUS_Business";
                }
                else if (strMode.ToLower().Equals("multidelete"))
                {
                    strSPName = "spD_Business";
                }
                else
                {
                    SetSQLCommandParameterWithValue("@Status", "True");
                    strSPName = "spUS_Business";
                }
                return GetExecuteScalarByCommandInt32(strSPName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Get all values for Business
        /// </summary>
        public List<BusinessViewModel> GetBusinessParameters(IDataReader objIDataReader)
        {
            if (objIDataReader != null)
            {
                objBusinessViewModellist = new List<BusinessViewModel>();
                using (objIDataReader)
                {
                    while (objIDataReader.Read())
                    {
                        objBusinessViewModel = new BusinessViewModel();
                        objBusinessViewModel.BusinessID = sqlhelper.GetDBInt64Value(objIDataReader["BusinessID"], 0);
                        objBusinessViewModel.UserID = sqlhelper.GetDBInt64Value(objIDataReader["UserID"], 0);
                        objBusinessViewModel.ParentCategoryID = sqlhelper.GetDBInt64Value(objIDataReader["ParentCategoryID"], 0);
                        objBusinessViewModel.CategoryID = sqlhelper.GetDBInt64Value(objIDataReader["CategoryID"], 0);
                        objBusinessViewModel.Name = sqlhelper.GetDBStringValue(objIDataReader["Name"], string.Empty);
                        
                        objBusinessViewModel.CreatedDate = sqlhelper.GetDBDateTimeValue(objIDataReader["CreatedDate"]);
                        objBusinessViewModel.CreatedBy = sqlhelper.GetDBInt64Value(objIDataReader["CreatedBy"], 0);
                        objBusinessViewModel.ModifiedDate = sqlhelper.GetDBDateTimeValue(objIDataReader["ModifiedDate"]);
                        objBusinessViewModel.ModifiedBy = sqlhelper.GetDBInt64Value(objIDataReader["ModifiedBy"], 0);
                        objBusinessViewModel.Status = sqlhelper.GetDBBoolValue(objIDataReader["Status"], false);

                        try
                        {
                            objBusinessViewModel.Image = sqlhelper.GetDBStringValue(objIDataReader["Image"], string.Empty);
                            if (!objBusinessViewModel.Image.Equals(string.Empty))
                            {
                                objBusinessViewModel.ImageURL = string.Format("/{0}/{1}/{2}", ConfigurationManager.AppSettings["BusinessImagePath"], "businesspath", objBusinessViewModel.Image);
                            }
                            else
                            {
                                objBusinessViewModel.ImageURL = "/images/noimage.png";
                            }

                            objBusinessViewModel.MetaAuthorContent = sqlhelper.GetDBStringValue(objIDataReader["MetaAuthorContent"], string.Empty);
                            objBusinessViewModel.MetaDescContent = sqlhelper.GetDBStringValue(objIDataReader["MetaDescContent"], string.Empty);
                            objBusinessViewModel.MetaKeyWordContent = sqlhelper.GetDBStringValue(objIDataReader["MetaKeyWordContent"], string.Empty);
                        }
                        catch (Exception ex) { }

                        try
                        {
                            objBusinessViewModel.ParentCategory = sqlhelper.GetDBStringValue(objIDataReader["ParentCategory"], string.Empty);
                            objBusinessViewModel.Category = sqlhelper.GetDBStringValue(objIDataReader["Category"], string.Empty);

                            objBusinessViewModel.TotalBranch = sqlhelper.GetDBIntValue(objIDataReader["TotalBranch"], 0);
                        }
                        catch(Exception ex) { }

                        objBusinessViewModellist.Add(objBusinessViewModel);
                    }
                }
                return objBusinessViewModellist;
            }
            else
            {
                return null;
            }
        }
        #endregion
    }
}
