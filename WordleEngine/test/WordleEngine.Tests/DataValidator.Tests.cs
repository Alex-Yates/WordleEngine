using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace WordleEngine.Tests {
    public class DataValidatorTests {
        public DataValidator validator = new DataValidator();

        [Fact]
        public void TestToUpperFunction() {
            // Validator should lower case all inputs
            Assert.Equal("AAHED", validator.ValidateAnswer("AaHed"));
        }

        [Fact]
        public void TestFirstLegalWord() {
            // The word "aahed" should be allowed by the validator
            Assert.Equal("AAHED", validator.ValidateAnswer("aahed"));
        }

        [Fact]
        public void TestLegalWordHello() {
            // The word "hello" should be allowed by the validator
            Assert.Equal("HELLO", validator.ValidateAnswer("hello"));
        }

        [Fact]
        public void TestLegalWordWorld() {
            // The word "world" should be allowed by the validator
            Assert.Equal("WORLD", validator.ValidateAnswer("world"));
        }

        [Fact]
        public void TestLastLegalWord() {
            // The word "zymic" should be allowed by the validator
            Assert.Equal("ZYMIC", validator.ValidateAnswer("zymic"));
        }

        [Fact]
        public void TestIllegalWord() {
            // Invalid 5 character strings should be rejected
            Assert.Throws<InvalidOperationException>(() => validator.ValidateAnswer("xxxxx"));
        }

        [Fact]
        public void TestLongWord() {
            // Words with more than 5 letters should be rejected
            Assert.Throws<InvalidOperationException>(() => validator.ValidateAnswer("enormous"));
        }

        [Fact]
        public void TestShortWord() {
            // Words with less than 5 letters should be rejected
            Assert.Throws<InvalidOperationException>(() => validator.ValidateAnswer("mini"));
        }
    }
}
