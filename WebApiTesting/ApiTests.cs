using System;
using Xunit;
using Moq;
using TodoLists.Controllers;
using TodoLists.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using FluentAssertions;

namespace WebApiTesting
{
    public class ApiTests
    {
        private readonly TodoListsContext _context;
        NotesController _controller;
        public ApiTests()
        {
            var optionBuilder = new DbContextOptionsBuilder<TodoListsContext>();
            optionBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            _context = new TodoListsContext(optionBuilder.Options);
            _controller = new NotesController(new BusinessLogic(_context),_context);
        }

        readonly List<Note> note = new List<Note>() {
               new Note
           {
               Title = "first",
               Message = "Ft",
               Id = 1,
               Label = new List<Label>
               {
                   new Label { label = "black"},
                   new Label { label = "green"}
               },
               CheckList = new List<CheckList>
               {
                   new CheckList { Checklist = "redbull"},
                   new CheckList { Checklist = "pepsi"}
               },
               Pinned = true
           },
               new Note
           {
               Title = "second",
               Message = "sec",
               Id = 2,
               Label = new List<Label>
               {
                   new Label { label = "black"},
                   new Label { label = "green"}
               },
               CheckList = new List<CheckList>
               {
                   new CheckList { Checklist = "redbull"},
                   new CheckList { Checklist = "pepsi"}
               },
               Pinned = false
           }
           };

        public Note note1 = new Note
        {
            Title = "test",
            Id = 1,
            Message = "test1",
            Label = new List<Label>
               {
                   new Label { label = "black"},
                   new Label { label = "green"}
               },
            CheckList = new List<CheckList>
               {
                   new CheckList { Checklist = "redbull"},
                   new CheckList { Checklist = "pepsi"}
               },
            Pinned = true
        };

        public Note note2 = new Note
           {
               Title = "second",
               Message = "sec",
               Id = 2,
               Label = new List<Label>
               {
                   new Label { label = "black"},
                   new Label { label = "green"}
               },
               CheckList = new List<CheckList>
               {
                   new CheckList { Checklist = "redbull"},
                   new CheckList { Checklist = "pepsi"}
               },
               Pinned = false
           };

            List <Note> noteList = new List<Note>() {new Note
           {
            Title = "test",
            Id = 1,
            Message = "test1",
               Label = new List<Label>
               {
                   new Label { label = "black"},
                   new Label { label = "green"}
               },
               CheckList = new List<CheckList>
               {
                   new CheckList { Checklist = "redbull"},
                   new CheckList { Checklist = "pepsi"}
               },
               Pinned = true
           } };

        [Fact]
        public async Task Test1Async()
        {
            var testMock = new Mock<IBusinessLogic>();
            testMock.Setup(x => x.GetNoteByPrimitive(0, null, null, false, null)).Returns(noteList);
            NotesController controllerTest = new NotesController(new BusinessLogic(_context), _context);
            await controllerTest.PostNote(note1);
            NotesController mockController = new NotesController(testMock.Object, _context);
            var actual = controllerTest.GetNoteByPrimitive(0, null, null, false, null);
            var okObject = actual as OkObjectResult;
            var actualValue = okObject.Value as List<Note>;
            var mockActual = mockController.GetNoteByPrimitive(0, null, null, false, null);
            var mockOkObject = mockActual as OkObjectResult;
            var mockActualValue = mockOkObject.Value as List<Note>;
            mockActualValue[0].Title.Should().Be(actualValue[0].Title);
            mockActualValue[0].Message.Should().Be(actualValue[0].Message);

        }

        [Fact]
        public async Task Test2()
        {
            var testMock = new Mock<IBusinessLogic>();
            testMock.Setup(x => x.PostNote(note1));
            NotesController controllerTest = new NotesController(new BusinessLogic(_context), _context);
            var mockOutput = await controllerTest.PostNote(note1);
            NotesController mockController = new NotesController(testMock.Object, _context);
            var actual = await mockController.PostNote(note1);
            var okObject = actual as CreatedAtActionResult;
            var mockOkObject = mockOutput as CreatedAtActionResult;
            var mockActualValue = mockOkObject.Value as Note;
            var actualValue = okObject.Value as Note;
            mockActualValue.Title.Should().Be(actualValue.Title);
            mockActualValue.Message.Should().Be(actualValue.Message);

        }

        [Fact]
        public async Task Test3()
        {

            var testMock = new Mock<IBusinessLogic>();
            testMock.Setup(x => x.PutNote(note1)).Returns(Task.FromResult(new CreatedAtActionResult("GetNote", "Notes", new { id = note1.Id }, note1)));
            NotesController controllerTest = new NotesController(new BusinessLogic(_context), _context);
            await controllerTest.PostNote(note1);
            NotesController mockController = new NotesController(testMock.Object, _context);
            var mockOutput = await mockController.PutNote(1, note1);
            var actual = await controllerTest.PutNote(1,note1);
            var okObject = actual as CreatedAtActionResult;
            var mockOkObject = mockOutput as CreatedAtActionResult;
            var mockActualValue = mockOkObject.Value as Note;
            var actualValue = okObject.Value as Note;
            mockActualValue.Title.Should().Be(actualValue.Title);
            mockActualValue.Message.Should().Be(actualValue.Message);

        }
        [Fact]
        public async Task Test4()
        {
            
            NotesController controllerTest = new NotesController(new BusinessLogic(_context), _context);
            await controllerTest.PostNote(note1);
            await controllerTest.PostNote(note2);
            var actual = await controllerTest.DeleteNote(1);
            var okObject = actual as OkObjectResult;
            var actualValue = okObject.Value as List<Note>;
            actualValue[0].Title.Should().Be(note1.Title);
            actualValue[0].Message.Should().Be(note1.Message);

        }
    }
}
