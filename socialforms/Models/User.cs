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

        public string Email { get; set; }

        public Gender Gender { get; set; }

        public String UserDescription { get; set; }

   
        public User(int id, string username, string password, DateTime birthdate, string email, Gender gender, string userDescription)
        {
            PersonId = id;
            Username = username;
            Password = password;
            Birthdate = birthdate;
            Email = email;
            Gender = gender;
            UserDescription = userDescription;

        }

        public User()
        {
        }

        public override string ToString()
        {
            return PersonId + "\n" + Username + "\n" + Password + "\n" + Birthdate.ToLongDateString() + Email + "\n" + Gender + "\n" + UserDescription;
        }


    }
}
