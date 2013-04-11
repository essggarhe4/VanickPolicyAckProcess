<%@ Page language="C#"   Inherits="Microsoft.SharePoint.Publishing.PublishingLayoutPage,Microsoft.SharePoint.Publishing,Version=14.0.0.0,Culture=neutral,PublicKeyToken=71e9bce111e9429c" meta:progid="SharePoint.WebPartPage.Document" %>
<%@ Register Tagprefix="SharePointWebControls" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> <%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> <%@ Register Tagprefix="PublishingWebControls" Namespace="Microsoft.SharePoint.Publishing.WebControls" Assembly="Microsoft.SharePoint.Publishing, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> <%@ Register Tagprefix="PublishingNavigation" Namespace="Microsoft.SharePoint.Publishing.Navigation" Assembly="Microsoft.SharePoint.Publishing, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<asp:Content ContentPlaceholderID="PlaceHolderAdditionalPageHead" runat="server">
	<SharePointWebControls:UIVersionedContent UIVersion="3" runat="server" __designer:Preview="" __designer:Values="&lt;P N='InDesign' T='False' /&gt;&lt;P N='ID' ID='1' T='ctl00' /&gt;&lt;P N='Page' ID='2' /&gt;&lt;P N='TemplateControl' R='2' /&gt;&lt;P N='AppRelativeTemplateSourceDirectory' R='-1' /&gt;">
		<ContentTemplate>
			<SharePointWebControls:CssRegistration name="<% $SPUrl:~sitecollection/Style Library/~language/Core Styles/pageLayouts.css %>" runat="server"/>
			<PublishingWebControls:editmodepanel runat="server" id="editmodestyles">
				<!-- Styles for edit mode only-->
				<SharePointWebControls:CssRegistration name="<% $SPUrl:~sitecollection/Style Library/~language/Core Styles/zz2_editMode.css %>" runat="server"/>
			</PublishingWebControls:editmodepanel>
		</ContentTemplate>
	</SharePointWebControls:UIVersionedContent>
	<SharePointWebControls:UIVersionedContent UIVersion="4" runat="server" __designer:Preview="
			&lt;link rel=&quot;stylesheet&quot; type=&quot;text/css&quot; href=&quot;/sites/Policy/Style%20Library/en-US/Core%20Styles/page-layouts-21.css&quot;/&gt;

			
		" __designer:Values="&lt;P N='UIVersion' T='4' /&gt;&lt;P N='InDesign' T='False' /&gt;&lt;P N='ID' ID='1' T='ctl01' /&gt;&lt;P N='Page' ID='2' /&gt;&lt;P N='TemplateControl' R='2' /&gt;&lt;P N='AppRelativeTemplateSourceDirectory' R='-1' /&gt;">
		<ContentTemplate>
			<SharePointWebControls:CssRegistration name="<% $SPUrl:~sitecollection/Style Library/~language/Core Styles/page-layouts-21.css %>" runat="server"/>
			<PublishingWebControls:EditModePanel runat="server">
				<!-- Styles for edit mode only-->
				<SharePointWebControls:CssRegistration name="<% $SPUrl:~sitecollection/Style Library/~language/Core Styles/edit-mode-21.css %>"
					After="<% $SPUrl:~sitecollection/Style Library/~language/Core Styles/page-layouts-21.css %>" runat="server"/>
			</PublishingWebControls:EditModePanel>
		</ContentTemplate>
	</SharePointWebControls:UIVersionedContent>
	<SharePointWebControls:CssRegistration name="<% $SPUrl:~sitecollection/Style Library/~language/Core Styles/rca.css %>" runat="server" __designer:Preview="&lt;link rel=&quot;stylesheet&quot; type=&quot;text/css&quot; href=&quot;/sites/Policy/Style%20Library/en-US/Core%20Styles/rca.css&quot;/&gt;
