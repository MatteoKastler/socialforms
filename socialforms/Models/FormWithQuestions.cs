using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace socialforms.Models
{
    public class FormWithQuestions
    {
        public Form SForm { get; set; }
        public List<Question> Questions { get; set; } = new List<Question>();
    }
}
