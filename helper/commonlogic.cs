using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;

namespace Xyngr.helper
{
    /// <summary>
    /// Common Logic Class
    /// </summary>
    public class CommonLogic
    {
        public static string GetDownloadBrowser_ServerCode = string.Empty;
        public static string strEnglishLanguage = GetConfigValue("DefaultLanguage");
        public static string strItalicLanguage = GetConfigValue("ItalicLanguage");
        public static string strCurrencyCode = string.Empty;

        public static string strLanguage
        {
            get
            {
                return GetResponseCookieValue("LanguageCode") == null ? GetConfigValue("DefaultLanguage") : GetResponseCookieValue("LanguageCode");
            }
        }

        private static string strPreviousLanguage
        {
            get
            {
                return GetCookieValue("LanguageCode") == null ? GetConfigValue("DefaultLanguage") : GetCookieValue("LanguageCode");
            }
        }

        StringBuilder strSiteMapUrl = new StringBuilder();
        string strSiteUrl = string.Empty;

        #region QUERYSTRING SUPPORTED FUNCTIONS

        public static string QueryString(string paramName)
        {
            return QueryStringCanBeDangerousContent(paramName).Trim();
        }

        public static string QueryStringCanBeDangerousContent(string paramName)
        {
            return HttpContext.Current.Request.QueryString[paramName] != null ? HttpContext.Current.Request.QueryString[paramName].ToString() : string.Empty;
        }

        #endregion

        #region SESSION SUPPORTED FUNCTIONS
        public static void SetSessionValue(string paramName, object paramValue)
        {
            HttpContext.Current.Session[paramName] = paramValue;
        }

        public static string GetSessionValue(string paramName)
        {
            if (HttpContext.Current.Session[paramName] != null)
            {
                return Convert.ToString(HttpContext.Current.Session[paramName]);
            }
            else
            {
                return string.Empty;
            }
        }

        #endregion

        #region COOKIE SUPPORTED FUNCTIONS

        public static string GetCookieValue(string paramName)
        {
            string tmpS = string.Empty;
            try
            {
                tmpS = HttpContext.Current.Request.Cookies[paramName].Value;
            }
            catch
            {
                tmpS = string.Empty;
            }
            return tmpS;
        }

        private static string GetResponseCookieValue(string paramName)
        {
            string tmpS = string.Empty;
            try
            {
                tmpS = HttpContext.Current.Response.Cookies[paramName].Value;
            }
            catch
            {
                tmpS = string.Empty;
            }
            return tmpS;
        }

        public static void SetCookies(string strLanguage)
        {
            System.Web.HttpContext.Current.Response.Cookies["LanguageCode"].Value = strLanguage;
            System.Web.HttpContext.Current.Response.Cookies["LanguageCode"].Expires = DateTime.Now.AddDays(15);
        }

        public static void ClearCookie(string paramName)
        {
            HttpCookie objCookie = HttpContext.Current.Request.Cookies[paramName];
            if (objCookie != null)
            {
                objCookie.Value = string.Empty;
                objCookie.Expires = DateTime.Now.AddDays(-2);
                System.Web.HttpContext.Current.Response.Cookies.Add(objCookie);
            }
        }

        #endregion

        #region WEB CONFIG SUPPORTED FUNCTION

        public static string GetConfigValue(string paramName)
        {
            string tmpS = string.Empty;
            try
            {
                tmpS = ConfigurationManager.AppSettings[paramName].ToString();
            }
            catch
            {
                tmpS = string.Empty;
            }
            return tmpS;

        }

        #endregion

        #region PARSING RELATED FUNCTION
        public static string ParseNativeStringValue(object value, string defaultValue)
        {
            return Convert.IsDBNull(value) ? defaultValue : Convert.ToString(value);
        }
        static public DateTime ParseNativeDateTime(string s)
        {
            try
            {
                return System.DateTime.Parse(s); // use default locale setting
            }
            catch
            {
                return Convert.ToDateTime("1/1/1900");
            }
        }
        #endregion

        #region SUPPORTED FUNCTIONS

        static public string IIF(bool condition, string a, string b)
        {
            string x = string.Empty;
            if (condition)
            {
                x = a;
            }
            else
            {
                x = b;
            }
            return x;
        }

        public static string ServerVariables(string paramName)
        {
            string tmpS = string.Empty;
            if (HttpContext.Current.Request.ServerVariables[paramName] != null)
            {
                try
                {
                    tmpS = HttpContext.Current.Request.ServerVariables[paramName].ToString();
                }
                catch
                {
                    tmpS = string.Empty;
                }
            }
            return tmpS;
        }

        static public string GetThisPageName(bool includePath)
        {
            string s = ServerVariables("SCRIPT_NAME");
            if (!includePath)
            {
                int ix = s.LastIndexOf("/");
                if (ix != -1)
                {
                    s = s.Substring(ix + 1);
                }
            }
            return s;
        }

        public static string supDate(int curr_date)
        {
            string sup = string.Empty;
            if (curr_date == 1 || curr_date == 21 || curr_date == 31)
            {
                sup = "st";
            }
            else if (curr_date == 2 || curr_date == 22)
            {
                sup = "nd";
            }
            else if (curr_date == 3 || curr_date == 23)
            {
                sup = "rd";
            }
            else
            {
                sup = "th";
            }

            return curr_date + sup;
        }
        #endregion

        #region FORMAT FUNCTIONS

        public static string DateTimeFormat(object value, string Format)
        {
            if (Convert.IsDBNull(value))
                return string.Empty;
            else
                return Convert.ToDateTime(value).ToString(Format);
        }

        public static string RemoveHtmlTag(string strDesc)
        {
            string rtnvalue;
            rtnvalue = System.Text.RegularExpressions.Regex.Replace(strDesc, "<[^>]*>", string.Empty);
            rtnvalue = rtnvalue.Replace("&lt;strong&gt;", " ").Replace("&lt;/strong&gt;", " ").Replace("&lt;b&gt;", " ").Replace("&lt;/b&gt;", " ").Replace("&lt;p&gt;", " ").Replace("&lt;/p&gt;", " ").Replace("&lt;u&gt;", " ").Replace("&lt;/u&gt;", " ").Replace("&lt;div&gt;", " ").Replace("&lt;/div&gt;", " ").Replace("&#160;", " ").Replace("&lt;br /&gt;", " ").Replace("&lt;br/&gt;", " ").Replace("&amp;rsquo;", "'");
            return rtnvalue;
        }

        public static string RemoveSpecialCharacter(string strDesc)
        {
            string rtnvalue;
            rtnvalue = System.Text.RegularExpressions.Regex.Replace(strDesc, @"[^0-9a-zA-Z]+", string.Empty);
            return rtnvalue;
        }

        public static string SetLength(string original, int MaxLength)
        {
            string strData = string.Empty;
            if (original.Length > MaxLength)
                strData = original.Substring(0, MaxLength) + "...";
            else
                strData = original;
            return strData;
        }

        #endregion

        #region Mail

        /// <summary>
        /// Sends the mail.
        /// </summary>
        /// <param name="ToMail">To mail.</param>
        /// <param name="FromMail">From mail.</param>
        /// <param name="Cc">The cc.</param>
        /// <param name="Bcc">The BCC.</param>
        /// <param name="Body">The body.</param>
        /// <param name="Subject">The subject.</param>
        /// <param name="isBodyHtml">if set to <c>true</c> [is body HTML].</param>
        public static void SendMail(string ToMail, string FromMail, string Cc, string Bcc, string Body, string Subject, bool isBodyHtml)
        {
            string SMTPHost = CommonLogic.GetApplicationValue("SMTPHost");
            string SMTPPort = CommonLogic.GetApplicationValue("SMTPPort");
            string SMTPUserName = CommonLogic.GetApplicationValue("SMTPUserName");
            string SMTPPassword = CommonLogic.GetApplicationValue("SMTPPassword");

            SmtpClient smtp = new SmtpClient();
            MailMessage mailmsg = new MailMessage();

            mailmsg.From = new MailAddress(FromMail);
            mailmsg.To.Add(ToMail);
            if (Cc != null && !Cc.Equals(string.Empty))
                mailmsg.CC.Add(Cc);
            if (Bcc != null && !Bcc.Equals(string.Empty))
                mailmsg.Bcc.Add(Bcc);

            mailmsg.Body = Body;
            mailmsg.Subject = Subject;
            mailmsg.IsBodyHtml = isBodyHtml;

            //// mailmsg.Priority = MailPriority.Low;

            ////Check SMTPUserName and SMTPPassword does not blank, it's blank then Use Default Credentials...
            if (SMTPUserName != null && SMTPPassword != null)
            {
                if(!SMTPUserName.Equals(string.Empty) && !SMTPPassword.Equals(string.Empty))
                {
                    NetworkCredential basicAuthenticationInfo = new NetworkCredential(SMTPUserName, SMTPPassword);
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = basicAuthenticationInfo;
                }
            }

            smtp.Host = SMTPHost;
            smtp.Port = Convert.ToInt32(SMTPPort);

            try
            {
                smtp.Send(mailmsg);
                mailmsg.Dispose();
            }
            catch (Exception ex)
            {
                //HttpContext.Current.Response.Write(ex.ToString());
                //CommonLogic.DisplayErrorMsg(ex.ToString());
                throw ex;
            }
        }

        /// <summary>
        /// Sends the mail new.
        /// </summary>
        /// <param name="ToMail">To mail.</param>
        /// <param name="FromMail">From mail.</param>
        /// <param name="Cc">The cc.</param>
        /// <param name="Bcc">The BCC.</param>
        /// <param name="Body">The body.</param>
        /// <param name="Subject">The subject.</param>
        /// <param name="isBodyHtml">if set to <c>true</c> [is body HTML].</param>
        public static void SendMailNew(string ToMail, string FromMail, string Cc, string Bcc, string Body, string Subject, bool isBodyHtml)
        {
            string SMTPHost = CommonLogic.GetApplicationValue("SMTPHost");
            string SMTPPort = CommonLogic.GetApplicationValue("SMTPPort");
            string SMTPUserName = "kalpesh.chaudhari@hiddenbrains.in";
            string SMTPPassword = "Kalpesh#1545";

            ////string SMTPUserName = "id.email003@gmail.com";
            ////string SMTPPassword = "HB@HB003";

            SmtpClient smtp = new SmtpClient();
            MailMessage mailmsg = new MailMessage();

            mailmsg.From = new MailAddress(FromMail);
            mailmsg.To.Add(ToMail);
            if (Cc != null && !Cc.Equals(string.Empty))
                mailmsg.CC.Add(Cc);
            if (Bcc != null && !Bcc.Equals(string.Empty))
                mailmsg.Bcc.Add(Bcc);

            mailmsg.Body = Body;
            mailmsg.Subject = Subject;
            mailmsg.IsBodyHtml = isBodyHtml;

            //// mailmsg.Priority = MailPriority.Low;

            ////Check SMTPUserName and SMTPPassword does not blank, it's blank then Use Default Credentials...
            if (SMTPUserName != null && SMTPPassword != null)
            {
                NetworkCredential basicAuthenticationInfo = new NetworkCredential(SMTPUserName, SMTPPassword);
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = basicAuthenticationInfo;
            }

            smtp.Host = "smtp.gmail.com";
            smtp.Port = 25;
            smtp.EnableSsl = true;

            try
            {
                smtp.Send(mailmsg);
                mailmsg.Dispose();
            }
            catch (Exception ex)
            {
                CommonLogic.DisplayErrorMsg(ex.ToString());
                ////throw ex;
            }
        }

