using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace addressbook_web_tests
{
    class ContactsData
    {
        string firstName,
            middleName,
            lastName;
        /*  nickname,
            title,
            company,
            address,
            secondaryAddress,
            home,
            telephoneHome,
            telephoneMobile,
            telephoneWork,
            telephoneFax,
            email,
            email2,
            email3,
            homepage,
            notes,
            group, 
            photo;*/
        //DateTime birthday, anniversary;
        public ContactsData(string firstName, string middleName, string lastName)
        {
            this.firstName = firstName;
            this.middleName = middleName;
            this.lastName = lastName;
        }
        public string Firstname 
        { 
            get { return firstName; }
            set { firstName = value; } 
        }

        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }

        public string MiddleName
        {
            get { return middleName; }
            set { middleName = value; }
        }
    }


}

