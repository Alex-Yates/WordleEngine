using System;
using System.Collections.Generic;
using System.Text;

namespace WordleEngine
{
    public class Guess
    {
        public readonly string Word;
        public readonly string Answer;
        public readonly int RemainingPossibleAnswers;

        public Guess(string word, string answer, int remaining) {         
            this.Word = word;
            this.Answer = answer;
            this.RemainingPossibleAnswers = remaining;
        }
    }
}
