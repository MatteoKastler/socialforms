using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace socialforms.Models
{
    public class Question
    {
        private int _questionId;

        public int QuestionId
        {
            get { return this._questionId; }
            set
            {
                if (value >= 0)
                {
                    this._questionId = value;
                }
            }
        }

        private int _formId;

        public int FormId
        {
            get { return this._formId; }
            set
            {
                if (value >= 0)
                {
                    this._formId = value;
                }
            }
        }

        public String Qtext {get; set; }

        public int QuestionType { get; set; }




    }
}
