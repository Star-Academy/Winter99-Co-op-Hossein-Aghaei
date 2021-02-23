﻿using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace search
{
    public class IndexCreator
    {
        private readonly IFileReader _docFileReader;

        public IndexCreator(IFileReader docFileReader)
        {
            _docFileReader = docFileReader;
        }

        public Dictionary<string, HashSet<string>> OrganizeDocsAndWords()
        {
            var allData = _docFileReader.ScanData();
            var result = allData.SelectMany(x =>
                QueryOnWordsOfSpecificDoc(x.Key, x.Value));
            var group = result.GroupBy(x => x.Key);

            return group.ToDictionary(x => x.Key, x =>
                new HashSet<string>(x.Select(y => y.Value)));
        }

        private static Dictionary<string, string> QueryOnWordsOfSpecificDoc(string docName, string docContent)
        {
            var wordsOfSpecificDoc = Regex.Split(docContent, "\\W+").Where(x => x.Length != 0)
                .Select(x => new{
                Word = x,
                Doc = docName
                });
            return wordsOfSpecificDoc.GroupBy(x => x.Word).Select(x => x.First()).
                ToDictionary(x => x.Word, x => x.Doc);
        }
    }
}