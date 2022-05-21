using Amazon.Lambda.Core;
using System;
using System.Collections.Generic;
using System.IO;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace WordleEngine{

    public class PlayWordle
    {
        public List<string> Answer(string input, ILambdaContext context)
        {     
            // Cleaning and validating the answer
            var validator = new DataValidator();
            string validatedAnswer;

            try {
                validatedAnswer = validator.ValidateAnswer(input);
            }
            catch {
                string errorMsg = "FAILED: " + input + " is not a legal word.";
                throw new InvalidOperationException(errorMsg);
            }

            // Set up the game
            List<string> guesses = new List<string>();
            GameMaster game = new GameMaster(input);
            PlayBot bot = new PlayBot();

            // Play the game
            for (int i = 0; i < 6; i++){
                string guessedWord = bot.ChooseWord();
                guesses.Add(guessedWord);
                
                if (game.GuessWord(guessedWord)) {
                    // The bot won
                    break;
                }

                List<Fact> facts = game.GetFacts(guessedWord);
                bot.ApplyFacts(facts);
            }

            // Return the results
            return guesses;
        }
    }
}