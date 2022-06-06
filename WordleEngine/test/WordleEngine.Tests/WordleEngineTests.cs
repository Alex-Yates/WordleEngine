using Xunit;
using Amazon.Lambda.Core;
using Amazon.Lambda.TestUtilities;
using System;
using System.Collections.Generic;

namespace WordleEngine.Tests
{

    public class AllowedWordsListTests
    {
        public AllowedWordsList wordlist = new AllowedWordsList();


        [Fact]
        public void TestsForContainsAndLowestRank()
        {
            // To do, write these tests
            Assert.True(false);
        }

        [Fact]
        public void AllowedWordsList_Contains_IncludesFirstWord()
        {
            // Checks the first word (alphabetically) is in the approved words list
            Assert.True(wordlist.Contains("AAHED"));
        }

        [Fact]
        public void AllowedWordsList_Contains_IncludesHello()
        {
            // Checks hello exists
            Assert.True(wordlist.Contains("HELLO"));
        }

        [Fact]
        public void AllowedWordsList_Contains_IncludesWorld()
        {
            // Checks world exists
            Assert.True(wordlist.Contains("WORLD"));
        }

        [Fact]
        public void AllowedWordsList_Contains_IncludesLastWord()
        {
            // Checks the last word (alphabetically) is in the approved words list
            Assert.True(wordlist.Contains("ZYMIC"));
        }

        [Fact]
        public void AllowedWordsList_Count_ReturnsCorrectNumberOfWordsForNewAllowedWordsList() {
            // Checks the total number of words is corect
            Assert.Equal(12947, wordlist.Count());
        }

        [Fact]
        public void AllowedWordsList_Count_ReturnsCorrectNumberOfWordsForAllowedWordsListWithOneWordMissing() {
            // Checks the total number of words is corect
            AllowedWordsList reducedlist = new AllowedWordsList();
            reducedlist.RemoveWord(1);
            Assert.Equal(12946, reducedlist.Count());
        }

        [Fact]
        public void DoesNotContainFakeWord()
        {
            // Checks the total number of words is corect
            Assert.False(wordlist.Contains("illegalword"));
        }

        [Fact]
        public void AllowedWordsList_ApplyFacts_0xAExistRuleShouldNotRemoveHello()
        {
            // Ensures HELLO is not removed if a fact says no A's
            AllowedWordsList notAWordlist = new AllowedWordsList();
            Fact notA = new Fact('a', false, -1, 0);
            List<Fact> noAFactList = new List<Fact>();
            noAFactList.Add(notA);
            notAWordlist.ApplyFacts(noAFactList);
            Assert.True(notAWordlist.Contains("HELLO"));
        }

        [Fact]
        public void AllowedWordsList_ApplyFacts_0xAExistRuleShouldRemoveAbout()
        {
            // Ensures ABOUT is not removed if a fact says no A's
            AllowedWordsList notAWordlist = new AllowedWordsList();
            Fact notA = new Fact('a', false, -1, 0);
            List<Fact> noAFactList = new List<Fact>();
            noAFactList.Add(notA);
            notAWordlist.ApplyFacts(noAFactList);
            Assert.False(notAWordlist.Contains("ABOUT"));
        }

        [Fact]
        public void AllowedWordsList_ApplyFacts_1xAExistRuleShouldRemoveHello()
        {
            // Ensures HELLO is not removed if a fact says no A's
            AllowedWordsList notAWordlist = new AllowedWordsList();
            Fact notA = new Fact('a', true, -1, 1);
            List<Fact> noAFactList = new List<Fact>();
            noAFactList.Add(notA);
            notAWordlist.ApplyFacts(noAFactList);
            Assert.False(notAWordlist.Contains("HELLO"));
        }

        [Fact]
        public void AllowedWordsList_ApplyFacts_1xAExistRuleShouldNotRemoveAbout()
        {
            // Ensures ABOUT is not removed if a fact says no A's
            AllowedWordsList notAWordlist = new AllowedWordsList();
            Fact notA = new Fact('a', true, -1, 1);
            List<Fact> noAFactList = new List<Fact>();
            noAFactList.Add(notA);
            notAWordlist.ApplyFacts(noAFactList);
            Assert.True(notAWordlist.Contains("ABOUT"));
        }

