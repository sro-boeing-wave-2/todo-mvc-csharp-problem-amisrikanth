using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoLists.Models;

namespace TodoLists.Controllers
{
     [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly TodoListsContext _context;
        private readonly IBusinessLogic _businessLogic;

        public NotesController(IBusinessLogic businessLogic, TodoListsContext context)
        {
            _context = context;
            _businessLogic = businessLogic;
        }

        // GET: api/Notes/5
        [HttpGet]
        public IActionResult GetNoteByPrimitive(
            [FromQuery(Name = "Id")] int Id,
            [FromQuery(Name = "Title")] string Title,
            [FromQuery(Name = "Message")] string Message,
            [FromQuery(Name = "Pinned")] bool Pinned,
            [FromQuery(Name = "Label")] string Label)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            List<Note> temp = new List<Note>();
            temp = _businessLogic.GetNoteByPrimitive(Id, Title, Message, Pinned, Label);
            if (temp == null)
            {
                return NotFound();
            }

            return Ok(temp);
        }

       

        // PUT: api/Notes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNote([FromRoute] int id, [FromBody] Note note)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            if (id != note.Id)
            {
                return BadRequest();
            }
            await _businessLogic.PutNote(note);
            try
            {
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetNoteByPrimitive", new { id = note.Id }, note);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NoteExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        

        // POST: api/Notes
        [HttpPost]
        public async Task<IActionResult> PostNote([FromBody] Note note)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _businessLogic.PostNote(note);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetNoteByPrimitive", new { id = note.Id }, note);
        }

        

        // DELETE: api/Notes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNote([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            List<Note> note = _businessLogic.DeleteNote(id);
            if (note == null)
            {
                return NotFound();
            }
            _context.Note.RemoveRange(note);
            _context.CheckList.RemoveRange();
            await _context.SaveChangesAsync();
            return Ok(note);
        }

        private bool NoteExists(int id)
        {
            return _context.Note.Any(e => e.Id == id);
        }
    }
}