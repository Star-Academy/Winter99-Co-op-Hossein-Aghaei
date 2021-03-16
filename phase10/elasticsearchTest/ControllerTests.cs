using System;
using System.Collections.Generic;
using elasticsearch;
using NSubstitute;
using Xunit;

namespace elasticsearchTest
{
    public class ControllerTests
    {
        private readonly Controller _sut;
        private readonly IView _consoleView;
        private readonly ISearch _searcher;

        public ControllerTests()
        {
            _consoleView = Substitute.For<IView>();
            _searcher =  Substitute.For<ISearch>();
            _sut = new Controller(_consoleView, _searcher);
        }

        [Fact]
        public void Run_ShouldThrowArgumentException_WhenThereIsNotNoSignWordInInput()
        {
            _consoleView.GetUserInput().Returns("-hi +dad");
            
            void Result() => _sut.Run();

            Assert.Throws<ArgumentException>(Result);
        }
        
        [Fact]
        public void Run_ShouldExecuteShowResultFunction_WhenInputIsValid()
        {
            _consoleView.GetUserInput().Returns("car -hi +dad");
            
            _sut.Run();

            _consoleView.Received(1).ShowSearchResult(Arg.Any<HashSet<string>>());
        }

        [Fact]
        public void Run_ShouldExecuteShowResultFunctionWithDocContainerParameter_WhenInputIsValid()
        {
            const string noSignWords = "hello dad";
            const string plusSignWords = "dog";
            const string minusSignWords = "evil";
            var expectedParameter = new DocContainer()
            {NoSignWords = noSignWords,
                PlusSignWords = plusSignWords,
                MinusSignWords = minusSignWords};
            _consoleView.GetUserInput().Returns("hello -evil +dog dad");
            
            _sut.Run();

            _searcher.Received(1).Search(expectedParameter);
        }

    }
}