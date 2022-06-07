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

        public Question(String text)
        {
            this.Qtext = text;
        }

        public Question(int qstId, int formId, String qtext, int questionType)
        {
            this.QuestionId = qstId;
            this.FormId = formId;
            this.Qtext = qtext;
            this.QuestionType = questionType;

        }

        public Question()
        {

        }

        public String toString()
        {
            return this.FormId + " " + this.Qtext;

        }



    }
}
