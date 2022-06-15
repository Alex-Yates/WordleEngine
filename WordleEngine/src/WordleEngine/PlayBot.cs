using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace WordleEngine
{
    public class PlayBot {
        private readonly WordList RemainingWords = new WordList();

        public PlayBot() {
            // no setup required
        }

        public Word ChooseWord() {
            // To do, build the actual engine part here

            /*
             *        :-P
             */

            // For now, just picks the lowest ranked remaining word
            return RemainingWords.GetLowestRankedWord();
        }

        // To whittle down the potential answers, based on some known facts
        public void ApplyFacts(List<Fact> facts) {
            this.RemainingWords.ApplyFacts(facts);
        }

        // To generate facts from a guessed word and its answer pattern
        public List<Fact> GetFacts(string guessedWord, string answerPattern) {
            WordList allowableWords = new WordList();
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

                    // Create the fact, assuming 0 of letter in solution (the most common scenario)
                    Fact solutionHasXCharsOfThisTypeRule = new Fact(letter, false, -1, 0);

                    // If there are more than zero of the letter, flipping "exists" from false to true
                    if (numberOfThisLetterInSolution != 0) {
                        solutionHasXCharsOfThisTypeRule = new Fact(letter, true, -1, numberOfThisLetterInSolution);
                    }

                    // if the fact does not already exist, add them.
                    if (!returnFactList.Exists(i => i.Name.Equals(solutionHasXCharsOfThisTypeRule.Name))) {
                        returnFactList.Add(solutionHasXCharsOfThisTypeRule);
                    }
                }
            }

            return returnFactList;
        }

        // Just for presentation
        public List<Word> GetTopFiveWords() {
            List<Word> topFiveWords = (from words in this.RemainingWords.AllowedWords
                                      orderby words.Rank ascending
                                      select words).Take(5).ToList();
            return topFiveWords;
        }

        // Just for presentation
        public int GetNumRemainingPossibleAnswers() {
            return this.RemainingWords.Count();
        }
    }
}
