using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VanickPolicyAckProcess.Data
{
    public class constants
    {
        public class columns
        {
            public class ApprovalList
            {
                public const string ApprovalUser = "Approval User";
                public const string ApproveComments = "Approve Comments";
                public const string ApprovePageID = "Approve Page ID";
                public const string Version = "Version";
                public const string Title = "Title";
                public const string PageCategory = "Category";                
            }

            public class PageList
            {
                public const string PageName = "Title";
                public const string PageCategory = "Page Category";
                public const string Version = "Version";
                public const string NotifyStatus = "Notify Status";
                public const string ApprovalGroup = "Approval Group";
                public const string PolicySupervisor = "Policy Supervisor";
                public const string ApprovalStatus = "Approval Status";                
            }

            public class ConfigurationList
            {
                public const string Key = "Title";
                public const string Value = "Value";
            }

            public class ConfigurationKeys
            {
                public const string PageName = "PAGE_NAME";
                public const string ApprovalList = "APPROVAL_LIST";
            }
        }

        public class Lists
        {
            public const string ConfigurationsList = "Configurations";
        }

        public class ViewStateVariables
        {
            public const string ApprovalPageList = "VanickPolicyApprovalPage";
            public const string PageList = "VanickPolicyPageList";
        }
    }

    
}
