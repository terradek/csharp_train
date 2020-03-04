using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace AddressbookWebTests
{
    public class TestBase
    {
        protected IWebDriver driver;
        private StringBuilder verificationErrors;
        protected string baseUrl;
        protected LoginHelper loginHelper;
        protected GroupsHelper groupsHelper;
        protected NavigatorHelper navigatorHelper;
        protected ContactsHelper contactsHelper;

        #region Setup
        [SetUp]
        public void SetupTest()
        {
            driver = new ChromeDriver();
            baseUrl = "http://localhost/addressbook";
            verificationErrors = new StringBuilder();
            loginHelper = new LoginHelper(driver);
            groupsHelper = new GroupsHelper(driver);
            navigatorHelper = new NavigatorHelper(driver, baseUrl);
            contactsHelper = new ContactsHelper(driver);
        }

        [TearDown]
        public void TeardownTest()
        {
            try
            {
                driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
            Assert.AreEqual("", verificationErrors.ToString());
        }
        #endregion
        
        #region Methods

        #endregion

    }
}
