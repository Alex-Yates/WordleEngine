using System;
using System.Collections.Generic;
using System.Text;

namespace WordleEngine
{
    class Fact
    {
        public readonly char Letter;
        public readonly bool Exists;
        public readonly int Position;

        public Fact(char letter, bool exists, int position) {
            this.Letter = letter;
            this.Exists = exists;
            this.Position = position;
        }

        internal char getLetter() {
            return this.Letter;
        }

        internal bool getExists() {
            return this.Exists;
        }

        internal int getPostition() {
            return this.Position;
        }
    }
}
