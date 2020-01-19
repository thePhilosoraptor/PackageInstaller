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
            string[] inputArr = { "A: B", "B: ", "C: A" };

            try
            {
                Queue<Package> installationOrder = GenerateInstallationOrder(inputArr);
                Console.WriteLine();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }



        /*
         * Define
         */
        public string[] GenerateInstallationOrder(string[] pkgsToInstall)
        {
            List<Package> pkgList = new List<Package>();

            for (int i = 0; i < pkgsToInstall.Length; i++)
            {
                string[] pkgStr = pkgsToInstall[i].Split(": ");

                if (pkgStr.Length == 2)
                {
                    Package pkg = new Package(pkgStr[0], pkgStr[1]);
                    pkgList.Add(pkg);
                }
                else
                {
                    throw new Exception("Input package improperly formatted at index " + i + ". Package name: " + newPackages[i]);
                }
            }
    }
}
