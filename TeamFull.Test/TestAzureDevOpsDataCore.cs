using System;
using System.Collections.Generic;
using System.Text;

namespace TeamFull.Test
{
    class TestAzureDevOpsDataCore
    {
        public void TestQuery1()
        {
            string azureDevOpsOrganizationUrl = "https://link-dev.visualstudio.com";
            Guid projectID = Guid.Parse("2984ef00-bb46-42d0-bd1a-99498c4ed0f0");
            string pat = "ucwgqwiifof3rcurzv7ji7wmns7phaf6lmmce3mg6pjv3wnmmhla";
            string queryContent = "SELECT [Id], [Title], [State] FROM workitems WHERE [Work Item Type] = 'Bug' AND [Assigned To] = @Me";
            AzureDevOps.Data.Core core = new AzureDevOps.Data.Core(azureDevOpsOrganizationUrl, pat);
            core.Connect();
            var result = core.RunQuery(projectID, queryContent);

            Console.WriteLine(result);
        }

        public void TestGetItem1()
        {
            string azureDevOpsOrganizationUrl = "https://link-dev.visualstudio.com";
            Guid projectID = Guid.Parse("2984ef00-bb46-42d0-bd1a-99498c4ed0f0");
            string pat = "ucwgqwiifof3rcurzv7ji7wmns7phaf6lmmce3mg6pjv3wnmmhla";
            int workItemId = 143124;
            AzureDevOps.Data.Core core = new AzureDevOps.Data.Core(azureDevOpsOrganizationUrl, pat);
            core.Connect();
            var result = core.GetWorkItem(projectID, workItemId);

            Console.WriteLine(result);
        }

        public void TestWorkItems1()
        {
            string azureDevOpsOrganizationUrl = "https://link-dev.visualstudio.com";
            Guid projectID = Guid.Parse("2984ef00-bb46-42d0-bd1a-99498c4ed0f0");
            string pat = "ucwgqwiifof3rcurzv7ji7wmns7phaf6lmmce3mg6pjv3wnmmhla";
            string queryContent = "SELECT [Id], [Task Due Date], [Parent] FROM workitems WHERE [Work Item Type] = 'Task' AND [Assigned To] = @Me AND [State] NOT IN ('Removed', 'Closed', 'Resolved')";
            List<string> fields = new List<string>() { 
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

            AzureDevOps.Data.Core core = new AzureDevOps.Data.Core(azureDevOpsOrganizationUrl, pat);
            
            core.Connect();
            var resultWorkItems = core.RunQuery(projectID, queryContent);
            List<int> workItemsIDs = new List<int>();
            List<int> parentsIDs = new List<int>();
            foreach (var item in resultWorkItems["WorkItems"].Children())
            {
                workItemsIDs.Add(item.Value<int>("Id"));
            }

            var result = core.GetWorkItems(projectID, workItemsIDs, fields);

            foreach (var item in result)
            {
                int parentId = int.Parse(item["Fields"]["System.Parent"].ToString());
                if (parentsIDs.IndexOf(parentId) > -1)
                {
                    parentsIDs.Add(parentId);
                }
            }

            var resultParents = core.GetWorkItems(projectID, parentsIDs, new List<String>() { "System.Id","System.WorkItemType","System.Title"});

            Console.WriteLine(resultParents);
            Console.WriteLine(result);
        }

        public void TestProjects1()
        {
            string azureDevOpsOrganizationUrl = "https://link-dev.visualstudio.com";
            string pat = "ucwgqwiifof3rcurzv7ji7wmns7phaf6lmmce3mg6pjv3wnmmhla";
            AzureDevOps.Data.Core core = new AzureDevOps.Data.Core(azureDevOpsOrganizationUrl, pat);
            core.Connect();
            var result = core.GetProjects();

            Console.WriteLine(result);
        }
    }
}
