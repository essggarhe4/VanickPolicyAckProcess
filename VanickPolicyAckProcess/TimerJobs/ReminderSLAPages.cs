using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint.Administration;
using VanickPolicyAckProcess.Data;

namespace VanickPolicyAckProcess.TimerJobs
{
    class ReminderSLAPages : SPJobDefinition
    {
        public ReminderSLAPages()
            : base()
        {

        }

        public ReminderSLAPages(string jobName, SPWebApplication webApplication) 
 
            : base(jobName, webApplication, null, SPJobLockType.ContentDatabase) 
        { 
 
            this.Title = jobName; 
 
        }

        public override void Execute(Guid contentDbId)
        {
            SPWebApplication webApplication = this.Parent as SPWebApplication;
            SPContentDatabase contentDb = webApplication.ContentDatabases[contentDbId];
            CalculateSLA(contentDb.Sites[0].ID, contentDb.Sites[0].RootWeb.ID);            
        }

        private void CalculateSLA(Guid siteID, Guid webID)
        {
            string globalMessage = string.Empty;
            try
            {
                Configuration config = new Configuration(siteID, webID);
                if (!string.IsNullOrEmpty(config.APPROVAL_LIST) && !string.IsNullOrEmpty(config.PAGE_NAME))
                {
                    ApproveData AD = new ApproveData(siteID, webID, config.PAGE_NAME, config.APPROVAL_LIST);
                    AD.AnalyzeSLAInformation();
                    globalMessage = AD.ApproveMessageError;
                    Microsoft.Office.Server.Diagnostics.PortalLog.LogString(globalMessage);
                }
            }
            catch (Exception ex)
            {
                Microsoft.Office.Server.Diagnostics.PortalLog.LogString("Vanick send email error: " + ex.Message + " stack trace: " + ex.StackTrace + " Global Messages: " + globalMessage);
            }
        }

    }
}
