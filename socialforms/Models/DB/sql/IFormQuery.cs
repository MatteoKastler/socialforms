using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace socialforms.Models.DB
{
    interface IFormQuery
    {
        Form getForm(int formId);
        bool Insert(Form form);
        bool Delete(int formId);
        List<int> GetQuestions(int formId);
        int cntQuestions(int formId);
        int cntUseranswers(int formId); //get amount of users that answered the form
    }
}

