using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace VanickPolicyAckProcess.Webparts.VanickPolicyApprove
{
    [ToolboxItemAttribute(false)]
    public class VanickPolicyApprove : WebPart
    {
        // Visual Studio might automatically update this path when you change the Visual Web Part project item.
        private const string _ascxPath = @"~/_CONTROLTEMPLATES/VanickPolicyAckProcess.Webparts/VanickPolicyApprove/VanickPolicyApproveUserControl.ascx";


        [WebBrowsable(true), Category("Configuration"), Personalizable(PersonalizationScope.Shared), DefaultValue(""), WebDisplayName("Page List"), WebDescription("List name where the webpart will get the information of pages. Example: Pages")]
        public string PageList
        {
            get { return pagelist; }
            set { pagelist = value; }
        }
        public string pagelist = string.Empty;

        [WebBrowsable(true), Category("Configuration"), Personalizable(PersonalizationScope.Shared), DefaultValue(""), WebDisplayName("Approve List"), WebDescription("List name where the webpart will get the information of approve log. Example: Approval List")]
        public string ApproveList
        {
            get { return approvelist; }
            set { approvelist = value; }
        }
        public string approvelist = string.Empty;

        protected override void CreateChildControls()
        {            
            VanickPolicyApproveUserControl control = (VanickPolicyApproveUserControl)Page.LoadControl(_ascxPath);

            if (string.IsNullOrEmpty(this.PageList)) control.PageList = string.Empty;
            else control.PageList = this.PageList;

            if (string.IsNullOrEmpty(this.ApproveList)) control.ApprovalList = string.Empty;
            else control.ApprovalList = this.ApproveList;

            Controls.Add(control);

            base.CreateChildControls(); 
        }

        protected override void OnPreRender(EventArgs e)
        {
            CssRegistration.Register("/_layouts/VanickPolicyAckProcess/css/vanickapprovedata.css");

            Page.ClientScript.RegisterStartupScript(GetType(), "JQuery",
                   "<SCRIPT language='javascript' src='/_layouts/VanickPolicyAckProcess/JavaScript/jquery-1.9.1.min.js'></SCRIPT>", false);

            Page.ClientScript.RegisterStartupScript(GetType(), "JQueryUI",
                   "<SCRIPT language='javascript' src='/_layouts/VanickPolicyAckProcess/JavaScript/jquery-ui.js'></SCRIPT>", false);

            Page.ClientScript.RegisterStartupScript(GetType(), "VanickApproveControl",
                   "<SCRIPT language='javascript' src='/_layouts/VanickPolicyAckProcess/JavaScript/ApproveControl.js'></SCRIPT>", false);

            Page.ClientScript.RegisterStartupScript(GetType(), "VanickPageCreator",
                   "<SCRIPT language='javascript' src='/_layouts/VanickPolicyAckProcess/JavaScript/PageCreator.js'></SCRIPT>", false);
            base.OnPreRender(e);
        }

        protected override void Render(HtmlTextWriter writer)
        {
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "ExeSetPageData", "SetPageData();", true);
            base.Render(writer);
        }
    }
}
