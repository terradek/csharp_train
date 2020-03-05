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
    public class ApplicationManager
    {
        protected IWebDriver driver;
        protected string baseUrl;

        protected LoginHelper loginHelper;
        protected GroupsHelper groupsHelper;
        protected NavigatorHelper navigatorHelper;
        protected ContactsHelper contactsHelper;

        public ApplicationManager()
        {
            driver = new ChromeDriver();
            baseUrl = "http://localhost/addressbook";

            loginHelper = new LoginHelper(driver);
            groupsHelper = new GroupsHelper(driver);
            navigatorHelper = new NavigatorHelper(driver, baseUrl);
            contactsHelper = new ContactsHelper(driver);
        }

        public LoginHelper Auth { get { return loginHelper; } }
        public GroupsHelper Groups { get { return groupsHelper; } }
        public NavigatorHelper Navigator { get { return navigatorHelper; } }
        public ContactsHelper Contacts { get { return contactsHelper; } }

        public void Stop()
        {
            try
            {
                driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
        }
    }
}
