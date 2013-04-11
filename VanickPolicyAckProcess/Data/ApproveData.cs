using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint;
using System.Globalization;

namespace VanickPolicyAckProcess.Data
{
    public class ApproveData
    {
        private Guid siteID { set; get; }
        private Guid webID { set; get; }
        private string listName { set; get; }
        private string approveList { set; get; }

        public string ApproveMessageError { get; set; }

        public List<DataPublishPage> ResultdataApprovgedPages {set; get;}

        public ApproveData(Guid siteid, Guid webid, string listname)
        {
            this.ApproveMessageError = string.Empty;
            this.siteID = siteid;
            this.webID = webid;
            this.listName = listname;
        }

        public ApproveData(Guid siteid, Guid webid, string listname, string approvelist)
        {
            this.ApproveMessageError = string.Empty;
            this.siteID = siteid;
            this.webID = webid;
            this.listName = listname;
            this.approveList = approvelist;
        }

        public string GetEmailByPage(string pageid)
        {
            string result = string.Empty;
            SPSecurity.RunWithElevatedPrivileges(delegate()
            {
                using (SPSite site = new SPSite(this.siteID))
                {
                    using (SPWeb web = site.OpenWeb(this.webID))
                    {
                        if (web.Lists.TryGetList(this.listName) != null)
                        {
                            SPList PageList = web.Lists[this.listName];

                            SPListItem PageItem = PageList.GetItemById(int.Parse(pageid));
                            SPFieldUserValueCollection localSharePointGroup = null;
                            if (PageItem[constants.columns.PageList.ApprovalGroup] != null)
                            {
                                localSharePointGroup = new SPFieldUserValueCollection(((SPListItem)PageItem).Web, PageItem[constants.columns.PageList.ApprovalGroup].ToString());
                                if (localSharePointGroup[0].User == null)
                                {
                                    //is group
                                    result = GetEmailsFromGroup(SPContext.Current.Site.ID, 
                                                                SPContext.Current.Web.ID, 
                                                                localSharePointGroup[0].LookupId, 
                                                                SPContext.Current.Web.CurrentUser);                                    
                                }
                            }
                            else
                            {
                                //This list doesn't exist
                                this.ApproveMessageError = "The list doesn't exist";
                            }
                        }   
                    }
                }
            });
            return result;
        }

        public List<DataPage> GetApprovalinformation(string pageid)
        {
            List<DataPage> datapageList = new List<DataPage>();
            SPSecurity.RunWithElevatedPrivileges(delegate()
            {
                using (SPSite site = new SPSite(this.siteID))
                {
                    using (SPWeb web = site.OpenWeb(this.webID))
                    {
                        if (web.Lists.TryGetList(this.listName) != null)
                        {
                            SPList apporoveList = web.Lists[this.listName];
                            if (isValidList(apporoveList))
                            {
                                SPQuery listQuery = new SPQuery();
                                //listQuery.Query = string.Format("<Where><Eq><FieldRef Name='Approve_x0020_Page_x0020_ID' /><Value Type='Number'>{0}</Value></Eq></Where>", pageid);
                                //listQuery.Query = string.Format("<Where><And><Eq><FieldRef Name='Policy_x0020_Page_x0020_ID' /><Value Type='Text'>{0}</Value></Eq><Eq><FieldRef Name='Author' LookupId='TRUE'/><Value Type='Integer'><UserID/></Value></Eq></And></Where>", pageid);
                                listQuery.Query = string.Format("<Where><Eq><FieldRef Name='Policy_x0020_Page_x0020_ID' /><Value Type='Text'>{0}</Value></Eq></Where>", pageid);


                                SPListItemCollection cgenericCollection = apporoveList.GetItems(listQuery);

                                string cuserln = SPContext.Current.Web.CurrentUser.ID.ToString();

                                foreach (SPListItem approveItem in cgenericCollection)
                                {
                                    
                                    if (approveItem["Author"].ToString().Split(';')[0].Equals(cuserln))
                                    {
                                        string ApprovalStatus = string.Empty;
                                        string ApproveComments = string.Empty;
                                        string ApprovePageID = string.Empty;
                                        int Version = 0;

                                        if (approveItem.Fields.ContainsField(constants.columns.ApprovalList.Title) && approveItem[constants.columns.ApprovalList.Title] != null)
                                        {
                                            ApprovalStatus = approveItem[constants.columns.ApprovalList.Title].ToString();
                                        }

                                        if (approveItem.Fields.ContainsField(constants.columns.ApprovalList.ApproveComments) && approveItem[constants.columns.ApprovalList.ApproveComments] != null)
                                        {
                                            ApproveComments = approveItem[constants.columns.ApprovalList.ApproveComments].ToString();
                                        }

                                        if (approveItem.Fields.ContainsField(constants.columns.ApprovalList.ApprovePageID) && approveItem[constants.columns.ApprovalList.ApprovePageID] != null)
                                        {
                                            ApprovePageID = approveItem[constants.columns.ApprovalList.ApprovePageID].ToString();
                                        }

                                        if (approveItem.Fields.ContainsField(constants.columns.ApprovalList.Version) && approveItem[constants.columns.ApprovalList.Version] != null)
                                        {
                                            Version = int.Parse(approveItem[constants.columns.ApprovalList.Version].ToString());
                                        }




                                        datapageList.Add(new DataPage
                                        {
                                            Status = ApprovalStatus,
                                            Comments = ApproveComments,
                                            Version = Version,
                                            DateCreated = ((DateTime)approveItem[SPBuiltInFieldId.Created]).ToString("f", DateTimeFormatInfo.InvariantInfo)
                                        });
                                    }
                                }
                            }
                            else
                            {
                                //This list is not valid. It has not all teh necesasary columns
                                this.ApproveMessageError = "The list does not have the correct columns";
                            }
                        }
                        else
                        {
                            //This list doesn't exist
                            this.ApproveMessageError = "The list doesn't exist";
                        }
                    }
                }
            });
            return datapageList;
        }

