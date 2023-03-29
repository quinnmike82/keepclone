using api.Data;
using api.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    public class NoteController : BaseApiController
    {
        private ApplicationContext _context;
        public NoteController (ApplicationContext context){
            _context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<Note>> GetNotes(){
            return await _context.Notes.ToListAsync();
        }

        [HttpPost("AddNote")]
        public async Task<Note> AddNote(Note note){
            await _context.Notes.AddAsync(note);
            await _context.SaveChangesAsync();
            return note;
        }
        [HttpGet("userid/{id}")]
        public async Task<IEnumerable<Note>> GetNoteByUserId(string id){
            return await _context.Notes.Where(n => n.UserId == id).ToListAsync();
        }
    }
}