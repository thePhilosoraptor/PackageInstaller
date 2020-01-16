using System;
using System.Collections.Generic;

namespace Installer
{
    public class PackageInstaller
    {
        /*
         * Main method to run and test PackageInstaller class
         */
        static void Main(string[] args)
        {
            string[] inputArr = { "A: B", "B: ", "C: A" };

            PackageInstaller PI = new PackageInstaller(inputArr);

            PI.PrintPkgList();
        }





        /*
         * Actual class def starts here
         */

        List<string[]> pkgList;

        public PackageInstaller(string[] packages)
        {
            pkgList = new List<string[]>();

            foreach (string s in packages)
            {
                pkgList.Add(s.Split(": "));
            }
        }

        public void SupplyNewPackages(string[] packages)
        {
            pkgList = new List<string[]>();

            foreach (string s in packages)
            {
                pkgList.Add(s.Split(": "));
            }
        }

        public void PrintPkgList()
        {
            foreach (string[] s in pkgList)
            {
                Console.WriteLine(s[0] + " | " + s[1]);
            }
        }

    }
}
