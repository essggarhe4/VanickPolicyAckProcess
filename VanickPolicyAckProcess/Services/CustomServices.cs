using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Services;
using Microsoft.SharePoint;
using System.Xml;

using System.ComponentModel;
using System.Data;
using System.Web;
using System.Web.Services.Protocols;
using System.Web.Script.Services;
using System.Collections.Specialized;
using Microsoft.SharePoint.Utilities;
using VanickPolicyAckProcess.Data;

namespace VanickPolicyAckProcess.Services
{
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1), WebService(Namespace = "http://vanick.com/")]
    [System.Web.Script.Services.ScriptService]
    class CustomServices : System.Web.Services.WebService
    {
        [WebMethod(EnableSession = true, Description = "Send Email")]
        public string SendEmail(string PageID, string PageName, string PageURL)
        {
            //string result = "edgar";         

            //return SendEmialInternal(email, Message).ToString();
            return sendEmailNotification(PageID, PageName, PageURL).ToString();
        }

        private bool sendEmailNotification(string PageID, string PageName, string PageURL)
        {
            bool result = false;
            try
            {
                Configuration config = new Configuration(SPContext.Current.Site.ID, SPContext.Current.Web.ID);
                if(!string.IsNullOrEmpty(config.PAGE_NAME) && !string.IsNullOrEmpty(config.APPROVAL_LIST))
                {
                    ApproveData AD = new ApproveData(SPContext.Current.Site.ID, SPContext.Current.Web.ID, config.PAGE_NAME);
                    string UserEmails = AD.GetEmailByPage(PageID);
                    if(!string.IsNullOrEmpty(UserEmails))
                    {
                    EmailControl emailControl = new EmailControl();
                    string bodyh = string.Format("You need to approve the policy: <a href='{0}'>{0}</a>", PageURL, PageName);
                    emailControl.SendEmialInternal(UserEmails, bodyh, "Approve policy");
                    }
                }
                result = true;
            }
            catch{

            }        
            return result;
        }


        private bool SendEmialInternal(string to, string body)
        {
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {
                    using (SPSite site = new SPSite(SPContext.Current.Site.ID,
                    SPContext.Current.Site.Zone))
                    {
                        using (SPWeb web = site.OpenWeb(SPContext.Current.Web.ID))
                        {
                            SPUtility.SendEmail(web, true, true, to,
                            "No agree Page", body);
                        }
                    }
                });
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
