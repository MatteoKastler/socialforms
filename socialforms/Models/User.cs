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


    }
}
