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
    public class ContactsTests : TestBase
    {

        [Test]
        public void ContactCreationTest()
        {
            app.Navigator.GoToBaseUrl();
            app.Auth.Login(new AccountData("admin", "secret"));
            app.Contacts.FillContactData(new ContactsData("dsfgh", "dfghdf", "dfghdgf"));
            app.Contacts.SubmitNewContact();
            app.Navigator.GoToHome();
            app.Auth.Logout();
        }

        [Test]
        public void ContactEditingTest()
        {
            app.Navigator.GoToBaseUrl();
            app.Auth.Login(new AccountData("admin", "secret"));
            if (!app.Contacts.IsContactOrGroupPresent())
            {
                app.Contacts.FillContactData(new ContactsData("dsfgh", "dfghdf", "dfghdgf"));
                app.Contacts.SubmitNewContact();
            }
            app.Contacts.EditContact(1);
            app.Contacts.ModifyContact(new ContactsData("32452", "2542", "25436"));
            app.Contacts.UpdateContact(); //click Update button
            app.Navigator.GoToHome();
            app.Auth.Logout();
        }

        [Test]
        public void ContactDeletionTest()
        {
            app.Navigator.GoToBaseUrl();
            app.Auth.Login(new AccountData("admin", "secret"));

            if (!app.Contacts.IsContactOrGroupPresent())
            {
                app.Contacts.FillContactData(new ContactsData("dsfgh", "dfghdf", "dfghdgf"));
                app.Contacts.SubmitNewContact();
            }
            app.Contacts.EditContact(1);
            app.Contacts.DeleteContact(); //click Update button
            app.Navigator.GoToHome();
            app.Auth.Logout();
        }

        [Test]
        public void ContactDeletionFromHomePageTest()
        {
            app.Navigator.GoToBaseUrl();
            app.Auth.Login(new AccountData("admin", "secret"));

            if (!app.Contacts.IsContactOrGroupPresent())
            {
                app.Contacts.FillContactData(new ContactsData("dsfgh", "dfghdf", "dfghdgf"));
                app.Contacts.SubmitNewContact();
            }
            app.Contacts.SelectContac(1);
            app.Contacts.DeleteContact(); //click Delete button
            app.Driver.SwitchTo().Alert().Accept();
            app.Navigator.GoToHome();
            app.Auth.Logout();
        }

        /*        [Test]
                public void ContactAddingToGroupTest()
                {
                    app.Navigator.GoToBaseUrl();
                    app.Auth.Login(new AccountData("admin", "secret"));
                    app.Contacts.EditContact(1);
                    app.Contacts.DeleteContact(); //click Update button
                    app.Navigator.GoToHome();
                    app.Auth.Logout();
                }*/
    }
}
