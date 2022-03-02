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

        public String Name { get; set; }

        public DateTime DateCreated { get; set; }

        public int NumQst { get; set; }


    }
}
