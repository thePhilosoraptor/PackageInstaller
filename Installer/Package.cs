using System;
using System.Collections.Generic;

namespace Installer
{
    /*
     * Package class
     *      readonly property: Name (string)
     *      readonly property: Dependency (string)
     *      
     * Contains methods for creating and printing a Package with at most a single dependency
     */

    class Package
    {
        public string Name { get; }
        public string Dependency { get; }

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

        public override string ToString()
        {
            return "Package name: " + Name + "\n\tDepencency: " + Dependency;
        }
    }
}
