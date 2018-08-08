using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoLists.Models;

namespace TodoLists.Controllers
{
    public interface IBusinessLogic
    {
        List<Note> DeleteNote(int id);
        List<Note> GetNoteByPrimitive(int Id, string Title, string Message, bool Pinned, string Label);
        Task PutNote(Note note);
        void PostNote(Note note);

    }
}
