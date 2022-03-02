using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace socialforms.Models.DB
{
    interface IRepositoryForms
    {
        void Connect();
        // Verbindung zur DB beenden
        void Disconnect();

        bool Insert(Form form);
        bool Delete(int formId);
        Form GetForm(int formId);
        List<Form> GetAllForms();
        bool Update(int formId, Form newFormData);
        int cntQuestions(int formId);

    }
}

