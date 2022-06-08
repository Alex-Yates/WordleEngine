using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace WordleEngine.Tests
{
    public class WordList_Contains_Tests {
        public WordList wordlist = new WordList();

        [Fact]
        public void WordList_Contains_IncludesFirstWordAlphabetically() {
            // Checks the first word (alphabetically) is in the approved words list
            Assert.True(wordlist.Contains("AAHED"));
        }

        [Fact]
        public void WordList_Contains_IncludesFirstWordByRank() {
            // Checks the first word (alphabetically) is in the approved words list
            Assert.True(wordlist.Contains("ABOUT"));
        }

        [Fact]
        public void WordList_Contains_IncludesLastWordAlphabetically() {
            // Checks the last word (alphabetically) is in the approved words list
            Assert.True(wordlist.Contains("ZYMIC"));
        }

        [Fact]
        public void WordList_Contains_IncludesLastWordByRank() {
            // Checks the last word (alphabetically) is in the approved words list
            Assert.True(wordlist.Contains("AAHED"));
        }

        [Fact]
        public void WordList_Contains_DoesNotContainFakeWord() {
            // Checks the total number of words is corect
            Assert.False(wordlist.Contains("illegalword"));
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
            // The most common word is ABOUT
            WordList newList = new WordList();
            Word lowestRank = newList.GetLowestRankedWord();
            Assert.True(lowestRank.GetName().Equals("ABOUT"));
        }

        [Fact]
        public void WordList_GetLowestRankedWord_ReturnsOtherIfAboutIsRemoved() {
            // The second most common word is OTHER
            WordList newList = new WordList();
            newList.RemoveWord(0);
            Word lowestRank = newList.GetLowestRankedWord();
            Assert.True(lowestRank.GetName().Equals("OTHER"));
        }
    }

    public class WordList_RemoveWord_Tests {
        [Fact]
        public void AllowedWordsList_RemoveWord_RemovesExactlyOneWord() {
            WordList newList = new WordList();
            int numBefore = newList.Count();
            newList.RemoveWord(0);
            int numAfter = newList.Count();
            Assert.Equal(numBefore, (numAfter + 1));
        }

        [Fact]
        public void WordList_RemoveWord_RemovesTheExpectedWord() {
            WordList newList = new WordList();

            // Just checking the WordList is set up as expected
            if (!newList.Contains("ABOUT")){
                var errorMsg = "ERROR! A newly created AllowedWordList was missing the word ABOUT. Cannot run this test.";
                throw new InvalidOperationException(errorMsg);
            }
            if (!newList.AllowedWords[0].GetName().Equals("ABOUT")) {
                string firstWord = newList.AllowedWords[0].GetName();
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
        // Declaring a few facts up front for simplicity and to reduce repetition/human error
        private readonly Fact ZeroA = new Fact('a', false, -1, 0);
        private readonly Fact OneA = new Fact('a', true, -1, 1);
        private readonly Fact TwoA = new Fact('a', true, -1, 2);
        private readonly Fact ThreeD = new Fact('d', true, -1, 3);
        private readonly Fact FourD = new Fact('a', true, -1, 4);
        private readonly Fact AExists = new Fact('a', true, -1, -1);
        private readonly Fact ADoesNotExists = new Fact('a', false, -1, -1);
        private readonly Fact AExistsAt0 = new Fact('a', true, 0, -1);
        private readonly Fact AExistsAt1 = new Fact('a', true, 1, -1);
        private readonly Fact AExistsAt2 = new Fact('a', true, 2, -1);
        private readonly Fact AExistsAt3 = new Fact('a', true, 3, -1);
        private readonly Fact AExistsAt4 = new Fact('a', true, 4, -1);
        private readonly Fact NoAAt0 = new Fact('a', false, 0, -1);
        private readonly Fact NoAAt1 = new Fact('a', false, 1, -1);
        private readonly Fact NoAAt2 = new Fact('a', false, 2, -1);
        private readonly Fact NoAAt3 = new Fact('a', false, 3, -1);
        private readonly Fact NoAAt4 = new Fact('a', false, 4, -1);

        // Helper method to simplify tests below 
        private bool applyingFactRemovesWord(Fact fact, string word) {
            WordList wordList = new WordList();
            List<Fact> factList = new List<Fact> { fact };
            wordList.ApplyFacts(factList);
            return !wordList.Contains(word);
        }

        [Fact]
        // Applying ZeroA should not remove HELLO
        public void WordList_ApplyFacts_0xAExistRuleShouldNotRemoveHello() {        
            Assert.False(applyingFactRemovesWord(ZeroA, "HELLO"));
        }

        [Fact]
        // Applying ZeroA should remove ABOUT
        public void WordList_ApplyFacts_0xAExistRuleShouldRemoveAbout() {
            Assert.True(applyingFactRemovesWord(ZeroA, "ABOUT"));
        }

        [Fact]
        // Applying OneA should not remove ABOUT
        public void WordList_ApplyFacts_1xAExistRuleShouldNotRemoveAbout() {
            Assert.False(applyingFactRemovesWord(OneA, "ABOUT"));
        }

        [Fact]
        // Applying OneA should remove HELLO
        public void WordList_ApplyFacts_1xAExistRuleShouldRemoveHello() {
            Assert.True(applyingFactRemovesWord(OneA, "HELLO"));
        }

        [Fact]
        // Applying TwoA should not remove SALAD
        public void WordList_ApplyFacts_2xAExistRuleShouldNotRemoveSalad() {
            Assert.False(applyingFactRemovesWord(TwoA, "SALAD"));
        }

        [Fact]
        // Applying TwoA should remove ABOUT
        public void WordList_ApplyFacts_2xAExistRuleShouldRemoveAbout() {
            Assert.True(applyingFactRemovesWord(TwoA, "ABOUT"));
        }

        [Fact]
        // Applying ThreeD should not remove ADDED
        public void WordList_ApplyFacts_3xDExistRuleShouldNotRemoveAdded() {
            Assert.False(applyingFactRemovesWord(ThreeD, "ADDED"));
        }

        [Fact]
        // Applying ThreeD should remove SALAD
        public void WordList_ApplyFacts_3xDExistRuleShouldRemoveSalad() {
            Assert.True(applyingFactRemovesWord(ThreeD, "SALAD"));
        }

        [Fact]
        // Applying FourD should remove ALL THE WORDS
        public void WordList_ApplyFacts_4xDExistRuleShouldRemoveAllWords() {
            WordList wordList = new WordList();
            List<Fact> factList = new List<Fact> { FourD };
            wordList.ApplyFacts(factList);
            Assert.True(wordList.Count() == 0);
        }

        [Fact]
        // Applying AExists should not remove ADDED
        public void WordList_ApplyFacts_AExistRuleShouldNotRemoveAdded() {
            Assert.False(applyingFactRemovesWord(AExists, "ADDED"));
        }

        [Fact]
        // Applying AExists should not remove DREAM
        public void WordList_ApplyFacts_AExistRuleShouldNotRemoveDream() {
            Assert.False(applyingFactRemovesWord(AExists, "DREAM"));
        }

        [Fact]
        // Applying AExists should remove HELLO
        public void WordList_ApplyFacts_AExistRuleShouldRemoveHello() {
            Assert.True(applyingFactRemovesWord(AExists, "HELLO"));
        }

        [Fact]
        // Applying ADoesNotExists should not remove HELLO
        public void WordList_ApplyFacts_ADoesNotExistsRuleShouldNotRemoveHello() {
            Assert.False(applyingFactRemovesWord(ADoesNotExists, "HELLO"));
        }

        [Fact]
        // Applying ADoesNotExists should remove ADDED
        public void WordList_ApplyFacts_ADoesNotExistsRuleShouldRemoveAdded() {
            Assert.True(applyingFactRemovesWord(ADoesNotExists, "DREAM"));
        }

        [Fact]
        // Applying AExistsAt0 should not remove ADDED
        public void WordList_ApplyFacts_AExistsAt0RuleShouldNotRemoveAdded() {
            Assert.False(applyingFactRemovesWord(AExistsAt0, "ADDED"));
        }

        [Fact]
        // Applying AExistsAt0 should remove DREAM
        public void WordList_ApplyFacts_AExistsAt0RuleShouldRemoveDream() {
            Assert.True(applyingFactRemovesWord(AExistsAt0, "DREAM"));
        }

        [Fact]
        // Applying AExistsAt1 should not remove BADGE
        public void WordList_ApplyFacts_AExistsAt1RuleShouldNotRemoveBadge() {
            Assert.False(applyingFactRemovesWord(AExistsAt1, "BADGE"));
        }

        [Fact]
        // Applying AExistsAt1 should remove DREAM
        public void WordList_ApplyFacts_AExistsAt1RuleShouldRemoveDream() {
            Assert.True(applyingFactRemovesWord(AExistsAt1, "DREAM"));
        }

        [Fact]
        // Applying AExistsAt2 should not remove PLATE
        public void WordList_ApplyFacts_AExistsAt2RuleShouldNotRemovePlate() {
            Assert.False(applyingFactRemovesWord(AExistsAt2, "PLATE"));
        }

        [Fact]
        // Applying AExistsAt2 should remove DREAM
        public void WordList_ApplyFacts_AExistsAt2RuleShouldRemoveDream() {
            Assert.True(applyingFactRemovesWord(AExistsAt2, "DREAM"));
        }

        [Fact]
        // Applying AExistsAt3 should not remove DREAM
        public void WordList_ApplyFacts_AExistsAt3RuleShouldNotRemoveDream() {
            Assert.False(applyingFactRemovesWord(AExistsAt3, "DREAM"));
        }

        [Fact]
        // Applying AExistsAt3 should remove PLATE
        public void WordList_ApplyFacts_AExistsAt3RuleShouldRemovePlate() {
            Assert.True(applyingFactRemovesWord(AExistsAt3, "PLATE"));
        }

        [Fact]
        // Applying AExistsAt4 should not remove SALSA
        public void WordList_ApplyFacts_AExistsAt4RuleShouldNotRemoveSalsa() {
            Assert.False(applyingFactRemovesWord(AExistsAt4, "SALSA"));
        }

        [Fact]
        // Applying AExistsAt4 should remove DREAM
        public void WordList_ApplyFacts_AExistsAt4RuleShouldRemoveDream() {
            Assert.True(applyingFactRemovesWord(AExistsAt4, "DREAM"));
        }

        [Fact]
        // Applying NoAAt0 should not remove DREAM
        public void WordList_ApplyFacts_NoAAt0RuleShouldNotRemoveDream() {
            Assert.False(applyingFactRemovesWord(NoAAt0, "DREAM"));
        }

        [Fact]
        // Applying NoAAt0 should remove ADDED
        public void WordList_ApplyFacts_NoAAt0RuleShouldRemoveAdded() {
            Assert.True(applyingFactRemovesWord(NoAAt0, "ADDED"));
        }

        [Fact]
        // Applying NoAAt1 should not remove DREAM
        public void WordList_ApplyFacts_NoAAt1RuleShouldNotRemoveDream() {
            Assert.False(applyingFactRemovesWord(NoAAt1, "DREAM"));
        }

        [Fact]
        // Applying NoAAt1 should remove BADGE
        public void WordList_ApplyFacts_NoAAt1RuleShouldRemoveAbout() {
            Assert.True(applyingFactRemovesWord(NoAAt1, "BADGE"));
        }

        [Fact]
        // Applying NoAAt2 should not remove DREAM
        public void WordList_ApplyFacts_NoAAt2RuleShouldNotRemoveDream() {
            Assert.False(applyingFactRemovesWord(NoAAt2, "DREAM"));
        }

        [Fact]
        // Applying NoAAt2 should remove PLATE
        public void WordList_ApplyFacts_NoAAt2RuleShouldRemovePlate() {
            Assert.True(applyingFactRemovesWord(NoAAt2, "PLATE"));
        }

        [Fact]
        // Applying NoAAt3 should not remove PLATE
        public void WordList_ApplyFacts_NoAAt3RuleShouldNotRemovePlate() {
            Assert.False(applyingFactRemovesWord(NoAAt3, "PLATE"));
        }

        [Fact]
        // Applying NoAAt3 should remove DREAM
        public void WordList_ApplyFacts_NoAAt3RuleShouldRemoveDream() {
            Assert.True(applyingFactRemovesWord(NoAAt3, "DREAM"));
        }

        [Fact]
        // Applying NoAAt4 should not remove DREAM
        public void WordList_ApplyFacts_NoAAt4RuleShouldNotRemoveDream() {
            Assert.False(applyingFactRemovesWord(NoAAt4, "DREAM"));
        }

        [Fact]
        // Applying NoAAt4 should remove SALSA
        public void WordList_ApplyFacts_NoAAt4RuleShouldRemoveSalsa() {
            Assert.True(applyingFactRemovesWord(NoAAt4, "SALSA"));
        }
    }
}
