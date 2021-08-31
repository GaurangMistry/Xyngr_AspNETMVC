using Xyngr.helper;
using System;
using System.Globalization;
using System.Threading;
using System.Web;
using System.Web.UI;
public class LanguageBase : System.Web.UI.Page
{
    /// <summary>
    /// Sets the <see cref="P:System.Web.UI.Page.Culture" /> and <see cref="P:System.Web.UI.Page.UICulture" /> for the current thread of the page.
    /// </summary>
    protected override void InitializeCulture()
    {
        try
        {
            HttpCookie LanguageValue = Request.Cookies["LanguageCode"];
            string LanguageCode = Request["SDFrontMaster$ucMenu$ddlLanguage"] != null ? Request["ctl00$ucMenu$ddlLanguage"] != null ? Request["ctl00$SDFrontMaster$ucMenu$ddlLanguage"] != null ? Request["SDFrontMaster$ucMenu$ddlLanguage"] : Request["ctl00$SDFrontMaster$ucMenu$ddlLanguage"] : Request["ctl00$ucMenu$ddlLanguage"] : Request["SDFrontMaster$ucMenu$ddlLanguage"]; //// Language Drop Down Control in Front End
            if (LanguageCode == null)
            {
                LanguageCode = Request["SDFrontMaster$ucMenu$ddlLanguage"] != null ? Request["ctl00$ucMenu$ddlLanguage"] != null ? Request["ctl00$SDFrontMaster$ucMenu$ddlLanguage"] != null ? Request["SDFrontMaster$ucMenu$ddlLanguage"] : Request["ctl00$SDFrontMaster$ucMenu$ddlLanguage"] : Request["ctl00$ucMenu$ddlLanguage"] : Request["SDFrontMaster$ucMenu$ddlLanguage"];
            }

            if (LanguageCode != null)
            {
                setCulture(LanguageCode);  //// Set Culture language from drop down
                ////Request.Cookies["LanguageCode"].Value = LanguageCode; //// Update REQUEST Cookie language from drop down
                CommonLogic.SetCookies(LanguageCode); //// Set Cookie language from drop down
            }
            else if (LanguageValue != null && LanguageValue.Value != null)
            {
                if (!LanguageValue.Value.Equals(string.Empty))
                {
                    setCulture(LanguageValue.Value); //// Set language from cookie if dropdown not found
                    ////Request.Cookies["LanguageCode"].Value = LanguageValue.Value; //// Update REQUEST Cookie language from cookie
                    CommonLogic.SetCookies(LanguageValue.Value); //// Set Cookie language from cookie
                }
                else
                {
                    setCulture("en-US"); //// Set default language if drop down and cookie both not found
                    CommonLogic.SetCookies("en-US"); //// Set default language if drop down and cookie both not found
                }
            }
            else
            {
                setCulture("en-US"); //// Set default language if drop down and cookie both not found
                CommonLogic.SetCookies("en-US"); //// Set default language if drop down and cookie both not found
            }
        }
        catch (Exception)
        {
            setCulture("en-US"); //// Set default language 
            CommonLogic.SetCookies("en-US"); //// Set default language 
        }

        base.InitializeCulture();
    }

    /// <summary>
    /// Sets the culture.
    /// </summary>
    /// <param name="LanguageValue">The language value.</param>
    private static void setCulture(string LanguageValue)
    {
        Thread.CurrentThread.CurrentUICulture = new CultureInfo(LanguageValue);
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(LanguageValue);

        string strCurrencyCode = CommonLogic.GetApplicationValue("CurrencyCode");
        if (strCurrencyCode != null)
        {
            CultureInfo UICulture = new CultureInfo(strCurrencyCode);
            CultureInfo Culture = CultureInfo.CreateSpecificCulture(strCurrencyCode);
            Thread.CurrentThread.CurrentUICulture.NumberFormat.CurrencySymbol = UICulture.NumberFormat.CurrencySymbol;
            Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencySymbol = Culture.NumberFormat.CurrencySymbol;
        }
    }

    /// <summary>
    /// page load event
    /// </summary>
    /// <param name="e">the event</param>
    protected override void OnLoad(EventArgs e)
    {
        if (this.Form != null)
        {
            this.Form.Action = Request.RawUrl;
        }

        base.OnLoad(e);
    }


    /// <summary>
    /// override Find Control event
    /// </summary>
    /// <param name="id">control id</param>
    /// <returns>return object</returns>
    public override Control FindControl(string id)
    {
        Control bc = null;
        try
        {
            bc = base.FindControl(id);
        }
        catch (HttpException)
        {
            bc = null;
        }

        return (bc != null) ? bc : FindControl(id, this.Controls);
    }

    public static Control FindControl(string id, ControlCollection col)
    {
        foreach (Control c in col)
        {
            Control child = FindControlRecursive(c, id);
            if (child != null)
            {
                return child;
            }
        }

        return null;
    }

    private static Control FindControlRecursive(Control root, string id)
    {
        if (root.ID != null && root.ID == id)
        {
            return root;
        }

        foreach (Control c in root.Controls)
        {
            Control rc = FindControlRecursive(c, id);
            if (rc != null)
            {
                return rc;
            }
        }

        return null;
    }

    /// <summary>
    /// Redirect to home page
    /// </summary>
    private void RedirectToDashboard()
    {
        Response.Redirect("sitemap.aspx", true);
    }

}

