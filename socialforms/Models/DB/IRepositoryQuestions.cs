using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace socialforms.Models.DB
{
    interface IRepositoryQuestions
    {
        // zur DB verbinden
        void Connect();
        // Verbindung zur DB beenden
        void Disconnect();

        bool Insert(Question question);
        bool Delete(int qstId);
        User GetQuestion(int qstId);
        List<Question> GetAllQuestions();
        int cntQuestions(int formId);
        bool Update(int qstId, Question NewQuestionData);
        int cntAnswers(int qstId);
        //weitere Methoden
    }
}

