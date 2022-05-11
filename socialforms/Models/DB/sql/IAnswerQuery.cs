using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace socialforms.Models.DB.sql {
    interface IAnswerQuery {

        bool Insert(Answer a);
        bool delete(int answId);
        Answer getAnswer(int answId);

    }
}
