using System;
using System.Linq;
using elasticsearch.DocManager;
using elasticsearch.SearchConnection;

namespace elasticsearch
{
    public class Controller
    {
        private readonly IView _consoleView;
        private readonly ISearch _searcher;

        public Controller(IView consoleView, ISearch searcher)
        {
            _consoleView = consoleView;
            _searcher = searcher;
        }

        public void Run()
        {
            var input = _consoleView.GetUserInput();
            var splitWords = SplitInputIntoSeparateDocs(input);
            if (!IsInputValid(splitWords))
                throw new ArgumentException("Sentence must have No Sign Word");
            _consoleView.ShowSearchResult(_searcher.Search(splitWords));
        }

        private static bool IsInputValid(DocContainer splitWords)
        {
            return !string.IsNullOrWhiteSpace(splitWords.NoSignWords);
        }

        private static DocContainer SplitInputIntoSeparateDocs(string inputSentence)
        {
            var splitSentence = inputSentence.Split();
            var noSignWords = splitSentence.Where(word
                => !word.StartsWith("+") && !word.StartsWith("-"));
            var plusSignWords = splitSentence.Where(word => word.StartsWith("+"));
            var minusSignWords = splitSentence.Where(word => word.StartsWith("-"));
            var docContainer = new DocContainer
            {
                NoSignWords = string.Join(' ' ,noSignWords),
                PlusSignWords = string.Join(' ' ,plusSignWords),
                MinusSignWords = string.Join(' ' ,minusSignWords)
            };
            return docContainer;
        }
    }
}