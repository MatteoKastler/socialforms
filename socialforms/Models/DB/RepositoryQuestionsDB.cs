using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace socialforms.Models.DB.sql
{
    public class RepositoryQuestionsDB : IRepositoryQuestions
    {
        public int cntAnswers(int qstId)
        {
            throw new NotImplementedException();
        }

        public int cntQuestions(int formId)
        {
            throw new NotImplementedException();
        }

        public void Connect()
        {
            throw new NotImplementedException();
        }

        public bool Delete(int qstId)
        {
            throw new NotImplementedException();
        }

        public void Disconnect()
        {
            throw new NotImplementedException();
        }

        public List<Question> GetAllQuestions()
        {
            throw new NotImplementedException();
        }

        public User GetQuestion(int qstId)
        {
            throw new NotImplementedException();
        }

        public bool Insert(Question question)
        {
            throw new NotImplementedException();
        }

        public bool Update(int qstId, Question NewQuestionData)
        {
            throw new NotImplementedException();
        }
    }
}
