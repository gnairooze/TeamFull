using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace TeamFull.Test
{
    class TestAzureDevOpsDataWorkItemQuery
    {
        public void TestRunQueryEnhanced1()
        {
            string orgUrl = "https://link-dev.visualstudio.com";
            Guid projectID = Guid.Parse("2984ef00-bb46-42d0-bd1a-99498c4ed0f0");
            string pat = "ucwgqwiifof3rcurzv7ji7wmns7phaf6lmmce3mg6pjv3wnmmhla";
            string queryContent = "SELECT [Id] FROM workitems WHERE [Work Item Type] = 'Task' AND [Assigned To] = 'Samar Hassan' AND [State] NOT IN ('Removed', 'Closed', 'Resolved')";
            List<string> workItemFields = new List<string>() {
                "System.Id",
                "System.Title",
                "System.State",
                "System.IterationPath",
                "System.WorkItemType",
                "System.AssignedTo",
                "System.CreatedDate",
                "System.Tags",
                "System.Parent",
                "Microsoft.VSTS.Common.ActivatedDate",
                "Microsoft.VSTS.Common.ResolvedDate",
                "Microsoft.VSTS.Common.ResolvedBy",
                "Microsoft.VSTS.Common.ClosedDate",
                "Microsoft.VSTS.Common.ClosedBy",
                "Microsoft.VSTS.Common.Priority",
                "Microsoft.VSTS.Common.Severity",
                "Microsoft.VSTS.Scheduling.RemainingWork",
                "Microsoft.VSTS.Scheduling.OriginalEstimate",
                "LinkDevAgile.Source",
                "LinkDevAgile.TestingEnvironment",
                "Custom.TaskDueDate"
            };

            AzureDevOps.Data.WorkItemQuery query = new AzureDevOps.Data.WorkItemQuery(orgUrl, pat);
            JArray workItems = query.RunQueryEnhanced(projectID, queryContent, workItemFields);

            Console.WriteLine(workItems);
        }
    }
}
