﻿using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace addressbook_web_tests
{
    [TestFixture]
    public class ContactsTests : TestBase
    {

        [Test]
        public void ContactCreationTest()
        {
            GoToBaseUrl();
            Login(new AccountData("admin", "secret"));
            FillContactData(new ContactsData("dsfgh", "dfghdf", "dfghdgf"));
            GoToHome();
            Logout();
        }

    }
}
