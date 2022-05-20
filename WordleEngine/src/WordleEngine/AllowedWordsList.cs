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

        public bool Contains(string input) {
            bool wordExists = false;
            string filter = "word = \'" + input + "\'";
            DataRow[] wordRow = AllowedWords.Select(filter);
            if(wordRow.Length == 1) {
                wordExists = true;
            }
            return wordExists;
        }

        public int Count() {
            return AllowedWords.Rows.Count;
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
    }
}
