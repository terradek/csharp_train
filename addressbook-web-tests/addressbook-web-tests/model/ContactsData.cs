using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressbookWebTests
{
    public class ContactsData
    {
        //private string
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
            Firstname = firstName;
            MiddleName = middleName;
            LastName = lastName;
        }
        public string Firstname { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }
    }


}

