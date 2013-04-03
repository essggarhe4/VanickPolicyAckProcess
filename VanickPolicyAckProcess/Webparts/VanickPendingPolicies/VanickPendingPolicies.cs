using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace VanickPolicyAckProcess.Webparts.VanickPendingPolicies
{
    [ToolboxItemAttribute(false)]
    public class VanickPendingPolicies : WebPart
    {
        // Visual Studio might automatically update this path when you change the Visual Web Part project item.
        private const string _ascxPath = @"~/_CONTROLTEMPLATES/VanickPolicyAckProcess.Webparts/VanickPendingPolicies/VanickPendingPoliciesUserControl.ascx";

        protected override void CreateChildControls()
        {
            Control control = Page.LoadControl(_ascxPath);
            Controls.Add(control);
        }

        protected override void OnPreRender(EventArgs e)
        {
            //CssRegistration.Register("/_layouts/VanickPolicyAckProcess/css/vanickapprovedata.css");

            //Page.ClientScript.RegisterStartupScript(GetType(), "SetPendingPolicies",
            //       "<SCRIPT language='javascript' src='/_layouts/VanickPolicyAckProcess/JavaScript/PendingPolicies.js'></SCRIPT>", false);           
            base.OnPreRender(e);
        }

        protected override void Render(HtmlTextWriter writer)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "SetPendingPolicies",
                   "<SCRIPT language='javascript' src='/_layouts/VanickPolicyAckProcess/JavaScript/PendingPolicies.js'></SCRIPT>", false);
            base.Render(writer);
        }
    }
}
