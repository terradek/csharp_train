using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace AddressbookWebTests
{
    public class NavigatorHelper: HelperBase
    {

        private string baseURL;

        public NavigatorHelper(IWebDriver driver, string baseURL) 
            :base(driver)
        {
            this.baseURL = baseURL;
        }

        public void GoToGroups()
        {
            driver.FindElement(By.LinkText("groups")).Click();
        }



        public void GoToHome()
        {
            //Returning Home
            driver.FindElement(By.LinkText("home")).Click();
        }

        public void GoToBaseUrl()
        {
            driver.Navigate().GoToUrl(baseURL);
        }
    }

}
