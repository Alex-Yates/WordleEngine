using System;
using System.Collections.Generic;
using System.Text;

namespace WordleEngine
{
    public class Fact
    {
        public readonly char Letter;
        public readonly bool Exists;
        public readonly int Position;

        public Fact(char letter, bool exists, int position) {
            if ((position < 0) || (position > 5)) {
                string errorMsg = "ERROR!: Rule cannot have position " + position + ". Position must be between 0 and 5.";
                throw new InvalidOperationException(errorMsg);
            }
            this.Letter = letter;
            this.Exists = exists;
            this.Position = position;
        }

        public char GetLetter() {
            return this.Letter;
        }

        public bool GetExists() {
            return this.Exists;
        }

        public int GetPosition() {
            return this.Position;
        }
    }
}