" __designer:Values="&lt;P N='Name' Bound='True' T='SPUrl:~sitecollection/Style Library/~language/Core Styles/rca.css' /&gt;&lt;P N='InDesign' T='False' /&gt;&lt;P N='ID' ID='1' T='ctl02' /&gt;&lt;P N='Page' ID='2' /&gt;&lt;P N='TemplateControl' R='2' /&gt;&lt;P N='AppRelativeTemplateSourceDirectory' R='-1' /&gt;"/>
	<SharePointWebControls:FieldValue id="PageStylesField" FieldName="HeaderStyleDefinitions" runat="server" __designer:Preview="&lt;table cellpadding=&quot;4&quot; cellspacing=&quot;0&quot; style=&quot;font: messagebox; color: buttontext; background-color: buttonface; border: solid 1px; border-top-color: buttonhighlight; border-left-color: buttonhighlight; border-bottom-color: buttonshadow; border-right-color: buttonshadow&quot;&gt;
                &lt;tr&gt;&lt;td nowrap&gt;&lt;span style=&quot;font-weight: bold; color: red&quot;&gt;Error Rendering Control&lt;/span&gt; - PageStylesField&lt;/td&gt;&lt;/tr&gt;
                &lt;tr&gt;&lt;td&gt;An unhandled exception has occurred.&lt;br /&gt;Object reference not set to an instance of an object.&lt;/td&gt;&lt;/tr&gt;
              &lt;/table&gt;" __designer:Values="&lt;P N='ID' ID='1' T='PageStylesField' /&gt;&lt;P N='FieldName' T='HeaderStyleDefinitions' /&gt;&lt;P N='ControlMode' E='1' /&gt;&lt;P N='Page' ID='2' /&gt;&lt;P N='TemplateControl' R='2' /&gt;&lt;P N='AppRelativeTemplateSourceDirectory' R='-1' /&gt;"/>
</asp:Content>
<asp:Content ContentPlaceholderID="PlaceHolderPageTitle" runat="server">
	<SharePointWebControls:FieldValue id="PageTitle" FieldName="Title" runat="server" __designer:Preview="" __designer:Values="&lt;P N='ID' ID='1' T='PageTitle' /&gt;&lt;P N='Visible' T='True' /&gt;&lt;P N='FieldName' T='Title' /&gt;&lt;P N='ControlMode' E='1' /&gt;&lt;P N='InDesign' T='False' /&gt;&lt;P N='Page' ID='2' /&gt;&lt;P N='TemplateControl' R='2' /&gt;&lt;P N='AppRelativeTemplateSourceDirectory' R='-1' /&gt;"/>
</asp:Content>
<asp:Content ContentPlaceholderID="PlaceHolderPageTitleInTitleArea" runat="server">
	<SharePointWebControls:UIVersionedContent UIVersion="3" runat="server" __designer:Preview="" __designer:Values="&lt;P N='InDesign' T='False' /&gt;&lt;P N='ID' ID='1' T='ctl03' /&gt;&lt;P N='Page' ID='2' /&gt;&lt;P N='TemplateControl' R='2' /&gt;&lt;P N='AppRelativeTemplateSourceDirectory' R='-1' /&gt;">
		<ContentTemplate>
			<SharePointWebControls:TextField runat="server" id="TitleField" FieldName="Title"/>
		</ContentTemplate>
	</SharePointWebControls:UIVersionedContent>
	<SharePointWebControls:UIVersionedContent UIVersion="4" runat="server" __designer:Preview="
			
		" __designer:Values="&lt;P N='UIVersion' T='4' /&gt;&lt;P N='InDesign' T='False' /&gt;&lt;P N='ID' ID='1' T='ctl04' /&gt;&lt;P N='Page' ID='2' /&gt;&lt;P N='TemplateControl' R='2' /&gt;&lt;P N='AppRelativeTemplateSourceDirectory' R='-1' /&gt;">
		<ContentTemplate>
			<SharePointWebControls:FieldValue FieldName="Title" runat="server"/>
		</ContentTemplate>
	</SharePointWebControls:UIVersionedContent>
</asp:Content>
<asp:Content ContentPlaceHolderId="PlaceHolderTitleBreadcrumb" runat="server"> 
	<SharePointWebControls:VersionedPlaceHolder UIVersion="3" runat="server" __designer:Preview="" __designer:Values="&lt;P N='ID' ID='1' T='ctl05' /&gt;&lt;P N='Page' ID='2' /&gt;&lt;P N='TemplateControl' R='2' /&gt;&lt;P N='AppRelativeTemplateSourceDirectory' R='-1' /&gt;"> <ContentTemplate> <asp:SiteMapPath ID="siteMapPath" runat="server" SiteMapProvider="CurrentNavigation" RenderCurrentNodeAsLink="false" SkipLinkText="" CurrentNodeStyle-CssClass="current" NodeStyle-CssClass="ms-sitemapdirectional"/> </ContentTemplate> </SharePointWebControls:VersionedPlaceHolder> 
	<SharePointWebControls:UIVersionedContent UIVersion="4" runat="server" __designer:Preview=" &lt;ul class=&quot;s4-breadcrumb&quot;&gt;

