using Xunit;

namespace WordleEngine.Tests { 
    public class Word_Equals_Tests {
        private readonly Word hello = new Word(1, "HELLO", 50);
        private readonly Word hello2 = new Word(1, "HELLO", 50);
        private readonly Word world = new Word(2, "WORLD", 53);

        [Fact]
        public void Word_Equals_HelloEqualsHello() {
            Assert.True(hello.Equals(hello2));
        }

        [Fact]
        public void Word_Equals_HelloDoesNotEqualWorld() {
            Assert.False(hello.Equals(world));
        }
    }

}