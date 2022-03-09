using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace socialforms.Models
{
    public class Answer
    {
        private int _answerId;

        public int AnswerId
        {
            get { return this._answerId; }
            set
            {
                if (value >= 0)
                {
                    this._answerId = value;
                }
            }
        }

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

        public String TextAnswer{ get; set; }
        public int ChoiceAnswer { get; set; }


    }
}