&lt;/ul&gt; " __designer:Values="&lt;P N='UIVersion' T='4' /&gt;&lt;P N='InDesign' T='False' /&gt;&lt;P N='ID' ID='1' T='ctl06' /&gt;&lt;P N='Page' ID='2' /&gt;&lt;P N='TemplateControl' R='2' /&gt;&lt;P N='AppRelativeTemplateSourceDirectory' R='-1' /&gt;"> <ContentTemplate> <SharePointWebControls:ListSiteMapPath runat="server" SiteMapProviders="CurrentNavigation" RenderCurrentNodeAsLink="false" PathSeparator="" CssClass="s4-breadcrumb" NodeStyle-CssClass="s4-breadcrumbNode" CurrentNodeStyle-CssClass="s4-breadcrumbCurrentNode" RootNodeStyle-CssClass="s4-breadcrumbRootNode" NodeImageOffsetX=0 NodeImageOffsetY=353 NodeImageWidth=16 NodeImageHeight=16 NodeImageUrl="/_layouts/images/fgimg.png" HideInteriorRootNodes="true" SkipLinkText="" /> </ContentTemplate> </SharePointWebControls:UIVersionedContent> </asp:Content>
<asp:Content ContentPlaceholderID="PlaceHolderMain" runat="server">
	<SharePointWebControls:UIVersionedContent UIVersion="3" runat="server" __designer:Preview="" __designer:Values="&lt;P N='InDesign' T='False' /&gt;&lt;P N='ID' ID='1' T='ctl07' /&gt;&lt;P N='Page' ID='2' /&gt;&lt;P N='TemplateControl' R='2' /&gt;&lt;P N='AppRelativeTemplateSourceDirectory' R='-1' /&gt;">
		<ContentTemplate>
			<table id="MSO_ContentTable" cellpadding="0" cellspacing="0" border="0" width="100%">
				<tbody>
				<tr>
					<td>
						<div class="pageContent">
							<PublishingWebControls:RichHtmlField id="content" FieldName="PublishingPageContent" runat="server" />
						</div>
					</td>
				</tr>
			</table>
			<div style="clear:both">&#160;</div>
			<PublishingWebControls:editmodepanel runat="server" id="editmodepanel1">
				<!-- Add field controls here to bind custom metadata viewable and editable in edit mode only.-->
				<table cellpadding="10" cellspacing="0" align="center" class="editModePanel">
					<tr>
						<td>
							<PublishingWebControls:RichImageField id="ContentQueryImage" FieldName="PublishingRollupImage" AllowHyperLinks="false" runat="server"/>
						</td>
						<td width="200">
							<asp:label text="<%$Resources:cms,Article_rollup_image_text%>" runat="server" />
						</td>
					</tr>
				</table>
				</PublishingWebControls:editmodepanel>
		</ContentTemplate>
	</SharePointWebControls:UIVersionedContent>
	<SharePointWebControls:UIVersionedContent UIVersion="4" runat="server" __designer:Preview="
			&lt;div class=&quot;article article-body&quot;&gt;
				
				&lt;div class=&quot;article-content&quot;&gt;
					
				&lt;/div&gt;
				
			&lt;/div&gt;
		" __designer:Values="&lt;P N='UIVersion' T='4' /&gt;&lt;P N='InDesign' T='False' /&gt;&lt;P N='ID' ID='1' T='ctl08' /&gt;&lt;P N='Page' ID='2' /&gt;&lt;P N='TemplateControl' R='2' /&gt;&lt;P N='AppRelativeTemplateSourceDirectory' R='-1' /&gt;">
		<ContentTemplate>
			<div class="article article-body">
				<PublishingWebControls:EditModePanel runat="server" CssClass="edit-mode-panel">
					<SharePointWebControls:TextField runat="server" FieldName="Title" />
					<SharePointWebControls:DropDownChoiceField runat="server" FieldName="Page Category" />					
					<SharePointWebControls:BooleanField runat="server" FieldName="Notify Status" />
					<SharePointWebControls:DateTimeField runat="server" FieldName="SLA" />
					<SharePointWebControls:UserField runat="server" FieldName="Approval Group" />
					<SharePointWebControls:UserField runat="server" FieldName="Policy Supervisor" />
					
				</PublishingWebControls:EditModePanel>
				<div class="article-content">
					<PublishingWebControls:RichHtmlField FieldName="PublishingPageContent" HasInitialFocus="True" MinimumEditHeight="400px" runat="server"/>
				</div>
				<PublishingWebControls:EditModePanel runat="server" CssClass="edit-mode-panel roll-up">
					<PublishingWebControls:RichImageField FieldName="PublishingRollupImage" AllowHyperLinks="false" runat="server" />
					<asp:Label text="<%$Resources:cms,Article_rollup_image_text%>" runat="server" />
				</PublishingWebControls:EditModePanel>
			</div>
		</ContentTemplate>
	</SharePointWebControls:UIVersionedContent>
</asp:Content>
