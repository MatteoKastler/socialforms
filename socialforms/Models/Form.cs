using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace socialforms.Models
{
    public class Form
    {
        private int _formId;

        public int FormId
        {
            get { return this._formId; }
            set
            {
                if(value >= 0)
                {
                    this._formId = value;
                }
            }
        }

        private int _userId;

        public int UserId
        {
            get { return this._userId; }
            set
            {
                if (value >= 0)
                {
                    this._userId = value;
                }
            }
        }

        public int answers { get; set; }
        public int questions { get; set; }

        public String FormName { get; set; }

        public DateTime CreateDate { get; set; }

        public static explicit operator Form(string v)
        {
            throw new NotImplementedException();
        }
        public String toString() {
            return this.FormName + " " + this.UserId + " " + this.CreateDate;

    }
    }
    
}

