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
        private List<ContactsData> contactsCache = null;
        
        public List<ContactsData> GetContactsList() {
            /*1) Из метода получения списка контактов можно убрать ожидания открытия нужного адреса и появления элемента на странице. 
            Вместо этого перед получением списка контактов можно сделать переход на страницу контактов (главную страницу приложения).
            2) Метод получения списка контактов можно улучшить. Напимер, алгоритм можно реализовать так:
               1. Получаем список всех строк таблицы контактов (это элементы с именем entry)
               2. В цикле пробегаемся по каждой строке, и с помощью element.FindElements получаем список ячеек (это элементы с тегом td)
               3. Берём текст из ячеек с нужным нам индексом (cells[1].Text)*/
            if (contactsCache == null) {
                contactsCache = new List<ContactsData>();
                //Getting a list of rows:
                var rows = driver.FindElements(By.XPath("//tr[@name='entry']"));
                foreach (var row in rows) {
                    //var lastName = row.Text.Split(" ")[0];
                    //var firstName = row.Text.Split(" ")[1];
                    string lastName = row.FindElements(By.XPath("./td"))[1].Text;
                    string firstName = row.FindElements(By.XPath("./td"))[2].Text;
                    ContactsData contact = new ContactsData(firstName, lastName);
                    contactsCache.Add(contact);
                }; 
            }
            return new List<ContactsData>(contactsCache);
        }

        public void ModifyContact(ContactsData contact)
        {
            ClearAndTypeField(By.Name("firstname"), contact.Firstname);
            ClearAndTypeField(By.Name("middlename"), contact.MiddleName);
            ClearAndTypeField(By.Name("lastname"), contact.LastName);
            ClearAndTypeField(By.Name("address"), contact.Address);
            ClearAndTypeField(By.Name("home"), contact.HomePhone);
            ClearAndTypeField(By.Name("mobile"), contact.MobilePhone);
            ClearAndTypeField(By.Name("work"), contact.WorkPhone);
            
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
            contactsCache = null;
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
                    .Until(driver => driver.FindElement(By.XPath($"(//input[@name='selected[]'])[{i+1}]"))); //i+1 to match 0th element from a list inside an !!!Xpath formula!!!
                result.Click();
            }  catch (Exception ex)   {    throw;     }
        }

        public IList<IWebElement> GetContactRow(int i) 
        {
            //driver.FindElement(By.XPath($"//img[@title='Edit'][{i+1}]"));
            //OR:
            return driver.FindElements(By.Name("entry"))[i]  //"i" starts from 0 since it's an Array-like received so we use instead of "i+1"
                .FindElements(By.TagName("td"));                //the counts for .FindElements starts from 0 - so to find 8th element we need to use -1
        }

        public void GoToContactEditing(int i) 
        {
            try {
/*                var result = new WebDriverWait(driver, TimeSpan.FromSeconds(40))
                    .Until(driver => driver.FindElement(By.XPath($"//img[@title='Edit'][{i+1}]")));
                result.Click();*/
                //OR:
                new WebDriverWait(driver, TimeSpan.FromSeconds(40))
                    .Until(driver => GetContactRow(i)[7]).Click();
            } catch (Exception ex)  { throw; }
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
            contactsCache = null;
        }

        public ContactsData GetContactDataFromEditForm() 
        {
            string firstName = driver.FindElement(By.Name("firstname")).GetAttribute("value");
            string lastName = driver.FindElement(By.Name("lastname")).GetAttribute("value");
            string middleName = driver.FindElement(By.Name("middlename")).GetAttribute("value");

            string address = driver.FindElement(By.Name("address")).GetAttribute("value");
            
            string homePhone = driver.FindElement(By.Name("home")).GetAttribute("value");
            string mobilePhone = driver.FindElement(By.Name("mobile")).GetAttribute("value");
            string workPhone = driver.FindElement(By.Name("work")).GetAttribute("value");

            return new ContactsData(firstName, lastName, middleName) {
                Address = address,
                HomePhone = homePhone,
                MobilePhone = mobilePhone,
                WorkPhone = workPhone
            };
        }

        public ContactsData GetContactDataFromHomePage(int i) 
        {
            IList<IWebElement> cells = GetContactRow(i);
            string lastName = cells[1].Text;
            string firstName = cells[2].Text;
            string address = cells[3].Text;
            string phones = cells[5].Text;

            return new ContactsData(firstName, lastName) {
                Address = address,
                AllPhones = phones
            };
        }
    }
}
