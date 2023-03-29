using System.ComponentModel.DataAnnotations.Schema;

namespace api.Entities
{
    public class Note
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string? Id { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User? User { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.Now;
        public DateTime UpdateAt { get; set; }
        public DateTime TimerSet { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string? FilePath { get; set; }
        public bool IsDeleted { get; set; } = false;
        public string ColorBG { get; set; }
        public bool IsDraft { get; set; } = false;

        //one to many
        public ICollection<NoteShare>? NoteShares { get; set; }
    }
}