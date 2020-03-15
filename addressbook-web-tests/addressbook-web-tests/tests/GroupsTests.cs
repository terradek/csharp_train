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
            GroupsData group = new GroupsData("dcvh", "cvbn", "cvbn");
            app.Groups.FillGroupData(group);
            app.Groups.SubmitNewGroup();

            app.Navigator.GoToGroups();
            List<GroupsData> newGroups = app.Groups.GetGroupsList();
            Assert.AreEqual(oldGroups.Count + 1, newGroups.Count);

            oldGroups.Add(group);
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);
            bool xxx = oldGroups == newGroups; //FALSE???

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
            app.Groups.SelectGroup(0);
            app.Groups.DeleteGroup();

            app.Navigator.GoToGroups();
            List<GroupsData> newGroups = app.Groups.GetGroupsList();
            Assert.AreEqual(oldGroups.Count - 1, newGroups.Count);

            oldGroups.RemoveAt(0);
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);
            
            //ASK cannot implement operator  (==, !=) overriding
            // Assert.IsTrue(oldGroups.Equals(newGroups)); //Always leads to TearDown
            bool x = oldGroups == newGroups; // always FALSE
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

            app.Groups.SelectGroup(0);
            app.Groups.ModifyGroup();
            GroupsData group = new GroupsData("sdgf", "sfdgds", "cvsdfgbn");
            app.Groups.FillGroupData(group);
            app.Groups.UpdateGroup();

            app.Navigator.GoToGroups();
            List<GroupsData> newGroups = app.Groups.GetGroupsList();
            Assert.AreEqual(oldGroups.Count, newGroups.Count);

            oldGroups[0].Name = group.Name;
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);

            app.Navigator.GoToHome();
        }
    }
}
