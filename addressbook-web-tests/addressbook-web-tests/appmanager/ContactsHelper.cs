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
    public class ContactsHelper : HelperBase
    {
        public ContactsHelper(IWebDriver driver) 
            :base(driver)
        {
        }
        public void FillContactData(ContactsData contact)
        {
            driver.FindElement(By.LinkText("add new")).Click();
            ModifyContact(contact);
        }

        public List<ContactsData> GetContactsList()
        {
            new WebDriverWait(driver, TimeSpan.FromSeconds(40)).Until(ExpectedConditions.UrlToBe("http://localhost/addressbook/"));
            new WebDriverWait(driver, TimeSpan.FromSeconds(40)).Until(ExpectedConditions.ElementIsVisible(By.XPath("//input[@id='MassCB']")));
            var lastNames = driver.FindElements(By.XPath("//table[@id='maintable']//td[2]"));
            var firstNames = driver.FindElements(By.XPath("//table[@id='maintable']//td[3]"));
            Assert.AreEqual(lastNames.Count, firstNames.Count);

            List<ContactsData> contacts = new List<ContactsData>();
            for (int i= 0; i < lastNames.Count; i++)
            {
                ContactsData contact = new ContactsData(firstNames[i].Text, lastNames[i].Text);
                contacts.Add(contact);
            }
            return contacts;
        }

        internal void ModifyContact(ContactsData contact)
        {
            ClearAndTypeField(By.Name("firstname"), contact.Firstname);
            ClearAndTypeField(By.Name("middlename"), contact.MiddleName);
            ClearAndTypeField(By.Name("lastname"), contact.LastName);
            /*
            driver.FindElement(By.Name("nickname")).Clear();
            driver.FindElement(By.Name("nickname")).SendKeys("dfghfd");
            driver.FindElement(By.Name("title")).Clear();
            driver.FindElement(By.Name("title")).SendKeys("dfghdgf");
            driver.FindElement(By.Name("company")).Clear();
            driver.FindElement(By.Name("company")).SendKeys("dfghfdgh");
            driver.FindElement(By.Name("address")).Clear();
            driver.FindElement(By.Name("address")).SendKeys("dfghgfd");
            driver.FindElement(By.Name("home")).Clear();
            driver.FindElement(By.Name("home")).SendKeys("dfghfdg");
            driver.FindElement(By.Name("mobile")).Clear();
            driver.FindElement(By.Name("mobile")).SendKeys("dfghfd");
            driver.FindElement(By.Name("work")).Clear();
            driver.FindElement(By.Name("work")).SendKeys("fdghgfd");
            driver.FindElement(By.Name("fax")).Clear();
            driver.FindElement(By.Name("fax")).SendKeys("fdghfd");
            driver.FindElement(By.Name("email")).Clear();
            driver.FindElement(By.Name("email")).SendKeys("dfghd");
            driver.FindElement(By.Name("email2")).Clear();
            driver.FindElement(By.Name("email2")).SendKeys("dfgh");
            driver.FindElement(By.Name("email3")).Clear();
            driver.FindElement(By.Name("email3")).SendKeys("dfgh");
            driver.FindElement(By.Name("homepage")).Clear();
            driver.FindElement(By.Name("homepage")).SendKeys("dfgh");
            driver.FindElement(By.Name("bday")).Click();
            new SelectElement(driver.FindElement(By.Name("bday"))).SelectByText("1");
            driver.FindElement(By.Name("bday")).Click();
            driver.FindElement(By.Name("bmonth")).Click();
            new SelectElement(driver.FindElement(By.Name("bmonth"))).SelectByText("April");
            driver.FindElement(By.Name("bmonth")).Click();
            driver.FindElement(By.Name("byear")).Clear();
            driver.FindElement(By.Name("byear")).SendKeys("1992");
            driver.FindElement(By.Name("aday")).Click();
            new SelectElement(driver.FindElement(By.Name("aday"))).SelectByText("5");
            driver.FindElement(By.Name("aday")).Click();
            driver.FindElement(By.Name("amonth")).Click();
            new SelectElement(driver.FindElement(By.Name("amonth"))).SelectByText("July");
            driver.FindElement(By.Name("amonth")).Click();
            driver.FindElement(By.Name("ayear")).Clear();
            driver.FindElement(By.Name("ayear")).SendKeys("1780");
            driver.FindElement(By.Name("address2")).Clear();
            driver.FindElement(By.Name("address2")).SendKeys("dfghgfdh");
            driver.FindElement(By.Name("phone2")).Clear();
            driver.FindElement(By.Name("phone2")).SendKeys("dfghgfd"); 
            driver.FindElement(By.Name("notes")).Clear();
            driver.FindElement(By.Name("notes")).SendKeys("dfghgfdhfdg");*/
        }

        public void SubmitNewContact()
        {
            driver.FindElement(By.XPath("//input[@name='id']/preceding-sibling::input[@type='submit']")).Click();
            new WebDriverWait(driver, TimeSpan.FromSeconds(40)).Until(ExpectedConditions.UrlToBe("http://localhost/addressbook/"));
        }
        public void UpdateContact()
        {
            driver.FindElement(By.XPath("//input[@value='Update']")).Click();
        }
        public void SelectContact(int i)
        {
            try
            {
                var result = new WebDriverWait(driver, TimeSpan.FromSeconds(40))
                    .Until(driver => driver.FindElement(By.XPath($"(//input[@name='selected[]'])[{i}]")));
                result.Click();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public void EditContact(int i)
        {
            try
            {
                var result = new WebDriverWait(driver, TimeSpan.FromSeconds(40))
                    .Until(driver => driver.FindElement(By.XPath($"//img[@title='Edit'][{i}]")));
                result.Click();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public void DeleteContact(bool wait=true)
        {
            driver.FindElement(By.XPath("//input[@value='Delete']")).Click();

            //ASK !!!! How can I get app.baseUrl in this class?
            //ASK how to add '&&' into .Until (or chain them)?
            if (wait)
            {
                //Waiting page comes back to HomePage
                var waitBaseUrl = new WebDriverWait(driver, TimeSpan.FromSeconds(40))
                    .Until(ExpectedConditions.UrlToBe("http://localhost/addressbook/"));
                var waitSelectAll = new WebDriverWait(driver, TimeSpan.FromSeconds(40))
                    .Until(ExpectedConditions.ElementIsVisible(By.XPath("//input[@id='MassCB']"))); 
            }
        }
    }
}
