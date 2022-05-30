using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace socialforms.Models.DB
{
    interface IFormQuery
    {
        // zur DB verbinden
        bool Connect();
        // Verbindung zur DB beenden
        void Disconnect();
        Form getForm(int formId);
        List<Form> getForms(int userId);
        bool Insert(Form form);
        bool Delete(int formId);
        List<int> GetQuestions(int formId);
        int cntQuestions(int formId);
        int cntUseranswers(int formId); //get amount of users that answered the form
    }
}

