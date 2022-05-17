using Xunit;
using Amazon.Lambda.Core;
using Amazon.Lambda.TestUtilities;
using System;

namespace WordleEngine.Tests {

    public class FunctionTest
    {
        [Fact]
        public void TestToLowerFunction()
        {
            // Invoke the lambda function and confirm the string was lower cased.
            var game = new PlayWordle();
            var context = new TestLambdaContext();
            var upperCase = game.Answer("HELLO", context);

            Assert.Equal("hello", upperCase);
        }

        [Fact]
        public void TestLegalWord()
        {
            // Invoke the lambda function and confirm the string was upper cased.
            var game = new PlayWordle();
            var context = new TestLambdaContext();
            var legalWord = game.Answer("world", context);

            Assert.Equal("world", legalWord);
        }

        [Fact]
        public void TestIllegalWord()
        {
            // Invoke the lambda function and confirm the string was upper cased.
            var game = new PlayWordle();
            var context = new TestLambdaContext();

            Assert.Throws<InvalidOperationException>(() => game.Answer("xxxxx", context));
        }

        [Fact]
        public void TestLongWord()
        {
            // Invoke the lambda function and confirm the string was upper cased.
            var game = new PlayWordle();
            var context = new TestLambdaContext();

            Assert.Throws<InvalidOperationException>(() => game.Answer("enormous", context));
        }

        [Fact]
        public void TestShortWord()
        {
            // Invoke the lambda function and confirm the string was upper cased.
            var game = new PlayWordle();
            var context = new TestLambdaContext();

            Assert.Throws<InvalidOperationException>(() => game.Answer("mini", context));
        }
    }

}

