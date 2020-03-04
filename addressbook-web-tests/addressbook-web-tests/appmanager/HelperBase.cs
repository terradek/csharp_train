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
    }
}