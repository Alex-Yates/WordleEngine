using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace WordleEngine
{
    public class PlayBot
    {
        private AllowedWordsList AllowedGuesses { get; set; }
        private AllowedWordsList RemainingWords { get; set; }

        public PlayBot() {
            this.AllowedGuesses = new AllowedWordsList();
            this.RemainingWords = new AllowedWordsList();
        }

        public string ChooseWord() {
            // For now, just picks the highest ranked remaining word.

            string chosenWord = RemainingWords.GetTopWord();

            // To do, build the actual engine part here
            return chosenWord;
        }

        public void ApplyFacts(List<Fact> facts) {
            this.RemainingWords.ApplyFacts(facts);
        }
    }
}
