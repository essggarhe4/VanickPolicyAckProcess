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
        public bool SendEmialInternal(string to, string body, string subject)
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