        /*

        [Fact]
        public void AllowedWordsList_ApplyFacts_CorrectlyRemovesWordsThatContainE()
        {
            // Ensures hello is removed if a fact says no E's
            AllowedWordsList notEWordlist = new AllowedWordsList();
            Fact notE = new Fact('e', false, -1, -1);
            List<Fact> noEFactList = new List<Fact>();
            noEFactList.Add(notE);
            notEWordlist.ApplyFacts(noEFactList);
            Assert.False(notEWordlist.Contains("HELLO"));
        }

        [Fact]
        public void AllowedWordsList_ApplyFacts_CorrectlyLeavesWordsThatContainE()
        {
            // Ensures hello is not removed if a fact says E's exist
            AllowedWordsList eWordlist = new AllowedWordsList();
            Fact yesE = new Fact('a', true, -1, -1);
            List<Fact> yesEFactList = new List<Fact>();
            yesEFactList.Add(yesE);
            eWordlist.ApplyFacts(yesEFactList);
            Assert.True(eWordlist.Contains("HELLO"));
        }

        [Fact]
        public void AllowedWordsList_ApplyFacts_CorrectlyRemovesWordsThatContainHInPos0()
        {
            // Ensures hello is removed if a fact says no E in position 0
            AllowedWordsList notH1Wordlist = new AllowedWordsList();
            Fact notH1 = new Fact('h', false, 0, -1);
            List<Fact> noH1FactList = new List<Fact>();
            noH1FactList.Add(notH1);
            notH1Wordlist.ApplyFacts(noH1FactList);
            Assert.False(notH1Wordlist.Contains("HELLO"));
        }

        [Fact]
        public void AllowedWordsList_ApplyFacts_CorrectlyLeavesWordsThatContainHInPos0()
        {
            // Ensures hello is not removed if a fact says E in position 0
            AllowedWordsList h1Wordlist = new AllowedWordsList();
            Fact yesH1 = new Fact('h', true, 0, -1);
            List<Fact> yesH1FactList = new List<Fact>();
            yesH1FactList.Add(yesH1);
            h1Wordlist.ApplyFacts(yesH1FactList);
            Assert.True(h1Wordlist.Contains("HELLO"));
        }

        [Fact]
        public void AllowedWordsList_ApplyFacts_CorrectlyRemovesWordsThatContainEInPos1()
        {
            // Ensures hello is removed if a fact says no E in position 1
            AllowedWordsList notE2Wordlist = new AllowedWordsList();
            Fact notE2 = new Fact('e', false, 1, -1);
            List<Fact> noE2FactList = new List<Fact>();
            noE2FactList.Add(notE2);
            notE2Wordlist.ApplyFacts(noE2FactList);
            Assert.False(notE2Wordlist.Contains("HELLO"));
        }

        [Fact]
        public void AllowedWordsList_ApplyFacts_CorrectlyLeavesWordsThatContainEInPos1()
        {
            // Ensures hello is not removed if a fact says E in position 1
            AllowedWordsList e2Wordlist = new AllowedWordsList();
            Fact yesE2 = new Fact('e', true, 1, -1);
            List<Fact> yesE2FactList = new List<Fact>();
            yesE2FactList.Add(yesE2);
            e2Wordlist.ApplyFacts(yesE2FactList);
            Assert.True(e2Wordlist.Contains("HELLO"));
        }

        [Fact]
        public void AllowedWordsList_ApplyFacts_CorrectlyRemovesWordsThatContainLInPos2()
        {
            // Ensures hello is removed if a fact says no E in position 2
            AllowedWordsList notL3Wordlist = new AllowedWordsList();
            Fact notL3 = new Fact('l', false, 2, -1);
            List<Fact> noL3FactList = new List<Fact>();
            noL3FactList.Add(notL3);
            notL3Wordlist.ApplyFacts(noL3FactList);
            Assert.False(notL3Wordlist.Contains("HELLO"));
        }

        [Fact]
        public void AllowedWordsList_ApplyFacts_CorrectlyLeavesWordsThatContainLInPos2()
        {
            // Ensures hello is not removed if a fact says E in position 2
            AllowedWordsList l3Wordlist = new AllowedWordsList();
            Fact yesL3 = new Fact('l', true, 2, -1);
            List<Fact> yesL3FactList = new List<Fact>();
            yesL3FactList.Add(yesL3);
            l3Wordlist.ApplyFacts(yesL3FactList);
            Assert.True(l3Wordlist.Contains("HELLO"));
        }

        [Fact]
        public void AllowedWordsList_ApplyFacts_CorrectlyRemovesWordsThatContainLInPos3()
        {
            // Ensures hello is removed if a fact says no E in position 3
            AllowedWordsList notL4Wordlist = new AllowedWordsList();
            Fact notL4 = new Fact('l', false, 3, -1);
            List<Fact> noL4FactList = new List<Fact>();
            noL4FactList.Add(notL4);
            notL4Wordlist.ApplyFacts(noL4FactList);
            Assert.False(notL4Wordlist.Contains("HELLO"));
        }

        [Fact]
        public void AllowedWordsList_ApplyFacts_CorrectlyLeavesWordsThatContainLInPos3()
        {
            // Ensures hello is not removed if a fact says E in position 3
            AllowedWordsList l4Wordlist = new AllowedWordsList();
            Fact yesL4 = new Fact('l', true, 3, -1);
            List<Fact> yesL4FactList = new List<Fact>();
            yesL4FactList.Add(yesL4);
            l4Wordlist.ApplyFacts(yesL4FactList);
            Assert.True(l4Wordlist.Contains("HELLO"));
        }

        [Fact]
        public void AllowedWordsList_ApplyFacts_CorrectlyRemovesWordsThatContainOInPos4()
        {
            // Ensures hello is removed if a fact says no E in position 4
            AllowedWordsList notO5Wordlist = new AllowedWordsList();
            Fact notO5 = new Fact('o', false, 4, -1);
            List<Fact> noO5FactList = new List<Fact>();
            noO5FactList.Add(notO5);
            notO5Wordlist.ApplyFacts(noO5FactList);
            Assert.False(notO5Wordlist.Contains("HELLO"));
        }

        [Fact]
        public void AllowedWordsList_ApplyFacts_CorrectlyLeavesWordsThatContainOInPos4()
        {
            // Ensures hello is not removed if a fact says E in position 4
            AllowedWordsList o5Wordlist = new AllowedWordsList();
            Fact yesO5 = new Fact('o', true, 4, -1);
            List<Fact> yesO5FactList = new List<Fact>();
            yesO5FactList.Add(yesO5);
            o5Wordlist.ApplyFacts(yesO5FactList);
            Assert.True(o5Wordlist.Contains("HELLO"));
        }

        */

