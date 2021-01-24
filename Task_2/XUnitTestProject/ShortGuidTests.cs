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
    public class ShortGuidTests
    {
        [Fact]
        public void CorrectConvertGuidToShortStringToGuid()
        {
            var guid = It.IsAny<Guid>();

            var shorted = guid.ToShortId();
            var newGuid = shorted.FromShortId();

            Assert.Equal(guid,newGuid);
        }
        [Fact]
        public void CorrectConvertGuidToShortStringToGuidWithChange()
        {
            var guid = It.IsAny<Guid>();

            var shorted = guid.ToShortId()+"==";
            var newGuid = shorted.FromShortId();

            Assert.Equal(guid, newGuid);
        }

        [Fact]
        public void CorrectConvertStringGuidToGuid()
        {
            var guid = It.IsAny<Guid>();

            var newGuid = guid.ToString().FromShortId();

            Assert.Equal(guid, newGuid);
        }
        [Fact]
        public void IfPassNullThenReturnNull()
        {
            var newGuid= ShortGuid.FromShortId(null);

            Assert.Null(newGuid);
        }

        [Fact]
        public void IfArgumentIsInvalidThrowArgumentFormatException()
        {
            string s1 = Guid.NewGuid().ToString() + "4";
            string s2 = Guid.NewGuid().ToString()[1..];
            string s3 = Guid.NewGuid().ToString()[4..];


            Assert.Throws<FormatException>(() => s1.FromShortId());
            Assert.Throws<FormatException>(() => s2.FromShortId());
            Assert.Throws<FormatException>(() => s3.FromShortId());
        }

    }
}
