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
        public void GameMaster_GuessWord_HelloReturnsGGGGGIfCorrect() {
            string secretWord = "hello";
            string guessedWord = "hello";
            string expectedAnswer = "GGGGG";

            GameMaster gm = new GameMaster(secretWord);
            string actualAnswer = gm.GetAnswer(guessedWord);
            string message = "actual: " + actualAnswer + " expected: " + expectedAnswer;
            Assert.True(actualAnswer.Equals(expectedAnswer), message);
        }

        [Fact]
        public void GameMaster_GuessWord_LatchReturnsXXXXXForSuper() {
            string secretWord = "super";
            string guessedWord = "latch";
            string expectedAnswer = "XXXXX";

            GameMaster gm = new GameMaster(secretWord);
            string actualAnswer = gm.GetAnswer(guessedWord);
            Assert.Equal(actualAnswer, expectedAnswer);
        }

        [Fact]
        public void GameMaster_GuessWord_AboutReturnsYXXXYForLatch() {
            string secretWord = "latch";
            string guessedWord = "about";
            string expectedAnswer = "YXXXY";

            GameMaster gm = new GameMaster(secretWord);
            string actualAnswer = gm.GetAnswer(guessedWord);
            Assert.Equal(actualAnswer, expectedAnswer);
        }

        [Fact]
        public void GameMaster_GuessWord_BuddyReturnsXXYXXForDrain() {
            // Interesting case. Are we handling multiple yellows correctly?
            string secretWord = "drain";
            string guessedWord = "buddy";
            string expectedAnswer = "XXYXX";

            GameMaster gm = new GameMaster(secretWord);
            string actualAnswer = gm.GetAnswer(guessedWord);
            Assert.Equal(actualAnswer, expectedAnswer);
        }

        [Fact]
        public void GameMaster_GuessWord_DandyReturnsGYYXXForDrain() {
            // Interesting case. Are we handling multiple yellows correctly?
            string secretWord = "drain";
            string guessedWord = "dandy";
            string expectedAnswer = "GYYXX";

            GameMaster gm = new GameMaster(secretWord);
            string actualAnswer = gm.GetAnswer(guessedWord);
            Assert.Equal(actualAnswer, expectedAnswer);
        }

        [Fact]
        public void GameMaster_GuessWord_AddedReturnsYYYXXForDandy() {
            // Interesting case. Are we handling multiple yellows correctly?
            string secretWord = "dandy";
            string guessedWord = "added";
            string expectedAnswer = "YYYXX";

            GameMaster gm = new GameMaster(secretWord);
            string actualAnswer = gm.GetAnswer(guessedWord);
            Assert.Equal(actualAnswer, expectedAnswer);
        }

        [Fact]
        public void GameMaster_GuessWord_AddedReturnsGGGGXForAdder() {
            // Interesting case. Are we handling multiple yellows correctly?
            string secretWord = "adder";
            string guessedWord = "added";
            string expectedAnswer = "GGGGX";

            GameMaster gm = new GameMaster(secretWord);
            string actualAnswer = gm.GetAnswer(guessedWord);
            Assert.Equal(actualAnswer, expectedAnswer);
        }

        [Fact]
        public void GameMaster_GuessWord_AddedReturnsYYXYGForDread() {
            // Interesting case. Are we handling multiple yellows correctly?
            string secretWord = "dread";
            string guessedWord = "added";
            string expectedAnswer = "YYXYG";

            GameMaster gm = new GameMaster(secretWord);
            string actualAnswer = gm.GetAnswer(guessedWord);
            Assert.Equal(actualAnswer, expectedAnswer);
        }

        [Fact]
        public void GameMaster_GuessWord_ThrowsErrorIfIllegalWordRrong() {
            GameMaster gm = new GameMaster("right");
            Assert.Throws<InvalidOperationException>(() => gm.GetAnswer("rrong"));
        }
    }
}
