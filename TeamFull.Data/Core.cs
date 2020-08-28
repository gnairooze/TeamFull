using Microsoft.TeamFoundation.Core.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using Microsoft.VisualStudio.Services.Client;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TeamFull.Data
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

        public string RunQuery(string projectID, string queryContent)
        {
            Wiql query = new Wiql() { Query = queryContent };
            WorkItemQueryResult queryResults = this._WorkItemClient.QueryByWiqlAsync(query, projectID).Result;
            

            return Newtonsoft.Json.JsonConvert.SerializeObject(queryResults);
        }

        public string GetProjects()
        {
            IEnumerable<TeamProjectReference> projects = this._ProjectClient.GetProjects().Result;

            return Newtonsoft.Json.JsonConvert.SerializeObject(projects);
        }
    }
}
