using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace socialforms.Models.DB.sql {
    interface IQuestionQuery {
        String getText(int qstId);
        int getType(int qstId);
        int getForm(int qstId);

        

    }
}
