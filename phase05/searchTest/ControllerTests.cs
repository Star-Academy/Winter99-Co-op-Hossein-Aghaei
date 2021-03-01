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

        public ControllerTests()
        {
            _consoleView = Substitute.For<IView>();
            var processor = Substitute.For<ISearch>();
            _sut = new Controller(_consoleView, processor);
        }

        [Fact]
        public void SplitInputIntoSeparateDocs_ShouldSplitInputIntoDocContainer()
        {
            //Arrange
            var noSignWords = new HashSet<string>() {"hello", "dad"};
            var plusSignWords = new HashSet<string>() {"dog"};
            var minusSignWords = new HashSet<string>() {"evil"};
            var expected = new DocContainer()
                {NoSignWords = noSignWords, PlusSignWords = plusSignWords, MinusSignWords = minusSignWords};
            
            //Act
            var result = _sut.SplitInputIntoSeparateDocs("hello -evil +dog dad");
            
            //Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Run_ShouldThrowArgumentException_WhenThereIsNotNoSignWordInInput()
        {
            //Arrange
            _consoleView.GetUserInput().Returns("-hi +dad");
            
            //Act
            void Result() => _sut.Run();

            //Assert
            Assert.Throws<ArgumentException>(Result);
        }
        
        [Fact]
        public void Run_ShouldExecuteShowResultFunction_WhenInputIsValid()
        {
            //Arrange
            _consoleView.GetUserInput().Returns("car -hi +dad");
            
            //Act
            _sut.Run();

            //Assert
            _consoleView.Received(1).ShowSearchResult(Arg.Any<HashSet<string>>());
        }

    }
}