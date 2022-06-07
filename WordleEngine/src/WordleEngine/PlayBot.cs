using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace WordleEngine
{
    public class PlayBot {
        private AllowedWordsList AllowedGuesses { get; set; }
        private AllowedWordsList RemainingWords { get; set; }

        public PlayBot() {
            this.AllowedGuesses = new AllowedWordsList();
            this.RemainingWords = new AllowedWordsList();
        }

        public Word ChooseWord() {
            // To do, build the actual engine part here

            // For now, just picks the lowest ranked remaining word
            return RemainingWords.GetLowestRankedWord();
        }

        // To whittle down the potential answers, based on some known facts
        public void ApplyFacts(List<Fact> facts) {
            this.RemainingWords.ApplyFacts(facts);
        }

        // To generate facts from a guessed word and its answer pattern
        public List<Fact> GetFacts(string guessedWord, string answerPattern) {
            AllowedWordsList allowableWords = new AllowedWordsList();
            guessedWord = guessedWord.ToUpper();

            // Validating the guessed word
            if (!allowableWords.Contains(guessedWord)) {
                var errorMsg = "ERROR! " + guessedWord + " is not a legal word!";
                throw new InvalidOperationException(errorMsg);
            }

            // Validating the answer pattern
            if (answerPattern.Length != 5) {
                var errorMsg = "ERROR! " + answerPattern + " is not in the correct format! (Five chars, all X, Y or G.)";
                throw new InvalidOperationException(errorMsg);
            }

            List<Fact> returnFactList = new List<Fact>();

            for (int i = 0; i < 5; i++) {
                char letter = guessedWord[i];

                // Case: G
                if (answerPattern[i].Equals('G')) {
                    Fact matchedLetterRule = new Fact(letter, true, i, -1);

                    // if the fact does not already exist, add it.
                    if (!returnFactList.Exists(i => i.Name.Equals(matchedLetterRule.Name))) {
                        returnFactList.Add(matchedLetterRule);
                    }
                }

                // Case: Y
                else if (answerPattern[i].Equals('Y')) {
                    // The letter exists in the word, but not sure in what position
                    Fact letterExistsRule = new Fact(letter, true, -1, -1);
                    // The letter does not exist in position i
                    Fact letterDoesNotExistInThisPositionRule = new Fact(letter, false, i, -1);

                    // if the facts do not already exist, add them.
                    if (!returnFactList.Exists(i => i.Name.Equals(letterExistsRule.Name))) {
                        returnFactList.Add(letterExistsRule);
                    }
                    if (!returnFactList.Exists(i => i.Name.Equals(letterDoesNotExistInThisPositionRule.Name))) {
                        returnFactList.Add(letterDoesNotExistInThisPositionRule);
                    }
                }

                // Case: X
                else {
                    /*
                        X is tricky.

                        It feels like an X means "this letter does not exist". That's not necessarily true.
                    
                        For example, if the solution is "WORLD", and you guess "HELLO", you would get the answer pattern "XYXGX".
                    
                        The third character of your answer pattern (X) is not telling you that no L's exist. It is instead telling 
                        you that no *more* L's exist.

                        To summarise, X tells us exactly HOW MANY of a given letter exist in the solution. 
                    */
                    int numberOfThisLetterInSolution = 0;
                    for (int j = 0; j < 5; j++) {
                        if ((answerPattern[j].Equals('G')) || (answerPattern[j].Equals('Y'))) {
                            if (guessedWord[j].Equals(letter)) {
                                numberOfThisLetterInSolution++;
                            }
                        }
                    }


                    // The solution has X of this letter
                    Fact solutionHasXCharsOfThisTypeRule = new Fact(letter, true, -1, numberOfThisLetterInSolution);

                    // If there are zero of the letter, flipping "exists" from true to false
                    if (numberOfThisLetterInSolution == 0) {
                        solutionHasXCharsOfThisTypeRule = new Fact(letter, false, -1, 0);
                    }

                    // if the fact does not already exist, add them.
                    if (!returnFactList.Exists(i => i.Name.Equals(solutionHasXCharsOfThisTypeRule.Name))) {
                        returnFactList.Add(solutionHasXCharsOfThisTypeRule);
                    }
                }
            }

            return returnFactList;
        }

        public List<Word> GetTopFiveWords() {
            List<Word> topFiveWords = (from words in this.RemainingWords.AllowedWords
                                      orderby words.GetRank() ascending
                                      select words).Take(5).ToList();
            return topFiveWords;
        }

        public int GetNumRemainingPossibleAnswers() {
            return this.RemainingWords.Count();
        }
    }
}
