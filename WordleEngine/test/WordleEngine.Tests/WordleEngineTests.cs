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
            Fact notE = new Fact('e', false, -1);
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
            Fact yesE = new Fact('e', true, -1);
            List<Fact> yesEFactList = new List<Fact>();
            yesEFactList.Add(yesE);
            eWordlist.ApplyFacts(yesEFactList);
            Assert.True(eWordlist.Contains("hello"));
        }

        [Fact]
        public void ApplyFactsCorrectlyRemovesWordsThatContainHInPos0()
        {
            // Ensures hello is removed if a fact says no E in position 0
            AllowedWordsList notH1Wordlist = new AllowedWordsList();
            Fact notH1 = new Fact('h', false, 0);
            List<Fact> noH1FactList = new List<Fact>();
            noH1FactList.Add(notH1);
            notH1Wordlist.ApplyFacts(noH1FactList);
            Assert.False(notH1Wordlist.Contains("hello"));
        }

        [Fact]
        public void ApplyFactsCorrectlyLeavesWordsThatContainHInPos0()
        {
            // Ensures hello is not removed if a fact says E in position 0
            AllowedWordsList h1Wordlist = new AllowedWordsList();
            Fact yesH1 = new Fact('h', true, 0);
            List<Fact> yesH1FactList = new List<Fact>();
            yesH1FactList.Add(yesH1);
            h1Wordlist.ApplyFacts(yesH1FactList);
            Assert.True(h1Wordlist.Contains("hello"));
        }

        [Fact]
        public void ApplyFactsCorrectlyRemovesWordsThatContainEInPos1()
        {
            // Ensures hello is removed if a fact says no E in position 1
            AllowedWordsList notE2Wordlist = new AllowedWordsList();
            Fact notE2 = new Fact('e', false, 1);
            List<Fact> noE2FactList = new List<Fact>();
            noE2FactList.Add(notE2);
            notE2Wordlist.ApplyFacts(noE2FactList);
            Assert.False(notE2Wordlist.Contains("hello"));
        }

        [Fact]
        public void ApplyFactsCorrectlyLeavesWordsThatContainEInPos1()
        {
            // Ensures hello is not removed if a fact says E in position 1
            AllowedWordsList e2Wordlist = new AllowedWordsList();
            Fact yesE2 = new Fact('e', true, 1);
            List<Fact> yesE2FactList = new List<Fact>();
            yesE2FactList.Add(yesE2);
            e2Wordlist.ApplyFacts(yesE2FactList);
            Assert.True(e2Wordlist.Contains("hello"));
        }

        [Fact]
        public void ApplyFactsCorrectlyRemovesWordsThatContainLInPos2()
        {
            // Ensures hello is removed if a fact says no E in position 2
            AllowedWordsList notL3Wordlist = new AllowedWordsList();
            Fact notL3 = new Fact('l', false, 2);
            List<Fact> noL3FactList = new List<Fact>();
            noL3FactList.Add(notL3);
            notL3Wordlist.ApplyFacts(noL3FactList);
            Assert.False(notL3Wordlist.Contains("hello"));
        }

        [Fact]
        public void ApplyFactsCorrectlyLeavesWordsThatContainLInPos2()
        {
            // Ensures hello is not removed if a fact says E in position 2
            AllowedWordsList l3Wordlist = new AllowedWordsList();
            Fact yesL3 = new Fact('l', true, 2);
            List<Fact> yesL3FactList = new List<Fact>();
            yesL3FactList.Add(yesL3);
            l3Wordlist.ApplyFacts(yesL3FactList);
            Assert.True(l3Wordlist.Contains("hello"));
        }

        [Fact]
        public void ApplyFactsCorrectlyRemovesWordsThatContainLInPos3()
        {
            // Ensures hello is removed if a fact says no E in position 3
            AllowedWordsList notL4Wordlist = new AllowedWordsList();
            Fact notL4 = new Fact('l', false, 3);
            List<Fact> noL4FactList = new List<Fact>();
            noL4FactList.Add(notL4);
            notL4Wordlist.ApplyFacts(noL4FactList);
            Assert.False(notL4Wordlist.Contains("hello"));
        }

        [Fact]
        public void ApplyFactsCorrectlyLeavesWordsThatContainLInPos3()
        {
            // Ensures hello is not removed if a fact says E in position 3
            AllowedWordsList l4Wordlist = new AllowedWordsList();
            Fact yesL4 = new Fact('l', true, 3);
            List<Fact> yesL4FactList = new List<Fact>();
            yesL4FactList.Add(yesL4);
            l4Wordlist.ApplyFacts(yesL4FactList);
            Assert.True(l4Wordlist.Contains("hello"));
        }

        [Fact]
        public void ApplyFactsCorrectlyRemovesWordsThatContainOInPos4()
        {
            // Ensures hello is removed if a fact says no E in position 4
            AllowedWordsList notO5Wordlist = new AllowedWordsList();
            Fact notO5 = new Fact('o', false, 4);
            List<Fact> noO5FactList = new List<Fact>();
            noO5FactList.Add(notO5);
            notO5Wordlist.ApplyFacts(noO5FactList);
            Assert.False(notO5Wordlist.Contains("hello"));
        }

        [Fact]
        public void ApplyFactsCorrectlyLeavesWordsThatContainOInPos4()
        {
            // Ensures hello is not removed if a fact says E in position 4
            AllowedWordsList o5Wordlist = new AllowedWordsList();
            Fact yesO5 = new Fact('o', true, 4);
            List<Fact> yesO5FactList = new List<Fact>();
            yesO5FactList.Add(yesO5);
            o5Wordlist.ApplyFacts(yesO5FactList);
            Assert.True(o5Wordlist.Contains("hello"));
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
        public void TestPositionMinusTwoFails() {
            // Positions -1 for a Fact should not be allowed
            Assert.Throws<InvalidOperationException>(() => new Fact('a',true,-2));
        }

        [Fact]
        public void TestPositionMinusOnePasses() {
            // Positions -1 for a Fact should work
            int pos = -1;
            Fact f = new Fact('a', true, pos);
            Assert.Equal(f.GetPosition(), pos);
        }

        [Fact]
        public void TestPositionZeroPasses() {
            // Positions 0 for a Fact should work
            int pos = 0;
            Fact f = new Fact('a', true, pos);
            Assert.Equal(f.GetPosition(), pos);
        }

        [Fact]
        public void TestPositionFourPasses() {
            // Positions 4 for a Fact should work
            int pos = 4;
            Fact f = new Fact('a', true, pos);
            Assert.Equal(f.GetPosition(), pos);
        }

        [Fact]
        public void TestPositionFiveFails()
        {
            // Positions 6 for a Fact should not be allowed
            Assert.Throws<InvalidOperationException>(() => new Fact('a', true, 5));
        }
    }

}

