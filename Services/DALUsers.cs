using Xyngr.helper;
using System;
using System.Collections.Generic;
using System.Data;
using xyngr.Models;

namespace xyngr.Services
{
    public class DALUsers : DataAccess
    {
        #region Variable Declaration
        UsersViewModel objUsersViewModel;
        List<UsersViewModel> objUsersViewModellist;
        #endregion
        #region Helper Functions
        /// <summary>
        /// Save Users Information
        /// </summary>
        public Int64 SaveUsers(UsersViewModel objUsersViewModel, string strMode)
        {
            try
            {
                GetConnection();
                if (!strMode.Equals("INSERT", StringComparison.CurrentCultureIgnoreCase))
                    SetSQLCommandParameterWithValue("@UserID", objUsersViewModel.UserID);

                SetSQLCommandParameterWithValue("@RoleID", objUsersViewModel.RoleID);
                SetSQLCommandParameterWithValue("@FirstName", objUsersViewModel.FirstName);
                SetSQLCommandParameterWithValue("@LastName", objUsersViewModel.LastName);
                SetSQLCommandParameterWithValue("@Email", objUsersViewModel.Email);
                SetSQLCommandParameterWithValue("@Password", objUsersViewModel.Password);
                SetSQLCommandParameterWithValue("@Phone", objUsersViewModel.Phone);
                SetSQLCommandParameterWithValue("@Status", objUsersViewModel.Status);
                if (strMode.Equals("INSERT", StringComparison.CurrentCultureIgnoreCase))
                {
                    return GetExecuteScalarByCommand("spI_Users");
                }
                else
                {
                    return GetExecuteScalarByCommand("spU_Users");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Get all Users Information
        /// </summary>
        public List<UsersViewModel> GetAllUsers(String SortBy, String SortOrder, String SearchField, String SearchValue)
        {
            try
            {
                GetConnection();
                SetSQLCommandParameterWithValue("@sortby", SortBy);
                SetSQLCommandParameterWithValue("@sortorder", SortOrder);
                SetSQLCommandParameterWithValue("@searchfield", SearchField);
                SetSQLCommandParameterWithValue("@searchvalue", SearchValue);
                IDataReader objIDataReader = GetReaderByCmd("spS_All_Users");
                return GetUsersParameters(objIDataReader);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Get all Users Information
        /// </summary>
        public List<UsersViewModel> GetAllUsersForFront(String SortBy, String SortOrder, String SearchField, String SearchValue)
        {
            try
            {
                GetConnection();
                SetSQLCommandParameterWithValue("@sortby", SortBy);
                SetSQLCommandParameterWithValue("@sortorder", SortOrder);
                SetSQLCommandParameterWithValue("@searchfield", SearchField);
                SetSQLCommandParameterWithValue("@searchvalue", SearchValue);
                IDataReader objIDataReader = GetReaderByCmd("spS_All_UsersForFront");
                return GetUsersParameters(objIDataReader);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Get Users Information by UserID
        /// </summary>
        public UsersViewModel GetUsersByID(Int64 UserID)
        {
            try
            {
                GetConnection();
                SetSQLCommandParameterWithValue("@UserID", UserID);
                IDataReader objIDataReader = GetReaderByCmd("spS_UsersById");
                List<UsersViewModel> objUsersViewModellist = new List<UsersViewModel>();
                objUsersViewModellist = GetUsersParameters(objIDataReader);
                if (objUsersViewModellist != null && objUsersViewModellist.Count > 0)
                {
                    return objUsersViewModellist[0];
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
        
        public LoginViewModel LoginValidation(LoginViewModel model)
        {
            try
            {
                GetConnection();
                SetSQLCommandParameterWithValue("@Email", model.Email);
                SetSQLCommandParameterWithValue("@Password", model.Password);
                IDataReader objIDataReader = GetReaderByCmd("spS_UserLoginValidate");
                if (objIDataReader != null)
                {
                    var objLoginViewModel = new LoginViewModel();

                    using (objIDataReader)
                    {
                        while (objIDataReader.Read())
                        {
                            objLoginViewModel.UserID = sqlhelper.GetDBInt64Value(objIDataReader["UserID"], 0);
                            //objLoginViewModel.RoleID = sqlhelper.GetDBInt64Value(objIDataReader["RoleID"], 0);
                            objLoginViewModel.FirstName = sqlhelper.GetDBStringValue(objIDataReader["FirstName"], string.Empty);
                            objLoginViewModel.LastName = sqlhelper.GetDBStringValue(objIDataReader["LastName"], string.Empty);
                            objLoginViewModel.Email = sqlhelper.GetDBStringValue(objIDataReader["Email"], string.Empty);
                        }
                    }
                    return objLoginViewModel;
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
        /// Get Users Information by UserID
        /// </summary>
        //public UsersViewModel GetUsersByDesireUrl(String DesireUrl)
        //{
        //    try
        //    {
        //        GetConnection();
        //        SetSQLCommandParameterWithValue("@DesireUrl", DesireUrl);
        //        IDataReader objIDataReader = GetReaderByCmd("spS_UsersByDesireUrl");
        //        List<UsersViewModel> objUsersViewModellist = new List<UsersViewModel>();
        //        objUsersViewModellist = GetUsersParameters(objIDataReader);
        //        if (objUsersViewModellist != null && objBOUserslist.Count > 0)
        //        {
        //            return objUsersViewModellist[0];
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
        /// MultiProcess Like DELETE,ACTIVE,INACTIVE for Users
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
                    strSPName = "spUS_Users";
                }
                else if (strMode.ToLower().Equals("multidelete"))
                {
                    strSPName = "spD_Users";
                }
                else
                {
                    SetSQLCommandParameterWithValue("@Status", "True");
                    strSPName = "spUS_Users";
                }
                return GetExecuteScalarByCommandInt32(strSPName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Get all values for Users
        /// </summary>
        public List<UsersViewModel> GetUsersParameters(IDataReader objIDataReader)
        {
            if (objIDataReader != null)
            {
                objUsersViewModellist = new List<UsersViewModel>();
                using (objIDataReader)
                {
                    while (objIDataReader.Read())
                    {
                        objUsersViewModel = new UsersViewModel();
                        objUsersViewModel.UserID = sqlhelper.GetDBInt64Value(objIDataReader["UserID"], 0);
                        objUsersViewModel.RoleID = sqlhelper.GetDBInt64Value(objIDataReader["RoleID"], 0);
                        objUsersViewModel.FirstName = sqlhelper.GetDBStringValue(objIDataReader["FirstName"], string.Empty);
                        objUsersViewModel.LastName = sqlhelper.GetDBStringValue(objIDataReader["LastName"], string.Empty);
                        objUsersViewModel.Email = sqlhelper.GetDBStringValue(objIDataReader["Email"], string.Empty);
                        objUsersViewModel.Password = sqlhelper.GetDBStringValue(objIDataReader["Password"], string.Empty);
                        objUsersViewModel.Phone = sqlhelper.GetDBStringValue(objIDataReader["Phone"], string.Empty);
                        objUsersViewModel.CreatedDate = sqlhelper.GetDBDateTimeValue(objIDataReader["CreatedDate"]);
                        objUsersViewModel.CreatedBy = sqlhelper.GetDBInt64Value(objIDataReader["CreatedBy"], 0);
                        objUsersViewModel.ModifiedDate = sqlhelper.GetDBDateTimeValue(objIDataReader["ModifiedDate"]);
                        objUsersViewModel.ModifiedBy = sqlhelper.GetDBInt64Value(objIDataReader["ModifiedBy"], 0);
                        objUsersViewModel.Status = sqlhelper.GetDBBoolValue(objIDataReader["Status"], false);
                        objUsersViewModellist.Add(objUsersViewModel);
                    }
                }
                return objUsersViewModellist;
            }
            else
            {
                return null;
            }
        }




        #endregion
    }
}
