using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AzureDevOps.Data
{
    public class WorkItemQuery : Core
    {
        public WorkItemQuery(string orgUrl, string personalAccessToken) : base(orgUrl, personalAccessToken)
        {
            this.Connect();
        }

        public JArray RunQueryEnhanced(Guid projectID, string queryContent, List<string> workItemFields)
        {
            var resultWorkItemIDs = this.RunQuery(projectID, queryContent);

            List<int> workItemsIDs = resultWorkItemIDs["WorkItems"].Children().Select(x => x.Value<int>("Id")).ToList();
            
            var resultWorkItems = this.GetWorkItems(projectID, workItemsIDs, workItemFields);

            if(workItemFields.IndexOf("System.Parent") == -1)
            {
                return resultWorkItems;
            }

            List<string> parentFields = new List<string>() {
                "System.Id",
                "System.WorkItemType",
                "System.Title"
            };

            List<int> parentsIDs = resultWorkItems.Select(x => int.Parse(x["Fields"]["System.Parent"].ToString())).Distinct().ToList();

            var resultParents = this.GetWorkItems(projectID, parentsIDs, parentFields);

            foreach (var item in resultWorkItems)
            {
                string parentId = item["Fields"]["System.Parent"].ToString();

                JToken parent = resultParents.Where(p => p["Fields"]["System.Id"].ToString() == parentId).First();
                item["Fields"]["TeamFull.Parent_Title"] = parent["Fields"]["System.Title"];
                item["Fields"]["TeamFull.Parent_WorkItemType"] = parent["Fields"]["System.WorkItemType"];
            }

            return resultWorkItems;
        }
    }
}
