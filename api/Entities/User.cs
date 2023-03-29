using System.ComponentModel.DataAnnotations.Schema;

namespace api.Entities
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string? Id { get; set; }
        public string Email { get; set; }
        //one to many
    }
}