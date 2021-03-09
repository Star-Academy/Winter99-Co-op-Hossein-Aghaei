using System;
using System.Collections.Generic;
using NSubstitute;
using search;
using Xunit;

namespace searchTest
{
    public class ControllerTests
    {
        private readonly Controller _sut;
        private readonly IView _consoleView;
        private readonly ISearch _processor;

        public ControllerTests()
        {
            _consoleView = Substitute.For<IView>();
            _processor =  Substitute.For<ISearch>();
            _sut = new Controller(_consoleView, _processor);
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
            var noSignWords = new HashSet<string>() {"hello", "dad"};
            var plusSignWords = new HashSet<string>() {"dog"};
            var minusSignWords = new HashSet<string>() {"evil"};
            var expectedParameter = new DocContainer()
            {NoSignWords = noSignWords,
                PlusSignWords = plusSignWords,
                MinusSignWords = minusSignWords};
            _consoleView.GetUserInput().Returns("hello -evil +dog dad");
            
            _sut.Run();

            _processor.Received(1).Search(expectedParameter);
        }

    }
}