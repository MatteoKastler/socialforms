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

        public String FormName { get; set; }

        public DateTime CreateDate { get; set; }

        

    }
}
