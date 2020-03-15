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
            driver.FindElement(By.XPath($"(//input[@name='selected[]'])[{i+1}]")).Click(); //i+1 to match 0th element from a list
        }

        public void DeleteGroup()
        {
            driver.FindElement(By.Name("delete")).Click();
        }

        public List<GroupsData> GetGroupsList()
        {
            var groups = new List<GroupsData>();
            var elements = driver.FindElements(By.XPath("//span[@class='group']"));

            foreach ( var element in elements)
            {
                GroupsData group = new GroupsData(element.Text);
                groups.Add(group);
            }

            return groups;
                
        }

        public void FillGroupData(GroupsData group)
        {
            ClearAndTypeField(By.Name("group_name"), group.Name);
            ClearAndTypeField(By.Name("group_header"), group.Header);
            ClearAndTypeField(By.Name("group_footer"), group.Footer);
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
