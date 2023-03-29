using System.ComponentModel.DataAnnotations.Schema;

namespace api.Entities
{
    public class NoteShare
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User? User { get; set; }
        public string NoteId { get; set; }
        [ForeignKey("NoteId")]
        public virtual Note? Note { get; set; }
        public bool Mode { get; set; }
    }
}