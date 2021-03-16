using System;

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
            var noSignWords = "";
            var plusSignWords = "";
            var minusSignWords = "";
            foreach (var word in inputSentence.Split())
            {
                if (!word.StartsWith("+") && !word.StartsWith("-"))
                    noSignWords = $"{noSignWords} {word}";
                if (word.StartsWith("+"))
                    plusSignWords = $"{plusSignWords} {word.Substring(1)}";
                if (word.StartsWith("-"))
                    minusSignWords = $"{minusSignWords} {word.Substring(1)}";
            }

            var docContainer = new DocContainer
            {
                NoSignWords = noSignWords.Trim(),
                PlusSignWords = plusSignWords.Trim(),
                MinusSignWords = minusSignWords.Trim()
            };
            return docContainer;
        }
    }
}