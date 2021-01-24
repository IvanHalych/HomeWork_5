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
    public class NumberGeneratorTests
    {
        [Theory]
        [InlineData(0)]
        [InlineData(19)]
        [InlineData(100)]
        public void IfArgumentIsInvalidThrowArgumentException(int length) 
        { 
            var action = new Action(()=> NumberGenerator.GeneratePositiveLong(length));
            Assert.ThrowsAny<ArgumentException>(action);
        }
        [Theory]
        [ClassData(typeof(EnumeratorLength))]
        public void ReturnNumberWithGivenNumberOfCharacters(int length)
        {
            int lengthAfter=NumberGenerator.GeneratePositiveLong(length).ToString().Length;

            Assert.Equal(lengthAfter,length);
        }

        public class EnumeratorLength :  IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                for (int i = 1; i <= 18; i++)
                    yield return new object[] {i};
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }
    }
}
