using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace WordleEngine.Tests {
    public class DataValidator_ValidateAnswer_Tests {
        public DataValidator validator = new DataValidator();

        [Theory]
        [MemberData(nameof(ValidWords), DisableDiscoveryEnumeration = true)]
        public void DataValidator_ValidateAnswer_TestValidWords(string word) {
            Assert.Equal(word, validator.ValidateAnswer(word));
        }

        [Theory]
        [MemberData(nameof(ValidWordsPairsWithMessedUpCasing), DisableDiscoveryEnumeration = true)]
        public void DataValidator_ValidateAnswer_TestUpperCasing(string upperCaseWord, string messedUpCaseWord) {
            Assert.Equal(upperCaseWord, validator.ValidateAnswer(messedUpCaseWord));
        }

        [Theory]
        [MemberData(nameof(InvalidWords), DisableDiscoveryEnumeration = true)]
        public void DataValidator_ValidateAnswer_TestInvalidWords(string invalidWord) {
            Assert.Throws<InvalidOperationException>(() => validator.ValidateAnswer(invalidWord));
        }

        public static TheoryData<string> ValidWords {
            get {
                var data = new TheoryData<string> {
                    { "AAHED" },  // First word alphabetically, last word by rank
                    { "ZYMIC" },  // Last word alpabetically
                    { "ABOUT" }   // First word by rank
                };
                return data;
            }
        }

        public static TheoryData<string, string> ValidWordsPairsWithMessedUpCasing {
            get {
                var data = new TheoryData<string, string> {
                    { "HELLO", "hello" },
                    { "WORLD", "World" },
                    { "ABOUT", "aBoUt" }
                };
                return data;
            }
        }

        public static TheoryData<string> InvalidWords {
            get {
                var data = new TheoryData<string> {
                    { "xxxxx" },    // Not a word, lower case
                    { "XXXXX" },    // Not a word, upper case
                    { "enormous" }, // Real word but too long, lower case
                    { "ENORMOUS" }, // Real word but too long, upper case
                    { "tiny" },     // Real word but too short, lower case
                    { "TINY" },     // Real word but too short, upper case
                };
                return data;
            }
        }
    }
}
