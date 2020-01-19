using System;
using System.Collections.Generic;

namespace Installer
{
    public class PackageInstaller
    {
        /*
         * Main method to test PackageInstaller class
         */
        static void Main(string[] args)
        {
            string[] inputArr = { "A: B", "B: ", "C: A"};

            try
            {
                PackageInstaller PI = new PackageInstaller(inputArr);

                PI.PrintPackages();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }




        /*
         * PackageInstaller class def.
         *      Defines a class that creates a list of Package objects to install and orders them so that any Packages' dependency is always installed
         *      before the Package itself
         */

        readonly List<Package> pkgList;

        public PackageInstaller()
        {
            pkgList = new List<Package>();
        }

        public PackageInstaller(string[] packages) 
            : this()
        {
            CreatePackageList(packages);
        }

        /*
         * Generates a List of Packages from an array of strings representing Packages in "<Package name>: <Installation dependencies>" format
         * Throws Exception if any input strings are improperly formatted
         */
        public void CreatePackageList(string[] packages)
        {
            for (int i = 0; i < packages.Length; i++)
            {
                string[] pkgStr = packages[i].Split(": ");
                if (pkgStr.Length == 2)
                {
                    Package pkg = new Package(pkgStr[0], pkgStr[1]);
                    pkgList.Add(pkg);
                }
                else
                {
                    throw new Exception("Input package improperly formatted at index " + i + ". Package name: " + packages[i]);
                }
            }
        }

        /*
         * Define
         */
        public string[] GenerateInstallationOrder(string[] pkgsToInstall)
        {
            return null;
        }

        public void PrintPackages()
        {
            foreach (Package p in pkgList)
            {
                Console.WriteLine(p.ToString());
            }
        }
    }
}
