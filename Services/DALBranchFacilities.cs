using Xyngr.helper;
using System;
using System.Collections.Generic;
using System.Data;

using xyngr.Models;

namespace xyngr.Services
{
    public class DALBranchFacilities : DataAccess
    {
        #region Variable Declaration
        BranchFacilitiesViewModel objBranchFacilitiesViewModel;
        List<BranchFacilitiesViewModel> objBranchFacilitiesViewModellist;
        #endregion
        #region Helper Functions
        /// <summary>
        /// Save BranchFacilities Information
        /// </summary>
        public Int64 SaveBranchFacilities(BranchFacilitiesViewModel objBranchFacilitiesViewModel, string strMode)
        {
            try
            {
                GetConnection();
                if (!strMode.Equals("INSERT", StringComparison.CurrentCultureIgnoreCase))
                    SetSQLCommandParameterWithValue("@BranchFacilityID", objBranchFacilitiesViewModel.BranchFacilityID);
                SetSQLCommandParameterWithValue("@FacilityID", objBranchFacilitiesViewModel.FacilityID);
                SetSQLCommandParameterWithValue("@BranchID", objBranchFacilitiesViewModel.BranchID);
                if (strMode.Equals("INSERT", StringComparison.CurrentCultureIgnoreCase))
                {
                    return GetExecuteScalarByCommand("spI_BranchFacilities");
                }
                else
                {
                    return GetExecuteScalarByCommand("spU_BranchFacilities");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Get all BranchFacilities Information
        /// </summary>
        public List<BranchFacilitiesViewModel> GetAllBranchFacilities(String SortBy, String SortOrder, String SearchField, String SearchValue)
        {
            try
            {
                GetConnection();
                SetSQLCommandParameterWithValue("@sortby", SortBy);
                SetSQLCommandParameterWithValue("@sortorder", SortOrder);
                SetSQLCommandParameterWithValue("@searchfield", SearchField);
                SetSQLCommandParameterWithValue("@searchvalue", SearchValue);
                IDataReader objIDataReader = GetReaderByCmd("spS_All_BranchFacilities");
                return GetBranchFacilitiesParameters(objIDataReader);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Get all BranchFacilities Information
        /// </summary>
        public List<BranchFacilitiesViewModel> GetAllBranchFacilitiesForFront(String SortBy, String SortOrder, String SearchField, String SearchValue)
        {
            try
            {
                GetConnection();
                SetSQLCommandParameterWithValue("@sortby", SortBy);
                SetSQLCommandParameterWithValue("@sortorder", SortOrder);
                SetSQLCommandParameterWithValue("@searchfield", SearchField);
                SetSQLCommandParameterWithValue("@searchvalue", SearchValue);
                IDataReader objIDataReader = GetReaderByCmd("spS_All_BranchFacilitiesForFront");
                return GetBranchFacilitiesParameters(objIDataReader);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Get BranchFacilities Information by BranchFacilityID
        /// </summary>
        public BranchFacilitiesViewModel GetBranchFacilitiesByID(Int64 BranchFacilityID)
        {
            try
            {
                GetConnection();
                SetSQLCommandParameterWithValue("@BranchFacilityID", BranchFacilityID);
                IDataReader objIDataReader = GetReaderByCmd("spS_BranchFacilitiesById");
                List<BranchFacilitiesViewModel> objBranchFacilitiesViewModellist = new List<BranchFacilitiesViewModel>();
                objBranchFacilitiesViewModellist = GetBranchFacilitiesParameters(objIDataReader);
                if (objBranchFacilitiesViewModellist != null && objBranchFacilitiesViewModellist.Count > 0)
                {
                    return objBranchFacilitiesViewModellist[0];
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
        /// Get BranchFacilities Information by BranchFacilityID
        /// </summary>
        //public BranchFacilitiesViewModel GetBranchFacilitiesByDesireUrl(String DesireUrl)
        //{
        //    try
        //    {
        //        GetConnection();
        //        SetSQLCommandParameterWithValue("@DesireUrl", DesireUrl);
        //        IDataReader objIDataReader = GetReaderByCmd("spS_BranchFacilitiesByDesireUrl");
        //        List<BranchFacilitiesViewModel> objBranchFacilitiesViewModellist = new List<BranchFacilitiesViewModel>();
        //        objBranchFacilitiesViewModellist = GetBranchFacilitiesParameters(objIDataReader);
        //        if (objBranchFacilitiesViewModellist != null && objBOBranchFacilitieslist.Count > 0)
        //        {
        //            return objBranchFacilitiesViewModellist[0];
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
        /// MultiProcess Like DELETE,ACTIVE,INACTIVE for BranchFacilities
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
                    strSPName = "spUS_BranchFacilities";
                }
                else if (strMode.ToLower().Equals("multidelete"))
                {
                    strSPName = "spD_BranchFacilities";
                }
                else
                {
                    SetSQLCommandParameterWithValue("@Status", "True");
                    strSPName = "spUS_BranchFacilities";
                }
                return GetExecuteScalarByCommandInt32(strSPName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Get all values for BranchFacilities
        /// </summary>
        public List<BranchFacilitiesViewModel> GetBranchFacilitiesParameters(IDataReader objIDataReader)
        {
            if (objIDataReader != null)
            {
                objBranchFacilitiesViewModellist = new List<BranchFacilitiesViewModel>();
                using (objIDataReader)
                {
                    while (objIDataReader.Read())
                    {
                        objBranchFacilitiesViewModel = new BranchFacilitiesViewModel();
                        objBranchFacilitiesViewModel.BranchFacilityID = sqlhelper.GetDBInt64Value(objIDataReader["BranchFacilityID"], 0);
                        objBranchFacilitiesViewModel.FacilityID = sqlhelper.GetDBInt64Value(objIDataReader["FacilityID"], 0);
                        objBranchFacilitiesViewModel.BranchID = sqlhelper.GetDBInt64Value(objIDataReader["BranchID"], 0);
                        objBranchFacilitiesViewModellist.Add(objBranchFacilitiesViewModel);
                    }
                }
                return objBranchFacilitiesViewModellist;
            }
            else
            {
                return null;
            }
        }
        #endregion
    }
}
