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
    public class ContactsTests : TestBase
    {

        [Test]
        public void ContactCreationTest()
        {
            app.Navigator.GoToBaseUrl();
            app.Auth.Login(new AccountData("admin", "secret"));
            
            List<ContactsData> oldContacts = app.Contacts.GetContactsList();
            app.Contacts.FillContactData(new ContactsData("dsfgh", "dfghdf", "dfghdgf"));
            app.Contacts.SubmitNewContact();
            app.Navigator.GoToHome();
            List<ContactsData> newContacts = app.Contacts.GetContactsList();
            Assert.AreEqual(oldContacts.Count + 1, newContacts.Count);

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
            List<ContactsData> oldContacts = app.Contacts.GetContactsList();
            
            app.Contacts.EditContact(0);
            app.Contacts.ModifyContact(new ContactsData("32452", "2542", "25436"));
            app.Contacts.UpdateContact(); //click Update button
            app.Navigator.GoToHome();
            
            List<ContactsData> newContacts = app.Contacts.GetContactsList();
            Assert.AreEqual(oldContacts.Count, newContacts.Count);
            
            app.Auth.Logout();
        }

        [Test]
        public void ContactDeletionFromEditPageTest()
        {
            app.Navigator.GoToBaseUrl();
            app.Auth.Login(new AccountData("admin", "secret"));

            if (!app.Contacts.IsContactOrGroupPresent())
            {
                app.Contacts.FillContactData(new ContactsData("dsfgh", "dfghdf", "dfghdgf"));
                app.Contacts.SubmitNewContact();
            }
            List<ContactsData> oldContacts = app.Contacts.GetContactsList();
            app.Contacts.EditContact(0);
            app.Contacts.DeleteContact(); //click Update button
            List<ContactsData> newContacts = app.Contacts.GetContactsList();
            Assert.AreEqual(oldContacts.Count - 1, newContacts.Count);
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

            List<ContactsData> oldContacts = app.Contacts.GetContactsList();
            
            app.Contacts.SelectContact(0);
            app.Contacts.DeleteContact(wait:false); //click Delete button
            app.Driver.SwitchTo().Alert().Accept();
            
            List<ContactsData> newContacts = app.Contacts.GetContactsList();
            Assert.AreEqual(oldContacts.Count - 1, newContacts.Count);

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