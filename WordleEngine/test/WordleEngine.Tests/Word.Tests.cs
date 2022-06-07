using Xunit;

namespace WordleEngine.Tests { 
    public class WordTests
    {
        Word hello = new Word(1, "HELLO", 50);

        [Fact]
        public void Word_GetRank_TestCaseShouldBe1() {
            Assert.Equal(1, hello.GetRank());
        }

        [Fact]
        public void Word_GetName_TestCaseShouldBeHELLO() {
            Assert.True(hello.GetName().Equals("HELLO"));
        }

        [Fact]
        public void Word_GetPrevelance_TestCaseShouldBe50() {
            Assert.Equal(50, hello.GetPrevelance());
        }

        [Fact]
        public void Word_Equals_ToDo_WriteThisTest() {
            Assert.True(false);
        }

        [Fact]
        public void Word_GetHashCode_ToDo_WriteThisTest()
        {
            Assert.True(false);
        }
    }

}