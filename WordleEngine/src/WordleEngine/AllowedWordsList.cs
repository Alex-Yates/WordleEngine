using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace WordleEngine {
    
    public class AllowedWordsList {

        private List<string> AllowedWords { get; set; }

        public AllowedWordsList() {
            var legalWordsFile = File.ReadAllLines(@"wordle-allowed-guesses.txt");
            this.AllowedWords = new List<string>(legalWordsFile);
        }

        public bool Contains(string word) {
            if (AllowedWords.Contains(word)){
                return true;
            }
            else { 
                return false;
            }
        }

        public int Count()
        {
            return AllowedWords.Count();
        }
    }
}
