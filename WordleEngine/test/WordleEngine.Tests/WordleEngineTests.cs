using Xunit;
using Amazon.Lambda.Core;
using Amazon.Lambda.TestUtilities;
using System;
using System.Collections.Generic;

namespace WordleEngine.Tests {

    public class AllowedWordsListTests {
        public AllowedWordsList wordlist = new AllowedWordsList();

        [Fact]
        public void ContainsFirstWord() {
            // Checks the first word (alphabetically) is in the approved words list
            Assert.True(wordlist.Contains("aahed"));
        }

        [Fact]
        public void ContainsHello() {
            // Checks hello exists
            Assert.True(wordlist.Contains("hello"));
        }

        [Fact]
        public void ContainsWorld() {
            // Checks world exists
            Assert.True(wordlist.Contains("world"));
        }

        [Fact]
        public void ContainsLastWord() {
            // Checks the last word (alphabetically) is in the approved words list
            Assert.True(wordlist.Contains("zymic"));
        }

        [Fact]
        public void ContainsCorrectNumberOfWords() {
            // Checks the total number of words is corect
            Assert.Equal(12947, wordlist.Count());
        }

        [Fact]
        public void DoesNotContainFakeWord() {
            // Checks the total number of words is corect
            Assert.False(wordlist.Contains("illegalword"));
        }

        [Fact]
        public void ApplyFactsCorrectlyRemovesWordsThatContainE()
        {
            // Ensures hello is removed if a fact says no E's
            AllowedWordsList notEWordlist = new AllowedWordsList();
            Fact notE = new Fact('e', false, 0);
            List<Fact> noEFactList = new List<Fact>();
            noEFactList.Add(notE);
            notEWordlist.ApplyFacts(noEFactList);
            Assert.False(notEWordlist.Contains("hello"));
        }

        [Fact]
        public void ApplyFactsCorrectlyLeavesWordsThatContainE()
        {
            // Ensures hello is not removed if a fact says E's exist
            AllowedWordsList eWordlist = new AllowedWordsList();
            Fact yesE = new Fact('e', true, 0);
            List<Fact> yesEFactList = new List<Fact>();
            yesEFactList.Add(yesE);
            eWordlist.ApplyFacts(yesEFactList);
            Assert.True(eWordlist.Contains("hello"));
        }

        [Fact]
        public void ApplyFactsCorrectlyRemovesWordsThatContainEInPos2()
        {
            // Ensures hello is removed if a fact says no E in position 2
            AllowedWordsList notE2Wordlist = new AllowedWordsList();
            Fact notE2 = new Fact('e', false, 2);
            List<Fact> noE2FactList = new List<Fact>();
            noE2FactList.Add(notE2);
            notE2Wordlist.ApplyFacts(noE2FactList);
            Assert.False(notE2Wordlist.Contains("hello"));
        }

        [Fact]
        public void ApplyFactsCorrectlyLeavesWordsThatContainEInPos2()
        {
            // Ensures hello is not removed if a fact says E in position 2
            AllowedWordsList e2Wordlist = new AllowedWordsList();
            Fact yesE2 = new Fact('e', true, 2);
            List<Fact> yesE2FactList = new List<Fact>();
            yesE2FactList.Add(yesE2);
            e2Wordlist.ApplyFacts(yesE2FactList);
            Assert.True(e2Wordlist.Contains("hello"));
        }
    }

    public class DataValidationTests {
        public DataValidator validator = new DataValidator();

        [Fact]
        public void TestToLowerFunction() {
            // Validator should lower case all inputs
            Assert.Equal("aahed", validator.ValidateAnswer("AaHed"));
        }

        [Fact]
        public void TestFirstLegalWord() {
            // The word "aahed" should be allowed by the validator
            Assert.Equal("aahed", validator.ValidateAnswer("aahed"));
        }

        [Fact]
        public void TestLegalWordHello() {
            // The word "hello" should be allowed by the validator
            Assert.Equal("hello", validator.ValidateAnswer("hello"));
        }

        [Fact]
        public void TestLegalWordWorld() {
            // The word "world" should be allowed by the validator
            Assert.Equal("world", validator.ValidateAnswer("world"));
        }

        [Fact]
        public void TestLastLegalWord() {
            // The word "zymic" should be allowed by the validator
            Assert.Equal("zymic", validator.ValidateAnswer("zymic"));
        }

        [Fact]
        public void TestIllegalWord() {
            // Invalid 5 character strings should be rejected
            Assert.Throws<InvalidOperationException>(() => validator.ValidateAnswer("xxxxx"));
        }

        [Fact]
        public void TestLongWord() {
            // Words with more than 5 letters should be rejected
            Assert.Throws<InvalidOperationException>(() => validator.ValidateAnswer("enormous"));
        }

        [Fact]
        public void TestShortWord() {
            // Words with less than 5 letters should be rejected
            Assert.Throws<InvalidOperationException>(() => validator.ValidateAnswer("mini"));
        }
    }

    public class FactTests {
        [Fact]
        public void TestPositionMinusOneFails() {
            // Positions -1 for a Fact should not be allowed
            Assert.Throws<InvalidOperationException>(() => new Fact('a',true,-1));
        }

        [Fact]
        public void TestPositionSixFails()
        {
            // Positions 6 for a Fact should not be allowed
            Assert.Throws<InvalidOperationException>(() => new Fact('a', true, 6));
        }
    }

}

