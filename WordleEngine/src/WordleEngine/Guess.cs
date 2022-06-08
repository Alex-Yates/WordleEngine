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
        public readonly List<Word> TopPossibleAnswers;

        public Guess(string word, string answer, int remaining, List<Word> topPossible) {         
            this.Word = word;
            this.Answer = answer;
            this.RemainingPossibleAnswers = remaining;
            this.TopPossibleAnswers = topPossible;
        }
    }
}
