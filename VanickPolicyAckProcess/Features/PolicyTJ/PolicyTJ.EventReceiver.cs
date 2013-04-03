using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Security;
using Microsoft.SharePoint.Administration;
using VanickPolicyAckProcess.TimerJobs;

namespace VanickPolicyAckProcess.Features.PolicyTJ
{
    /// <summary>
    /// This class handles events raised during feature activation, deactivation, installation, uninstallation, and upgrade.
    /// </summary>
    /// <remarks>
    /// The GUID attached to this class may be used during packaging and should not be modified.
    /// </remarks>

    [Guid("2ad3818d-bb2e-4893-9eee-cfbc53ba2f8d")]
    public class PolicyTJEventReceiver : SPFeatureReceiver
    {
        const string List_JOB_NAME = "Vanick - Policy Approval SLA Email";
        // Uncomment the method below to handle the event raised after a feature has been activated.

        public override void FeatureActivated(SPFeatureReceiverProperties properties)
        {
            SPSite tmpsite = properties.Feature.Parent as SPSite;
            SPSecurity.RunWithElevatedPrivileges(delegate()
            {
                using (SPSite site = new SPSite(tmpsite.ID))
                {
                    foreach (SPJobDefinition job in site.WebApplication.JobDefinitions)
                    {
                        if (job.Name == List_JOB_NAME)

                            job.Delete();
                    }
                    ReminderSLAPages VanickPolicySLAEmail = new ReminderSLAPages(List_JOB_NAME, site.WebApplication);
                    SPDailySchedule schedule = new SPDailySchedule();
                    schedule.BeginHour = 23;
                    schedule.BeginMinute = 0;
                    schedule.BeginSecond = 0;
                    VanickPolicySLAEmail.Schedule = schedule;
                    VanickPolicySLAEmail.Update();
                }
            });            
        }


        // Uncomment the method below to handle the event raised before a feature is deactivated.

        public override void FeatureDeactivating(SPFeatureReceiverProperties properties)
        {
            SPSite tmpsite = properties.Feature.Parent as SPSite;
            SPSecurity.RunWithElevatedPrivileges(delegate()
            {
                using (SPSite site = new SPSite(tmpsite.ID))
                {
                    foreach (SPJobDefinition job in site.WebApplication.JobDefinitions)
                    {
                        if (job.Name == List_JOB_NAME)
                            job.Delete();
                    }
                }
            });            
        }


        // Uncomment the method below to handle the event raised after a feature has been installed.

        //public override void FeatureInstalled(SPFeatureReceiverProperties properties)
        //{
        //}


        // Uncomment the method below to handle the event raised before a feature is uninstalled.

        //public override void FeatureUninstalling(SPFeatureReceiverProperties properties)
        //{
        //}

        // Uncomment the method below to handle the event raised when a feature is upgrading.

        //public override void FeatureUpgrading(SPFeatureReceiverProperties properties, string upgradeActionName, System.Collections.Generic.IDictionary<string, string> parameters)
        //{
        //}
    }
}
