<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="VanickPendingPoliciesUserControl.ascx.cs" Inherits="VanickPolicyAckProcess.Webparts.VanickPendingPolicies.VanickPendingPoliciesUserControl" %>


<div id="vanicktabspending">
    <ul>
        <li><a href="#vanicktabspending-pending">Pending</a></li>
        <li><a href="#vanicktabspending-approved">Approved</a></li>
    </ul>
    <div id="vanicktabspending-pending">
        <asp:Literal ID="LitaralPage" runat="server"></asp:Literal>
    </div>
    <div id="vanicktabspending-approved">
       <asp:Literal ID="LiteralApprovePages" runat="server"></asp:Literal>
    </div>    
</div>
