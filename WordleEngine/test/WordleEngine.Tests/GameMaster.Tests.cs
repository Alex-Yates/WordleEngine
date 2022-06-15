using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace WordleEngine.Tests {
    public class GameMaster_GameMaster_Tests {
        [Theory]
        [MemberData(nameof(InvalidWords), DisableDiscoveryEnumeration = true)]
        public void GameMaster_GameMaster_InvalidWordThrowsError(string invalidWord) {
            Assert.Throws<InvalidOperationException>(() => new GameMaster(invalidWord));
        }

        public static TheoryData<string> InvalidWords {
            get {
                var data = new TheoryData<string> {
                    { "XXXXX" },    // Not a word
                    { "ENORMOUS" }, // Real word but too long
                    { "TINY" }      // Real word but too short
                };
                return data;
            }
        }
    }

    public class GameMaster_GetAnswer_Tests {
        
        [Theory]
        [MemberData(nameof(GuessAnswerCombos), DisableDiscoveryEnumeration = true)]
        public void GameMaster_GetAnswer_ReturnsCorrectAnswer(string secretWord, string guessedWord, string expectedAnswer) {
            GameMaster gm = new GameMaster(secretWord);
            string actualAnswer = gm.GetAnswer(guessedWord);
            string message = "actual: " + actualAnswer + ", expected: " + expectedAnswer;
            Assert.True(actualAnswer.Equals(expectedAnswer), message);
        }

        [Theory]
        [MemberData(nameof(InvalidWords), DisableDiscoveryEnumeration = true)]
        public void GameMaster_GetAnswer_ThrowsErrorIfIllegalWord(string invalidWord) {
            GameMaster gm = new GameMaster("RIGHT");
            Assert.Throws<InvalidOperationException>(() => gm.GetAnswer(invalidWord));
        }

        public static TheoryData<string, string, string> GuessAnswerCombos {
            get {
                var data = new TheoryData<string, string, string> {
                    { "HELLO", "HELLO", "GGGGG" }, // Handles G correctly
                    { "SUPER", "LATCH", "XXXXX" }, // Handles X correctly
                    { "LATCH", "ABOUT", "YXXXY" }, // Handles Y correctly, in the simple case
                    { "DRAIN", "BUDDY", "XXYXX" }, // Handles Y correctly, when 1 D in solution and 2 D in guess, misplaced, no other hits
                    { "DRAIN", "DANDY", "GYYXX" }, // Handles Y correctly, when 1 D in solution and 2 D in guess, one correctly placed, a couple of other hits
                    { "DANDY", "ADDED", "YYYXX" }, // Handles Y correctly, when 2 D in solution and 3 D in guess, all misplaced, one other Y hit
                    { "DREAD", "ADDED", "YYXYG" }, // Handles Y correctly, when 2 D in solution and 3 D in guess, one correctly placed, two other hits
                    { "ADDER", "ADDED", "GGGGX" }  // Handles Y correctly, when 2 D in solution and 3 D in guess, two correctly placed, two other hits
                };
                return data;
            }
        }
    
        public static TheoryData<string> InvalidWords {
            get {
                var data = new TheoryData<string> {
                    { "XXXXX" },    // Not a word
                    { "ENORMOUS" }, // Real word but too long
                    { "TINY" }      // Real word but too short
                };
                return data;
            }
        }
    }
}