        public List<DataPublishPage> GetApprovalinformationByUser()
        {
            List<DataApprove> dataapproveList = new List<DataApprove>();
            List<DataPublishPage> dataPublishPageList = new List<DataPublishPage>();
            List<DataPublishPage> ResultdataPublishPageList = new List<DataPublishPage>();
            ResultdataApprovgedPages = new List<DataPublishPage>();
            SPSecurity.RunWithElevatedPrivileges(delegate()
            {
                using (SPSite site = new SPSite(this.siteID))
                {
                    using (SPWeb web = site.OpenWeb(this.webID))
                    {
                        if (web.Lists.TryGetList(this.approveList) != null && web.Lists.TryGetList(this.listName) != null)
                        {
                            SPList PublicPageList = web.Lists[this.listName];
                            SPQuery pageListQuery = new SPQuery();
                            pageListQuery.Query = "<Where><Eq><FieldRef Name='_ModerationStatus' /><Value Type='ModStat'>Approved</Value></Eq></Where>";
                            pageListQuery.Folder = PublicPageList.RootFolder.SubFolders["Policies"];
                            SPListItemCollection pagesCollection = PublicPageList.GetItems(pageListQuery);
                            foreach (SPListItem pageItem in pagesCollection)
                            {
                                string tempversion = string.Empty;
                                string tempcategory = string.Empty;
                                SPFieldUserValueCollection localSharePointGroup = null;
                                if(pageItem.Fields.ContainsField(constants.columns.PageList.Version) && pageItem[constants.columns.PageList.Version] != null)
                                    tempversion = pageItem[constants.columns.PageList.Version].ToString();
                                if (pageItem.Fields.ContainsField(constants.columns.PageList.PageCategory) && pageItem[constants.columns.PageList.PageCategory] != null)
                                    tempcategory = pageItem[constants.columns.PageList.PageCategory].ToString();
                                if (pageItem[constants.columns.PageList.ApprovalGroup] != null)
                                {                                    
                                    localSharePointGroup = new SPFieldUserValueCollection(pageItem.Web, pageItem[constants.columns.PageList.ApprovalGroup].ToString());
                                    if (localSharePointGroup[0].User == null)
                                    {                                        
                                        if (SPContext.Current.Web.Groups.GetByID(localSharePointGroup[0].LookupId) != null)
                                        {
                                            if (isUserinGroup(SPContext.Current.Site.ID, SPContext.Current.Web.ID, localSharePointGroup[0].LookupId, SPContext.Current.Web.CurrentUser))
                                            {
                                                dataPublishPageList.Add(new DataPublishPage
                                                {
                                                    PageID = pageItem.ID.ToString(),
                                                    PageName = pageItem.Title,
                                                    PageURL = (string)pageItem[SPBuiltInFieldId.EncodedAbsUrl],
                                                    PageVersion = tempversion,
                                                    PageCategory = tempcategory,
                                                    PageGroup = localSharePointGroup
                                                });
                                            }                                            
                                        }                                       
                                    }
                                    else
                                    {
                                        if (localSharePointGroup[0].User.LoginName.Equals(SPContext.Current.Web.CurrentUser.LoginName))
                                        {
                                            dataPublishPageList.Add(new DataPublishPage
                                            {
                                                PageID = pageItem.ID.ToString(),
                                                PageName = pageItem.Title,
                                                PageURL = pageItem.Url,
                                                PageVersion = tempversion,
                                                PageCategory = tempcategory,
                                                PageGroup = localSharePointGroup
                                            });
                                        }                                        
                                    }
                                }                                                                
                            }


                            SPList apporoveList = web.Lists[this.approveList];
                            if (isValidList(apporoveList))
                            {
                                SPQuery listQuery = new SPQuery();
                                //listQuery.Query = string.Format("<Where><Eq><FieldRef Name='Approve_x0020_Page_x0020_ID' /><Value Type='Number'>{0}</Value></Eq></Where>", pageid);
                                //listQuery.Query = string.Format("<Query><Where><Eq><FieldRef Name='Author' LookupId=’TRUE’/><Value Type=’Integer’><UserID/></Value></Eq></Where></Query>");

                                SPListItemCollection cgenericCollection = apporoveList.GetItems(listQuery);
                                string cuserln = SPContext.Current.Web.CurrentUser.ID.ToString();
                                foreach (SPListItem approveItem in cgenericCollection)
                                {
                                    string PageName = string.Empty;
                                    if (approveItem["Author"].ToString().Split(';')[0].Equals(cuserln))
                                    {
                                        string PageVersion = string.Empty;
                                        string PageCategory = string.Empty;
                                        string PageID = string.Empty;

                                        if (approveItem.Fields.ContainsField(constants.columns.ApprovalList.Title) && approveItem[constants.columns.ApprovalList.Title] != null)
                                        {
                                            PageName = approveItem[constants.columns.ApprovalList.Title].ToString();
                                        }

                                        if (approveItem.Fields.ContainsField(constants.columns.ApprovalList.Version) && approveItem[constants.columns.ApprovalList.Version] != null)
                                        {
                                            PageVersion = approveItem[constants.columns.ApprovalList.Version].ToString();
                                        }

                                        if (approveItem.Fields.ContainsField(constants.columns.ApprovalList.ApprovePageID) && approveItem[constants.columns.ApprovalList.ApprovePageID] != null)
                                        {
                                            PageCategory = approveItem[constants.columns.ApprovalList.PageCategory].ToString();
                                        }

                                        if (approveItem.Fields.ContainsField(constants.columns.ApprovalList.ApprovePageID) && approveItem[constants.columns.ApprovalList.ApprovePageID] != null)
                                        {
                                            PageID = approveItem[constants.columns.ApprovalList.ApprovePageID].ToString();
                                        }

                                        dataapproveList.Add(new DataApprove
                                        {
                                            PageName = PageName,
                                            PageID = PageID,
                                            Version = PageVersion,
                                            Category = PageCategory
                                        });
                                    }
                                }

                                //get all pages          
                                foreach (DataPublishPage DPP in dataPublishPageList)
                                {
                                    int approveCount = dataapproveList.Count(ap => ap.PageID == DPP.PageID && ap.Version == DPP.PageVersion);
                                    if ( approveCount <= 0)
                                    {
                                        ResultdataPublishPageList.Add(DPP);
                                    }
                                    else
                                    {
                                        ResultdataApprovgedPages.Add(DPP);
                                    }
                                }
                                int dd = ResultdataPublishPageList.Count;
                            }
                            else
                            {
                                //This list is not valid. It has not all teh necesasary columns
                                this.ApproveMessageError = "The list does not have the correct columns";
                            }
                        }
                        else
                        {
                            //This list doesn't exist
                            this.ApproveMessageError = "The list doesn't exist";
                        }
                    }
                }
            });
            return ResultdataPublishPageList;
        }

