using Xyngr.helper;
using System;
using System.Collections.Generic;
using System.Data;

using xyngr.Models;

namespace xyngr.Services
{
    public class DALRoles : DataAccess
    {
        #region Variable Declaration
        RolesViewModel objRolesViewModel;
        List<RolesViewModel> objRolesViewModellist;
        #endregion
        #region Helper Functions
        /// <summary>
        /// Save Roles Information
        /// </summary>
        public Int64 SaveRoles(RolesViewModel objRolesViewModel, string strMode)
        {
            try
            {
                GetConnection();
                if (!strMode.Equals("INSERT", StringComparison.CurrentCultureIgnoreCase))
                    SetSQLCommandParameterWithValue("@RoleID", objRolesViewModel.RoleID);
                SetSQLCommandParameterWithValue("@Role", objRolesViewModel.Role);
                if (strMode.Equals("INSERT", StringComparison.CurrentCultureIgnoreCase))
                {
                    return GetExecuteScalarByCommand("spI_Roles");
                }
                else
                {
                    return GetExecuteScalarByCommand("spU_Roles");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Get all Roles Information
        /// </summary>
        public List<RolesViewModel> GetAllRoles(String SortBy, String SortOrder, String SearchField, String SearchValue)
        {
            try
            {
                GetConnection();
                SetSQLCommandParameterWithValue("@sortby", SortBy);
                SetSQLCommandParameterWithValue("@sortorder", SortOrder);
                SetSQLCommandParameterWithValue("@searchfield", SearchField);
                SetSQLCommandParameterWithValue("@searchvalue", SearchValue);
                IDataReader objIDataReader = GetReaderByCmd("spS_All_Roles");
                return GetRolesParameters(objIDataReader);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Get all Roles Information
        /// </summary>
        public List<RolesViewModel> GetAllRolesForFront(String SortBy, String SortOrder, String SearchField, String SearchValue)
        {
            try
            {
                GetConnection();
                SetSQLCommandParameterWithValue("@sortby", SortBy);
                SetSQLCommandParameterWithValue("@sortorder", SortOrder);
                SetSQLCommandParameterWithValue("@searchfield", SearchField);
                SetSQLCommandParameterWithValue("@searchvalue", SearchValue);
                IDataReader objIDataReader = GetReaderByCmd("spS_All_RolesForFront");
                return GetRolesParameters(objIDataReader);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Get Roles Information by RoleID
        /// </summary>
        public RolesViewModel GetRolesByID(Int64 RoleID)
        {
            try
            {
                GetConnection();
                SetSQLCommandParameterWithValue("@RoleID", RoleID);
                IDataReader objIDataReader = GetReaderByCmd("spS_RolesById");
                List<RolesViewModel> objRolesViewModellist = new List<RolesViewModel>();
                objRolesViewModellist = GetRolesParameters(objIDataReader);
                if (objRolesViewModellist != null && objRolesViewModellist.Count > 0)
                {
                    return objRolesViewModellist[0];
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
        /// Get Roles Information by RoleID
        /// </summary>
        //public RolesViewModel GetRolesByDesireUrl(String DesireUrl)
        //{
        //    try
        //    {
        //        GetConnection();
        //        SetSQLCommandParameterWithValue("@DesireUrl", DesireUrl);
        //        IDataReader objIDataReader = GetReaderByCmd("spS_RolesByDesireUrl");
        //        List<RolesViewModel> objRolesViewModellist = new List<RolesViewModel>();
        //        objRolesViewModellist = GetRolesParameters(objIDataReader);
        //        if (objRolesViewModellist != null && objBORoleslist.Count > 0)
        //        {
        //            return objRolesViewModellist[0];
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
        /// MultiProcess Like DELETE,ACTIVE,INACTIVE for Roles
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
                    strSPName = "spUS_Roles";
                }
                else if (strMode.ToLower().Equals("multidelete"))
                {
                    strSPName = "spD_Roles";
                }
                else
                {
                    SetSQLCommandParameterWithValue("@Status", "True");
                    strSPName = "spUS_Roles";
                }
                return GetExecuteScalarByCommandInt32(strSPName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Get all values for Roles
        /// </summary>
        public List<RolesViewModel> GetRolesParameters(IDataReader objIDataReader)
        {
            if (objIDataReader != null)
            {
                objRolesViewModellist = new List<RolesViewModel>();
                using (objIDataReader)
                {
                    while (objIDataReader.Read())
                    {
                        objRolesViewModel = new RolesViewModel();
                        objRolesViewModel.RoleID = sqlhelper.GetDBInt64Value(objIDataReader["RoleID"], 0);
                        objRolesViewModel.Role = sqlhelper.GetDBStringValue(objIDataReader["Role"], string.Empty);
                        objRolesViewModellist.Add(objRolesViewModel);
                    }
                }
                return objRolesViewModellist;
            }
            else
            {
                return null;
            }
        }
        #endregion
    }
}