        [Fact]
        public void AllowedWordsList_ApplyFacts_CorrectlyRemovesAddedWhenZeroDs()
        {
            bool testPass = true;
            string failMsg = "";

            // Checks ADDED is removed if a fact says no Ds
            AllowedWordsList noDWordList = new AllowedWordsList();
            Fact noD = new Fact('D', false, -1, 0);
            List<Fact> noDFactList = new List<Fact>();
            noDFactList.Add(noD);
            noDWordList.ApplyFacts(noDFactList);
            if (noDWordList.Contains("ADDED"))
            {
                failMsg = failMsg + "Failed to remove ADDED when total Ds was 0. ";
                testPass = false;
            }
            Assert.True(testPass, failMsg);
        }

        [Fact]
        public void AllowedWordsList_ApplyFacts_CorrectlyRemovesAddedWhenOneD()
        {
            bool testPass = true;
            string failMsg = "";

            // Checks ADDED is removed if a fact says one D
            AllowedWordsList oneDWordList = new AllowedWordsList();
            Fact oneD = new Fact('D', true, -1, 1);
            List<Fact> oneDFactList = new List<Fact>();
            oneDFactList.Add(oneD);
            oneDWordList.ApplyFacts(oneDFactList);
            if (oneDWordList.Contains("ADDED"))
            {
                failMsg = failMsg + "Failed to remove ADDED when total Ds was 1. ";
                testPass = false;
            }
            Assert.True(testPass, failMsg);
        }

