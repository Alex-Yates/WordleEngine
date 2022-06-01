using System;
using System.Collections.Generic;
using System.IO;
using System.Data;
using System.Text.RegularExpressions;
using System.Linq;

namespace WordleEngine {
    
    public class AllowedWordsList {

        public List<Word> AllowedWords = new List<Word>();

        public AllowedWordsList() {
            var legalWordsFile = @"wordle-allowed-guesses.csv";
            this.AllowedWords = ConvertCSVToAllowedWordsList(legalWordsFile);
        }

        private static List<Word> ConvertCSVToAllowedWordsList(string strFilePath) {
            StreamReader sr = new StreamReader(strFilePath);
            string[] headers = sr.ReadLine().Split(',');
            List<Word> wordList = new List<Word>();

            while (!sr.EndOfStream) {
                string[] row = Regex.Split(sr.ReadLine(), ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");
                string text = row[1];
                int prevalence = int.Parse(row[2]);
                Word word = new Word(text, prevalence);
                wordList.Add(word);
            }

            return wordList;
        }

        public bool Contains(string input) {
            List<Word> matchingWords = AllowedWords.FindAll(i => i.GetName().Equals(input));
            return (matchingWords.Count == 1);
        }

        public int Count()
        {
            return AllowedWords.Count;
        }

        public void ApplyFacts(List<Fact> facts) {
            foreach (Fact fact in facts) {
                // Position must either be -1 (position irrelavent) or between 0 and 4 (specific position known)
                if ((fact.Position > 4) || (fact.Position < -1)){
                    string errorMsg = "Error!: Fact must have position between -1 and 4";
                    throw new InvalidOperationException(errorMsg);
                }

                // Total must either be -1 (unspecified) or between 0 and 5 (impossible to have more than 5)
                if ((fact.Total > 5) || (fact.Total < -1))
                {
                    string errorMsg = "Error!: Fact must have total between -1 and 5";
                    throw new InvalidOperationException(errorMsg);
                }

                int position = fact.Position;
                int total = fact.Total;
                bool exists = fact.Exists;
                char letter = fact.Letter;

                // Case: where the letter exists but the position is irrelevant
                if ((position == -1) && (exists)) {
                    // keep only the words that contain the letter
                    List<Word> wordsToKeep = AllowedWords.FindAll(i => i.GetName().Contains(letter));
                    AllowedWords = wordsToKeep;
                }

                // Case: where letter does not exist in any position
                if ((position == -1) && (!exists)) {
                    // keep only the words that do not contain the letter
                    List<Word> wordsToRemove = AllowedWords.FindAll(i => i.GetName().Contains(letter));
                    foreach (Word wordToRemove in wordsToRemove){
                        AllowedWords.Remove(wordToRemove);
                    }
                }

                // Case: where letter exists in a specific position
                if ((position != -1) && (exists)) {
                    // keep only the words that contain the letter in the specific position
                    List<Word> wordsToKeep = AllowedWords.FindAll(i => i.GetName()[position].Equals(letter));
                    AllowedWords = wordsToKeep;
                }

                // Case: where letter does not exist in a specific position
                if ((position != -1) && !(exists)) {
                    // keep only the words do not contain the letter in the specific position
                    List<Word> wordsToRemove = AllowedWords.FindAll(i => i.GetName()[position].Equals(letter));
                    foreach (Word wordToRemove in wordsToRemove) {
                        AllowedWords.Remove(wordToRemove);
                    }
                }

                // Case: where there is a known total number of a specific letter
                if (total != -1){
                    // keep only the words that have the specified total number of a given letter             
                    List<Word> wordsToKeep = AllowedWords.FindAll(i => (i.GetName().Count(character => character.Equals(letter))) == total);
                    AllowedWords = wordsToKeep;
                }
            }
        }

        public string GetTopWord() { 
            return AllowedWords[0].GetName();
        }
    }
}
