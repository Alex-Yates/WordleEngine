using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace WordleEngine.Tests {
    public class GameMasterTests {
        [Fact]
        public void GameMasterCannotBeCreatedWithALongWord() {
            Assert.Throws<InvalidOperationException>(() => new GameMaster("illegalWord"));
        }

        [Fact]
        public void GameMasterCannotBeCreatedWithAShortWord() {
            Assert.Throws<InvalidOperationException>(() => new GameMaster("oops"));
        }

        [Fact]
        public void GameMasterCannotBeCreatedWithAnIllegalFiveLetterWord() {
            Assert.Throws<InvalidOperationException>(() => new GameMaster("rrong"));
        }

        [Fact]
        public void GameMasterGuessWordIsGGGGGIfCorrect() {
            string secretWord = "hello";
            string guessedWord = "hello";
            string expectedAnswer = "GGGGG";

            GameMaster gm = new GameMaster(secretWord);
            string actualAnswer = gm.GuessWord(guessedWord);
            string message = "actual: " + actualAnswer + " expected: " + expectedAnswer;
            Assert.True(actualAnswer.Equals(expectedAnswer), message);
        }

        [Fact]
        public void GameMasterGuessWordIsXXXXXIfNoMatches() {
            string secretWord = "super";
            string guessedWord = "latch";
            string expectedAnswer = "XXXXX";

            GameMaster gm = new GameMaster(secretWord);
            string actualAnswer = gm.GuessWord(guessedWord);
            Assert.Equal(actualAnswer, expectedAnswer);
        }

        [Fact]
        public void GameMasterGuessWordThrowsErrorIfIllegalWord() {
            GameMaster gm = new GameMaster("right");
            Assert.Throws<InvalidOperationException>(() => gm.GuessWord("rrong"));
        }
    }
}
