using System;
using System.Collections.Generic;
using Xunit;
using Moq;
using NotesApp.Services.Abstractions;
using NotesApp.Services.Models;
using NotesApp.Services.Services;
using NotesApp.Tools;
using System.Collections;

namespace XUnitTestProject
{
    public class StringGeneratorTests
    {
        [Fact]
        public void IfArgumentIsZeroThenReturnEmptyString()
        {
            var str = StringGenerator.GenerateNumbersString(0, It.IsAny<bool>());
            Assert.Equal( string.Empty,str);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(-18)]
        [InlineData(-100)]
        public void IfArgumentIsInvalidThrowArgumentException(int length)
        {
            void Action()=> StringGenerator.GenerateNumbersString(length, It.IsAny<bool>());
            Assert.ThrowsAny<ArgumentException>(Action);
        }

        [Fact]
        public void IfPassFalseThenReturnStringWithOutFirstZero()
        {
            var str = StringGenerator.GenerateNumbersString(10, false);
            var first = str[0];

            Assert.NotEqual('0', first);
        }

        [Theory]
        [ClassData(typeof(NumberGeneratorTests.EnumeratorLength))]
        public void ReturnStringWithGivenNumberOfCharacters(int length)
        {
            int lengthAfter = StringGenerator.GenerateNumbersString(length, It.IsAny<bool>()).Length;

            Assert.Equal(lengthAfter, length);
        }

        [Theory]
        [ClassData(typeof(NumberGeneratorTests.EnumeratorLength))]
        public void ReturnNumberWithGivenNumberOfCharactersAndCanConvertToNumber(int length)
        {
            var str = StringGenerator.GenerateNumbersString(length, It.IsAny<bool>());
            int lengthAfter = str.Length;
            Assert.Equal(length,lengthAfter);
            Assert.True(double.TryParse(str, out _));
        }
    }
}
