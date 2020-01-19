using System;
using System.Collections.Generic;

namespace Installer
{
    class Package : IEquatable<Package>
    {
        public string Name { get; private set; }
        public List<string> Dependencies { get; private set; }

        public Package(string pkgName, string singleDependency)
        {
            Name = pkgName;
            Dependencies.Add(singleDependency);
        }

        public Package(string pkgName, List<string> pkgDependencies)
        {
            Name = pkgName;
            Dependencies = pkgDependencies;
        }

        public void UpdateDependencies(List<string> newDependencies)
        {
            Dependencies = newDependencies;
        }

        public override bool Equals(Object obj)
        {

            return Equals(obj as Package);
        }

        public bool Equals(Package otherPkg)
        {
            return otherPkg != null && Name.Equals(otherPkg.Name);
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }
}
