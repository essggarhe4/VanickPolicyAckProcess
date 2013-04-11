using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using VanickPolicyAckProcess.Data;
using Microsoft.SharePoint;
using System.Collections.Generic;
using System.Text;

namespace VanickPolicyAckProcess.Webparts.VanickPendingPolicies
{
    public partial class VanickPendingPoliciesUserControl : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //string ApprovalList = ViewState[constants.ViewStateVariables.ApprovalPageList].ToString();
            //string PageList = ViewState[constants.ViewStateVariables.PageList].ToString();

            //string ApprovalList = "Policy Approval";
            //string PageList = "Pages";
            Configuration config = new Configuration(SPContext.Current.Site.ID, SPContext.Current.Web.ID);
            if (!string.IsNullOrEmpty(config.APPROVAL_LIST) && !string.IsNullOrEmpty(config.PAGE_NAME))
            {
                GetPendingPolicies(config.PAGE_NAME, config.APPROVAL_LIST);
            }            
            else
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "ErrorMessagec", string.Format("<script>alert('You need to configure the webpart');</script>"));
            } 
        }

        private void GetPendingPolicies(string PageList, string ApprovalList)
        {
            List<DataPublishPage> approveDataResult = new List<DataPublishPage>();
            List<DataPublishPage> ResultApprovePolicies = new List<DataPublishPage>();

            ApproveData AD = new ApproveData(SPContext.Current.Site.ID, SPContext.Current.Web.ID, PageList, ApprovalList);
            approveDataResult = AD.GetApprovalinformationByUser();
            ResultApprovePolicies = AD.ResultdataApprovgedPages;

            List<string> categoriesList = new List<string>();

            foreach (DataPublishPage DTP in approveDataResult)
            {
                if (categoriesList.IndexOf(DTP.PageCategory) == -1)
                    categoriesList.Add(DTP.PageCategory);
            }

            StringBuilder htmlAccordion = new StringBuilder();
            htmlAccordion.Append("<div id='PolicyPendingAccordion'>");

            if (categoriesList.Count == 0)
            {
                htmlAccordion.Append(string.Format("<h3>{0}</h3>", "No pending policies"));
                htmlAccordion.Append("<div>");
                htmlAccordion.Append("<ul>");
                htmlAccordion.Append(string.Format("<li>{0}</li>", "You don't have pending policies"));                
                htmlAccordion.Append("</ul>");
                htmlAccordion.Append("</div>");
            }

            foreach (string categ in categoriesList)
            {
                List<DataPublishPage> pendingpages = approveDataResult.FindAll(pa => pa.PageCategory == categ);
                htmlAccordion.Append(string.Format("<h3>{0} ({1})</h3>",categ, pendingpages.Count));
                htmlAccordion.Append("<div>");
                htmlAccordion.Append("<ul>");
                foreach (DataPublishPage rr in pendingpages)
                {
                    htmlAccordion.Append(string.Format("<li><a href='{1}'>{0}</a></li>", rr.PageName, rr.PageURL));
                }
                htmlAccordion.Append("</ul>");
                htmlAccordion.Append("</div>");
            }
            htmlAccordion.Append("</div>");

            LitaralPage.Text = htmlAccordion.ToString();

            //For approve pages
            List<string> categoriesListApprove = new List<string>();

            foreach (DataPublishPage DTP in ResultApprovePolicies)
            {
                if (categoriesListApprove.IndexOf(DTP.PageCategory) == -1)
                    categoriesListApprove.Add(DTP.PageCategory);
            }

            StringBuilder htmlAccordionApprove = new StringBuilder();
            htmlAccordionApprove.Append("<div id='PolicyApproveAccordion'>");

            if (categoriesListApprove.Count == 0)
            {
                htmlAccordionApprove.Append(string.Format("<h3>{0}</h3>", "No Approve policies"));
                htmlAccordionApprove.Append("<div>");
                htmlAccordionApprove.Append("<ul>");
                htmlAccordionApprove.Append(string.Format("<li>{0}</li>", "You don't have approved policies"));
                htmlAccordionApprove.Append("</ul>");
                htmlAccordionApprove.Append("</div>");
            }

            foreach (string categ in categoriesListApprove)
            {
                List<DataPublishPage> approvepages = ResultApprovePolicies.FindAll(pa => pa.PageCategory == categ);
                htmlAccordionApprove.Append(string.Format("<h3>{0} ({1})</h3>", categ, approvepages.Count));
                htmlAccordionApprove.Append("<div>");
                htmlAccordionApprove.Append("<ul>");
                foreach (DataPublishPage rr in approvepages)
                {
                    htmlAccordionApprove.Append(string.Format("<li><a href='{1}'>{0}</a></li>", rr.PageName, rr.PageURL));
                }
                htmlAccordionApprove.Append("</ul>");
                htmlAccordionApprove.Append("</div>");
            }
            htmlAccordionApprove.Append("</div>");
            LiteralApprovePages.Text = htmlAccordionApprove.ToString();
        }

    }
}
