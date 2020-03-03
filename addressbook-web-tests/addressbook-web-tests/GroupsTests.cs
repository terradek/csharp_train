using System;
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
    public class GroupsTests
    {
        private IWebDriver driver;
        private StringBuilder verificationErrors;
        private string baseURL;

        [SetUp]
        public void SetupTest()
        {
            driver = new ChromeDriver();
            baseURL = "http://localhost/addressbook";
            verificationErrors = new StringBuilder();
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

        [Test]
        public void GroupCreationTest()
        {
            GoToBaseUrl();
            Login(new AccountData("admin", "secret"));
            GoToGroups();
            CreateNewGroup();
            FillGroupData(new GroupsData("dcvh", "cvbn", "cvbn"));
            GoToHome();
            Logout();
        }

        private void Logout()
        {
            driver.FindElement(By.LinkText("Logout")).Click();
        }

        private void GoToGroups()
        {
            driver.FindElement(By.LinkText("groups")).Click();
        }

        private void FillGroupData(GroupsData group)
        {
            //Filling up a form
            driver.FindElement(By.Name("group_name")).Click();
            driver.FindElement(By.Name("group_name")).Clear();
            driver.FindElement(By.Name("group_name")).SendKeys(group.Name);
            driver.FindElement(By.Name("group_header")).Click();
            driver.FindElement(By.Name("group_header")).Clear();
            driver.FindElement(By.Name("group_header")).SendKeys(group.Header);
            driver.FindElement(By.Name("group_footer")).Click();
            driver.FindElement(By.Name("group_footer")).Clear();
            driver.FindElement(By.Name("group_footer")).SendKeys(group.Footer);
            //Submitting a form
            driver.FindElement(By.Name("submit")).Click();
        }

        private void CreateNewGroup()
        {
            //Creating a new group
            driver.FindElement(By.Name("new")).Click();
        }

        private void GoToHome()
        {
            //Returning Home
            driver.FindElement(By.LinkText("home")).Click();
        }

        private void Login(AccountData account)
        {
            driver.FindElement(By.Name("pass")).Click();
            driver.FindElement(By.Name("pass")).Clear();
            driver.FindElement(By.Name("pass")).SendKeys(account.Password);
            driver.FindElement(By.Name("user")).Click();
            driver.FindElement(By.Name("user")).Clear();
            driver.FindElement(By.Name("user")).SendKeys(account.User);
            driver.FindElement(By.XPath("//input[@value='Login']")).Click();
        }

        private void GoToBaseUrl()
        {
            driver.Navigate().GoToUrl(baseURL);
        }


    }
}
