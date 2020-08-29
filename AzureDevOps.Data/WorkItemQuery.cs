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

        }

        public JArray RunQueryEnhanced(Guid projectID, string queryContent, List<string> workItemFields)
        {
            this.Connect();
            var resultWorkItemIDs = this.RunQuery(projectID, queryContent);

            #region put work items IDs in a list
            List<int> workItemsIDs = new List<int>();
            foreach (var item in resultWorkItemIDs["WorkItems"].Children())
            {
                workItemsIDs.Add(item.Value<int>("Id"));
            }
            #endregion

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

            List<int> parentsIDs = new List<int>();
            foreach (var item in resultWorkItems)
            {
                int parentId = int.Parse(item["Fields"]["System.Parent"].ToString());
                if (parentsIDs.IndexOf(parentId) == -1)
                {
                    parentsIDs.Add(parentId);
                }
            }

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
