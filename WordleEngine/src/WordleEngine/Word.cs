using System;
using System.Collections.Generic;
using System.Text;

namespace WordleEngine
{
    public class Word
    {
        private string Name { get; set; }
        private  int  Prevelance { get; set; }
        private int Rank { get; set; }

        public Word(int rank, string name, int prevelance) {
            this.Rank = rank;
            this.Name = name;
            this.Prevelance = prevelance;
        }

        public string GetName() {
            return this.Name;
        }

        public int GetRank() {
            return this.Rank;
        }

        public int GetPrevelance() {
            return this.Prevelance;
        }

        public override int GetHashCode() {
            return this.Rank;
        }

        public override bool Equals(object obj) {
            if (obj == null) return false;
            Word? objAsWord = obj as Word;
            if (objAsWord == null) return false; 
            return this.Rank == objAsWord.GetRank();
        }
    }
}
