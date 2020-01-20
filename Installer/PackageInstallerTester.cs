using System;
using System.Collections.Generic;
using System.Text;

namespace Installer
{
    class PackageInstallerTester
    {
        /*
         * Main method to test PackageInstaller class
         */
        static void Main(string[] args)
        {
            List<string[]> tests = new List<string[]>
            {
    //      0
                null,
                new string[] { },
                new string[] { "" },
                new string[] { "A" },
                new string[] { "A " },
    //      5
                new string[] { "A:" },
                new string[] { "A: " },
                new string[] { "A: B" },
                new string[] { "A: A" },
                new string[] { "A: B, C" },
    //      10
                new string[] { "A: B: C" },
                new string[] { "A: B", "B" },
                new string[] { "A: B", "B: " },
                new string[] { "A: B", "B: B" },
                new string[] { "A: B", "B: A" },
    //      15
                new string[] { "A: B", "B: C" },
                new string[] { "A: B", "B: C", "C: " },
                new string[] { "A: B", "B: C", "C: C" },
                new string[] { "A: B", "B: C", "C: A" },
                new string[] { "A: B", "B: C", "C: B" },
    //      20 
                new string[] { "A: B", "B: B", "C: B" },
                new string[] { "A: C", "B: C", "C: A" },
                new string[] { "A: D", "B: C", "C: B", "D: " },
                new string[] { "A: B", "B: C", "C: D", "D: A" },
                new string[] { "A: D", "B: C", "C: ", "D: " },
    //      25
                new string[] { "A: ", "B: ", "C: ", "D: ", "E: ", "F: ", "G: "},
                new string[] { "A: B", "A: " },
                new string[] { "A: B", "A: B", "B: " },
                new string[] { "A: B", "A: C", "B: ", "C: " },
                new string[] { "KittenService: CamelCaser", "CamelCaser: " },
    //      30
                new string[] { "KittenService: ", "Leetmeme: Cyberportal", "Cyberportal: Ice", "CamelCaser: KittenService", "Fraudstream: Leetmeme", "Ice: "},
                new string[] { "KittenService: ", "Leetmeme: Cyberportal", "Cyberportal: Ice", "CamelCaser: KittenService", "Fraudstream: ", "Ice: Leetmeme"}
            };


            for (int i = 0; i < tests.Count; i++)
            {
                Console.WriteLine("Test number " + i);

                try
                {
                    string installationOrder = PackageInstaller.GeneratePackageInstallationOrder(tests[i], true);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

                Console.WriteLine("--------------------------------------------------------------------------");
            }
        }
    }
}