        public void AnalyzeSLAInformation()
        {
            List<DataApprove> dataapproveList = new List<DataApprove>();
            List<DataPublishPage> dataPublishPageList = new List<DataPublishPage>();
            List<DataPublishPage> ResultdataPublishPageList = new List<DataPublishPage>();
            SPSecurity.RunWithElevatedPrivileges(delegate()
            {
                using (SPSite site = new SPSite(this.siteID))
                {
                    using (SPWeb web = site.OpenWeb(this.webID))
                    {
                        if (web.Lists.TryGetList(this.approveList) != null && web.Lists.TryGetList(this.listName) != null)
                        {
                            SPList PublicPageList = web.Lists[this.listName];
                            SPQuery pageListQuery = new SPQuery();
                            pageListQuery.Query = "<Where><And><Eq><FieldRef Name='_ModerationStatus' /><Value Type='ModStat'>Approved</Value></Eq><Gt><FieldRef Name='SLA' /><Value IncludeTimeValue='FALSE' Type='DateTime'><Today /></Value></Gt></And></Where>";
                            pageListQuery.Folder = PublicPageList.RootFolder.SubFolders["Policies"];
                            SPListItemCollection pagesCollection = PublicPageList.GetItems(pageListQuery);
                            foreach (SPListItem pageItem in pagesCollection)
                            {
                                string tempversion = string.Empty;
                                string tempcategory = string.Empty;
                                SPFieldUserValueCollection localSharePointGroup = null;
                                SPFieldUserValueCollection localPolicySupervisor = null;
                                if (pageItem.Fields.ContainsField(constants.columns.PageList.Version) && pageItem[constants.columns.PageList.Version] != null)
                                    tempversion = pageItem[constants.columns.PageList.Version].ToString();
                                if (pageItem.Fields.ContainsField(constants.columns.PageList.PageCategory) && pageItem[constants.columns.PageList.PageCategory] != null)
                                    tempcategory = pageItem[constants.columns.PageList.PageCategory].ToString();
                                if (pageItem[constants.columns.PageList.ApprovalGroup] != null && pageItem[constants.columns.PageList.PolicySupervisor] != null)
                                {
                                    localSharePointGroup = new SPFieldUserValueCollection(pageItem.Web, pageItem[constants.columns.PageList.ApprovalGroup].ToString());
                                    localPolicySupervisor = new SPFieldUserValueCollection(pageItem.Web, pageItem[constants.columns.PageList.PolicySupervisor].ToString());
                                    if (localSharePointGroup[0].User == null)
                                    {
                                        if (SPContext.Current.Web.Groups.GetByID(localSharePointGroup[0].LookupId) != null)
                                        {
                                            SPList apporoveList = web.Lists[this.approveList];
                                            SPQuery listQuery = new SPQuery();
                                            listQuery.Query = string.Format("<Where><And><Eq><FieldRef Name='Policy_x0020_Page_x0020_ID' /><Value Type='Text'>{0}</Value></Eq><Eq><FieldRef Name='_UIVersionString' /><Value Type='Text'>{1}</Value></Eq></And></Where>", pageItem.ID.ToString(), pageItem[constants.columns.PageList.Version].ToString());
                                            
                                            SPListItemCollection cgenericCollection = apporoveList.GetItems(listQuery);
                                            if (cgenericCollection.Count == 0)
                                            {
                                                string allnames = GetAllNamesinGroup(SPContext.Current.Site.ID, SPContext.Current.Web.ID, localSharePointGroup[0].LookupId);
                                                if (!string.IsNullOrEmpty(allnames))
                                                {
                                                    EmailControl emailControl = new EmailControl();
                                                    string bodyh = string.Format("The next user {0} has not approve the policy: {1}", allnames, pageItem.Title);
                                                    emailControl.SendEmialInternal(localPolicySupervisor[0].User.Email, bodyh, "Pending policy");
                                                }
                                            }
                                            else
                                            {
                                                List<SPUser> usersisgroup = GetUsersInGroup(SPContext.Current.Site.ID, SPContext.Current.Web.ID, localSharePointGroup[0].LookupId);
                                                List<string> readyUserList = new List<string>();
                                                string NamesNotapprov = string.Empty;
                                                foreach (SPListItem approveItem in cgenericCollection)
                                                {
                                                    SPFieldUserValue userValue = new SPFieldUserValue(SPContext.Current.Web, approveItem["Created By"].ToString());
                                                    readyUserList.Add(userValue.User.LoginName);
                                                }

                                                foreach (SPUser cuser in usersisgroup)
                                                {
                                                    if (readyUserList.IndexOf(cuser.LoginName) == -1)
                                                    {
                                                        if (string.IsNullOrEmpty(NamesNotapprov))
                                                        {
                                                            NamesNotapprov = cuser.Name;
                                                        }
                                                        else
                                                        {
                                                            NamesNotapprov += string.Format(" ,{0}", cuser.Name);
                                                        }
                                                    }
                                                }                                                

                                                if (!string.IsNullOrEmpty(NamesNotapprov))
                                                {
                                                    EmailControl emailControl = new EmailControl();
                                                    string bodyh = string.Format("The next user {0} has not approve the policy: {1}", NamesNotapprov, pageItem.Title);
                                                    emailControl.SendEmialInternal(localPolicySupervisor[0].User.Email, bodyh, "Pending policy");
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {       
                                        //No send individual
                                    }
                                }
                            }                            
                        }
                        else
                        {
                            //This list doesn't exist
                            this.ApproveMessageError = "The list doesn't exist";
                        }
                    }
                }
            });            
        }

        private bool isValidList(SPList navigationList)
        {
            return true;
            //if (navigationList.Fields.ContainsField(constants.columns.ApprovalList.ApproveComments)
            //    && navigationList.Fields.ContainsField(constants.columns.ApprovalList.ApprovePageID)
            //    && navigationList.Fields.ContainsField(constants.columns.ApprovalList.Version)
            //    )
            //{
            //    return true;
            //}
            //else
            //{
            //    return false;
            //}
        }

        private bool isUserinGroup(Guid siteid, Guid webid, int groupid, SPUser oUser)
        {
            Boolean bUserIsInGroup = false;
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {
                    using (SPSite site = new SPSite(siteid))
                    {
                        using (SPWeb web = site.OpenWeb(webid))
                        {
                            SPGroup isgroup = web.Groups.GetByID(groupid);
                            if (oUser != null)
                            {
                                foreach (SPUser item in isgroup.Users)
                                {
                                    if (item.LoginName == oUser.LoginName)
                                    {
                                        bUserIsInGroup = true;
                                        break;
                                    }
                                }
                            }
                        }
                    }

                });
            }
            catch
            {
            }
            return bUserIsInGroup;
        }

        private string GetEmailsFromGroup(Guid siteid, Guid webid, int groupid, SPUser oUser)
        {
            string ResultEmail = string.Empty;
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {
                    using (SPSite site = new SPSite(siteid))
                    {
                        using (SPWeb web = site.OpenWeb(webid))
                        {
                            SPGroup isgroup = web.Groups.GetByID(groupid);
                            if (oUser != null)
                            {
                                foreach (SPUser item in isgroup.Users)
                                {
                                    if(string.IsNullOrEmpty(ResultEmail))
                                        ResultEmail = item.Email;
                                    else
                                        ResultEmail += string.Format(";{0}", item.Email);
                                }
                            }
                        }
                    }

                });
            }
            catch
            {
            }
            return ResultEmail;
        }

