using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace WordleEngine.Tests {
    public class PlayBotTests {
        [Fact]
        public void PlaybotGetFactsThrowsErrorIfIllegalWord() {
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
        public void PlaybotGetFactsReturnsCorrectFactsForXXXXXPattern() {
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
        public void PlaybotGetFactsReturnsCorrectFactsForYXXXXPattern() {
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
        public void PlaybotGetFactsReturnsCorrectFactsForGXXXYPattern() {
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
        public void PlaybotGetFactsReturnsCorrectFactsForXXYXXPattern() {
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
        public void PlaybotGetFactsReturnsCorrectFactsForXXGXXPattern() {
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
        public void PlaybotGetFactsReturnsCorrectFactsForXXXXYPattern() {
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
        public void PlaybotGetFactsReturnsCorrectFactsForXXXXGPattern() {
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
        public void PlaybotGetFactsReturnsCorrectFactsForXYXGXPattern() {
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
        public void PlaybotGetFactsReturnsCorrectFactsForYYYYYPattern() {
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
        public void PlaybotGetFactsReturnsCorrectFactsForGGGGGPattern() {
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
        public void PlaybotGetFactsReturnsCorrectFactsWithDoubleLetters() {
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
