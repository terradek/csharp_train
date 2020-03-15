using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressbookWebTests
{
    public class GroupsData :IEquatable<GroupsData>, IComparable<GroupsData>
    {

        public GroupsData(string name, string header = "", string footer = "")
        {
            Name = name;
            Header = header;
            Footer = footer;
        }
        public string Name { get; set; }
        public string Header { get; set; }
        public string Footer { get; set; }

        public bool Equals(GroupsData other)
        {
            if (Object.ReferenceEquals(other, null))
                return false;
            if (Object.ReferenceEquals(other, this))
                return true;
            return Name == other.Name;
        }
        public override bool Equals(object obj)
        {
            return Equals(obj as GroupsData);
        }
        public static bool operator == (GroupsData lhs, GroupsData rhs)
        {
 /*           if (Object.ReferenceEquals(lhs, null))
                return false;
            if (Object.ReferenceEquals(rhs, null))
                return false;
            if (Object.ReferenceEquals(lhs, rhs))
                return true;*/
            return lhs.Equals(rhs);
        }
        public static bool operator !=(GroupsData lhs, GroupsData rhs)
        {
            if (Object.ReferenceEquals(lhs, null))
                return false;
            if (Object.ReferenceEquals(rhs, null))
                return false;
            if (Object.ReferenceEquals(lhs, rhs))
                return true;
            return !lhs.Equals(rhs);

        }
        public override int GetHashCode()
        {
           return Name.GetHashCode();
        }

        public override string ToString()
        {
            return Name;
        }

        public int CompareTo(GroupsData other)
        {
            if (Object.ReferenceEquals(other, null))
                return 1;
            if (Object.ReferenceEquals(this, other))
                return 0;
            return Name.CompareTo(other.Name);
        }
    }
}
