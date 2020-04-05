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
            
            app.Navigator.GoToBaseUrl();
            List<ContactsData> oldContacts = app.Contacts.GetContactsList();
            ContactsData contact = new ContactsData("dsfgh1", "dfghdf1", "dfghdgf1") { Address = "asdsad", HomePhone = "+9(34)66-77-88", MobilePhone = "+3(070)33-55-65", WorkPhone = "+38(777)77-33-55"};
            app.Contacts.FillContactData(contact);
            app.Contacts.SubmitNewContact();
            app.Navigator.GoToHome();
            
            app.Navigator.GoToBaseUrl();
            List<ContactsData> newContacts = app.Contacts.GetContactsList();
            Assert.AreEqual(oldContacts.Count + 1, newContacts.Count);

            app.Contacts.GoToContactEditing(newContacts.Count - 1);
            var contactFromForm = app.Contacts.GetContactDataFromEditForm();
            
            app.Navigator.GoToHome();
            var contactFromHomePage = app.Contacts.GetContactDataFromHomePage(newContacts.Count - 1);

            Assert.AreEqual(contactFromForm, contactFromHomePage);
            Assert.AreEqual(contactFromForm.Address, contactFromHomePage.Address);
            Assert.AreEqual(contactFromForm.AllPhones, contactFromHomePage.AllPhones);

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
            app.Navigator.GoToBaseUrl();
            List<ContactsData> oldContacts = app.Contacts.GetContactsList();
            
            app.Contacts.GoToContactEditing(0);
            ContactsData contactEdited = new ContactsData("32452", "2542", "25436");
            app.Contacts.ModifyContact(contactEdited);
            app.Contacts.UpdateContact(); //click Update button
            app.Navigator.GoToHome();
            
            app.Navigator.GoToBaseUrl();
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
            app.Navigator.GoToBaseUrl();
            List<ContactsData> oldContacts = app.Contacts.GetContactsList();
            app.Contacts.GoToContactEditing(0);
            app.Contacts.DeleteContact(); //click Update button
            
            app.Navigator.GoToBaseUrl();
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

            app.Navigator.GoToBaseUrl();
            List<ContactsData> oldContacts = app.Contacts.GetContactsList();
            
            app.Contacts.SelectContact(0);
            app.Contacts.DeleteContact(wait:false); //click Delete button
            app.Driver.SwitchTo().Alert().Accept();
            
            app.Navigator.GoToBaseUrl();
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
        public void ContactComparingTest() {
            int accountNumber = 0;
            app.Navigator.GoToBaseUrl();
            app.Auth.Login(new AccountData("admin", "secret"));

            ContactsData contactFromHomePage = app.Contacts.GetContactDataFromHomePage(accountNumber);
            

            app.Contacts.GoToContactEditing(accountNumber);
            ContactsData contactFromForm = app.Contacts.GetContactDataFromEditForm();
            Assert.AreEqual(contactFromForm, contactFromHomePage);
            Assert.AreEqual(contactFromForm.Address, contactFromHomePage.Address);
            Assert.AreEqual(contactFromForm.AllPhones, contactFromHomePage.AllPhones);

            app.Navigator.GoToBaseUrl();
            app.Contacts.GoToContactDetails(accountNumber);
            string contactFromDetails = app.Contacts.GetContactDataFromDetails();
            string firstPart = Regex.Replace(contactFromForm.Firstname + contactFromForm.MiddleName + contactFromForm.LastName + contactFromForm.Address, @"[^\w|\d]", "");
            string secondPart = "H" + Regex.Replace(contactFromForm.HomePhone, @"[^\d]", "") + "M" + Regex.Replace(contactFromForm.MobilePhone, @"[^\d]", "") + "W" + Regex.Replace(contactFromForm.WorkPhone, @"[^\d]", "");
            Assert.AreEqual(contactFromDetails, firstPart + secondPart);

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