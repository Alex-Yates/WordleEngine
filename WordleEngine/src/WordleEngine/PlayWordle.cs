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
            var validator = new DataValidator();

            string validatedAnswer = validator.ValidateAnswer(input);

            return validatedAnswer;
        }
    }
}