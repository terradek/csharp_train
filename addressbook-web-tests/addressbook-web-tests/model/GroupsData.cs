using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressbookWebTests
{
    public class GroupsData
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

    }
}
