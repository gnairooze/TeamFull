using System;
using System.Collections.Generic;
using System.Text;

namespace TeamFull.LegacyTest
{
    class TestTeamFullDataCore
    {
        public void TestQuery1()
        {
            string azureDevOpsOrganizationUrl = "https://link-dev.visualstudio.com";
            string projectID = "2984ef00-bb46-42d0-bd1a-99498c4ed0f0";
            string pat = "ucwgqwiifof3rcurzv7ji7wmns7phaf6lmmce3mg6pjv3wnmmhla";
            string queryContent = "SELECT [Id], [Title], [State] FROM workitems WHERE [Work Item Type] = 'Bug' AND [Assigned To] = @Me";
            TeamFull.Data.Core core = new Data.Core(azureDevOpsOrganizationUrl, pat);
            core.Connect();
            var result = core.RunQuery(projectID, queryContent);

            Console.WriteLine(result);
        }

        public void TestProjects1()
        {
            string azureDevOpsOrganizationUrl = "https://link-dev.visualstudio.com";
            string pat = "ucwgqwiifof3rcurzv7ji7wmns7phaf6lmmce3mg6pjv3wnmmhla";
            TeamFull.Data.Core core = new Data.Core(azureDevOpsOrganizationUrl, pat);
            core.Connect();
            var result = core.GetProjects();

            Console.WriteLine(result);
        }
    }
}
