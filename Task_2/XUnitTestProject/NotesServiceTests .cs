using System;
using Xunit;
using Moq;
using NotesApp.Services.Abstractions;
using NotesApp.Services.Models;
using NotesApp.Services.Services;

namespace XUnitTestProject
{
    public class NoteServiceTests
    {
        [Fact]
        public void IfNoteIsNullThrowArgumentNullException()
        {
            var noteService = new NotesService(new Mock<INotesStorage>().Object,new Mock<INoteEvents>().Object);
            void Action() => noteService.AddNote(null, It.IsAny<int>());

            Assert.Throws<ArgumentNullException>(Action);
        }
        [Fact]
        public void IfDoINotesStorageAddNoteThenDoINoteEventsNotifyAdded()
        {
            var note = new Note();
            var id = It.IsAny<int>();
            var storege = new Mock<INotesStorage>();
            var events = new Mock<INoteEvents>();
            var noteService = new NotesService(storege.Object, events.Object);

            noteService.AddNote(note, id);

            storege.Verify(n => n.AddNote(note, id),Times.Once);
            events.Verify(n => n.NotifyAdded(note, id), Times.Once);
        }
        [Fact]
        public void IfDoNotINotesStorageAddNoteThenDoNotINoteEventsNotifyAdded()
        {
            var id = It.IsAny<int>();
            Note note = new Note();
            var storege = new Mock<INotesStorage>();
            var events = new Mock<INoteEvents>();
            var noteService = new NotesService(storege.Object, events.Object);
            try
            {
                noteService.AddNote(null, id);
            }
            catch { }

            storege.Verify(n => n.AddNote(It.IsAny<Note>(), id), Times.Never);
            events.Verify(n => n.NotifyAdded(It.IsAny<Note>(), id), Times.Never);
        }
        [Fact]
        public void IfDoINotesStorageDeleteNoteThenDoINoteEventsNotifyDeleted()
        {
            var id = It.IsAny<int>();
            var guid = It.IsAny<Guid>();
            var storege = new Mock<INotesStorage>();
            storege.Setup(s=>s.DeleteNote(guid)).Returns(true);
            var events = new Mock<INoteEvents>();
            var noteService = new NotesService(storege.Object, events.Object);

            noteService.DeleteNote(guid,id);

            storege.Verify(n => n.DeleteNote(guid), Times.Once);
            events.Verify(n => n.NotifyDeleted(guid, id), Times.Once);
        }
        [Fact]
        public void IfDoNotINotesStorageDeleteNoteThenDoNotINoteEventsNotifyDeleted()
        {
            var id = It.IsAny<int>();
            var guid = It.IsAny<Guid>();
            var storege = new Mock<INotesStorage>();
            storege.Setup(s => s.DeleteNote(guid)).Returns(false);
            var events = new Mock<INoteEvents>();
            var noteService = new NotesService(storege.Object, events.Object);

            noteService.DeleteNote(guid, id);

            storege.Verify(n => n.DeleteNote(guid), Times.Once);
            events.Verify(n => n.NotifyDeleted(guid, id), Times.Never);
        }
    }
}
