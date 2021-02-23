using System.Collections.Generic;

namespace search
{
    public class Controller : ISplitter
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
            
        }

        public DocContainer SplitInputIntoSeparateDocs(string inputSentence)
        {
            var noSignWords = new HashSet<string>();
            var plusSignWords = new HashSet<string>();
            var minusSignWords = new HashSet<string>();
            foreach (var word in inputSentence.Split())
            {
                if (!word.StartsWith("+") && !word.StartsWith("-"))
                    noSignWords.Add(word);
                if (word.StartsWith("+"))
                    plusSignWords.Add(word);
                else
                    minusSignWords.Add(word);
            }

            var docContainer = new DocContainer
            {
                NoSignWords = noSignWords, PlusSignWords = plusSignWords, MinusSignWords = minusSignWords
            };
            return docContainer;
        }
    }
}