        private List<SPUser> GetUsersInGroup(Guid siteid, Guid webid, int groupid)
        {
            List<SPUser> ResultUsers = new List<SPUser>();
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {
                    using (SPSite site = new SPSite(siteid))
                    {
                        using (SPWeb web = site.OpenWeb(webid))
                        {
                            SPGroup isgroup = web.Groups.GetByID(groupid);
                            
                            foreach (SPUser item in isgroup.Users)
                            {
                                ResultUsers.Add(item);
                            }                            
                        }
                    }

                });
            }
            catch
            {
            }
            return ResultUsers;
        }

        private string GetAllNamesinGroup(Guid siteid, Guid webid, int groupid)
        {
            string resultemails = string.Empty;
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {
                    using (SPSite site = new SPSite(siteid))
                    {
                        using (SPWeb web = site.OpenWeb(webid))
                        {
                            SPGroup isgroup = web.Groups.GetByID(groupid);

                            foreach (SPUser item in isgroup.Users)
                            {
                                if (string.IsNullOrEmpty(resultemails))
                                {
                                    resultemails = item.Name;
                                }
                                else
                                {
                                    resultemails += string.Format(" ,{0}", item.Name);
                                }                                
                            }
                        }
                    }

                });
            }
            catch
            {
            }
            return resultemails;
        } 
    }
}
