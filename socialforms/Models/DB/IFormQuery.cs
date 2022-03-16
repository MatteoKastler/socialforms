using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace socialforms.Models.DB
{
    interface IFormQuery
    {
        bool Insert(Form form);
        bool Delete(int formId);
        String getName(int formId);
        List<Question> GetQuestions();
        int cntQuestions(int formId);
        int cntUsersAnswered(); //get amount of users that answered the form
        DateTime getCreateDate();
        bool Update(int qstId, Question NewQuestionData);
       
    }
}

