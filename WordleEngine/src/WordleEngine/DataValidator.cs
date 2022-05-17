using System;
using System.Collections.Generic;
using System.Text;

namespace WordleEngine{

    public class DataValidator
    {
        public AllowedWordsList AllLegalWords { get; set; }

        public DataValidator()
        {
            AllLegalWords = new AllowedWordsList();
        }

        public string ValidateAnswer(string input)
        {
            input = input.ToLower();
            if (AllLegalWords.Contains(input)) {
                return input;
            }
            else {              
                throw new InvalidOperationException("ERROR: Not a legal word");
            }
        }
    }
}

