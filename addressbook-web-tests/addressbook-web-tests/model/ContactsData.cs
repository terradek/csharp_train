using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressbookWebTests
{
    public class ContactsData: IEquatable<ContactsData>, IComparable<ContactsData>
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
        public ContactsData(string firstName, string lastName, string middleName = "")
        {
            Firstname = firstName;
            MiddleName = middleName;
            LastName = lastName;
        }
        public string Firstname { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }

        public int CompareTo(ContactsData other)
        {
            if (Object.ReferenceEquals(other, null))
                return 1;
            if (Object.ReferenceEquals(other, this))
                return 0;
            var otherFirstLastName = other.Firstname + other.LastName;
            var thisFirstLastName = Firstname + LastName;
            return thisFirstLastName.CompareTo(otherFirstLastName);
        }

        public bool Equals(ContactsData other)
        {
            if (Object.ReferenceEquals(other, null))
                return false;
            if (Object.ReferenceEquals(this, other))
                return true;
            return (other.Firstname == Firstname && other.LastName == LastName);
        }

        //ASK how to implement GetHashCode() & ToString() in Contacts?
        /*        public override int GetHashCode()
                {
                   return Name.GetHashCode();
                }

                public override string ToString()
                {
                    return Name;
                }*/
    }


}