        [Fact]
        public void AllowedWordsList_ApplyFacts_CorrectlyRemovesAddedWhenTwoDs()
        {
            bool testPass = true;
            string failMsg = "";

            // Checks ADDED is removed if a fact says two Ds
            AllowedWordsList twoDWordList = new AllowedWordsList();
            Fact twoD = new Fact('D', true, -1, 2);
            List<Fact> twoDFactList = new List<Fact>();
            twoDFactList.Add(twoD);
            twoDWordList.ApplyFacts(twoDFactList);
            if (twoDWordList.Contains("ADDED"))
            {
                failMsg = failMsg + "Failed to remove ADDED when total Ds was 2. ";
                testPass = false;
            }
            Assert.True(testPass, failMsg);
        }

        [Fact]
        public void AllowedWordsList_ApplyFacts_CorrectlyLeavesAddedWhenThreeDs()
        {
            bool testPass = true;
            string failMsg = "";

            // Checks ADDED is NOT removed if a fact says three Ds
            AllowedWordsList threeDWordList = new AllowedWordsList();
            Fact threeD = new Fact('D', true, -1, 3);
            List<Fact> threeDFactList = new List<Fact>();
            threeDFactList.Add(threeD);
            threeDWordList.ApplyFacts(threeDFactList);
            if (!threeDWordList.Contains("ADDED"))
            {
                failMsg = failMsg + "Remove ADDED when total Ds was 3. ";
                testPass = false;
            }
            Assert.True(testPass, failMsg);
        }

        [Fact]
        public void AllowedWordsList_ApplyFacts_CorrectlyRemovesAddedWhenFourDs()
        {
            bool testPass = true;
            string failMsg = "";

            // Checks ADDED is removed if a fact says four Ds
            AllowedWordsList fourDWordList = new AllowedWordsList();
            Fact fourD = new Fact('D', true, -1, 4);
            List<Fact> fourDFactList = new List<Fact>();
            fourDFactList.Add(fourD);
            fourDWordList.ApplyFacts(fourDFactList);
            if (fourDWordList.Contains("ADDED"))
            {
                failMsg = failMsg + "Failed to remove ADDED when total Ds was 4. ";
                testPass = false;
            }
            Assert.True(testPass, failMsg);
        }

        [Fact]
        public void AllowedWordsList_ApplyFacts_CorrectlyRemovesAddedWhenFiveDs()
        {
            bool testPass = true;
            string failMsg = "";
            // Checks ADDED is removed if a fact says five Ds
            AllowedWordsList fiveDWordList = new AllowedWordsList();
            Fact fiveD = new Fact('D', true, -1, 5);
            List<Fact> fiveDFactList = new List<Fact>();
            fiveDFactList.Add(fiveD);
            fiveDWordList.ApplyFacts(fiveDFactList);
            if (fiveDWordList.Contains("ADDED"))
            {
                failMsg = failMsg + "Failed to remove ADDED when total Ds was 5. ";
                testPass = false;
            }

            Assert.True(testPass, failMsg);
        }
    }

    public class DataValidatorTests
    {
        public DataValidator validator = new DataValidator();

        [Fact]
        public void TestToUpperFunction()
        {
            // Validator should lower case all inputs
            Assert.Equal("AAHED", validator.ValidateAnswer("AaHed"));
        }

        [Fact]
        public void TestFirstLegalWord()
        {
            // The word "aahed" should be allowed by the validator
            Assert.Equal("AAHED", validator.ValidateAnswer("aahed"));
        }

