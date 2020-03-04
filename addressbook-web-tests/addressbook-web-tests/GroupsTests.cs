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
            navigatorHelper.GoToBaseUrl();
            loginHelper.Login(new AccountData("admin", "secret"));
            navigatorHelper.GoToGroups();
            groupsHelper.CreateNewGroup();
            groupsHelper.FillGroupData(new GroupsData("dcvh", "cvbn", "cvbn"));
            navigatorHelper.GoToHome();
            loginHelper.Logout();
        }

        [Test]
        public void GroupDelete()
        {
            navigatorHelper.GoToBaseUrl();
            loginHelper.Login(new AccountData("admin", "secret"));
            navigatorHelper.GoToGroups();
            groupsHelper.SelectGroup(1);
            groupsHelper.DeleteGroup();
            navigatorHelper.GoToHome();
        }
    }
}
