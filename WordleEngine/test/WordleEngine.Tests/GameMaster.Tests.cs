using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace WordleEngine.Tests {
    public class GameMaster_GameMaster_Tests {
        [Fact]
        public void GameMaster_GameMaster_LongWordThrowsError() {
            Assert.Throws<InvalidOperationException>(() => new GameMaster("illegalWord"));
        }

        [Fact]
        public void GameMaster_GameMaster_ShortWordThrowsError() {
            Assert.Throws<InvalidOperationException>(() => new GameMaster("oops"));
        }

        [Fact]
        public void GameMaster_GameMaster_RRONGThrowsError() {
            Assert.Throws<InvalidOperationException>(() => new GameMaster("rrong"));
        }
    }

    public class GameMaster_GuessWord_Tests {
        [Fact]
        public void GameMaster_GuessWord_GGGGGIfCorrect() {
            string secretWord = "hello";
            string guessedWord = "hello";
            string expectedAnswer = "GGGGG";

            GameMaster gm = new GameMaster(secretWord);
            string actualAnswer = gm.GuessWord(guessedWord);
            string message = "actual: " + actualAnswer + " expected: " + expectedAnswer;
            Assert.True(actualAnswer.Equals(expectedAnswer), message);
        }

        [Fact]
        public void GameMaster_GuessWord_XXXXXIfNoMatches() {
            string secretWord = "super";
            string guessedWord = "latch";
            string expectedAnswer = "XXXXX";

            GameMaster gm = new GameMaster(secretWord);
            string actualAnswer = gm.GuessWord(guessedWord);
            Assert.Equal(actualAnswer, expectedAnswer);
        }

        [Fact]
        public void GameMaster_GuessWord_YXXXYIfAppropriate() {
            string secretWord = "latch";
            string guessedWord = "about";
            string expectedAnswer = "YXXXY";

            GameMaster gm = new GameMaster(secretWord);
            string actualAnswer = gm.GuessWord(guessedWord);
            Assert.Equal(actualAnswer, expectedAnswer);
        }

        [Fact]
        public void GameMaster_GuessWord_ThrowsErrorIfIllegalWord() {
            GameMaster gm = new GameMaster("right");
            Assert.Throws<InvalidOperationException>(() => gm.GuessWord("rrong"));
        }
    }
}
