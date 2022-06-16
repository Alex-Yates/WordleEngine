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
            string[] actual = { top5[0].Name, top5[1].Name, top5[2].Name, top5[3].Name, top5[4].Name };

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

        [Theory]
        [MemberData(nameof(WordPatternFactCombos), DisableDiscoveryEnumeration = true)]
        public void Playbot_GetFacts_TestWordPatternCombosGenerateExpectedFacts(string guessedWord, string expectedPattern, string[] expectedFacts) {
            string failureMessage = CheckForPlayBotGetFactsErrors(guessedWord, expectedPattern, expectedFacts);
            Assert.True(failureMessage.Equals(""), failureMessage);
        }

        // Helper function, used by Playbot_GetFacts_TestWordPatternCombosGenerateExpectedFacts
        //   Returns a string of errors. If all goes to plan, it should be an empty string (i.e. no errors).
        //   Feels ugly, but doing it this way since it allows me to write nice, clear error messages.
        //   There's probably a better way?
        public string CheckForPlayBotGetFactsErrors(string guessedWord, string pattern, string[] expectedFacts) {
            // setup
            bool passed = true;
            string returnMsg = "";
            PlayBot bot = new PlayBot();

            // the method under test
            List<Fact> facts = bot.GetFacts(guessedWord, pattern);

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
                returnMsg += "Actual facts are: ";
                foreach (Fact fact in facts) {
                    returnMsg = returnMsg + fact.Name + "; ";
                }
            }

            // return result
            return returnMsg;
        }

        public static TheoryData<string, string, string[]> WordPatternFactCombos {
            get {
                // Creating the expected fact lists first, because they cannot be cleanly created inline within the TheoryData<string[]> constructor
                string[] hello_xxxxx_facts = { "0x H exist", "0x E exist", "0x L exist", "0x O exist" };
                string[] hello_yxxxx_facts = { "H exists", "H does not exist in position 0", "0x E exist", "0x L exist", "0x O exist" };
                string[] world_xxyxx_facts = { "0x W exist", "0x O exist", "R exists", "R does not exist in position 2", "0x L exist", "0x D exist" };
                string[] world_xxxxy_facts = { "0x W exist", "0x O exist", "0x R exist", "0x L exist", "D exists", "D does not exist in position 4" };
                string[] world_gxxxx_facts = { "W exists in position 0", "0x O exist", "0x R exist", "0x L exist", "0x D exist" };
                string[] world_xxgxx_facts = { "0x W exist", "0x O exist", "R exists in position 2", "0x L exist", "0x D exist" };
                string[] world_xxxxg_facts = { "0x W exist", "0x O exist", "0x R exist", "0x L exist", "D exists in position 4" };
                string[] hello_gxxxy_facts = { "H exists in position 0", "0x E exist", "0x L exist", "O exists", "O does not exist in position 4" };
                string[] hello_xxxyx_facts = { "0x H exist", "0x E exist", "1x L exist", "L exists", "L does not exist in position 3", "0x O exist" };
                string[] hello_xyxgx_facts = { "0x H exist", "E exists", "E does not exist in position 1", "1x L exist", "L exists in position 3", "0x O exist" };
                string[] hello_yyyyy_facts = { "H exists", "H does not exist in position 0", "E exists", "E does not exist in position 1", "L exists", 
                                               "L does not exist in position 2", "L does not exist in position 3", "O exists", "O does not exist in position 4" };
                string[] hello_ggggg_facts = { "H exists in position 0", "E exists in position 1", "L exists in position 2", "L exists in position 3", 
                                               "O exists in position 4" };

                // PlayBot.GetFacts() test data
                var data = new TheoryData<string, string, string[]> {
                    { "HELLO", "XXXXX", hello_xxxxx_facts }, // XXXXX - 5x X
                    { "HELLO", "YXXXX", hello_yxxxx_facts }, // YXXXX - 1x Y (first letter)
                    { "WORLD", "XXYXX", world_xxyxx_facts }, // XXYXX - 1x Y (middle letter)
                    { "WORLD", "XXXXY", world_xxxxy_facts }, // XXXXY - 1x Y (last letter)
                    { "WORLD", "GXXXX", world_gxxxx_facts }, // GXXXX - 1x G (first letter)
                    { "WORLD", "XXGXX", world_xxgxx_facts }, // XXGXX - 1x G (middle letter)
                    { "WORLD", "XXXXG", world_xxxxg_facts }, // XXXXG - 1x G (last letter)
                    { "HELLO", "GXXXY", hello_gxxxy_facts }, // GXXXY - 1x G + 1x Y
                    { "HELLO", "XXXYX", hello_xxxyx_facts }, // XXXYX - 1x Y, 1x X for a duplicate Y letter
                    { "HELLO", "XYXGX", hello_xyxgx_facts }, // XYXGX - 1x G, 1x Y, 1x X for a duplicate G letter
                    { "HELLO", "YYYYY", hello_yyyyy_facts }, // YYYYY - 5x Y
                    { "HELLO", "GGGGG", hello_ggggg_facts }, // GGGGG - 5x G: Winner winner chicken dinner!
                };
                return data;
            }
        }
    }
}
