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
    public class GroupsHelper : HelperBase
    {
        public GroupsHelper(IWebDriver driver) 
            :base(driver)
        {
        }

        public void SelectGroup(int i)
        {
            driver.FindElement(By.XPath($"(//input[@name='selected[]'])[{i}]")).Click();
        }

        public void DeleteGroup()
        {
            driver.FindElement(By.Name("delete")).Click();
        }
        public void FillGroupData(GroupsData group)
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
            
        }

        public void SubmitNewGroup()
        {
            //Submitting a form
            driver.FindElement(By.Name("submit")).Click();
        }

        public void UpdateGroup()
        {
            //Submitting a form
            driver.FindElement(By.Name("update")).Click();
        }

        internal void ModifyGroup()
        {
            driver.FindElement(By.Name("edit")).Click();
        }

        public void CreateNewGroup()
        {
            //Creating a new group
            driver.FindElement(By.Name("new")).Click();
        }
    }
}
