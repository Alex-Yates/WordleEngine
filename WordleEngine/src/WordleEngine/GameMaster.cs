using System;
using System.Collections.Generic;
using System.Text;

namespace WordleEngine
{
    public class GameMaster
    {
        private readonly string SecretWord;
        private readonly DataValidator Validator = new DataValidator();

        // Creates a new GameMaster
        public GameMaster(string secretWord) {
            string validatedSecretWord = Validator.ValidateAnswer(secretWord);
            this.SecretWord = validatedSecretWord;
        }

        // Returns an answer (e.g. "XGXYY") for a given guessed word
        public string GetAnswer(string guessedWord) {
            // Cleaning and validating the answer
            string validatedGuessedWord = Validator.ValidateAnswer(guessedWord);

            // X = Grey (this letter does not exist in SecretWord)
            // Y = Yellow (this letter exists in the SecretWord, but in a different position)
            // G = Green (this letter exists in the SecretWord, in the same position)

            // Assume no matches to start with
            string defaultAnswer = "XXXXX";
            char[] answerArray = defaultAnswer.ToCharArray();

            // Iterating through the guessed word, and flipping Xs to Ys or Gs as appropriate
            for (int i = 0; i < 5; i++) {
                if (this.SecretWord.Contains(validatedGuessedWord[i])){
                    answerArray[i] = 'Y';
                }
                if (this.SecretWord[i].Equals(validatedGuessedWord[i])){
                    answerArray[i] = 'G';
                }
            }

            // Converting char[] back to string and returning result
            string actualAnswer = new string(answerArray);
            return actualAnswer;
        }
    }
}
