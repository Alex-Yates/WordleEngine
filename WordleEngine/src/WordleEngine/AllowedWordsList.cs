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
                // Position must either be 0 (position irrelavent) or between 1 and 5 (specific position known)
                if ((fact.Position > 5) || (fact.Position < 0)){
                    string errorMsg = "Error!: Fact must have position between -1 and 4";
                    throw new InvalidOperationException(errorMsg);
                }

                DataRow[] rowsToKeep = new DataRow[0];

                // Case: where the letter exists but the position is irrelevant
                if ((fact.GetPosition() == 0) && (fact.GetExists())) {
                    // keep only the words that contain the letter
                    List<Word> wordsToKeep = AllowedWords.FindAll(i => i.GetName().Contains(fact.GetLetter()));
                    AllowedWords = wordsToKeep;
                }

                // Case: where letter does not exist in any position
                if ((fact.GetPosition() == 0) && !(fact.GetExists())) {
                    // keep only the words that do not contain the letter
                    List<Word> wordsToRemove = AllowedWords.FindAll(i => i.GetName().Contains(fact.GetLetter()));
                    foreach (Word wordToRemove in wordsToRemove){
                        AllowedWords.Remove(wordToRemove);
                    }
                }

                // Case: where letter exists in a specific position
                if ((fact.GetPosition() != 0) && (fact.GetExists())) {
                    // keep only the words that contain the letter in the specific position
                    List<Word> wordsToKeep = AllowedWords.FindAll(i => i.GetName()[fact.GetPosition()].Equals(fact.GetLetter()));
                    AllowedWords = wordsToKeep;
                }

                // Case: where letter does not exist in a specific position
                if ((fact.GetPosition() != 0) && !(fact.GetExists())) {
                    // keep only the words do not contain the letter in the specific position
                    List<Word> wordsToRemove = AllowedWords.FindAll(i => i.GetName()[fact.GetPosition()].Equals(fact.GetLetter()));
                    foreach (Word wordToRemove in wordsToRemove) {
                        AllowedWords.Remove(wordToRemove);
                    }
                }
            }
        }

        public string GetTopWord() { 
            return AllowedWords[0].GetName();
        }
    }
}
