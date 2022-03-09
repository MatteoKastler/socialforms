using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace socialforms.Models
{
    public class Answer
    {
        private int _answerId;

        public int answerId
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

        public int questionId
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

        public int userId
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

        public String textAnswer { get; set; }
        public int choiceAnswer { get; set; }


    }
}
