using System;
using System.Collections.Generic;
using System.Text;

namespace WordleEngine
{
    public class GameMaster
    {
        private readonly string SecretWord;

        public GameMaster(string secretWord) {
            AllowedWordsList allowableWords = new AllowedWordsList();
            secretWord = secretWord.ToUpper();
            if (!allowableWords.Contains(secretWord)){
                var errorMsg = "ERROR! " + secretWord + " is not a legal word!";
                throw new InvalidOperationException(errorMsg);
            }
            this.SecretWord = secretWord;
        }

        public string GuessWord(string guessedWord) {
            guessedWord = guessedWord.ToUpper();
            AllowedWordsList allowableWords = new AllowedWordsList();
            if (!allowableWords.Contains(guessedWord)) {
                var errorMsg = "ERROR! " + guessedWord + " is not a legal word!";
                throw new InvalidOperationException(errorMsg);
            }

            string defaultAnswer = "XXXXX";

            char[] answerArray = defaultAnswer.ToCharArray();

            for (int i = 0; i < 5; i++) {
                if (this.SecretWord.Contains(guessedWord[i])){
                    answerArray[i] = 'Y';
                }
                if (this.SecretWord[i].Equals(guessedWord[i]))
                {
                    answerArray[i] = 'G';
                }
            }

            string actualAnswer = new string(answerArray);

            return actualAnswer;
        }
    }
}
