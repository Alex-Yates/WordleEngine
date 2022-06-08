using Amazon.Lambda.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;


// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace WordleEngine{

    public class PlayWordle
    {
        // Creates message: "IDE0060 Remove unused parameter 'context' if it is not part of a shipped public API WordleEngine"
        //   I think I can probably ignore this error since context is required for AWS Lambda functions?
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
                
                string answer = game.GetAnswer(guessedWord.Name);

                List<Fact> facts = bot.GetFacts(guessedWord.Name, answer);
                bot.ApplyFacts(facts);

                string thisWordName = guessedWord.Name;
                int numRemaining = bot.GetNumRemainingPossibleAnswers();
                List<Word> topPossible = bot.GetTopFiveWords();
                Guess thisGuess = new Guess(thisWordName, answer, numRemaining, topPossible);

                guesses.Add(thisGuess);

                if (answer.Equals("GGGGG")) {
                    break;
                }
            }

            // Return the results
            string json = JsonConvert.SerializeObject(guesses);
            string jsonFormatted = JValue.Parse(json).ToString(Formatting.Indented);
            Console.WriteLine(jsonFormatted);
            return json;
        }
    }
}