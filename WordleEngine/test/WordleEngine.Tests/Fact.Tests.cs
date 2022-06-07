using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace WordleEngine.Tests {
    public class FactTests {
        [Fact]
        public void TestPositionMinusTwoFails() {
            // Positions -1 for a Fact should not be allowed
            Assert.Throws<InvalidOperationException>(() => new Fact('a', true, -2, -1));
        }

        [Fact]
        public void TestPositionMinusOnePasses() {
            // Positions -1 for a Fact should work
            int pos = -1;
            Fact f = new Fact('a', true, pos, -1);
            Assert.Equal(f.GetPosition(), pos);
        }

        [Fact]
        public void TestPositionZeroPasses() {
            // Positions 0 for a Fact should work
            int pos = 0;
            Fact f = new Fact('a', true, pos, -1);
            Assert.Equal(f.GetPosition(), pos);
        }

        [Fact]
        public void TestPositionFourPasses() {
            // Positions 4 for a Fact should work
            int pos = 4;
            Fact f = new Fact('a', true, pos, -1);
            Assert.Equal(f.GetPosition(), pos);
        }

        [Fact]
        public void TestPositionFiveFails() {
            // Positions 6 for a Fact should not be allowed
            Assert.Throws<InvalidOperationException>(() => new Fact('a', true, 5, -1));
        }
    }
}
