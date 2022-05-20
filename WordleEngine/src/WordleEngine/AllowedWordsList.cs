using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data;
using System.Text.RegularExpressions;

namespace WordleEngine {
    
    public class AllowedWordsList {

        private DataTable AllowedWords { get; set; }

        public AllowedWordsList() {
            var legalWordsFile = @"wordle-allowed-guesses.csv";
            this.AllowedWords = ConvertCSVtoDataTable(legalWordsFile);
        }

        private static DataTable ConvertCSVtoDataTable(string strFilePath)
        {
            StreamReader sr = new StreamReader(strFilePath);
            string[] headers = sr.ReadLine().Split(',');
            DataTable dt = new DataTable();
            foreach (string header in headers)
            {
                dt.Columns.Add(header);
            }
            while (!sr.EndOfStream)
            {
                string[] rows = Regex.Split(sr.ReadLine(), ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");
                DataRow dr = dt.NewRow();
                for (int i = 0; i < headers.Length; i++)
                {
                    dr[i] = rows[i];
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }

        public bool Contains(string input)
        {
            bool wordExists = false;
            string filter = "word = \'" + input + "\'";
            DataRow[] wordRow = AllowedWords.Select(filter);
            if (wordRow.Length == 1)
            {
                wordExists = true;
            }
            return wordExists;
        }

        public int Count()
        {
            return AllowedWords.Rows.Count;
        }

        public void ApplyFacts(List<Fact> facts) {
            foreach (Fact fact in facts) {
                
                // Position must either be 0 (position irrelavent) or between 1 and 5 (specific position known)
                if ((fact.Position > 5) or(fact.Position < 0)){
                    string errorMsg = "Error!: Fact must have position between 0 and 5";
                    throw new InvalidOperationException(errorMsg);
                }

                DataRow[] rowsToKeep;

                // Case: where the letter exists but the position is irrelevant
                if (fact.getPosition() == 1) {
                    // delete all the words that contain this letter
                    string expression = "Word.Contains(fact.getLetter)";
                    rowsToKeep = this.AllowedWords.Select(expression);
                }

                // Case: where letter does not exist in any position
                throw new InvalidOperationException("to do: write this code");

                // Case: where letter exists in a specific position
                throw new InvalidOperationException("to do: write this code");

                // Case: where letter does not exist in a specific position
                throw new InvalidOperationException("to do: write this code");

                // Create new datatable (quicker than iterating through and removing rows individually)
                DataTable tempDataTable = rowsToKeep.CopyToDataTable;
                this.AllowedWords.Clear();
                this.AllowedWords.Merge(tempDataTable);
                tempDataTable.Dispose();

            }
        }
    }
}
