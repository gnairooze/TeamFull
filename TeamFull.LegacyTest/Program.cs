using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TeamFull.LegacyTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Start();

            Console.WriteLine("Press any key to exit ...");
            Console.Read();
        }

        private static void Start()
        {
            TestTeamFullDataCore test = new TestTeamFullDataCore();
            test.TestProjects1();
            test.TestQuery1();
        }
    }
}
