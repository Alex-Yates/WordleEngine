using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace WordleEngine.Tests {
    public class Fact_Fact_Tests {
        [Fact]
        public void Fact_Fact_PositionMinusTwoThrowsError() {
            // Positions -1 for a Fact should not be allowed
            Assert.Throws<InvalidOperationException>(() => new Fact('a', true, -2, -1));
        }

        [Fact]
        public void Fact_Fact_PositionMinusOneIsAllowed() {
            // Positions -1 for a Fact should work
            int pos = -1;
            Fact f = new Fact('a', true, pos, -1);
            Assert.Equal(f.GetPosition(), pos);
        }

        [Fact]
        public void Fact_Fact_PositionZeroIsAllowed() {
            // Positions 0 for a Fact should work
            int pos = 0;
            Fact f = new Fact('a', true, pos, -1);
            Assert.Equal(f.GetPosition(), pos);
        }

        [Fact]
        public void Fact_Fact_PositionFourIsAllowed() {
            // Positions 4 for a Fact should work
            int pos = 4;
            Fact f = new Fact('a', true, pos, -1);
            Assert.Equal(f.GetPosition(), pos);
        }

        [Fact]
        public void Fact_Fact_PositionFiveThrowsError() {
            // Positions 6 for a Fact should not be allowed
            Assert.Throws<InvalidOperationException>(() => new Fact('a', true, 5, -1));
        }

        [Fact]
        public void Fact_Fact_TotalMinusTwoThrowsError() {
            // Total -2 for a Fact should not be allowed
            Assert.Throws<InvalidOperationException>(() => new Fact('a', true, -1, -2));
        }

        [Fact]
        public void Fact_Fact_TotalMinusOneIsAllowed() {
            // Total -1 for a Fact should work
            int total = -1;
            Fact f = new Fact('a', true, -1, total);
            Assert.Equal(f.GetTotal(), total);
        }

        [Fact]
        public void Fact_Fact_TotalZeroIsAllowed() {
            // Total 0 for a Fact should work
            int total = 0;
            Fact f = new Fact('a', true, -1, total);
            Assert.Equal(f.GetTotal(), total);
        }

        [Fact]
        public void Fact_Fact_TotalFiveIsAllowed() {
            // Total 4 for a Fact should work
            int total = 5;
            Fact f = new Fact('a', true, -1, total);
            Assert.Equal(f.GetTotal(), total);
        }

        [Fact]
        public void Fact_Fact_TotalSixThrowsError() {
            // Total 6 for a Fact should not be allowed
            Assert.Throws<InvalidOperationException>(() => new Fact('a', true, -1, 6));
        }
    }
}
