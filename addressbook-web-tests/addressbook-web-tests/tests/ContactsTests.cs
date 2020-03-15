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
            ContactsData contact = new ContactsData("dsfgh", "dfghdf", "dfghdgf");
            app.Contacts.FillContactData(contact);
            app.Contacts.SubmitNewContact();
            app.Navigator.GoToHome();
            
            List<ContactsData> newContacts = app.Contacts.GetContactsList();
            Assert.AreEqual(oldContacts.Count + 1, newContacts.Count);

            oldContacts.Add(contact);
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);

            app.Auth.Logout();
        }

        [Test]
        public void ContactEditingTest()
        {
            app.Navigator.GoToBaseUrl();
            app.Auth.Login(new AccountData("admin", "secret"));
            
            if (!app.Contacts.IsContactOrGroupPresent())
            {
                ContactsData contact = new ContactsData("dsfgh", "dfghdf", "dfghdgf");
                app.Contacts.FillContactData(contact);
                app.Contacts.SubmitNewContact();
            }
            List<ContactsData> oldContacts = app.Contacts.GetContactsList();
            
            app.Contacts.EditContact(0);
            ContactsData contactEdited = new ContactsData("32452", "2542", "25436");
            app.Contacts.ModifyContact(contactEdited);
            app.Contacts.UpdateContact(); //click Update button
            app.Navigator.GoToHome();
            
            List<ContactsData> newContacts = app.Contacts.GetContactsList();
            Assert.AreEqual(oldContacts.Count, newContacts.Count);

            oldContacts[0].Firstname = contactEdited.Firstname;
            oldContacts[0].LastName = contactEdited.LastName;
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);

            app.Auth.Logout();
        }

        [Test]
        public void ContactDeletionFromEditPageTest()
        {
            app.Navigator.GoToBaseUrl();
            app.Auth.Login(new AccountData("admin", "secret"));
            ContactsData contact = new ContactsData("dsfgh", "dfghdf", "dfghdgf");

            if (!app.Contacts.IsContactOrGroupPresent())
            {
                app.Contacts.FillContactData(contact);
                app.Contacts.SubmitNewContact();
            }
            List<ContactsData> oldContacts = app.Contacts.GetContactsList();
            app.Contacts.EditContact(0);
            app.Contacts.DeleteContact(); //click Update button
            
            List<ContactsData> newContacts = app.Contacts.GetContactsList();
            Assert.AreEqual(oldContacts.Count - 1, newContacts.Count);

            oldContacts.RemoveAt(0);
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);

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

            oldContacts.RemoveAt(0);
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);

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