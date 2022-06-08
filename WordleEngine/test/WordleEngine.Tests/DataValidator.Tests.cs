using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace WordleEngine.Tests {
    public class DataValidator_ValidateAnswer_Tests {
        public DataValidator validator = new DataValidator();

        [Fact]
        public void DataValidator_ValidateAnswer_TestToUpperFunction() {
            // Validator should lower case all inputs
            Assert.Equal("AAHED", validator.ValidateAnswer("AaHed"));
        }

        [Fact]
        public void DataValidator_ValidateAnswer_TestFirstLegalWordAlphabetically() {
            // The word "aahed" should be allowed by the validator
            Assert.Equal("AAHED", validator.ValidateAnswer("aahed"));
        }

        [Fact]
        public void DataValidator_ValidateAnswer_TestLastLegalWordAlphabetically() {
            // The word "zymic" should be allowed by the validator
            Assert.Equal("ZYMIC", validator.ValidateAnswer("zymic"));
        }

        [Fact]
        public void DataValidator_ValidateAnswer_TestHello() {
            // The word "hello" should be allowed by the validator
            Assert.Equal("HELLO", validator.ValidateAnswer("hello"));
        }

        [Fact]
        public void DataValidator_ValidateAnswer_TestWorld() {
            // The word "world" should be allowed by the validator
            Assert.Equal("WORLD", validator.ValidateAnswer("world"));
        }

        [Fact]
        public void DataValidator_ValidateAnswer_TestFirstLegalWordByRanky() {
            // The word "aahed" should be allowed by the validator
            Assert.Equal("ABOUT", validator.ValidateAnswer("ABOUT"));
        }

        [Fact]
        public void DataValidator_ValidateAnswer_TestLastLegalWordByRank() {
            // The word "zymic" should be allowed by the validator
            Assert.Equal("AAHED", validator.ValidateAnswer("AAHED"));
        }

        [Fact]
        public void DataValidator_ValidateAnswer_IllegalGarbageWordShouldThrowException() {
            // Invalid 5 character strings should be rejected
            Assert.Throws<InvalidOperationException>(() => validator.ValidateAnswer("xxxxx"));
        }

        [Fact]
        public void DataValidator_ValidateAnswer_IllegalLongWordShouldThrowException() {
            // Words with more than 5 letters should be rejected
            Assert.Throws<InvalidOperationException>(() => validator.ValidateAnswer("enormous"));
        }

        [Fact]
        public void DataValidator_ValidateAnswer_IllegalShortWordShouldThrowException() {
            // Words with less than 5 letters should be rejected
            Assert.Throws<InvalidOperationException>(() => validator.ValidateAnswer("mini"));
        }
    }
}
