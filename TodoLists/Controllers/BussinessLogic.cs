using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoLists.Models;

namespace TodoLists.Controllers
{
    public class BusinessLogic : IBusinessLogic
    {
        private readonly TodoListsContext _context;

        public BusinessLogic(TodoListsContext context)
        {
            _context = context;
        }

        public List<Note> DeleteNote(int id)
        {
            return _context.Note.Include(x => x.CheckList).Include(x => x.Label).Where(x => x.Id == id).ToList();
        }

        public List<Note> GetNoteByPrimitive(int Id, string Title, string Message, bool Pinned, string Label)
        {
            return _context.Note.Include(x => x.CheckList).Include(x => x.Label)
                            .Where(element => element.Title == ((Title == null) ? element.Title : Title)
                                  && element.Message == ((Message == null) ? element.Message : Message)
                                  && element.Pinned == ((!Pinned) ? element.Pinned : Pinned)
                                  && element.Id == ((Id == 0) ? element.Id : Id)
                                  && element.Label.Any(x => (Label != null) ? x.label == Label : true)).ToList();
        }

        public async Task PutNote(Note note)
        {
            await _context.Note.Include(x => x.CheckList).Include(x => x.Label).ForEachAsync(element =>
            {
                if (element.Id == note.Id)
                {
                    element.Message = note.Message;
                    element.Pinned = note.Pinned;
                    element.Title = note.Title;
                    _context.Label.RemoveRange(element.Label);
                    element.Label.AddRange(note.Label);
                    _context.CheckList.RemoveRange(element.CheckList);
                    element.CheckList.AddRange(note.CheckList);
                }
            });
        }

        public void PostNote(Note note)
        {
            _context.Note.Add(note);
        }
    }
}
