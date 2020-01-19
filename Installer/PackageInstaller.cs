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

                PI.OrderPackages();

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

        private List<Package> pkgList;

        public PackageInstaller()
        {
            pkgList = new List<Package>();
        }

        public PackageInstaller(string[] packages)
        {
            CreatePackageList(packages);
        }

        /*
         * Creates a List of Packages from an array of strings representing Packages in "<Package name>: <Installation dependencies>" format
         * Throws Exception if any input strings are improperly formatted
         */
        public void CreatePackageList(string[] packages)
        {
            pkgList = new List<Package>(packages.Length);

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
         * Orders pkgList such that, for all Packages dependent on another Package, the other Package is listed first.
         * Does not allow for cycles in Package dependencies.
         */
        public void OrderPackages()
        {
            List<Package> orderedPkgs = new List<Package>(pkgList.Count);
            List<Package> unorderedPkgs = pkgList;

            LinkedList<Package> pkgChain = new LinkedList<Package>();

            while (unorderedPkgs.Count > 0)
            {
                // If the chain is currently empty, add the first unordered Package to it
                if (pkgChain.Count == 0)
                {
                    pkgChain.AddFirst(unorderedPkgs[0]);
                }

                // If dependency Package for first Package in pkgChain is already in pkgChain,
                //      This indicates that a cycle has occurred, throws an Exception
                if (FindPackageInListByName(pkgChain, pkgChain.First.Value.Dependency) != null)
                {
                    throw new Exception("Dependency cycle found. Invalid input.");
                }

                // If there is no dependency for the first Package in chain,
                //      Add entire chain to ordered Packages list and remove from unordered Packages list, then clear chain
                if (pkgChain.First.Value.Dependency == "")
                {
                    orderedPkgs.AddRange(pkgChain);

                    foreach (Package p in pkgChain)
                    {
                        unorderedPkgs.Remove(p);
                    }

                    pkgChain.Clear();
                }
                //  Else if the dependency for the first Package in chain is already in ordered Package list,
                //      Add entire chain to ordered Packages list and remove from unordered Packages list, then clear chain
                else if (FindPackageInListByName(orderedPkgs, pkgChain.First.Value.Dependency) != null)
                {
                    orderedPkgs.AddRange(pkgChain);

                    foreach (Package p in pkgChain)
                    {
                        unorderedPkgs.Remove(p);
                    }

                    pkgChain.Clear();
                }
                // Else, add dependency Package for first Package in pkgChain to front of pkgChain and loop
                else
                {
                    Package nextPkg = FindPackageInListByName(unorderedPkgs, pkgChain.First.Value.Dependency);

                    // The next Package being null indicates that the dependency Package does not exist and thus can never be satisfied,
                    //      Throws Exception in response
                    if (nextPkg == null)
                    {
                        throw new Exception("Package not found, please add Package " + pkgChain.First.Value.Dependency + " and try again.");
                    }
                    else
                    {
                        pkgChain.AddFirst(nextPkg);
                    }
                }
            }

            pkgList = orderedPkgs;
        }

        private Package FindPackageInListByName(List<Package> list, string name)
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

        private Package FindPackageInListByName(LinkedList<Package> llist, string name)
        {
            foreach (Package p in llist)
            {
                if (p.Name == name)
                {
                    return p;
                }
            }

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
