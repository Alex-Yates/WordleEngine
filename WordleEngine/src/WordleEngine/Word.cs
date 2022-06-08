using System;
using System.Collections.Generic;
using System.Text;

namespace WordleEngine
{
    public class Word
    {
        public readonly string Name;
        public readonly int Prevelance;
        public readonly int Rank;

        public Word(int rank, string name, int prevelance) {
            this.Rank = rank;
            this.Name = name;
            this.Prevelance = prevelance;
        }
       
        public override int GetHashCode() {
            return this.Rank;
        }

        // Creates the warning: "CS8765  Nullability of type of parameter 'obj' doesn't match overridden member (possibly because of nullability attributes)"
        //   Not sure why. Need to figure it out.
        public override bool Equals(object obj) {
            
            if (obj == null) {
                // if obj is null, it is not equal
                return false;
            }


            // Generates the message: "IDE0019 Use pattern matching"
            //   https://docs.microsoft.com/en-us/dotnet/fundamentals/code-analysis/style-rules/ide0019
            //   Not sure how to fix this.
            Word? objAsWord = obj as Word;
            
            if (objAsWord == null) {
                // if object does not cast to Word, it is not equal
                return false;
            }
            
            // Words are equal if they have the same rank
            return this.Rank == objAsWord.Rank;
        }
    }
}
