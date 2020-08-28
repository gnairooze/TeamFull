using System;

namespace TeamFull.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Start();
        }

        private static void Start()
        {
            TestAzureDevOpsDataCore test = new TestAzureDevOpsDataCore();
            //test.TestProjects1();
            //test.TestQuery1();
            //test.TestGetItem1();
            test.TestWorkItems1();
        }
    }
}
