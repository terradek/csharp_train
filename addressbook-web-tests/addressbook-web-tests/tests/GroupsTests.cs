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
            app.Navigator.GoToBaseUrl();
            app.Auth.Login(new AccountData("admin", "secret"));
            app.Navigator.GoToGroups();
            app.Groups.CreateNewGroup();
            app.Groups.FillGroupData(new GroupsData("dcvh", "cvbn", "cvbn"));
            app.Navigator.GoToHome();
            app.Auth.Logout();
        }

        [Test]
        public void GroupDelete()
        {
            app.Navigator.GoToBaseUrl();
            app.Auth.Login(new AccountData("admin", "secret"));
            app.Navigator.GoToGroups();
            app.Groups.SelectGroup(1);
            app.Groups.DeleteGroup();
            app.Navigator.GoToHome();
        }
    }
}
