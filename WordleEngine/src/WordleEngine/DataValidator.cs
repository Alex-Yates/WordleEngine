using System;
using System.Collections.Generic;
using System.Text;

namespace WordleEngine{

    public class DataValidator
    {
        private WordList AllLegalWords { get; set; }

        public DataValidator() {
            AllLegalWords = new WordList();
        }

        public string ValidateAnswer(string input)
        {
            input = input.ToUpper();
            if (AllLegalWords.Contains(input)) {
                return input;
            }
            else {
                string errorMsg = "ERROR: \"" + input + "\" is not a legal word";
                throw new InvalidOperationException(errorMsg);
            }
        }
    }
}

