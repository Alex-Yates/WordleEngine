using Amazon.Lambda.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;


// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace WordleEngine{

    public class PlayWordle
    {
        public string Answer(string input, ILambdaContext context)
        {     
            // Cleaning and validating the answer
            var validator = new DataValidator();
            string validatedAnswer;

            try {
                validatedAnswer = validator.ValidateAnswer(input);
            }
            catch {
                string errorMsg = "FAILED: " + input + " is not a legal word.";
                return errorMsg;
            }

            // Set up the game
            List<Guess> guesses = new List<Guess>();
            GameMaster game = new GameMaster(input);
            PlayBot bot = new PlayBot();

            // Play the game
            for (int i = 0; i < 6; i++){
                Word guessedWord = bot.ChooseWord();
                
                string answer = game.GuessWord(guessedWord.GetName());

                List<Fact> facts = bot.GetFacts(guessedWord.GetName(), answer);
                bot.ApplyFacts(facts);

                string thisWordName = guessedWord.GetName();
                int numRemaining = bot.GetNumRemainingPossibleAnswers();
                Guess thisGuess = new Guess(thisWordName, answer, numRemaining);

                guesses.Add(thisGuess);

                if (answer.Equals("GGGGG")) {
                    break;
                }
            }

            // Return the results
            string json = JsonConvert.SerializeObject(guesses);
            Console.WriteLine(json);
            return json;
        }
    }
}