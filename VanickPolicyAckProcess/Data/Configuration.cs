﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint;

namespace VanickPolicyAckProcess.Data
{
    public class Configuration
    {
        private Guid siteID { set; get; }
        private Guid webID { set; get; }
        private string listName { set; get; }
        private string approveList { set; get; }

        public string PAGE_NAME { set; get; }
        public string APPROVAL_LIST { set; get; }
        public string PUBLISH_LABEL_APPROVE { set; get; }
        public string PUBLISH_LABEL_NO_APPROVE { get; set; }

        public Configuration(Guid siteid, Guid webid)
        {            
            this.siteID = siteid;
            this.webID = webid;
            GetApprovalinformation();
        }

        public void GetApprovalinformation()
        {            
            SPSecurity.RunWithElevatedPrivileges(delegate()
            {
                using (SPSite site = new SPSite(this.siteID))
                {
                    using (SPWeb web = site.OpenWeb(this.webID))
                    {
                        if (web.Lists.TryGetList(constants.Lists.ConfigurationsList) != null)
                        {
                            SPList configurationList = web.Lists[constants.Lists.ConfigurationsList];
                            SPQuery listQuery = new SPQuery();
                            SPListItemCollection cgenericCollection = configurationList.GetItems(listQuery);

                            Dictionary<string, string> configurationKeys = new Dictionary<string, string>();

                            foreach (SPListItem configurationItem in cgenericCollection)
                            {
                                if(configurationItem.Fields.ContainsField(constants.columns.ConfigurationList.Key) && 
                                    configurationItem[constants.columns.ConfigurationList.Key] != null &&
                                    configurationItem.Fields.ContainsField(constants.columns.ConfigurationList.Value) &&
                                    configurationItem[constants.columns.ConfigurationList.Value] != null)
                                {
                                    configurationKeys.Add(configurationItem[constants.columns.ConfigurationList.Key].ToString(),
                                                          configurationItem[constants.columns.ConfigurationList.Value].ToString()
                                                          );
                                }
                            }

                            this.PUBLISH_LABEL_APPROVE = "";
                            this.PUBLISH_LABEL_NO_APPROVE = "";

                            this.PAGE_NAME = configurationKeys[constants.columns.ConfigurationKeys.PageName];
                            this.APPROVAL_LIST = configurationKeys[constants.columns.ConfigurationKeys.ApprovalList];
                            this.PUBLISH_LABEL_APPROVE = configurationKeys[constants.columns.ConfigurationKeys.PUBLISH_LABEL_APPROVE];
                            this.PUBLISH_LABEL_NO_APPROVE = configurationKeys[constants.columns.ConfigurationKeys.PUBLISH_LABEL_NO_APPROVE];    
                    
                        }                        
                    }
                }
            });            
        }
    }
}
