using Xunit;
using Amazon.Lambda.Core;
using Amazon.Lambda.TestUtilities;
using System;

namespace WordleEngine.Tests {

    public class AllowedWordsListTests {
        public AllowedWordsList wordlist = new AllowedWordsList();

        [Fact]
        public void ContainsFirstWord()
        {
            // Checks the first word (alphabetically) is in the approved words list
            Assert.True(wordlist.Contains("aahed"));
        }

        [Fact]
        public void ContainsHello()
        {
            // Checks hello exists
            Assert.True(wordlist.Contains("hello"));
        }

        [Fact]
        public void ContainsWorld()
        {
            // Checks world exists
            Assert.True(wordlist.Contains("world"));
        }

        [Fact]
        public void ContainsLastWord()
        {
            // Checks the last word (alphabetically) is in the approved words list
            Assert.True(wordlist.Contains("zymic"));
        }

        [Fact]
        public void ContainsCorrectNumberOfWords()
        {
            // Checks the total number of words is corect
            Assert.Equal(12947, wordlist.Count());
        }

        [Fact]
        public void DoesNotContainFakeWord()
        {
            // Checks the total number of words is corect
            Assert.False(wordlist.Contains("illegalword"));
        }
    }

    public class DataValidationTests {
        public DataValidator validator = new DataValidator();

        [Fact]
        public void TestToLowerFunction()
        {
            // Validator should lower case all inputs
            Assert.Equal("aahed", validator.ValidateAnswer("AaHed"));
        }

        [Fact]
        public void TestFirstLegalWord()
        {
            // The word "aahed" should be allowed by the validator
            Assert.Equal("aahed", validator.ValidateAnswer("aahed"));
        }

        [Fact]
        public void TestLegalWordHello()
        {
            // The word "hello" should be allowed by the validator
            Assert.Equal("hello", validator.ValidateAnswer("hello"));
        }

        [Fact]
        public void TestLegalWordWorld()
        {
            // The word "world" should be allowed by the validator
            Assert.Equal("world", validator.ValidateAnswer("world"));
        }

        [Fact]
        public void TestLastLegalWord()
        {
            // The word "zymic" should be allowed by the validator
            Assert.Equal("zymic", validator.ValidateAnswer("zymic"));
        }

        [Fact]
        public void TestIllegalWord()
        {
            // Invalid 5 character strings should be rejected
            Assert.Throws<InvalidOperationException>(() => validator.ValidateAnswer("xxxxx"));
        }

        [Fact]
        public void TestLongWord()
        {
            // Words with more than 5 letters should be rejected
            Assert.Throws<InvalidOperationException>(() => validator.ValidateAnswer("enormous"));
        }

        [Fact]
        public void TestShortWord()
        {
            // Words with less than 5 letters should be rejected
            Assert.Throws<InvalidOperationException>(() => validator.ValidateAnswer("mini"));
        }
    }

}

