using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace socialforms.Models.DB
{
    interface IRepositoryAnswers
    {
        void Connect();
        // Verbindung zur DB beenden
        void Disconnect();

        bool Insert(Answer answ);
        bool Delete(int ansId);
        Form GetAnswers(int ansId);
        List<Answer> GetAllAnswers();
    }
}