        [Fact]
        public void TestLegalWordHello()
        {
            // The word "hello" should be allowed by the validator
            Assert.Equal("HELLO", validator.ValidateAnswer("hello"));
        }

        [Fact]
        public void TestLegalWordWorld()
        {
            // The word "world" should be allowed by the validator
            Assert.Equal("WORLD", validator.ValidateAnswer("world"));
        }

        [Fact]
        public void TestLastLegalWord()
        {
            // The word "zymic" should be allowed by the validator
            Assert.Equal("ZYMIC", validator.ValidateAnswer("zymic"));
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

    public class FactTests
    {
        [Fact]
        public void TestPositionMinusTwoFails()
        {
            // Positions -1 for a Fact should not be allowed
            Assert.Throws<InvalidOperationException>(() => new Fact('a', true, -2, -1));
        }

        [Fact]
        public void TestPositionMinusOnePasses()
        {
            // Positions -1 for a Fact should work
            int pos = -1;
            Fact f = new Fact('a', true, pos, -1);
            Assert.Equal(f.GetPosition(), pos);
        }

        [Fact]
        public void TestPositionZeroPasses()
        {
            // Positions 0 for a Fact should work
            int pos = 0;
            Fact f = new Fact('a', true, pos, -1);
            Assert.Equal(f.GetPosition(), pos);
        }

        [Fact]
        public void TestPositionFourPasses()
        {
            // Positions 4 for a Fact should work
            int pos = 4;
            Fact f = new Fact('a', true, pos, -1);
            Assert.Equal(f.GetPosition(), pos);
        }

        [Fact]
        public void TestPositionFiveFails()
        {
            // Positions 6 for a Fact should not be allowed
            Assert.Throws<InvalidOperationException>(() => new Fact('a', true, 5, -1));
        }
    }

    public class GameMasterTests
    {
        [Fact]
        public void GameMasterCannotBeCreatedWithALongWord()
        {


            Assert.Throws<InvalidOperationException>(() => new GameMaster("illegalWord"));
        }

        [Fact]
        public void GameMasterCannotBeCreatedWithAShortWord()
        {
            Assert.Throws<InvalidOperationException>(() => new GameMaster("oops"));
        }

        [Fact]
        public void GameMasterCannotBeCreatedWithAnIllegalFiveLetterWord()
        {
            Assert.Throws<InvalidOperationException>(() => new GameMaster("rrong"));
        }

        [Fact]
        public void GameMasterGuessWordIsGGGGGIfCorrect()
        {
            string secretWord = "hello";
            string guessedWord = "hello";
            string expectedAnswer = "GGGGG";

            GameMaster gm = new GameMaster(secretWord);
            string actualAnswer = gm.GuessWord(guessedWord);
            string message = "actual: " + actualAnswer + " expected: " + expectedAnswer;
            Assert.True(actualAnswer.Equals(expectedAnswer), message);
        }

        [Fact]
        public void GameMasterGuessWordIsXXXXXIfNoMatches()
        {
            string secretWord = "super";
            string guessedWord = "latch";
            string expectedAnswer = "XXXXX";

            GameMaster gm = new GameMaster(secretWord);
            string actualAnswer = gm.GuessWord(guessedWord);
            Assert.Equal(actualAnswer, expectedAnswer);
        }

        [Fact]
        public void GameMasterGuessWordThrowsErrorIfIllegalWord()
        {
            GameMaster gm = new GameMaster("right");
            Assert.Throws<InvalidOperationException>(() => gm.GuessWord("rrong"));
        }
    }


    public class PlayBotTests
    {
        [Fact]
        public void PlaybotGetFactsThrowsErrorIfIllegalWord()
        {
            PlayBot bot = new PlayBot();
            Assert.Throws<InvalidOperationException>(() => bot.GetFacts("rrong", "XXXXX"));
        }

        public string CheckForPlayBotGetFactsErrors(string guessedWord, string expectedPattern, string[] expectedFacts)
        {
            // setup
            bool passed = true;
            string returnMsg = "";
            PlayBot bot = new PlayBot();

            // the method under test
            List<Fact> facts = bot.GetFacts(guessedWord, expectedPattern);

            // Checking the total number of facts matches
            if (facts.Count != expectedFacts.Length)
            {
                returnMsg = returnMsg + "Expected " + expectedFacts.Length + " facts but had " + facts.Count + "; ";
                passed = false;
            }

            // Checking all the expected facts exist
            foreach (string factName in expectedFacts)
            {
                if (!facts.Exists(i => i.Name.Equals(factName)))
                {
                    returnMsg = returnMsg + "Missing fact: " + factName + "; ";
                    passed = false;
                }
            }

            // if failed, adding the actual facts to the return message
            if (!passed)
            {
                returnMsg = returnMsg + "Actual facts are: ";
                foreach (Fact fact in facts)
                {
                    returnMsg = returnMsg + fact.Name + "; ";
                }
            }

            // return result
            return returnMsg;
        }

        [Fact]
        public void PlaybotGetFactsReturnsCorrectFactsForXXXXXPattern()
        {
            string guessedWord = "HELLO";
            string expectedPattern = "XXXXX";
            string[] expectedFacts = {
                "0x H exist",
                "0x E exist",
                "0x L exist",
                "0x O exist"
            };

            string failureMessage = CheckForPlayBotGetFactsErrors(guessedWord, expectedPattern, expectedFacts);
            Assert.True(failureMessage.Equals(""), failureMessage);
        }

        [Fact]
        public void PlaybotGetFactsReturnsCorrectFactsForYXXXXPattern()
        {
            string guessedWord = "HELLO";
            string expectedPattern = "YXXXX";
            string[] expectedFacts = {
                "H exists",
                "H does not exist in position 0",
                "0x E exist",
                "0x L exist",
                "0x O exist"
            };

            string failureMessage = CheckForPlayBotGetFactsErrors(guessedWord, expectedPattern, expectedFacts);
            Assert.True(failureMessage.Equals(""), failureMessage);
        }

        [Fact]
        public void PlaybotGetFactsReturnsCorrectFactsForGXXXYPattern()
        {
            string guessedWord = "HELLO";
            string expectedPattern = "GXXXY";
            string[] expectedFacts = {
                "H exists in position 0",
                "0x E exist",
                "0x L exist",
                "O exists",
                "O does not exist in position 4"
            };

            string failureMessage = CheckForPlayBotGetFactsErrors(guessedWord, expectedPattern, expectedFacts);
            Assert.True(failureMessage.Equals(""), failureMessage);
        }

        [Fact]
        public void PlaybotGetFactsReturnsCorrectFactsForXXYXXPattern()
        {
            string guessedWord = "WORLD";
            string expectedPattern = "XXYXX";
            string[] expectedFacts = {
                "0x W exist",
                "0x O exist",
                "R exists",
                "R does not exist in position 2",
                "0x L exist",
                "0x D exist"
            };

            string failureMessage = CheckForPlayBotGetFactsErrors(guessedWord, expectedPattern, expectedFacts);
            Assert.True(failureMessage.Equals(""), failureMessage);
        }

        [Fact]
        public void PlaybotGetFactsReturnsCorrectFactsForXXGXXPattern()
        {
            string guessedWord = "WORLD";
            string expectedPattern = "XXGXX";
            string[] expectedFacts = {
                "0x W exist",
                "0x O exist",
                "R exists in position 2",
                "0x L exist",
                "0x D exist"
            };

            string failureMessage = CheckForPlayBotGetFactsErrors(guessedWord, expectedPattern, expectedFacts);
            Assert.True(failureMessage.Equals(""), failureMessage);
        }

        [Fact]
        public void PlaybotGetFactsReturnsCorrectFactsForXXXXYPattern()
        {
            string guessedWord = "WORLD";
            string expectedPattern = "XXXXY";
            string[] expectedFacts = {
                "0x W exist",
                "0x O exist",
                "0x R exist",
                "0x L exist",
                "D exists",
                "D does not exist in position 4",
            };

            string failureMessage = CheckForPlayBotGetFactsErrors(guessedWord, expectedPattern, expectedFacts);
            Assert.True(failureMessage.Equals(""), failureMessage);
        }

        [Fact]
        public void PlaybotGetFactsReturnsCorrectFactsForXXXXGPattern()
        {
            string guessedWord = "WORLD";
            string expectedPattern = "XXXXG";
            string[] expectedFacts = {
                "0x W exist",
                "0x O exist",
                "0x R exist",
                "0x L exist",
                "D exists in position 4"
            };

            string failureMessage = CheckForPlayBotGetFactsErrors(guessedWord, expectedPattern, expectedFacts);
            Assert.True(failureMessage.Equals(""), failureMessage);
        }

        [Fact]
        public void PlaybotGetFactsReturnsCorrectFactsForXYXGXPattern()
        {
            string guessedWord = "HELLO";
            string expectedPattern = "XYXGX";
            string[] expectedFacts = {
                "0x H exist",
                "E exists",
                "E does not exist in position 1",
                "1x L exist",
                "L exists in position 3",
                "0x O exist"
            };

            string failureMessage = CheckForPlayBotGetFactsErrors(guessedWord, expectedPattern, expectedFacts);
            Assert.True(failureMessage.Equals(""), failureMessage);
        }

        [Fact]
        public void PlaybotGetFactsReturnsCorrectFactsForYYYYYPattern()
        {
            string guessedWord = "HELLO";
            string expectedPattern = "YYYYY";
            string[] expectedFacts = {
                "H exists",
                "H does not exist in position 0",
                "E exists",
                "E does not exist in position 1",
                "L exists",
                "L does not exist in position 2",
                "L does not exist in position 3",
                "O exists",
                "O does not exist in position 4"
            };

            string failureMessage = CheckForPlayBotGetFactsErrors(guessedWord, expectedPattern, expectedFacts);
            Assert.True(failureMessage.Equals(""), failureMessage);
        }

        [Fact]
        public void PlaybotGetFactsReturnsCorrectFactsForGGGGGPattern()
        {
            string guessedWord = "HELLO";
            string expectedPattern = "GGGGG";
            string[] expectedFacts = {
                "H exists in position 0",
                "E exists in position 1",
                "L exists in position 2",
                "L exists in position 3",
                "O exists in position 4"
            };

            string failureMessage = CheckForPlayBotGetFactsErrors(guessedWord, expectedPattern, expectedFacts);
            Assert.True(failureMessage.Equals(""), failureMessage);
        }

        [Fact]
        public void PlaybotGetFactsReturnsCorrectFactsWithDoubleLetters()
        {
            string guessedWord = "HELLO";
            string expectedPattern = "XXXYX";
            string[] expectedFacts = {
                "0x H exist",
                "0x E exist",
                "1x L exist",
                "L exists",
                "L does not exist in position 3",
                "0x O exist"
            };

            string failureMessage = CheckForPlayBotGetFactsErrors(guessedWord, expectedPattern, expectedFacts);
            Assert.True(failureMessage.Equals(""), failureMessage);
        }
    }

    public class WordTests
    {
        Word hello = new Word(1, "HELLO", 50);

        [Fact]
        public void Word_GetRank_TestCaseShouldBe1() {
            Assert.Equal(1, hello.GetRank());
        }

        [Fact]
        public void Word_GetName_TestCaseShouldBeHELLO() {
            Assert.True(hello.GetName().Equals("HELLO"));
        }

        [Fact]
        public void Word_GetPrevelance_TestCaseShouldBe50() {
            Assert.Equal(50, hello.GetPrevelance());
        }

        [Fact]
        public void Word_Equals_ToDo_WriteThisTest() {
            Assert.True(false);
        }

        [Fact]
        public void Word_GetHashCode_ToDo_WriteThisTest()
        {
            Assert.True(false);
        }
    }

}