using System;
using System.Collections.Generic;
using System.Linq;

namespace Installer
{
    /* PackageInstaller static class def.
    *      Public methods:
    *           GeneratePackageInstallationOrder(string[] packages)
    *           GeneratePackageInstallationOrder(string[] packages, bool verbose)
    */
    public static class PackageInstaller
    {

        /* Summary:
         *      A static method to generate a string representing a valid installation ordering of packages considering their dependencies
         * Params:
         *      string[]:   an array of strings representing package names and their installation dependencies in "<Package name>: <dependency>" format
         *      bool:       a switch for printing extra, intermediate information to console
         * Returns:
         *      string: string with the package names in a valid installation order, separated by commas
         * Throws:
         *      Exception with a helpful message
         */
        public static string GeneratePackageInstallationOrder(string[] packages, bool verbose)
        {
            List<Package> packageList = CreatePackageList(packages);

            if (verbose)
            {
                Console.WriteLine("Packages in input order:");
                PrintPackages(packageList);
            }

            packageList = OrderPackages(packageList);

            if (verbose)
            {
                Console.WriteLine("Packages in installation order:");
                PrintPackages(packageList);
            }

            string installationOrder = ConvertPackageListToString(packageList);

            if (verbose)
            {
                Console.WriteLine("Order to install packages:\n" + installationOrder);
            }

            return installationOrder;
        }

        /*
         * If no verbosity preference is given, default to not verbose
         */
        public static string GeneratePackageInstallationOrder(string[] packages)
        {
            return GeneratePackageInstallationOrder(packages, false);
        }

        /* Summary:
         *      Creates a List of Package objects from an array of strings 
         * Params:
         *      string[]: an array of strings representing Packages in "<Package name>: <Installation dependency>" format
         * Returns:
         *      A List of Package objects created from the array
         * Throws:
         *      Exception if any input strings are improperly formatted
         *      Exception if duplicate Package name found
         */
        private static List<Package> CreatePackageList(string[] packages)
        {
            if (packages != null)
            {
                List<Package> packageList = new List<Package>(packages.Length);

                for (int i = 0; i < packages.Length; i++)
                {
                    string[] packageStr = packages[i].Split(": ");

                    if (packageStr.Length == 2)
                    {
                        Package package = new Package(packageStr[0], packageStr[1]);

                        if (FindPackageInListByName(packageList, package.Name) == null)
                        {
                            packageList.Add(package);
                        }
                        else
                        {
                            throw new Exception("Invalid input. Duplicate package name found: \"" + package.Name + "\"");
                        }
                    }
                    else
                    {
                        throw new Exception("Invalid input. Package #" + (i + 1) + " improperly formatted. Package name: \"" + packages[i] + "\"");
                    }
                }

                return packageList;
            }
            else
            {
                return null;
            }
        }

        /* Summary:
         *      Orders List of Packages such that, for all Packages, a Package’s dependency will always precede that Package
         * Params:
         *      List<Package>: packages to be ordered
         * Returns:
         *      List of Package objects in valid order
         * Throws:
         *      Exception upon discovery of a cycle in Package dependencies
         *      Exception if a Package is dependent upon a Package that is not on the install list
         */
        private static List<Package> OrderPackages(List<Package> packageList)
        {
            List<Package> orderedPackages = new List<Package>(packageList.Count);
            List<Package> unorderedPackages = packageList;

            LinkedList<Package> packageChain = new LinkedList<Package>();   // Creates a chain to keep track of visited Packages

            // Loop until there are no more unordered Packages
            while (unorderedPackages.Count > 0)
            {
                // If the chain is currently empty, add the first unordered Package to it
                if (packageChain.Count == 0)
                {
                    packageChain.AddFirst(unorderedPackages[0]);
                }

                // If dependency Package for first Package in packageChain is already in packageChain,
                //      This indicates that a cycle has occurred thus input is invalid; throws an Exception
                if (FindPackageInListByName(packageChain, packageChain.First.Value.Dependency) != null)
                {
                    string errorMessage = "Invalid input. Dependency cycle found: \n";

                    foreach(Package package in packageChain.Reverse())
                    { 
                        errorMessage += package.ToString() + "\n";
                    }

                    throw new Exception(errorMessage);
                }

                // If there is no dependency for the first Package in chain,
                //      Add entire chain to ordered Packages list and remove from unordered Packages list, then clear chain
                if (packageChain.First.Value.Dependency == "")
                {
                    orderedPackages.AddRange(packageChain);

                    foreach (Package p in packageChain)
                    {
                        unorderedPackages.Remove(p);
                    }

                    packageChain.Clear();
                }
                //  Else if the dependency for the first Package in chain is already in ordered Package list,
                //      Add entire chain to ordered Packages list and remove from unordered Packages list, then clear chain
                else if (FindPackageInListByName(orderedPackages, packageChain.First.Value.Dependency) != null)
                {
                    orderedPackages.AddRange(packageChain);

                    foreach (Package p in packageChain)
                    {
                        unorderedPackages.Remove(p);
                    }

                    packageChain.Clear();
                }
                // Else, add the dependency Package for the first Package in packageChain to front of packageChain and loop
                else
                {
                    Package nextPackage = FindPackageInListByName(unorderedPackages, packageChain.First.Value.Dependency);

                    // The next Package being null indicates that the dependency Package does not exist and thus input is invalid,
                    //      Throws Exception in response
                    if (nextPackage == null)
                    {
                        throw new Exception("Package not found, please add Package \"" + packageChain.First.Value.Dependency + "\" and try again.");
                    }
                    else
                    {
                        packageChain.AddFirst(nextPackage);
                    }
                }
            }

            return orderedPackages;
        }

        /* Summary:
         *      Searches IEnumerable object for Package of given name
         * Params:
         *      IEnumerable<Package>:   the list to be searched
         *      string:                 name of package to search for
         * Returns:
         *      The package of matching name, null if not found
         */
        private static Package FindPackageInListByName(IEnumerable<Package> list, string name)
        {
            foreach (Package p in list)
            {
                if (p.Name == name)
                {
                    return p;
                }
            }

            return null;
        }

        /* Sumary:
         *      Formats List of Packages for output
         * Params:
         *      List<Package>: packages to be formatted
         * Returns:
         *      string of package names separated by commas
         */
        private static string ConvertPackageListToString(List<Package> packageList)
        {
            string packageOrder = "";

            if (packageList.Count > 0)
            {
                int i;
                for (i = 0; i < packageList.Count - 1; i++)
                {
                    packageOrder += packageList[i].Name + ", ";
                }

                packageOrder += packageList[i].Name;
            }

            return packageOrder;
        }

        /* Summary:
         *      Prints detailed information on a List of Packages to console
         * Params:
         *      List<Package>: packages to print
         */
        private static void PrintPackages(List<Package> packageList)
        {
            foreach (Package p in packageList)
            {
                Console.WriteLine(p.ToString());
            }

            Console.WriteLine();
        }
    }
}
