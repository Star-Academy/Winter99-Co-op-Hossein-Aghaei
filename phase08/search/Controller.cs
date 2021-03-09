using System;
using System.Collections.Generic;
using System.Linq;

namespace search
{
    public class Controller
    {
        private readonly IView _consoleView;
        private readonly ISearch _processor;

        public Controller(IView consoleView, ISearch processor)
        {
            _consoleView = consoleView;
            _processor = processor;
        }

        public void Run()
        {
            var input = _consoleView.GetUserInput();
            var splitWords = SplitInputIntoSeparateDocs(input);
            if (!IsInputValid(splitWords))
                throw new ArgumentException("Sentence must have No Sign Word");
            _consoleView.ShowSearchResult(_processor.Search(splitWords));
        }
        private static bool IsInputValid(DocContainer splitWords)
        {
            return splitWords.NoSignWords.Any();
        }

        private static DocContainer SplitInputIntoSeparateDocs(string inputSentence)
        {
            var noSignWords = new HashSet<string>();
            var plusSignWords = new HashSet<string>();
            var minusSignWords = new HashSet<string>();
            foreach (var word in inputSentence.Split())
            {
                if (!word.StartsWith("+") && !word.StartsWith("-"))
                    noSignWords.Add(word);
                if (word.StartsWith("+"))
                    plusSignWords.Add(word.Substring(1));
                if (word.StartsWith("-"))
                    minusSignWords.Add(word.Substring(1));
            }

            var docContainer = new DocContainer
            {
                NoSignWords = noSignWords,
                PlusSignWords = plusSignWords,
                MinusSignWords = minusSignWords
            };
            return docContainer;
        }
    }
}