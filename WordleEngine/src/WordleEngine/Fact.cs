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
        public readonly int Total;
        public readonly string Name;

        public Fact(char letter, bool exists, int position, int total) {

            // Verifying the parameters

            if (!PositionIsWithinValidRange(position) || !TotalIsWithinValidRange(total)) {
                string errorMsg = "ERROR!: Attempted to create a fact with either the position or total outside the expected range. Letter was: " + letter + ", Exists was: " + exists + ", Position was: " + position + ", Total was: " + total + ".";
                throw new InvalidOperationException(errorMsg);
            }

            if (ParametersContradictEachOther(exists, position, total)) {
                string errorMsg = "ERROR!: Parameters contradict each other. Letter was: " + letter + ", Exists was: " + exists + ", Position was: " + position + ", Total was: " + total + ".";
                throw new InvalidOperationException(errorMsg);
            }

            // Constructing the Fact

            this.Letter = Char.ToUpper(letter);
            this.Exists = exists;
            this.Position = position;  // Zero based. 0 is the first letter of the word. -1 means not sure what position.
            this.Total = total; // The number of a given letter. Hence, if 1, there is one of the letter in the word. If 0, there are none of this letter. -1 means "ignore this field"

            // If we don't know the total, the fact name is constructed as follows
            if (this.Total == -1) {
                string existsName = " does not exist";
                if (exists)
                {
                    existsName = " exists";
                }

                string positionName = "";
                if (position != -1)
                {
                    positionName = " in position " + position.ToString();
                }

                // For example "A exists", "B does not exist", "C exists in position 0", "D does not exist in position 4"
                this.Name = letter + existsName + positionName;
            }

            // However, if we do know the total, the fact name will be constructed like this instead 
            else {
                // For example: "0x A exist", "1x B exist", "2x C exist", "3x D exist"
                this.Name = total + "x " + letter + " exist";
            }

        }

        public override string ToString() {
            return this.Name;
        }

        private bool PositionIsWithinValidRange(int position) {
            if ((position < -1) || (position > 4)) {
                return false;
            }
            return true;
        }

        private bool TotalIsWithinValidRange(int total) {
            if ((total < -1) || (total > 5)) {
                return false;
            }
            return true;
        }

        private bool ParametersContradictEachOther(bool exists, int position, int total) {

            bool positionIsKnown = !(position == -1);

            /*
             *   Summary of compatible values:
             * 
             *                        |               exists               |               !exists              |
             *                        | !positionIsKnown | positionIsKnown | !positionIsKnown | positionIsKnown |
             *   ---------------------+------------------+-----------------+------------------+-----------------+
             *   total -1 (unknown)   |      true        |      true       |      false       |      true       |
             *                        +------------------+-----------------+------------------+-----------------+
             *   total 0 (not exists) |      false       |      false      |      true        |      false      |
             *                        +------------------+-----------------+------------------+-----------------+
             *   total 1-5 (exists)   |      true        |      false      |      false       |      false      |
             *   ---------------------+------------------+-----------------+------------------+-----------------+
             */

            // If positionIsKnown, total must be -1. (Total is only relevant when applied to a whole word, not a specific character.)
            if (positionIsKnown && (total != -1)) {
                return true;
            }

            // If !positionIsKnown and exists, total must not be 0. (It's a contradiction to claim both that the letter exists, and that there are zero instances of the letter.)
            if ((!positionIsKnown && exists) && (total == 0)) {
                return true;
            }

            // If !positionIsKnown and !exists, total must be 0. (If we know the letter does not exist in any position, there must be zero of that letter.)
            if ((!positionIsKnown && !exists) && (total != 0)) {
                return true;
            }

            // Otherwise...
            return false;
        }
    }
}
