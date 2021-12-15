using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace socialforms.Models
{
    public class User
    {

        private int _personId;

        public int PersonId
        {
            get { return this._personId; }
            set
            {
                if (value >= 0)
                {
                    this._personId = value;
                }
            }
        }

        public string Username { get; set; }

        public String Password { get; set; }
        public DateTime Birthdate { get; set; }

        public string EMail { get; set; }

        public Gender Gender { get; set; }

        public User(int id, string username, string passsword, DateTime birthdate, string email, Gender gender)
        {
            PersonId = id;
            Username = username;
            Password = passsword;
            Birthdate = birthdate;
            EMail = email;
            Gender = gender;
        }

        public User()
        {
        }

        public override string ToString()
        {
            return PersonId + "\n" + Username + "\n" + Password + "\n" + Birthdate.ToLongDateString();
        }


    }
}
