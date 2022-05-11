using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace socialforms.Models.DB.sql {
    interface IAnswerQuery {
        // zur DB verbinden
        void Connect();
        // Verbindung zur DB beenden
        void Disconnect();
        bool Insert(Answer a);
        bool delete(int answId);
        Answer getAnswer(int answId);
    }
}
