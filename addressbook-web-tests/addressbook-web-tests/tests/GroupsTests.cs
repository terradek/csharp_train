using System;
using System.Collections.Generic;
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
            List<GroupsData> oldGroups = app.Groups.GetGroupsList();
            
            app.Groups.CreateNewGroup();
            app.Groups.FillGroupData(new GroupsData("dcvh", "cvbn", "cvbn"));
            app.Groups.SubmitNewGroup();

            app.Navigator.GoToGroups();
            List<GroupsData> newGroups = app.Groups.GetGroupsList();
            Assert.AreEqual(oldGroups.Count + 1, newGroups.Count);

            app.Navigator.GoToHome();
            app.Auth.Logout();
        }

        [Test]
        public void GroupDeletionTest()
        {
            app.Navigator.GoToBaseUrl();
            app.Auth.Login(new AccountData("admin", "secret"));
            app.Navigator.GoToGroups();

            if (!app.Groups.IsContactOrGroupPresent())
            {
                app.Groups.CreateNewGroup();
                app.Groups.FillGroupData(new GroupsData("dcvh", "cvbn", "cvbn"));
                app.Groups.SubmitNewGroup();
                app.Navigator.GoToGroups();
            }

            List<GroupsData> oldGroups = app.Groups.GetGroupsList();
            app.Groups.SelectGroup(1);
            app.Groups.DeleteGroup();

            app.Navigator.GoToGroups();
            List<GroupsData> newGroups = app.Groups.GetGroupsList();
            Assert.AreEqual(oldGroups.Count - 1, newGroups.Count);

            app.Navigator.GoToHome();
        }

        [Test]
        public void GroupEditingTest()
        {
            app.Navigator.GoToBaseUrl();
            app.Auth.Login(new AccountData("admin", "secret"));
            app.Navigator.GoToGroups();

            if (!app.Groups.IsContactOrGroupPresent())
            {
                app.Groups.CreateNewGroup();
                app.Groups.FillGroupData(new GroupsData("dcvh", "cvbn", "cvbn"));
                app.Groups.SubmitNewGroup();
                app.Navigator.GoToGroups();

            }

            List<GroupsData> oldGroups = app.Groups.GetGroupsList();

            app.Groups.SelectGroup(1);
            app.Groups.ModifyGroup();
            app.Groups.FillGroupData(new GroupsData("sdgf", "sfdgds", "cvsdfgbn"));
            app.Groups.UpdateGroup();

            app.Navigator.GoToGroups();
            List<GroupsData> newGroups = app.Groups.GetGroupsList();
            Assert.AreEqual(oldGroups.Count, newGroups.Count);

            app.Navigator.GoToHome();
        }
    }
}
