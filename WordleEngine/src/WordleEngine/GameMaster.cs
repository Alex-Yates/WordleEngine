using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

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
            // char[] guessArray = validatedGuessedWord.ToCharArray();

            // X = Grey (this letter does not exist in SecretWord)
            // Y = Yellow (this letter exists in the SecretWord, but in a different position)
            // G = Green (this letter exists in the SecretWord, in the same position)

            // Assume no matches to start with
            string defaultAnswer = "XXXXX";
            char[] answerArray = defaultAnswer.ToCharArray();

            //////////// FIRST PASS - ASSUMES NO DUPLICATE LETTER COMPLEXITY /////////////////////////////////

            // Iterating through the guessed word, and flipping Xs to Ys or Gs as appropriate
            for (int i = 0; i < 5; i++) {
                if (this.SecretWord.Contains(validatedGuessedWord[i])) {
                    answerArray[i] = 'Y';
                }
                if (this.SecretWord[i].Equals(validatedGuessedWord[i])) {
                    answerArray[i] = 'G';
                }
            }

            //////////// SECOND PASS - HANDLES COMPLEXITY WITH DUPLICATE LETTERS /////////////////////////////////

            // creating array of all the duplicated letters in the guess
            var dupeLettersInGuess =
                from n in validatedGuessedWord
                group n by n into nGroup
                where nGroup.Count() > 1
                select nGroup.Key;

            // If any dupe letters exist in SecretWord AND there are more of that letter in Guess than in SecretWord, 
            //   we need to flip some Ys back to Xs. We do not need to worry about Gs because Gs should never
            //   be flipped to Xs.
            foreach (char dupeLetter in dupeLettersInGuess) {
                // There will either be 0, 1 or 2 dupeLetters.
                // For each letter, we'll calculate whether the letter even exists in solution and if we have a surplus
                int numLetterInSecretWord = this.SecretWord.Where(letter => letter.Equals(dupeLetter)).Count();
                int numLetterInGuess = validatedGuessedWord.Where(letter => letter.Equals(dupeLetter)).Count();
                int numSurplusLetters = numLetterInGuess - numLetterInSecretWord;

                // Only need to act if both the letter exists in solution, AND we have a surplus.
                if ((numLetterInSecretWord > 0) && (numSurplusLetters > 0)) {

                    // Flip numSurplusLetters from Y back to X
                    int pos = 4;
                    while (numSurplusLetters > 0) {
                        if (pos < 0) {
                            string currentAnswer = new string(answerArray);
                            string errorMsg = "ERROR in GameMaster.GetAnswer!: Problem with WHILE loop when handling dupe letters. SecretWord was " + this.SecretWord + ". GuessedWord was " + validatedGuessedWord + ". pos was " + pos + ". numSurplusLetters was " + numSurplusLetters + ". dupeLetter was " + dupeLetter + ". currentAnswer was " + currentAnswer + ".";
                            throw new InvalidOperationException(errorMsg);
                        }

                        // Iterating from back so that the first 'Y' is last to be flipped back to an 'X'.
                        //   (Leaving the first of the dupe letters as Y, and the last as X)
                        if ((validatedGuessedWord[pos].Equals(dupeLetter)) && (answerArray[pos].Equals('Y'))) {
                            answerArray[pos] = 'X';
                            numSurplusLetters--;
                        }
                        pos--;
                    }
                }
            }

            // Converting answerArray from char[] back to string and returning result
            string actualAnswer = new string(answerArray);
            return actualAnswer;
        }
    }
}
