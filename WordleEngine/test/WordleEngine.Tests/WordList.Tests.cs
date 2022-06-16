using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace WordleEngine.Tests
{
    public class WordList_Contains_Tests {
        public WordList wordlist = new WordList();

        [Fact]
        public void WordList_Contains_ReturnsFalseForFakeWord() {
            // Checks the total number of words is corect
            Assert.False(wordlist.Contains("illegalword"));
        }

        [Theory]
        [MemberData(nameof(ValidWords), DisableDiscoveryEnumeration = true)]
        public void WordList_Contains_ReturnsTrueForValidWords(string word) {
            Assert.True(wordlist.Contains(word));
        }

        public static TheoryData<string> ValidWords {
            get {
                var data = new TheoryData<string> {
                    { "ABOUT" },  // First word by rank
                    { "AAHED" },  // First word alphabetically, last word by rank
                    { "ZYMIC" },  // Last word alpabetically
                    { "TESTS" }   // Random word from somewhere in the middle
                };
                return data;
            }
        }
    }

    public class WordList_Count_Tests {
        [Fact]
        public void WordList_Count_ReturnsCorrectNumberOfWordsForNewAllowedWordsList() {
            // Checks the total number of words is corect
            WordList wordlist = new WordList();
            Assert.Equal(12947, wordlist.Count());
        }

        [Fact]
        public void WordList_Count_ReturnsCorrectNumberOfWordsForAllowedWordsListWithOneWordMissing() {
            // Checks the total number of words is corect
            WordList reducedlist = new WordList();
            reducedlist.RemoveWord(1);
            Assert.Equal(12946, reducedlist.Count());
        }
    }

    public class WordList_GetLowestRankedWord_Tests {
        [Fact]
        public void WordList_GetLowestRankedWord_ReturnsAboutForNewList() {
            // The first word by rank is ABOUT
            WordList newList = new WordList();
            Word lowestRank = newList.GetLowestRankedWord();
            Assert.True(lowestRank.Name.Equals("ABOUT"));
        }

        [Fact]
        public void WordList_GetLowestRankedWord_ReturnsOtherIfAboutIsRemoved() {
            // The second second word by rank is OTHER
            WordList newList = new WordList();
            newList.RemoveWord(0);
            Word lowestRank = newList.GetLowestRankedWord();
            Assert.True(lowestRank.Name.Equals("OTHER"));
        }
    }

    public class WordList_RemoveWord_Tests {
        [Fact]
        public void AllowedWordsList_RemoveWord_RemovesExactlyOneWord() {
            WordList newList = new WordList();
            int numBefore = newList.Count();
            newList.RemoveWord(0);
            int numAfter = newList.Count();
            Assert.Equal(numAfter, (numBefore - 1));
        }

        [Fact]
        public void WordList_RemoveWord_RemovesTheExpectedWord() {
            WordList newList = new WordList();

            // Just checking the WordList is set up as expected
            if (!newList.Contains("ABOUT")){
                var errorMsg = "ERROR! A newly created AllowedWordList was missing the word ABOUT. Cannot run this test.";
                throw new InvalidOperationException(errorMsg);
            }
            if (!newList.AllowedWords[0].Name.Equals("ABOUT")) {
                string firstWord = newList.AllowedWords[0].Name;
                var errorMsg = "ERROR! A newly created AllowedWordList had the word " + firstWord + " as the top ranked word. Expected ABOUT. Cannot run this test.";
                throw new InvalidOperationException(errorMsg);
            }

            Word about = newList.AllowedWords[0];
            newList.RemoveWord(0);
            Word notAbout = newList.AllowedWords[0];

            Assert.False(about.Equals(notAbout));
        }
    }

    public class WordList_ApplyFacts_Tests {
        [Fact]
        // There are no 5 letter words with 4 Ds, so applying FourD should remove ALL THE WORDS
        public void WordList_ApplyFacts_4xDExistRuleShouldRemoveAllWords() {
            WordList wordList = new WordList();
            List<Fact> factList = new List<Fact> { FourD };
            wordList.ApplyFacts(factList);
            Assert.True(wordList.Count() == 0);
        }

        // Iterating through a long list of test cases to to verify whether given Facts correctly remove/leave Words in a WordList.
        [Theory]
        [MemberData(nameof(WordList_ApplyFacts_TestData), DisableDiscoveryEnumeration = true)]
        public void WordList_ApplyFacts_RemovesTheCorrectWords(Fact fact, string word, bool expected) {
            Assert.Equal(expected, ApplyingFactRemovesWord(fact, word));
        }

        // Helper method to simplify WordList_ApplyFacts_RemovesTheCorrectWords
        private bool ApplyingFactRemovesWord(Fact fact, string word) {
            WordList wordList = new WordList();
            List<Fact> factList = new List<Fact> { fact };
            wordList.ApplyFacts(factList);
            return !wordList.Contains(word);
        }

        // Declaring a few facts up front for simplicity and to reduce repetition/human error
        private static readonly Fact ZeroA = new Fact('A', false, -1, 0);
        private static readonly Fact OneA = new Fact('A', true, -1, 1);
        private static readonly Fact TwoD = new Fact('D', true, -1, 2);
        private static readonly Fact ThreeD = new Fact('D', true, -1, 3);
        private static readonly Fact FourD = new Fact('A', true, -1, 4);
        private static readonly Fact AExists = new Fact('A', true, -1, -1);
        private static readonly Fact ADoesNotExists = new Fact('A', false, -1, 0);
        private static readonly Fact AExistsAt0 = new Fact('A', true, 0, -1);
        private static readonly Fact AExistsAt1 = new Fact('A', true, 1, -1);
        private static readonly Fact AExistsAt2 = new Fact('A', true, 2, -1);
        private static readonly Fact AExistsAt3 = new Fact('A', true, 3, -1);
        private static readonly Fact AExistsAt4 = new Fact('A', true, 4, -1);
        private static readonly Fact NoAAt0 = new Fact('A', false, 0, -1);
        private static readonly Fact NoAAt1 = new Fact('A', false, 1, -1);
        private static readonly Fact NoAAt2 = new Fact('A', false, 2, -1);
        private static readonly Fact NoAAt3 = new Fact('A', false, 3, -1);
        private static readonly Fact NoAAt4 = new Fact('A', false, 4, -1);

        public static TheoryData<Fact, string, bool> WordList_ApplyFacts_TestData {
            get {
                var data = new TheoryData<Fact, string, bool> {
                    { ZeroA, "HELLO", false },          // Applying ZeroA should not remove HELLO
                    { ZeroA, "ABOUT", true },           // Applying ZeroA should remove ABOUT
                    { OneA, "ABOUT", false },           // Applying OneA should not remove ABOUT
                    { OneA, "SALSA", true },            // Applying OneA should remove SALSA
                    { OneA, "HELLO", true },            // Applying OneA should remove HELLO
                    { TwoD, "ADDER", false },           // Applying TwoD should not remove ADDER
                    { TwoD, "ABOUT", true },            // Applying TwoD should remove ABOUT
                    { TwoD, "SALAD", true },            // Applying TwoD should remove SALAD
                    { TwoD, "ADDED", true },            // Applying TwoD should remove ADDED
                    { ThreeD, "ADDED", false },         // Applying ThreeD should not remove ADDED
                    { ThreeD, "SALAD", true },          // Applying ThreeD should remove SALAD
                    { AExists, "ADDED", false },        // Applying AExists should not remove ADDED
                    { AExists, "DREAM", false },        // Applying AExists should not remove DREAM
                    { AExists, "SALSA", false },        // Applying AExists should not remove SALSA
                    { AExists, "HELLO", true },         // Applying AExists should remove HELLO
                    { ADoesNotExists, "HELLO", false }, // Applying ADoesNotExists should not remove HELLO
                    { ADoesNotExists, "DREAM", true },  // Applying ADoesNotExists should remove ADDED
                    { ADoesNotExists, "SALSA", true },  // Applying ADoesNotExists should remove SALSA
                    { AExistsAt0, "ADDED", false },     // Applying AExistsAt0 should not remove ADDED
                    { AExistsAt0, "DREAM", true },      // Applying AExistsAt0 should remove DREAM
                    { AExistsAt1, "BADGE", false },     // Applying AExistsAt1 should not remove BADGE
                    { AExistsAt1, "DREAM", true },      // Applying AExistsAt1 should remove DREAM
                    { AExistsAt2, "PLATE", false },     // Applying AExistsAt2 should not remove PLATE
                    { AExistsAt2, "DREAM", true },      // Applying AExistsAt2 should remove DREAM
                    { AExistsAt3, "DREAM", false },     // Applying AExistsAt3 should not remove DREAM
                    { AExistsAt3, "PLATE", true },      // Applying AExistsAt3 should remove PLATE
                    { AExistsAt4, "SALSA", false },     // Applying AExistsAt4 should not remove SALSA
                    { AExistsAt4, "DREAM", true },      // Applying AExistsAt4 should remove DREAM
                    { NoAAt0, "DREAM", false },         // Applying NoAAt0 should not remove DREAM
                    { NoAAt0, "ADDED", true },          // Applying NoAAt0 should remove ADDED
                    { NoAAt1, "DREAM", false },         // Applying NoAAt1 should not remove DREAM
                    { NoAAt1, "BADGE", true },          // Applying NoAAt1 should remove BADGE
                    { NoAAt2, "DREAM", false },         // Applying NoAAt2 should not remove DREAM
                    { NoAAt2, "PLATE", true },          // Applying NoAAt2 should remove PLATE
                    { NoAAt3, "PLATE", false },         // Applying NoAAt3 should not remove PLATE
                    { NoAAt3, "DREAM", true },          // Applying NoAAt3 should remove DREAM
                    { NoAAt4, "DREAM", false },         // Applying NoAAt4 should not remove DREAM
                    { NoAAt4, "SALSA", true }           // Applying NoAAt4 should remove SALSA
                };
                return data;
            }
        }
    }
}
