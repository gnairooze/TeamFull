using Microsoft.TeamFoundation.Core.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using Microsoft.VisualStudio.Services.Client;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.WebApi;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AzureDevOps.Data
{
    public class Core
    {
        #region attributes
        private string _PersonalAccessToken;
        private VssConnection _Connection;
        private WorkItemTrackingHttpClient _WorkItemClient;
        private ProjectHttpClient _ProjectClient;
        #endregion

        #region properties
        public string OrgUrl { get; set; }
        #endregion

        #region constructors
        public Core(string orgUrl, string personalAccessToken)
        {
            this.OrgUrl = orgUrl;
            this._PersonalAccessToken = personalAccessToken;
        }
        #endregion

        public void Connect()
        {
            this._Connection = new VssConnection(new Uri(OrgUrl), new VssBasicCredential(string.Empty, this._PersonalAccessToken));

            this._WorkItemClient = this._Connection.GetClient<WorkItemTrackingHttpClient>();
            this._ProjectClient = this._Connection.GetClient<ProjectHttpClient>();
        }

        public JObject RunQuery(Guid projectID, string queryContent)
        {
            Wiql query = new Wiql() { Query = queryContent };
            WorkItemQueryResult queryResults = this._WorkItemClient.QueryByWiqlAsync(query, projectID).Result;

            return JObject.FromObject(queryResults);
        }

        public JArray GetProjects()
        {
            IEnumerable<TeamProjectReference> projects = this._ProjectClient.GetProjects().Result;

            return JArray.FromObject(projects);
        }

        public JObject GetWorkItem(Guid projectID, int workItemID, IEnumerable<string> fields = null)
        {
            WorkItem result = this._WorkItemClient.GetWorkItemAsync(projectID, workItemID, fields).Result;

            return JObject.FromObject(result);
        }

        public JArray GetWorkItems(Guid projectID, IEnumerable<int> workItemIDs, IEnumerable<string> fields = null)
        {
            List<WorkItem> result = this._WorkItemClient.GetWorkItemsAsync(projectID, workItemIDs, fields).Result;

            return JArray.FromObject(result);
        }
    }
}
