namespace api.DTOs
{
    public class NoteAdd
    {
        public string userId { get; set; }
        public string title { get; set; }
        public string content { get; set; }
        public string colorBG { get; set; }
        public DateTime timerSet {get;set;}
        
    }
}