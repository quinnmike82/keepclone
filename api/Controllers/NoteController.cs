using System.Net.Http.Headers;
using api.Data;
using api.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace api.Controllers
{
    public class NoteController : BaseApiController
    {
        private readonly IWebHostEnvironment _environment;
        private ApplicationContext _context;
        public NoteController(IWebHostEnvironment environment, ApplicationContext context)
        {
            _context = context;
            _environment = environment;
        }

        [HttpGet]
        public async Task<IEnumerable<Note>> GetNotes()
        {
            return await _context.Notes.ToListAsync();
        }

        [HttpPost("AddNote")]
        public async Task<ActionResult> AddNote()
        {
            var file = Request.Form.Files[0];
            Note note = new Note(){
                UserId = Request.Form["userId"],
                Content = Request.Form["content"],
                Title = Request.Form["title"],
                TimerSet = DateTime.Parse(Request.Form["timerSet"]),
                ColorBG = Request.Form["colorBG"]
            };
            await _context.Notes.AddAsync(note);
            if (file.Length > 0)
            {
                var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                var filePath = Path.Combine("D://Dotnet//CloneKeep//api//uploads", note.Id);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                note.FilePath = filePath;
                await _context.SaveChangesAsync();
                
            }
            else
            {
                await _context.SaveChangesAsync();
            }
            return Ok(note);
        }
        [HttpGet("userid/{id}")]
        public async Task<IEnumerable<Note>> GetNoteByUserId(string id)
        {
            return await _context.Notes.Where(n => n.UserId == id).ToListAsync();
        }

        [HttpPut("updateNote")]
        public async Task<Note> UpdateNote(Note note){
            _context.Notes.Update(note);
            await _context.SaveChangesAsync();
            return note;
        }



    }
}