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
            if ((position < -1) || (position > 4)) {
                string errorMsg = "ERROR!: Rule cannot have position " + position + ". Position must be between -1 and 4.";
                throw new InvalidOperationException(errorMsg);
            }

            if ((total < -1) || (total > 5))
            {
                string errorMsg = "ERROR!: Rule cannot have total " + total + ". Total must be between -1 and 5.";
                throw new InvalidOperationException(errorMsg);
            }

            if (exists && (total == 0)) {
                string errorMsg = "ERROR!: If exists, total cannot be 0.";
                throw new InvalidOperationException(errorMsg);
            }

            if (!exists && (total != 0)) {
                string errorMsg = "ERROR!: If not exists, total must be 0.";
                throw new InvalidOperationException(errorMsg);
            }


            this.Letter = Char.ToUpper(letter);
            this.Exists = exists;
            this.Position = position;  // Zero based. 0 is the first letter of the word. -1 means not sure what position.
            this.Total = total; // The number of a given letter. Hence, if 1, there is one of the letter in the word. If 0, there are none of this letter. -1 means "ignore this field"

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
            else {
                // For example: "0x A exist", "1x B exist", "2x C exist", "3x D exist"
                this.Name = total + "x " + letter + " exist";
            }
        }
    }
}
