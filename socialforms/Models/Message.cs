using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace socialforms.Models
{
    public class Message
    {
        public String Header { get; set; }
        public String MessageText { get; set; }
        public string Solution { get; set; }

        public Message() : this("", "", "") { }
        public Message(string header, string message)
            : this(header, message, "") { }

        public Message(string header, string message, string solution)
        {
            this.Header = header;
            this.MessageText = message;
            this.Solution = solution;
        }
    }
}
