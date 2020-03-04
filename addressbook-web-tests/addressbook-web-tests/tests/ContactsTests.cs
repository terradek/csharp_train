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
            navigatorHelper.GoToBaseUrl();
            loginHelper.Login(new AccountData("admin", "secret"));
            contactsHelper.FillContactData(new ContactsData("dsfgh", "dfghdf", "dfghdgf"));
            navigatorHelper.GoToHome();
            loginHelper.Logout();
        }

    }
}
