using System;
using System.Collections.Generic;
using System.Text;

namespace WordleEngine
{
    public class GameMaster
    {
        private readonly string SecretWord;

        public GameMaster(string secretWord) {
            this.SecretWord = secretWord;
        }

        public List<Fact> GetFacts(string guessedWord) {
            if (guessedWord.Length != 5) {
                var errorMsg = "ERROR! You guessed " + guessedWord + " but guesses must have 5 letters!";
                throw new InvalidOperationException(errorMsg);
            }
            List<Fact> returnFactList = new List<Fact>();

            for (int i = 0; i < 5; i++)
            {
                // Case: letters is in the correct position
                if (guessedWord[i] == SecretWord[i])
                {
                    Fact matchedLetterRule = new Fact(SecretWord[i], true, i);
                    returnFactList.Add(matchedLetterRule);
                }

                // Case: letter is in the word, but not in the correct position
                else if (SecretWord.Contains(guessedWord[i]))
                {
                    // The letter exists in the word, but not sure in what position
                    Fact letterExistsRule = new Fact(guessedWord[i], true, -1);
                    // The letter does not exist in position i
                    Fact letterDoesNotExistInThisPosition = new Fact(guessedWord[i], false, i);

                    returnFactList.Add(letterExistsRule);
                    returnFactList.Add(letterDoesNotExistInThisPosition);
                }

                // Case: letter is not in the word at all
                else {
                    Fact matchedLetterRule = new Fact(SecretWord[i], false, -1);
                    returnFactList.Add(matchedLetterRule);
                }
            }

            return returnFactList;
        }

        public bool GuessWord(string guessedWord) {
            return guessedWord.Equals(this.SecretWord);
        }

    }
}
