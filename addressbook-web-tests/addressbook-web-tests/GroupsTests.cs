using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace addressbook_web_tests
{
    [TestFixture]
    public class GroupsTests : TestBase
    {

        [Test]
        public void GroupCreationTest()
        {
            GoToBaseUrl();
            Login(new AccountData("admin", "secret"));
            GoToGroups();
            CreateNewGroup();
            FillGroupData(new GroupsData("dcvh", "cvbn", "cvbn"));
            GoToHome();
            Logout();
        }

        [Test]
        public void GroupDelete()
        {
            GoToBaseUrl();
            Login(new AccountData("admin", "secret"));
            GoToGroups();
            SelectGroup(1);
            DeleteGroup();
            GoToHome();
        }
    }
}
