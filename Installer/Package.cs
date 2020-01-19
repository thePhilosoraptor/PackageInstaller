using System;
using System.Collections.Generic;

namespace Installer
{
    /*
     * Package class
     * Contains methods for creating, comparing, and printing a Package with at most a single dependency
     */

    class Package : IEquatable<Package>
    {
        public string Name { get; private set; }
        public string Dependency { get; private set; }

        public Package(string pkgName)
        {
            Name = pkgName;
            Dependency = "";
        }

        public Package(string pkgName, string pkgDependency)
        {
            Name = pkgName;
            Dependency = pkgDependency;
        }

        public void UpdateDependency(string newDependency)
        {
            Dependency = newDependency;
        }

        public override bool Equals(Object obj)
        {

            return Equals(obj as Package);
        }

        /*
         * Compares Packages based solely on Package Name
         */
        public bool Equals(Package otherPkg)
        {
            return otherPkg != null && Name.Equals(otherPkg.Name);
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public override string ToString()
        {
            return "Package name: " + Name + "\n\tDepencency: " + Dependency;
        }
    }
}
