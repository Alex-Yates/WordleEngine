using System;
using System.Collections.Generic;
using System.Text;

namespace WordleEngine
{
    public class Word
    {
        private string Name { get; set; }
        private  int  Prevelance { get; set; }

        public Word(string name, int prevalence) {
            this.Name = name;
            this.Prevelance = prevalence;
        }

        public string GetName() {
            return this.Name;
        }

        public int GetPrevalence() {
            return this.Prevelance;
        }
    }
}
