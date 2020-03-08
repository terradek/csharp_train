using OpenQA.Selenium;

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
    }
}