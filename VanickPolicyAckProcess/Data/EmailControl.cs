using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;

namespace VanickPolicyAckProcess.Data
{
    public class EmailControl
    {
        public bool SendEmialInternal(Guid SiteID, Microsoft.SharePoint.Administration.SPUrlZone zone, Guid WebID, string to, string body, string subject)
        {
            if (string.IsNullOrEmpty(to) || string.IsNullOrEmpty(body) || string.IsNullOrEmpty(subject))
                return false;
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {
                    using (SPSite site = new SPSite(SiteID, zone))
                    {
                        using (SPWeb web = site.OpenWeb(WebID))
                        {
                            SPUtility.SendEmail(web, true, false, to,
                            subject, body);
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
