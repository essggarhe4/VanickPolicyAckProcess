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
            }
        }     
    }
}
