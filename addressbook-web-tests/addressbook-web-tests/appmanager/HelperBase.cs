using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

namespace AddressbookWebTests
{
    public class HelperBase
    {
        protected readonly IWebDriver driver;

        public HelperBase(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void ClearAndTypeField(By by, string text)
        {
            if (text !=null)
            {
                driver.FindElement(by).Clear();
                driver.FindElement(by).SendKeys(text); 
            }
        }

        public bool IsContactOrGroupPresent()
        {
            //var random = Guid.NewGuid().ToString();
            return IsElementPresent(By.XPath("//input[@type='checkbox' and @name='selected[]']"))
                && (driver.Url == "http://localhost/addressbook/" || driver.Url == "http://localhost/addressbook/group.php");
        }

        public bool IsElementPresent(By by)
        {
            try
            {
                new WebDriverWait(driver, TimeSpan.FromSeconds(2)).Until(driver => driver.FindElement(by));
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}