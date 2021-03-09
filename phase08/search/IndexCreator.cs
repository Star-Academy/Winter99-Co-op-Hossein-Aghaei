using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Model;

namespace search
{
    public class IndexCreator
    {
        private readonly IFileReader _docFileReader;
        private readonly IDocumentRepository _documentRepository;

        public IndexCreator(IFileReader docFileReader, IDocumentRepository documentRepository)
        {
            _docFileReader = docFileReader;
            _documentRepository = documentRepository;
        }
        
        public void OrganizeDocsAndWords(string directoryPath)
        { 
            var allDocsNameKeyWithTheirContentValue = _docFileReader.ScanData(directoryPath);
            SaveAllData(allDocsNameKeyWithTheirContentValue);
        }

        private void SaveAllData(Dictionary<string, string> allDocuments)
        {
            var allDocs = allDocuments.Keys.ToList();
            var existingDocuments = _documentRepository.GetExistingDocs(allDocs);
            var newDocuments = allDocuments.Keys.Except(existingDocuments);
            foreach (var doc in newDocuments)
            {
                SaveOneDocument(doc, allDocuments[doc]);
            }
        }

        private void SaveOneDocument(string docName, string content)
        {
            var wordsInDoc = SplitWordsOfDoc(content);
            var newTerms = wordsInDoc.Where(x => !_documentRepository.ContainsWord(x)).ToList();
            var duplicateWords = wordsInDoc.Except(newTerms).ToList();
            var newWords = newTerms.Select(x => new Word() {Term = x}).ToList();
            var newDoc = new Doc() {Name = docName, WordsOfDoc = newWords, Content = content};
            _documentRepository.AddNewDoc(newDoc);
            _documentRepository.AddDuplicateWords(duplicateWords, newDoc);
        }
        
        private static HashSet<string> SplitWordsOfDoc(string docContent)
        {
            var wordsOfSpecificDoc = Regex.Split(docContent, "\\W+").
                Where(x => x.Length != 0).ToHashSet();
            return wordsOfSpecificDoc;
        }
    }
}