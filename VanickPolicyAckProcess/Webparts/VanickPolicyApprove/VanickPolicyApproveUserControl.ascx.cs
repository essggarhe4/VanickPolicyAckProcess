using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Collections.Generic;
using VanickPolicyAckProcess.Data;
using Microsoft.SharePoint;
using System.Web.Script.Serialization;

namespace VanickPolicyAckProcess.Webparts.VanickPolicyApprove
{
    public partial class VanickPolicyApproveUserControl : UserControl
    {

        public String ApprovalList { get; set; }
        public String PageList { get; set; }
        public string ApprovalGroup { get; set; }

        List<DataPage> approveDataResult;

        protected void Page_Load(object sender, EventArgs e)
        {            
            //this.ApprovalGroup = "Approval group test";
            if (!string.IsNullOrEmpty(ApprovalList) && !string.IsNullOrEmpty(PageList))
            {
                //ViewState[constants.ViewStateVariables.ApprovalPageList] = ApprovalList;
                //ViewState[constants.ViewStateVariables.PageList] = PageList;
                PageDataInit();
            }
            else
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "ErrorMessagec", string.Format("<script>alert('You need to configure the webpart');</script>"));                
            }            
        }

        private void PageDataInit()
        {
            GetPageData();
            //if (GroupExists(SPContext.Current.Web.SiteGroups,this.ApprovalGroup))
            //{
            //    GetPageData();
            //}
            //else
            //{
            //    //Not configurre
            //}
        }

        private void GetPageData()
        {
            if (SPContext.Current != null)
            {
                if (SPContext.Current.List != null && SPContext.Current.List.Title.ToString().Equals(this.PageList))
                {                    
                    if (SPContext.Current.Item != null)
                    {
                        if (SPContext.Current.File.ParentFolder != null && SPContext.Current.File.ParentFolder.Name.Equals("Policies"))
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "SetisCorrectPage", string.Format("<script>isCorrectPage = true;</script>"));
                        }
                        else
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "SetisCorrectPage", string.Format("<script>isCorrectPage = false;</script>"));
                        }

                        ApproveData AD = new ApproveData(SPContext.Current.Site.ID, SPContext.Current.Web.ID, this.ApprovalList);
                        approveDataResult = AD.GetApprovalinformation(SPContext.Current.Item.ID.ToString());
                        //SPContext.Current.Item[SPBuiltInFieldId.Created_x0020_By]
                        SPFieldUserValue userValue = new SPFieldUserValue(SPContext.Current.Web, SPContext.Current.Item["Created By"].ToString());
                        //SPGroup currentApprovalGroup = SPContext.Current.Web.SiteGroups[this.ApprovalGroup];


                        string Author = string.Empty;

                        SPUser cuser = SPContext.Current.Web.CurrentUser;
                        if (userValue.User != null)
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "SetAuthoremailPageID", string.Format("<script>currentemailAuthor = '{0}';</script>", userValue.User.Email));
                            
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "SetPageName", string.Format("<script>currentPageName = '{0}';</script>", SPContext.Current.File.Title));

                            if(SPContext.Current.Item.Fields.ContainsField(constants.columns.PageList.PageCategory) && SPContext.Current.Item[constants.columns.PageList.PageCategory] != null)
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "SetcurrentPageCategory", string.Format("<script>currentPageCategory = '{0}';</script>", SPContext.Current.Item[constants.columns.PageList.PageCategory].ToString()));

                            if (SPContext.Current.Item.Fields.ContainsField(constants.columns.PageList.Version) && SPContext.Current.Item[constants.columns.PageList.Version] != null)
                                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "SetcurrentPageVersion", string.Format("<script type=\"text/javascript\">currentPageVersion = \"{0}\";</script>", SPContext.Current.Item[constants.columns.PageList.Version].ToString()));

                            if (SPContext.Current.Item.Fields.ContainsField(constants.columns.PageList.NotifyStatus) && SPContext.Current.Item[constants.columns.PageList.NotifyStatus] != null)
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "SetcurrentPageNotifyStatus", string.Format("<script>currentPageNotifyStatus = {0};</script>", SPContext.Current.Item[constants.columns.PageList.NotifyStatus].ToString().ToLower()));

                            if (SPContext.Current.Item.Fields.ContainsField(constants.columns.PageList.SLA) && SPContext.Current.Item[constants.columns.PageList.SLA] != null)
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "SetcurrentPageSLA", string.Format("<script>currentPageSLA = {0};</script>", "true"));
                            else
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "SetcurrentPageSLA", string.Format("<script>currentPageSLA = {0};</script>", "false"));

                            Page.ClientScript.RegisterStartupScript(this.GetType(), "SetUserName", string.Format("<script>currentuserName = '{0}';</script>", cuser.Name));
                            //currentemailAuthor    currentuserName


                            Page.ClientScript.RegisterStartupScript(this.GetType(), "SetPageID", string.Format("<script>currentPageID = '{0}';</script>", SPContext.Current.Item.ID.ToString()));
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "SetPageURL", string.Format("<script>currentPageURL = '{0}';</script>", SPContext.Current.File.Url));
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "SetListPage", string.Format("<script>currentListName = '{0}';</script>", this.PageList));
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "SetListApproval", string.Format("<script>currentListApproval = '{0}';</script>", this.ApprovalList));


                            Page.ClientScript.RegisterStartupScript(this.GetType(), "Setcurrentsiteurl", string.Format("<script>currentsiteurl = '{0}';</script>", SPContext.Current.Web.Url));                            



                            Page.ClientScript.RegisterStartupScript(this.GetType(), "SetApprovePageData", string.Format("<script>ApprovePageData = {0};</script>", new JavaScriptSerializer().Serialize(approveDataResult)));

                            //if (((SPListItem)SPContext.Current.Item).ModerationInformation.Status == SPModerationStatusType.Pending)
                            //{
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "SetPageStatus", string.Format("<script>approvePageStatus = '{0}';</script>", ((SPModerationStatusType)int.Parse(SPContext.Current.Item[constants.columns.PageList.ApprovalStatus].ToString()))));
                            //Page.ClientScript.RegisterStartupScript(this.GetType(), "SetPageStatus", string.Format("<script>approvePageStatus = '{0}';</script>", ((SPListItem)SPContext.Current.Item).ModerationInformation.Status.ToString()));
                            //}

                            //Author = userValue.User.Name;
                            if (cuser.LoginName == userValue.User.LoginName)
                            {
                                //Is the Author                 
                                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "SetPageAuthor", "<script type='text/javascript'>isPageAuthor = true;</script>");
                            }
                            else
                            {
                                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "SetPageAuthor", "<script type='text/javascript'>isPageAuthor = false;</script>");
                                //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "SetPageApprover", "<script>isPageApprover = false;</script>");
                            }

                            SPFieldUserValueCollection localSharePointGroup = null;
                            if (SPContext.Current.Item[constants.columns.PageList.ApprovalGroup] != null)
                            {
                                localSharePointGroup = new SPFieldUserValueCollection(((SPListItem)SPContext.Current.Item).Web, SPContext.Current.Item[constants.columns.PageList.ApprovalGroup].ToString());
                                if (localSharePointGroup[0].User == null)
                                {
                                    //is group
                                    if (SPContext.Current.Web.Groups.GetByID(localSharePointGroup[0].LookupId) != null)
                                    {
                                        if (isUserinGroup(SPContext.Current.Site.ID, SPContext.Current.Web.ID, localSharePointGroup[0].LookupId, SPContext.Current.Web.CurrentUser))
                                        {
                                            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "SetPageApprover", "<script>isPageApprover = true;</script>");
                                        }
                                        else
                                        {
                                            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "SetPageApprover", "<script>isPageApprover = false;</script>");
                                        }
                                    }
                                    else
                                    {
                                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "SetPageApprover", "<script>isPageApprover = false;</script>");
                                    }
                                }
                                else
                                {
                                    if (localSharePointGroup[0].User.LoginName.Equals(SPContext.Current.Web.CurrentUser.LoginName))
                                    {
                                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "SetPageApprover", "<script>isPageApprover = true;</script>");
                                    }
                                    else
                                    {
                                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "SetPageApprover", "<script>isPageApprover = false;</script>");
                                    }
                                }
                            }
                            else
                            {
                                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "SetPageApprover", "<script>isPageApprover = true;</script>");
                            }

                            //SPGroup currentApprovalGroup = SPContext.Current.Web.SiteGroups[this.ApprovalGroup];
                            //else if (isUserinGroup(currentApprovalGroup, userValue.User))
                            //{
                            //    //Is the approval
                            //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "SetPageApprover", "<script>isPageApprover = true;</script>");
                            //}
                            
                        }
                        //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "ExeSetPageData", "SetPageData();", true);

                        //LitaralPage.Text = string.Format("<label>Page Title: {0}, Page ID: {1}</label>",SPContext.Current.Item["Title"], SPContext.Current.Item.ID );
                    }
                }
            }

        }

        public static bool GroupExists(SPGroupCollection groups, string name)
        {

            if (string.IsNullOrEmpty(name) ||

                (name.Length > 255) ||

                (groups == null) ||

                (groups.Count == 0))

                return false;

            else

                return (groups.GetCollection(new String[] { name }).Count > 0);

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
    }
}
