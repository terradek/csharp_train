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
           
            var compare = LastName.CompareTo(other.LastName);
            
            if (compare == 0) 
                return Firstname.CompareTo(other.Firstname);
             else
                return compare;

            /*3) Надо исправить метод CompareTo(). В нём лучше не склеивать фамилию и имя перед сравнением, 
            а сравнить сначала фамилии и если они равны, то сравнить имена и возвратить результ, иначе возвратить результат сравнения фамилий.*/
            //var otherFirstLastName = other.Firstname + other.LastName;
            //var thisFirstLastName = Firstname + LastName;
            //return thisFirstLastName.CompareTo(otherFirstLastName);
        }

        public bool Equals(ContactsData other)
        {
            if (Object.ReferenceEquals(other, null))
                return false;
            if (Object.ReferenceEquals(this, other))
                return true;
            return (other.Firstname == Firstname && other.LastName == LastName);
        }

        public override int GetHashCode() {
            /*4) Методы GetHashCode и ToString описаны верно, в них также можно ещё добавить фамилию контакта (вот в них её можно склеить с именем).*/
            return (Firstname+LastName).GetHashCode();
        }

        public override string ToString() {
            return $"{Firstname} {LastName}";
        }
    }


}

