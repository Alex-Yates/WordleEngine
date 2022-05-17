using System;
using System.Collections.Generic;
using System.Text;

public class DataValidator {

    public string Answer { get; set; }
    public List<string> LegalWords { get; }

    public DataValidator(string answer, List<string> legalWords) {
        this.Answer = answer;
        this.LegalWords = legalWords;          
    }

    public string ValidateAnswer(){
        this.Answer = this.Answer.ToLower();
        if (!LegalWords.Contains(Answer)) {
            throw new InvalidOperationException("ERROR: Not a legal word");
        }   
        return Answer;
    }
}


