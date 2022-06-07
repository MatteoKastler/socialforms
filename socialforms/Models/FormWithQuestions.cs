using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace socialforms.Models
{
    public class FormWithQuestions
    {
        public Form SForm { get; set; }
        public List<String> QstList { get; set; } = new List<String>();

        public Question Qtext { get; set; }
    }
}
