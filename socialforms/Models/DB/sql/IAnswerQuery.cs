using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace socialforms.Models.DB.sql {
    interface IAnswerQuery {
        // zur DB verbinden
        Task ConnectAsync();
        // Verbindung zur DB beenden
        Task DisconnectAsync();
        bool Insert(Answer a);
        bool delete(int answId);
        Answer getAnswer(int answId);
    }
}
