using Amazon.Lambda.Core;
using System;
using System.Collections.Generic;
using System.IO;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace WordleEngine{

    public class PlayWordle
    {
        public string Answer(string input, ILambdaContext context)
        {
            // var legalWords = new List<string> { "hello", "world" };

            var legalWordsFile = File.ReadAllLines(@"wordle-allowed-guesses.txt");
            var legalWords = new List<string>(legalWordsFile);

            var validator = new DataValidator(input, legalWords);

            string validatedAnswer = validator.ValidateAnswer();

            return validatedAnswer;
        }
    }
}