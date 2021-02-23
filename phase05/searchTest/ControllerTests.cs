using System.Collections.Generic;
using NSubstitute;
using search;
using Xunit;

namespace searchTest
{
    public class ControllerTests
    {
        private readonly Controller _sut;
        private readonly IView consoleView;
        private readonly ISearch processor;

        public ControllerTests()
        {
            consoleView = Substitute.For<IView>();
            processor = Substitute.For<ISearch>();
            _sut = new Controller(consoleView, processor);
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
        }
    }
}