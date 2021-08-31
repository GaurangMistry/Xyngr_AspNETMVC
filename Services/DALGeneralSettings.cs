using Xyngr.helper;
using System;
using System.Collections.Generic;
using System.Data;
using xyngr.Models;

namespace xyngr.Services
{
    public class DALGeneralSettings : DataAccess
    {
        #region Variable Declaration
        GeneralSettingsViewModel objGeneralSettingsViewModel;
        List<GeneralSettingsViewModel> objGeneralSettingsViewModellist;
        #endregion
        #region Helper Functions
        /// <summary>
        /// Save GeneralSettings Information
        /// </summary>
        public Int64 SaveGeneralSettings(GeneralSettingsViewModel objGeneralSettingsViewModel, string strMode)
        {
            try
            {
                GetConnection();
                if (!strMode.Equals("INSERT", StringComparison.CurrentCultureIgnoreCase))
                    SetSQLCommandParameterWithValue("@GeneralSettingID", objGeneralSettingsViewModel.GeneralSettingID);
                SetSQLCommandParameterWithValue("@PageTitle", objGeneralSettingsViewModel.PageTitle);
                SetSQLCommandParameterWithValue("@SupportEmail", objGeneralSettingsViewModel.SupportEmail);
                SetSQLCommandParameterWithValue("@SupportPhone", objGeneralSettingsViewModel.SupportPhone);
                SetSQLCommandParameterWithValue("@CopyrightText", objGeneralSettingsViewModel.CopyrightText);
                SetSQLCommandParameterWithValue("@Address", objGeneralSettingsViewModel.Address);
                SetSQLCommandParameterWithValue("@City", objGeneralSettingsViewModel.City);
                SetSQLCommandParameterWithValue("@State", objGeneralSettingsViewModel.State);
                SetSQLCommandParameterWithValue("@Country", objGeneralSettingsViewModel.Country);
                SetSQLCommandParameterWithValue("@Longitude", objGeneralSettingsViewModel.Longitude);
                SetSQLCommandParameterWithValue("@Latitude", objGeneralSettingsViewModel.Latitude);
                SetSQLCommandParameterWithValue("@GoogleAnalytic", objGeneralSettingsViewModel.GoogleAnalytic);

                SetSQLCommandParameterWithValue("@FBLink", objGeneralSettingsViewModel.FBLink);
                SetSQLCommandParameterWithValue("@GPlusLink", objGeneralSettingsViewModel.GPlusLink);
                SetSQLCommandParameterWithValue("@TwitterLink", objGeneralSettingsViewModel.TwitterLink);
                SetSQLCommandParameterWithValue("@InstagramLink", objGeneralSettingsViewModel.InstagramLink);
                SetSQLCommandParameterWithValue("@YoutubeLink", objGeneralSettingsViewModel.YoutubeLink);

                SetSQLCommandParameterWithValue("@MetaAuthorContent", objGeneralSettingsViewModel.MetaAuthorContent);
                SetSQLCommandParameterWithValue("@MetaDescContent", objGeneralSettingsViewModel.MetaDescContent);
                SetSQLCommandParameterWithValue("@MetaKeyWordContent", objGeneralSettingsViewModel.MetaKeyWordContent);
                if (strMode.Equals("INSERT", StringComparison.CurrentCultureIgnoreCase))
                {
                    return GetExecuteScalarByCommand("spI_GeneralSettings");
                }
                else
                {
                    return GetExecuteScalarByCommand("spU_GeneralSettings");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Get all GeneralSettings Information
        /// </summary>
        public List<GeneralSettingsViewModel> GetAllGeneralSettings(String SortBy, String SortOrder, String SearchField, String SearchValue)
        {
            try
            {
                GetConnection();
                SetSQLCommandParameterWithValue("@sortby", SortBy);
                SetSQLCommandParameterWithValue("@sortorder", SortOrder);
                SetSQLCommandParameterWithValue("@searchfield", SearchField);
                SetSQLCommandParameterWithValue("@searchvalue", SearchValue);
                IDataReader objIDataReader = GetReaderByCmd("spS_All_GeneralSettings");
                return GetGeneralSettingsParameters(objIDataReader);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Get all GeneralSettings Information
        /// </summary>
        public List<GeneralSettingsViewModel> GetAllGeneralSettingsForFront(String SortBy, String SortOrder, String SearchField, String SearchValue)
        {
            try
            {
                GetConnection();
                SetSQLCommandParameterWithValue("@sortby", SortBy);
                SetSQLCommandParameterWithValue("@sortorder", SortOrder);
                SetSQLCommandParameterWithValue("@searchfield", SearchField);
                SetSQLCommandParameterWithValue("@searchvalue", SearchValue);
                IDataReader objIDataReader = GetReaderByCmd("spS_All_GeneralSettingsForFront");
                return GetGeneralSettingsParameters(objIDataReader);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Get GeneralSettings Information by GeneralSettingID
        /// </summary>
        public GeneralSettingsViewModel GetGeneralSettingsByID(Int64 GeneralSettingID)
        {
            try
            {
                GetConnection();
                SetSQLCommandParameterWithValue("@GeneralSettingID", GeneralSettingID);
                IDataReader objIDataReader = GetReaderByCmd("spS_GeneralSettingsById");
                List<GeneralSettingsViewModel> objGeneralSettingsViewModellist = new List<GeneralSettingsViewModel>();
                objGeneralSettingsViewModellist = GetGeneralSettingsParameters(objIDataReader);
                if (objGeneralSettingsViewModellist != null && objGeneralSettingsViewModellist.Count > 0)
                {
                    return objGeneralSettingsViewModellist[0];
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
        /// MultiProcess Like DELETE,ACTIVE,INACTIVE for GeneralSettings
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
                    strSPName = "spUS_GeneralSettings";
                }
                else if (strMode.ToLower().Equals("multidelete"))
                {
                    strSPName = "spD_GeneralSettings";
                }
                else
                {
                    SetSQLCommandParameterWithValue("@Status", "True");
                    strSPName = "spUS_GeneralSettings";
                }
                return GetExecuteScalarByCommandInt32(strSPName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Get all values for GeneralSettings
        /// </summary>
        public List<GeneralSettingsViewModel> GetGeneralSettingsParameters(IDataReader objIDataReader)
        {
            if (objIDataReader != null)
            {
                objGeneralSettingsViewModellist = new List<GeneralSettingsViewModel>();
                using (objIDataReader)
                {
                    while (objIDataReader.Read())
                    {
                        objGeneralSettingsViewModel = new GeneralSettingsViewModel();
                        objGeneralSettingsViewModel.GeneralSettingID = sqlhelper.GetDBInt64Value(objIDataReader["GeneralSettingID"], 0);
                        objGeneralSettingsViewModel.PageTitle = sqlhelper.GetDBStringValue(objIDataReader["PageTitle"], string.Empty);
                        objGeneralSettingsViewModel.SupportEmail = sqlhelper.GetDBStringValue(objIDataReader["SupportEmail"], string.Empty);
                        objGeneralSettingsViewModel.SupportPhone = sqlhelper.GetDBStringValue(objIDataReader["SupportPhone"], string.Empty);
                        objGeneralSettingsViewModel.CopyrightText = sqlhelper.GetDBStringValue(objIDataReader["CopyrightText"], string.Empty);
                        objGeneralSettingsViewModel.Address = sqlhelper.GetDBStringValue(objIDataReader["Address"], string.Empty);
                        objGeneralSettingsViewModel.City = sqlhelper.GetDBStringValue(objIDataReader["City"], string.Empty);
                        objGeneralSettingsViewModel.State = sqlhelper.GetDBStringValue(objIDataReader["State"], string.Empty);
                        objGeneralSettingsViewModel.Country = sqlhelper.GetDBStringValue(objIDataReader["Country"], string.Empty);
                        objGeneralSettingsViewModel.Longitude = sqlhelper.GetDBStringValue(objIDataReader["Longitude"], string.Empty);
                        objGeneralSettingsViewModel.Latitude = sqlhelper.GetDBStringValue(objIDataReader["Latitude"], string.Empty);
                        objGeneralSettingsViewModel.GoogleAnalytic = sqlhelper.GetDBStringValue(objIDataReader["GoogleAnalytic"], string.Empty);
                        objGeneralSettingsViewModel.FBLink = sqlhelper.GetDBStringValue(objIDataReader["FBLink"], string.Empty);
                        objGeneralSettingsViewModel.GPlusLink = sqlhelper.GetDBStringValue(objIDataReader["GPlusLink"], string.Empty);
                        objGeneralSettingsViewModel.TwitterLink = sqlhelper.GetDBStringValue(objIDataReader["TwitterLink"], string.Empty);
                        objGeneralSettingsViewModel.InstagramLink = sqlhelper.GetDBStringValue(objIDataReader["InstagramLink"], string.Empty);
                        objGeneralSettingsViewModel.YoutubeLink = sqlhelper.GetDBStringValue(objIDataReader["YoutubeLink"], string.Empty);
                        objGeneralSettingsViewModel.MetaAuthorContent = sqlhelper.GetDBStringValue(objIDataReader["MetaAuthorContent"], string.Empty);
                        objGeneralSettingsViewModel.MetaDescContent = sqlhelper.GetDBStringValue(objIDataReader["MetaDescContent"], string.Empty);
                        objGeneralSettingsViewModel.MetaKeyWordContent = sqlhelper.GetDBStringValue(objIDataReader["MetaKeyWordContent"], string.Empty);
                        objGeneralSettingsViewModellist.Add(objGeneralSettingsViewModel);
                    }
                }
                return objGeneralSettingsViewModellist;
            }
            else
            {
                return null;
            }
        }
        #endregion
    }
}
