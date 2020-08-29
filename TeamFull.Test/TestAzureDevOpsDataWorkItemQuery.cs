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
            string orgUrl = Environment.GetEnvironmentVariable("VSTS_URL", EnvironmentVariableTarget.User);
            string pat = Environment.GetEnvironmentVariable("VSTS_PAT", EnvironmentVariableTarget.User);
            string projectName = Environment.GetEnvironmentVariable("test_project", EnvironmentVariableTarget.User);
            string queryContent = "SELECT [Id] FROM workitems WHERE [Work Item Type] = 'Task' AND [Assigned To] = @Me AND [State] NOT IN ('Removed', 'Closed', 'Resolved')";
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
            JArray projects = query.GetProjects();
            Guid projectID = Guid.Parse(projects.SelectToken($"$[?(@.Name == '{projectName}')].Id").ToString());
            JArray workItems = query.RunQueryEnhanced(projectID, queryContent, workItemFields);

            Console.WriteLine(workItems);
        }
    }
}
