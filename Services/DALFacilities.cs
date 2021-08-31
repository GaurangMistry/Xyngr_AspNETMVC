using Xyngr.helper;
using System;
using System.Collections.Generic;
using System.Data;
using xyngr.Models;

namespace xyngr.Services
{
    public class DALFacilities : DataAccess
    {
        #region Variable Declaration
        FacilitiesViewModel objFacilitiesViewModel;
        List<FacilitiesViewModel> objFacilitiesViewModellist;
        #endregion
        #region Helper Functions
        /// <summary>
        /// Save Facilities Information
        /// </summary>
        public Int64 SaveFacilities(FacilitiesViewModel objFacilitiesViewModel, string strMode)
        {
            try
            {
                GetConnection();
                if (!strMode.Equals("INSERT", StringComparison.CurrentCultureIgnoreCase))
                    SetSQLCommandParameterWithValue("@FacilityID", objFacilitiesViewModel.FacilityID);
                SetSQLCommandParameterWithValue("@ParentCategoryID", objFacilitiesViewModel.ParentCategoryID);
                SetSQLCommandParameterWithValue("@CategoryID", objFacilitiesViewModel.CategoryID);
                SetSQLCommandParameterWithValue("@Facility", objFacilitiesViewModel.Facility);
                SetSQLCommandParameterWithValue("@Status", objFacilitiesViewModel.Status);
                if (strMode.Equals("INSERT", StringComparison.CurrentCultureIgnoreCase))
                {
                    return GetExecuteScalarByCommand("spI_Facilities");
                }
                else
                {
                    return GetExecuteScalarByCommand("spU_Facilities");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Get all Facilities Information
        /// </summary>
        public List<FacilitiesViewModel> GetAllFacilities(String SortBy, String SortOrder, String SearchField, String SearchValue)
        {
            try
            {
                GetConnection();
                SetSQLCommandParameterWithValue("@sortby", SortBy);
                SetSQLCommandParameterWithValue("@sortorder", SortOrder);
                SetSQLCommandParameterWithValue("@searchfield", SearchField);
                SetSQLCommandParameterWithValue("@searchvalue", SearchValue);
                IDataReader objIDataReader = GetReaderByCmd("spS_All_Facilities");
                return GetFacilitiesParameters(objIDataReader);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Get all Facilities Information
        /// </summary>
        public List<FacilitiesViewModel> GetAllFacilitiesForFront(String SortBy, String SortOrder, String SearchField, String SearchValue)
        {
            try
            {
                GetConnection();
                SetSQLCommandParameterWithValue("@sortby", SortBy);
                SetSQLCommandParameterWithValue("@sortorder", SortOrder);
                SetSQLCommandParameterWithValue("@searchfield", SearchField);
                SetSQLCommandParameterWithValue("@searchvalue", SearchValue);
                IDataReader objIDataReader = GetReaderByCmd("spS_All_FacilitiesForFront");
                return GetFacilitiesParameters(objIDataReader);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Get Facilities Information by FacilityID
        /// </summary>
        public FacilitiesViewModel GetFacilitiesByID(Int64 FacilityID)
        {
            try
            {
                GetConnection();
                SetSQLCommandParameterWithValue("@FacilityID", FacilityID);
                IDataReader objIDataReader = GetReaderByCmd("spS_FacilitiesById");
                List<FacilitiesViewModel> objFacilitiesViewModellist = new List<FacilitiesViewModel>();
                objFacilitiesViewModellist = GetFacilitiesParameters(objIDataReader);
                if (objFacilitiesViewModellist != null && objFacilitiesViewModellist.Count > 0)
                {
                    return objFacilitiesViewModellist[0];
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
        /// Get Facilities Information by FacilityID
        /// </summary>
        //public FacilitiesViewModel GetFacilitiesByDesireUrl(String DesireUrl)
        //{
        //    try
        //    {
        //        GetConnection();
        //        SetSQLCommandParameterWithValue("@DesireUrl", DesireUrl);
        //        IDataReader objIDataReader = GetReaderByCmd("spS_FacilitiesByDesireUrl");
        //        List<FacilitiesViewModel> objFacilitiesViewModellist = new List<FacilitiesViewModel>();
        //        objFacilitiesViewModellist = GetFacilitiesParameters(objIDataReader);
        //        if (objFacilitiesViewModellist != null && objBOFacilitieslist.Count > 0)
        //        {
        //            return objFacilitiesViewModellist[0];
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
        /// MultiProcess Like DELETE,ACTIVE,INACTIVE for Facilities
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
                    strSPName = "spUS_Facilities";
                }
                else if (strMode.ToLower().Equals("multidelete"))
                {
                    strSPName = "spD_Facilities";
                }
                else
                {
                    SetSQLCommandParameterWithValue("@Status", "True");
                    strSPName = "spUS_Facilities";
                }
                return GetExecuteScalarByCommandInt32(strSPName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Get all values for Facilities
        /// </summary>
        public List<FacilitiesViewModel> GetFacilitiesParameters(IDataReader objIDataReader)
        {
            if (objIDataReader != null)
            {
                objFacilitiesViewModellist = new List<FacilitiesViewModel>();
                using (objIDataReader)
                {
                    while (objIDataReader.Read())
                    {
                        objFacilitiesViewModel = new FacilitiesViewModel();
                        objFacilitiesViewModel.FacilityID = sqlhelper.GetDBInt64Value(objIDataReader["FacilityID"], 0);
                        objFacilitiesViewModel.ParentCategoryID = sqlhelper.GetDBInt64Value(objIDataReader["ParentCategoryID"], 0);
                        objFacilitiesViewModel.CategoryID = sqlhelper.GetDBInt64Value(objIDataReader["CategoryID"], 0);
                        objFacilitiesViewModel.Facility = sqlhelper.GetDBStringValue(objIDataReader["Facility"], string.Empty);
                        objFacilitiesViewModel.CreatedDate = sqlhelper.GetDBDateTimeValue(objIDataReader["CreatedDate"]);
                        objFacilitiesViewModel.CreatedBy = sqlhelper.GetDBInt64Value(objIDataReader["CreatedBy"], 0);
                        objFacilitiesViewModel.ModifiedDate = sqlhelper.GetDBDateTimeValue(objIDataReader["ModifiedDate"]);
                        objFacilitiesViewModel.ModifiedBy = sqlhelper.GetDBInt64Value(objIDataReader["ModifiedBy"], 0);
                        objFacilitiesViewModel.Status = sqlhelper.GetDBBoolValue(objIDataReader["Status"], false);

                        try
                        {
                            objFacilitiesViewModel.ParentCategory = sqlhelper.GetDBStringValue(objIDataReader["ParentCategory"], string.Empty);
                            objFacilitiesViewModel.Category = sqlhelper.GetDBStringValue(objIDataReader["Category"], string.Empty);
                        }
                        catch (Exception ex) { }

                        objFacilitiesViewModellist.Add(objFacilitiesViewModel);
                    }
                }
                return objFacilitiesViewModellist;
            }
            else
            {
                return null;
            }
        }
        #endregion
    }
}
