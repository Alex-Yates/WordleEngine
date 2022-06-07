using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace WordleEngine.Tests {
    public class PlayBot_GetTopFiveWords_Tests {
        [Fact]
        public void Playbot_GetTopFiveWords_PicksTheTopFiveWords() {
            PlayBot bot = new PlayBot();
            
            List<Word> top5 = bot.GetTopFiveWords();

            string[] expected = { "ABOUT", "OTHER", "WHICH", "THEIR", "THERE" };
            string[] actual = { top5[0].GetName(), top5[1].GetName(), top5[2].GetName(), top5[3].GetName(), top5[4].GetName() };

            Assert.Equal(actual, expected);
        }
    }

    public class PlayBot_GetNumRemainingPossibleAnswers_Tests {
        [Fact]
        public void Playbot_GetNumRemainingPossibleAnswers_NewBotHas12947PossibleAnswers() {
            PlayBot bot = new PlayBot();

            int numRemainingPossibleAnswers = bot.GetNumRemainingPossibleAnswers();

            Assert.True(numRemainingPossibleAnswers == 12947);
        }
    }

    public class PlayBot_GetFacts_Tests {
        [Fact]
        public void Playbot_GetFacts_ThrowsErrorIfIllegalWord() {
            PlayBot bot = new PlayBot();
            Assert.Throws<InvalidOperationException>(() => bot.GetFacts("rrong", "XXXXX"));
        }

        public string CheckForPlayBotGetFactsErrors(string guessedWord, string expectedPattern, string[] expectedFacts) {
            // setup
            bool passed = true;
            string returnMsg = "";
            PlayBot bot = new PlayBot();

            // the method under test
            List<Fact> facts = bot.GetFacts(guessedWord, expectedPattern);

            // Checking the total number of facts matches
            if (facts.Count != expectedFacts.Length) {
                returnMsg = returnMsg + "Expected " + expectedFacts.Length + " facts but had " + facts.Count + "; ";
                passed = false;
            }

            // Checking all the expected facts exist
            foreach (string factName in expectedFacts) {
                if (!facts.Exists(i => i.Name.Equals(factName))) {
                    returnMsg = returnMsg + "Missing fact: " + factName + "; ";
                    passed = false;
                }
            }

            // if failed, adding the actual facts to the return message
            if (!passed) {
                returnMsg = returnMsg + "Actual facts are: ";
                foreach (Fact fact in facts) {
                    returnMsg = returnMsg + fact.Name + "; ";
                }
            }

            // return result
            return returnMsg;
        }

        [Fact]
        public void Playbot_GetFacts_ReturnsCorrectFactsForXXXXXPattern() {
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
        public void Playbot_GetFacts_ReturnsCorrectFactsForYXXXXPattern() {
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
        public void Playbot_GetFacts_ReturnsCorrectFactsForGXXXYPattern() {
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
        public void Playbot_GetFacts_ReturnsCorrectFactsForXXYXXPattern() {
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
        public void Playbot_GetFacts_ReturnsCorrectFactsForXXGXXPattern() {
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
        public void Playbot_GetFacts_ReturnsCorrectFactsForXXXXYPattern() {
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
        public void Playbot_GetFacts_ReturnsCorrectFactsForXXXXGPattern() {
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
        public void Playbot_GetFacts_ReturnsCorrectFactsForXYXGXPattern() {
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
        public void Playbot_GetFacts_ReturnsCorrectFactsForYYYYYPattern() {
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
        public void Playbot_GetFacts_ReturnsCorrectFactsForGGGGGPattern() {
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
        public void Playbot_GetFacts_ReturnsCorrectFactsWithDoubleLetters() {
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
}
