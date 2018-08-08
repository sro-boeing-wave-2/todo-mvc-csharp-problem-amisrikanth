using System;
using Xunit;
using Moq;
using TodoLists.Controllers;
using TodoLists.Models;
using System.Collections.Generic;
using Microsoft.En


namespace ApiTesting
{
    public class ApiTests
    {
        private readonly TodoListsContext _context;
        private readonly IBusinessLogic _service;

        public ApiTests(TodoListsContext context, IBusinessLogic service)
        {
            _context = context;
            _service = service;
        }

        public List<Note> CreateData(TodoListsContext todoContext)
        {
            var note = new List<Note>() {
               new Note
           {
               Title = "first",
               Message = "Ft",
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
               Title = "first",
               Message = "Ft",
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

           
            _context.Note.AddRange(note);
            //todoContext.SaveChanges();
            return note;
        }
        [Fact]
        public void Test1()
        {
            var testMock = new Mock<IBusinessLogic>();
            testMock.Setup(x => x.GetNoteByPrimitive(0, null, null, false, null)).Returns(CreateData(_context));

           

        }
    }
}
