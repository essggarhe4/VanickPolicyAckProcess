using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint;

namespace VanickPolicyAckProcess.Data
{
    public class DataPublishPage
    {
        public string PageID { set; get; }
        public string PageName { set; get; }
        public string PageURL { set; get; }
        public string PageVersion { set; get; }
        public string PageCategory { set; get; }
        public SPFieldUserValueCollection PageGroup { set; get; }
        public SPFieldUserValueCollection PlicySupervisor { set; get; }
    }
}
