using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace AddressbookWebTests
{
    [TestFixture]
    public class GroupsTests : TestBase
    {

        [Test]
        public void GroupCreationTest()
        {
            GoToBaseUrl();
            loginHelper.Login(new AccountData("admin", "secret"));
            GoToGroups();
            groupsHelper.CreateNewGroup();
            groupsHelper.FillGroupData(new GroupsData("dcvh", "cvbn", "cvbn"));
            GoToHome();
            loginHelper.Logout();
        }

        [Test]
        public void GroupDelete()
        {
            GoToBaseUrl();
            loginHelper.Login(new AccountData("admin", "secret"));
            GoToGroups();
            groupsHelper.SelectGroup(1);
            groupsHelper.DeleteGroup();
            GoToHome();
        }
    }
}