        /// <summary>
        /// Sends the mail1.
        /// </summary>
        /// <param name="ToMail">To mail.</param>
        /// <param name="FromMail">From mail.</param>
        /// <param name="Cc">The cc.</param>
        /// <param name="Bcc">The BCC.</param>
        /// <param name="Body">The body.</param>
        /// <param name="Subject">The subject.</param>
        /// <param name="strHost">The STR host.</param>
        /// <param name="strPort">The STR port.</param>
        public static void SendMail1(string ToMail, string FromMail, string Cc, string Bcc, string Body, string Subject, string strHost, string strPort)
        {
            SmtpClient smtp = new SmtpClient();
            MailMessage mailmsg = new MailMessage();

            mailmsg.From = new MailAddress(FromMail);
            mailmsg.To.Add(ToMail);
            if (Cc != "")
            {
                string[] addCC = Cc.Split(',');

                for (int i = 0; i < addCC.Length; i++)
                {
                    if (!string.IsNullOrEmpty(addCC[i]))
                    {
                        mailmsg.CC.Add(addCC[i]);
                    }
                }
            }

            if (Bcc != "")
            {
                string[] addBcc = Bcc.Split(',');

                for (int i = 0; i < addBcc.Length; i++)
                {
                    if (!string.IsNullOrEmpty(addBcc[i]))
                    {
                        mailmsg.Bcc.Add(addBcc[i]);
                    }
                }
            }

            mailmsg.Body = Body;
            mailmsg.Subject = Subject;
            mailmsg.IsBodyHtml = true;

            mailmsg.Priority = MailPriority.High;

            if (!string.IsNullOrEmpty(strHost))
                smtp.Host = strHost;
            else
                smtp.Host = "smtp.gmail.com";

            if (!string.IsNullOrEmpty(strPort))
                smtp.Port = Convert.ToInt32(strPort);
            else
                smtp.Port = 25;


            //System.Net.NetworkCredential basicAuthenticationInfo = new System.Net.NetworkCredential("info@dublinconventionbureau.com", "26Estreet");
            System.Net.NetworkCredential basicAuthenticationInfo = new System.Net.NetworkCredential("p.nagasivaganga@hiddenbrains.in", "SAIpurnesh");

            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;

            smtp.UseDefaultCredentials = false;
            smtp.Credentials = basicAuthenticationInfo;

            try
            {
                smtp.Send(mailmsg);
                mailmsg.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Sends the mail with attachment.
        /// </summary>
        /// <param name="ToMail">To mail.</param>
        /// <param name="FromMail">From mail.</param>
        /// <param name="Cc">The cc.</param>
        /// <param name="Bcc">The BCC.</param>
        /// <param name="Body">The body.</param>
        /// <param name="Subject">The subject.</param>
        /// <param name="FilePath">The file path.</param>
        public static void SendMailWithAttachment(string ToMail, string FromMail, string Cc, string Bcc, string Body, string Subject, string FilePath)
        {
            SmtpClient smtp = new SmtpClient();
            MailMessage mailmsg = new MailMessage();

            mailmsg.From = new MailAddress(FromMail);
            mailmsg.To.Add(ToMail);
            if (Cc != null && !Cc.Equals(string.Empty))
                mailmsg.CC.Add(Cc);
            if (Bcc != null && !Bcc.Equals(string.Empty))
                mailmsg.Bcc.Add(Bcc);
            else
            {
                string bccAddress = GetConfigValue("TestEmailID");
                if (bccAddress != null)
                    mailmsg.Bcc.Add(bccAddress);
            }
            mailmsg.Body = Body;
            mailmsg.Subject = Subject;
            mailmsg.IsBodyHtml = true;

            ////mailmsg.Priority = MailPriority.High;

            if (File.Exists(FilePath))
            {
                FileInfo objFileInfo = new FileInfo(FilePath);
                Attachment objAttachment = new Attachment(FilePath);
                string strFileName = Subject.Replace(" ", "_");
                objAttachment.Name = strFileName + objFileInfo.Extension;
                mailmsg.Attachments.Add(objAttachment);

            }

            ////Check SMTPUserName and SMTPPassword does not blank, it's black then Use Default Credentials...
            if (GetApplicationValue("SMTPUserName").ToString() != string.Empty && GetApplicationValue("SMTPPassword").ToString() != string.Empty)
            {
                NetworkCredential basicAuthenticationInfo = new NetworkCredential(GetApplicationValue("SMTPUserName").ToString(), GetApplicationValue("SMTPPassword").ToString());
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = basicAuthenticationInfo;
            }

            smtp.Host = GetApplicationValue("SMTPHost");
            smtp.Port = Convert.ToInt32(GetApplicationValue("SMTPPort"));


            try
            {
                smtp.Send(mailmsg);
                mailmsg.Dispose();
            }
            catch (Exception)
            {
                ////throw ex;
            }
        }
        #endregion

        #region Resize Image

        public static void ResizeImage(string SourcePath, string DestPath, int Width, int Height)
        {
            Bitmap bmp = CreateThumbnail(SourcePath, Width, Height);

            if (bmp != null)
            {
                try
                {
                    bmp.Save(DestPath, System.Drawing.Imaging.ImageFormat.Jpeg);
                    if (System.IO.File.Exists(SourcePath))
                    {
                        System.IO.File.Delete(SourcePath);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    bmp.Dispose();
                }
            }
        }

        public static Bitmap CreateThumbnail(string lcFilename, int lnWidth, int lnHeight)
        {
            System.Drawing.Bitmap bmpOut = null;
            try
            {
                bool IsThumbWithRatio = true;
                Bitmap loBMP = new Bitmap(lcFilename);
                ImageFormat loFormat = loBMP.RawFormat;

                decimal lnRatio = default(decimal);
                int lnNewWidth = 0;
                int lnNewHeight = 0;

                if (IsThumbWithRatio == true)
                {
                    if (loBMP.Width > loBMP.Height)
                    {
                        lnRatio = (decimal)lnWidth / loBMP.Width;
                        lnNewWidth = lnWidth;
                        decimal lnTemp = loBMP.Height * lnRatio;
                        lnNewHeight = (int)lnTemp;
                    }
                    else
                    {
                        lnRatio = (decimal)lnHeight / loBMP.Height;
                        lnNewHeight = lnHeight;
                        decimal lnTemp = loBMP.Width * lnRatio;
                        lnNewWidth = (int)lnTemp;
                    }
                }
                else
                {
                    lnNewWidth = lnWidth;
                    lnNewHeight = lnHeight;
                }

                bmpOut = new Bitmap(lnNewWidth, lnNewHeight);
                Graphics gps = Graphics.FromImage(bmpOut);
                gps.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                gps.FillRectangle(Brushes.White, 0, 0, lnNewWidth, lnNewHeight);
                gps.DrawImage(loBMP, 0, 0, lnNewWidth, lnNewHeight);
                loBMP.Dispose();
            }
            catch (Exception)
            {
                return null;
            }
            return bmpOut;
        }

        #endregion

        #region Application Logic

        /// <summary>
        /// Check the session, if expire the redirect to given page.
        /// </summary>
        /// <param name="PageName">Page Name on which want to redirect.</param>
        public static void CheckSession(string PageName)
        {
            if (System.Web.HttpContext.Current.Session["UserId"] == null)
                System.Web.HttpContext.Current.Response.Redirect(PageName, true);
        }

        /// <summary>
        /// Check the session from pop up page, if expire reload parent page so that login page will be triggered with return url to parent page
        /// </summary>
        public static void CheckSessionFromPopUp(Page PopPage)
        {
            if (System.Web.HttpContext.Current.Session["UserId"] == null)
            {
                ScriptManager.RegisterClientScriptBlock(PopPage, PopPage.GetType(), DateTime.Now.ToString(), "window.parent.location=window.parent.location;", true);
            }
        }

        /// <summary>
        /// Check the session front from pop up page, if expire reload parent page so that login page will be triggered with return url to parent page
        /// </summary>
        public static void CheckSessionFrontFromPopUp(Page PopPage)
        {
            if (System.Web.HttpContext.Current.Session["FrontUserId"] == null)
            {

                string strReturnUrl = System.Web.HttpContext.Current.Server.UrlEncode(System.Web.HttpContext.Current.Request.Url.ToString());
                ScriptManager.RegisterClientScriptBlock(PopPage, PopPage.GetType(), DateTime.Now.ToString(), string.Format("window.parent.location='{0}'", GetLoginUrlForFront()), true);
            }
        }

        public static void CheckSessionFrontFromPopUp(Page PopPage, string returnURL)
        {
            if (System.Web.HttpContext.Current.Session["FrontUserId"] == null)
            {
                string strReturnUrl = returnURL; // System.Web.HttpContext.Current.Server.UrlEncode(returnURL);
                ScriptManager.RegisterClientScriptBlock(PopPage, PopPage.GetType(), DateTime.Now.ToString(), string.Format("window.parent.location='{0}?", GetLoginUrlForFront()) + "returnurl=" + strReturnUrl + "'", true);
            }
        }

        /// <summary>
        /// Check the session, if expire then redirect to login page.
        /// </summary>
        public static void CheckSession()
        {
            if (System.Web.HttpContext.Current.Session["UserID"] == null)
            {
                string strReturnUrl = System.Web.HttpContext.Current.Server.UrlEncode(System.Web.HttpContext.Current.Request.Url.ToString());
                System.Web.HttpContext.Current.Response.Redirect("/?returnurl=" + strReturnUrl);
                //System.Web.HttpContext.Current.Response.Redirect("/Entra");
            }
        }

        /// <summary>
        /// Check the session, if expire then redirect to login page.
        /// </summary>
        public static void CheckPlanType(PlanType plantype)
        {
            if (!System.Web.HttpContext.Current.Session["PlanType"].Equals(plantype))
            {
                string strReturnUrl = System.Web.HttpContext.Current.Server.UrlEncode(System.Web.HttpContext.Current.Request.Url.ToString());
                System.Web.HttpContext.Current.Response.Redirect("/login.aspx?returnurl=" + strReturnUrl);
            }
        }
        /// <summary>
        /// Checks the admin session.
        /// </summary>
        public static void CheckAdminSession()
        {
            if (System.Web.HttpContext.Current.Session["AdminUserId"] == null)
            {
                string strReturnUrl = System.Web.HttpContext.Current.Server.UrlEncode(System.Web.HttpContext.Current.Request.Url.ToString());
                System.Web.HttpContext.Current.Response.Redirect("login.aspx?returnurl=" + strReturnUrl);
            }
        }

        /// <summary>
        /// Check if user is logged or not (except for Admin User).
        /// </summary>

        public static void IsUserLogged()
        {
            string strReturnUrl = System.Web.HttpContext.Current.Server.UrlEncode(System.Web.HttpContext.Current.Request.Url.ToString());
            if (System.Web.HttpContext.Current.Session["UserID"] != null && System.Web.HttpContext.Current.Session["UserRole"] != null)
            {

                if (!System.Web.HttpContext.Current.Session["UserRole"].ToString().Equals("1"))
                {
                    System.Web.HttpContext.Current.Response.Redirect("/Entra?returnurl=" + strReturnUrl);
                }
                    //System.Web.HttpContext.Current.Response.Redirect("/Entra");

                //System.Web.HttpContext.Current.Response.Redirect("/");
            }
            else
            {
                //System.Web.HttpContext.Current.Response.Redirect("/Entra");
                System.Web.HttpContext.Current.Response.Redirect("/Entra?returnurl=" + strReturnUrl);
            }
        }

        /// <summary>
        /// Check the session for front, if expire the redirect to given page.
        /// </summary>
        /// <param name="PageName">Page Name on which want to redirect.</param>
        public static void CheckSessionForFront(string PageName)
        {
            if (System.Web.HttpContext.Current.Session["FrontUserId"] == null)
            {
                System.Web.HttpContext.Current.Response.Redirect(PageName, true);
            }
        }

        /// <summary>
        /// Check the session for front, if expire then redirect to login page.
        /// </summary>
        public static void CheckSessionForFront()
        {
            if (System.Web.HttpContext.Current.Session["FrontUserId"] == null)
            {
                string strReturnUrl = System.Web.HttpContext.Current.Server.UrlEncode(System.Web.HttpContext.Current.Request.RawUrl.Replace("/", "@@@"));
                System.Web.HttpContext.Current.Response.Redirect(getLoginRequiredUrlForFront(strReturnUrl));
            }

            if (Convert.ToString(System.Web.HttpContext.Current.Session["FrontRoleName"]).Equals("4") && (HttpContext.Current.Request.Url.AbsoluteUri.Contains("addproperty") || HttpContext.Current.Request.Url.AbsoluteUri.Contains("ownerpropertydetails")))
            {
                System.Web.HttpContext.Current.Response.Redirect(CommonLogic.getOwnerProfileUrl(), false);
            }
        }

        /// <summary>
        /// Check logged user is in specified role or not.
        /// </summary>

        public static bool IsLoggedUserInRole(userrole userRoleType)
        {
            if (System.Web.HttpContext.Current.Session["FrontUserId"] != null && System.Web.HttpContext.Current.Session["FrontRoleName"] != null)
            {
                if (Convert.ToInt32(System.Web.HttpContext.Current.Session["FrontRoleName"]) == ((int)userRoleType))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// get logged UserID for front-end.
        /// </summary>
        public static string getLoggedUserIdForFront()
        {
            return System.Web.HttpContext.Current.Session["FrontUserId"].ToString();
        }

        /// <summary>
        /// get logged User RoleId for front-end.
        /// </summary>

        public static string getLoggedUserRoleIDForFront()
        {
            return System.Web.HttpContext.Current.Session["FrontRoleName"].ToString();
        }


        /// <summary>
        /// Sends the mail on error.
        /// </summary>
        /// <param name="ex">The ex.</param>
        public static void SendMailOnError(Exception ex)
        {
            try
            {
                StringBuilder strMessage = new StringBuilder();
                strMessage.Append("<table style='border: 1px solid #cecece;'>");
                strMessage.Append("<tr><td style='backgroundcolor:orange;height:10px;'></td></tr>");
                strMessage.Append("<tr><td>Target Site Name      :" + ParseNativeStringValue(ex.TargetSite.Name, "No Information") + "</td></tr>");
                strMessage.Append("<tr><td>Error Message         :" + ParseNativeStringValue(ex.Message, "No Information") + "</td></tr>");
                strMessage.Append("<tr><td>Error Source          :" + ParseNativeStringValue(ex.Source, "No Information") + "</td></tr>");
                strMessage.Append("<tr><td>Error Source          :" + ParseNativeStringValue(ex.StackTrace, "No Information") + "</td></tr>");
                strMessage.Append("</br></br>");
                strMessage.Append("<tr><td style='backgroundcolor:orange;height:5px;'></td></tr>");
                strMessage.Append("</table>");
                ////SendMail(string.Empty, string.Empty, string.Empty, string.Empty, strMessage.ToString(), "An exception occurred.");
            }
            catch
            {
            }
        }

        /// <summary>
        /// Sets the active inactive.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="status">The status.</param>
        /// <returns></returns>
        public static string SetActiveInactive(string id, string status)
        {
            if (status.Equals("active", StringComparison.CurrentCultureIgnoreCase))
            {
                //return string.Format("<img src='../../propertymanager/admin/images/icon-active.gif' alt='Active' style='cursor:pointer' id='img_{0}{1}' title='Click here to inactivate' name='imgbtnInActive' class='SetStatusClick'  onclick='return SetActiveInactive({1},true);' />", status, id);
                return "<span class='label label-block label-success'>Active</span>";
            }
            else if (status == "inactive")
            {
                //return string.Format("<img src='../../propertymanager/admin/images/icon-inactive.gif' alt='Inactive' style='cursor:pointer' id='img_{0}{1}' title='Click here to activate' name='imgbtnActive' class='SetStatusClick'  onclick='return SetActiveInactive({1},true);' />", status, id);
                return "<span class='label label-block label-red'>Inactive</span>";
            }
            else
            {
                //return string.Format("<img src='../../propertymanager/admin/images/icon-delete.gif' alt='Delete' style='cursor:pointer' id='img_{0}{1}' title='Click here to delete' name='imgbtnDelete' class='SetStatusClick'  onclick='return SetActiveInactive({1},true);' />", status, id);
                return string.Empty;
            }
        }

        public static string CheckForApproval(string status)
        {
            if (status.Equals("approve", StringComparison.CurrentCultureIgnoreCase))
            {
                return "<span class='label label-block label-important'>Approved</span>";
            }
            else if (status.Equals("denied", StringComparison.CurrentCultureIgnoreCase))
            {
                return "<span class='label label-block label-default'>Denied</span>";
            }
            else
            {
                return "<span class='label label-block label-default'>Panding</span>";
            }
        }

       
        private static string GetFormattedUrl(string ConfigURL, object DesiredUrl)
        {
            string strDesiredUrl = string.Empty;
            if (DesiredUrl != null)
                strDesiredUrl = string.Format(GetConfigValue(ConfigURL), DesiredUrl);
            return strDesiredUrl;
        }

        public static string getPropertyDetailUrl(object DesiredUrl)
        {
            return GetFormattedUrl("PropertyDetailURL", DesiredUrl);
        }

        public static string GetPropertyDetailTabSpecificURL(string DesiredUrl, string tab)
        {
            return string.Format(GetConfigValue("PropertyDetailTabSpecificURL"), DesiredUrl, tab);
        }

        /// <summary>
        /// Gets the property detail search specific URL.
        /// </summary>
        /// <param name="DesiredUrl">The desired URL.</param>
        /// <param name="Arrivaldate">The arrivaldate.</param>
        /// <param name="DepartureDate">The departure date.</param>
        /// <returns></returns>
        public static string GetPropertyDetailSearchSpecificURL(object DesiredUrl, string Arrivaldate, string DepartureDate)
        {
            return string.Format(GetConfigValue("PropertyDetailSearchSpecificURL"), DesiredUrl, Arrivaldate, DepartureDate);
        }

        /// <summary>
        /// Gets the property detail with persons search specific URL.
        /// </summary>
        /// <param name="DesiredUrl">The desired URL.</param>
        /// <param name="Arrivaldate">The arrivaldate.</param>
        /// <param name="DepartureDate">The departure date.</param>
        /// <param name="NoOfPersons">The no of persons.</param>
        /// <returns></returns>
        public static string GetPropertyDetailWithPersonsSearchSpecificURL(object DesiredUrl, string Arrivaldate, string DepartureDate, string NoOfPersons)
        {
            return string.Format(GetConfigValue("PropertyDetailWithPersonsSearchSpecificURL"), DesiredUrl, Arrivaldate, DepartureDate, NoOfPersons);
        }

        public static string getNewsDetailUrl(object DesiredUrl)
        {
            return GetFormattedUrl("NewsDetailURL", DesiredUrl);
        }

        /// <summary>
        /// Gets the affiliate news detail URL.
        /// </summary>
        /// <param name="DesiredUrl">The desired URL.</param>
        /// <returns>Return string</returns>
        public static string GetAffiliateNewsDetailUrl(object DesiredUrl, string UserUrl)
        {
            return string.Format(GetConfigValue("AFPNewsDetailURL"), UserUrl, DesiredUrl);
        }

        /// <summary>
        /// Gets the affiliate feedback URL.
        /// </summary>
        /// <param name="UserUrl">The user URL.</param>
        /// <returns></returns>
        public static string GetAffiliateFeedbackUrl(string UserUrl)
        {
            return string.Format(GetConfigValue("AFPFeedbackURL"), UserUrl);
        }

        /// <summary>
        /// Gets the affiliate CMS page URL.
        /// </summary>
        /// <param name="DesiredUrl">The desired URL.</param>
        /// <param name="UserUrl">The user URL.</param>
        /// <returns></returns>
        public static string GetAffiliateCMSPageUrl(string UserUrl, object DesiredUrl)
        {
            return string.Format(GetConfigValue("AFPCMSPageDetailURL"), UserUrl, DesiredUrl);
        }

        public static string getTestimonialDetailUrl(object DesiredUrl)
        {
            return GetFormattedUrl("TestimonialDetailURL", DesiredUrl);
        }

        /// <summary>
        /// Gets the login required URL for front.
        /// </summary>
        /// <param name="DesiredUrl">The desired URL.</param>
        /// <returns></returns>
        private static string getLoginRequiredUrlForFront(object DesiredUrl)
        {
            return GetFormattedUrl("LoginRequiredURL", DesiredUrl);
        }

        /// <summary>
        /// Gets the login URL for front.
        /// </summary>
        /// <returns></returns>
        public static string GetLoginUrlForFront()
        {
            return GetConfigValue("LoginURL");
        }

        /// <summary>
        /// Gets the book now reservation URL.
        /// </summary>
        /// <param name="propertyId">The property id.</param>
        /// <param name="checkIn">The check in.</param>
        /// <param name="checkOut">The check out.</param>
        /// <returns></returns>
        public static string GetBookNowReservationURL(long propertyId, string checkIn, string checkOut)
        {
            return string.Format(GetConfigValue("BookNowReservationURL"), propertyId, checkIn, checkOut);
        }

        /// <summary>
        /// Gets the book now with person reservation URL.
        /// </summary>
        /// <param name="propertyId">The property id.</param>
        /// <param name="checkIn">The check in.</param>
        /// <param name="checkOut">The check out.</param>
        /// <param name="days">The days.</param>        
        /// <returns></returns>
        public static string GetBookNowWithPersonReservationURL(long propertyId, string checkIn, string checkOut, string people)
        {
            return string.Format(GetConfigValue("BookNowWithPersonReservationURL"), propertyId, checkIn, checkOut, people);
        }

        /// <summary>
        /// Gets the book now URL.
        /// </summary>
        /// <param name="propertyId">The property id.</param>
        /// <param name="checkIn">The check in.</param>
        /// <param name="checkOut">The check out.</param>
        /// <param name="days">The days.</param>
        /// <param name="price">The price.</param>
        /// <returns></returns>
        public static string GetBookNowURL(string reservationID)
        {
            ////return string.Format(GetConfigValue("BookNowURL"), propertyId, checkIn, checkOut, days, price, reservationID);
            return string.Format(GetConfigValue("BookNowURL"), reservationID);
        }

        /// <summary>
        /// Gets the book now with person URL.
        /// </summary>
        /// <param name="propertyId">The property id.</param>
        /// <param name="checkIn">The check in.</param>
        /// <param name="checkOut">The check out.</param>
        /// <param name="days">The days.</param>
        /// <param name="price">The price.</param>
        /// <param name="people">The people.</param>
        /// <returns></returns>
        public static string GetBookNowWithPersonURL(string people, string reservationID)
        {
            return string.Format(GetConfigValue("BookNowWithPersonURL"), people, reservationID);
            ////return string.Format(GetConfigValue("BookNowWithPersonURL"), propertyId, checkIn, checkOut, days, price, people, reservationID);
        }

        /// <summary>
        /// Gets the confirmation tril period URL.
        /// </summary>
        /// <returns></returns>
        public static string GetConfirmationTrilPeriodURL()
        {
            return GetConfigValue("GetConfirmationTrilPeriodURL");
        }

        public static string getSendNewsLetterUrl(object DesiredUrl)
        {
            return GetFormattedUrl("TestimonialDetailURL", DesiredUrl);
        }

        public static string GetHomeUrlForFront()
        {
            return GetConfigValue("HomeURL");
        }

        public static string getHomeUrlForAffiliate()
        {
            return GetFormattedUrl("AffiliateDomainURL", QueryString("domainurl"));
        }

        public static string getHomeUrlForAffiliateAdmin()
        {
            return GetConfigValue("AffiliateAdminDomainURL");
        }

        public static string getAffiliatePropertiesUrl()
        {
            return GetFormattedUrl("AFPPropertiesURL", QueryString("domainurl"));
        }

        public static string getAffiliateAboutUsUrl()
        {
            return GetFormattedUrl("AFPAboutUsURL", QueryString("domainurl"));
        }
        public static string getAffiliateContactUSUrl()
        {
            return GetFormattedUrl("AFPContactUsURL", QueryString("domainurl"));
        }

        public static string GetAffiliateLoginUrl()
        {
            return GetConfigValue("AAPLoginURL");
        }

        /// <summary>
        /// Gets the master senoir login URL.
        /// </summary>
        /// <returns>Return String</returns>
        public static string GetMasterSenoirLoginUrl()
        {
            return GetConfigValue("AAPMasterSeniorLoginURL");
        }

        /// <summary>
        /// Gets the search property URL for PM front.
        /// </summary>
        /// <returns></returns>
        public static string GetSearchPropertyUrlForPMFront()
        {
            return GetConfigValue("PMAffiliateSearchPropertyURL");
        }

        /// <summary>
        /// Gets the property list URL for PM front.
        /// </summary>
        /// <returns></returns>
        public static string GetPropertyListUrlForPMFront()
        {
            return GetConfigValue("PMAffiliatePropertyListURL");
        }

        public static string GetHomeWithoutSearchUrlForPMFront()
        {
            return GetConfigValue("PMAffiliateHomeWthSURL");
        }

        private static string getAffiliateLoginRequiredUrl(string returnURL)
        {
            return string.Format(GetConfigValue("AAPLoginRequiredURL"), returnURL);
        }

        /// <summary>
        /// Gets the affiliate admin home URL.
        /// </summary>

        public static string getAffiliateAdminHomeUrl()
        {
            if (CommonLogic.IsSubAffiliate())
            {
                return GetConfigValue("APPManagerMiniSiteHome");
            }
            else
            {
                return GetConfigValue("APPManagerHome");
            }
        }

        /// <summary>
        /// Gets the affiliate property owners list URL.
        /// </summary>

        public static string getAffiliaterPropertyOwnersListUrl()
        {
            if (CommonLogic.IsSubAffiliate())
            {
                return GetConfigValue("APPManagerMiniSitePropertyOwnersListURL");
            }
            else
            {
                return GetConfigValue("APPManagerPropertyOwnersListURL");
            }
        }

        /// <summary>
        /// Gets the affiliate list answer URL.
        /// </summary>

        public static string getAffiliaterListAnswerUrl()
        {
            if (CommonLogic.IsSubAffiliate())
            {
                return GetConfigValue("AAPMiniSiteListAnswerURL");
            }
            else
            {
                return GetConfigValue("AAPListAnswerURL");
            }
        }

        /// <summary>
        /// Determines whether [is sub affiliate].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [is sub affiliate]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsSubAffiliate()
        {
            string strReturnUrl = System.Web.HttpContext.Current.Server.UrlEncode(System.Web.HttpContext.Current.Request.RawUrl.Replace("/", "@@@"));
            if (strReturnUrl.Contains("minisite") && (System.Web.HttpContext.Current.Session["MiniSiteID"] != null))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static string getAffiliateMiniSiteAdminHomeUrl()
        {
            return GetConfigValue("APPManagerMiniSiteHome");
        }

        /// <summary>
        /// Gets the affiliate admin property list URL.
        /// </summary>
        public static string getAffiliateAdminPropertyListURL()
        {
            if (CommonLogic.IsSubAffiliate())
            {
                return GetConfigValue("APPMinisitePropertyListUrl");
            }
            else
            {
                return GetConfigValue("APPPropertyListUrl");
            }
        }

        /// <summary>
        /// Gets the affiliate published property list URL.
        /// </summary>
        /// <returns></returns>
        public static string getAffiliatePublishedPropertyListURL()
        {
            if (CommonLogic.IsSubAffiliate())
            {
                return GetConfigValue("APPMinisitePublishedPropertyListUrl");
            }
            else
            {
                return GetConfigValue("APPPublishedPropertyListUrl");
            }
        }

        /// <summary>
        /// Gets the affiliate banner list URL.
        /// </summary>

        public static string getAffiliateBannerListURL()
        {
            if (CommonLogic.IsSubAffiliate())
            {
                return GetConfigValue("APPMiniSiteBannerListUrl");
            }
            else
            {
                return GetConfigValue("APPBannerListUrl");
            }
        }

        /// <summary>
        /// Gets the affiliate banner client list URL.
        /// </summary>

        public static string getAffiliateBannerClientListURL()
        {
            if (CommonLogic.IsSubAffiliate())
            {
                return GetConfigValue("APPMiniSiteBannerClientListUrl");
            }
            else
            {
                return GetConfigValue("APPBannerClientListUrl");
            }
        }

        /// <summary>
        /// Gets the affiliate add banner URL.
        /// </summary>

        public static string getAffiliateAddBannerURL()
        {
            if (CommonLogic.IsSubAffiliate())
            {
                return GetConfigValue("APPMiniSiteAddBanner");
            }
            else
            {
                return GetConfigValue("APPAddBanner");
            }
        }

        /// <summary>
        /// Gets the affiliate admin add property URL.
        /// </summary>

        public static string getAffiliateAdminAddPropertyURL()
        {
            if (CommonLogic.IsSubAffiliate())
            {
                return GetConfigValue("APPManagerMiniSiteAddPropertyURL");
            }
            else
            {
                return GetConfigValue("APPManagerAddPropertyURL");
            }
        }

        /// <summary>
        /// Gets the affiliate owner property list URL.
        /// </summary>

        public static string getAffiliateOwnerPropertyListURL()
        {
            if (CommonLogic.IsSubAffiliate())
            {
                return GetConfigValue("APPManagerMiniSiteOwnerPropertyListUrl");
            }
            else
            {
                return GetConfigValue("APPManagerOwnerPropertyListUrl");
            }
        }

        /// <summary>
        /// Gets the affiliate property question URL.
        /// </summary>

        public static string getAffiliatePropertyQuestionURL()
        {
            if (CommonLogic.IsSubAffiliate())
            {
                return GetConfigValue("AAPMiniSitePropertyQuestionsURL");
            }
            else
            {
                return GetConfigValue("AAPPropertyQuestionsURL");
            }
        }

        /// <summary>
        /// Gets the affiliate add auction property URL.
        /// </summary>

        public static string getAffiliateAddAuctionPropertyURL()
        {
            if (CommonLogic.IsSubAffiliate())
            {
                return GetConfigValue("APPMiniSiteManagerAddAuctionPropertyUrl");
            }
            else
            {
                return GetConfigValue("APPManagerAddAuctionPropertyUrl");
            }
        }

        /// <summary>
        /// Gets the affiliate add property availability URL.
        /// </summary>
        public static string getAffiliateAddPropertyAvailabilityURL()
        {
            if (CommonLogic.IsSubAffiliate())
            {
                return GetConfigValue("APPMiniSiteManagerAddPropertyAvailabilityURL");
            }
            else
            {
                return GetConfigValue("APPManagerAddPropertyAvailabilityURL");
            }
        }

        /// <summary>
        /// Gets the affiliate add property cost URL.
        /// </summary>
        /// <returns></returns>
        public static string getAffiliateAddPropertyCostURL()
        {
            if (CommonLogic.IsSubAffiliate())
            {
                return GetConfigValue("APPMiniSiteManagerAddPropertyCostURL");
            }
            else
            {
                return GetConfigValue("APPManagerAddPropertyCostURL");
            }
        }

        /// <summary>
        /// Gets the affiliate add property discount URL.
        /// </summary>

        public static string getAffiliateAddPropertyDiscountURL()
        {
            if (CommonLogic.IsSubAffiliate())
            {
                return GetConfigValue("APPMiniSiteManagerAddPropertyDiscountURL");
            }
            else
            {
                return GetConfigValue("APPManagerAddPropertyDiscountURL");
            }
        }

        /// <summary>
        /// Gets the affiliate auction list by date URL.
        /// </summary>

        public static string getAffiliateAuctionListByDateURL()
        {
            if (CommonLogic.IsSubAffiliate())
            {
                return GetConfigValue("APPMiniSiteManagerAuctionListByDateURL");
            }
            else
            {
                return GetConfigValue("APPManagerAuctionListByDateURL");
            }
        }

        /// <summary>
        /// Gets the affiliate edit E mail template URL.
        /// </summary>

        public static string getAffiliateEditEMailTemplateURL()
        {
            if (CommonLogic.IsSubAffiliate())
            {
                return GetConfigValue("APPMiniSiteManagerEditEMailTemplate");
            }
            else
            {
                return GetConfigValue("APPManagerEditEMailTemplate");
            }
        }

        /// <summary>
        /// Gets the affiliate add banner client URL.
        /// </summary>

        public static string getAffiliateAddBannerClientURL()
        {
            if (CommonLogic.IsSubAffiliate())
            {
                return GetConfigValue("APPMiniSiteAddBannerClient");
            }
            else
            {
                return GetConfigValue("APPAddBannerClient");
            }
        }

        /// <summary>
        /// Gets the affiliate property winner list URL.
        /// </summary>

        public static string getAffiliatePropertyWinnerListURL()
        {
            if (CommonLogic.IsSubAffiliate())
            {
                return GetConfigValue("APPManagerMyMiniSitePropertyWinnerListURL");
            }
            else
            {
                return GetConfigValue("APPManagerMyPropertyWinnerListURL");
            }
        }

        /// <summary>
        /// Gets the affiliate property feedback URL.
        /// </summary>

        public static string getAffiliatePropertyFeedbackURL()
        {
            if (CommonLogic.IsSubAffiliate())
            {
                return GetConfigValue("APMiniSitePropertyFeedback");
            }
            else
            {
                return GetConfigValue("APPropertyFeedback");
            }
        }

        /// <summary>
        /// Gets the affiliate my guests list URL.
        /// </summary>

        public static string getAffiliateMyGuestsListURL()
        {
            if (CommonLogic.IsSubAffiliate())
            {
                return GetConfigValue("APPManagerMiniSiteMyGuestsList");
            }
            else
            {
                return GetConfigValue("APPManagerMyGuestsList");
            }
        }

        /// <summary>
        /// Gets the affiliate service list URL.
        /// </summary>

        public static string getAffiliateServiceListURL()
        {
            if (CommonLogic.IsSubAffiliate())
            {
                return GetConfigValue("APPManagerMiniSiteServiceList");
            }
            else
            {
                return GetConfigValue("APPManagerServiceList");
            }
        }

        /// <summary>
        /// Gets the affiliate property owner questions URL.
        /// </summary>

        public static string getAffiliatePropertyOwnerQuestionsURL()
        {
            if (CommonLogic.IsSubAffiliate())
            {
                return GetConfigValue("AAPMinisitePropertyOwnerQuestionsURL");
            }
            else
            {
                return GetConfigValue("AAPPropertyOwnerQuestionsURL");
            }
        }

        /// <summary>
        /// Gets the affiliate my owner property winners URL.
        /// </summary>

        public static string getAffiliateMyOwnerPropertyWinnersURL()
        {
            if (CommonLogic.IsSubAffiliate())
            {
                return GetConfigValue("APPMiniSiteManagerMyOwnerPropertyWinners");
            }
            else
            {
                return GetConfigValue("APPManagerMyOwnerPropertyWinners");
            }
        }

        /// <summary>
        /// APs the mini site property owner feedback.
        /// </summary>

        public static string getAffiliatePropertyOwnerFeedbackURL()
        {
            if (CommonLogic.IsSubAffiliate())
            {
                return GetConfigValue("APMiniSitePropertyOwnerFeedback");
            }
            else
            {
                return GetConfigValue("APPropertyOwnerFeedback");
            }
        }

        /// <summary>
        /// Gets the affiliate my reservation URL.
        /// </summary>

        public static string getAffiliateMyReservationURL()
        {
            if (CommonLogic.IsSubAffiliate())
            {
                return GetConfigValue("APPMiniSiteManagerMyReservation");
            }
            else
            {
                return GetConfigValue("APPManagerMyReservation");
            }
        }

        /// <summary>
        /// Gets the affiliate my owner reservation URL.
        /// </summary>

        public static string getAffiliateMyOwnerReservationURL()
        {
            if (CommonLogic.IsSubAffiliate())
            {
                return GetConfigValue("APPMiniSiteManagerMyOwnerReservation");
            }
            else
            {
                return GetConfigValue("APPManagerMyOwnerReservation");
            }
        }

        /// <summary>
        /// Gets the affiliate my property requests URL.
        /// </summary>

        public static string getAffiliaterMyPropertyRequestsURL()
        {
            if (CommonLogic.IsSubAffiliate())
            {
                return GetConfigValue("APPMiniSiteManagerMyPropertyRequests");
            }
            else
            {
                return GetConfigValue("APPManagerMyPropertyRequests");
            }
        }

        /// <summary>
        /// Gets the affiliate my owner property requests URL.
        /// </summary>

        public static string getAffiliaterMyOwnerPropertyRequestsURL()
        {
            if (CommonLogic.IsSubAffiliate())
            {
                return GetConfigValue("APPMiniSiteManagerMyOwnerPropertyRequests");
            }
            else
            {
                return GetConfigValue("APPManagerMyOwnerPropertyRequests");
            }
        }

        /// <summary>
        /// Gets the affiliate my ads received URL.
        /// </summary>
        /// <returns>Return string</returns>
        public static string getAffiliaterMyAdsReceivedURL()
        {
            if (CommonLogic.IsSubAffiliate())
            {
                return GetConfigValue("APPMiniSiteManagerMyAdsReceived");
            }
            else
            {
                return GetConfigValue("APPManagerMyAdsReceived");
            }
        }

        /// <summary>
        /// Gets the affiliate my owner ads received URL.
        /// </summary>
        /// <returns>Return string</returns>
        public static string getAffiliaterMyOwnerAdsReceivedURL()
        {
            if (CommonLogic.IsSubAffiliate())
            {
                return GetConfigValue("APPMiniSiteManagerMyOwnerAdsReceived");
            }
            else
            {
                return GetConfigValue("APPManagerMyOwnerAdsReceived");
            }
        }

        /// <summary>
        /// Gets the affiliate my property bid list URL.
        /// </summary>
        /// <returns>Return string</returns>
        public static string getAffiliateMyPropertyBidListURL()
        {
            if (CommonLogic.IsSubAffiliate())
            {
                return GetConfigValue("APPMiniSiteManagerMyPropertyBidList");
            }
            else
            {
                return GetConfigValue("APPManagerMyPropertyBidList");
            }
        }

        /// <summary>
        /// Gets the affiliate my owner property bid list URL.
        /// </summary>
        /// <returns>Return string</returns>
        public static string getAffiliateMyOwnerPropertyBidListURL()
        {
            if (CommonLogic.IsSubAffiliate())
            {
                return GetConfigValue("APPMiniSiteManagerMyOwnerPropertyBidList");
            }
            else
            {
                return GetConfigValue("APPManagerMyOwnerPropertyBidList");
            }
        }

        /// <summary>
        /// Gets the affiliate add property details URL.
        /// </summary>
        /// <returns>Return string</returns>
        public static string getAffiliateAddPropertyDetailsURL()
        {
            if (CommonLogic.IsSubAffiliate())
            {
                return GetConfigValue("APPMiniSiteManagerAddPropertyDetails");
            }
            else
            {
                return GetConfigValue("APPManagerAddPropertyDetails");
            }
        }

        /// <summary>
        /// Gets the affiliate add gallery details URL.
        /// </summary>
        /// <returns></returns>
        public static string GetAffiliateAddgalleryDetailsURL()
        {
            if (CommonLogic.IsSubAffiliate())
            {
                return GetConfigValue("APPMiniSiteAddAffiliateGalleryDetails");
            }
            else
            {
                return GetConfigValue("APPManagerAddAffiliateGalleryDetails");
            }
        }

        /// <summary>
        /// Gets the affiliate gallery URL.
        /// </summary>
        /// <returns></returns>
        public static string GetAffiliateGalleryURL()
        {
            if (CommonLogic.IsSubAffiliate())
            {
                return GetConfigValue("APPMiniSiteAffiliateGallery");
            }
            else
            {
                return GetConfigValue("APPManagerAffiliateGallery");
            }
        }

        /// <summary>
        /// Gets the affiliate my options URL.
        /// </summary>
        /// <returns>Return string</returns>
        public static string getAffiliateMyOptionsURL()
        {
            if (CommonLogic.IsSubAffiliate())
            {
                return GetConfigValue("APPMiniSiteManagerMyOptions");
            }
            else
            {
                return GetConfigValue("APPManagerMyOptions");
            }
        }

        /// <summary>
        /// Gets the affiliate my owner options URL.
        /// </summary>
        /// <returns>Return string</returns>
        public static string getAffiliateMyOwnerOptionsURL()
        {
            if (CommonLogic.IsSubAffiliate())
            {
                return GetConfigValue("APPMiniSiteManagerMyOwnerOptions");
            }
            else
            {
                return GetConfigValue("APPManagerMyOwnerOptions");
            }
        }

        /// <summary>
        /// Gets the affiliate credit plan URL.
        /// </summary>
        /// <returns>Return string</returns>
        public static string getAffiliateCreditPlanURL()
        {
            if (CommonLogic.IsSubAffiliate())
            {
                return GetConfigValue("APPMiniSiteManagerCreditPlan");
            }
            else
            {
                return GetConfigValue("APPManagerCreditPlan");
            }
        }

        /// <summary>
        /// Gets the affiliate credit history URL.
        /// </summary>
        /// <returns>Return string</returns>
        public static string getAffiliateCreditHistoryURL()
        {
            if (CommonLogic.IsSubAffiliate())
            {
                return GetConfigValue("APPMiniSiteManagerCreditHistory");
            }
            else
            {
                return GetConfigValue("APPManagerCreditHistory");
            }
        }

        /// <summary>
        /// Gets the affiliate money generate URL.
        /// </summary>
        /// <returns>Return string</returns>
        public static string getAffiliateMoneyGenerateURL()
        {
            if (CommonLogic.IsSubAffiliate())
            {
                return GetConfigValue("APPMiniSiteManagerMoneyGenerate");
            }
            else
            {
                return GetConfigValue("APPManagerMoneyGenerate");
            }
        }

        /// <summary>
        /// Gets the affiliate E mail template URL.
        /// </summary>
        /// <returns>Return string</returns>
        public static string getAffiliateEMailTemplateURL()
        {
            if (CommonLogic.IsSubAffiliate())
            {
                return GetConfigValue("APPMiniSiteManagerEMailTemplate");
            }
            else
            {
                return GetConfigValue("APPManagerEMailTemplate");
            }
        }

        /// <summary>
        /// Gets the affiliate newsletter list URL.
        /// </summary>
        /// <returns>Return string</returns>
        public static string getAffiliateNewsletterListURL()
        {
            if (CommonLogic.IsSubAffiliate())
            {
                return GetConfigValue("APPMiniSiteManagerListNewsletter");
            }
            else
            {
                return GetConfigValue("APPManagerListNewsletter");
            }
        }

        /// <summary>
        /// Gets the affiliate news list URL.
        /// </summary>
        /// <returns></returns>
        public static string getAffiliateNewsListURL()
        {
            if (CommonLogic.IsSubAffiliate())
            {
                return GetConfigValue("APPMiniSiteManagerListNews");
            }
            else
            {
                return GetConfigValue("APPManagerListNews");
            }
        }

        /// <summary>
        /// Gets the affiliate won property list URL.
        /// </summary>
        /// <returns>Return string</returns>
        public static string getAffiliateWonPropertyListURL()
        {
            if (CommonLogic.IsSubAffiliate())
            {
                return GetConfigValue("APPMiniSiteManagerListWonProperty");
            }
            else
            {
                return GetConfigValue("APPManagerListWonProperty");
            }
        }

        /// <summary>
        /// Gets the affiliate property event list URL.
        /// </summary>
        /// <returns></returns>
        public static string getAffiliatePropertyEventListURL()
        {
            if (CommonLogic.IsSubAffiliate())
            {
                return GetConfigValue("APPMiniSiteManagerListPropertyEvents");
            }
            else
            {
                return GetConfigValue("APPManagerListPropertyEvents");
            }
        }

        /// <summary>
        /// Gets the affiliate bank details URL.
        /// </summary>
        /// <returns></returns>
        public static string getAffiliateBankDetailsURL()
        {
            return GetConfigValue("APPManagerBankDetails");
        }

        /// <summary>
        /// Gets the affiliate banner list by mode URL.
        /// </summary>

        public static string getAffiliateBannerListByModeURL()
        {
            if (CommonLogic.IsSubAffiliate())
            {
                return GetConfigValue("APPMiniSiteBannerListByMode");
            }
            else
            {
                return GetConfigValue("APPBannerListByMode");
            }
        }

        /// <summary>
        /// Gets the affiliate banner client list by mode URL.
        /// </summary>
        public static string getAffiliateBannerClientListByModeURL()
        {
            if (CommonLogic.IsSubAffiliate())
            {
                return GetConfigValue("APPMiniSiteBannerClientListByMode");
            }
            else
            {
                return GetConfigValue("APPBannerClientListByMode");
            }
        }

        /// <summary>
        /// Gets the affiliate admin owner property list URL.
        /// </summary>
        public static string getAffiliateAdminOwnerPropertyListURL()
        {
            if (CommonLogic.IsSubAffiliate())
            {
                return GetConfigValue("APPMiniSitePropertyRequestDetailUrl");
            }
            else
            {
                return GetConfigValue("APPPropertyRequestDetailUrl");
            }
        }

        /// <summary>
        /// Gets the affiliate property bid list URL.
        /// </summary>
        /// <returns></returns>
        public static string getAffiliatePropertyBidListURL()
        {
            if (CommonLogic.IsSubAffiliate())
            {
                return GetConfigValue("APPMiniSitePropertyBidDetailUrl");
            }
            else
            {
                return GetConfigValue("APPPropertyBidDetailUrl");
            }
        }

        /// <summary>
        /// Gets the affiliate admin manage profile URL.
        /// </summary>
        public static string getAffiliateAdminManageProfileURL()
        {
            if (CommonLogic.IsSubAffiliate())
            {
                return GetConfigValue("APPManageMiniSiteProfile");
            }
            else
            {
                return GetConfigValue("APPManageProfile");
            }
        }

        /// <summary>
        /// Gets the affiliate change password URL.
        /// </summary>
        public static string getAffiliateChangePasswordURL()
        {
            if (CommonLogic.IsSubAffiliate())
            {
                return GetConfigValue("APPMiniSiteChangePassword");
            }
            else
            {
                return GetConfigValue("APPChangePassword");
            }
        }

        /// <summary>
        /// Gets the affiliate edit newsletter URL.
        /// </summary>
        /// <returns>Return String</returns>
        public static string getAffiliateEditNewsletterURL()
        {
            if (CommonLogic.IsSubAffiliate())
            {
                return GetConfigValue("APPMiniSiteManagerEditNewsletter");
            }
            else
            {
                return GetConfigValue("APPManagerEditNewsletter");
            }
        }

        /// <summary>
        /// Gets the affiliate edit news URL.
        /// </summary>
        /// <returns>Return String</returns>
        public static string getAffiliateEditNewsURL()
        {
            if (CommonLogic.IsSubAffiliate())
            {
                return GetConfigValue("APPMiniSiteManagerEditNews");
            }
            else
            {
                return GetConfigValue("APPManagerEditNews");
            }
        }

        /// <summary>
        /// Gets the affiliate add newsletter URL.
        /// </summary>

        public static string getAffiliateAddNewsletterURL()
        {
            if (CommonLogic.IsSubAffiliate())
            {
                return GetConfigValue("APPMiniSiteManagerAddNewsletter");
            }
            else
            {
                return GetConfigValue("APPManagerAddNewsletter");
            }
        }

        /// <summary>
        /// Gets the affiliate add news URL.
        /// </summary>
        /// <returns>Return String</returns>
        public static string getAffiliateAddNewsURL()
        {
            if (CommonLogic.IsSubAffiliate())
            {
                return GetConfigValue("APPMiniSiteManagerAddNews");
            }
            else
            {
                return GetConfigValue("APPManagerAddNews");
            }
        }

        /// <summary>
        /// Gets the affiliate property details URL.
        /// </summary>

        public static string getAffiliatePropertyDetailsURL()
        {
            if (CommonLogic.IsSubAffiliate())
            {
                return GetConfigValue("APPMiniSiteManagerPropertyDetailsURL");
            }
            else
            {
                return GetConfigValue("APPManagerPropertyDetailsURL");
            }
        }

        /// <summary>
        /// Gets the affiliate property search flexible URL.
        /// </summary>
        public static string getAffiliatePropertySearchFlexibleURL()
        {
            if (CommonLogic.IsSubAffiliate())
            {
                return GetConfigValue("APPMiniSiteManagerPropertySearchFlexible");
            }
            else
            {
                return GetConfigValue("APPManagerPropertySearchFlexible");
            }
        }

        /// <summary>
        /// Gets the affiliate property search with flexible URL.
        /// </summary>
        /// <returns></returns>
        public static string getAffiliatePropertySearchWithFlexibleURL()
        {
            if (CommonLogic.IsSubAffiliate())
            {
                return GetConfigValue("APPMiniSiteManagerPropertySearchWithFlexible");
            }
            else
            {
                return GetConfigValue("APPManagerPropertySearchWithFlexible");
            }
        }

        /// <summary>
        /// Gets the affiliate property search URL.
        /// </summary>
        public static string getAffiliatePropertySearchURL()
        {
            if (CommonLogic.IsSubAffiliate())
            {
                return GetConfigValue("APPMiniSiteManagerPropertySearch");
            }
            else
            {
                return GetConfigValue("APPManagerPropertySearch");
            }
        }

        /// <summary>
        /// Gets the affiliate property search WO date URL.
        /// </summary>
        /// <returns></returns>
        public static string getAffiliatePropertySearchWODateURL()
        {
            if (CommonLogic.IsSubAffiliate())
            {
                return GetConfigValue("APPMiniSiteManagerPropertySearchWODate");
            }
            else
            {
                return GetConfigValue("APPManagerPropertySearchWODate");
            }
        }

        /// <summary>
        /// Gets the affiliate edit property URL.
        /// </summary>

        public static string getAffiliateEditPropertyURL()
        {
            if (CommonLogic.IsSubAffiliate())
            {
                return GetConfigValue("APPMiniSiteManagerEditProperty");
            }
            else
            {
                return GetConfigValue("APPManagerEditProperty");
            }
        }

        /// <summary>
        /// Gets the affiliate guest coming leaving URL.
        /// </summary>

        public static string getAffiliateGuestComingLeavingURL()
        {
            if (CommonLogic.IsSubAffiliate())
            {
                return GetConfigValue("APPMiniSiteManagerGuestComingLeaving");
            }
            else
            {
                return GetConfigValue("APPManagerGuestComingLeaving");
            }
        }

        /// <summary>
        /// Gets the affiliate property list by mode URL.
        /// </summary>

        public static string getAffiliatePropertyListByModeUrl()
        {
            if (CommonLogic.IsSubAffiliate())
            {
                return GetConfigValue("APPMiniSitePropertyListByModeUrl");
            }
            else
            {
                return GetConfigValue("APPPropertyListByModeUrl");
            }
        }

        /// <summary>
        /// Gets the affiliate property bid list URL.
        /// </summary>

        public static string getAffiliatePropertyBidListUrl()
        {
            if (CommonLogic.IsSubAffiliate())
            {
                return GetConfigValue("APPMiniSiteManagerPropertyBidListURL");
            }
            else
            {
                return GetConfigValue("APPManagerPropertyBidListURL");
            }
        }

        /// <summary>
        /// Gets the affiliate add service URL.
        /// </summary>

        public static string getAffiliateAddServiceUrl()
        {
            if (CommonLogic.IsSubAffiliate())
            {
                return GetConfigValue("APPMiniSiteManagerAddService");
            }
            else
            {
                return GetConfigValue("APPManagerAddService");
            }
        }

        /// <summary>
        /// Gets the affiliate add guest URL.
        /// </summary>
        public static string getAffiliateAddGuestUrl()
        {
            if (CommonLogic.IsSubAffiliate())
            {
                return GetConfigValue("APPMiniSiteManagerAddGuestUrl");
            }
            else
            {
                return GetConfigValue("APPManagerAddGuestUrl");
            }
        }


        /// <summary>
        /// Gets the affiliate add feedback URL.
        /// </summary>
        /// <returns></returns>
        public static string getAffiliateAddFeedbackUrl()
        {
            if (CommonLogic.IsSubAffiliate())
            {
                return GetConfigValue("APPMiniSiteManagerAddFeedbackUrl");
            }
            else
            {
                return GetConfigValue("APPManagerAddFeedbackUrl");
            }
        }

        /// <summary>
        /// Gets the affiliate add static page URL.
        /// </summary>
        /// <returns>Return String</returns>
        public static string GetAffiliateAddStaticPageUrl()
        {
            if (CommonLogic.IsSubAffiliate())
            {
                return GetConfigValue("APPMiniSiteManagerAddStaticPageUrl");
            }
            else
            {
                return GetConfigValue("APPManagerAddStaticPageUrl");
            }
        }

        /// <summary>
        /// Gets the affiliate list static page URL.
        /// </summary>
        /// <returns>Return String</returns>
        public static string GetAffiliateListStaticPageUrl()
        {
            if (CommonLogic.IsSubAffiliate())
            {
                return GetConfigValue("APPMiniSiteManagerStaticPageUrl");
            }
            else
            {
                return GetConfigValue("APPManagerStaticPageUrl");
            }
        }

        /// <summary>
        /// Gets the affiliate ads received detail URL.
        /// </summary>

        public static string getAffiliateAdsReceivedDetailUrl()
        {
            if (CommonLogic.IsSubAffiliate())
            {
                return GetConfigValue("APPMiniSiteManagerAdsReceivedDetail");
            }
            else
            {
                return GetConfigValue("APPManagerAdsReceivedDetail");
            }
        }

        /// <summary>
        /// Gets the affiliate edit reservation URL.
        /// </summary>
        public static string getAffiliateEditReservationUrl()
        {
            if (CommonLogic.IsSubAffiliate())
            {
                return GetConfigValue("APPMiniSiteManagerEditReservationUrl");
            }
            else
            {
                return GetConfigValue("APPManagerEditReservationUrl");
            }
        }

        /// <summary>
        /// Gets the edit owner edit reservation URL.
        /// </summary>
        /// <returns></returns>
        public static string getEditOwnerEditReservationURL()
        {
            return GetConfigValue("OwnerEditReservation");
        }

        /// <summary>
        /// Gets the reservation details URL.
        /// </summary>
        /// <returns></returns>
        public static string getReservationDetailsURL()
        {
            return GetConfigValue("OwnerReservationDetails");
        }

        /// <summary>
        /// Gets the add owner add reservation option URL.
        /// </summary>
        /// <returns></returns>
        public static string getAddOwnerAddReservationOptionURL()
        {
            return GetConfigValue("OwnerAddReservationOption");
        }

        /// <summary>
        /// Gets the affiliate add reservation option URL.
        /// </summary>
        /// <returns></returns>
        public static string getAffiliateAddReservationOptionUrl()
        {
            if (CommonLogic.IsSubAffiliate())
            {
                return GetConfigValue("APPMiniSiteManagerAddReservationOptionUrl");
            }
            else
            {
                return GetConfigValue("APPManagerAddReservationOptionUrl");
            }
        }

        /// <summary>
        /// Gets the affiliate add reservation option with dates URL.
        /// </summary>
        /// <returns></returns>
        public static string getAffiliateAddReservationOptionWithDatesUrl()
        {
            if (CommonLogic.IsSubAffiliate())
            {
                return GetConfigValue("APPMiniSiteManagerAddReservationOptionWithDateUrl");
            }
            else
            {
                return GetConfigValue("APPManagerAddReservationOptionWithDateUrl");
            }
        }

        /// <summary>
        /// Gets the affiliate list bid URL.
        /// </summary>

        public static string getAffiliateListBidUrl()
        {
            if (CommonLogic.IsSubAffiliate())
            {
                return GetConfigValue("AAPMiniSiteListBidURL");
            }
            else
            {
                return GetConfigValue("AAPListBidURL");
            }
        }

        /// <summary>
        /// Gets the affiliate edit property URL.
        /// </summary>

        public static string getAffiliateEditPropertyUrl()
        {
            if (CommonLogic.IsSubAffiliate())
            {
                return GetConfigValue("AAPMiniSiteOwnerEditProperty");
            }
            else
            {
                return GetConfigValue("AAPOwnerEditProperty");
            }
        }

        /// <summary>
        /// Gets the affiliate owner property details URL.
        /// </summary>

        public static string getAffiliateOwnerPropertyDetailsUrl()
        {
            if (CommonLogic.IsSubAffiliate())
            {
                return GetConfigValue("AAPMiniSiteOwnerPropertyDetailsURL");
            }
            else
            {
                return GetConfigValue("AAPOwnerPropertyDetailsURL");
            }
        }

        /// <summary>
        /// Gets the affiliate owner details URL.
        /// </summary>

        public static string getAffiliateOwnerDetailsUrl()
        {
            if (CommonLogic.IsSubAffiliate())
            {
                return GetConfigValue("APPMiniSiteManagerOwnerDetailsURL");
            }
            else
            {
                return GetConfigValue("APPManagerOwnerDetailsURL");
            }
        }

        public static string getAffiliateMinisiteAdminManageProfileURL()
        {
            return GetConfigValue("APPManageMiniSiteProfile");
        }

        public static string getPurchaseCreditsURL()
        {
            return GetConfigValue("PurchaseCreditsURL");
        }

        public static string getRegisterURL()
        {
            return GetConfigValue("RegisterURL");
        }

        /// <summary>
        /// Gets the pre post requirement URL.
        /// </summary>
        /// <returns></returns>
        public static string getPrePostRequirementUrl()
        {
            return GetConfigValue("PrePostRequirementURL");
        }

        /// <summary>
        /// Gets the post requirement URL.
        /// </summary>
        /// <returns></returns>
        public static string getPostRequirementUrl()
        {
            return GetConfigValue("PostRequirementURL");
        }

        /// <summary>
        /// Gets the post Owner Welcome URL.
        /// </summary>
        /// <returns></returns>
        public static string getOwnerWelcomeUrl()
        {
            return GetConfigValue("OwnerWelcome");
        }
        
        public static string getOwnerProfileUrl()
        {
            return GetConfigValue("OwnerProfile");
        }
        /// <summary>
        /// return formatted Property Location used by PropertyList and Watch list
        /// </summary>
        //public static string getLocation(object city, object district, object state, object country)
        //{
        //    StringBuilder sbLocation = new StringBuilder();
        //    if (!city.ToString().IsNullOrEmpty())
        //        sbLocation.AppendFormat(" {0},", city);
        //    if (!district.ToString().IsNullOrEmpty())
        //        sbLocation.AppendFormat(" {0},", district);
        //    if (!state.ToString().IsNullOrEmpty())
        //        sbLocation.AppendFormat(" {0}, ", state);
        //    if (!country.ToString().IsNullOrEmpty())
        //        sbLocation.AppendFormat("{0}.", country);
        //    return sbLocation.ToString();
        //}

        /// <summary>
        /// return rating visibility if property HAS rating then true or else false
        /// </summary>
        //public static bool getRatingVisibility(object PropertyRating)
        //{
        //    bool isvisible = false;
        //    if (!PropertyRating.ToString().IsNullOrEmpty() && !PropertyRating.ToString().Equals(-1))
        //    {
        //        isvisible = true;
        //    }
        //    return isvisible;
        //}

        public static string Encode(string text)
        {
            byte[] mybyte = System.Text.Encoding.UTF8.GetBytes(text);
            string returntext = System.Convert.ToBase64String(mybyte);
            return returntext;
        }

        public static string Decode(string text)
        {
            byte[] mybyte = System.Convert.FromBase64String(text);
            string returntext = System.Text.Encoding.UTF8.GetString(mybyte);
            return returntext;
        }


        /// <summary>
        /// Encrypts a string
        /// </summary>
        /// <param name="PlainText">string to be encrypted</param>
        /// <returns>An encrypted string</returns>
        public static string Encrypt(string PlainText)
        {
            string Password = "Zoomhouse", Salt = "Login", HashAlgorithm = "SHA1", InitialVector = "OFRna73m*aze01xY";
            int PasswordIterations = 2, KeySize = 256;

            if (PlainText == null)
                return string.Empty;

            byte[] InitialVectorBytes = Encoding.ASCII.GetBytes(InitialVector);
            byte[] SaltValueBytes = Encoding.ASCII.GetBytes(Salt);
            byte[] PlainTextBytes = Encoding.UTF8.GetBytes(PlainText);
            PasswordDeriveBytes DerivedPassword = new PasswordDeriveBytes(Password, SaltValueBytes, HashAlgorithm, PasswordIterations);
            byte[] KeyBytes = DerivedPassword.GetBytes(KeySize / 8);
            RijndaelManaged SymmetricKey = new RijndaelManaged();
            SymmetricKey.Mode = CipherMode.CBC;
            byte[] CipherTextBytes = null;

            using (ICryptoTransform Encryptor = SymmetricKey.CreateEncryptor(KeyBytes, InitialVectorBytes))
            {
                using (MemoryStream MemStream = new MemoryStream())
                {
                    using (CryptoStream CryptoStream = new CryptoStream(MemStream, Encryptor, CryptoStreamMode.Write))
                    {
                        CryptoStream.Write(PlainTextBytes, 0, PlainTextBytes.Length);
                        CryptoStream.FlushFinalBlock();
                        CipherTextBytes = MemStream.ToArray();
                        MemStream.Close();
                        CryptoStream.Close();
                    }
                }
            }
            SymmetricKey.Clear();
            return Convert.ToBase64String(CipherTextBytes);
        }

        /// <summary>
        /// Decrypts a string
        /// </summary>
        /// <param name="CipherText">string to be Decrypted</param>
        /// <returns>A decrypted string</returns>
        public static string Decrypt(string CipherText)
        {
            string Password = "Zoomhouse", Salt = "Login", HashAlgorithm = "SHA1", InitialVector = "OFRna73m*aze01xY";
            int PasswordIterations = 2, KeySize = 256;

            if (CipherText == null)
                return string.Empty;
            byte[] InitialVectorBytes = Encoding.ASCII.GetBytes(InitialVector);
            byte[] SaltValueBytes = Encoding.ASCII.GetBytes(Salt);
            byte[] CipherTextBytes = Convert.FromBase64String(CipherText);
            PasswordDeriveBytes DerivedPassword = new PasswordDeriveBytes(Password, SaltValueBytes, HashAlgorithm, PasswordIterations);
            byte[] KeyBytes = DerivedPassword.GetBytes(KeySize / 8);
            RijndaelManaged SymmetricKey = new RijndaelManaged();
            SymmetricKey.Mode = CipherMode.CBC;
            byte[] PlainTextBytes = new byte[CipherTextBytes.Length];
            int ByteCount = 0;
            using (ICryptoTransform Decryptor = SymmetricKey.CreateDecryptor(KeyBytes, InitialVectorBytes))
            {
                using (MemoryStream MemStream = new MemoryStream(CipherTextBytes))
                {
                    using (CryptoStream CryptoStream = new CryptoStream(MemStream, Decryptor, CryptoStreamMode.Read))
                    {
                        ByteCount = CryptoStream.Read(PlainTextBytes, 0, PlainTextBytes.Length);
                        MemStream.Close();
                        CryptoStream.Close();
                    }
                }
            }
            SymmetricKey.Clear();
            return Encoding.UTF8.GetString(PlainTextBytes, 0, ByteCount);
        }

        #region Encryption and Decryption

        /// <summary>
        /// Encrypts the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="key">The key.</param>
        /// <returns>encrypted data</returns>
        public static string Encryptdata(string message, string key)
        {
            byte[] results;
            System.Text.UTF8Encoding utf8 = new System.Text.UTF8Encoding();
            MD5CryptoServiceProvider hashProvider = new MD5CryptoServiceProvider();
            byte[] tdesKey = hashProvider.ComputeHash(utf8.GetBytes(key));
            TripleDESCryptoServiceProvider tdesAlgorithm = new TripleDESCryptoServiceProvider();
            tdesAlgorithm.Key = tdesKey;
            tdesAlgorithm.Mode = CipherMode.ECB;
            tdesAlgorithm.Padding = PaddingMode.PKCS7;
            byte[] dataToEncrypt = utf8.GetBytes(message);
            try
            {
                ICryptoTransform encryptor = tdesAlgorithm.CreateEncryptor();
                results = encryptor.TransformFinalBlock(dataToEncrypt, 0, dataToEncrypt.Length);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                tdesAlgorithm.Clear();
                hashProvider.Clear();
            }

            return Convert.ToBase64String(results);
        }

        /// <summary>
        /// Decrypts the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="key">The key.</param>
        /// <returns>return decrypted data</returns>
        public static string Decryptdata(string message, string key)
        {
            byte[] results;
            System.Text.UTF8Encoding utf8 = new UTF8Encoding();
            MD5CryptoServiceProvider hashProvider = new MD5CryptoServiceProvider();
            byte[] tdesKey = hashProvider.ComputeHash(utf8.GetBytes(key));
            TripleDESCryptoServiceProvider tdesAlgorithm = new TripleDESCryptoServiceProvider();
            tdesAlgorithm.Key = tdesKey;
            tdesAlgorithm.Mode = CipherMode.ECB;
            tdesAlgorithm.Padding = PaddingMode.PKCS7;
            byte[] dataToDecrypt = Convert.FromBase64String(message.Replace(" ", "+"));
            try
            {
                ICryptoTransform decryptor = tdesAlgorithm.CreateDecryptor();
                results = decryptor.TransformFinalBlock(dataToDecrypt, 0, dataToDecrypt.Length);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                tdesAlgorithm.Clear();
                hashProvider.Clear();
            }

            return utf8.GetString(results);
        }

        #endregion

        //public static string getCurrencyWithPrice(object Price)
        //{
        //    string strCurrency = string.Empty;
        //    if (!Price.ToString().IsNullOrEmpty())
        //    {
        //        if (!Price.ToString().ParseNativeDouble().Equals(0))
        //            strCurrency = Price.ToString().ParseNativeDouble().ToString("C");
        //        else
        //            strCurrency = "-";
        //    }
        //    return strCurrency;
        //}

        //public static string getStaticPageUrl(staticpagecodes StaticPageCode)
        //{
        //    string pageURL = "#";
        //    string staticPageCacheKey = string.Concat(StaticPageCode.ToString(), "_url");
        //    try
        //    {

        //        // Cache the Current Static Page's desiredurl, reset cache after every 30 minutes
        //        if (HttpContext.Current.Cache[staticPageCacheKey] == null)
        //        {
        //            BOStaticPageMaster obj_bo_staticpagemaster = BALStaticPageMaster.GetStaticPageMasterByCode(StaticPageCode.ToString());
        //            if (obj_bo_staticpagemaster != null)
        //            {
        //                HttpContext.Current.Cache.Insert(staticPageCacheKey, string.Format(GetConfigValue("CMSURL"), obj_bo_staticpagemaster.DesiredUrl), null, DateTime.UtcNow.AddMinutes(30), Cache.NoSlidingExpiration);
        //                pageURL = HttpContext.Current.Cache[staticPageCacheKey].ToString();
        //            }
        //        }
        //        else
        //        {
        //            pageURL = HttpContext.Current.Cache[staticPageCacheKey].ToString();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        CommonLogic.DisplayErrorMsg(ex.ToString());
        //    }
        //    return pageURL;
        //}

        //public static string getStaticPagePopupUrl(staticpagecodes StaticPageCode)
        //{
        //    string pageURL = getStaticPageUrl(StaticPageCode);
        //    string FullPageURL = CommonLogic.GetConfigValue("CMSURL").Replace("{0}", string.Empty);
        //    string PopupURL = CommonLogic.GetConfigValue("CMSPopupURL").Replace("{0}", string.Empty);
        //    return pageURL.Replace(FullPageURL, PopupURL);
        //}

        //public static void GenerateFlashXML(string FilePath)
        //{
        //    DataSet dsFlashXML = BALStatistics.GetFlashXML(getLoggedAffiliateId());
        //    if (dsFlashXML != null && dsFlashXML.Tables.Count > 0)
        //    {
        //        Page objPage = HttpContext.Current.Handler as Page;
        //        HttpRequest _request = HttpContext.Current.Request;
        //        System.Globalization.CultureInfo culture = System.Globalization.CultureInfo.CreateSpecificCulture(strEnglishLanguage);

        //        StringBuilder strXML = new StringBuilder();
        //        var dtFlashXML = dsFlashXML.Tables[0];
        //        string _preContinentID = string.Empty;
        //        string _preCountryID = string.Empty;
        //        string _preStateID = string.Empty;

        //        string _uri = string.Empty;
        //        if (_request.Url.Port == 80)
        //            _uri = string.Concat("http://", _request.Url.Host);
        //        else
        //            _uri = string.Format("http://{0}:{1}", _request.Url.Host, _request.Url.Port);

        //        string _noImage = string.Concat(_uri, "/images/no-img-58x54.gif");
        //        string _countryImageUrl = CommonLogic.GetConfigValue("CountryImageUploadRootURL");
        //        string _stateImageUrl = CommonLogic.GetConfigValue("StateImageUploadRootURL");
        //        string _propertyImageUrl = CommonLogic.GetConfigValue("PropertyImageUploadRootURL");

        //        string _countryDesiredUrl = string.Concat(_uri, CommonLogic.GetConfigValue("CountryPropertyURL"));
        //        string _stateDesiredUrl = string.Concat(_uri, CommonLogic.GetConfigValue("PopularLocationURL"));

        //        strXML.AppendLine("<?xml version='1.0' encoding='utf-8' ?>");
        //        strXML.AppendLine("<Continents xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xmlns:xsd='http://www.w3.org/2001/XMLSchema\'>");

        //        for (int i = 0; i < dtFlashXML.Rows.Count; i++)
        //        {
        //            ////End Country Details
        //            if (i != 0 && _preCountryID != dtFlashXML.Rows[i]["CountryID"].ToString())
        //            {
        //                strXML.AppendLine("</States>");
        //                strXML.AppendLine("</Country>");
        //            }

        //            ////End Continent Details
        //            if (i != 0 && _preContinentID != dtFlashXML.Rows[i]["ContinentID"].ToString())
        //            {
        //                strXML.AppendLine("</Countries>");
        //                strXML.AppendLine("</Continent>");
        //            }

        //            // Write Continent if does not exist in xml string
        //            if (_preContinentID != dtFlashXML.Rows[i]["ContinentID"].ToString())
        //            {
        //                strXML.AppendLine("<Continent>");
        //                strXML.AppendFormat("<ContinentID>{0}</ContinentID>", dtFlashXML.Rows[i]["ContinentID"]);
        //                strXML.AppendFormat("<ContinentName>{0}</ContinentName>", dtFlashXML.Rows[i]["Continent"]);
        //                strXML.AppendLine("<Countries>");
        //            }

        //            // Write Country Details if does not exist in xml string
        //            if (_preCountryID != dtFlashXML.Rows[i]["CountryID"].ToString())
        //            {
        //                string CountryImage = _noImage;
        //                string cimage = dtFlashXML.Rows[i]["CountryImage"].ToString();
        //                // Get Saved Country Coordinates
        //                Coordinate objCoordinate = new Coordinate(dtFlashXML.Rows[i]["CountryLatitude"].ToString().ParseNativeDouble(), dtFlashXML.Rows[i]["CountryLongitude"].ToString().ParseNativeDouble());

        //                if (!cimage.IsNullOrEmpty() && File.Exists(objPage.Server.MapPath(Path.Combine(_countryImageUrl, cimage))))
        //                    CountryImage = string.Concat(_uri, _countryImageUrl, "/", cimage);

        //                strXML.AppendLine("<Country>");
        //                strXML.AppendFormat("<CountryID>{0}</CountryID>", dtFlashXML.Rows[i]["CountryID"]);
        //                strXML.AppendFormat("<CountryName>{0}</CountryName>", dtFlashXML.Rows[i]["Country"]);
        //                strXML.AppendFormat("<CountryImage>{0}</CountryImage>", CountryImage);
        //                strXML.AppendFormat("<CountryUrl>{0}</CountryUrl>", string.Format(_countryDesiredUrl, dtFlashXML.Rows[i]["CountryDesiredUrl"]));

        //                if (objCoordinate.Latitude == 0 || objCoordinate.Longitude == 0)
        //                {
        //                    // If Coordinates are empty i.e Zero
        //                    // Lookup Geo Coordinates
        //                    objCoordinate = Geocode.GetCoordinates(dtFlashXML.Rows[i]["Country"].ToString());
        //                    if (objCoordinate.Latitude == 0 || objCoordinate.Longitude == 0)
        //                    {
        //                        // Get Geo Coordinates by Country Name and State Name
        //                        objCoordinate = Geocode.GetCoordinates(string.Concat(dtFlashXML.Rows[i]["Country"], ", ", dtFlashXML.Rows[i]["Country"]));
        //                    }
        //                    // Save Country Coordinates
        //                    BALCountryMaster.SaveCountryGeoLocation(dtFlashXML.Rows[i]["CountryID"].ToString(), objCoordinate);
        //                }

        //                strXML.AppendFormat("<CountryLatitude>{0}</CountryLatitude>", objCoordinate.Latitude.ToString(culture));
        //                strXML.AppendFormat("<CountryLongitude>{0}</CountryLongitude>", objCoordinate.Longitude.ToString(culture));
        //                strXML.AppendLine("<States>");
        //            }

        //            // Write State Details if does not exist in xml string
        //            if (_preStateID != dtFlashXML.Rows[i]["StateID"].ToString())
        //            {
        //                string StateImage = _noImage;
        //                string simage = dtFlashXML.Rows[i]["StateImage"].ToString();
        //                // Get Saved State Coordinates
        //                Coordinate objCoordinate = new Coordinate(dtFlashXML.Rows[i]["StateLatitude"].ToString().ParseNativeDouble(), dtFlashXML.Rows[i]["StateLongitude"].ToString().ParseNativeDouble());

        //                if (!simage.IsNullOrEmpty() && File.Exists(objPage.Server.MapPath(Path.Combine(_stateImageUrl, simage))))
        //                    StateImage = string.Concat(_uri, _stateImageUrl, "/", simage);

        //                strXML.AppendLine("<State>");
        //                strXML.AppendFormat("<StateID>{0}</StateID>", dtFlashXML.Rows[i]["StateID"]);
        //                strXML.AppendFormat("<StateName>{0}</StateName>", dtFlashXML.Rows[i]["State"]);
        //                strXML.AppendFormat("<StateImage>{0}</StateImage>", StateImage);
        //                strXML.AppendFormat("<StateUrl>{0}</StateUrl>", string.Format(_stateDesiredUrl, dtFlashXML.Rows[i]["StateDesiredUrl"]));

        //                // If Coordinates are empty i.e Zero
        //                // Lookup Geo Coordinates
        //                if (objCoordinate.Latitude == 0 || objCoordinate.Longitude == 0)
        //                {
        //                    // Get Geo Coordinates by Country Name and State Name
        //                    objCoordinate = Geocode.GetCoordinates(string.Concat(dtFlashXML.Rows[i]["Country"], ", ", dtFlashXML.Rows[i]["Country"]));
        //                    // Save Country Coordinates
        //                    BALStateMaster.SaveStateGeoLocation(dtFlashXML.Rows[i]["StateID"].ToString(), objCoordinate);
        //                }
        //                strXML.AppendFormat("<StateLatitude>{0}</StateLatitude>", objCoordinate.Latitude.ToString(culture));
        //                strXML.AppendFormat("<StateLongitude>{0}</StateLongitude>", objCoordinate.Longitude.ToString(culture));

        //                strXML.AppendFormat("<PropertyCount>{0}</PropertyCount>", dtFlashXML.Rows[i]["PropertyCount"]);
        //                strXML.AppendLine("<Properties>");

        //                List<BOPropertyMaster> obj_bo_propertymasterlist = new List<BOPropertyMaster>();
        //                obj_bo_propertymasterlist = BALPropertyMaster.GetAllPropertiesForMap(dtFlashXML.Rows[i]["StateID"].ToString(), getLoggedAffiliateId());

        //                if (obj_bo_propertymasterlist != null && obj_bo_propertymasterlist.Count > 0)
        //                {
        //                    foreach (BOPropertyMaster obj_bo_propertymaster in obj_bo_propertymasterlist)
        //                    {
        //                        string PropertyImage = _noImage;
        //                        string pimage = obj_bo_propertymaster.DefaultImage.Image.Replace("Image_", "Thumb_");

        //                        strXML.AppendLine("<Property>");
        //                        strXML.AppendFormat("<PropertyID>{0}</PropertyID>", obj_bo_propertymaster.PropertyID);
        //                        strXML.AppendFormat("<PropertyName>{0}</PropertyName>", obj_bo_propertymaster.PropertyName);

        //                        if (!pimage.IsNullOrEmpty() && File.Exists(objPage.Server.MapPath(Path.Combine(_propertyImageUrl, pimage))))
        //                            PropertyImage = string.Concat(_uri, _propertyImageUrl, "/", pimage);

        //                        strXML.AppendFormat("<PropertyImage>{0}</PropertyImage>", PropertyImage);
        //                        strXML.AppendFormat("<PropertyUrl>{0}</PropertyUrl>", string.Concat(_uri, CommonLogic.getPropertyDetailUrl(obj_bo_propertymaster.DesiredUrl)));
        //                        strXML.AppendFormat("<PropertyLatitude>{0}</PropertyLatitude>", obj_bo_propertymaster.Latitude);
        //                        strXML.AppendFormat("<PropertyLongitude>{0}</PropertyLongitude>", obj_bo_propertymaster.Longitude);
        //                        strXML.AppendFormat("<PropertyArea>{0}</PropertyArea>", obj_bo_propertymaster.Area);
        //                        strXML.AppendLine("</Property>");
        //                    }
        //                }
        //                strXML.AppendLine("</Properties>");
        //                strXML.AppendLine("</State>");
        //            }

        //            _preStateID = dtFlashXML.Rows[i]["StateID"].ToString();
        //            _preCountryID = dtFlashXML.Rows[i]["CountryID"].ToString();
        //            _preContinentID = dtFlashXML.Rows[i]["ContinentID"].ToString();
        //        }

        //        strXML.AppendLine("</States>");
        //        strXML.AppendLine("</Country>");
        //        strXML.AppendLine("</Countries>");
        //        strXML.AppendLine("</Continent>");
        //        strXML.AppendLine("</Continents>");

        //        // Write xml file
        //        string _path = objPage.Server.MapPath(FilePath);
        //        if (Directory.Exists(objPage.Server.MapPath(CommonLogic.GetConfigValue("FlashXMLUserUploadRootDirectory"))) == false)
        //            Directory.CreateDirectory(objPage.Server.MapPath(CommonLogic.GetConfigValue("FlashXMLUserUploadRootDirectory")));
        //        FileStream fStream = new FileStream(_path, FileMode.Create, FileAccess.Write, FileShare.Read, 8, true);
        //        fStream.Close();
        //        StreamWriter sWriter = new StreamWriter(_path);
        //        sWriter.Write(strXML);
        //        sWriter.Flush();
        //        sWriter.Close();
        //    }
        //}

        //public static double SetCurrencyValue(double Price)
        //{
        //    if (CommonLogic.GetApplicationValue("CurrencyCode").Equals("en-US", StringComparison.CurrentCultureIgnoreCase))
        //        Price = Price / CommonLogic.GetApplicationValue("ItalyEuroValue").ParseNativeDouble();
        //    return Price;
        //}

        //public static double GetCurrencyValue(double Price)
        //{
        //    if (CommonLogic.GetApplicationValue("CurrencyCode").Equals("en-US", StringComparison.CurrentCultureIgnoreCase))
        //        Price = Price * CommonLogic.GetApplicationValue("ItalyEuroValue").ParseNativeDouble();
        //    return Price.ToString(".00").ParseNativeDouble();
        //}



        #endregion

        #region Application Supported Function

        /// <summary>
        /// Get the Value of application variable which pass in the parameter ApplicationItem
        /// </summary>
        /// <param name="ApplicationItem"></param>

        public static string GetApplicationValue(string ApplicationItem)
        {
            string tmpString = string.Empty;
            try
            {
                tmpString = System.Web.HttpContext.Current.Application[ApplicationItem].ToString();
            }
            catch
            {
                tmpString = string.Empty;
            }
            return tmpString;
        }

        public static void SetApplicationValue(string paramName, object paramValue)
        {
            HttpContext.Current.Application[paramName] = paramValue;
        }

        #endregion

        #region Get File Related

        public static string getMonth(int Month)
        {
            switch (Month)
            {
                case 1:
                    return "January";
                case 2:
                    return "February";
                case 3:
                    return "March";
                case 4:
                    return "April";
                case 5:
                    return "May";
                case 6:
                    return "June";
                case 7:
                    return "July";
                case 8:
                    return "August";
                case 9:
                    return "September";
                case 10:
                    return "October";
                case 11:
                    return "November";
                case 12:
                    return "December";
                default:
                    return string.Empty;
            }
        }

        #endregion

        #region "RewriteUrl"

        public static string FormatString(string StrItemValue)
        {
            if (StrItemValue != null)
            {
                string strTitle = string.Empty;
                strTitle = StrItemValue.ToString();
                strTitle = strTitle.ToLower();
                strTitle = strTitle.Trim();
                strTitle = strTitle.Trim('-');
                strTitle = strTitle.Replace("c#", "C-Sharp");
                strTitle = strTitle.Replace("vb.net", "VB-Net");
                strTitle = strTitle.Replace("asp.net", "Asp-Net");
                strTitle = strTitle.Replace(".", "-");
                strTitle = strTitle.Replace("&", "and");
                strTitle = strTitle.Replace(" ", "_");

                char[] chars = "$%#@!*?;:~`+=()[]{}|\\'<>,/^\".".ToCharArray();

                for (int i = 0; i <= chars.Length - 1; i++)
                {
                    string strChar = chars.GetValue(i).ToString();
                    if (strTitle.Contains(strChar))
                    {
                        strTitle = strTitle.Replace(strChar, string.Empty);
                    }
                }

                strTitle = strTitle.Replace(" ", "-");
                strTitle = strTitle.Replace("--", "-");
                strTitle = strTitle.Replace("---", "-");
                strTitle = strTitle.Replace("----", "-");
                strTitle = strTitle.Replace("-----", "-");
                strTitle = strTitle.Replace("----", "-");
                strTitle = strTitle.Replace("---", "-");
                strTitle = strTitle.Replace("--", "-");
                strTitle = strTitle.Trim();
                strTitle = strTitle.Trim('-');
                return strTitle;
            }
            else
            {
                return "-";
            }
        }

        #endregion

        #region "Meta Tag"

        public static string GenerateMetaString(string MetaAuthorContent, string MetaDescContent, string MetaKeywordContent)
        {
            string strMeta = string.Empty;
            if (!MetaAuthorContent.Equals(string.Empty))
            {
                strMeta += string.Format("<meta name=\"{0}\" content=\"{1}\" />", "Author", MetaAuthorContent);
            }
            if (!MetaDescContent.Equals(string.Empty))
            {
                strMeta += string.Format("<meta name=\"{0}\" content=\"{1}\" />", "Description", MetaDescContent);
            }
            if (!MetaKeywordContent.Equals(string.Empty))
            {
                strMeta += string.Format("<meta name=\"{0}\" content=\"{1}\" />", "Keywords", MetaKeywordContent);
            }
            return strMeta;
        }


        #endregion

        #region Language Functions
        public static string SetLanguageValue(string Mode, string strValue, string strNewValue)
        {
            try
            {
                if (Mode.Equals("insert", StringComparison.CurrentCultureIgnoreCase))
                {
                    return SetValueByAllLanguage(strNewValue);
                }
                else
                {
                    if (strValue != null)
                    {
                        return SetValueByTag(strValue, strNewValue);
                    }
                    else
                    {
                        return SetValueByAllLanguage(strNewValue);
                    }
                }
            }
            catch
            {
                return string.Empty;
            }
        }

        public static string GetValueByLanguage(string Value, string Mode)
        {
            if (Value != null && !Mode.Equals("byid", StringComparison.CurrentCultureIgnoreCase))
            {
                if (!Value.Equals(string.Empty))
                {
                    if (Value.IndexOf("<" + strLanguage + ">") != -1 && Value.IndexOf("</" + strLanguage + ">") != -1)
                        Value = Value.Substring(Value.IndexOf("<" + strLanguage + ">") + 7, (Value.IndexOf("</" + strLanguage + ">") - Value.IndexOf("<" + strLanguage + ">")) - 7);
                    else
                        Value = Value.Substring(Value.IndexOf("<" + strEnglishLanguage + ">") + 7, (Value.IndexOf("</" + strEnglishLanguage + ">") - Value.IndexOf("<" + strEnglishLanguage + ">")) - 7); // Show Default Language if text is empty
                }
            }
            return Value;
        }

        private static string SetValueByTag(string Value, string repValue)
        {
            string First = string.Empty, Last = string.Empty;

            if (Value.Contains("<" + strLanguage + ">"))
            {
                First = Value.Substring(0, Value.IndexOf("<" + strLanguage + ">") + 7); // <en-US>test</en-US><es-ES>
                Last = Value.Substring(Value.LastIndexOf("</" + strLanguage + ">"), Value.Length - Value.LastIndexOf("</" + strLanguage + ">"));
                Value = First + repValue + Last;
            }
            else
            {
                Value += SetValueByLanguage(repValue, strLanguage);
            }
            return Value;
        }

        private static string SetValueByLanguage(string Value, string Language)
        {
            if (Language.Equals(strEnglishLanguage) || Language == null)
            {
                Value = string.Format("<{0}>{1}</{0}>", strEnglishLanguage, Value);
            }
            else if (Language.Equals(strItalicLanguage))
            {
                Value = string.Format("<{0}>{1}</{0}>", strItalicLanguage, Value);
            }
            //else if (Language.Equals(strFrenchLanguage))
            //{
            //    Value = string.Format("<{0}>{1}</{0}>", strFrenchLanguage, Value);
            //}
            return Value;
        }

        public static string SetValueByAllLanguage(string Value)
        {
            StringBuilder rtnValue = new StringBuilder();
            rtnValue.AppendFormat("<{0}>{1}</{0}>", strEnglishLanguage, Value);
            rtnValue.AppendFormat("<{0}>{1}</{0}>", strItalicLanguage, Value);
            return rtnValue.ToString();
        }

        /// <summary>
        /// Gets the between strings.
        /// </summary>
        /// <param name="strSource">The STR source.</param>
        /// <param name="strStart">The STR start.</param>
        /// <param name="strEnd">The STR end.</param>
        /// <returns></returns>
        public static string GetBetweenStrings(string strSource, string strStart, string strEnd)
        {
            int Start, End;
            if (strSource.Contains(strStart) && strSource.Contains(strEnd))
            {
                Start = strSource.IndexOf(strStart, 0) + strStart.Length;
                End = strSource.IndexOf(strEnd, Start);
                return strSource.Substring(Start, End - Start);
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Sets the value between tags.
        /// </summary>
        /// <param name="Value">The value.</param>
        /// <param name="repValue">The rep value.</param>
        /// <returns></returns>
        public static string SetValueBetweenTags(string Value, string repValue)
        {
            string First = string.Empty, Last = string.Empty;
            First = "<" + Value + ">";
            Last = "</" + Value + ">";
            return Value = First + repValue + Last;
        }

        //public static string getCultureInfo()
        //{
        //    return string.Format("<meta name=\"accept-language\" content=\"{0}\" />", CommonLogic.strLanguage.Split('-')[0]);
        //}

        //public static bool isLanguageChanged()
        //{
        //    Page objPage = HttpContext.Current.Handler as Page;
        //    if (objPage != null)
        //    {
        //        bool flag = false;
        //        if (!objPage.IsPostBack || !strPreviousLanguage.Equals(strLanguage))
        //        {
        //            flag = true;
        //        }
        //        return flag;
        //    }
        //    else
        //        return false;
        //}

        #endregion

        #region Error Message

        /// <summary>
        /// Writes Error Message in Response Stream
        /// </summary>
        /// <param name="strMessage"></param>
        public static void DisplayErrorMsg(string strMessage)
        {
            // In web.config
            // <compilation debug="true">

            // Set in webconfig -> compilation -> debug = true, to Display Error Message
            // Set in webconfig -> compilation -> debug = false, to NOT Display Error Message

            if (HttpContext.Current.IsDebuggingEnabled)
            {
                HttpContext.Current.Response.Write(strMessage);
            }
        }

        #endregion

        /// <summary>
        /// Create a UniqueFileName
        /// </summary>

        public static string CreateUniqueFileName()
        {
            string filename = string.Empty;
            filename = DateTime.Now.Year.ToString() + DateTime.Now.Month + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString();
            return filename;
        }

        /// <summary>
        /// Check if user is logged or not (For Front).
        /// </summary>

        public static bool IsUserLoggedForFront()
        {
            if (System.Web.HttpContext.Current.Session["UserId"] != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Check if user is logged or not (For Front).
        /// </summary>
        public static bool IsUserLoggedForAffiliateFront()
        {
            if (System.Web.HttpContext.Current.Session["AFUserId"] != null)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Converts the list to data table.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public static DataTable ConvertListToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            table.TableName = "My Table";
            foreach (PropertyDescriptor prop in properties)
            {
                if (!prop.Name.Equals("ExtensionData"))
                {
                    table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
                }
            }

            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                {
                    if (!prop.Name.Equals("ExtensionData"))
                    {
                        row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                    }
                }

                table.Rows.Add(row);
            }

            return table;
        }

        ///// <summary>
        ///// Gets the payment mode by ID.
        ///// </summary>
        ///// <param name="propertyID">The property ID.</param>
        ///// <returns></returns>
        //public static int GetPaymentModeByID(long propertyID)
        //{
        //    return BALPropertyMaster.GetPaymentModeByID(propertyID);
        //}

        ///// <summary>
        ///// Gets the paypal email.
        ///// </summary>
        ///// <returns></returns>
        //public static string GetPaypalBusinessEmail()
        //{
        //    return BALGeneralSetting.GetPaypalEmail();
        //}

        ////public static bool IsUserLoggedForAffiliateFront()
        ////{
        ////    if (System.Web.HttpContext.Current.Session["AFUserId"] != null)
        ////    {
        ////        return true;
        ////    }
        ////    return false;
        ////}

        ///// <summary>
        ///// Determines whether [is free now].
        ///// </summary>
        ///// <returns>
        /////   <c>true</c> if [is free now]; otherwise, <c>false</c>.
        ///// </returns>
        //public static bool IsFreeNow()
        //{
        //    BOGeneralSetting objBOGeneralSetting = new BOGeneralSetting();
        //    objBOGeneralSetting = BALGeneralSetting.GetAllGeneralSettings();
        //    if (objBOGeneralSetting != null)
        //    {
        //        return objBOGeneralSetting.IsFreeNow;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        /// <summary>
        /// Forces the SSL.
        /// </summary>
        public static void ForceSSL()
        {
            if (!HttpContext.Current.Request.IsSecureConnection)
            {
                string redirectUrl = HttpContext.Current.Request.Url.ToString().Replace("http:", "https:");
                HttpContext.Current.Response.Redirect(redirectUrl);
            }
        }

        //public static BOEmailTemplate getEmailTemplate(string TemplateCode)
        //{
        //    string strDesc = string.Empty;

        //    BOEmailTemplate objBOEmailTemplate = new BOEmailTemplate();
        //    objBOEmailTemplate = BALEmailTemplate.GetEmailTemplateByCode(TemplateCode);

        //    return objBOEmailTemplate;
        //}


        public static string CalculateMD5Hash(string input)
        {
            // step 1, calculate MD5 hash from input
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);

            // step 2, convert byte array to hex string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }

        public static void ImageCompression(byte[] InputVal, string OutputVal)
        {
            string key = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["TinifyCompressKey"]);
            string url = "https://api.tinify.com/shrink";

            WebClient client = new WebClient();
            string auth = Convert.ToBase64String(Encoding.UTF8.GetBytes("api:" + key));
            client.Headers.Add(HttpRequestHeader.Authorization, "Basic " + auth);

            try
            {
                client.UploadData(url, InputVal);
                /* Compression was successful, retrieve output from Location header. */
                client.DownloadFile(client.ResponseHeaders["Location"], OutputVal);
            }
            catch (WebException)
            {
                /* Something went wrong! You can parse the JSON body for details. */
                Console.WriteLine("Compression failed.");
            }
        }

        public static string Generatehash512(string text)
        {
            byte[] message = Encoding.UTF8.GetBytes(text);

            UnicodeEncoding UE = new UnicodeEncoding();
            byte[] hashValue;
            SHA512Managed hashString = new SHA512Managed();
            string hex = "";
            hashValue = hashString.ComputeHash(message);
            foreach (byte x in hashValue)
            {
                hex += String.Format("{0:x2}", x);
            }
            return hex;
        }
    }
}
