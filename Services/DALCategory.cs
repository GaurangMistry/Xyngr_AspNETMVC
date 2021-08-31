using Xyngr.helper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;

using xyngr.Models;

namespace xyngr.Services
{
    public class DALCategory : DataAccess
    {
        #region Variable Declaration
        CategoryViewModel objCategoryViewModel;
        List<CategoryViewModel> objCategoryViewModellist;
        #endregion
        #region Helper Functions
        /// <summary>
        /// Save Category Information
        /// </summary>
        public Int64 SaveCategory(CategoryViewModel objCategoryViewModel, string strMode)
        {
            try
            {
                GetConnection();
                if (!strMode.Equals("INSERT", StringComparison.CurrentCultureIgnoreCase))
                    SetSQLCommandParameterWithValue("@CategoryID", objCategoryViewModel.CategoryID);
                SetSQLCommandParameterWithValue("@ParentCategoryID", objCategoryViewModel.ParentCategoryID);
                SetSQLCommandParameterWithValue("@Category", objCategoryViewModel.CategoryName);
                SetSQLCommandParameterWithValue("@Image", objCategoryViewModel.Image);
                SetSQLCommandParameterWithValue("@MetaAuthorContent", objCategoryViewModel.MetaAuthorContent);
                SetSQLCommandParameterWithValue("@MetaDescContent", objCategoryViewModel.MetaDescContent);
                SetSQLCommandParameterWithValue("@MetaKeyWordContent", objCategoryViewModel.MetaKeyWordContent);

                SetSQLCommandParameterWithValue("@Status", objCategoryViewModel.Status);
                if (strMode.Equals("INSERT", StringComparison.CurrentCultureIgnoreCase))
                {
                    return GetExecuteScalarByCommand("spI_Category");
                }
                else
                {
                    return GetExecuteScalarByCommand("spU_Category");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Get all Category Information
        /// </summary>
        public List<CategoryViewModel> GetAllCategory(String SortBy, String SortOrder, String SearchField, String SearchValue)
        {
            try
            {
                GetConnection();
                SetSQLCommandParameterWithValue("@sortby", SortBy);
                SetSQLCommandParameterWithValue("@sortorder", SortOrder);
                SetSQLCommandParameterWithValue("@searchfield", SearchField);
                SetSQLCommandParameterWithValue("@searchvalue", SearchValue);
                IDataReader objIDataReader = GetReaderByCmd("spS_All_Category");
                return GetCategoryParameters(objIDataReader);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Get all Category Information
        /// </summary>
        public List<CategoryViewModel> GetAllCategoryForFront()
        {
            try
            {
                GetConnection();
                IDataReader objIDataReader = GetReaderByCmd("spS_All_CategoryForFront");
                return GetCategoryParameters(objIDataReader);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get all Category Information
        /// </summary>
        public List<CategoryViewModel> GetCategoryByParentIDForFront(Int64 ParentCategoryID)
        {
            try
            {
                GetConnection();
                SetSQLCommandParameterWithValue("@ParentCategoryID", ParentCategoryID);
                IDataReader objIDataReader = GetReaderByCmd("spS_CategoryByParentCategoryID");
                return GetCategoryParameters(objIDataReader);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get Category Information by CategoryID
        /// </summary>
        public CategoryViewModel GetCategoryByID(Int64 CategoryID)
        {
            try
            {
                GetConnection();
                SetSQLCommandParameterWithValue("@CategoryID", CategoryID);
                IDataReader objIDataReader = GetReaderByCmd("spS_CategoryById");
                List<CategoryViewModel> objCategoryViewModellist = new List<CategoryViewModel>();
                objCategoryViewModellist = GetCategoryParameters(objIDataReader);
                if (objCategoryViewModellist != null && objCategoryViewModellist.Count > 0)
                {
                    return objCategoryViewModellist[0];
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
        /// Get Category Information by CategoryID
        /// </summary>
        //public CategoryViewModel GetCategoryByDesireUrl(String DesireUrl)
        //{
        //    try
        //    {
        //        GetConnection();
        //        SetSQLCommandParameterWithValue("@DesireUrl", DesireUrl);
        //        IDataReader objIDataReader = GetReaderByCmd("spS_CategoryByDesireUrl");
        //        List<CategoryViewModel> objCategoryViewModellist = new List<CategoryViewModel>();
        //        objCategoryViewModellist = GetCategoryParameters(objIDataReader);
        //        if (objCategoryViewModellist != null && objBOCategorylist.Count > 0)
        //        {
        //            return objCategoryViewModellist[0];
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
        /// MultiProcess Like DELETE,ACTIVE,INACTIVE for Category
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
                    strSPName = "spUS_Category";
                }
                else if (strMode.ToLower().Equals("multidelete"))
                {
                    strSPName = "spD_Category";
                }
                else
                {
                    SetSQLCommandParameterWithValue("@Status", "True");
                    strSPName = "spUS_Category";
                }
                return GetExecuteScalarByCommandInt32(strSPName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Get all values for Category
        /// </summary>
        public List<CategoryViewModel> GetCategoryParameters(IDataReader objIDataReader)
        {
            if (objIDataReader != null)
            {
                objCategoryViewModellist = new List<CategoryViewModel>();
                using (objIDataReader)
                {
                    while (objIDataReader.Read())
                    {
                        objCategoryViewModel = new CategoryViewModel();
                        objCategoryViewModel.CategoryID = sqlhelper.GetDBInt64Value(objIDataReader["CategoryID"], 0);
                        objCategoryViewModel.ParentCategoryID = sqlhelper.GetDBInt64Value(objIDataReader["ParentCategoryID"], 0);
                        objCategoryViewModel.CategoryName = sqlhelper.GetDBStringValue(objIDataReader["Category"], string.Empty);
                        objCategoryViewModel.ParentCategory = sqlhelper.GetDBStringValue(objIDataReader["ParentCategory"], string.Empty);
                        objCategoryViewModel.CreatedDate = sqlhelper.GetDBDateTimeValue(objIDataReader["CreatedDate"]);
                        objCategoryViewModel.CreatedBy = sqlhelper.GetDBInt64Value(objIDataReader["CreatedBy"], 0);
                        objCategoryViewModel.ModifiedDate = sqlhelper.GetDBDateTimeValue(objIDataReader["ModifiedDate"]);
                        objCategoryViewModel.ModifiedBy = sqlhelper.GetDBInt64Value(objIDataReader["ModifiedBy"], 0);
                        objCategoryViewModel.Status = sqlhelper.GetDBBoolValue(objIDataReader["Status"], false);

                        try
                        {
                            objCategoryViewModel.Image = sqlhelper.GetDBStringValue(objIDataReader["Image"], string.Empty);
                            if(!objCategoryViewModel.Image.Equals(string.Empty))
                            {
                                objCategoryViewModel.ImageURL = string.Format("/{0}/{1}/{2}", ConfigurationManager.AppSettings["CategoryImagePath"], "categorypath", objCategoryViewModel.Image);
                            }
                            else
                            {
                                objCategoryViewModel.ImageURL = "/images/noimage.png";
                            }
                            objCategoryViewModel.MetaAuthorContent = sqlhelper.GetDBStringValue(objIDataReader["MetaAuthorContent"], string.Empty);
                            objCategoryViewModel.MetaDescContent = sqlhelper.GetDBStringValue(objIDataReader["MetaDescContent"], string.Empty);
                            objCategoryViewModel.MetaKeyWordContent = sqlhelper.GetDBStringValue(objIDataReader["MetaKeyWordContent"], string.Empty);
                        }
                        catch(Exception ex) { }

                        objCategoryViewModellist.Add(objCategoryViewModel);
                    }
                }
                return objCategoryViewModellist;
            }
            else
            {
                return null;
            }
        }

        
        #endregion
    }
}
