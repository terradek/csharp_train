using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public IWebDriver Driver { get { return driver; } }

        public void Stop()
        {

            try {
                driver.Quit();
            } catch { /* Ignore errors if unable to close the browser */ }

                Process[] conhostProcesses = Process.GetProcessesByName("conhost");
                Process[] chromeDriverProcesses = Process.GetProcessesByName("chromedriver");
                Process[] geckoDriverProcesses = Process.GetProcessesByName("geckodriver");
                Process[] runtimebrokerProcesses = Process.GetProcessesByName("runtimebroker");

            try {
                foreach (var conhostProcess in conhostProcesses) {
                    //if (conhostProcess.StartInfo.Environment["USERNAME"] == "user")  //shows only the userName of a test process itself - not the conhostProcess user 
                    if (conhostProcess.SessionId !=0)
                    conhostProcess.Kill();
                }        
            } catch { }

            try {
                foreach (var chromeDriverProcess in chromeDriverProcesses)
                    chromeDriverProcess.Kill();
            } catch { }

            try {
                foreach (var geckoDriverProcess in geckoDriverProcesses)
                    geckoDriverProcess.Kill();
            }  catch { /* Ignore errors if unable to close the browser */  }

            try {
                foreach (var runtimebrokerProcess in runtimebrokerProcesses)
                    runtimebrokerProcess.Kill();
            }  catch {   }

        }
    }
}
