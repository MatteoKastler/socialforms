using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace socialforms.Models.DB.sql {
    interface IQuestionQuery {

        bool Insert(Question q);
        bool delete(int qstId);
        Question getQuestion(int qstId);

    }
}
